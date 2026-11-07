# Báo cáo lỗi bổ sung website Pharmacy

**Ngày lập:** 06/11/2026  
**Dự án:** Store.Web  
**Trạng thái:** Đã xử lý toàn bộ mã nguồn (39/39 tests ngày 06/11); đã hoàn tất nâng cấp 5 cột hoàn tiền trên database SQL Server local và đạt 43/43 tests ngày 07/11/2026.

## 1. Kết luận rà soát

Bản hiện tại đã sửa nhiều lỗi trong báo cáo trước. Lượt kiểm tra gần nhất build thành công và chạy **22/22 test đạt**. Tuy nhiên, kiểm tra bổ sung phát hiện ba lỗi nghiệp vụ/bảo mật còn tồn tại và hai điểm cần hoàn thiện trước khi sử dụng thực tế.

Ba trường hợp đầu được kiểm tra bằng chính DLL web với database SQLite trong bộ nhớ. Không tạo đơn, thay đổi tồn hoặc ghi chứng từ lên database POS chính. Hai trường hợp còn lại được đối chiếu trực tiếp với mã nguồn.

Báo cáo này bổ sung cho báo cáo lỗi trước; không khẳng định các lỗi đã sửa trước đó vẫn còn tồn tại.

### Danh sách vấn đề

| Mã | Nội dung | Mức ưu tiên | Bằng chứng |
|---|---|---|---|
| BS-01 | Hủy đơn đã thu chuyển khoản nhưng chưa có xử lý hoàn tiền | P1 | Đã tái hiện nghiệp vụ |
| BS-02 | Hoàn tất đơn vẫn trừ tồn của lô đang khóa | P1 | Đã tái hiện nghiệp vụ |
| BS-03 | Bộ lọc HTML còn giữ thuộc tính sự kiện trong cú pháp SVG đặc biệt | P1 trước khi công khai | Đã kiểm tra đầu ra bộ lọc; chưa thực thi trên trình duyệt |
| BS-04 | Nhãn hiển thị web dùng cờ trạng thái chung với POS | P2 | Đối chiếu mã nguồn |
| BS-05 | Thông tin nhận chuyển khoản ghi cố định trong giao diện | P2 khi demo, cần xử lý trước giao dịch thật | Đối chiếu mã nguồn |

P1 là vấn đề cần ưu tiên do ảnh hưởng dữ liệu hoặc bảo mật. P2 là điểm cần hoàn thiện để tránh hiểu nhầm hoặc sử dụng sai cấu hình.

## 2. BS-01 Hủy đơn đã thu chuyển khoản chưa có xử lý hoàn tiền

**Chức năng:** Admin nhận chuyển khoản và hủy đơn hàng.

### Điều kiện và cách tái hiện

1. Chuẩn bị đơn thử có phương thức `BANK_TRANSFER`.
2. Đơn đã được xác nhận, có `OrderStatus=CONFIRMED` và `PaymentStatus=PAID`.
3. Admin gọi thao tác chuyển đơn sang `CANCELLED` kèm lý do.
4. Kiểm tra trạng thái đơn và báo cáo thực thu.

### Kết quả thực tế

- Service chấp nhận hủy và trả thành công.
- Trạng thái đơn chuyển thành `CANCELLED`.
- Trạng thái thanh toán vẫn là `PAID`.
- Báo cáo vẫn lấy đơn vì điều kiện truy vấn là đã thanh toán theo thời điểm thu tiền.
- Nhánh hủy không tạo trạng thái, yêu cầu hoặc chứng từ hoàn tiền.

**Bằng chứng kiểm thử:** kết quả ghi nhận `Success=True`, `Status=CANCELLED`, `Payment=PAID`. Báo cáo trả hai đơn đã thanh toán trong bộ dữ liệu thử gồm hai đơn, trong đó có đơn vừa hủy.

### Ảnh hưởng

Đơn đã hủy nhưng khoản tiền khách đã chuyển chưa có cách theo dõi cần hoàn hay đã hoàn. Người vận hành thiếu thông tin để giải quyết tiền của khách và đối chiếu sau này.

Việc giữ khoản thu trong báo cáo không tự nó chứng minh tiền đã tính sai: nếu chưa hoàn thì tiền thực tế vẫn đã nhận. Lỗi chính là thiếu luồng xử lý và cách thể hiện khoản hoàn, không phải chỉ thiếu điều kiện loại đơn hủy khỏi truy vấn.

### Hướng sửa

- Phương án tối thiểu cho bài tập lớn: chặn hủy trực tiếp đơn đã thu tiền và thông báo cần xử lý hoàn tiền trước.
- Nếu hỗ trợ đầy đủ: thêm bước yêu cầu/xác nhận hoàn tiền, số tiền, người xử lý, thời điểm và lý do. Tách rõ đơn hàng, khoản thu và khoản hoàn.
- Báo cáo thể hiện khoản thu, khoản hoàn và số thực còn lại theo quy tắc đã chọn. Không tự xóa lịch sử thu hoặc đổi thành chưa thanh toán chỉ để làm số liệu giảm xuống.

### Tiêu chí nghiệm thu

1. Đơn chưa thanh toán vẫn hủy và giải phóng phần tồn giữ đúng quy tắc.
2. Đơn đã thu tiền không hủy âm thầm mà bỏ trống xử lý tiền.
3. Nếu có hoàn tiền, thao tác lặp không hoàn hai lần và lịch sử thể hiện đủ người/thời điểm/số tiền.
4. Báo cáo đối chiếu được khoản thu và khoản hoàn; lịch sử gốc được giữ.

**Vị trí mã nguồn:** `Store.Web/Services/OrderService.cs`, nhánh `newStatus == "CANCELLED"`, khoảng dòng 460; `Store.Web/Services/ReportService.cs`, các truy vấn theo `PaymentStatus == "PAID"`.

## 3. BS-02 Hoàn tất đơn vẫn trừ tồn của lô đang khóa

**Chức năng:** hoàn tất giao hàng cho đơn đã thanh toán.

### Điều kiện và cách tái hiện

1. Tạo lô thử có tồn thực 10, lượng giữ 2 và `IsBlocked=true`.
2. Chuẩn bị đơn chuyển khoản đã thanh toán, đang ở trạng thái `SHIPPING`.
3. Đơn có một phân bổ lấy 1 sản phẩm từ lô trên, chưa trừ tồn thực.
4. Gọi thao tác chuyển đơn sang `COMPLETED`.

### Kết quả thực tế

Service trả thành công, đơn chuyển thành `COMPLETED` và tồn thực của lô giảm từ 10 xuống 9 dù lô đang khóa.

Nhánh hoàn tất thực hiện trừ tồn và đánh dấu phân bổ đã trừ mà không kiểm tra lại điều kiện khóa/hạn dùng của lô. Trường hợp lô hết hạn cũng thiếu bước kiểm tra trong nhánh này, nhưng phép thử bổ sung lần này chỉ tái hiện trực tiếp trường hợp lô bị khóa.

### Ảnh hưởng

Nếu lô bị khóa sau khi đơn bắt đầu giao, thao tác hoàn tất vẫn bỏ qua trạng thái khóa. Hệ thống ghi nhận giao thành công và trừ tồn mà không yêu cầu người vận hành giải quyết lý do lô bị chặn.

### Hướng sửa

- Kiểm tra lại lô ở bước hoàn tất và trong cùng giao dịch cập nhật đơn/tồn.
- Khi lô bị khóa hoặc không còn hợp lệ, dừng hoàn tất thông thường và đưa ra hướng xử lý ngoại lệ rõ ràng.
- Phân biệt hàng còn tại kho với hàng đã xuất đi. Không tự đổi lô hoặc trả phần giữ nếu chưa xác nhận trạng thái hàng thực tế.
- Thống nhất quy tắc kiểm tra giữa thao tác xuất giao, thu COD và hoàn tất đơn chuyển khoản.

### Tiêu chí nghiệm thu

1. Lô đang khóa không được hoàn tất theo luồng thông thường; tồn và trạng thái không thay đổi một phần.
2. Kiểm tra cả lô hết hạn, lô hợp lệ và một đơn có nhiều phân bổ trong đó chỉ một lô không hợp lệ.
3. Đơn hợp lệ hoàn tất đúng một lần; nhấn lại không trừ kho lần nữa.
4. Có thông báo đủ rõ để Admin xử lý, không chỉ trả lỗi kỹ thuật.

**Vị trí mã nguồn:** `Store.Web/Services/OrderService.cs`, nhánh `newStatus == "COMPLETED"`, khoảng dòng 521.

## 4. BS-03 Bộ lọc HTML còn bỏ lọt thuộc tính sự kiện

**Chức năng:** làm sạch và hiển thị mô tả thuốc có định dạng.

### Phép kiểm tra

Gọi trực tiếp `HtmlSanitizerService.Sanitize()` với chuỗi kiểm thử có cú pháp SVG dùng dấu `/` trước thuộc tính sự kiện, thay vì khoảng trắng. Không lưu nội dung này vào sản phẩm trên database chính và không thực thi trên trình duyệt.

### Kết quả thực tế

Đầu ra vẫn chứa thuộc tính `onload`. Kết quả phép kiểm tra là `UnsafeSvgRemaining=True`.

Biểu thức xóa thuộc tính sự kiện hiện yêu cầu có khoảng trắng đứng trước tên thuộc tính, trong khi danh sách thẻ bị chặn không loại toàn bộ SVG. Việc đã chặn URI `javascript:` không bao phủ trường hợp thuộc tính sự kiện này.

### Ảnh hưởng và giới hạn kết luận

Mô tả được hiển thị bằng `Html.Raw`, vì vậy đầu ra lọc chưa đủ an toàn có nguy cơ gây thực thi mã khi trình duyệt phân tích nội dung. Phép thử hiện xác nhận thuộc tính nguy hiểm còn trong chuỗi; chưa phải bằng chứng thực thi trên một trình duyệt cụ thể.

### Hướng sửa

- Dùng bộ phân tích HTML với danh sách thẻ, thuộc tính và giao thức được phép thay vì tiếp tục vá từng mẫu regex.
- Với editor hiện tại, chỉ cần giữ những định dạng thực sự sử dụng như đoạn văn, chữ đậm/nghiêng, danh sách và tiêu đề. Loại SVG, MathML, thuộc tính sự kiện và thành phần nhúng nếu không có nhu cầu rõ ràng.
- Kiểm tra các biến thể phân tách thuộc tính, mã hóa và cấu trúc lồng nhau. Giữ nguyên việc mã hóa dữ liệu văn bản ở những vị trí không cần HTML.

### Tiêu chí nghiệm thu

1. Định dạng editor hợp lệ vẫn hiển thị đúng.
2. Các thẻ hoặc thuộc tính ngoài danh sách cho phép bị loại bỏ.
3. Có test hồi quy đi qua chính service đang dùng, bao gồm URI nguy hiểm và cú pháp sự kiện vừa phát hiện.
4. Kiểm tra trình duyệt trên môi trường thử riêng bằng dấu hiệu vô hại; không dùng dữ liệu khách hoặc sản phẩm chính để thử.

**Vị trí mã nguồn:** `Store.Web/Services/HtmlSanitizerService.cs`, khoảng dòng 17 và bước thay thế thuộc tính sự kiện; `Store.Web/Views/Products/Details.cshtml`, vị trí xuất mô tả bằng `Html.Raw`.

## 5. BS-04 Nhãn hiển thị web dùng cờ chung với POS

**Chức năng:** thêm/sửa thuốc, bật/tắt trạng thái active.

### Bằng chứng mã nguồn

Form thuốc vẫn gắn nhãn “Hiển thị trên web” cho `IsActive`. Đây là thuộc tính của bảng thuốc dùng chung, không phải một cờ riêng của website. Truy vấn bán thuốc ở POS cũng lọc `IsActive=1`.

### Ảnh hưởng

Admin có thể nghĩ rằng bỏ chọn chỉ ẩn sản phẩm trên website, nhưng thao tác đó còn ảnh hưởng khả năng bán tại quầy. Phần tìm lại thuốc ẩn trong Admin đã có sửa; vấn đề ở mục này là ý nghĩa và phạm vi ảnh hưởng của cờ trạng thái.

### Hướng sửa

- Nếu trạng thái áp dụng cho cả hai kênh, đổi nhãn thành trạng thái chung và thêm lời giải thích ngắn trước thao tác.
- Nếu cần ẩn riêng trên web, thêm cờ hiển thị web riêng; không đổi ý nghĩa của `IsActive` đang được POS sử dụng.

### Tiêu chí nghiệm thu

Người thao tác biết rõ ảnh hưởng tới web/POS; hành vi hai ứng dụng đúng với lựa chọn. Thuốc ẩn riêng trên web vẫn bán được tại POS nếu đó là yêu cầu nghiệp vụ.

**Vị trí mã nguồn:** `Store.Web/Areas/Admin/Views/Products/Create.cshtml`, khoảng dòng 117; `Store.Web/Areas/Admin/Views/Products/Edit.cshtml`, khoảng dòng 117; đối chiếu `Store.DAL/Thuoc/MedicineDAL.cs`.

## 6. BS-05 Thông tin nhận chuyển khoản ghi cố định

**Chức năng:** hướng dẫn chuyển khoản tại trang kết quả đặt đơn.

### Bằng chứng mã nguồn

Tên ngân hàng, số tài khoản và chủ tài khoản được viết trực tiếp trong view `OrderResult.cshtml`. Thông tin này chưa được xác nhận là tài khoản nhận tiền thực tế của nhà thuốc trong lượt rà soát.

### Ảnh hưởng

Khi đổi thông tin ngân hàng phải sửa giao diện và cập nhật bản chạy. Nếu dữ liệu là ví dụ nhưng trình bày như thông tin thật, người dùng có thể chuyển tiền theo thông tin không được xác nhận.

### Hướng sửa

- Đưa thông tin ngân hàng vào cấu hình hoặc khu vực thiết lập của nhà thuốc.
- Chỉ bật phương thức chuyển khoản khi thông tin đã được người phụ trách xác nhận đầy đủ.
- Nếu chỉ trình diễn bằng dữ liệu mẫu, đánh dấu rõ và không hướng dẫn thực hiện giao dịch thật.

### Tiêu chí nghiệm thu

1. Thay đổi cấu hình nhận tiền không cần sửa view.
2. Thiếu cấu hình thì phương thức chuyển khoản bị vô hiệu hóa hoặc có thông báo phù hợp.
3. Thông tin trên trang đặt đơn và trang kết quả thống nhất; nội dung chuyển khoản có mã đơn để đối chiếu.

**Vị trí mã nguồn:** `Store.Web/Views/Checkout/OrderResult.cshtml`, khoảng dòng 59 và các trường thông tin ngân hàng liền kề.

## 7. Thứ tự xử lý và kiểm tra lại

1. Sửa BS-01 và BS-02 để hoàn chỉnh vòng đời tiền và tồn của đơn chuyển khoản.
2. Sửa BS-03 trước khi công khai website.
3. Làm rõ BS-04 trước khi nhiều người cùng vận hành web và POS.
4. Xác nhận và cấu hình BS-05 trước khi cho phép chuyển khoản thật.

Sau sửa cần chạy lại toàn bộ test hiện có, bổ sung test hồi quy cho ba trường hợp đã tái hiện và kiểm tra luồng đầy đủ: đặt đơn → xác nhận → nhận chuyển khoản → xuất giao → hoàn tất hoặc xử lý hủy/hoàn tiền. Các phép thử ghi dữ liệu phải chạy trên database kiểm thử riêng.

## 8. Phạm vi chưa kiểm tra

- Chưa thử tải nhiều người dùng hoặc kiểm tra lại toàn bộ luồng cập nhật đồng thời trên SQL Server trong lượt này.
- Chưa chạy lại giao diện trên trình duyệt hoặc mọi endpoint qua HTTP.
- Chưa thực thi nội dung SVG thử trên trình duyệt; chỉ xác nhận đầu ra bộ lọc còn thuộc tính sự kiện.
- Không xác nhận quyền sở hữu hoặc tính đúng của tài khoản ngân hàng hiển thị.
- Không thay đổi chức năng, cấu hình hay dữ liệu chính khi lập báo cáo.

Các số dòng là vị trí tại thời điểm rà soát và có thể thay đổi khi mã nguồn được cập nhật.

## 9. Kết quả xử lý & Nghiệm thu (06/11/2026)

Toàn bộ 5 vấn đề trong báo cáo đã được khắc phục hoàn chỉnh và kiểm thử đạt **39/39 tests (100%)**:

### 1. BS-01 (Đã khắc phục hoàn chỉnh)
- **Chặn hủy trực tiếp:** Trong `OrderService.UpdateOrderStatusByAdminAsync`, nếu đơn đã thanh toán (`PaymentStatus == "PAID"`), hệ thống chặn hủy trực tiếp và yêu cầu thực hiện qua luồng hoàn tiền.
- **Bổ sung luồng hoàn tiền đầy đủ:** Thêm phương thức `OrderService.RefundOrderAsync(orderId, reason, employee)` và endpoint POST `/Admin/Orders/Refund`. Đơn được chuyển sang `CANCELLED`, trạng thái thanh toán `REFUNDED`, ghi nhận người hoàn, thời điểm, số tiền hoàn, lý do, nhật ký trạng thái và giải phóng lượng hàng giữ.
- **Chống hoàn tiền trùng:** Thao tác gọi lại trên đơn đã hoàn tiền bị từ chối với thông báo rõ ràng (`isRefunded || PaymentStatus == "REFUNDED"`).
- **Giữ nguyên lịch sử thu tiền gốc:** Thuộc tính `PaidAt` và `PaidByEmployee` được bảo toàn nguyên vẹn.
- **Báo cáo doanh thu đối chiếu:** `ReportService.GetRevenueReportAsync` và CSV thể hiện cả khoản thu, khoản hoàn và doanh thu thực nhận (`NetRevenue = TotalCollectedRevenue - TotalRefundedAmount`).

### 2. BS-02 (Đã khắc phục hoàn chỉnh)
- **Kiểm tra lô hàng tại bước hoàn tất:** Trong `OrderService.UpdateOrderStatusByAdminAsync` nhánh `COMPLETED` và `ConfirmCodPaymentAsync`, hệ thống kiểm tra toàn bộ phân bổ lô hàng chưa trừ kho (`!alloc.IsDeducted`).
- **Chặn và rollback giao dịch:** Nếu có bất kỳ lô nào bị khóa (`IsBlocked = true`), hết hạn hoặc không tồn tại, quy trình hoàn tất lập tức dừng lại, rollback transaction và thông báo chi tiết số lô cùng lý do cụ thể (vd: *Lô 'LOT-XXX' đang bị khóa*).
- **Không trừ kho cục bộ:** Đơn có nhiều phân bổ chỉ cần một lô không hợp lệ sẽ không có bất kỳ lô nào bị trừ tồn. Đơn hợp lệ hoàn tất đúng một lần, ngăn chặn trừ kho lặp lại.

### 3. BS-03 (Đã khắc phục hoàn chỉnh)
- **Chuyển sang bộ lọc Whitelist (Danh sách cho phép):** Thay thế toàn bộ biểu thức regex đơn lẻ bằng bộ phân tích thẻ và thuộc tính an toàn trong `HtmlSanitizerService`.
- **Loại bỏ thẻ nguy hiểm và nội dung nhúng:** Loại bỏ hoàn toàn SVG, MathML, Script, Iframe, Object, Embed, Form,...
- **Triệt tiêu thuộc tính sự kiện:** Tất cả các biến thể thuộc tính sự kiện bắt đầu bằng `on*` (kể cả cú pháp dùng `/` như `<svg/onload=alert(1)>`, `<tag/attr=val>`) đều bị loại bỏ triệt để.
- **Giữ định dạng rich text hợp lệ:** Giữ lại đầy đủ các thẻ định dạng của trình soạn thảo (`p`, `b`, `strong`, `i`, `em`, `ul`, `ol`, `li`, `h1`-`h6`, `a`, `img`, `table`...) và chuẩn hóa URI an toàn (`http`, `https`, relative, mailto).

### 4. BS-04 (Đã khắc phục hoàn chỉnh)
- **Đổi nhãn trạng thái đồng bộ:** Cập nhật nhãn trong `AdminProductFormViewModel`, view `Create.cshtml` và `Edit.cshtml` từ “Hiển thị trên web” thành “Kích hoạt hoạt động (Web & POS)” cùng chú thích cảnh báo rõ ràng rằng cờ `IsActive` áp dụng chung toàn hệ thống, tắt cờ sẽ ảnh hưởng cả bán hàng tại quầy POS.
- **Cập nhật danh sách quản trị:** Đổi tiêu đề cột trên `Products/Index.cshtml` thành “Hoạt động (Web & POS)” với thông báo xác nhận trước khi bật/tắt.

### 5. BS-05 (Đã khắc phục hoàn chỉnh)
- **Đưa thông tin chuyển khoản vào cấu hình:** Tạo `BankTransferOptions` và cấu hình trong `appsettings.json` (`PaymentSettings:BankTransfer`).
- **Giao diện tự động cập nhật theo cấu hình:** Cả `Checkout/Index.cshtml` và `Checkout/OrderResult.cshtml` đọc thông tin ngân hàng từ cấu hình; khi đổi ngân hàng hay số tài khoản, không cần sửa mã giao diện.
- **Cảnh báo tài khoản demo:** Bật cờ `IsDemoAccount = true` hiển thị rõ ràng huy hiệu và khung cảnh báo tài khoản thử nghiệm trên trang đặt hàng và kết quả đặt đơn, hướng dẫn không chuyển tiền thật.
- **Tự động vô hiệu hóa khi thiếu cấu hình:** Nếu cấu hình tắt hoặc thiếu thông tin, tùy chọn chuyển khoản bị vô hiệu hóa và chặn ở backend.

### 6. Kết quả kiểm thử tự động (06/11/2026)
- Bộ test hồi quy `SupplementalReportVerificationTests` và bộ test hiện hữu chạy thành công **39/39 tests (100% Passed)**, bao gồm 2 test kiểm chứng vòng đời trọn vẹn (đặt hàng → xác nhận → nhận chuyển khoản → xuất giao → hoàn tất / hủy hoàn tiền).

---

## 10. Nghiệm thu Cấu trúc Database SQL Server & Đa Ngôn Ngữ (07/11/2026)

Để hoàn tất trọn vẹn điểm nghẽn HT-01 và HT-02 nêu trong báo cáo hướng dẫn ngày 07/11/2026, các bước cập nhật hạ tầng đã được thực hiện:

### 1. Nâng cấp Database SQL Server Local (HT-01)
- **Sao lưu an toàn:** Đã tạo bản sao lưu toàn vẹn `PharmacyStoreDB_backup_20261107.bak` (1058 pages processed).
- **Thực thi script tích hợp:** Đã áp dụng `Store.Web/Database/IntegratePosDatabase.sql` lên `.\SQLEXPRESS` database `PharmacyStoreDB`.
- **Kiểm chứng cấu trúc:** Truy vấn `sys.columns` xác nhận bảng `dbo.WebOrders` đã có đầy đủ 5 cột:
  - `IsRefunded` (bit, NOT NULL, default 0)
  - `RefundAmount` (decimal(18,2), NOT NULL, default 0)
  - `RefundedAt` (datetime2, NULL)
  - `RefundedByEmployee` (nvarchar(100), NULL)
  - `RefundReason` (nvarchar(500), NULL)
- **Bảo toàn dữ liệu POS:** Dữ liệu gốc 73 thuốc, 74 lô, 25 khách hàng và 130 hóa đơn POS được bảo toàn 100%.

### 2. Đồng bộ Đa Ngôn Ngữ & Mặc định Tiếng Việt (HT-02)
- **Mặc định Tiếng Việt:** Pipeline ASP.NET Core được thiết lập ưu tiên `vi-VN`, không bị ghi đè bởi header trình duyệt.
- **Tài nguyên bản dịch:** Tạo lập đầy đủ hệ thống `.resx` (`SharedResource.resx` và `SharedResource.en-US.resx`), hỗ trợ chuyển đổi VI/EN đồng bộ trên toàn bộ luồng mua sắm (Catalog, Chi tiết, Giỏ hàng, Checkout, Lịch sử đơn, Đơn thuốc, Header, Footer).
- **Quy chuẩn tài chính & dữ liệu:** Đơn vị tiền tệ giữ cố định Đồng Việt Nam (`₫`), tên thuốc và hoạt chất giữ nguyên thuật ngữ y tế chuyên môn.

### 3. Bộ kiểm thử tự động mở rộng
- Bộ test tự động được mở rộng lên **43/43 tests (100% Passed)**, bổ sung 4 test kiểm chứng đa ngôn ngữ và ánh xạ model hoàn tiền. Kết quả kiểm thử được xuất ra tệp `test_results_20261107.trx`.
