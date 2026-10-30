# Dọn thư mục bản chính — 30/10/2026

Đã dọn 2,867.0 tệp từ artifacts, graphify-out, cache .vs, obj, bin của các project phụ và thiết lập IDE cá nhân. Bản chạy Store.POS/bin được giữ nguyên đầy đủ.

Bản lưu trước dọn: `E:\PharmacyStoreSolution\PharmacyStore-cleanup-20261030_165002_58cabf.zip`. ZIP đã được kiểm tra CRC toàn bộ. Có thể giải nén các mục cần khôi phục vào thư mục dự án; không cần giải nén toàn bộ để chạy phần mềm.

Đã đối chiếu SHA-256 của 321 tệp mã nguồn/tài nguyên/cấu hình dự án và tệp chạy POS: không thay đổi, không thiếu tệp. Không thao tác database, không thay đổi bản lưu previous hoặc các file sao lưu SQL Server.

Các thư mục bin/obj/.vs có thể xuất hiện lại khi mở Visual Studio hoặc build. Chúng là đầu ra được tạo lại, không phải mã nguồn cần nộp. Khi chạy kiểm tra, artifacts cũng có thể được tạo lại để chứa kết quả mới. Tài liệu cũ tham chiếu artifacts được giữ để đối chiếu; các kết quả cũ nằm trong ZIP trên.

Dung lượng thư mục chính sau dọn khoảng 106.7 MB, gồm bản chạy POS. Lần build sau cần cho phép restore các phụ thuộc (lệnh trong README đã hỗ trợ restore mặc định).
