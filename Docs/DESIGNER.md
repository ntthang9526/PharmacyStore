# Hướng dẫn chỉnh sửa giao diện trong Visual Studio

Mở solution `PharmacyStore.slnx`, vào project `Store.POS/Forms`, mở file Form bất kỳ (ví dụ `frmBanHang.cs`, `frmMain.cs`, `frmQuanLyThuoc`, `frmKhachHang.cs`,...) bằng **View Designer** (phím tắt `Shift + F7`).

---

## 1. Cơ chế bố cục chuẩn: Sử dụng `Dock` thay vì `Anchor`

- **Không sử dụng `Anchor = Top | Bottom | Left | Right` kết hợp với `Dock = None`:**
  - `Anchor` dựa trên tọa độ pixel tuyệt đối ban đầu. Khi form thay đổi kích thước, phóng to cửa sổ hoặc co giãn responsive, `Anchor` sẽ khiến các control (nút, bảng bố cục, ô nhập) bị kéo giãn biến dạng bất thường ("tự phình to").
  - Do đó, toàn bộ hệ thống bố cục tuân thủ chuẩn WinForms: sử dụng **`Dock = DockStyle.Fill`** (cho bảng, danh sách, container) và **`Dock = DockStyle.Top`** (cho ô nhập liệu, hàng lọc ngày) kết hợp với **`TableLayoutPanel`** và **`Margin`**.
- **Mở khóa chỉnh sửa trong Visual Studio Designer:**
  - Form chính `frmMain`: Đã gỡ bỏ cờ `$this.Locked = True` trong file `.resx`, cho phép chọn và chỉnh sửa tự do.
  - Để thay đổi kích thước ô chứa trong `TableLayoutPanel`: Bấm vào mũi tên nhỏ `►` ở góc trên bên phải của `TableLayoutPanel` -> chọn **Edit Rows and Columns** -> chỉnh lại độ rộng cột hoặc chiều cao dòng (`Percent` hoặc `Absolute` pixel).
- **Đối với ô nhập (`TextBox`):**
  - Cấu hình `AutoSize = false` cho các ô nhập để cho phép chỉnh cả chiều cao (`Size.Height`) trực tiếp trong cửa sổ Properties.

---

## 2. Chuẩn hóa ô nhập và màu sắc giao diện

- **Viền và ô nhập phẳng:**
  - Loại bỏ hoàn toàn tình trạng viền lồng viền. Ô nhập trong các panel gói sử dụng `Dock = Fill` và `BorderStyle = None` bên trong panel có viền, mang lại đường viền đơn nét, phẳng và hiện đại.
  - Bố cục vùng thanh toán bên phải (`frmBanHang`) có chiều rộng 365px; các nút tra cứu, voucher, lưu đơn chờ và thanh toán nằm trong khung. Khi thiếu chiều cao, vùng thanh toán có thể cuộn.
- **ComboBox:**
  - `FlatStyle` của các ComboBox (`cboNhomThuoc`, `cboNhaCungCap`, `cboDonViTinh`, `cboThuoc`, `cboLoai`,...) dùng `FlatStyle.Standard`.
  - ComboBox hiển thị rõ viền chuẩn và mũi tên chọn, không bị chìm/trùng màu nền trắng.
- **Ô chỉ đọc (Read-only / Calculated):**
  - Các ô chỉ đọc (như "Tiền trả lại" ở bán hàng, "Tồn kho" ở quản lý thuốc) được phân biệt rõ với nền xám nhạt (`#F1F5F9`), giúp người dùng nhận diện ngay đây là ô hiển thị dữ liệu tính toán tự động.

---

## 3. Lưu ý khi biên dịch

- Sau khi chỉnh sửa bằng Visual Studio Designer, chỉ cần nhấn **F5** hoặc **Ctrl + Shift + B** để biên dịch lại.
- Các file tài nguyên `.Refresh.resx`, `.Excel.resx`, `.Dashboard.resx` đã có cơ chế tự động loại trừ (`ExcludePartialFormResources` trong `Store.POS.csproj`), không gây lỗi `MSB3577`.


## 4. Chỉnh cột của các bảng

**Nguyên tắc: chỉ fill khi tổng độ rộng các cột đang hiển thị nhỏ hơn khung chứa. Khi cột rộng hơn khung, giữ Width đã thiết kế và dùng cuộn ngang; không ép co cột.**

- Chọn bảng trong View Designer, bấm **Edit Columns**. Các cột đã được khai báo sẵn với tiêu đề tiếng Việt và `DataPropertyName` tương ứng.
- Chọn cột rồi chỉnh **Width**. `AutoSizeMode = None` cho phép tự căn độ rộng bằng `Width`; giới hạn nhỏ nhất là `MinimumWidth` của từng cột; đây là độ rộng gốc. Khi còn khoảng trống trong khung, cột tên được giãn để fill; khi khung nhỏ lại, cột trở về độ rộng gốc. Bảng rộng hơn khung dùng cuộn ngang.
- Đổi tiêu đề bằng **HeaderText**. Giữ nguyên `DataPropertyName` để dữ liệu vẫn hiển thị đúng. Các cột mã nhóm/NCC trong quản lý thuốc được ẩn vì đã có cột tên.
- File `.resx` của từng form chứa `UserAddedColumn = True` cho các cột khai báo sẵn. Giữ các mục này để Visual Studio không loại bỏ cột khi mở/lưu Designer.
- Chế độ không tự sinh cột dữ liệu được đặt trong `PosFormStyle.ApplyTableStyle`, vì Visual Studio không lưu thuộc tính `AutoGenerateColumns` trong Designer. Tiêu đề và độ rộng gốc giữ trong Designer. ConfigureConditionalFill chỉ giãn phần còn trống, không ép cột co nhỏ hơn độ rộng gốc.

## 5. Tránh Designer tự sinh lại giao diện

- Các bảng bán hàng không gắn mẫu BindingSource trong Designer. Danh sách thuốc và giỏ hàng được gắn dữ liệu trong sự kiện Load khi chạy, nên Designer không thêm lại các cột từ model.
- Mỗi cột khai báo sẵn có UserAddedColumn trong resx. Khi sửa cột, giữ DataPropertyName của cột còn sử dụng.
- Phân quyền chỉ ẩn thao tác bị cấm, không đặt lại vị trí nút, Margin hoặc chiều cao hàng về giá trị cố định. Nút đã ẩn trong thiết kế được giữ nguyên khi quản trị viên mở form.
- Độ rộng gốc lấy từ Designer; chỉ fill khi tổng độ rộng cột nhỏ hơn khung chứa. Cột bị xóa không được tạo lại khi làm mới hoặc thay đổi kích thước.
- Khi đang chỉnh cùng một form, lưu và đóng tab Designer trước khi sửa file Designer từ bên ngoài Visual Studio để tránh bản chưa lưu trong bộ nhớ ghi đè tệp.
## 6. Nút thao tác ở khung chi tiết

- Thuốc, Nhóm thuốc, Nhà cung cấp, Khách hàng và Voucher dùng FlowLayoutPanel ở đáy khung. Nút được xếp liên tiếp; nút bị ẩn theo quyền không chiếm ô trống.
- Chỉnh Size và Margin của nút trong Designer; chỉnh FlowDirection/WrapContents của panel để thay đổi cách xếp. Hàng chứa nút dùng AutoSize, hàng trống phía trên dùng Percent 100 để giữ các nút sát đáy.
- Phân quyền chỉ ẩn thao tác, gồm cả File mẫu ở Quản lý thuốc; không ghi đè kích thước hoặc vị trí đã thiết kế.

## 7. Giao diện thống nhất toàn hệ thống (29/10/2026)

- Nền trang xám nhạt, vùng nhập liệu trắng, màu chính xanh ngọc; cột tiền căn phải. Các thuộc tính màu, phông, khoảng cách và độ rộng cột nằm trong Designer.
- Menu bên trái đánh dấu màn hình đang mở; tiêu đề phía trên thể hiện vị trí hiện tại. Hóa đơn có mục riêng và phím F3. Chuyển màn hình vẫn giữ giỏ hàng hoặc nội dung đang nhập.
- Giỏ hàng ưu tiên tên thuốc, đơn vị, số lượng, đơn giá và thành tiền. Nút ⋯ mở thao tác tại dòng trong vùng giỏ, không che vùng thu tiền. Các cột khác vẫn có thể chỉnh trong Edit Columns.
- Hóa đơn: thao tác thu tiền/sửa/hủy chỉ hiện khi chọn đơn chờ; nút xuất Excel nằm riêng. Cột Cần thu hiển thị số còn phải thu của đơn chờ, bằng 0 với đơn đã thanh toán hoặc hủy. Việc này chỉ thay đổi hiển thị; dữ liệu tiền hàng và lịch sử giữ nguyên.
- Hóa đơn và phiếu nhập dùng toàn bộ chiều rộng; chọn một dòng mới hiện chi tiết bên dưới. Tỷ lệ hai vùng lấy từ RowStyles trong Designer, được khôi phục khi chọn chứng từ. Bấm vùng trống để bỏ chọn và thu gọn chi tiết.
- Báo cáo đặt bảng theo ngày phía trên và bảng hóa đơn phía dưới; bốn số tổng hợp có cột riêng với độ rộng bằng nhau.
- Đã kiểm tra bố cục 14 màn hình nhúng ở kích thước cửa sổ 1500×820, 1382×753 và 1100×700. Chưa xác nhận trên các mức DPI khác của Windows; nên kiểm tra lại khi thay máy hoặc đổi mức phóng đại màn hình.

## 8. Đơn thuốc và bản in

- `frmDonThuoc` có file Designer/resx riêng, cột thuốc và hướng dẫn dùng khai báo sẵn. Chỉnh bố cục bằng RowStyles/ColumnStyles; không dùng danh sách thuốc để tự sinh cột trong Designer.
- Ô **Thuốc kê đơn** nằm cùng hàng tiêu đề tên thuốc trong editor danh mục, không tăng chiều cao khung nhập. Nút **Đơn thuốc** nằm ở hàng nhập mã dưới giỏ.
- Nút **In / PDF** được khai báo trong Designer hóa đơn; chỉ bật khi chọn đơn đã thanh toán. `frmHoaDon.Print.cs` và `frmBanHang.Prescription.cs` là phần mã, không phải form độc lập.


## Phím tắt thao tác

- F1 Trang chủ; F2 Bán hàng; F3 Hóa đơn; F4 Quản lý thuốc; F5 Quản lý khác; F6 Nhập kho; F7 Cảnh báo; F8 Báo cáo. Phím tắt trùng với nhãn trên thanh bên và dùng được khi đang nhập liệu trong màn hình con.
- Esc không đóng màn hình bán hàng hay hủy giỏ hàng. Chuyển màn hình vẫn giữ giỏ đang nhập.
- Thông báo thao tác dùng câu ngắn; các xác nhận xóa, hủy và nhận tiền vẫn được giữ.

## Ô mã vạch trong Quản lý thuốc

txtMaVach và lblMaVach nằm trong barcodeLayout ở file Designer. Đơn vị/giá bán dùng unitPriceLayout đặt cạnh nhau, giữ nguyên tổng chiều cao khung nhập liệu. Có thể chỉnh vị trí, Size, Margin hoặc tỉ lệ cột của các panel bằng Designer; runtime chỉ quản lý trạng thái nhập và dữ liệu.

Quản lý thuốc: `medicineCodesLayout` chứa `barcodeLayout` và `ingredientLayout` cùng hàng; `txtHoatChat` và cột `colHoatChat` khai báo trong Designer, có thể chỉnh kích thước tại đó.
