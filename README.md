# PharmacyStore — bản gộp thử

Mở `PharmacyStore.slnx`, chọn **Store.POS** làm project khởi động.
Database thử: `PharmacyStoreTrialDB` trên `.\SQLEXPRESS`.
Tài khoản mẫu: `admin` / `123456`.

## Các mục trong dự án

| Thư mục | Nội dung |
|---|---|
| `Store.POS/Forms/` | Các màn hình, chia theo chức năng |
| `Store.POS/Common/` | Định dạng giao diện chung và phiên đăng nhập |
| `Store.BLL/` | Xử lý nghiệp vụ, chia theo chức năng |
| `Store.DAL/` | Truy vấn dữ liệu, chia theo chức năng |
| `Store.DAL/Database/` | Script khởi tạo database thử |
| `Store.Shared/` | Models và DTO dùng chung |
| `Store.API/` | API và Controllers |
| `Store.SmokeTests/Checks/` | Các nhóm kiểm tra chức năng |
| `Docs/` | Ghi chú gộp và thay đổi |
| `artifacts/logs/` | Kết quả build/kiểm tra |
| `artifacts/previews/` | Ảnh giao diện xem thử |
| `Backups/` | Bản sao lưu, checksum và bản đồ di chuyển file |

`Forms/` gồm: **DangNhap, TrangChu, BanHang, Thuoc, DanhMuc, NhapKho, Kho, Voucher, HoaDon, CanhBao, BaoCao**.

Trong BLL/DAL, mục `Kho/` chứa nhập kho, tồn theo lô và danh sách lô; `DanhMuc/` chứa nhóm thuốc, nhà cung cấp và khách hàng. Bố cục tĩnh nằm trong file `.Designer.cs`, tài nguyên `.resx` ở cùng thư mục. Chuột phải file `frm…cs` → **View Designer** để chỉnh vị trí, màu sắc và cột bảng. Các file `.Layout.cs` tạo lại giao diện lúc chạy đã được loại bỏ. Hai form thêm nhanh nhóm/NCC có Design riêng (`frmThemNhomThuoc` và `frmThemDanhMuc`).

Các project vẫn thuộc các nhóm Clients, Server, Shared trong Solution; thêm Tests và Documentation để truy cập kiểm tra/tài liệu. Các thư mục `bin/`, `obj/`, `.vs/` là đầu ra/cấu hình của công cụ.

## Kiểm tra

Chạy từ thư mục chứa solution:

```powershell
dotnet build PharmacyStore.slnx --no-restore -m:1 -p:UseSharedCompilation=false
dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --expansion
dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --render-only
```

Kiểm tra dùng database thử riêng, dọn các bản ghi do chính bài kiểm tra tạo ra. Ảnh được lưu vào `artifacts/previews/`.

Xem [ghi chú gộp](Docs/MERGE-NOTES.md) để biết nội dung, cách tạo database và các giới hạn. Bản nguồn `PharmacyStore` được giữ nguyên.

## Phân quyền và nhập hàng

Admin quản lý danh mục, giá bán, voucher và nhân viên. Nhân viên được bán hàng, nhập kho, thêm/sửa thông tin cơ bản khách hàng và tra cứu. Cả hai xem toàn bộ hóa đơn, báo cáo và phiếu nhập. Đổi vai trò ở **Quản lý khác → Nhân viên → Sửa → Vai trò**.

- **Quản lý khác → Phiếu nhập hàng**: xem các lần nhập, nhân viên nhập và chi tiết giá/số lượng theo lô; tìm kiếm bằng chữ và lọc ngày.
- **Quản lý khác → Đổi mật khẩu**: đổi mật khẩu tài khoản đang đăng nhập. Admin có thể đặt lại mật khẩu nhân viên trong form Nhân viên.
- Giá nhập trên bảng thuốc/lô là **giá nhập gần nhất**; dấu — nghĩa là chưa có phiếu nhập tương ứng.
- Database thử hiện tại đã bổ sung Role. Với bản database thử cũ khác, chạy `Store.DAL/Database/AddEmployeeRoles.sql` trên đúng database đó; không chạy lại InitDatabase.sql.
- Kiểm tra mới: `dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --roles`.

Phân quyền hiện nằm trong ứng dụng POS; kết nối SQL trực tiếp chưa phải ranh giới bảo mật cho máy trạm không tin cậy. Chi tiết ở cuối ghi chú gộp.

Các mục/nút không được cấp quyền sẽ ẩn. Nhân viên dùng Nhập kho (F4) để lập và lưu phiếu, tên nhân viên được ghi trên phiếu. Thêm mới thuốc/NCC/nhóm vẫn do Admin thực hiện. Các ô mã tự sinh đã bỏ khỏi phần nhập liệu; mã vẫn có trên bảng danh sách.

## Excel, hình thức và trạng thái thanh toán

- Thuốc: **Nhập Excel**, **Xuất Excel**, **File mẫu**. Chỉ Admin nhập danh mục, nhân viên được xuất. Nhập là thêm mới, không ghi đè thuốc cũ. Nhóm/NCC phải tồn tại và xác định được duy nhất; hệ thống báo lỗi thay vì tạo NCC với số điện thoại giả. File xuất danh mục dùng lại được cùng cấu trúc nhập; bản ghi đã có sẽ bị báo trùng.
- Nhập kho: chọn NCC → **File mẫu** hoặc **Nhập Excel** → xem trước → thêm vào phiếu → **Lưu phiếu**. Việc đọc file/xem trước chưa cộng tồn. Mẫu chứa thuốc NCC, để trống số lượng/giá nhập để người dùng tự điền. Nhân viên và Admin đều dùng được.
- Báo cáo: **Xuất Excel** tạo hai sheet doanh thu/ngày và hóa đơn đã thanh toán. Hóa đơn: xuất danh sách hoặc chi tiết phiếu đang xem, gồm trạng thái/hình thức thanh toán.
- File Excel tối đa 20 MB và khoảng 10.000 dòng dữ liệu. Dùng đúng file mẫu, giữ mã vạch ở dạng Text. Có xem trước và lỗi từng dòng; nhập danh mục lưu từng dòng hợp lệ và thông báo kết quả, các dòng lỗi không ghi dữ liệu.
- Bán hàng → **Lưu chờ thanh toán**: lưu đơn, chưa giữ/trừ tồn, chưa trừ lượt voucher, chưa ghi doanh thu. Đơn chưa phân lô.
- Quản lý khác → **Hóa đơn** → chọn đơn chờ → chọn Tiền mặt/Chuyển khoản QR → **Thanh toán**. Hệ thống kiểm tra lại hàng còn hạn/voucher, trừ tồn FEFO và chuyển sang Đã thanh toán trong một giao dịch. Không cho thanh toán hai lần. QR vẫn được nhân viên xác nhận thủ công, chưa kết nối đối soát ngân hàng.
- Giá thuốc được chốt lúc lập đơn chờ; kiểm tra thuốc còn kinh doanh lúc thu tiền. Voucher phải còn hoạt động/lượt và giữ nguyên giá trị giảm đã chốt. Đơn thiếu hàng/voucher vẫn chờ. Chưa bổ sung hủy/sửa hóa đơn chờ hoặc hoàn tiền; có thể lập đơn mới nếu cần thay đổi.
- Doanh thu theo ngày thu tiền; các hóa đơn cũ dùng ngày lập vì trước đây lưu ngay khi thu. Hóa đơn cũ được giữ là Đã thanh toán, hình thức Chưa xác định để không suy đoán dữ liệu.

Database hiện tại đã được nâng cấp bằng `Store.DAL/Database/AddOrderPayments.sql`. Script chạy lại được, không tạo/xóa database. Thêm cột PaymentMethod, PaymentStatus, PaidAt, PaidEmployeeId và bảng OrderPendingItems để lưu chi tiết đơn chưa trừ tồn. Nếu dùng bản database thử khác, áp dụng script này trước khi chạy phiên bản mới; không chạy lại InitDatabase.sql trên database đang có.

Kiểm tra mới: `dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --excel-payments`.
