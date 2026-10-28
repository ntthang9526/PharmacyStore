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

## Thu nhỏ cửa sổ mặc định (24/10/2026)

Đổi cửa sổ chính về trạng thái Normal, mở giữa màn hình với vùng nội dung 1382x753 như kích thước cũ. Vẫn cho phép người dùng kéo giãn/phóng to khi cần; giữ giao diện nhúng và thanh điều hướng dọc.


## Sửa panel thao tác giỏ hàng (24/10/2026)

- Bố cục bán hàng thành 3 cột: Giỏ hàng → panelAction (120px) → Thanh toán. Panel không còn đặt nổi trên ô +, không che giỏ hàng hay thanh toán; cấu hình nằm trong Designer.
- Bấm dòng hoặc rê vào nút + để chọn sản phẩm cần thao tác. Giữ sản phẩm đang sửa khi chuột đi qua vùng trống, dòng khác hoặc khi tải lại dữ liệu; dùng tham chiếu dòng giỏ hàng thay vì phụ thuộc vào vùng tô chọn. Sửa số lượng, Enter, xóa và đặt số lượng 0 đều cập nhật đúng dòng; xóa hết giỏ thì đóng panel.
- Mở rộng giới hạn hiển thị số lượng đến int.MaxValue để không lỗi khi giỏ có dòng trên 1000; kiểm tra tồn kho khi áp dụng vẫn giữ nguyên.
- Sửa lỗi build trùng frmBanHang.resources do frmBanHang.Refresh.resx rỗng bị nhận nhầm là tài nguyên form; giữ tệp, loại khỏi EmbeddedResource và đánh dấu các .Refresh.cs là Code, nhóm dưới form tương ứng. Khôi phục 4 cột bị thiếu trong Designer khách hàng, giữ các chỉnh sửa bố cục hiện có.
- Build thường: 0 cảnh báo/0 lỗi. Đạt 125 kiểm tra nghiệp vụ (gồm 19 kiểm tra mới panel), 61 form nhúng, 18 Designer không SQL, 94 trạng thái/đồng bộ/nhân viên, 63 tìm kiếm/bỏ chọn và 598 bố cục (tổng 959 kiểm tra). Kiểm tra panel không chồng lấn ở 1182x653, 1120x650 và 900x570.
- Nhật ký: artifacts/logs/cart-actions-*.log. Ảnh: artifacts/previews/cart-actions-columns.png. Bản sao trước sửa giỏ hàng: Backups/BACKUP-before-cart-actions.zip; trước khôi phục cột khách hàng: Backups/BACKUP-customer-designer-before-column-repair.zip. Không thay đổi cấu trúc database.

## Điều chỉnh panel theo yêu cầu (24/10/2026)

Bỏ cột thao tác riêng, phục hồi hai cột Giỏ hàng/Thanh toán. Panel action nổi phía trên groupbox Thanh toán, nằm hoàn toàn bên phải giỏ hàng; tự đặt theo dòng đang chọn và giới hạn trong vùng thanh toán khi cuộn/đổi kích thước. Giữ các sửa lỗi chọn dòng, sửa/xóa và tải lại dữ liệu. Build 0 lỗi/0 cảnh báo; 125 kiểm tra đạt. Ảnh: artifacts/previews/cart-actions-payment-overlay.png.


## Khôi phục tự ẩn panel thao tác (24/10/2026)

Khôi phục xử lý MouseLeave cho panel và toàn bộ ô nhập/nút bên trong. Chuột ra khỏi nút + của dòng đang thao tác và panel thì tự ẩn sau 180ms; thời gian ngắn này cho phép đi qua khoảng cách từ nút + sang panel. Rê vào panel/ô nhập sẽ hủy lịch ẩn. Giữ vị trí nổi trên Thanh toán và các sửa lỗi dòng chọn. Build 0 lỗi/0 cảnh báo, 130 kiểm tra nghiệp vụ và hành vi panel đạt.


## Tinh gọn điều hướng (24/10/2026)

Bỏ nút Tạm ẩn ở 12 form chức năng và nút Form đang mở ở thanh bên; bỏ menu và mã xử lý dư. Chuyển chức năng tự ẩn form cũ, giữ instance và dữ liệu chưa lưu; bấm chức năng tương ứng để mở lại. Trang chủ ẩn các form và hiện tổng quan. Giữ đồng bộ dữ liệu và hành vi panel thao tác đã sửa. Build 0 lỗi/0 cảnh báo; đạt 71 kiểm tra điều hướng nhúng, 92 giữ trạng thái/đồng bộ/nhân viên, 130 nghiệp vụ/panel, 18 Designer và 554 bố cục. Sao lưu: Backups/BACKUP-before-simple-navigation.zip.


## Phân quyền, giá nhập và phiếu nhập (24/10/2026)

- Tiếp tục sửa trên PharmacyStore-MergedTrial, không sửa PharmacyStore gốc. Sao lưu nguồn trước sửa: Backups/BACKUP-before-roles-import-history.zip.
- Thêm Employees.Role (ADMIN / STAFF) trên PharmacyStoreTrialDB hiện có bằng Store.DAL/Database/AddEmployeeRoles.sql. Script có thể chạy lại, không tạo database/bảng mới, không đặt lại mật khẩu hoặc dữ liệu. Tài khoản admin được gán ADMIN lần đầu; các tài khoản cũ khác là STAFF. InitDatabase.sql đã đồng bộ cấu trúc cho database mới, không chạy lại trên database đang dùng.
- Admin quản lý thuốc, nhóm, NCC, nhập kho, voucher và nhân viên; được đổi vai trò/đặt lại mật khẩu nhân viên. Nhân viên bán hàng, thêm/sửa thông tin cơ bản khách hàng, tra cứu thuốc/danh mục/lô/voucher; không tự chỉnh điểm hay xóa khách hàng. Cả hai xem toàn bộ hóa đơn, báo cáo và phiếu nhập, không lọc theo nhân viên lập.
- Quyền được kiểm tra khi ghi dữ liệu, trong transaction; đối chiếu tài khoản đang hoạt động và EmployeeId của phiên với nhân viên lập giao dịch. Chặn sửa quyền bằng cách giả mã Admin, thêm nhanh, chỉnh giá và sửa voucher ngoài giao diện. Không khóa tài khoản hiện tại hoặc khóa/hạ quyền Admin cuối cùng. Giao diện cập nhật quyền khi chuyển chức năng, sau ghi dữ liệu và định kỳ 5 giây; editor có quyền thay đổi được khởi tạo lại khi vai trò đổi, giỏ bán hàng vẫn giữ nguyên.
- Giá nhập gần nhất trên bảng thuốc/lô lấy từ StockInDetails theo ImportDate, StockInId, DetailId. Có nhãn riêng; dữ liệu lô chưa có phiếu nhập hiện dấu —. Không lấy giá bán làm giá nhập, không suy ra giá vốn tồn kho từ giá nhập gần nhất.
- Quản lý khác → Phiếu nhập hàng: tìm theo mã phiếu, NCC, nhân viên, ghi chú, tên thuốc, số lô/mã vạch; lọc khoảng ngày. Chọn phiếu để xem chi tiết thuốc, đơn vị, số lô, HSD, số lượng, giá nhập, thành tiền và nhân viên nhập. Phiếu đã lưu chỉ xem, giữ nguyên giá lịch sử; tạo phiếu mới vẫn ở Nhập kho (F4), chỉ Admin. Chưa thêm sửa/hủy phiếu đã ghi sổ vì cần xử lý đối ứng tồn kho.
- Quản lý khác → Đổi mật khẩu: chỉ đổi tài khoản hiện tại, yêu cầu mật khẩu cũ, mật khẩu mới 6–128 ký tự và xác nhận khớp, không trùng mật khẩu cũ. Lưu PBKDF2 có salt, kiểm tra ghi đồng thời để không ghi đè mật khẩu vừa thay đổi. Chuyển form sẽ xóa ô mật khẩu. Admin đặt lại mật khẩu khi quên tại Quản lý khác → Nhân viên → Sửa.
- Bố cục hai form mới nằm trong Designer, mở Design không cần SQL; đã kiểm tra khi nhúng và thu nhỏ cửa sổ. Bộ kiểm tra --roles bao gồm 254 kiểm tra quyền/mật khẩu/lịch sử/đồng bộ và bố cục, dọn riêng fixture sau chạy. Các bộ hồi quy đạt: nghiệp vụ 132, nhân viên/cửa sổ 93, điều hướng nhúng 71, Designer 20, bố cục 592. Build solution 0 cảnh báo/0 lỗi. Nhật ký artifacts/*roles-check.log và artifacts/roles-check.log.
- Giới hạn bảo mật hiện tại: đây là kiểm tra quyền trong ứng dụng POS đang kết nối SQL trực tiếp bằng Windows. Không ngăn người có quyền SQL trực tiếp hoặc sửa chương trình máy trạm. Khi triển khai máy trạm không tin cậy, chuyển thao tác dữ liệu qua API xác thực và kiểm tra quyền phía máy chủ; API hiện tại chỉ có đăng nhập, chưa có token/endpoint nghiệp vụ bảo vệ.

## Sửa phần giao diện bị che khi nhúng (24/10/2026)

- Rà cả 14 form chức năng khi nhúng vào cửa sổ chính 1382x753 và khi thu về 1100x700. Kiểm tra mới tính phần nhìn thấy qua toàn bộ các panel cha, không chỉ giới hạn của panel trực tiếp; kiểm tra cả ô nhập, combo, bảng và chữ trong label. Bộ --layout-audit tạo ảnh từng màn hình vào artifacts/previews/layout-audit và báo lỗi nếu còn phần bị cắt.
- Bỏ MinimumSize 900x570 áp cứng vào form con khi nhúng. Vùng nội dung ở cửa sổ nhỏ thực tế chỉ rộng 880px, trước đây form con tràn mép phải và che một phần nút/cột. Giữ nguyên kích thước mặc định cửa sổ chính và kích thước tối thiểu của form khi mở độc lập.
- Form Nhân viên: nhóm thông tin chuyển sang hai cột nhãn/ô nhập để đủ chỗ cho vai trò. Nhóm nút cố định riêng ở dưới, đủ cả Thêm/Sửa/Lưu/Bỏ qua/Khóa; vùng thông tin có cuộn dự phòng. Rút gọn nhãn, thêm gợi ý mật khẩu giữ nguyên nếu để trống. Tự tăng chiều cao tiêu đề bảng; đặt độ rộng tối thiểu các cột để số điện thoại/vai trò không bị ép nhỏ.
- Form Thuốc: cột tên/nhóm/NCC có độ rộng tối thiểu và xuống dòng, hàng tự tăng chiều cao. Các cột số/tiền có độ rộng đủ đọc; dùng cuộn ngang khi vùng bảng nhỏ. Form Phiếu nhập tự tăng chiều cao tiêu đề hai bảng. Các dòng tiền Tạm tính/Giảm giá/Tổng tiền ở Bán hàng tăng chiều cao để không cắt ký tự theo font.
- Kiểm tra đạt: toàn bộ 14 form tại hai kích thước không còn control/label bị cắt, 94 nhân viên/cửa sổ, 71 điều hướng nhúng, 254 quyền/phiếu nhập, 132 nghiệp vụ/panel bán hàng, 20 Designer không SQL và 594 bố cục. Build solution 0 cảnh báo/0 lỗi. Nhật ký artifacts/layout-audit.log và artifacts/*layout-check.log.
- Chỉ thay đổi giao diện và bộ kiểm tra, không đổi cấu trúc/dữ liệu database. Sao lưu trước sửa: Backups/BACKUP-before-layout-clipping.zip. Chưa kiểm tra ở các mức phóng đại Windows khác với môi trường hiện tại.

## Nhân viên nhập kho và tinh gọn phân quyền (24/10/2026)

- Theo yêu cầu mới, STAFF được nhập kho như Admin: chọn NCC/thuốc có sẵn, thêm dòng, lập và lưu phiếu; dùng Quyen.NhapKho riêng trong transaction. EmployeeId phải khớp phiên, tài khoản còn hoạt động; phiếu ghi đúng nhân viên nhập. Quyền tạo/sửa thuốc, NCC, nhóm và voucher vẫn dành cho Admin, gồm cả các form thêm nhanh.
- Nhân viên không thấy mục Nhân viên trong Quản lý khác, các nút thêm/sửa/xóa/lưu/hủy chỉnh sửa ở màn hình chỉ xem, nút thêm nhanh NCC/thuốc/nhóm và ô chỉnh điểm khách hàng. Mở rộng ô chọn NCC/thuốc khi ẩn nút +. Không thay đổi quyền xem toàn bộ hóa đơn, báo cáo, lô và phiếu nhập.
- Bỏ hẳn TextBox mã tự sinh cùng nhãn ở Thuốc, NCC, Nhóm thuốc, Khách hàng và Nhân viên trong Designer. Các form dùng biến mã bản ghi riêng (hoặc selectedId đã có); sửa/xóa/tải lại vẫn dùng đúng bản ghi được chọn. Mã trên bảng danh sách, ô nhập số lô và mã tra cứu/voucher vẫn giữ.
- Đổi vai trò khi đang mở Nhập kho cập nhật ngay nút thêm nhanh; giữ instance và các dòng phiếu đang soạn. Các lần bị từ chối quyền không ghi dữ liệu. Không sửa cấu trúc database, không đặt lại tài khoản/mật khẩu.
- Build solution 0 cảnh báo/0 lỗi. Đạt 280 kiểm tra phân quyền/phiếu nhập, 69 tìm kiếm/chọn/sửa sau bỏ ô mã, 132 nghiệp vụ; kiểm tra bố cục cả 14 form ở cỡ mặc định và thu nhỏ đạt, Designer không SQL 20, bố cục độc lập 576. Bộ nhân viên/cửa sổ đạt 94 kiểm tra, gồm xác nhận không còn ô mã. Nhật ký artifacts/staff-stock-*.log. Sao lưu Backups/BACKUP-before-staff-stock-hidden-controls.zip.

## Gộp Excel, hình thức thanh toán và màu TEST_Update (25/10/2026)

- Gộp chọn lọc từ PharmacyStore-TEST_Update/PharmacyStore-test vào MergedTrial. Không chép form/layout, DBConnection, phân quyền IsAdmin theo username, script InitDatabase phá hủy dữ liệu hoặc script constraints của TEST_Update. Giữ các nút đã bỏ, ô mã đã bỏ, cache form/đồng bộ dữ liệu, các sửa panel action và quyền nhân viên nhập kho.
- Excel dùng ClosedXML 0.105.1. Danh mục thuốc có import/export/file mẫu; kho có import/file mẫu; báo cáo và hóa đơn có xuất Excel. Thêm preview chung trước nhập, kiểm tra loại mẫu/độ dài/giá/tồn hạn/master data, mã vạch trùng (trong DAL transaction), và nhập theo đúng NCC. Không tự tạo NCC/nhóm với thông tin giả. ID thuốc có trong file phải hợp lệ và khớp tên; tên trùng khi thiếu ID bị từ chối. Giữ barcode Text và tiền số trong Excel, xuất thuốc dùng lại được cấu trúc nhập. Mẫu nhập kho không điền sẵn số lượng/giá cho thuốc thực.
- Nút Excel nằm trong Designer, file xử lý .Excel.cs nhóm dưới form. Chỉ đổi bảng màu xanh ngọc/slate/emerald/rose theo TEST_Update, không sao chép font hoặc điều hướng cũ. Bổ sung bảng preview có Designer riêng. Kiểm tra cả nút Lưu phiếu và chi tiết hóa đơn khi thu nhỏ cửa sổ.
- Thêm Orders.PaymentMethod/PaymentStatus/PaidAt/PaidEmployeeId và OrderPendingItems bằng AddOrderPayments.sql; không tạo database mới. Dữ liệu cũ giữ PAID, method Chưa xác định, không giả định hình thức/giờ/nhân viên thu tiền cũ. InitDatabase được nối migration sau phần dữ liệu mẫu; script gốc vẫn chặn database đã tồn tại.
- Chờ thanh toán: lưu chi tiết riêng theo thuốc và giá đã chốt, chưa phân lô/giữ hàng/trừ kho/trừ voucher/ghi doanh thu. Khi thanh toán, khóa đơn chờ, đối chiếu số tiền, kiểm tra tài khoản/thuốc/voucher, trừ FEFO và tạo OrderDetails, ghi method/giờ/người thu, xóa dòng chờ, chuyển PAID cùng một transaction. Lỗi trả về giữ nguyên chờ và rollback. Hai tiến trình xác nhận chỉ một lần commit. Không cho chuyển đơn đã thu về chờ, chưa có hủy đơn/hoàn tiền.
- Báo cáo và KPI chỉ tính PAID theo COALESCE(PaidAt,OrderDate); danh sách hóa đơn hiện trạng thái, phương thức, tiền cần trả/đã thu, NV lập/NV thu và lọc chỉ chờ. Trang chủ có trạng thái trong hóa đơn gần nhất. Thanh toán QR vẫn xác nhận thủ công, không có bank webhook.
- Sao lưu nguồn trước gộp: Backups/BACKUP-before-excel-payment-theme.zip. Full backup SQL COPY_ONLY trước nâng cấp: C:/Program Files/Microsoft SQL Server/MSSQL17.SQLEXPRESS/MSSQL/Backup/PharmacyStoreTrialDB_before_excel_payment_20261025.bak.
- Build 0 cảnh báo/0 lỗi. Đạt 110 kiểm tra Excel/thanh toán (đọc lại file báo cáo/hóa đơn/mẫu/danh mục, chống xác nhận đồng thời, giá snapshot, ngày thu tiền, rollback voucher/tồn, UI đơn chờ ở hai kích thước), 132 nghiệp vụ, 285 phân quyền, 94 nhân viên/cửa sổ, 71 form nhúng, 69 tìm kiếm/chọn, 21 Designer không SQL, 594 bố cục và kiểm tra 14 form nhúng tại mặc định/thu nhỏ. Nhật ký artifacts/excel-*.log. Các fixture được dọn riêng; dữ liệu có sẵn còn 1 hóa đơn, 4 thuốc và 3 nhân viên, không còn dòng chờ kiểm thử.

## Chỉ cảnh báo lô sắp hết hạn (25/10/2026)

Theo yêu cầu, LayLoSapHetHan chỉ trả về lô có tồn > 0, thuốc đang kinh doanh và HSD từ hôm nay đến trước N ngày (mặc định 0–29 ngày). Loại lô HSD trước hôm nay khỏi cả trang chủ và F5; giữ nguyên dữ liệu lô, tồn kho và màn hình quản lý lô. Đổi tiêu đề/thẻ/nhãn trống thành Lô sắp hết hạn, bỏ nhánh tô đỏ lô quá hạn trong F5. Không đổi ngưỡng cảnh báo sắp hết hàng.

Đạt 140 kiểm tra nghiệp vụ, gồm các mốc hôm qua/hôm nay/1/29/30/31 ngày, lô hết tồn, ngưỡng tùy chọn, dữ liệu lô hết hạn còn nguyên và số lượng/nội dung hiển thị ở trang chủ/F5. Nhật ký artifacts/expiry-soon-check.log. Sao lưu Backups/BACKUP-before-expiry-soon-only.zip.

Ứng dụng hiện đang mở và khóa các DLL trong thư mục bin mặc định; không đóng phiên của người dùng. Build/test riêng ở artifacts/builds/expiry-soon-only, chạy bằng Store.POS.exe trong thư mục đó. Mã nguồn đã cập nhật; khi đóng ứng dụng, build thông thường sẽ tạo lại bản bin mới.


## 25/10/2026 — Bảng, bố cục phiếu và báo cáo doanh thu

- Tất cả bảng chọn cả hàng, tiêu đề giữ màu nền khi chọn; khóa kéo giãn cột, tiêu đề và dữ liệu không xuống dòng. Cột tự căn theo nội dung và có thanh cuộn ngang khi cần. Giữ kích thước cửa sổ chính hiện tại.
- Hóa đơn và phiếu nhập hàng: danh sách bên trái, chi tiết bên phải; thay đổi có trong Designer.
- Báo cáo chỉ lấy hóa đơn PAID theo ngày thanh toán, dùng ngày lập cho dữ liệu cũ chưa có PaidAt. Tổng và bảng theo ngày được tính từ cùng danh sách hóa đơn để tránh lệch khi có thanh toán đồng thời.
- Xuất Excel làm mới báo cáo theo khoảng ngày đang chọn trước khi xuất; bổ sung tên khách hàng vào dữ liệu xuất. Khoảng ngày sai xóa số liệu cũ trên form, không xuất báo cáo cũ.
- Kiểm tra tập trung: --grid-receipt-report; kiểm tra thêm --layout-audit, --excel-payments, --designer-offline và bộ smoke mặc định. Không đổi cấu trúc database, các bản ghi kiểm tra được dọn sau khi chạy.
- Bản chạy riêng: artifacts/builds/grid-receipt-report/Store.POS.exe. Dùng thư mục riêng vì ứng dụng đang mở khóa các thư viện trong bin thông thường.
- Bản sao nguồn trước sửa: Backups/BACKUP-before-grid-receipt-report.zip.


## 25/10/2026 — Giãn cột và lưu ngưỡng cảnh báo

- Bảng giãn đầy chiều ngang khi nội dung đủ chỗ; nội dung rộng hơn khung giữ độ rộng cần thiết và cuộn ngang. Tính lại khi dữ liệu hoặc kích thước đổi.
- F5 hiện lô sắp hết hạn tăng dần theo hạn trước, lô hết hạn cuối danh sách, số ngày âm hiển thị “Hết hạn” và hàng màu đỏ nhạt. Trang chủ tiếp tục chỉ hiện lô sắp hết hạn.
- Nhấn Enter trong ô ngưỡng tồn để lưu số nguyên 1–100.000. Giá trị được lưu theo tài khoản Windows tại LocalAppData/PharmacyStore/nguong-ton.txt, giữ sau khi tắt ứng dụng và áp dụng cho trang chủ. Không thay đổi số lượng hàng thực tế hay cấu trúc DB.
- Kiểm tra: --fill-alert, --grid-receipt-report, smoke mặc định và --layout-audit. Bản chạy: artifacts/builds/fill-alert-settings/Store.POS.exe. Bản sao nguồn: Backups/BACKUP-before-fill-alert-settings.zip.


## 25/10/2026 — Độ rộng cột quản lý trong Designer

- Bỏ ConfigureGridColumns và các sự kiện tự đo/ghi đè độ rộng cột khi chạy. Lựa chọn hàng và bỏ chọn vẫn nằm trong phần xử lý hành vi.
- Toàn bộ thiết lập cố định của bảng (chọn hàng, màu tiêu đề, không kéo giãn, không xuống dòng, độ rộng và FillWeight) nằm trong từng Designer.cs.
- Cột tên/địa chỉ/ghi chú nhận phần rộng còn dư; mã, ngày, đơn vị, số lượng và tiền dùng chiều rộng cố định phù hợp. Tên dài hiển thị cắt gọn, có tooltip mặc định của bảng.
- Xem trước Excel có cột khai báo sẵn trong Designer cho cả thuốc và nhập kho; runtime chỉ ẩn cột không có trong dữ liệu.
- Bố cục động theo thao tác (panel giỏ hàng, phân quyền, nhúng form), màu cảnh báo theo dữ liệu vẫn cập nhật runtime.
- Bản chạy: artifacts/builds/designer-column-widths/Store.POS.exe. Bản sao nguồn: Backups/BACKUP-before-designer-column-widths.zip.


## 25/10/2026 — Tăng nhẹ cửa sổ chính

- ClientSize mặc định 1500 × 820, đặt trong frmMain.Designer.cs; cửa sổ mở giữa màn hình, vẫn kéo giãn được.
- Kiểm tra bố cục form nhúng tại kích thước mới, kích thước cũ và khi thu nhỏ.
- Bản chạy: artifacts/builds/main-size-1500x820/Store.POS.exe.


## 26/10/2026 — Làm gọn ô nhập

- Ô nhập dùng viền phẳng, nền trắng, chữ màu slate; ô chỉ đọc nền xám nhạt. ComboBox kiểu Flat, font các ô nhập thông thường thống nhất Segoe UI 10.5.
- Ô nhập bố cục hẹp ở bán hàng giữ cỡ chữ vừa khung, ô chữ lớn giữ kích thước hiện có. Tất cả thuộc tính cố định nằm trong Designer.
- Bản sao: Backups/BACKUP-before-input-polish.zip. Bản chạy: artifacts/builds/input-polish/Store.POS.exe.
