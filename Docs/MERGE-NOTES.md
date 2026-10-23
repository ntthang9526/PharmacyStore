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


## Sửa theo các mục 1, 2, 8, 9 (22/10/2026)

- Khôi phục cột bảng thuốc và khách hàng trực tiếp trong Designer; tắt tự sinh cột, hiện tiêu đề tiếng Việt và ẩn khóa nhóm/NCC trong bảng thuốc. Giữ bố cục và thay đổi hiện có của người dùng.
- Tiền làm tròn đến đồng nguyên, từ 0,5 làm tròn lên. Làm tròn đơn giá trước khi nhân số lượng; đồng bộ giỏ hàng, tiền mặt, tiền thừa, QR, kiểm tra giá trong giao dịch, giá lưu chi tiết hóa đơn và nhập kho. Voucher tiền cố định làm tròn về đồng; voucher phần trăm vẫn nhập tối đa 2 số thập phân và làm tròn số tiền giảm. Áp dụng cho giao dịch mới, không ghi lại lịch sử hóa đơn cũ.
- Điện thoại khách hàng bắt buộc đúng 10 chữ số ASCII, bắt đầu bằng 0, dùng chung kiểm tra khi thêm/sửa/tra cứu bán hàng. Số điện thoại NCC giữ quy tắc cũ để hỗ trợ số bàn.
- Bán hàng: tìm theo mã vạch; ô nhập nhận mã vạch rồi Enter (cả máy quét dạng bàn phím), giữ nguyên số 0 đầu và mã dài 13 số. Ưu tiên mã vạch khớp chính xác, sau đó mã thuốc nội bộ. Mã vạch trùng yêu cầu chọn trong danh sách tìm kiếm.
- Sao lưu mã trước sửa tại Backups/BACKUP-before-selected-fixes.zip. Không thêm database hay thay cấu trúc bảng. Bản chạy kiểm tra riêng tại artifacts/builds/selected-fixes/Store.POS.exe do bản chạy cũ đang mở giữ tệp.
- Xác nhận cuối: build 0 lỗi/0 cảnh báo; đạt 104 kiểm tra nghiệp vụ và sửa mới, 127 voucher/nhập kho/hóa đơn, 17 Designer không SQL, 62 tìm kiếm/bỏ chọn và 513 bố cục. Tổng 823 kiểm tra. Số bản ghi thật giữ nguyên (1 hóa đơn, 2 khách hàng, 4 thuốc); checksum mã nguồn gốc: 0 tệp thay đổi. Chưa kiểm tra máy quét vật lý hoặc giao dịch ngân hàng thật.
- Sau khi ứng dụng cũ đóng, đã build thành công lại đường dẫn chạy thông thường Store.POS/bin/Debug/net8.0-windows/Store.POS.exe (0 cảnh báo, 0 lỗi). Có thể mở bản này hoặc chạy lại từ Visual Studio.

## Quản lý nhân viên, tạm ẩn và đồng bộ form (23/10/2026)

- Quản lý khác → Nhân viên: tìm họ tên/tài khoản/điện thoại/mã; thêm, sửa, đổi mật khẩu, khóa/mở lại tài khoản. Mật khẩu trống khi sửa sẽ giữ mật khẩu cũ. Không hiển thị mật khẩu trong danh sách; mật khẩu mới và đổi mật khẩu được lưu bằng PBKDF2 có salt. Tài khoản cũ vẫn đăng nhập được.
- Kiểm tra họ tên bắt buộc tối đa 100 ký tự; tài khoản 3–50 ký tự chữ/số/_.-; mật khẩu mới 6–128 ký tự; điện thoại nếu nhập phải đúng 10 số bắt đầu bằng 0. Chặn trùng tài khoản và khóa tài khoản đang đăng nhập. Khóa là ngừng đăng nhập, giữ nguyên lịch sử hóa đơn/nhập kho.
- Các form chức năng mở độc lập, mỗi chức năng dùng lại một cửa sổ. Nút Tạm ẩn giữ nguyên giỏ hàng, phiếu nhập và thông tin chưa lưu. Mở lại từ nút chức năng hoặc Form đang mở trên trang chính. Khi đóng ứng dụng có thông báo nếu còn form đang mở/ẩn.
- Sau khi ghi dữ liệu thành công, các form trong cùng phiên chạy tự tải lại trong khoảng 0,5 giây, gồm cả form đang ẩn. Giữ bộ lọc, các mục đang chọn và nội dung đang soạn; cập nhật dữ liệu tham chiếu, tên, giá và tồn kho. Giỏ hàng tính lại voucher, tổng tiền, tiền thừa; báo và khóa thanh toán nếu thuốc ngừng bán hoặc vượt tồn. Khi đổi giá ngay trước thanh toán, yêu cầu kiểm tra lại số tiền mới.
- Đồng bộ áp dụng trong cùng ứng dụng; nút Làm mới trang chính tải lại toàn bộ form đang mở. Quyền truy cập vẫn theo cơ chế chung hiện có của dự án; không bổ sung vai trò.
- Không tạo database/bảng mới; tiếp tục dùng Employees trong PharmacyStoreTrialDB. Sao lưu trước thay đổi: Backups/BACKUP-before-employees-windows.zip. Mã nhân viên chia trong Store.POS/Forms/NhanVien, Store.BLL/NhanVien và Store.DAL/NhanVien; phần tải lại của mỗi form nằm cùng form trong file .Refresh.cs.
- Kiểm tra: build 0 cảnh báo/0 lỗi; 94 kiểm tra mới nhân viên/cửa sổ, 106 nghiệp vụ, 130 nhập kho/voucher/hóa đơn, 63 tìm kiếm/bỏ chọn, 33 thêm nhanh, 18 Designer không SQL và 598 bố cục (tổng 1.042 kiểm tra). Đã chạy lại 94 kiểm tra mới trên bản build thông thường. Nhật ký: artifacts/logs/employees-windows-*.log. Ảnh: artifacts/previews/frmNhanVien.png và sales-refresh-warning.png.
- Bản chạy đã cập nhật: Store.POS/bin/Debug/net8.0-windows/Store.POS.exe. Các kiểm tra dọn đúng dữ liệu tạm; dữ liệu có sẵn còn 2 nhân viên, 1 hóa đơn, 2 khách hàng và 4 thuốc. Không sửa thư mục PharmacyStore gốc.

## Form nhúng và điều hướng dọc (23/10/2026)

- Bỏ thanh điều hướng ngang; chuyển Trang chủ và Form đang mở sang thanh dọc bên trái. Cửa sổ chính mặc định phóng to, có thể thu/phóng bằng nút tiêu chuẩn của Windows.
- Toàn bộ 12 form chức năng nhúng không viền trong contentHost; mỗi lần hiện một form. Chuyển chức năng, về Trang chủ hoặc Tạm ẩn chỉ ẩn form để giữ giỏ hàng, phiếu nhập và nội dung chưa lưu. Mở lại từ nút chức năng hoặc Form đang mở sẽ dùng đúng instance cũ. Đóng form đang hiện trả về trang tổng quan.
- Giữ cơ chế đồng bộ khi lưu dữ liệu, gồm cả form đang ẩn. Giữ quản lý nhân viên, quy tắc làm tròn, số điện thoại, mã vạch và trừ kho FEFO. F1–F6 vẫn hoạt động khi con trỏ đang ở form nhúng.
- Bố cục trang chính nằm trong Designer với TableLayoutPanel; sửa thứ tự Dock/Anchor để bố cục co giãn thật sự. Bán hàng có giỏ hàng co giãn, cột thanh toán riêng; phần tiền mặt có thể cuộn nếu thiếu chiều cao. Nút thao tác giỏ hàng và danh sách tìm kiếm đặt theo vị trí thực của bảng/ô tìm kiếm.
- Kiểm tra: 61 kiểm tra form nhúng/kích thước, 94 giữ trạng thái/nhân viên/đồng bộ, 106 nghiệp vụ, 130 nhập kho/voucher/hóa đơn, 63 tìm kiếm/bỏ chọn, 33 thêm nhanh, 18 Designer không SQL, 598 bố cục; tổng 1.103 kiểm tra đạt. Build thường và bản kiểm tra đều 0 cảnh báo/0 lỗi. Chưa thử mức DPI khác.
- Nhật ký tại artifacts/logs/embedded-forms-*.log. Ảnh tại artifacts/previews/embedded-home.png, embedded-sales-laptop.png, embedded-sales-cash.png và embedded-employees.png. Có kiểm tra cửa sổ ở kích thước 1100x700, 1366x728 và 1600x900.
- Sao lưu trước thay đổi: Backups/BACKUP-before-embedded-forms.zip. Bản chạy: Store.POS/bin/Debug/net8.0-windows/Store.POS.exe. Không thay cấu trúc database.
