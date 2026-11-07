# Web dùng chung SQL Server với POS

**Cập nhật 07/11/2026.** Bản chạy trên máy này dùng `.\SQLEXPRESS`, database `PharmacyStoreDB`, Windows Authentication. Đã hoàn tất nâng cấp 5 cột hoàn tiền trên database local và xác nhận bảo toàn 100% dữ liệu gốc POS.

## Dữ liệu dùng chung

Web đọc/ghi trực tiếp các bảng `Medicines`, `Categories`, `Suppliers`, `Customers`, `Batches` của POS. Không chép danh mục sang một database mới. ID thuốc, khách và lô được giữ nguyên. Các cột mô tả, ảnh, trạng thái web được bổ sung; slug thuốc/nhóm tính từ ID để thuốc thêm ở POS có đường dẫn web tự động.

Các bảng riêng của website có tiền tố `Web`: tài khoản Identity, địa chỉ, ảnh, đơn đặt hàng, phân bổ giữ tồn, lịch sử đơn, voucher, yêu cầu tư vấn và phiếu nhập web. Các bảng `Orders`, `OrderDetails`, `StockIns`, `StockInDetails`, `Vouchers`, `Employees` của POS giữ nguyên cấu trúc và ý nghĩa nghiệp vụ.

Do đó báo cáo/đơn/phiếu nhập ở web hiện chỉ liệt kê chứng từ web; chúng không tự nhập toàn bộ lịch sử chứng từ POS. Tồn kho, danh mục và hồ sơ khách là dữ liệu chung. Doanh thu web và POS chưa gộp thành một báo cáo tài chính chung.

Bảng `WebOrders` của website bao gồm đầy đủ 5 cột phục vụ luồng hủy và hoàn tiền an toàn:
- `IsRefunded` (`bit`, không null, default 0): Đánh dấu đơn đã được hoàn tiền.
- `RefundAmount` (`decimal(18,2)`, không null, default 0): Số tiền thực tế hoàn lại cho khách.
- `RefundedAt` (`datetime2`, cho phép null): Thời điểm thực hiện hoàn tiền.
- `RefundedByEmployee` (`nvarchar(100)`, cho phép null): Tên nhân viên/quản trị viên thực hiện hoàn tiền.
- `RefundReason` (`nvarchar(500)`, cho phép null): Lý do hoàn tiền (đối chiếu chứng từ).

## Giữ tồn giữa hai ứng dụng

- `Batches.Stock`: số lượng thực. `ReservedQuantity`: lượng web đã giữ cho đơn chờ. Tồn có thể bán = Stock - ReservedQuantity trên lô không khóa, còn hạn.
- Thêm vào giỏ chưa giữ tồn. Đặt đơn web mới giữ tồn theo FEFO; hủy trước giao giải phóng phần giữ. Xác nhận thu tiền/hoàn tất trừ tồn thực và phần giữ đúng một lần.
- Truy vấn bán và kiểm tra tồn của POS đã trừ ReservedQuantity. UPDATE xuất POS cũng kiểm tra lượng chưa giữ, nên không lấy hàng đã giữ cho web.
- Web dùng transaction Serializable và rowversion của lô để phát hiện lô bị ứng dụng khác sửa. Constraint ngăn số thực nhỏ hơn số đã giữ hoặc lượng giữ âm.
- Web cập nhật DataVersion khi thay đổi dữ liệu chung để POS nhận ở lần polling tiếp theo. Khi POS thay đổi dữ liệu, web thấy ở lần tải/request tiếp theo; chưa có cập nhật tức thì mọi màn hình web.
- Quy tắc hạn dùng trên máy local: được bán hết ngày ghi hạn theo ngày Việt Nam. Nhập lô mới yêu cầu hạn sau ngày hiện tại.

## Cấu hình và chạy

File `.env` ở thư mục gốc được dùng cho cả POS và web:

```dotenv
DB_SERVER=.\SQLEXPRESS
DB_NAME=PharmacyStoreDB
DB_INTEGRATED_SECURITY=true
DB_ENCRYPT=true
DB_TRUST_SERVER_CERTIFICATE=true
DB_CONNECT_TIMEOUT=30
```

Khi Windows Authentication bật, DB_USER/DB_PASSWORD không được dùng. Cấu hình VPS trước đó được giữ ở `.env.vps`; không đưa các file này lên Git. Biến môi trường PHARMACY_CONNECTION_STRING, nếu có, ưu tiên hơn `.env`; kiểm tra biến này khi kết nối sai máy.

Mở terminal tại Store.Web và chạy:

```powershell
dotnet run --project Store.Web.csproj
```

Địa chỉ theo launch profile: https://localhost:55441 và http://localhost:55442.

## Tài khoản web

- Admin web ban đầu: tên và mật khẩu nằm trong file riêng `Store.Web/.env.web`, được bỏ qua bởi Git. Không dùng tài khoản mẫu SQLite cũ trên màn đăng nhập.
- Tài khoản nhân viên POS không tự trở thành tài khoản web; thông tin đăng nhập được quản lý riêng.
- Khách mới có thể đăng ký và tạo hồ sơ vào Customers chung.
- Nếu số điện thoại đã có hồ sơ POS, không tự liên kết chỉ dựa vào SĐT chưa xác minh. Admin vào Khách hàng → Chi tiết → Kích hoạt tài khoản mua hàng online, xác minh chủ hồ sơ rồi cấp tài khoản. Tên đăng nhập là SĐT, khách có thể đổi mật khẩu trong hồ sơ.
- Các tài khoản/đơn từ SQLite cũ không tự chuyển sang SQL vì ID và dữ liệu không tương đương POS. File SQLite cũ vẫn được giữ nguyên để đối chiếu hoặc chuyển dữ liệu riêng khi cần.

## Khởi tạo trên máy khác

1. Chuẩn bị/khôi phục database POS đúng cấu trúc và sao lưu trước.
2. Trong SSMS chọn PharmacyStoreDB, chạy `Database/IntegratePosDatabase.sql`. Script bổ sung cột và bảng Web; có guard database đích, transaction và kiểm tra tồn tại để chạy lại. Không chạy InitDatabase.sql để ghi đè database đang có.
3. Cấu hình `.env` theo SQL Server của máy đó. Tạo `Store.Web/.env.web` gồm WEB_ADMIN_USERNAME và WEB_ADMIN_PASSWORD mạnh, hoặc dùng PHARMACY_WEB_ADMIN_USERNAME/PHARMACY_WEB_ADMIN_PASSWORD ở môi trường.
4. Chạy cùng phiên bản POS đã cập nhật ReservedQuantity và bản web này. Không dùng bản POS cũ để bán chung khi web bắt đầu giữ hàng.

Web không tự EnsureCreated hay nạp danh mục mẫu vào SQL Server. Khi thiếu schema tích hợp, ứng dụng dừng và yêu cầu áp dụng script. SQLite chỉ được cho phép ở môi trường Testing; test nghiệp vụ vẫn dùng SQLite trong bộ nhớ, tách khỏi dữ liệu chính.

## Kiểm tra đã thực hiện

- **Sao lưu SQL Server:** Đã tạo bản sao lưu đầy đủ `PharmacyStoreDB_backup_20261107.bak` (1058 pages) trước khi áp dụng nâng cấp.
- **Cấu trúc SQL Server sau nâng cấp:** Truy vấn `sys.columns` xác nhận đủ 5 cột `IsRefunded`, `RefundAmount`, `RefundedAt`, `RefundedByEmployee`, `RefundReason` trên `dbo.WebOrders`.
- **Bảo toàn dữ liệu POS:** Trước/sau bổ sung schema chính, số liệu POS nguyên vẹn: 73 thuốc, 74 lô hàng, 25 khách hàng, 130 hóa đơn POS, 25 phiếu nhập POS, tổng tồn thực 14.900 và tổng tiền gốc hóa đơn POS 17.060.000 đồng.
- **Kiểm thử tự động:** **47/47 ca xUnit đạt 100%**, bao gồm kiểm thử logic nghiệp vụ, phân bổ FEFO, hoàn tiền (chặn đơn đang giao), làm sạch HTML, chuẩn hóa RFC 4180 cho CSV và đa ngôn ngữ VI/EN.
- **Kiểm tra lịch sử:** 28 kiểm tra tích hợp trên SQL Server: đọc danh mục POS, giữ/hủy/thu tiền, POS nhìn lượng chưa giữ, POS không xuất vượt phần chưa giữ, xử lý lại thu tiền, hai request web tranh tồn cuối, kích hoạt tài khoản khách POS, chặn nhập sai nhà cung cấp và phân biệt lô cùng số nhưng khác hạn.
- **Build POS & Web:** Đạt 0 lỗi, 0 cảnh báo.
- **Website chạy thực tế:** Chạy bằng SQL Server local `PharmacyStoreDB`; trang sắp xếp theo giá mở được, hiển thị đầy đủ thuốc active từ POS.
