# Pharmacy Web — ASP.NET Core MVC

Website bán hàng và quản trị trực tuyến cho Nhà thuốc Pharmacy, tích hợp trực tiếp cơ sở dữ liệu SQL Server chung với phần mềm quản lý tại quầy (Store.POS).

> **Tài liệu hướng dẫn đầy đủ:**
> - [Hướng dẫn cài đặt & sử dụng chi tiết](docs/HUONG-DAN-CAI-DAT-VA-SU-DUNG.md)
> - [Kiến trúc tích hợp SQL Server & POS](docs/SQLSERVER-POS-INTEGRATION.md)
> - [Kịch bản Demo báo cáo](docs/DEMO-SCRIPT.md)

## Chạy trên máy hiện tại

```powershell
dotnet run --project Store.Web.csproj
```

Mở https://localhost:55441 hoặc http://localhost:55442. POS và web dùng cấu hình `.env` tại thư mục gốc. Tài khoản Admin web ban đầu xem trong `Store.Web/.env.web`; không chia sẻ file này hoặc đưa lên Git.

## Dữ liệu

Thuốc, nhóm, NCC, khách và tồn/lô lấy trực tiếp từ POS. Các bảng tài khoản và chứng từ web có tiền tố `Web` trong cùng database. Hóa đơn/phiếu nhập/báo cáo POS và web hiện là các luồng riêng, không tự gộp lịch sử hay doanh thu.

Giỏ chưa giữ hàng. Đặt đơn giữ ReservedQuantity, POS chỉ bán phần chưa giữ; hủy trả phần giữ, hoàn tất trừ tồn thực. Cần dùng cùng bản POS đã cập nhật quy tắc này.

Khách đã có hồ sơ POS: Admin mở chi tiết khách, xác minh rồi kích hoạt tài khoản web. Khách mới đăng ký tạo hồ sơ chung. Tài khoản nhân viên POS không được dùng trực tiếp làm tài khoản web.

SQLite và dữ liệu web cũ được giữ nguyên, chưa tự nhập vào SQL. Chỉ dùng SQLite cho môi trường Testing.

## Khởi tạo trên máy khác

Sao lưu database POS, chạy `Database/IntegratePosDatabase.sql`, cấu hình `.env` và tài khoản bootstrap riêng theo hướng dẫn. Web không tự nạp danh mục mẫu vào database SQL Server.

## Kiểm thử

```powershell
dotnet test tests/PharmacyWeb.Tests/PharmacyWeb.Tests.csproj
```

17 ca xUnit đạt trong lượt chuyển đổi; kiểm tra tích hợp SQL Server/POS được chạy trên database phục hồi riêng. Xem hướng dẫn tích hợp và báo cáo kiểm thử để biết phạm vi.
