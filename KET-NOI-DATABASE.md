# Kết nối database cho nhóm

Phần mềm đọc cấu hình từ `.env`. Tài khoản `pharmacy_app` kết nối SQL Server; mỗi người vẫn đăng nhập Pharmacy bằng tài khoản nhân viên của mình.

## Chạy từ mã nguồn

1. Lấy phiên bản mã nguồn mới nhất và cài .NET 8 SDK / Visual Studio có hỗ trợ .NET Desktop.
2. Sao chép `.env.example` thành `.env`, đặt cạnh `PharmacyStore.slnx`. Bật hiển thị đuôi file để tránh lưu thành `.env.txt`.
3. Nhập mật khẩu `pharmacy_app` được trưởng nhóm chia sẻ riêng vào `DB_PASSWORD`. Server hiện tại là `tcp:51.79.162.49,3736`, database `PharmacyStoreDB`.
4. Build lại, chạy dự án `Store.POS`, đăng nhập bằng tài khoản Admin/nhân viên trong Pharmacy.

Không cần tạo database hay cài SQL Server trên từng máy. Các máy đọc và ghi cùng database VPS. Khi đã restore dữ liệu lên VPS, không chạy lại script khởi tạo database.

## Chạy bằng bản phần mềm đã build

Nhận toàn bộ thư mục bản chạy, đặt file `.env` cạnh `Store.POS.exe`, rồi mở phần mềm. Máy cần .NET 8 Desktop Runtime nếu bản phát hành chưa kèm runtime.

Build/publish tự chép `.env` vào thư mục đầu ra. Khi sửa `.env` ở thư mục mã nguồn, build lại; khi sửa `.env` cạnh file exe, đóng rồi mở lại ứng dụng.

## Quy tắc cấu hình

- Nếu có biến môi trường `PHARMACY_CONNECTION_STRING`, giá trị này được ưu tiên hơn `.env`. Xóa biến cũ nếu muốn dùng `.env`.
- Ứng dụng tìm `.env` cạnh file chạy trước, rồi tìm ngược lên thư mục dự án. Cấu hình được đọc một lần mỗi lần mở chương trình.
- Nếu không có cả biến môi trường và `.env`, ứng dụng dùng database local `.\SQLEXPRESS`, tên `PharmacyStoreDB`.
- `.env` có sẵn nhưng thiếu trường bắt buộc thì báo lỗi, không tự chuyển sang database khác.
- Mỗi dòng có dạng `TEN_BIEN=gia_tri`; dòng trống và dòng bắt đầu bằng `#` được bỏ qua. Có thể bọc giá trị bằng nháy đơn hoặc nháy kép. Mật khẩu chứa `#`, `;`, `=` được giữ nguyên; không thêm chú thích cuối dòng.

## Kết nối từ mạng khác

Máy cần truy cập được `51.79.162.49:3736`. Nếu firewall VPS đang giới hạn theo IP, thêm IP public của mạng thành viên vào danh sách cho phép; cổng nội bộ Windows vẫn là TCP 1433. Có thể kiểm tra từ PowerShell:

```powershell
Test-NetConnection 51.79.162.49 -Port 3736
```

## Chia sẻ cấu hình

`.env` chứa mật khẩu dạng văn bản, không phải file được mã hóa. File này đã được Git bỏ qua; chỉ chia sẻ riêng trong nhóm. Đưa `.env.example` và hướng dẫn này lên Git, không đưa `.env` thật lên Git. Không dùng `pharmacy_admin` làm tài khoản chạy phần mềm.
