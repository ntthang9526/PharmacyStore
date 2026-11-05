# PHARMACY STORE — WEBSITE BÁN HÀNG & QUẢN TRỊ NHÀ THUỐC
## DỰ ÁN BÀI TẬP LỚN ASP.NET MVC CHẠY THẬT (FULL-STACK APPLICATION)

Hệ thống Website Bán hàng và Quản trị Nhà thuốc **Pharmacy** được phát triển trên nền tảng **ASP.NET Core MVC (.NET 8 LTS)**, kế thừa trọn vẹn nghiệp vụ dược phẩm từ phần mềm quản lý nhà thuốc WinForms (`Store.POS`, `Store.BLL`, `Store.DAL`) với cơ sở dữ liệu riêng biệt và cơ chế bảo mật server-side hoàn chỉnh.

---

## 1. Công nghệ & Giả định Kiến trúc

- **Framework Web:** **ASP.NET Core MVC (.NET 8.0)** — Target framework LTS ổn định, đồng bộ với kiến trúc `.NET 8` của `Store.API` và `Store.POS` trong solution `PharmacyStore.slnx`.
  > *Lưu ý đồ án:* Đề bài môn học ghi "ASP.NET MVC" mà không chỉ định MVC 5 hay Core MVC. Nhóm lựa chọn ASP.NET Core MVC (.NET 8) kết hợp Razor Views truyền thống (Controllers, Views, Areas/Admin, Partial Views, ViewModels) để tận dụng các chuẩn bảo mật hiện đại. Nhóm sẽ xác nhận giả định này với Giảng viên bộ môn khi báo cáo.
- **Cơ sở dữ liệu (ORM):** **Entity Framework Core 8.0**. Hỗ trợ linh hoạt cả **SQL Server (`PharmacyWebDB`)** lẫn **SQLite (`PharmacyWeb.db`)** thông qua cấu hình `DatabaseProvider` trong `appsettings.json`.
- **Xác thực & Phân quyền:** **ASP.NET Core Identity** với 2 vai trò sản phẩm: `Customer` và `Admin`.
- **Front-end:** HTML5, **Bootstrap 5.3**, CSS Custom Variables (Design Tokens chuẩn POS Teal `#0F766E` và Mint `#CCFBF1`), JavaScript Fetch AJAX.
- **Kiểm thử tự động:** **xUnit** với 15 bài kiểm thử Unit & Integration Tests bao phủ FEFO, Concurrency, Idempotency, Voucher và XSS Sanitizer.

---

## 2. Cấu trúc Thư mục trong `Store.Web`

```text
E:\PharmacyStoreSolution\PharmacyStore\Store.Web/
├── Store.Web.csproj                 // Tệp dự án web ASP.NET Core MVC (.NET 8)
├── Areas/Admin/                     // Phân hệ Quản trị (Dashboard, Products, Orders,...)
├── Controllers/                     // Controller Storefront (Home, Products, Cart,...)
├── Data/
│   ├── Entities/                    // 15 Thực thể CSDL (Medicine, Batch, Order,...)
│   ├── Seed/DbInitializer.cs        // Dữ liệu mẫu ban đầu (20 thuốc, 24 lô, 6 voucher)
│   └── PharmacyWebDbContext.cs      // DbContext chính
├── Models/ViewModels/               // ViewModels có Data Annotations
├── Services/                        // Nghiệp vụ dùng chung giữa MVC và REST API
├── Views/                           // Giao diện Razor Storefront
├── wwwroot/                         // CSS Tokens, JS tương tác, upload
├── App_Data/Prescriptions/          // Lưu trữ bảo mật ảnh đơn thuốc riêng tư
├── tests/PharmacyWeb.Tests/         // Project kiểm thử tự động xUnit (15 tests)
├── docs/                            // Bộ tài liệu đồ án đầy đủ
│   ├── REQUIREMENTS-MAPPING.md      // Bảng ánh xạ 11 yêu cầu môn học và bằng chứng
│   ├── DESIGN.md                    // Hệ thống Design Tokens, màu sắc và UX
│   ├── ARCHITECTURE.md              // Kiến trúc CSDL, FEFO và vòng đời đơn hàng
│   ├── TEST-REPORT.md               // Báo cáo kết quả kiểm thử tự động và thủ công
│   └── DEMO-SCRIPT.md               // Kịch bản demo báo cáo chi tiết cho giảng viên
├── appsettings.json                 // Cấu hình kết nối DB (không chứa secret)
└── Program.cs                       // Cấu hình dịch vụ, pipeline và Identity
```

---

## 3. Hướng dẫn Mở, Chạy & Kiểm thử Ứng dụng

### 3.1. Yêu cầu Môi trường
- Đã cài đặt **.NET SDK 8.0** trở lên (hệ thống hiện có .NET SDK 8.0 / 10.0).

### 3.2. Chạy Kiểm thử Tự động (Tests)
Mở PowerShell tại thư mục `E:\PharmacyStoreSolution\PharmacyStore\Store.Web` và thực thi:
```powershell
dotnet test "E:\PharmacyStoreSolution\PharmacyStore\Store.Web\tests\PharmacyWeb.Tests\PharmacyWeb.Tests.csproj"
```
*Kết quả:* **15/15 tests Passed** (Bao phủ xuất kho FEFO, Idempotency, hủy đơn hoàn tồn và bảo mật XSS).

### 3.3. Chạy Ứng dụng Web
Mở PowerShell tại thư mục `E:\PharmacyStoreSolution\PharmacyStore\Store.Web` và chạy:
```powershell
dotnet run --project Store.Web.csproj
```
Sau đó mở trình duyệt tại:
- **Khu vực Mua sắm (Storefront):** `http://localhost:5000` (hoặc cổng hiển thị trên console).
- **Khu vực Quản trị (Admin):** `http://localhost:5000/Admin/Dashboard`.

---

## 4. Tài khoản Mẫu Đăng Nhập Kiểm Thử

Hệ thống đã tự động nạp sẵn dữ liệu mẫu khi khởi động lần đầu:

| Vai trò | Tên tài khoản (SĐT / Username) | Mật khẩu | Chức năng kiểm thử chính |
| :--- | :--- | :--- | :--- |
| **Quản trị viên (Admin)** | `admin` *(hoặc `admin@pharmacy.local`)* | `Admin@123456` | Toàn quyền 9 phân hệ Admin: Dashboard KPI, duyệt đơn hàng & thu tiền COD, quản lý kho lô hàng FEFO, thêm thuốc rich text, duyệt ảnh đơn thuốc, báo cáo doanh thu. |
| **Khách hàng (Customer)** | `0912345678` | `Customer@123456` | Hồ sơ **Trần Văn Tuấn**: Giỏ hàng, áp mã giảm giá `GIAM10K`, đặt hàng COD, theo dõi đơn, hủy đơn mới, gửi ảnh đơn thuốc tư vấn. |

---

## 5. Cấu hình Cơ sở Dữ liệu (Database Configuration)

Trong tệp `appsettings.json`:
- **Chế độ mặc định (Khuyên dùng khi demo nhanh):**
  ```json
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "SqliteConnection": "Data Source=PharmacyWeb.db"
  }
  ```
  *Ứng dụng tự động tạo file `PharmacyWeb.db` và nạp toàn bộ 20 sản phẩm, 24 lô, danh mục, voucher và đơn hàng mẫu ngay khi chạy lần đầu.*
- **Chế độ SQL Server Local:**
  Đổi `"DatabaseProvider": "SqlServer"` và cấu hình chuỗi kết nối `"DefaultConnection": "Server=localhost;Database=PharmacyWebDB;Trusted_Connection=True;TrustServerCertificate=True"`.

---

## 6. Danh mục Tài liệu Bàn giao

- [`docs/REQUIREMENTS-MAPPING.md`](docs/REQUIREMENTS-MAPPING.md) — Ánh xạ chi tiết 11 yêu cầu môn học với mã nguồn thực tế.
- [`docs/DESIGN.md`](docs/DESIGN.md) — Hệ thống Design Tokens CSS, Typography, Bố cục chuẩn nhà thuốc.
- [`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md) — Sơ đồ CSDL, quy tắc xuất kho FEFO và bảo mật Concurrency.
- [`docs/TEST-REPORT.md`](docs/TEST-REPORT.md) — Báo cáo chi tiết 15 ca kiểm thử tự động và ma trận kiểm thử trình duyệt.
- [`docs/DEMO-SCRIPT.md`](docs/DEMO-SCRIPT.md) — Kịch bản trình bày mẫu từng bước dành cho buổi chấm thi.
