> Cập nhật SQL Server 05/11/2026: bản chạy chính đã dùng chung database POS, bảng web có tiền tố Web. Các mô tả SQLite/tài khoản mẫu/cấu trúc cũ bên dưới là tài liệu trước chuyển đổi; ưu tiên [SQLSERVER-POS-INTEGRATION.md](SQLSERVER-POS-INTEGRATION.md).

# KỊCH BẢN DEMO BÁO CÁO BÀI TẬP LỚN (DEMO SCRIPT)
## WEBSITE BÁN HÀNG & QUẢN TRỊ NHÀ THUỐC PHARMACY

Tài liệu này được biên soạn để sinh viên trình bày trực tiếp với Giảng viên bộ môn theo một luồng tương tác thực tế, trực quan và liền mạch từ phía **Khách hàng (Storefront)** đến **Quản trị viên (Admin)**.

---

## 1. Chuẩn bị Trước Buổi Demo

1. Mở Terminal tại thư mục `E:\PharmacyStoreSolution\PharmacyWeb\src\PharmacyWeb` và chạy lệnh:
   ```powershell
   dotnet run
   ```
2. Mở trình duyệt tại địa chỉ: `http://localhost:5000` (hoặc cổng hiển thị trên console).
3. Tài khoản mẫu chuẩn bị sẵn:
   - **Quản trị viên (Admin):** Tài khoản `admin` / Mật khẩu: `Admin@123456`
   - **Khách hàng (Customer):** Tài khoản `0912345678` / Mật khẩu: `Customer@123456` (Trần Văn Tuấn)

---

## 2. Kịch bản Demo 1 — Khách hàng Mua sắm & Đặt hàng (Storefront)

### Bước 1: Khám phá Trang chủ & Tìm kiếm Thuốc
- **Thao tác:** Trên trang chủ `http://localhost:5000/`, chỉ cho giảng viên thấy:
  + Header 3 tầng cố định với nhận diện màu Teal POS (`#0F766E`) và Mint POS (`#CCFBF1`).
  + Khu vực Hero đôi: Banner bên trái và Hộp hành động **"Mua thuốc theo đơn (Rx) - 3 bước"** bên phải.
  + Gõ vào ô tìm kiếm: `"Panadol"` hoặc `"Paracetamol"`.
- **Điểm nhấn giải thích:** Bố cục tuân thủ chuẩn nhà thuốc Long Châu, font chữ Be Vietnam Pro hỗ trợ tiếng Việt sắc nét.

### Bước 2: Duyệt Catalog & Lọc Bất Đồng Bộ (AJAX)
- **Thao tác:** Bấm vào menu **"Tất cả sản phẩm"** (`/Products`).
  + Chọn nhóm thuốc *"Thuốc giảm đau - hạ sốt"* hoặc kéo khoảng giá.
  + Đổi kiểu sắp xếp sang *"Giá tăng dần"*.
- **Điểm nhấn giải thích:** Lưới sản phẩm cập nhật tức thì qua **Fetch AJAX** (trả về Partial View phía server), không bị chớp giật hay reload toàn bộ trang.

### Bước 3: Phân biệt Thuốc OTC & Thuốc Kê đơn (Rx)
- **Thao tác:** 
  + Chỉ vào sản phẩm **Panadol Extra**: Có nhãn *"Mua trực tiếp (OTC)"*, nút bấm *"Chọn mua"* cho phép thêm thẳng vào giỏ hàng.
  + Chỉ vào sản phẩm **Augmentin 1g**: Có nhãn *"Thuốc kê đơn (Rx)"*, nút chuyển thành *"Gửi đơn tư vấn"*, backend chặn tuyệt đối việc mua tự do không có đơn.

### Bước 4: Giỏ hàng, Mã Giảm Giá & Phí Giao Hàng
- **Thao tác:** Mở Giỏ hàng (`/Cart`).
  + Thử tăng số lượng thuốc bằng nút `+` / `-`.
  + Nhập mã voucher `GIAM10K` &rarr; Bấm **"Áp dụng"**: Giảm ngay 10.000 ₫ vào tổng thanh toán.
  + Nhập thử mã `PHHET` để giảng viên thấy hệ thống báo lỗi thực tế: *"Mã giảm giá đã hết lượt sử dụng"*.
  + Giải thích quy tắc: Đơn từ 100.000 ₫ được **Miễn phí giao hàng**, đơn dưới 100.000 ₫ tính phí 15.000 ₫.

### Bước 5: Tiến hành Đặt hàng (Checkout) & Mã Đơn Thật
- **Thao tác:** Bấm **"Tiến hành đặt hàng"** &rarr; Đăng nhập tài khoản `0912345678` / `Customer@123456`.
  + Hệ thống tự động điền sẵn tên người nhận và sổ địa chỉ đã lưu.
  + Chọn phương thức **"Thanh toán khi nhận hàng (COD)"** &rarr; Bấm **"Xác nhận & Đặt hàng ngay"**.
- **Điểm nhấn giải thích:** Trang kết quả hiển thị **Mã đơn hàng thật** (`HD...`), trạng thái ban đầu là `Chờ xác nhận (NEW)` và `Chưa thu tiền (UNPAID)`. Khóa **IdempotencyKey** bảo vệ chống tạo trùng đơn nếu khách bấm lặp lại.

### Bước 6: Khách hàng Gửi Đơn Thuốc Tư vấn
- **Thao tác:** Vào mục **"Tư vấn đơn thuốc"** (`/PrescriptionRequest`).
  + Chọn ảnh đơn thuốc mẫu và bấm gửi.
  + Đơn được tạo với mã `RX-...` ở trạng thái `Chờ dược sĩ kiểm tra`. Ảnh được lưu trữ bảo mật trong thư mục riêng tư, chỉ tải qua API có xác thực.

---

## 3. Kịch bản Demo 2 — Quản trị viên Xử lý Nghiệp vụ (Admin Back-Office)

### Bước 1: Tổng quan KPI Dashboard & Phân quyền Server
- **Thao tác:** Đăng xuất và đăng nhập tài khoản `admin` / `Admin@123456` &rarr; Bấm **"Khu Vực Quản Trị"** (`/Admin/Dashboard`).
  + Thử đăng nhập tài khoản Customer rồi gõ trực tiếp URL `/Admin/Dashboard`: Giảng viên sẽ thấy màn hình **`403 — Không Đủ Quyền Truy Cập`** bảo vệ chặt chẽ ở cấp độ Server.
  + Trên Dashboard: Xem số lượng đơn hàng mới, doanh thu đã thu hôm nay, cảnh báo các lô sắp hết hạn (<90 ngày) và lô tồn thấp.

### Bước 2: Xử lý Đơn hàng Theo Luồng Trạng thái & Xuất kho FEFO
- **Thao tác:** Vào mục **"Quản lý Đơn hàng"** (`/Admin/Orders`), chọn đơn vừa đặt ở Kịch bản 1:
  + Xem bảng **"Phân bổ lô hàng (FEFO)"**: Giảng viên sẽ thấy hệ thống tự động giữ chỗ từ lô có hạn dùng gần nhất (`LOT-PND-2024A`).
  + Bấm nút **"Xác nhận đơn (CONFIRMED)"** &rarr; Bấm **"Xuất giao hàng (SHIPPING)"**.
  + Khi bưu tá báo giao thành công: Bấm **"Xác nhận thu COD & Hoàn tất (COMPLETED)"**.
- **Điểm nhấn giải thích:** Lúc này hệ thống thực hiện trừ tồn kho vĩnh viễn (`Stock -= Quantity`), chuyển trạng thái thanh toán sang `PAID` và ghi nhận người thu cùng thời gian chi tiết trong nhật ký kiểm toán.

### Bước 3: Quản lý Kho Lô Hàng & Lập Phiếu Nhập Nhiều Dòng
- **Thao tác:** Vào mục **"Kho & Lô hàng"** (`/Admin/Inventory`):
  + Lọc danh sách theo các lô *"Sắp hết hạn"*, *"Đã hết hạn"*, *"Tạm khóa"*.
  + Bấm nút **"Tạm khóa lô"** &rarr; Lập tức tồn có thể bán của thuốc giảm tương ứng trên toàn hệ thống.
  + Vào **"Lập phiếu nhập kho"**: Chọn nhà cung cấp, thêm 2 dòng thuốc với số lô, hạn dùng và đơn giá vốn &rarr; Bấm lưu để ghi nhận phiếu nhập và tăng tồn kho.

### Bước 4: Thêm Thuốc Mới với Rich Text Editor & Upload Ảnh
- **Thao tác:** Vào mục **"Thuốc & Sản phẩm"** &rarr; **"Thêm thuốc mới"**:
  + Soạn thảo mô tả có in đậm, danh sách chấm bằng **Rich Text Toolbar**.
  + Tải ảnh chính và ảnh phụ &rarr; Bấm lưu.
- **Điểm nhấn giải thích:** Toàn bộ nội dung Rich Text được lọc trắng qua `HtmlSanitizerService` ở server để loại bỏ script độc hại trước khi lưu DB.

### Bước 5: Xem Đơn Thuốc Tư vấn & Phản hồi Dược sĩ
- **Thao tác:** Vào mục **"Đơn thuốc tư vấn"** (`/Admin/Prescriptions`):
  + Bấm xem đơn `RX-...` gửi ở Kịch bản 1 &rarr; Bấm xem ảnh đơn thuốc phóng to.
  + Nhập nội dung dặn dò của dược sĩ và bấm **"Gửi phản hồi cho bệnh nhân"**.

### Bước 6: Báo cáo Doanh thu Thực thu & Xuất CSV
- **Thao tác:** Vào mục **"Báo cáo Doanh thu"** (`/Admin/Reports`):
  + Chọn khoảng ngày lọc theo ngày thực tế.
  + Chỉ cho giảng viên thấy: Báo cáo doanh thu **chỉ cộng dồn các đơn đã thực thu (PAID)**, hoàn toàn không tính đơn mới hay đơn hủy.
  + Bấm nút **"Xuất dữ liệu CSV"** &rarr; Tải về file `.csv` chuẩn UTF-8 xem được ngay trong Excel.

---

## 4. Kịch bản Demo 3 — Bộ Kiểm thử Tự động (Testing Proof)

- **Thao tác:** Mở Terminal và gõ:
  ```powershell
  dotnet test
  ```
- **Kết quả trình chiếu:** **15/15 Passed (100%)** bao gồm các bài test nghiệp vụ quan trọng nhất:
  1. Kiểm thử thuật toán xuất kho FEFO.
  2. Kiểm thử chống tạo trùng đơn (Idempotency Key).
  3. Kiểm thử giải phóng lượng giữ và hoàn voucher khi khách hủy đơn.
  4. Kiểm thử trừ tồn kho vĩnh viễn khi Admin xác nhận thu tiền COD.
  5. Kiểm thử lọc thẻ `<script>` và sự kiện XSS trong Rich Text.
