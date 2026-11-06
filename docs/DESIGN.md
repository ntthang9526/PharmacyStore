# DESIGN.md — Hệ thống Thiết kế Giao diện Website Nhà thuốc Pharmacy

## 1. Định hướng Thị giác & Triết lý Thiết kế (Bố cục Nhà thuốc Long Châu • Màu sắc & Tên theo Phần mềm POS)

- **Thương hiệu:** **Pharmacy** (đồng bộ tên và bảng màu với phần mềm quản lý nhà thuốc WinForms `Store.POS`, `Store.BLL`, `Store.DAL`).
- **Sự kết hợp giữa Bố cục Long Châu & Nhận diện POS:**
  1. **Header 3 tầng luôn cố định khi cuộn (`position: sticky; top: 0` trên `#appStoreHeader` + `body { overflow-x: clip }`):**
     - Thanh tiêu đề chính màu **Teal POS (`#0F766E` → `#115E59`)** + Topbar (`#134E4A`) kèm **thanh tìm kiếm hình viên thuốc (`border-radius: 9999px`)** và các từ khóa gợi ý phổ biến bên dưới (*Panadol Extra*, *Oresol*, *Vitamin C 500mg*, *Augmentin 1g*).
     - **Thanh Danh mục nền trắng (`#FFFFFF`)** gắn liền ngay dưới thanh tiêu đề, luôn hiển thị cố định trên đỉnh màn hình khi người dùng cuộn xuống (không lặp lại khối danh mục thừa trong nội dung trang chủ).
  2. **Bố cục Trang chủ chuẩn Long Châu:** Khu vực Hero đôi (Banner chính bên trái + Hộp hành động **"Mua thuốc theo đơn (Rx) - 3 bước"** bên phải), tiếp nối bởi **6 ô tiện ích nhanh** (*Cần mua thuốc*, *Thuốc OTC*, *Thuốc kê đơn*, *Đơn hàng của tôi*, *Vitamin & Đề kháng*, *Vật tư y tế*) và các khối sản phẩm đóng khung trắng (`.lc-section-card`) có dải ribbon tiêu đề Teal.
  3. **Thẻ Sản phẩm (Product Card):** Khung trắng bo góc `14px`, hiển thị **pill Đơn vị tính** (`Hộp`, `Chai`, `Tuýp`) màu Mint POS (`#CCFBF1`, chữ `#134E4A`), giá tiền in đậm màu `#115E59`, nút bấm hình viên thuốc **"Chọn mua"** (`#0F766E`) cho thuốc OTC hoặc **"Tư vấn ngay / Gửi đơn"** cho thuốc kê đơn Rx.
- **Kế thừa quy tắc từ phần mềm WinForms hiện có (`Store.POS`):**
  - Giữ màu chủ đạo **Teal `#0F766E`** và vùng nhấn **Mint `#CCFBF1` / `#134E4A`** từ `PosFormStyle.cs`.
  - Giữ quy tắc ô chỉ đọc/tính toán tự động nền xám nhạt (`#F1F5F9`), nút nguy hiểm đỏ hồng (`#BE123C`), cột tiền tệ và số lượng căn phải (`tabular-nums`), tách bạch `TotalStock` (Tồn thực tế) và `AvailableStock` (Tồn có thể bán).
- **Những điều tuyệt đối tránh (Anti-patterns):**
  - Không dùng hiệu ứng kính mờ (glassmorphism) toàn trang, không dùng hiệu ứng 3D xoay nghiêng hay animation làm thẻ sản phẩm nhảy khỏi lưới bố cục.
  - Không tự bịa đặt số giấy phép GPP, chứng nhận Bộ Y tế, đánh giá 5 sao giả, số lượng khách hàng ảo hay tư vấn y khoa tự động. Mọi thông tin liên hệ mẫu đều gắn nhãn `[Dữ liệu mẫu bài tập]`.

---

## 2. Hệ thống Màu sắc (Color Tokens — CSS Variables)

Tất cả cặp màu chữ và nền đều được kiểm tra đạt chuẩn tương phản **WCAG AA** (≥ 4.5:1 cho chữ thường, ≥ 3.0:1 cho chữ lớn và viền điều khiển).

| Nhóm | Token CSS | Mã Hex | Vai trò sử dụng | Độ tương phản |
| :--- | :--- | :--- | :--- | :--- |
| **Brand Primary (POS Teal)** | `--color-primary` | `#0F766E` | Màu thương hiệu chính, header storefront, nút CTA "Chọn mua" | `5.44:1` với `#FFFFFF` |
| **Brand Hover** | `--color-primary-hover` | `#0D9488` | Trạng thái hover phụ | `3.9:1` / `#115E59` đạt `7.4:1` |
| **Brand Deep** | `--color-primary-active` | `#115E59` | Trạng thái nhấn giữ (active), giá bán nổi bật, tiêu đề khối | `7.43:1` với `#FFFFFF` |
| **Brand Soft (POS Mint)** | `--color-primary-soft` | `#CCFBF1` | Nền vùng nhấn nhẹ, pill đơn vị tính, badge giỏ hàng | `8.85:1` với chữ `#134E4A` |
| **Brand Soft Text** | `--color-primary-soft-fg` | `#134E4A` | Chữ trên nền Mint `#CCFBF1` hoặc `#F0FDFA`, nền Topbar | `9.40:1` với `#F0FDFA` |
| **Surface Ground** | `--color-bg-page` | `#F4F7F9` | Nền tổng thể của trang (Storefront & Admin) | — |
| **Surface Card** | `--color-bg-surface` | `#FFFFFF` | Nền thẻ sản phẩm, khối danh mục, bảng dữ liệu, modal | — |
| **Surface Read-only** | `--color-bg-readonly` | `#F1F5F9` | Nền ô chỉ đọc / tính toán tự động (kế thừa chuẩn WinForms) | — |
| **Text Primary** | `--color-text-primary` | `#0F172A` | Tiêu đề, tên thuốc, nội dung chính | `17.8:1` trên `#FFFFFF` |
| **Text Secondary** | `--color-text-secondary` | `#334155` | Hoạt chất, mô tả phụ, nhãn form, thông tin dòng bảng | `10.3:1` trên `#FFFFFF` |
| **Text Muted** | `--color-text-muted` | `#475569` | Ghi chú phụ, placeholder, breadcrumb phụ | `7.05:1` trên `#FFFFFF` |
| **Border Default** | `--color-border` | `#CBD5E1` | Viền ô nhập liệu, viền phân cách bảng | — |
| **Border Subtle** | `--color-border-subtle` | `#E2E8F0` | Viền thẻ sản phẩm, đường kẻ ngang trong bảng | — |
| **Success (Còn hàng / Đã thu)** | `--color-success` / `--color-success-bg` | `#166534` / `#DCFCE7` | Trạng thái Còn hàng, Đang bán, Được xuất bán, Đã thanh toán | `7.1:1` (`#166534` trên `#DCFCE7`) |
| **Warning (Cảnh báo / Rx)** | `--color-warning` / `--color-warning-bg` | `#9A3412` / `#FEF3C7` | Nhãn Thuốc kê đơn (Rx), Sắp hết hạn, Chờ kiểm tra đơn | `6.8:1` (`#9A3412` trên `#FEF3C7`) |
| **Danger (Lỗi / Khóa / Hủy)** | `--color-danger` / `--color-danger-bg` | `#BE123C` / `#FFE4E6` | Hết hạn, Tạm khóa lô, Ngừng bán, Đã hủy, Xóa/Khóa dữ liệu | `6.1:1` (`#BE123C` trên `#FFE4E6`) |
| **Info (OTC / Hướng dẫn)** | `--color-info` / `--color-info-bg` | `#0369A1` / `#E0F2FE` | Nhãn **Mua trực tiếp (OTC)**, thông tin hướng dẫn tư vấn | `5.6:1` (`#0369A1` trên `#E0F2FE`) |

---

## 3. Typography & Phân cấp Chữ

- **Font chính (`--font-sans`):** `'Be Vietnam Pro', system-ui, -apple-system, 'Segoe UI', Roboto, sans-serif` — Thiết kế chuyên biệt cho tiếng Việt, dấu thanh rõ ràng, dễ đọc ở kích thước nhỏ trên bảng dữ liệu và thẻ thuốc.
- **Font dữ liệu mã (`--font-mono`):** `'JetBrains Mono', 'Cascadia Mono', Consolas, monospace` — Chỉ dùng cho mã đơn hàng (`HD00929`), số lô (`LOT-PND-2024A`), mã vạch (`8935001800112`), mã voucher (`GIAM10K`).
- **Quy tắc số liệu:** Mọi cột giá tiền, số lượng tồn, thành tiền đều áp dụng `font-variant-numeric: tabular-nums` để thẳng hàng dọc tuyệt đối.

| Cấp độ | Token CSS | Kích thước | Độ đậm (Weight) | Line Height | Ứng dụng |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Page Title (H1)** | `--text-2xl` | `1.5rem` (24px) | `700` | `1.3` | Tiêu đề trang, tên thuốc ở trang chi tiết |
| **Section Title (H2)** | `--text-xl` | `1.25rem` (20px) | `700` | `1.35` | Tiêu đề khu vực, tiêu đề modal, khối dashboard |
| **Card / Block Title (H3)** | `--text-lg` | `1.0625rem` (17px) | `600` | `1.4` | Giá bán nổi bật trên thẻ, tiêu đề bảng |
| **Body Default** | `--text-base` | `1rem` (16px) | `400` / `500` | `1.5` | Nội dung đọc, ô nhập liệu trên mobile (chống auto-zoom iOS), tên sản phẩm |
| **UI Compact / Table** | `--text-sm` | `0.875rem` (14px) | `400` / `500` | `1.45` | Bảng quản trị, bộ lọc, hoạt chất, nhãn form |
| **Badge / Caption** | `--text-xs` | `0.75rem` (12px) | `600` | `1.35` | Nhãn trạng thái, đơn vị tính phụ, chú thích nhỏ |

---

## 4. Spacing, Bo góc (Radius) & Độ nổi (Elevation)

- **Hệ lưới khoảng cách (8px Spatial System):**
  - `--space-1`: `4px` (khoảng cách giữa icon và nhãn nhỏ)
  - `--space-2`: `8px` (khoảng cách nội bộ nhóm badge/button)
  - `--space-3`: `12px` (padding ô nhập, khoảng cách dòng form gọn)
  - `--space-4`: `16px` (padding chuẩn của card sản phẩm, khoảng cách lưới mobile)
  - `--space-5`: `20px` (padding khung quản trị, khoảng cách khối)
  - `--space-6`: `24px` (khoảng cách giữa các phần chính trên trang)
  - `--space-8`: `32px` (khoảng cách phân vùng lớn)
- **Bo góc (`Radius`):**
  - `--radius-sm`: `6px` (badge, nút nhỏ trong bảng)
  - `--radius-md`: `8px` (ô nhập liệu, nút bấm chuẩn, dropdown)
  - `--radius-lg`: `12px` (product card, panel quản trị, modal)
- **Độ nổi (`Elevation`):**
  - Ưu tiên phân tách bằng đường viền sắc nét `1px solid var(--color-border-subtle)` kết hợp bóng đổ rất nhẹ `--shadow-sm: 0 1px 3px rgba(15, 23, 42, 0.06)`.
  - Modal / Drawer dùng `--shadow-lg: 0 12px 32px rgba(15, 23, 42, 0.14)` và lớp phủ nền `rgba(15, 23, 42, 0.45)`.

---

## 5. Quy chuẩn Component Dùng chung & Trạng thái (States)

### 5.1. Product Card (Thẻ sản phẩm Storefront)
- **Cấu trúc cố định chiều cao:**
  1. **Khung ảnh tỷ lệ `4:3` (`180px`):** Nền `#F8FAFC`, hiển thị ảnh thuốc hoặc nhãn hộp SVG chuẩn y tế (không bao giờ để ảnh vỡ). Góc trái trên gắn nhãn phân loại:
     - Thuốc mua trực tiếp: Badge xanh lá/teal nhẹ `Mua trực tiếp (OTC)`.
     - Thuốc kê đơn: Badge xanh dương đậm `Thuốc kê đơn (Rx)` kèm icon đơn thuốc.
  2. **Nhóm thuốc & Hoạt chất:** 1 dòng chữ nhỏ (`--text-xs`), cắt dòng gọn (`line-clamp: 1`).
  3. **Tên thuốc:** Tối đa 2 dòng (`line-clamp: 2`, chiều cao cố định `2.8em`) để mọi thẻ luôn thẳng hàng.
  4. **Giá bán & Đơn vị tính:** Hiển thị rõ `45.000 ₫ / Hộp` (không tự quy đổi hộp/vỉ/viên). Với thuốc cần tư vấn vẫn hiển thị giá tham khảo theo đơn vị niêm yết.
  5. **Trạng thái tồn:** Hiển thị `Còn hàng`, `Sắp hết (còn 3)`, `Tạm hết hàng`, hoặc `Ngừng kinh doanh`.
  6. **Nút hành động (CTA) đáy thẻ:**
     - Nếu `IsPrescription === false` và `AvailableStock > 0`: Nút chính **`Thêm vào giỏ`** (`#0F766E`).
     - Nếu `IsPrescription === true`: Nút chính **`Yêu cầu tư vấn / Gửi đơn`** (viền `#0369A1`, nền `#E0F2FE`, chữ `#0369A1` đậm) — tuyệt đối không hiện nút Mua ngay giống thuốc thường.
     - Nếu `AvailableStock === 0` hoặc `!IsSelling`: Nút disabled **`Tạm hết hàng`** / **`Ngừng kinh doanh`**.

### 5.2. Bảng dữ liệu Quản trị (Admin Data Table)
- Header nền `#F1F5F9`, chữ `#334155` đậm, cố định khi cuộn ngang.
- Các cột mã dùng font monospace; cột tiền tệ và số lượng tồn căn phải (`text-end`).
- Cột trạng thái luôn kết hợp **Biểu tượng SVG + Chữ mô tả** (không chỉ dùng chấm màu).
- Trên màn hình nhỏ (`< 768px`), bảng có khung cuộn ngang độc lập kèm chỉ dẫn và ghim cột tên/thao tác quan trọng.

### 5.3. Form Nhập liệu, Validation & Rich Text Editor
- Mọi `<input>`, `<select>`, `<textarea>` đều có `<label for="...">` rõ ràng, chiều cao tối thiểu `42px` (`44px` trên mobile).
- Ô chỉ đọc (`readonly` / `disabled` như Tổng vốn phiếu nhập, Điểm tích lũy, Tồn thực tế) có nền `--color-bg-readonly: #F1F5F9`.
- **Inline Validation:** Khi lỗi, viền đổi sang `#BE123C`, hiển thị thông báo lỗi cụ thể ngay dưới trường (`aria-invalid="true"`, `aria-describedby`), và tập trung focus vào trường lỗi đầu tiên.
- **Focus Ring:** Mọi phần tử tương tác khi dùng phím `Tab` đều có vòng focus rõ nét: `outline: 2px solid #0F766E; outline-offset: 2px`.

### 5.4. Hệ thống Phản hồi (Feedback Pattern)
- **Toast Notification (Góc phải dưới / trên mobile):** Cho thao tác thành công ngắn (`Đã thêm Panadol Extra vào giỏ`, `Đã lưu thay đổi sản phẩm`). Tự đóng sau 3.5 giây, có nút đóng thủ công.
- **Inline Alert Banner:** Cho cảnh báo nghiệp vụ ngay trong ngữ cảnh (ví dụ: cảnh báo tồn kho hoặc giá thay đổi tại bước Checkout, cảnh báo xung đột phiên bản đơn hàng `OrderVersion`).
- **Confirmation Modal (Hộp thoại xác nhận):** Bắt buộc cho các thao tác quan trọng hoặc nguy hiểm: Xóa thuốc khỏi giỏ, Hủy đơn hàng, Tạm khóa/Mở khóa lô hàng, Ẩn/Ngừng bán sản phẩm, Xóa Nhóm/NCC, Khóa tài khoản khách hàng. Giữ focus bên trong modal và trả focus về nút vừa bấm khi đóng.
