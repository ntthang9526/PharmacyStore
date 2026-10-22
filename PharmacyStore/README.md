# PharmacyStore — bản gộp thử

Mở `PharmacyStore.slnx`, chọn **Store.POS** làm project khởi động.
Database thử: `PharmacyStoreTrialDB` trên `.\SQLEXPRESS`.
Tài khoản mẫu: `admin` / `123456`.

## Các mục trong dự án

| Thư mục | Nội dung |
|---|---|
| `Store.POS/Forms/` | Các màn hình, chia theo chức năng |
| `Store.POS/Common/` | Định dạng giao diện chung và phiên đăng nhập |
| `Store.BLL/` | Xử lý nghiệp vụ, chia theo chức năng |
| `Store.DAL/` | Truy vấn dữ liệu, chia theo chức năng |
| `Store.DAL/Database/` | Script khởi tạo database thử |
| `Store.Shared/` | Models và DTO dùng chung |
| `Store.API/` | API và Controllers |
| `Store.SmokeTests/Checks/` | Các nhóm kiểm tra chức năng |
| `Docs/` | Ghi chú gộp và thay đổi |
| `artifacts/logs/` | Kết quả build/kiểm tra |
| `artifacts/previews/` | Ảnh giao diện xem thử |
| `Backups/` | Bản sao lưu, checksum và bản đồ di chuyển file |

`Forms/` gồm: **DangNhap, TrangChu, BanHang, Thuoc, DanhMuc, NhapKho, Kho, Voucher, HoaDon, CanhBao, BaoCao**.

Trong BLL/DAL, mục `Kho/` chứa nhập kho, tồn theo lô và danh sách lô; `DanhMuc/` chứa nhóm thuốc, nhà cung cấp và khách hàng. Bố cục tĩnh nằm trong file `.Designer.cs`, tài nguyên `.resx` ở cùng thư mục. Chuột phải file `frm…cs` → **View Designer** để chỉnh vị trí, màu sắc và cột bảng. Các file `.Layout.cs` tạo lại giao diện lúc chạy đã được loại bỏ. Hai form thêm nhanh nhóm/NCC có Design riêng (`frmThemNhomThuoc` và `frmThemDanhMuc`).

Các project vẫn thuộc các nhóm Clients, Server, Shared trong Solution; thêm Tests và Documentation để truy cập kiểm tra/tài liệu. Các thư mục `bin/`, `obj/`, `.vs/` là đầu ra/cấu hình của công cụ.

## Kiểm tra

Chạy từ thư mục chứa solution:

```powershell
dotnet build PharmacyStore.slnx --no-restore -m:1 -p:UseSharedCompilation=false
dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --expansion
dotnet run --project Store.SmokeTests/Store.SmokeTests.csproj -- --render-only
```

Kiểm tra dùng database thử riêng, dọn các bản ghi do chính bài kiểm tra tạo ra. Ảnh được lưu vào `artifacts/previews/`.

Xem [ghi chú gộp](Docs/MERGE-NOTES.md) để biết nội dung, cách tạo database và các giới hạn. Bản nguồn `PharmacyStore` được giữ nguyên.
