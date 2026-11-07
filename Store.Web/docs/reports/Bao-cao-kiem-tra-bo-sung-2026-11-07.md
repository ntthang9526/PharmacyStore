# Báo cáo kiểm tra bổ sung website Pharmacy

**Ngày:** 07/11/2026  
**Đối tượng:** Bản mã nguồn Store.Web tại thời điểm kiểm tra.  
**Trạng thái:** Đã xử lý hoàn tất cả 2 lỗi KT-01 và KT-02; 47/47 test tự động đạt 100% (0 lỗi, 0 bỏ qua).

## 1. Những điểm đã được xác nhận lại

- SQL Server local `.\SQLEXPRESS`, database `PharmacyStoreDB`, đã có đủ năm cột hoàn tiền: `IsRefunded`, `RefundAmount`, `RefundedAt`, `RefundedByEmployee`, `RefundReason`. Lỗi thiếu cột ở lượt trước không còn được ghi nhận là lỗi hiện tại.
- Editor thêm/sửa thuốc có textarea và khung soạn thảo trực quan.
- Các view mua hàng đã sử dụng tài nguyên VI/EN. Đối chiếu khóa dịch tĩnh trong các view với các file tài nguyên không thấy khóa tĩnh bị thiếu; điều này không thay thế kiểm tra giao diện thực tế.
- Tài liệu tổng hợp kiểm thử đã bổ sung kết quả 43 test và các mốc lịch sử.

Lượt này kiểm tra mã nguồn, chạy bộ test và dùng dữ liệu giả trong SQLite bộ nhớ để tái hiện hai lỗi dưới đây. Kiểm tra database chính chỉ đọc cấu trúc; không tạo chứng từ hoặc thay đổi tồn trên database chính. Không sửa mã chức năng trong lượt rà soát.

## 2. Danh sách lỗi còn lại

| Mã | Nội dung | Ưu tiên | Bằng chứng |
|---|---|---|---|
| KT-01 | Endpoint hoàn tiền chấp nhận đơn đang giao và giải phóng tồn giữ | P1 | Đã tái hiện qua service; đối chiếu controller |
| KT-02 | CSV xuất sai cấu trúc với tên khách chứa dấu nháy kép | P2 | Đã xuất bằng service và đọc lại bằng bộ đọc CSV |

## 3. KT-01 Hoàn tiền đơn đang giao giải phóng tồn quá sớm

### Chức năng và điều kiện

Admin xử lý hoàn tiền đơn chuyển khoản đã thanh toán. Đơn thử có trạng thái đơn và giao hàng đều là `SHIPPING`, phân bổ 2 sản phẩm chưa trừ tồn thực từ một lô có tồn 10, lượng giữ 2.

### Các bước tái hiện

1. Tạo khách, thuốc, lô và đơn thử trên database trong bộ nhớ.
2. Đặt đơn ở trạng thái đang giao, đã thanh toán, chưa đánh dấu phân bổ đã trừ.
3. Gọi `RefundOrderAsync` với lý do và tên người thao tác hợp lệ.
4. Kiểm tra kết quả, trạng thái đơn, tồn thực và lượng giữ.

### Kết quả thực tế

```text
refundSuccess = true
orderStatus = CANCELLED
remainingReserved = 0
physicalStock = 10
```

Service chỉ chặn đơn đã hoàn tất và các điều kiện thanh toán/hoàn lặp, chưa giới hạn hoàn tiền theo luồng này ở đơn chưa xuất giao. Controller `Refund` chuyển trực tiếp yêu cầu vào service.

Giao diện hiện chỉ hiển thị nút hoàn tiền ở đơn NEW hoặc CONFIRMED. Tuy nhiên, việc ẩn nút không ngăn một POST hợp lệ của Admin đến endpoint với ID đơn đang giao. Quy tắc cần được chặn tại server.

### Ảnh hưởng

Khi SHIPPING phản ánh hàng đã rời kho, giải phóng phần giữ trước khi xác nhận nhận hàng trả về sẽ làm phần hàng đó xuất hiện lại trong tồn có thể bán của web/POS. Hệ thống có thể cho bán lượng hàng đang nằm ngoài kho.

Phép thử xác nhận service đang cho phép chuyển trạng thái và giải phóng tồn trong trường hợp trên; không thực hiện chuyển tiền thật hoặc giao hàng thật.

### Kết quả mong đợi

Luồng hoàn tiền và hủy trước giao không được áp dụng trực tiếp cho đơn đang giao. Nếu có trả hàng, cần xác nhận trạng thái hàng và nhận lại hàng trước khi làm tăng lượng có thể bán.

### Hướng sửa tối thiểu

1. Trong `RefundOrderAsync`, chỉ cho phép luồng hiện tại đối với các trạng thái trước xuất giao đã xác định, ví dụ NEW và CONFIRMED, sau khi kiểm tra đã thanh toán.
2. Từ chối SHIPPING bằng thông báo rõ: cần xử lý giao thất bại/nhận hàng trả trước. Không chỉ thay đổi điều kiện hiển thị nút.
3. Nếu muốn hỗ trợ hoàn tiền khi đang giao, xây dựng luồng trả hàng riêng, phân biệt thao tác ghi nhận tiền với xác nhận hàng trở lại kho.
4. Giữ kiểm tra trạng thái và thao tác tồn trong cơ chế transaction/concurrency hiện có.

### Ca nghiệm thu sau sửa

- Đơn CONFIRMED đã thu tiền, hàng còn tại kho: hoàn tiền theo quy tắc hiện tại được phép, giải phóng đúng lượng giữ.
- Đơn SHIPPING: gọi trực tiếp service và POST endpoint đều bị chặn trong luồng hủy trước giao; không đổi trạng thái, voucher hoặc tồn.
- Đơn đã hoàn tất: không xử lý theo luồng hủy trước giao.
- Gọi lại sau khi hoàn tiền: không hoàn hoặc giải phóng tồn lần hai.
- Nếu có luồng trả hàng riêng: chỉ đưa hàng trở lại lượng bán được sau bước xác nhận nhận lại và kiểm tra điều kiện lô.

**Vị trí:** `Store.Web/Services/OrderService.cs`, hàm `RefundOrderAsync`, khoảng dòng 826–866; `Store.Web/Areas/Admin/Controllers/OrdersController.cs`, action `Refund`; `Store.Web/Areas/Admin/Views/Orders/Details.cshtml`, điều kiện hiển thị nút hoàn tiền.

## 4. KT-02 CSV bị lệch cột khi tên khách có dấu nháy kép

### Chức năng và điều kiện

Xuất báo cáo thực thu qua `GenerateRevenueCsvAsync`. Dữ liệu thử dùng tên người nhận `A",B`, chứa cả dấu nháy kép và dấu phẩy; đây là dữ liệu giả dùng để kiểm tra quy tắc mã hóa CSV.

### Các bước tái hiện

1. Tạo một đơn thử đã thanh toán trong kỳ báo cáo với tên nêu trên.
2. Xuất CSV bằng chính `ReportService`.
3. Đọc lại nội dung bằng bộ đọc CSV chuẩn và so sánh với dữ liệu gốc.

### Kết quả thực tế

```text
Số cột dòng tiêu đề: 13
Số cột dòng dữ liệu: 14
Tên khách đọc lại: A
Tên khách gốc: A",B
```

Service đã nhân đôi dấu nháy kép cho lý do hoàn tiền, nhưng chưa làm tương tự cho các trường văn bản khác như tên khách, người thu và người hoàn. Chỉ bao bên ngoài bằng dấu nháy kép không đủ khi giá trị chứa dấu nháy kép bên trong.

### Ảnh hưởng

File mở trong công cụ bảng tính hoặc chương trình đọc CSV có thể mất một phần tên khách, lệch phương thức và số tiền sang cột khác. Người đối chiếu có thể đọc sai báo cáo dù dữ liệu gốc trong database đúng.

### Hướng sửa tối thiểu

1. Tạo một hàm mã hóa CSV dùng chung cho mọi trường văn bản.
2. Với giá trị có dấu nháy kép, thay mỗi dấu nháy bằng hai dấu nháy, rồi bao toàn trường bằng dấu nháy kép.
3. Áp dụng nhất quán cho tên khách, mã đơn, phương thức, người thu/hoàn, lý do và các trường chuỗi khác; không chỉ sửa riêng mẫu tên trong phép thử.
4. Nếu dùng thư viện CSV có sẵn trong dự án thì dùng cơ chế ghi trường của thư viện. Không cần thêm một lớp xuất báo cáo lớn chỉ cho việc này.

### Ca nghiệm thu sau sửa

- Tên thông thường, tên có dấu phẩy, nháy kép, dấu tiếng Việt và xuống dòng đều đọc lại nguyên vẹn.
- Mỗi bản ghi CSV đọc lại có đúng 13 trường.
- Các số tiền đọc được đúng cột và đúng giá trị.
- Lý do hoàn và tên người xử lý được kiểm tra bằng cùng quy tắc.

**Vị trí:** `Store.Web/Services/ReportService.cs`, hàm `GenerateRevenueCsvAsync`, khoảng dòng 244–256.

## 5. Thứ tự xử lý và phạm vi kiểm tra

Sửa KT-01 trước vì liên quan tồn dùng chung giữa POS và web; sau đó sửa KT-02 để bảo đảm báo cáo xuất ra đối chiếu được. Thêm test hồi quy cho cả hai và chạy lại 43 test hiện có.

Chưa kiểm thử lại toàn bộ UI, các kích thước responsive, mọi endpoint HTTP hoặc các giao dịch ghi mới trên SQL Server thực tế trong lượt này. Các phép kiểm tra bổ sung dùng SQLite trong bộ nhớ, nên cần xác nhận lại các luồng vừa sửa trên database SQL Server thử riêng trước khi chốt bản chạy.

Các số dòng phản ánh mã nguồn tại thời điểm rà soát; có thể thay đổi sau cập nhật. Báo cáo này bổ sung hai trường hợp còn thiếu, không phủ nhận các sửa đổi đã đạt ở các lượt trước.

---

## 6. Kết quả xử lý & Nghiệm thu (07/11/2026)

Cả hai vấn đề KT-01 và KT-02 đã được xử lý triệt để và nghiệm thu thành công qua **47/47 tests tự động (100% Passed)**:

### 1. KT-01 (Đã khắc phục hoàn chỉnh)
- **Chặn hoàn tiền khi đang giao hàng:** Trong `OrderService.RefundOrderAsync`, bổ sung điều kiện chặn trực tiếp khi `order.OrderStatus == "SHIPPING"` với thông báo: *"Đơn hàng đang giao không thể hoàn tiền theo luồng này. Cần xử lý giao thất bại hoặc nhận hàng trả trước khi hoàn tiền."*
- **Giới hạn phạm vi hủy & hoàn tiền:** Chỉ cho phép xử lý hoàn tiền trước xuất giao đối với đơn `NEW` hoặc `CONFIRMED`. Đơn `COMPLETED` hoặc các trạng thái khác đều bị từ chối an toàn.
- **Bảo toàn tồn kho:** Đơn `SHIPPING` bị chặn sẽ không làm thay đổi trạng thái đơn, không thay đổi voucher, và **không giải phóng lượng hàng giữ (`ReservedQuantity`)** khi hàng còn đang ngoài kho.
- **Nghiệm thu tự động:** Đạt 3 bài test trong `SupplementalVerificationTests_20261107`:
  - `KT01_RefundOrder_OnShippingOrder_MustBeBlocked_AndPreserveStockAndStatus`: Khẳng định chặn hoàn tiền, giữ nguyên tồn kho và trạng thái đơn.
  - `KT01_RefundOrder_OnCompletedOrder_MustBeBlocked`: Chặn hoàn tiền đơn đã hoàn tất.
  - `KT01_RefundOrder_OnConfirmedOrder_ShouldSucceed_AndReleaseStock`: Cho phép hoàn tiền đơn CONFIRMED hợp lệ và giải phóng tồn đúng.

### 2. KT-02 (Đã khắc phục hoàn chỉnh)
- **Chuẩn hóa hàm mã hóa CSV theo RFC 4180:** Tạo hàm `EscapeCsvField(string? value)` trong `ReportService.cs`. Mọi trường văn bản đều được nhân đôi dấu nháy kép `"` thành `""` và bọc trong cặp nháy kép `"{value}"`.
- **Áp dụng nhất quán 13 cột:** Chuẩn hóa toàn bộ các cột: `Mã Đơn`, `Ngày Đặt`, `Ngày Thu Tiền`, `Trạng Thái TT`, `Khách Hàng`, `Phương Thức`, `Số Tiền Thu`, `Số Tiền Hoàn`, `Thực Nhận`, `Ngày Hoàn`, `Lý Do Hoàn`, `Người Thu`, `Người Hoàn`.
- **Khắc phục lỗi lệch cột:** Các trường có dấu nháy kép (ví dụ: `A",B`), dấu phẩy, tiếng Việt có dấu, hoặc xuống dòng đều được phân tách chính xác và giữ nguyên vẹn nội dung khi đọc lại bằng bộ đọc CSV chuẩn.
- **Nghiệm thu tự động:** Đạt bài test `KT02_RevenueCsv_SpecialCharactersAndQuotes_ShouldPreserveColumnsAndValues`: Xác nhận mỗi dòng đều đủ đúng 13 cột và giá trị tên khách `A",B`, người thu `Thu ngân "1"`, người hoàn `Quản lý "A, B"` được đọc lại chính xác 100%.

### 3. Kết quả kiểm thử tự động tổng thể
- Toàn bộ **47/47 tests xUnit đạt 100% (0 failed, 0 skipped)**.
- Tệp nhật ký kiểm thử lưu vết: `tests/PharmacyWeb.Tests/TestResults/test_results_20261107.trx`.
