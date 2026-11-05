# KẾ HOẠCH ĐIỀU PHỐI VÀ THIẾT LẬP NHÓM AGENT (PHARMACY WEB)

## 1. Thành phần Nhóm Agent
- **Coordinator (Agent Điều phối / Kiến trúc & Tích hợp)**: Quản lý solution, Program.cs, project file, migration, cấu hình chung, chốt hợp đồng dữ liệu, nghiệm thu và điều phối.
- **`frontend_mvc_agent` (Agent UI/UX & Frontend MVC)**: Phụ trách giao diện Storefront & Admin, Razor Layouts, Partial Views, CSS/JS, Bootstrap, Responsive, Validation, trạng thái UI.
- **`backend_agent` (Agent Backend / Dữ liệu & Nghiệp vụ)**: Phụ trách DbContext, Entities, Services, MVC/API Controllers, Identity/Auth, xử lý tồn kho theo lô, tính giá/voucher, transaction server-side.
- **`qa_security_agent` (Agent QA / Bảo mật & Tài liệu)**: Phụ trách đối chiếu 11 yêu cầu môn học, kiểm thử bảo mật (CSRF, XSS, Authorize), kiểm thử luồng nghiệp vụ, lập tài liệu hướng dẫn và kịch bản demo.

---

## 2. Ma trận Phân quyền & Sở hữu Tệp tin

| Tệp / Thư mục | Người sở hữu duy nhất | Quyền của Agent khác |
| :--- | :--- | :--- |
| `Program.cs`, `Store.Web.csproj`, `appsettings*.json` | **Coordinator** | Chỉ đọc |
| `Data/`, `Entities/`, `Migrations/` | **Coordinator** (duyệt/tích hợp) + **Backend** (đề xuất) | Chỉ đọc |
| `Services/`, `Controllers/`, `Areas/Admin/Controllers/` | **Backend** | Coordinator review; Frontend chỉ đọc hợp đồng |
| `Views/`, `Areas/Admin/Views/`, `wwwroot/` | **Frontend MVC** | Backend chỉ đọc; QA kiểm thử giao diện |
| `Models/ViewModels/`, `Models/DTOs/` | **Coordinator** (chốt hợp đồng) | Frontend & Backend tuân thủ triển khai |
| `Tests/`, `Docs/QA/`, `Demo/` | **QA & Security** | Chỉ đọc từ các bên khác |

---

## 3. Trạng thái Kỹ năng (Skills)
- `ui-ux-pro-max`: **Có sẵn** (`C:\Users\Dell G15\.gemini\config\skills\ui-ux-pro-max\SKILL.md`)
- `frontend-design`: **Có sẵn** (`C:\Users\Dell G15\.gemini\config\skills\frontend-design\SKILL.md`)
- `impeccable`: **Có sẵn** (`C:\Users\Dell G15\.gemini\skills\impeccable\SKILL.md`)
- `design-system`: **Thiếu** $\rightarrow$ Kế thừa trực tiếp Token và Quy chuẩn Component từ `PharmacyWebPrototype/DESIGN.md`.

---

## 4. Quy trình Tiếp nhận Prompt 2 & Triển khai
1. **Tiếp nhận & Bóc tách**: Coordinator phân tích Prompt 2, ban hành Hợp đồng DTO/ViewModel và lộ trình bàn giao.
2. **Khởi tạo Nền tảng**: Thiết lập khung dự án ASP.NET MVC, Database `PharmacyWebDB`, cấu hình Identity.
3. **Triển khai Song song**: Backend dựng nghiệp vụ & API/Service; Frontend dựng Razor Views & Client logic theo Prototype.
4. **Tích hợp & Rà soát**: Coordinator ghép nối dữ liệu thật; QA chạy bộ test bảo mật, đối chiếu 11 tiêu chí và kịch bản nghiệm thu.
