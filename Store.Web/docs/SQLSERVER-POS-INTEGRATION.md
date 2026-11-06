# Web dùng chung SQL Server với POS

Cập nhật 05/11/2026. Bản chạy trên máy này dùng `.\SQLEXPRESS`, database `PharmacyStoreDB`, Windows Authentication.

## Dữ liệu dùng chung

Web đọc/ghi trực tiếp các bảng `Medicines`, `Categories`, `Suppliers`, `Customers`, `Batches` của POS. Không chép danh mục sang một database mới. ID thuốc, khách và lô được giữ nguyên. Các cột mô tả, ảnh, trạng thái web được bổ sung; slug thuốc/nhóm tính từ ID để thuốc thêm ở POS có đường dẫn web tự động.

Các bảng riêng của website có tiền tố `Web`: tài khoản Identity, địa chỉ, ảnh, đơn đặt hàng, phân bổ giữ tồn, lịch sử đơn, voucher, yêu cầu tư vấn và phiếu nhập web. Các bảng `Orders`, `OrderDetails`, `StockIns`, `StockInDetails`, `Vouchers`, `Employees` của POS giữ nguyên cấu trúc và ý nghĩa nghiệp vụ.

Do đó báo cáo/đơn/phiếu nhập ở web hiện chỉ liệt kê chứng từ web; chúng không tự nhập toàn bộ lịch sử chứng từ POS. Tồn kho, danh mục và hồ sơ khách là dữ liệu chung. Doanh thu web và POS chưa gộp thành một báo cáo tài chính chung.

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

- Sao lưu COPY_ONLY trước khi sửa và thử script trên database phục hồi riêng; chạy lại script thành công.
- 28 kiểm tra tích hợp trên SQL Server: đọc danh mục POS, giữ/hủy/thu tiền, POS nhìn lượng chưa giữ, POS không xuất vượt phần chưa giữ, xử lý lại thu tiền, hai request web tranh tồn cuối, kích hoạt tài khoản khách POS, chặn nhập sai nhà cung cấp và phân biệt lô cùng số nhưng khác hạn.
- 17 ca xUnit đạt; có thêm test đảm bảo model dùng bảng POS cho danh mục và bảng Web riêng cho đơn/tài khoản. Bộ test hiện vẫn có một test mẫu rỗng từ dự án cũ.
- Build POS đạt 0 lỗi/0 cảnh báo.
- Trước/sau bổ sung schema chính: giữ nguyên 73 thuốc, 25 khách, 130 hóa đơn POS, 25 phiếu nhập POS, tổng tồn thực 14.900 và tổng tiền gốc hóa đơn POS 17.060.000 đồng.
- Website chạy bằng SQL Server local; trang sắp xếp theo giá mở được, hiển thị 71 thuốc active từ POS.

Đã có bản sao lưu SQL Server trước chuyển đổi. Đường dẫn được lưu trong kết quả kiểm tra của cuộc làm việc; không đặt bản backup có dữ liệu khách lên Git.
