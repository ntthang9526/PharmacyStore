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
  - Bố cục vùng thanh toán bên phải (`frmBanHang`) được căn chỉnh gọn gàng (chiều rộng 336px), các nút "Hủy", "Nhập", "Thanh toán" không bị đè hay tràn ra ngoài khung.
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

- Chọn bảng trong View Designer, bấm **Edit Columns**. Các cột đã được khai báo sẵn với tiêu đề tiếng Việt và `DataPropertyName` tương ứng.
- Chọn cột rồi chỉnh **Width**. `AutoSizeMode = None` và `MinimumWidth = 5` cho phép tự căn độ rộng; độ rộng này là mức gốc. Khi tổng cột nhỏ hơn khung, cột tên được giãn để lấp phần trống; khi khung nhỏ lại, các cột trở về mức gốc và có thể cuộn ngang.
- Đổi tiêu đề bằng **HeaderText**. Giữ nguyên `DataPropertyName` để dữ liệu vẫn hiển thị đúng. Các cột mã nhóm/NCC trong quản lý thuốc được ẩn vì đã có cột tên.
- File `.resx` của từng form chứa `UserAddedColumn = True` cho các cột khai báo sẵn. Giữ các mục này để Visual Studio không loại bỏ cột khi mở/lưu Designer.
- Chế độ không tự sinh cột dữ liệu được đặt trong `PosFormStyle.ApplyTableStyle`, vì Visual Studio không lưu thuộc tính `AutoGenerateColumns` trong Designer. Tiêu đề giữ trong Designer. ConfigureConditionalFill chỉ giãn cột khi còn khoảng trống; không ép các cột co nhỏ hơn độ rộng gốc.

## 5. Tránh Designer tự sinh lại giao diện

- Các bảng bán hàng không gắn mẫu BindingSource trong Designer. Danh sách thuốc và giỏ hàng được gắn dữ liệu trong sự kiện Load khi chạy, nên Designer không thêm lại các cột từ model.
- Mỗi cột khai báo sẵn có UserAddedColumn trong resx. Khi sửa cột, giữ DataPropertyName của cột còn sử dụng.
- Phân quyền chỉ ẩn thao tác bị cấm, không đặt lại vị trí nút, Margin hoặc chiều cao hàng về giá trị cố định. Nút đã ẩn trong thiết kế được giữ nguyên khi quản trị viên mở form.
- Fill giữ độ rộng mới nhất của cột làm mức gốc. Cột bị xóa không được tạo lại khi làm mới hoặc thay đổi kích thước.
- Khi đang chỉnh cùng một form, lưu và đóng tab Designer trước khi sửa file Designer từ bên ngoài Visual Studio để tránh bản chưa lưu trong bộ nhớ ghi đè tệp.