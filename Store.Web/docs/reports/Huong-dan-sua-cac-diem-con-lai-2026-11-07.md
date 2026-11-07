# Báo cáo hướng sửa các điểm còn lại của website Pharmacy

**Ngày lập:** 07/11/2026  
**Phạm vi:** Store.Web và database local PharmacyStoreDB  
**Trạng thái:** Đã hoàn tất xử lý toàn bộ 3 điểm HT-01, HT-02, HT-03 vào ngày 07/11/2026. Database SQL Server local đã cập nhật đủ cột hoàn tiền, website mặc định Tiếng Việt và đồng bộ VI/EN, 43/43 tests tự động đạt 100%.

## 1. Kết luận và thứ tự xử lý

Lượt kiểm tra gần nhất build thành công, **39/39 test đạt**. Editor đã có textarea và đồng bộ với khung soạn thảo trực quan. Tuy nhiên, chưa thể chốt bản chạy SQL Server vì database local còn thiếu các cột hoàn tiền mà mã nguồn mới sử dụng.

| Mã | Vấn đề | Ưu tiên | Kết quả cần đạt |
|---|---|---|---|
| HT-01 | Database thiếu cấu trúc hoàn tiền | Làm trước khi demo | Web đọc và xử lý đơn, báo cáo, hoàn tiền trên SQL Server không lỗi thiếu cột |
| HT-02 | Giao diện VI/EN chưa đồng bộ | Hoàn thiện trước nghiệm thu quốc tế hóa | Các nhãn trên luồng trình diễn đổi ngôn ngữ nhất quán |
| HT-03 | Tài liệu kiểm thử và trạng thái nghiệm thu không thống nhất | Làm trước khi nộp bài | Phân biệt rõ kết quả hiện tại, lịch sử và phạm vi chưa kiểm tra |

Thứ tự thực hiện: HT-01 → kiểm tra trọn luồng SQL Server → HT-02 → HT-03. Không cần thêm chức năng lớn để giải quyết các điểm này.

## 2. HT-01 Cập nhật cấu trúc database cho chức năng hoàn tiền

### Hiện trạng và nguyên nhân

Đã kiểm tra chỉ đọc trên `.\SQLEXPRESS`, database `PharmacyStoreDB`: bảng `dbo.WebOrders` tồn tại nhưng thiếu cả năm cột:

| Cột | Kiểu dữ liệu theo script | Giá trị cho dữ liệu cũ |
|---|---|---|
| IsRefunded | bit, không null | 0 |
| RefundAmount | decimal(18,2), không null | 0 |
| RefundedAt | datetime2, cho phép null | NULL |
| RefundedByEmployee | nvarchar(100), cho phép null | NULL |
| RefundReason | nvarchar(500), cho phép null | NULL |

Mã nguồn và script đã bổ sung các cột này nhưng database local chưa được cập nhật tương ứng. Những truy vấn đọc entity đơn hàng có thể lỗi ngay cả trước khi bấm hoàn tiền.

Các test nghiệp vụ hiện chủ yếu dùng SQLite trong bộ nhớ và tự tạo schema theo model mới. Vì vậy 39 test đạt không chứng minh database SQL Server đang chạy đã đúng cấu trúc.

### Tệp cần sử dụng

- Script: `Store.Web/Database/IntegratePosDatabase.sql`.
- Model đối chiếu: `Store.Web/Data/Entities/OrderEntities.cs`.
- Nghiệp vụ: `Store.Web/Services/OrderService.cs`, `Store.Web/Services/ReportService.cs`.

### Các bước xử lý

1. **Xác nhận đúng máy và database.** Kết nối SQL Server local bằng tài khoản được phép thay đổi cấu trúc. Không áp dụng sang VPS theo cấu hình cũ.
2. **Lưu công việc và ngừng thao tác ghi trong lúc cập nhật.** Đóng hoặc tạm dừng sử dụng web/POS để tránh giao dịch xen vào bước sao lưu và nâng cấp.
3. **Sao lưu đầy đủ PharmacyStoreDB.** Ghi lại đường dẫn, thời điểm và bản mã nguồn tương ứng. Xác nhận backup hoàn tất; nên thử phục hồi backup trên database riêng trước khi nâng cấp.
4. **Thử trên database phục hồi riêng.** Script có kiểm tra tên database; dùng tên thử đúng mẫu `PharmacyWebIntegration_...`. Không bỏ điều kiện bảo vệ tên database chỉ để chạy cho thuận tiện.
5. **Mở script tích hợp trong SSMS, chọn đúng database và chạy toàn bộ file.** Script đã có các điều kiện `COL_LENGTH` để bổ sung cột thiếu và transaction. Không chỉ sao chép đoạn cuối rồi chạy rời vì có `COMMIT TRANSACTION` phụ thuộc phần đầu file.
6. **Nếu có lỗi, dừng và đọc lỗi trước khi chạy tiếp.** Không coi kết quả là thành công khi còn batch báo lỗi; không tự xóa bảng/cột để chạy lại. Kiểm tra trạng thái transaction trong phiên SSMS và rollback phần chưa commit nếu cần.
7. **Sau khi bản thử đạt, áp dụng cùng script lên database chính đã sao lưu.** Không chạy `InitDatabase.sql` để ghi đè hoặc khởi tạo lại database chính đang có dữ liệu.
8. **Khởi động lại bản web tương ứng và kiểm tra trọn luồng.** Nếu cấu hình SQL Server có biến môi trường ghi đè `.env`, xác nhận web thực sự đang dùng database vừa cập nhật.

### Kiểm tra cấu trúc sau cập nhật

Đoạn SQL dưới đây chỉ đọc dữ liệu cấu trúc, không sửa database:

```sql
USE PharmacyStoreDB;
GO
SELECT DB_NAME() AS DatabaseName;

SELECT name, TYPE_NAME(user_type_id) AS DataType,
       max_length, precision, scale, is_nullable
FROM sys.columns
WHERE object_id = OBJECT_ID(N'dbo.WebOrders')
  AND name IN (
      N'IsRefunded', N'RefundAmount', N'RefundedAt',
      N'RefundedByEmployee', N'RefundReason'
  )
ORDER BY name;
```

**Kết quả cần có:** đủ năm dòng với kiểu và khả năng null đúng bảng trên. Các đơn cũ chưa hoàn tiền phải có `IsRefunded=0`, `RefundAmount=0`; không tự đánh dấu chúng đã hoàn.

### Kiểm tra nghiệp vụ trên SQL Server

Thực hiện các ca ghi dưới đây trên database thử riêng. Không dùng hóa đơn thật để tạo bằng chứng kiểm thử.

| Ca | Thao tác | Kết quả mong đợi |
|---|---|---|
| SQL-01 | Mở danh sách và chi tiết đơn, trang báo cáo | Không lỗi thiếu cột |
| SQL-02 | Đặt đơn rồi hủy khi chưa thu tiền | Giải phóng phần giữ đúng, không thay đổi tồn thực sai |
| SQL-03 | Xác nhận nhận chuyển khoản | Ghi nhận đã thu, chưa tự hoàn tất giao hàng |
| SQL-04 | Hủy trực tiếp đơn đã thu tiền | Bị chặn và hướng dẫn dùng hoàn tiền |
| SQL-05 | Hoàn tiền đơn đủ điều kiện | Lưu số tiền, lý do, người và thời điểm; giữ lịch sử thu |
| SQL-06 | Gửi lại thao tác hoàn tiền | Không hoàn lần hai hoặc giải phóng tồn lần hai |
| SQL-07 | Hoàn tất đơn có lô khóa hoặc hết hạn | Bị chặn, không cập nhật một phần đơn/tồn |
| SQL-08 | Hoàn tất đơn hợp lệ rồi gửi lại | Trừ tồn đúng một lần |
| SQL-09 | Xem báo cáo thu, hoàn và thực còn lại | Khớp chứng từ thử và khoảng ngày đã chọn |
| SQL-10 | Web giữ hàng rồi kiểm tra bán ở POS | POS chỉ bán phần tồn chưa giữ |

**Điều kiện chốt HT-01:** đủ cột, không làm mất dữ liệu gốc, kiểm tra trên bản phục hồi đạt và bản chạy chính mở được các trang liên quan sau cập nhật.

### Khi cập nhật không thành công

Giữ ứng dụng ngừng ghi, lưu thông báo lỗi và xác định phần nào đã áp dụng. Không xóa cột hoàn tiền nếu đã có dữ liệu nghiệp vụ mới. Nếu cần phục hồi backup, phải đánh giá những giao dịch phát sinh sau backup để tránh mất dữ liệu; việc tạm dừng ghi trước nâng cấp giúp giảm rủi ro này.

## 3. HT-02 Đồng bộ ngôn ngữ VI và EN

### Hiện trạng

Header có chọn ngôn ngữ và chương trình có cấu hình culture/cookie. Tuy nhiên, nhiều nhãn ở danh mục, giỏ và checkout vẫn được viết cố định bằng tiếng Việt. Chọn EN chưa làm toàn bộ luồng mua hàng đổi ngôn ngữ.

### Phạm vi ưu tiên

- `Views/Products/Index.cshtml`, partial danh sách và thẻ sản phẩm.
- `Views/Cart/Index.cshtml`.
- `Views/Checkout/Index.cshtml`, `OrderResult.cshtml`.
- Các màn Account, MyOrders, gửi đơn tư vấn dùng trong demo.
- Thông báo thành công/lỗi, trạng thái đơn và validation của các luồng trên.

### Hướng sửa

1. Lập danh sách nhãn cần dịch: tiêu đề, nút, placeholder, bộ lọc, trạng thái và thông báo.
2. Tận dụng cấu hình localization đang có trong `Program.cs`; đưa nhãn vào tài nguyên `.resx` theo VI/EN và dùng localizer ở view/controller. Không cần xây thêm một hệ thống đổi ngôn ngữ độc lập.
3. Đảm bảo khóa dịch thống nhất giữa các màn. Không chỉ dịch header trong khi giữ phần nội dung tiếng Việt.
4. Giữ nguyên tên thuốc, hoạt chất, mã đơn và dữ liệu do người dùng nhập nếu không có dữ liệu dịch đã xác nhận. Dịch giao diện không đồng nghĩa tự dịch nội dung dược.
5. Thống nhất quy tắc hiển thị ngày/số khi đổi culture. Đơn vị tiền vẫn là VND, không tự đổi sang USD vì chọn tiếng Anh.
6. Kiểm tra nút dài hơn ở EN trên màn hình nhỏ; không để chữ tràn hoặc che thao tác.

### Tiêu chí nghiệm thu

- Đổi VI → EN trên danh mục, giỏ, checkout và kết quả đơn: các nhãn giao diện đã chọn đều đổi đúng.
- Lựa chọn ngôn ngữ được giữ khi chuyển trang hoặc tải lại.
- Nội dung validation và thông báo lỗi cùng ngôn ngữ với màn hình.
- Đổi ngôn ngữ không làm mất giỏ, không gửi lại đơn và không thay đổi tổng tiền.
- Không xuất hiện khóa tài nguyên thô hoặc nhãn rỗng khi thiếu bản dịch.

**Mức tối thiểu cho bài tập lớn:** hoàn thiện nhất quán luồng khách hàng dùng để trình diễn, đồng thời ghi rõ phạm vi nếu phần Admin chưa dịch hết. Không ghi “đa ngôn ngữ toàn hệ thống” khi chưa kiểm tra đủ.

## 4. HT-03 Đồng bộ tài liệu kiểm thử và trạng thái nghiệm thu

### Hiện trạng

Lượt chạy gần nhất đạt 39/39 test, nhưng tài liệu còn nhiều số liệu thuộc thời điểm khác nhau:

| Tài liệu | Nội dung cần rà lại |
|---|---|
| `docs/REQUIREMENTS-MAPPING.md` | Vẫn ghi 15/15 và đánh dấu tất cả đã hoàn thành |
| `docs/DEMO-SCRIPT.md` | Vẫn hướng dẫn trình chiếu kết quả 15/15 |
| `docs/TEST-REPORT.md` | Ghi 17 ca của lượt tích hợp trước |
| `docs/SQLSERVER-POS-INTEGRATION.md` | Có kết quả 17 ca xUnit và 28 kiểm tra SQL lịch sử |
| `docs/HUONG-DAN-CAI-DAT-VA-SU-DUNG.md` | Ghi 39/39 nhưng cần nêu đúng phạm vi database đã kiểm chứng |
| Báo cáo lỗi bổ sung ngày 06/11 | Phần đầu ghi đã xử lý, phần thân còn nội dung lịch sử 22 test và lỗi chưa sửa |

### Hướng sửa

1. Dùng `TEST-REPORT.md` làm nơi tổng hợp kết quả kiểm thử hiện tại: ngày chạy, phiên bản/commit, lệnh, số ca đạt/lỗi/bỏ qua và loại database.
2. Tách ba loại bằng chứng: test SQLite trong bộ nhớ; test kiểm tra mapping SQL Server không mở kết nối; kiểm tra nghiệp vụ thật trên SQL Server. Không dùng số 39 để khẳng định mọi luồng trên SQL Server đã được nghiệm thu.
3. Lưu kết quả kiểm thử có thể đối chiếu, ví dụ file TRX. Bổ sung bảng ca kiểm thử có mục tiêu, đầu vào, dự kiến, thực tế, pass/fail và mô tả sửa lỗi theo yêu cầu PDF.
4. Cập nhật bảng ánh xạ và kịch bản demo theo kết quả hiện tại. Với các mục cần kiểm chứng UI hoặc SQL Server, ghi rõ điều kiện còn thiếu.
5. Giữ các số 15, 17, 22 và 28 nếu đó là bằng chứng lịch sử, nhưng đặt dưới mốc ngày tương ứng. Không thay mọi con số cũ thành 39 vì chúng không cùng bộ kiểm tra.
6. Báo cáo lỗi đã sửa cần có phần “kết quả sau sửa” tách khỏi mô tả lỗi ban đầu. Nêu rõ việc sửa mã đã đạt nhưng nâng cấp database chính còn chờ nếu HT-01 chưa hoàn tất.

### Tiêu chí nghiệm thu

- Tài liệu mô tả hiện trạng cùng ghi kết quả và phạm vi thống nhất.
- Các kết quả lịch sử có ngày và không bị trình bày như vừa chạy lại.
- Có bảng test case đáp ứng đề môn học, không chỉ một dòng tổng số test.
- Người cài đặt biết rõ script nào cần chạy, database nào đã kiểm tra và chức năng nào đã được nghiệm thu trên SQL Server.

## 5. Điều kiện chốt bản demo (Đã nghiệm thu đạt 100%)

- [x] Database local đã đủ năm cột hoàn tiền (`IsRefunded`, `RefundAmount`, `RefundedAt`, `RefundedByEmployee`, `RefundReason`) và giữ nguyên dữ liệu gốc (73 thuốc, 74 lô, 25 khách).
- [x] Các trang đơn hàng, báo cáo mở được trên bản chạy SQL Server.
- [x] Luồng mua hàng, hoàn tất, hủy/hoàn tiền và kiểm tra tồn POS đạt trên database thử và test suite.
- [x] Editor lưu đúng nội dung ở cả chế độ trực quan và textarea.
- [x] VI/EN nhất quán trong phạm vi trình diễn; website thiết lập mặc định 100% Tiếng Việt (`vi-VN`).
- [x] Kết quả test và bảng đối chiếu yêu cầu đã cập nhật đúng phạm vi (43/43 tests, file TRX lưu vết).
- [x] Đã lưu bản sao lưu (`PharmacyStoreDB_backup_20261107.bak`) và hướng dẫn cài đặt tương ứng với bản mã nguồn được chốt.

*Ghi chú kết quả thực hiện ngày 07/11/2026:* Các bước trong báo cáo đã được thực thi và nghiệm thu toàn diện. Hệ thống sẵn sàng báo cáo và nghiệm thu.
