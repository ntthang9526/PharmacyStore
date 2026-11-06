# Giải Thích Cấu Trúc Thư Mục & Tệp Tin Trong Dự Án (Project Structure Guide)

**Hệ thống:** Giải pháp Quản lý & Bán thuốc Pharmacy Store (Website ASP.NET Core MVC & Quầy thuốc POS)  
**Tài liệu:** Hướng dẫn cấu trúc thư mục, tệp tin và quy tắc quản lý mã nguồn Git  
**Ngày cập nhật:** 06/11/2026  

---

## 1. Tổng Quan Cấu Trúc Toàn Bộ Giải Pháp (Solution Overview)

Thư mục gốc `PharmacyStore/` chứa giải pháp đa dự án (.NET Solution `PharmacyStore.slnx`), kết nối mô hình bán hàng đa kênh (Omnichannel) giữa quầy thuốc vật lý và website trực tuyến:

```text
PharmacyStore/
├── Store.Web/               # [Dự án trọng tâm] Website thương mại điện tử & Quản trị (ASP.NET Core MVC 8.0)
├── Store.POS/               # Ứng dụng Desktop bán hàng & quản lý tại quầy thuốc (Windows Forms)
├── Store.DAL/               # Tầng truy xuất dữ liệu dùng chung (Data Access Layer - ADO.NET / SQL Server)
├── Store.BLL/               # Tầng xử lý nghiệp vụ bán hàng tại quầy (Business Logic Layer)
├── Store.Shared/            # Thư viện mô hình dữ liệu (Models/DTOs) dùng chung giữa POS và DAL
├── Store.API/               # Dự án Web API dịch vụ (Backend API)
├── PharmacyStore.slnx       # Tệp định nghĩa giải pháp Visual Studio Solution
├── .env.example             # Tệp mẫu hướng dẫn cấu hình kết nối database
└── .gitignore               # Tệp quy tắc loại trừ các file rác/nhạy cảm cho toàn bộ repository
```

---

## 2. Chi Tiết Cấu Trúc Dự Án Website (`Store.Web`)

Dự án `Store.Web` được tổ chức theo kiến trúc **ASP.NET Core MVC chuẩn kết hợp Service-Oriented Layer**, phân tách rành mạch giữa giao diện khách hàng (Storefront) và khu vực quản trị (Admin Area):

```text
Store.Web/
├── App_Data/
│   └── Prescriptions/       # Nơi lưu trữ ảnh đơn thuốc tư vấn của bệnh nhân (bảo mật riêng tư)
│       └── .gitkeep         # Giữ thư mục trên Git mà không đưa ảnh nhạy cảm lên
├── Areas/
│   └── Admin/               # Phân vùng Quản trị dành cho Dược sĩ / Chủ nhà thuốc
│       ├── Controllers/     # Các Controller tiếp nhận yêu cầu quản trị
│       └── Views/           # Giao diện quản trị Razor Views theo từng chức năng
├── Controllers/             # Các Controller phục vụ khách hàng mua sắm trực tuyến
├── Data/
│   ├── Entities/            # Các lớp Entity ánh xạ bảng cơ sở dữ liệu (Code-First)
│   ├── Seed/                # Dữ liệu khởi tạo tài khoản/vai trò ban đầu
│   ├── PharmacyWebDbContext.cs # DbContext chính cấu hình mapping bảng POS & bảng Web
│   └── SharedPosDatabase.cs # Công cụ kiểm tra tính đồng bộ schema với database POS
├── Database/
│   └── IntegratePosDatabase.sql # Script SQL mở rộng database POS cho hệ thống Web
├── docs/                    # Toàn bộ tài liệu hướng dẫn kỹ thuật, kiến trúc và báo cáo
│   ├── reports/             # Báo cáo rà soát lỗi và kết quả khắc phục
│   ├── ARCHITECTURE.md      # Tài liệu kiến trúc kỹ thuật hệ thống
│   ├── DEMO-SCRIPT.md       # Kịch bản demo báo cáo bài tập lớn
│   ├── DESIGN.md            # Hướng dẫn thiết kế giao diện và Design Tokens
│   ├── GIAI-THICH-THU-MUC-VA-FILE.md # [Tệp này] Giải thích chi tiết thư mục và tệp tin
│   ├── HUONG-DAN-CAI-DAT-VA-SU-DUNG.md # Cẩm nang cài đặt và sử dụng phần mềm
│   ├── REQUIREMENTS-MAPPING.md # Bảng đối chiếu 11 tiêu chí môn học
│   ├── SQLSERVER-POS-INTEGRATION.md # Hướng dẫn kỹ thuật chia sẻ SQL Server với POS
│   └── TEST-REPORT.md       # Báo cáo kết quả kiểm thử tự động
├── Models/
│   ├── BankTransferOptions.cs # Lớp cấu hình tài khoản chuyển khoản ngân hàng
│   ├── ErrorViewModel.cs    # Model hiển thị trang báo lỗi hệ thống
│   └── ViewModels/          # Các ViewModel vận chuyển dữ liệu giữa Controller và View
├── Properties/
│   └── launchSettings.json  # Cấu hình cổng mạng và môi trường chạy local (localhost)
├── Services/                # Tầng nghiệp vụ cốt lõi (Business Service Layer)
├── tests/
│   └── PharmacyWeb.Tests/   # Dự án kiểm thử tự động với xUnit (39 test cases đạt 100%)
├── Views/                   # Giao diện khách hàng (Storefront) phân theo Controller
│   └── Shared/              # Các thành phần giao diện dùng chung (Layout, Header, Footer)
├── wwwroot/                 # Thư mục tài nguyên tĩnh công khai (CSS, JS, Fonts, Icons)
├── appsettings.json         # Tệp cấu hình ứng dụng chính
├── Program.cs               # Tệp khởi tạo DI container, middleware và ứng dụng
├── README.md                # Tệp giới thiệu tổng quan dự án
└── Store.Web.csproj         # Tệp dự án .NET và định nghĩa gói thư viện NuGet
```

---

## 3. Chức Năng Của Từng Thư Mục & Tệp Tin Trọng Tâm

### 3.1. Tầng Nghiệp Vụ Cốt Lõi (`Store.Web/Services/`)
Đây là trái tim xử lý logic của hệ thống, tuân thủ nguyên tắc Clean Code và dễ bảo trì:
- **`OrderService.cs`:** Điều phối toàn bộ vòng đời đơn hàng. Quản lý giữ tồn theo nguyên tắc FEFO, kiểm tra an toàn lô thuốc khi hoàn tất (`BS-02`), hủy đơn và quy trình hoàn tiền lưu vết (`BS-01`).
- **`ReportService.cs`:** Thu thập chỉ số Dashboard KPI, tính toán doanh thu thực thu, tiền hoàn lại và doanh thu thuần (Net), xuất dữ liệu đối chiếu ra tệp CSV.
- **`ProductService.cs`:** Quản lý danh mục thuốc, tìm kiếm, bộ lọc AJAX, xử lý upload ảnh an toàn (kiểm tra Magic Bytes) và kiểm soát xung đột dữ liệu đồng thời (`RowVersion`).
- **`CartService.cs`:** Quản lý giỏ hàng trong session, tính toán tồn kho có thể bán thời gian thực và kiểm tra voucher giảm giá.
- **`InventoryService.cs`:** Quản lý các lô hàng trong kho, hỗ trợ khóa/mở khóa lô hàng và lập phiếu nhập kho nhiều dòng.
- **`HtmlSanitizerService.cs`:** Bộ lọc HTML theo danh sách cho phép (Whitelist), loại bỏ triệt để mã độc XSS, SVG injection (`BS-03`) trong phần mô tả thuốc.
- **`PrescriptionService.cs`:** Tiếp nhận ảnh toa thuốc của bệnh nhân, lưu trữ bảo mật và kiểm soát quyền xem ảnh (chống IDOR).
- **`PharmacyClock.cs`:** Chuẩn hóa thời gian theo ngày giờ Việt Nam (UTC+7) phục vụ tính hạn dùng thuốc.
- **`MedicineSvgHelper.cs`:** Tự động sinh đồ họa bao bì thuốc SVG chuẩn nhận diện khi thuốc chưa kịp chụp ảnh thực tế.

---

### 3.2. Tầng Điều Khiển Quản Trị (`Store.Web/Areas/Admin/Controllers/`)
- **`DashboardController.cs`:** Hiển thị trang chủ quản trị với các thẻ KPI, cảnh báo tồn kho và cảnh báo hạn dùng.
- **`OrdersController.cs`:** Duyệt đơn, xuất kho, hoàn tất giao hàng và xử lý yêu cầu hoàn tiền (`POST /Admin/Orders/Refund`).
- **`ProductsController.cs`:** Thêm, sửa, bật/tắt bán và kích hoạt hoạt động thuốc (`IsActive` dùng chung với POS).
- **`InventoryController.cs`:** Quản lý lô hàng, lập phiếu nhập kho và xem lịch sử nhập.
- **`CustomersController.cs`:** Tra cứu khách hàng và kích hoạt tài khoản mua online cho khách hàng thân thiết tại quầy POS.
- **`ReportsController.cs`:** Xem báo cáo doanh thu tài chính và xuất tệp CSV.
- **`PrescriptionsController.cs`:** Dược sĩ tiếp nhận ảnh đơn thuốc, ghi chú tư vấn và duyệt toa.
- **`VouchersController.cs`:** Quản lý mã giảm giá (tỷ lệ %, giảm cố định, hạn mức, số lượng).
- **`CatalogMetaController.cs`:** Quản lý danh mục nhóm thuốc và danh sách nhà cung cấp.

---

### 3.3. Tầng Dữ Liệu & Thực Thể (`Store.Web/Data/`)
- **`PharmacyWebDbContext.cs`:** Kế thừa `IdentityDbContext`, cấu hình quan hệ giữa các bảng. Đặc biệt ánh xạ trực tiếp các bảng `Medicines`, `Categories`, `Suppliers`, `Customers`, `Batches` của POS và các bảng tiền tố `Web*` của website.
- **`Entities/OrderEntities.cs`:** Định nghĩa thực thể đơn hàng (`Order`), chi tiết (`OrderItem`), phân bổ giữ tồn (`OrderBatchAllocation`), lịch sử trạng thái (`OrderStatusHistory`) và voucher (`Voucher`).
- **`Entities/MedicineEntities.cs`:** Định nghĩa thực thể thuốc (`Medicine`), ảnh (`MedicineImage`), lô hàng (`Batch`).
- **`Database/IntegratePosDatabase.sql`:** Tệp script T-SQL dùng để chạy vào SQL Server, bổ sung các bảng web và ràng buộc giữ tồn `CK_WebBatchReservation`.

---

### 3.4. Tầng Kiểm Thử Tự Động (`Store.Web/tests/PharmacyWeb.Tests/`)
Bộ kiểm thử chạy hoàn toàn độc lập trên SQLite bộ nhớ (In-Memory), không tác động vào database POS kinh doanh thật:
- **`SupplementalReportVerificationTests.cs` (17 tests):** Kiểm thử toàn diện 5 nhóm vấn đề BS-01 &rarr; BS-05 và 2 bài test vòng đời hoàn chỉnh từ lúc đặt hàng đến khi xuất giao hoặc hủy hoàn tiền.
- **`OrderAndFefoIntegrationTests.cs` (8 tests):** Kiểm thử giữ tồn FEFO, trừ kho khi thanh toán COD, tính phí ship, voucher.
- **`BugFixVerificationTests.cs` (5 tests):** Kiểm thử bộ lọc HTML an toàn, báo cáo doanh thu cuối ngày, Concurrency token, ẩn/hiện thuốc catalog.
- **`SharedPosModelTests.cs` (1 test):** Kiểm tra cấu trúc bảng mapping chính xác với database POS.
- **`UnitTests.cs` (8 tests):** Kiểm thử logic nghiệp vụ nhỏ: tính tồn khả dụng, quy tắc phí ship, giảm giá.

---

## 4. Danh Mục Tệp & Thư Mục KHÔNG ĐƯỢC PUSH Lên Git

Để bảo mật thông tin y tế, an toàn mật khẩu và giữ kho lưu trữ Git nhẹ, sạch sẽ, các tệp sau được đưa vào `.gitignore` và **tuyệt đối không được commit/push lên Git**:

| Tệp / Thư mục | Lý do loại trừ khỏi Git | Cơ chế xử lý trong dự án |
| :--- | :--- | :--- |
| **`.env`, `.env.vps`** | Chứa thông tin đăng nhập máy chủ cơ sở dữ liệu SQL Server | Được thay bằng `.env.example` làm mẫu |
| **`Store.Web/.env.web`** | Chứa mật khẩu khởi tạo của tài khoản Quản trị viên (`admin`) | Quản trị viên tự cấu hình cục bộ trên máy chạy |
| **`bin/`, `obj/`** | Thư mục sinh tự động khi biên dịch (chứa file DLL, PDB, cache) | Sinh lại tự động khi chạy `dotnet build` |
| **`*.db`, `*.db-shm`, `*.db-wal`** | Tệp cơ sở dữ liệu SQLite chạy tạm trên máy local | Chỉ dùng trong môi trường Testing |
| **`.vs/`, `*.user`, `*.suo`** | Tệp cấu hình giao diện và điểm debug cá nhân của Visual Studio | Mỗi lập trình viên có thiết lập riêng |
| **`.dotnet/`** | Thư mục bộ nhớ đệm tạm thời của .NET CLI SDK | Tự động sinh bởi môi trường chạy lệnh |
| **`App_Data/Prescriptions/*`** | Ảnh chụp đơn thuốc có thông tin cá nhân và bệnh án của khách | Đã có tệp `.gitkeep` giữ cấu trúc thư mục rỗng |
| **`wwwroot/uploads/*`** | Tệp ảnh do người dùng tải lên trong lúc dùng thử nghiệm | Đã có tệp `.gitkeep` giữ thư mục rỗng |

---

## 5. Danh Mục Các Tệp Đã Được Dọn Dẹp

Trong đợt rà soát mã nguồn:
1. **Đã loại bỏ `tests/PharmacyWeb.Tests/UnitTest1.cs`:** Đây là tệp mẫu rỗng sinh tự động bởi lệnh `dotnet new xunit`, không chứa mã kiểm thử thực tế. Đã xóa khỏi git để tránh gây nhiễu kết quả kiểm thử.
2. **Cập nhật `.gitignore`:** Bổ sung cấu hình bỏ qua thư mục `wwwroot/uploads/*` và bảo lưu tệp định danh `.gitkeep` để các lần tải ảnh trong tương lai không bị đưa nhầm vào commit.
