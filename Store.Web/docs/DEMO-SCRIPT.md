> Cập nhật ngày 07/11/2026: Website mặc định là Tiếng Việt (`vi-VN`), hỗ trợ chuyển đổi đa ngôn ngữ VI/EN toàn diện. Hệ thống kết nối database SQL Server POS `PharmacyStoreDB` tại `.\SQLEXPRESS`, đã nâng cấp và kiểm chứng đủ 5 cột hoàn tiền. Bộ kiểm thử tự động đạt 47/47 tests (100% Passed).

# KỊCH BẢN DEMO BÁO CÁO BÀI TẬP LỚN (DEMO SCRIPT)
## WEBSITE BÁN HÀNG & QUẢN TRỊ NHÀ THUỐC PHARMACY

Tài liệu này được biên soạn để sinh viên trình bày trực tiếp với Giảng viên bộ môn theo một luồng tương tác thực tế, trực quan và liền mạch từ phía **Khách hàng (Storefront)** đến **Quản trị viên (Admin)** và **Kiểm thử tự động (Testing)**.

---

## 1. Chuẩn bị Trước Buổi Demo

1. Mở Terminal tại thư mục `E:\PharmacyStoreSolution\PharmacyStore\Store.Web` và chạy lệnh:
   ```powershell
   dotnet run
   ```
2. Mở trình duyệt tại địa chỉ: `http://localhost:5000` hoặc `https://localhost:55441`.
   > **Điểm lưu ý:** Website tự động mở với ngôn ngữ mặc định là **Tiếng Việt (`vi-VN`)**, không phụ thuộc vào ngôn ngữ cài đặt trên hệ điều hành hay trình duyệt của máy giảng viên.
3. Tài khoản mẫu chuẩn bị sẵn:
   - **Quản trị viên (Admin):** Tài khoản `webadmin` / Mật khẩu cấu hình trong `.env.web` (hoặc `admin` / `Admin@123456`)
   - **Khách hàng (Customer):** Tài khoản `0912345678` / Mật khẩu: `Customer@123456` (Trần Văn Tuấn)

---

## 2. Kịch bản Demo 1 — Khách hàng Mua sắm & Đa ngôn ngữ (Storefront)

### Bước 1: Khám phá Trang chủ & Mặc định Tiếng Việt
- **Thao tác:** Trên trang chủ, chỉ cho giảng viên thấy:
  + Giao diện mặc định 100% Tiếng Việt với nhận diện màu Teal POS (`#0F766E`) và Mint POS (`#CCFBF1`).
  + Header thông tin: Hotline miễn phí `1800 6928`, cam kết chính hãng, thanh tìm kiếm dạng viên thuốc (Pill Search).
  + Banner hành động đôi: Mua trực tiếp hoặc Gửi đơn thuốc tư vấn.
  + Gõ tìm kiếm: `"Panadol"` hoặc `"Vitamin"`.

### Bước 2: Trình diễn Chuyển Đổi Đa Ngôn Ngữ (VI &harr; EN)
- **Thao tác:** Bấm vào nút chuyển đổi ngôn ngữ `<i class="bi bi-globe"></i> VI` trên Header:
  + Trang web lập tức chuyển sang chế độ Tiếng Anh (`en-US`), nút đổi thành `EN`.
  + Toàn bộ nhãn trên Header, thanh điều hướng, các nút hành động, Footer tự động đổi sang Tiếng Anh chuẩn mực.
  + Điều hướng sang trang Catalog (`/Products`), Giỏ hàng (`/Cart`), Checkout (`/Checkout`): Tất cả bộ lọc, sắp xếp, tiêu đề, nút bấm đều ở Tiếng Anh đồng bộ.
  + **Điểm nhấn giải thích:**
    1. Tận dụng tài nguyên `.resx` chuẩn của ASP.NET Core (`SharedResource.resx` và `SharedResource.en-US.resx`).
    2. Tên thuốc, hoạt chất y tế và địa chỉ của người dùng được giữ nguyên bản ngữ chuyên môn, không bị dịch méo mó.
    3. Tiền tệ vẫn giữ nguyên đơn vị Đồng Việt Nam (`₫`), không tự đổi sang USD gây sai lệch thanh toán.
    4. Bấm lại nút `EN` để chuyển về Tiếng Việt mượt mà; tùy chọn được lưu bền vững qua Cookie `pharmacy_lang`.

### Bước 3: Duyệt Catalog & Lọc Bất Đồng Bộ (AJAX)
- **Thao tác:** Bấm vào menu **"Tất cả sản phẩm"** (`/Products`).
  + Chọn nhóm thuốc *"Thuốc giảm đau - hạ sốt"* hoặc nhập khoảng giá.
  + Đổi kiểu sắp xếp sang *"Giá tăng dần"*.
- **Điểm nhấn giải thích:** Lưới sản phẩm cập nhật tức thì qua **Fetch AJAX** (trả về Partial View phía server), không bị chớp giật hay reload toàn bộ trang.

### Bước 4: Phân biệt Thuốc OTC & Thuốc Kê đơn (Rx)
- **Thao tác:** 
  + Chỉ vào sản phẩm **Panadol Extra**: Có nhãn *"Mua trực tiếp"*, nút *"Chọn mua"* cho phép thêm thẳng vào giỏ hàng.
  + Chỉ vào sản phẩm **Augmentin 1g**: Có nhãn *"Cần đơn bác sĩ"*, nút chuyển thành *"Gửi đơn tư vấn"*, backend chặn tuyệt đối việc mua tự do không có đơn.

### Bước 5: Giỏ hàng, Mã Giảm Giá & Phí Giao Hàng
- **Thao tác:** Mở Giỏ hàng (`/Cart`).
  + Thử tăng/giảm số lượng thuốc bằng nút `+` / `-`.
  + Nhập mã voucher `GIAM10K` &rarr; Bấm **"Áp dụng"**: Giảm ngay 10.000 ₫ vào tổng thanh toán.
  + Nhập thử mã không tồn tại để thấy thông báo lỗi rõ ràng.
  + Quy tắc phí vận chuyển: Đơn từ 100.000 ₫ được **Miễn phí giao hàng**, đơn dưới 100.000 ₫ tính phí 15.000 ₫.

### Bước 6: Tiến hành Đặt hàng (Checkout) & Mã Đơn Thật
- **Thao tác:** Bấm **"Tiến hành đặt hàng"** &rarr; Đăng nhập tài khoản khách hàng.
  + Hệ thống tự động điền sẵn tên người nhận và sổ địa chỉ đã lưu.
  + Chọn phương thức **"Thanh toán khi nhận hàng (COD)"** hoặc **"Chuyển khoản ngân hàng"** &rarr; Bấm **"Xác nhận & Đặt hàng ngay"**.
- **Điểm nhấn giải thích:** 
  + Khóa **IdempotencyKey** bảo vệ chống tạo trùng đơn nếu khách bấm lặp lại.
  + Nếu chọn chuyển khoản: Trang kết quả hiển thị thông tin số tài khoản nhà thuốc và cú pháp chuyển tiền mẫu an toàn (có nhãn cảnh báo tài khoản demo).

### Bước 7: Khách hàng Gửi Đơn Thuốc Tư Vấn
- **Thao tác:** Vào mục **"Tư vấn đơn thuốc"** (`/PrescriptionRequest`).
  + Nhập thông tin bệnh nhân, chọn ảnh đơn thuốc và bấm gửi.
  + Đơn được tạo với mã `RX-...` ở trạng thái `Chờ dược sĩ kiểm tra`. Ảnh được lưu trữ bảo mật trong thư mục riêng tư, chỉ tải qua API có xác thực.

---

## 3. Kịch bản Demo 2 — Quản trị viên Xử lý Nghiệp vụ & Hoàn tiền (Admin Back-Office)

### Bước 1: Tổng quan KPI Dashboard & Phân quyền Server
- **Thao tác:** Đăng nhập tài khoản Admin &rarr; Vào **"Khu Vực Quản Trị"** (`/Admin/Dashboard`).
  + Thử dùng tài khoản Customer truy cập trực tiếp URL `/Admin/Dashboard`: Giảng viên sẽ thấy màn hình **`403 — Không Đủ Quyền Truy Cập`** bảo vệ chặt chẽ ở cấp độ Server.
  + Trên Dashboard: Xem số lượng đơn hàng mới, doanh thu thực thu, cảnh báo các lô sắp hết hạn (<90 ngày) và lô tồn thấp.

### Bước 2: Xử lý Đơn hàng Theo Luồng Trạng thái & Xuất kho FEFO
- **Thao tác:** Vào mục **"Quản lý Đơn hàng"** (`/Admin/Orders`), chọn đơn vừa đặt:
  + Xem bảng phân bổ lô FEFO: Hệ thống tự động giữ chỗ từ lô có hạn dùng gần nhất.
  + Bấm nút **"Xác nhận đơn (CONFIRMED)"** &rarr; Bấm **"Xuất giao hàng (SHIPPING)"**.
  + Khi bưu tá báo giao thành công: Bấm **"Xác nhận thu COD & Hoàn tất (COMPLETED)"**.
- **Điểm nhấn giải thích:** Lúc này hệ thống thực hiện trừ tồn kho vĩnh viễn (`Stock -= Quantity`), chuyển trạng thái thanh toán sang `PAID` và ghi nhận nhật ký kiểm toán.

### Bước 3: Nghiệp vụ Chặn Hủy Trực Tiếp & Luồng Hoàn Tiền (BS-01)
- **Thao tác:** Chọn một đơn hàng đã thu tiền (PAID):
  + Nếu bấm hủy trực tiếp: Hệ thống hiển thị cảnh báo chặn lại vì đơn đã thu tiền, yêu cầu thực hiện thủ tục hoàn tiền.
  + Bấm nút **"Hoàn tiền đơn hàng"**: Nhập lý do (ví dụ: *Khách đổi ý, thuốc dị ứng*), xác nhận hoàn tiền.
  + Hệ thống lưu số tiền hoàn, thời điểm hoàn, nhân viên thực hiện vào 5 cột kiểm toán mới (`IsRefunded`, `RefundAmount`, `RefundedAt`, `RefundedByEmployee`, `RefundReason`) trên SQL Server.
  + Vào **"Báo cáo Doanh thu"**: Giảng viên sẽ thấy số liệu được đối chiếu minh bạch: Tổng thực thu, Tổng tiền hoàn và Doanh thu thực còn lại.

### Bước 4: Chặn Xuất Kho Lô Hết Hạn hoặc Đang Khóa (BS-02)
- **Thao tác:** Vào kho lô hàng, khóa một lô hoặc giả lập lô hết hạn.
  + Khi hoàn tất đơn có chứa lô này: Backend lập tức chặn đứng và thông báo lỗi rõ ràng, bảo vệ tuyệt đối an toàn dược phẩm cho người bệnh.

### Bước 5: Soạn thảo Thuốc Mới với Rich Text Editor & Textarea Đồng Bộ
- **Thao tác:** Vào **"Thuốc & Sản phẩm"** &rarr; **"Thêm thuốc mới"**:
  + Soạn thảo mô tả bằng thanh công cụ Rich Text (In đậm, In nghiêng, Danh sách).
  + Bấm qua lại giữa chế độ trực quan và Textarea HTML để giảng viên thấy dữ liệu luôn được đồng bộ 100%.
  + Toàn bộ mã HTML được lọc qua `HtmlSanitizerService` ở server, ngăn chặn triệt để XSS, MathML và SVG injection.

---

## 4. Kịch bản Demo 3 — Bộ Kiểm thử Tự động (Testing Proof)

- **Thao tác:** Mở Terminal tại `Store.Web` và gõ:
  ```powershell
  dotnet test tests/PharmacyWeb.Tests/PharmacyWeb.Tests.csproj --no-restore -p:GitRepositoryConfigurationScope=local
  ```
- **Kết quả trình chiếu:** **47/47 Passed (100%)** với thời gian chạy dưới 3 giây.
- **Giải thích cho giảng viên về sự phát triển của bộ test:**
  1. **15 ca gốc:** Kiểm thử xuất kho FEFO, IdempotencyKey, hủy đơn trả tồn, trừ tồn khi thu COD, lọc XSS.
  2. **17 ca xUnit tích hợp POS:** Xác nhận cấu trúc dùng chung danh mục POS và tách riêng bảng Web.
  3. **22 ca sau rà soát đợt 1:** Bổ sung kiểm tra báo cáo doanh thu cuối ngày, danh mục ẩn, concurrency token.
  4. **39 ca sau sửa lỗi bổ sung (06/11):** Bao quát luồng hoàn tiền BS-01, chặn lô khóa/hết hạn BS-02, làm sạch SVG XSS BS-03, nhãn POS/Web BS-04, cấu hình ngân hàng BS-05.
  5. **43 ca sau hoàn thiện đợt 1 (07/11):** Bổ sung 4 ca kiểm tra đa ngôn ngữ VI/EN, văn hóa mặc định tiếng Việt và kiểm tra ánh xạ 5 cột hoàn tiền trên Entity Model.
  6. **47 ca hoàn thiện kiểm tra bổ sung (07/11):** Bổ sung 4 ca kiểm chứng KT-01 (chặn hoàn tiền đơn đang giao, giữ nguyên tồn kho) và KT-02 (chuẩn hóa RFC 4180 escape nháy kép cho 13 cột xuất CSV).
  7. Kết quả có tệp XML TRX chuẩn lưu vết tại `tests/PharmacyWeb.Tests/TestResults/test_results_20261107.trx`.
