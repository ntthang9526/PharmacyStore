# Pharmacy — dữ liệu trình diễn bổ sung

Đã bổ sung riêng một bộ gồm 50 thuốc, 51 lô, 15 khách hàng, 10 nhà cung cấp, 10 voucher, 100 hóa đơn và 20 phiếu nhập vào PharmacyStoreTrialDB. Các dữ liệu đã có không bị xóa. Đây là số lượng **thêm mới**, không phải tổng toàn bộ database.

Tên thuốc dùng tên generic tương ứng hoạt chất, tham khảo danh mục Medicines A–Z của NHS: https://www.nhs.uk/medicines/. Đây không phải xác nhận sản phẩm đăng ký lưu hành tại Việt Nam. Đơn vị đóng gói, giá bán, giá vốn, phân loại phục vụ luồng kê đơn và toàn bộ khách hàng/NCC/giao dịch là dữ liệu giả định của bài tập, không dùng để tư vấn hoặc kinh doanh. Không có liều điều trị thực tế trong đơn mẫu.

## Đăng nhập và tìm kiếm

- Admin: dùng tài khoản đang có, không đổi mật khẩu cũ.
- Nhân viên mới: `pharmacy_nv`, mật khẩu `Demo@123456`.
- Khách mới có SĐT từ `0907010001` đến `0907010015`.
- Mã vạch thuốc mới: `9992030000001` đến `9992030000050`.
- Các lô mới bắt đầu bằng `PH26-`; tên thuốc không có tiền tố DEMO.

## Hóa đơn và báo cáo

- 100 hóa đơn mới: **HD00835–HD00934**.
- **HD00835–HD00924**: 90 hóa đơn đã thu, phân bố khoảng 30 ngày, có tiền mặt/chuyển khoản/voucher.
- **HD00925–HD00933**: 9 hóa đơn chờ.
- **HD00934**: đã hủy, có lý do.
- **HD00929**: đơn chờ thường với voucher PH5K.
- **HD00930**: đơn kê đơn còn thiếu thông tin, dùng để minh họa bị chặn và bổ sung đơn.
- **HD00931**: đơn kê đơn đủ thông tin, dùng để thanh toán đơn chờ.
- **HD00932**: yêu cầu 10 đơn vị nhưng chỉ còn 3, dùng để minh họa thiếu hàng.
- **HD00933**: voucher PHHET đã hết lượt, cần sửa trước khi thu.

Chọn khoảng thời gian từ đầu tháng 10/2026 đến 30/10/2026 để xem dữ liệu rải theo ngày. Báo cáo có cả dữ liệu cũ và dữ liệu mới. Khi bấm thanh toán/nhập kho trong lúc demo, phần mềm vẫn ghi thêm chứng từ và thay đổi tồn trong database bài tập.

## Voucher

- Phần trăm: PH10, PH15, PH20.
- Tiền cố định: PH2K, PH5K, PH8K, PH10K, PH25K.
- Hết lượt: PHHET.
- Bị khóa: PHKHOA.

## Tồn kho và trình diễn

Tìm Loratadine/Cetirizine/Omeprazole/Lansoprazole trong bộ mới để xem các tình huống tồn thấp, hết hàng, khóa lô và lô hết hạn. Mở Cảnh báo để thấy lô sắp hết hạn, cả lô của thuốc đã ẩn. Các cờ tồn và trạng thái này là chủ ý của dữ liệu trình diễn, không phải kết luận về bản thân hoạt chất.

20 phiếu nhập mới có ghi chú chứa `PHARMACY20261030`, lập trước các hóa đơn bán mẫu. Tổng chứng từ và nhập–xuất–tồn từng lô được kiểm tra trong transaction khi nạp. Script `ThemDuLieuPharmacy.sql` có dấu mốc để chạy lại không tạo trùng và không đặt lại dữ liệu đã thao tác.

Các nhãn DEMO trên dữ liệu mẫu của lần nạp trước được bỏ riêng; script vẫn giữ dấu mốc nội bộ để nhận diện nguồn dữ liệu. Việc này không xóa chứng từ hoặc thay số tiền/tồn của các giao dịch trước.
