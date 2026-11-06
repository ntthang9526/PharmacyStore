# Kiểm thử sau chuyển sang SQL Server chung POS — 05/11/2026

- Build web và POS thành công; POS 0 lỗi, 0 cảnh báo.
- xUnit: 17 ca đạt, 0 lỗi, 0 bỏ qua. Có một test mẫu rỗng của dự án cũ.
- Script bổ sung bảng/cột đã thử trên database phục hồi riêng, gồm chạy lại để kiểm tra idempotence.
- 28 kiểm tra tích hợp SQL Server đạt: đọc dữ liệu POS, giữ hàng, POS nhìn tồn chưa giữ, chặn xuất phần web giữ, hủy sau POS bán, thu tiền một lần, chặn thu đơn hủy, hai yêu cầu web tranh tồn cuối, kích hoạt tài khoản khách POS, chặn nhập sai nhà cung cấp và phân biệt lô cùng số nhưng khác hạn.
- Số thuốc/khách/hóa đơn/phiếu nhập/tổng tồn/tổng tiền POS không thay đổi khi bổ sung schema chính.
- Website mở được catalog và sắp xếp theo giá bằng SQL Server local; danh sách có 71 thuốc active của POS.
- Database phục hồi dùng để kiểm thử đã được dọn sau khi hoàn tất; database chính và các bản sao lưu vẫn được giữ.

Không chạy test ghi đơn/kho trên database chính. Các test dùng SQLite trong bộ nhớ hoặc database SQL Server phục hồi riêng. Chưa kiểm tra toàn bộ bảo mật, giao diện responsive và luồng chứng từ khác của hệ thống.

Xem [SQLSERVER-POS-INTEGRATION.md](SQLSERVER-POS-INTEGRATION.md) để biết cấu hình và phạm vi dữ liệu dùng chung.
