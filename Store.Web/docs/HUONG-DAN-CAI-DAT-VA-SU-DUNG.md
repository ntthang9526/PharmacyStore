# Hướng Dẫn Cài Đặt & Sử Dụng Hệ Thống Website Nhà Thuốc Pharmacy

**Hệ thống:** Website Bán Thuốc & Quản Trị Nhà Thuốc Trực Tuyến (Pharmacy Store Web)  
**Công nghệ:** ASP.NET Core MVC 8.0, Entity Framework Core, SQL Server (Dùng chung POS)  
**Ngày cập nhật:** 06/11/2026  
**Trạng thái kiểm thử:** 39/39 Tests Đạt (100% Passed)

---

## MỤC LỤC
1. [Tổng Quan Hệ Thống](#1-tổng-quan-hệ-thống)
2. [Yêu Cầu Môi Trường & Công Cụ](#2-yêu-cầu-môi-trường--công-cụ)
3. [Hướng Dẫn Cài Đặt & Khởi Tạo](#3-hướng-dẫn-cài-đặt--khởi-tạo)
4. [Hướng Dẫn Sử Dụng — Dành Cho Khách Hàng (Storefront)](#4-hướng-dẫn-sử-dụng--dành-cho-khách-hàng-storefront)
5. [Hướng Dẫn Sử Dụng — Dành Cho Quản Trị Viên (Admin Back-Office)](#5-hướng-dẫn-sử-dụng--dành-cho-quản-trị-viên-admin-back-office)
6. [Quy Tắc Quản Lý Kho & Giữ Tồn Chuẩn FEFO](#6-quy-tắc-quản-lý-kho--giữ-tồn-chuẩn-fefo)
7. [Xử Lý Sự Cố & Câu Hỏi Thường Gặp (Troubleshooting)](#7-xử-lý-sự-cố--câu-hỏi-thường-gặp-troubleshooting)

---

## 1. Tổng Quan Hệ Thống

Hệ thống Website Pharmacy được thiết kế tích hợp trực tiếp vào cơ sở dữ liệu SQL Server sẵn có của phần mềm quản lý nhà thuốc tại quầy (**Store.POS**).

### Mô hình phân chia dữ liệu:
- **Dữ liệu dùng chung (Shared Data):**
  - Danh mục thuốc (`Categories`)
  - Nhà cung cấp (`Suppliers`)
  - Thuốc & Sản phẩm y tế (`Medicines`)
  - Kho lô hàng & Hạn dùng (`Batches`)
  - Hồ sơ khách hàng (`Customers`)
- **Dữ liệu riêng của Website (Web Data — Bảng có tiền tố `Web*`):**
  - Tài khoản phân quyền ASP.NET Core Identity (`WebUsers`, `WebRoles`)
  - Sổ địa chỉ khách hàng (`WebCustomerAddresses`)
  - Đơn đặt hàng & Chi tiết (`WebOrders`, `WebOrderItems`)
  - Phân bổ giữ tồn theo lô FEFO (`WebOrderBatchAllocations`)
  - Lịch sử kiểm toán đơn hàng (`WebOrderStatusHistories`)
  - Mã giảm giá trực tuyến (`WebVouchers`)
  - Yêu cầu tư vấn đơn thuốc kê đơn (`WebPrescriptionRequests`, `WebPrescriptionImages`)

```
   ┌────────────────────────────────────────────────────────┐
   │             Database SQL Server: PharmacyStoreDB       │
   ├────────────────────────────┬───────────────────────────┤
   │      Dữ liệu dùng chung    │     Dữ liệu riêng Web     │
   │  - Medicines, Categories   │  - WebOrders              │
   │  - Batches (Lô & Hạn dùng) │  - WebOrderBatchAllocations│
   │  - Customers               │  - WebUsers, WebRoles     │
   │  - Suppliers               │  - WebVouchers            │
   └─────────────▲──────────────┴─────────────▲─────────────┘
                 │                            │
         ┌───────┴────────┐           ┌───────┴────────┐
         │ Store.POS App  │           │ Store.Web App  │
         │ (Bán tại quầy) │           │ (Website online│
         └────────────────┘           └────────────────┘
```

---

## 2. Yêu Cầu Môi Trường & Công Cụ

Để cài đặt và vận hành hệ thống, máy tính cần đáp ứng các yêu cầu sau:

1. **Hệ điều hành:** Windows 10/11 hoặc Windows Server 2019/2022.
2. **.NET SDK:** Phiên bản **.NET 8.0 SDK** (hoặc mới hơn). Kiểm tra bằng lệnh:
   ```powershell
   dotnet --version
   ```
3. **Cơ sở dữ liệu:** Microsoft SQL Server 2019/2022 (bao gồm bản miễn phí **SQL Server Express** `.\SQLEXPRESS`).
4. **Công cụ quản trị DB:** SQL Server Management Studio (SSMS) hoặc Azure Data Studio.
5. **Trình duyệt web:** Google Chrome, Microsoft Edge hoặc Mozilla Firefox bản mới nhất.

---

## 3. Hướng Dẫn Cài Đặt & Khởi Tạo

### Bước 1: Mở thư mục dự án
Mở terminal PowerShell tại thư mục chứa dự án:
```powershell
cd E:\PharmacyStoreSolution\PharmacyStore\Store.Web
```

### Bước 2: Khởi tạo Cơ sở dữ liệu SQL Server
1. Mở **SQL Server Management Studio (SSMS)** và kết nối vào máy chủ SQL Server (ví dụ: `.\SQLEXPRESS`).
2. Đảm bảo database `PharmacyStoreDB` của hệ thống đã sẵn sàng.
3. Mở file script tích hợp:
   `E:\PharmacyStoreSolution\PharmacyStore\Store.Web\Database\IntegratePosDatabase.sql`
4. Thực thi script (nhấn **F5** hoặc nút **Execute**).
   > **Lưu ý an toàn:** Script được thiết kế Idempotent (có kiểm tra `IF NOT EXISTS` và `COL_LENGTH`), tự động bổ sung các bảng `Web*`, các cột hoàn tiền `IsRefunded`, `RefundAmount`, ràng buộc bảo vệ tồn kho `CK_WebBatchReservation` mà **không làm mất dữ liệu thuốc hay hóa đơn cũ của quầy POS**.

### Bước 3: Cấu hình tệp môi trường & kết nối

#### 3.1. Cấu hình kết nối SQL Server chung (`.env`)
Tạo hoặc kiểm tra file `.env` tại thư mục gốc giải pháp (`E:\PharmacyStoreSolution\PharmacyStore\.env`):
```dotenv
DB_SERVER=.\SQLEXPRESS
DB_NAME=PharmacyStoreDB
DB_INTEGRATED_SECURITY=true
DB_ENCRYPT=true
DB_TRUST_SERVER_CERTIFICATE=true
DB_CONNECT_TIMEOUT=30
```
*(Nếu dùng xác thực tài khoản SQL: đặt `DB_INTEGRATED_SECURITY=false`, bổ sung `DB_USER=sa` và `DB_PASSWORD=Mật_khẩu_của_bạn`)*.

#### 3.2. Cấu hình tài khoản Quản trị khởi tạo (`.env.web`)
Tạo hoặc kiểm tra file `E:\PharmacyStoreSolution\PharmacyStore\Store.Web\.env.web` (file này được `.gitignore` bảo vệ, không chia sẻ lên git):
```dotenv
WEB_ADMIN_USERNAME=admin
WEB_ADMIN_PASSWORD=Admin@123456
```

#### 3.3. Cấu hình Chuyển khoản ngân hàng (`appsettings.json`)
Trong file `Store.Web/appsettings.json`, khu vực `PaymentSettings:BankTransfer` cho phép thay đổi tài khoản nhận tiền mà **không cần sửa code giao diện**:
```json
{
  "DatabaseProvider": "SqlServer",
  "PaymentSettings": {
    "BankTransfer": {
      "IsEnabled": true,
      "IsDemoAccount": true,
      "BankName": "Vietcombank (Chi nhánh Cần Thơ)",
      "AccountNumber": "1029384756",
      "AccountHolder": "NHA THUOC PHARMACY STORE",
      "TransferInstruction": "Dược sĩ sẽ kiểm tra biến động số dư và xác nhận ngay cho quý khách.",
      "DemoNotice": "Thông tin tài khoản ngân hàng thử nghiệm phục vụ đồ án/demo. Quý khách vui lòng không thực hiện giao dịch chuyển tiền thật."
    }
  }
}
```

### Bước 4: Chạy kiểm thử tự động (Verification Tests)
Trước khi chạy ứng dụng, chạy toàn bộ bộ test tự động để đảm bảo hệ thống toàn vẹn:
```powershell
dotnet test tests/PharmacyWeb.Tests/PharmacyWeb.Tests.csproj
```
Kết quả cần đạt: **39/39 tests Passed (100%)**.

### Bước 5: Khởi động Website
Chạy lệnh khởi động ứng dụng:
```powershell
dotnet run --project Store.Web.csproj
```
Màn hình console xuất hiện thông báo:
```text
Now listening on: https://localhost:55441
Now listening on: http://localhost:55442
```
Mở trình duyệt và truy cập: **`https://localhost:55441`** hoặc **`http://localhost:55442`**.

---

## 4. Hướng Dẫn Sử Dụng — Dành Cho Khách Hàng (Storefront)

### 4.1. Đăng ký & Đăng nhập tài khoản
1. **Đăng ký tài khoản mới:**
   - Bấm **"Đăng ký"** trên thanh Header.
   - Nhập Họ tên, Số điện thoại (10 chữ số) và Mật khẩu (tối thiểu 6 ký tự).
   - Hệ thống tự động tạo hồ sơ khách hàng vào bảng `Customers` dùng chung.
2. **Khách hàng đã có số điện thoại tại quầy thuốc:**
   - Nhằm bảo mật và chống mạo danh, số điện thoại đã phát sinh giao dịch tại quầy cần liên hệ Dược sĩ/Admin để được kích hoạt tài khoản mua online.

### 4.2. Tìm kiếm, Duyệt danh mục & Lọc thuốc
- **Tìm kiếm nhanh:** Gõ tên thuốc (vd: *Panadol, Paracetamol, Amoxicillin*) hoặc hoạt chất vào ô tìm kiếm trên Header.
- **Lọc theo nhóm & Mức giá:** Truy cập trang **"Sản phẩm"** (`/Products`). Chọn nhóm thuốc ở danh mục bên trái hoặc thanh trượt giá. Lưới sản phẩm cập nhật ngay lập tức qua công nghệ **AJAX** không bị tải lại trang.
- **Phân biệt thuốc:**
  - **Thuốc mua trực tiếp (OTC):** Có nhãn xanh *"Mua trực tiếp"*, cho phép thêm vào giỏ hàng và đặt mua tự do.
  - **Thuốc kê đơn (Rx):** Có nhãn vàng *"Cần đơn bác sĩ"*. Website **chặn hoàn toàn việc đưa thuốc Rx vào giỏ hàng tự do**, thay vào đó hiển thị nút *"Gửi ảnh đơn thuốc để dược sĩ tư vấn"*.

### 4.3. Giỏ hàng & Áp dụng Mã giảm giá (Voucher)
1. Bấm biểu tượng giỏ hàng góc phải trên cùng.
2. Tăng/giảm số lượng sản phẩm. Hệ thống tự động kiểm tra lượng tồn thực tế có thể bán trong kho.
3. **Áp dụng Voucher:** Nhập mã (ví dụ: `GIAM10K`) và bấm **"Áp dụng"**. Hệ thống đối chiếu giá trị đơn hàng tối thiểu và chiết khấu tương ứng.
4. **Phí vận chuyển:**
   - Đơn hàng có tổng tiền từ **100.000 ₫** trở lên: **Miễn phí giao hàng (0 ₫)**.
   - Đơn hàng dưới 100.000 ₫: Phí giao hàng tiêu chuẩn là **15.000 ₫**.

### 4.4. Đặt hàng (Checkout) & Chọn phương thức thanh toán
1. Tại màn hình Giỏ hàng, bấm **"Tiến hành đặt hàng"**.
2. Điền thông tin Người nhận, Số điện thoại và Địa chỉ giao hàng.
3. Chọn 1 trong 2 phương thức thanh toán:
   - **Thanh toán khi nhận hàng (COD):** Trả tiền mặt cho bưu tá khi nhận kiện thuốc.
   - **Chuyển khoản ngân hàng (Bank Transfer):**
     - Đọc thông tin ngân hàng hiển thị (Vietcombank, Số tài khoản, Chủ tài khoản).
     - Lưu ý nhãn cảnh báo demo nếu hệ thống đang ở chế độ thử nghiệm.
4. Bấm **"Xác nhận & Đặt hàng ngay"**.
5. Màn hình thông báo **Đặt hàng thành công** xuất hiện với:
   - **Mã đơn hàng thật** (dạng `HDyymmddxxxx`).
   - Cú pháp chuyển khoản chuẩn xác: `THANH TOAN [Mã_Đơn]` (đối với phương thức chuyển khoản).
   - Danh sách chi tiết thuốc đã đặt.

### 4.5. Gửi yêu cầu tư vấn đơn thuốc (Rx)
1. Khách hàng truy cập menu **"Tư vấn đơn thuốc"** (`/PrescriptionRequest`).
2. Tải lên 1 hoặc nhiều ảnh chụp đơn thuốc của bác sĩ (hỗ trợ JPG, PNG, WEBP).
3. Điền ghi chú (triệu chứng, tiền sử dị ứng nếu có) và bấm **"Gửi đơn thuốc"**.
4. Dược sĩ nhà thuốc sẽ tiếp nhận, kiểm tra toa và liên hệ tư vấn qua số điện thoại.

### 4.6. Quản lý & Hủy đơn hàng cá nhân
1. Vào mục **"Đơn hàng của tôi"** (`/MyOrders`).
2. Xem tiến trình trạng thái: *Chờ xác nhận &rarr; Đã xác nhận &rarr; Đang giao hàng &rarr; Đã hoàn tất*.
3. **Hủy đơn hàng:** Khi đơn còn ở trạng thái `Chờ xác nhận (NEW)`, khách hàng có thể bấm nút **"Hủy đơn"** và ghi rõ lý do. Lượng hàng đã giữ trong kho sẽ được tự động giải phóng ngay lập tức.

---

## 5. Hướng Dẫn Sử Dụng — Dành Cho Quản Trị Viên (Admin Back-Office)

### 5.1. Đăng nhập Khu vực Quản trị
- Truy cập đường dẫn: **`https://localhost:55441/Admin`**.
- Đăng nhập bằng tài khoản Quản trị:
  - **Tên đăng nhập:** `admin` (hoặc cấu hình trong `.env.web`)
  - **Mật khẩu:** `Admin@123456`
- Nếu tài khoản thường cố tình truy cập khu vực Admin, hệ thống tự động chặn và trả về lỗi **`403 — Không Đủ Quyền Truy Cập`**.

### 5.2. Bảng điều khiển (Dashboard)
Hiển thị tổng quan tình hình kinh doanh thời gian thực:
- **Số đơn hàng mới** đang chờ xử lý.
- **Doanh thu thực thu hôm nay** và **tháng này** (đã trừ các khoản hoàn tiền nếu có).
- **Cảnh báo tồn kho:** Danh sách các lô thuốc tồn thấp (&le; 5 đơn vị) và các lô sắp hết hạn (&le; 90 ngày).
- **Đơn thuốc cần duyệt:** Số lượng yêu cầu tư vấn thuốc Rx đang chờ dược sĩ xử lý.

---

### 5.3. Quy trình Xử lý Đơn hàng (Order Workflow)

Toàn bộ quy trình luân chuyển đơn hàng được kiểm soát chặt chẽ qua các nút bấm nghiệp vụ tại trang **Chi tiết đơn hàng** (`/Admin/Orders/Details/{id}`):

```
       [ Đơn Mới (NEW) ]
               │
               ▼  (Xác nhận đơn)
     [ Đã Xác Nhận (CONFIRMED) ]
         │                 │
         │ (Chuyển khoản)  │ (Xuất kho giao hàng)
         │ [Xác nhận thu]  │
         ▼                 ▼
   [ Thanh toán PAID ]  [ Đang Giao Hàng (SHIPPING) ]
         │                 │
         │                 ▼  (Kiểm tra lô còn hạn & không khóa)
         │              [ Giao Thành Công / Thu COD ]
         │                 │
         │                 ▼
         └────────► [ Hoàn Tất (COMPLETED) ]  --> Trừ tồn kho thực vĩnh viễn
                           │
       (Nếu hủy khi đã thu tiền)
                           ▼
                  [ Hoàn Tiền (REFUNDED) ]    --> Trả hàng giữ, ghi vết kiểm toán
```

#### Chi tiết các bước thực hiện:
1. **Bước 1: Xác nhận đơn (`NEW` &rarr; `CONFIRMED`):**
   - Quản trị viên kiểm tra thông tin người nhận, địa chỉ giao hàng và bấm **"Xác nhận đơn"**.
2. **Bước 2: Xử lý tiền chuyển khoản ngân hàng (nếu có):**
   - Khách chuyển khoản theo mã đơn: Quản trị viên đối chiếu biến động số dư và bấm **"Xác nhận đã nhận chuyển khoản"**.
   - Trạng thái thanh toán đổi sang `PAID`. Đơn hàng chuyển sang trạng thái sẵn sàng xuất giao mà **chưa bị trừ kho vĩnh viễn quá sớm**.
3. **Bước 3: Xuất kho giao hàng (`CONFIRMED` &rarr; `SHIPPING`):**
   - Quản trị viên bấm **"Xuất giao hàng"**. Hệ thống kiểm tra các lô phân bổ, nếu có lô bị khóa hoặc hết hạn sẽ cảnh báo dừng lại.
4. **Bước 4: Hoàn tất đơn hàng & Trừ kho vĩnh viễn (`SHIPPING` &rarr; `COMPLETED`):**
   - **Đối với đơn đã chuyển khoản:** Khi bên vận chuyển báo giao thành công, bấm **"Hoàn tất giao hàng"**.
   - **Đối với đơn COD:** Bấm **"Xác nhận thu tiền COD & Hoàn tất"**.
   - **Cơ chế an toàn (BS-02):** Hệ thống tự động kiểm tra lại từng lô phân bổ. Nếu lô thuốc bị phát hiện **Tạm khóa** hoặc **Hết hạn**, hệ thống **dừng hoàn tất ngay lập tức, rollback toàn bộ dữ liệu** và báo lỗi chi tiết số lô cho Admin xử lý.
   - Nếu lô hợp lệ: Trừ tồn kho thực vĩnh viễn (`Stock -= Quantity`), giải phóng lượng giữ (`ReservedQuantity -= Quantity`).

#### Luồng Hủy đơn & Hoàn tiền (BS-01):
- **Đối với đơn chưa thu tiền (`UNPAID` / `PENDING_TRANSFER`):**
  - Quản trị viên bấm nút **"Hủy đơn"**, nhập lý do &rarr; Trạng thái đổi sang `CANCELLED`, hoàn trả lượng hàng giữ trong kho về phần có thể bán.
- **Đối với đơn đã thu tiền (`PAID`):**
  - Hệ thống **chặn tuyệt đối việc hủy âm thầm** qua nút hủy thông thường.
  - Quản trị viên bấm nút **"Hoàn tiền & Hủy đơn"**, hộp thoại mở ra hiển thị số tiền cần hoàn lại cho khách.
  - Nhập lý do hoàn tiền (vd: *Khách chuyển nhầm, hết thuốc thay thế...*) &rarr; Bấm xác nhận.
  - Đơn chuyển sang `CANCELLED`, trạng thái thanh toán là `REFUNDED`, ghi nhận người hoàn, số tiền, ngày giờ chi tiết.
  - Lịch sử thu tiền gốc (`PaidAt`) được bảo toàn nguyên vẹn. Hệ thống có cơ chế ngăn chặn bấm hoàn tiền 2 lần.

---

### 5.4. Quản lý Thuốc & Danh mục (Products)
- **Thêm/Sửa thông tin thuốc:** Quản trị viên cập nhật tên, đơn vị, giá bán niêm yết, hoạt chất, quy cách đóng gói và tải lên ảnh đại diện / ảnh phụ.
- **Trình soạn thảo mô tả an toàn (BS-03):** Tích hợp trình soạn thảo định dạng đậm, nghiêng, danh sách, tiêu đề. Hệ thống áp dụng bộ lọc Whitelist **chặn tuyệt đối mã độc XSS, mã SVG (`<svg/onload=...>`) hay iframe nhúng nguy hiểm**.
- **Ý nghĩa cờ trạng thái dùng chung với POS (BS-04):**
  - Cờ **`IsSelling` (Cho phép bán hàng):** Mở bán thuốc trên Web và tại quầy POS.
  - Cờ **`IsActive` (Kích hoạt hoạt động):** Cờ dùng chung toàn hệ thống. **Lưu ý: Nếu tắt cờ này, thuốc sẽ vừa bị ẩn trên Website, vừa bị vô hiệu hóa không bán được tại phần mềm quầy thuốc POS**. Giao diện có hiển thị chú thích cảnh báo rõ ràng.

---

### 5.5. Quản lý Kho & Lô hàng (Inventory)
- **Tra cứu tồn kho:** Xem số lượng tồn thực tế (`Stock`), số lượng đang giữ cho đơn online (`ReservedQuantity`) và lượng có thể bán (`AvailableStock = Stock - ReservedQuantity`).
- **Khóa lô hàng:** Khi phát hiện một lô thuốc có dấu hiệu nghi ngờ chất lượng hoặc có lệnh thu hồi, bấm **"Tạm khóa lô"**. Lập tức lô này sẽ không được xuất bán cho đơn mới và chặn mọi thao tác hoàn tất xuất kho.
- **Nhập kho thuốc mới:** Bấm **"Nhập kho lô mới"**, chọn Nhà cung cấp, nhập nhiều dòng thuốc cùng lúc với Số lô, Hạn dùng, Số lượng và Đơn giá vốn.

---

### 5.6. Quản lý Khách hàng & Cấp tài khoản Web
- Vào mục **"Khách hàng"** (`/Admin/Customers`).
- **Khách hàng mới tạo online:** Hiển thị tài khoản đã liên kết, có thể khóa/mở khóa tài khoản khi cần.
- **Kích hoạt tài khoản cho khách có sẵn tại quầy POS:**
  - Bấm vào chi tiết khách hàng từ danh sách POS.
  - Xác minh đúng số điện thoại chính chủ &rarr; Bấm **"Kích hoạt tài khoản mua hàng online"** và cấp mật khẩu ban đầu. Khách hàng có thể đăng nhập bằng SĐT của mình để mua thuốc online.

---

### 5.7. Báo cáo Doanh thu & Dòng tiền Thực thu (Reports)
- Truy cập mục **"Báo cáo Doanh thu"** (`/Admin/Reports`).
- Chọn khoảng thời gian **Từ ngày &rarr; Đến ngày** và bấm **"Xem báo cáo"**.
- Bảng tổng kết đối chiếu rõ ràng 3 chỉ số cốt lõi:
  1. **Tổng thực thu trong kỳ:** Tổng số tiền đã xác nhận thu của các đơn hàng.
  2. **Tổng tiền hoàn lại:** Tổng số tiền đã hoàn trả cho các đơn hủy đã thu tiền.
  3. **Doanh thu thực còn lại (Net Revenue):** `Doanh thu thực nhận = Tiền thu - Tiền hoàn`.
- Bấm **"Xuất dữ liệu CSV"** để tải file bảng tính Excel/CSV đối chiếu chi tiết từng mã đơn, ngày thu, ngày hoàn, người thu và lý do hoàn tiền.

---

## 6. Quy Tắc Quản Lý Kho & Giữ Tồn Chuẩn FEFO

Hệ thống áp dụng nghiêm ngặt nguyên lý **FEFO (First Expired, First Out - Hạn gần xuất trước)** để hạn chế tối đa nguy cơ thuốc hết hạn:

1. **Khi khách đặt hàng:**
   - Hệ thống quét các lô thuốc thỏa mãn: `IsBlocked = false` (không bị khóa) và `ExpiryDate >= Ngày hiện tại` (còn hạn dùng).
   - Tự động sắp xếp các lô theo thứ tự: **Hạn dùng tăng dần (`ExpiryDate ASC`) &rarr; ID tăng dần (`Id ASC`)**.
   - Tăng `ReservedQuantity` tương ứng trên các lô được phân bổ.
2. **Sự phối hợp giữa Web và quầy POS:**
   - Tại quầy POS, khi nhân viên gõ tên thuốc để bán, phần mềm POS chỉ cho phép bán số lượng chưa giữ: `Tồn bán được = Stock - ReservedQuantity`.
   - POS không thể xuất nhầm hàng đã được khách online đặt và giữ chỗ.
3. **Khi đơn hàng kết thúc:**
   - Đơn hủy: Giảm `ReservedQuantity`, trả hàng lại kho tự do.
   - Đơn hoàn tất: Giảm cả `Stock` và `ReservedQuantity` vĩnh viễn.

---

## 7. Xử Lý Sự Cố & Câu Hỏi Thường Gặp (Troubleshooting)

### Q1: Gặp lỗi kết nối SQL Server khi chạy `dotnet run`?
- **Nguyên nhân:** Tên máy chủ SQL Server trong file `.env` chưa đúng với cấu hình máy của bạn hoặc dịch vụ SQL Server chưa bật.
- **Cách khắc phục:**
  1. Mở `services.msc`, kiểm tra dịch vụ `SQL Server (SQLEXPRESS)` đang ở trạng thái **Running**.
  2. Mở file `.env` tại thư mục gốc `PharmacyStore\.env`, kiểm tra dòng `DB_SERVER=.\SQLEXPRESS` (hoặc `localhost`, `127.0.0.1`).
  3. Nếu máy dùng SQL Server Authentication, đảm bảo `DB_INTEGRATED_SECURITY=false` và điền đúng `DB_USER`, `DB_PASSWORD`.

### Q2: Tại sao bấm hoàn tất đơn hàng hệ thống báo lỗi *"Lô hàng đang bị khóa hoặc hết hạn"*?
- **Giải thích:** Đây là tính năng bảo vệ an toàn y tế (BS-02). Một trong các lô phân bổ cho đơn hàng này đã bị Admin bấm tạm khóa trong kho hoặc vừa qua ngày hết hạn.
- **Cách xử lý:** Vào mục **"Kho & Lô hàng"** kiểm tra mã số lô được thông báo trong lỗi. Nếu lô đủ tiêu chuẩn xuất hàng trở lại thì mở khóa lô rồi thực hiện hoàn tất đơn; nếu lô hỏng/hết hạn thật thì tiến hành hủy hoàn tiền đơn theo đúng quy định.

### Q3: Muốn đổi số tài khoản ngân hàng nhận tiền chuyển khoản thì làm thế nào?
- **Cách xử lý (BS-05):** Mở file `Store.Web/appsettings.json`, chỉnh sửa các giá trị `BankName`, `AccountNumber`, `AccountHolder` trong phần `PaymentSettings:BankTransfer`. Khởi động lại ứng dụng là thông tin mới sẽ tự động cập nhật lên cả trang đặt hàng và trang kết quả đơn mà không cần biên dịch lại mã nguồn.

### Q4: Làm thế nào để chạy lại toàn bộ test tự động?
Mở PowerShell tại thư mục `Store.Web` và thực thi:
```powershell
dotnet test tests/PharmacyWeb.Tests/PharmacyWeb.Tests.csproj
```
Toàn bộ 39 ca kiểm thử tự động (Unit test, Integration test, FEFO test, Concurrency test, Sanitizer test, Lifecycle test) sẽ được thực thi trên môi trường SQLite bộ nhớ độc lập, không làm ảnh hưởng đến dữ liệu SQL Server đang kinh doanh.
