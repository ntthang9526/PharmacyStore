# Bản gộp thử PharmacyStore

Bản gốc: `E:\PharmacyStoreSolution\PharmacyStore`.
Nguồn bổ sung: `E:\PharmacyStoreSolution\LTTQ12`.
Giữ giao diện chính, bán hàng và QR của PharmacyStore; thêm quản lý thuốc, nhóm,
nhà cung cấp, khách hàng, nhập kho, báo cáo, cảnh báo, voucher và lưu hóa đơn.

## Chạy thử

1. Tạo database riêng bằng `sqlcmd -S .\SQLEXPRESS -E -C -b -f 65001 -i Store.DAL\Database\InitDatabase.sql`.
   Script sẽ dừng nếu `PharmacyStoreTrialDB` đã tồn tại, không xóa database.
2. Nếu SQL Server khác `.\SQLEXPRESS`, đặt biến môi trường `PHARMACY_TRIAL_CONNECTION_STRING`
   thành chuỗi kết nối đến database thử riêng trước khi chạy ứng dụng.
3. Mở `PharmacyStore.slnx`, chọn Store.POS làm project khởi động.
4. Tài khoản mẫu: `admin` / `123456`.

## Các thay đổi tương thích

- Dùng chung kết nối SQL Server, mặc định trỏ PharmacyStoreTrialDB.
- Đăng nhập thật và lưu nhân viên vào phiên.
- Lưu hóa đơn/chi tiết/trừ kho/voucher trong một transaction; mang theo khách hàng.
- Không xóa giỏ nếu lưu thất bại; đối chiếu lại giá và voucher trước khi lưu.
- CRUD dùng tham số và ID tự tăng; lỗi dữ liệu được trả về giao diện.
- Sửa thuốc không ghi đè tồn tất cả lô; nhập tồn qua màn hình nhập kho.
- Thuốc mới chọn nhà cung cấp/nhóm từ database, tồn khởi đầu bằng 0.
- Ngừng kinh doanh thuốc thay vì xóa lịch sử hóa đơn.
- Kiểu Order/OrderDetail khớp INT/DECIMAL trong database.

## Giới hạn

- QR vẫn là xác nhận thủ công từ giao diện; chưa có đối soát ngân hàng.
- Cơ chế mật khẩu mẫu của hai nguồn vẫn so sánh trực tiếp; chưa chuyển sang hash.
- Chưa bổ sung in hóa đơn, tích điểm tự động hoặc API nghiệp vụ mới.
- Backups/SOURCE-SNAPSHOT.json chứa checksum của mã nguồn gốc để xác minh bản gốc không bị đổi.

## Kết quả kiểm tra

- Database PharmacyStoreTrialDB đã được tạo trên .\SQLEXPRESS với dữ liệu mẫu.
- Solution đã build thành công; không còn cảnh báo sau các sửa tương thích.
- 54 kiểm tra tích hợp và khởi tạo/nạp dữ liệu form đều đạt (artifacts/logs/smoke-test.log).
- Có kiểm tra hai giao dịch bán chạy đồng thời, không bán vượt tồn.
- Dữ liệu do bài kiểm thử tạo ra đã được dọn; dữ liệu mẫu được giữ để dùng thử.
- Chạy lại kiểm tra: dotnet run --project Store.SmokeTests\Store.SmokeTests.csproj
- Chưa thao tác thủ công từng nút trên giao diện; chưa thử chuyển khoản ngân hàng thật.
## Sửa lỗi menu Quản lý khác
Menu được quản lý theo vòng đời cửa sổ chính, không Dispose ngay trong sự kiện Closed. Đã đạt 10 kiểm tra mở/đóng lại menu và mở ba màn hình quản lý (artifacts/logs/menu-regression.log).

## Đồng bộ giao diện theo form bán hàng

Đã cập nhật 9 form: đăng nhập, màn hình chính, quản lý thuốc, nhóm thuốc, nhà cung cấp, khách hàng, nhập kho, cảnh báo và báo cáo.
Dùng chung nền xám sáng, Segoe UI, các khung thông tin, bảng trắng/xám và nút xanh/đỏ nhạt; các màn hình quản lý bố trí danh sách bên trái và phần nhập liệu bên phải.
Form bán hàng và QR được giữ nguyên. Các control và sự kiện nghiệp vụ được giữ lại trong bố cục mới.

Kiểm tra cuối: 64 kiểm tra nghiệp vụ/menu đạt (artifacts/logs/ui-regression.log); 380 kiểm tra bố cục tại kích thước thường và nhỏ nhất đạt (artifacts/logs/ui-layout.log). Ảnh các form nằm trong artifacts/previews/. Chưa kiểm tra các mức DPI/độ phóng đại khác của Windows.


## Điều chỉnh phạm vi giao diện
Đã trả giao diện đăng nhập và màn hình chính về trước lần đổi giao diện. Chỉ 7 form gộp từ LTTQ12 dùng bố cục mới: thuốc, nhóm thuốc, nhà cung cấp, khách hàng, nhập kho, cảnh báo và báo cáo. Giữ nguyên đăng nhập thật, điều hướng và bản sửa vòng đời menu Quản lý khác.

## Chọn dòng, tên danh mục và thêm nhanh

- Các bảng trong 7 form gộp không tự chọn dòng lúc mở/nạp lại dữ liệu; bấm ra ngoài, đổi ô nhập hoặc rời cửa sổ sẽ bỏ chọn.
- Bảng thuốc hiển thị tên nhóm/nhà cung cấp; các khóa nội bộ được ẩn để vẫn sửa và lưu đúng bản ghi. Ô tìm NCC hiển thị tên.
- Khi thêm/sửa thuốc, nút + cạnh Nhóm thuốc/NCC mở form thêm nhanh. Lưu xong tự chọn mục mới, giữ nguyên nội dung thuốc đang nhập.
- Nhóm: tên bắt buộc, tối đa 100 ký tự. NCC: tên bắt buộc tối đa 150; điện thoại 9–11 chữ số bắt đầu bằng 0; địa chỉ bắt buộc tối đa 250.
- Chặn ký tự điều khiển, nhóm trùng tên, NCC trùng tên hoặc điện thoại; dùng tham số SQL và giao dịch khi tạo mới.
- Đã đạt 33 kiểm tra yêu cầu riêng, 64 kiểm tra nghiệp vụ/menu và kiểm tra ảnh/bố cục (requested-*.log).


## Bổ sung bỏ chọn, tìm kiếm và lô hàng

- Ở bốn form danh mục thuốc/NCC/nhóm/khách hàng: bấm vùng trống sẽ bỏ chọn và xóa phần thông tin chi tiết về trạng thái ban đầu, thoát chế độ thêm/sửa. Giữ bộ lọc tìm kiếm. Bấm ô nhập hoặc nút thao tác vẫn giữ bản ghi để sửa/lưu và thêm nhanh.
- Form NCC hiện đủ mã, tên, điện thoại, địa chỉ; form nhóm hiện mã và tên. Bảng thuốc vẫn dùng tên NCC/nhóm thay mã tham chiếu.
- Ô tìm thuốc, NCC, khách hàng cho gõ chữ; tìm khớp một phần, hỗ trợ Enter. Thêm ô tìm tên/mã nhóm. Từ khóa rỗng trả toàn bộ; dấu %, _ và [ được tìm như ký tự thông thường.
- Quản lý khác → Lô hàng: danh sách chỉ xem, tìm số lô/mã lô/tên thuốc/mã vạch/tên NCC/tên nhóm. Hiện hạn dùng, tồn, tổng nhập và lần nhập gần nhất; cuộn ngang để xem các cột phía cuối.
- Những lô mẫu không có chi tiết phiếu nhập hiển thị dấu — cho tổng nhập/ngày nhập. NCC là nhà cung cấp hiện tại của thuốc.
- Đạt 60 kiểm tra bổ sung, 33 kiểm tra thêm nhanh/bỏ chọn trước đó, 68 kiểm tra nghiệp vụ/menu và 404 kiểm tra bố cục; ảnh lưu trong artifacts/previews/. Các kiểm tra chạy trên database thử riêng và dọn đúng bản ghi kiểm thử.


## Trang tổng quan, voucher, hóa đơn và nhập kho

- Giữ khung xanh của trang chính; bổ sung doanh thu hôm nay (tiền hàng trừ giảm giá), số hóa đơn, số thuốc tồn còn hạn dưới 10, số lô còn hàng dưới 30 ngày/gồm đã hết hạn. Hiện danh sách cảnh báo và 5 hóa đơn gần nhất; nhấp đúp hóa đơn mở chi tiết. Cập nhật khi mở trang, bấm Làm mới/Trang chủ và sau khi đóng chức năng con.
- Quản lý khác → Voucher: thêm/sửa/xóa, khóa hoặc mở hoạt động, tìm mã. Kiểm tra mã 1–20 ký tự A–Z/0–9/_/-, không trùng, giảm >0; phần trăm ≤100; lượt không âm. Kiểm tra lượt gốc khi sửa để không ghi đè lượt vừa dùng. Voucher đã có hóa đơn không xóa được, có thể khóa.
- Quản lý khác → Hóa đơn: tìm mã HD/số, nhân viên, khách hàng, điện thoại, voucher; hai mốc ngày tùy chọn; xem thuốc, lô, số lượng, đơn giá lúc bán và thành tiền. Chỉ xem dữ liệu đã lưu; không bổ sung in/xuất hay hủy hóa đơn.
- Tìm thuốc/NCC/khách hàng chuyển sang TextBox, bỏ dropdown và các lần tải danh sách chọn không cần thiết. Các lựa chọn danh mục trong phần nhập liệu vẫn giữ dropdown.
- Nhập kho: nút + thêm NCC/thuốc; thuốc mới thuộc NCC đang chọn, có tên/nhóm/đơn vị/giá bán, tồn khởi đầu 0 và có thể thêm nhóm ngay tại đó. Giữ ghi chú và thông tin phiếu đang nhập khi mở/hủy thêm nhanh.
- Mặc định và sau làm mới phiếu, NCC/thuốc đều chưa chọn. Chọn NCC mới không chọn sẵn thuốc. Số lô và nút thêm dòng chỉ mở sau khi chọn đủ hai mục; bỏ/đổi lựa chọn xóa số lô cũ. Phiếu đã có dòng khóa đổi/thêm NCC để bảo đảm một NCC/phiếu.
- Không thêm bảng hay thay cấu trúc database cho đợt này; vẫn dùng PharmacyStoreTrialDB. Có Backups/BACKUP-before-dashboard.zip chứa mã trước đợt thay đổi để khôi phục nếu cần.
- Kiểm tra: 127 kiểm tra bổ sung (artifacts/logs/expansion-test.log), 33 kiểm tra thêm nhanh cũ, 62 kiểm tra yêu cầu trước, 72 kiểm tra nghiệp vụ/menu. Tất cả chỉ dùng database thử và dọn riêng bản ghi kiểm thử.


## Sắp xếp thư mục theo chức năng

Mã giao diện nằm trong Store.POS/Forms/, chia theo chức năng; các file .Designer.cs, .Layout.cs, .Dashboard.cs và .resx ở cùng form. Common/ chứa định dạng chung và phiên đăng nhập. Store.BLL và Store.DAL được chia theo cùng các nghiệp vụ, không đổi tên lớp hay namespace.

Store.SmokeTests/Checks/ chứa các nhóm kiểm tra. artifacts/logs/ chứa nhật ký, artifacts/previews/ chứa ảnh; Backups/ chứa bản sao lưu và checksum của nguồn gốc. Docs/ chứa ghi chú gộp. Bản đồ đường dẫn cũ/mới tại Backups/organization-map.json. Các lệnh chạy kiểm tra trong tài liệu vẫn được thực hiện từ thư mục gốc bản gộp thử.


## Đồng bộ Design và giao diện chạy

Chuyển 14 bố cục sang InitializeComponent chuẩn trong .Designer.cs: trang chính, bảy form gộp, voucher, hóa đơn, lô hàng và ba form thêm nhanh. Các control, TableLayoutPanel, font/màu và cột bảng có mặt trong Designer; bỏ ApplySalesLayout và các hàm dựng lại cây control khi chạy. Nạp dữ liệu giữ riêng; không kết nối SQL khi ở DesignMode.

Tách form thêm nhóm thành frmThemNhomThuoc, giữ frmThemDanhMuc cho thêm NCC để mỗi loại có một bản Design chỉnh được. frmThemThuoc có constructor mặc định để mở Design. Các nút thêm nhanh, validate và trả ID để tự chọn mục mới được giữ lại. Giao diện đăng nhập, bán hàng, QR vốn đã có Designer được giữ bố cục. Việc tải danh sách thuốc ở form bán hàng được chuyển từ constructor sang Load và bỏ qua khi DesignMode; QR có constructor mặc định và không tải QR mạng trong DesignMode.

Đã đối chiếu cây control/khung/màu/font/cột của Designer với constructor cho cả 14 form; 513 kiểm tra bố cục đạt, cùng các bộ kiểm tra thao tác cũ. Ảnh Design không có dữ liệu thật, dữ liệu chỉ nạp khi chạy. Báo cáo đối chiếu tại artifacts/logs/designer-parity.log; ảnh tại artifacts/designer/. Có Backups/BACKUP-before-designer-sync.zip để phục hồi mã trước đợt này. Chưa thao tác trực tiếp trong Visual Studio hoặc thử DPI khác.
