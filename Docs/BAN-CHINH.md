# Chuyển sang bản chính — 30/10/2026

- Thư mục chính: `E:\PharmacyStoreSolution\PharmacyStore`.
- Database chính: `PharmacyStoreDB`.
- Thư mục cũ: `E:\PharmacyStoreSolution\PharmacyStore-previous-20261030_163623`.
- Database cũ: `PharmacyStoreDB_previous_20261030_163623`.
- Sao lưu SQL Server: `C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\Backup`.
- Hai file sao lưu: `PharmacyStoreDB_before_primary_20261030_163623.bak` và `PharmacyStoreTrialDB_before_primary_20261030_163623.bak`. Đã chạy RESTORE VERIFYONLY WITH CHECKSUM thành công.

Database bản đang dùng được đổi tên trực tiếp từ PharmacyStoreTrialDB thành PharmacyStoreDB; không nạp lại, không xóa database. Tên tệp mdf/ldf vật lý được giữ nguyên để tránh di chuyển dữ liệu không cần thiết.

Trước/sau chuyển: 130 hóa đơn, 72 thuốc, tổng tồn 14.902, tổng tiền hàng của các hóa đơn 17.004.000 đ. Đây là số liệu đối chiếu lúc chuyển, không phải doanh thu thực thu. Nếu tiếp tục thao tác, các số có thể tăng/thay đổi.

Đã tạo lại solution ở đường dẫn mới, không lỗi/cảnh báo; kiểm tra độc lập bằng chính thư viện của bản chính xác nhận kết nối PharmacyStoreDB, đọc danh mục và mở giao diện Pharmacy. Không tạo hóa đơn trong bước xác minh.

Bản source cũ cùng dữ liệu Git được giữ trong thư mục previous. Không dùng các exe trong thư mục lưu này để tránh mở nhầm ứng dụng cũ; mở file chương trình tại thư mục chính mới.

Góp ý: giữ hai bản sao lưu cho đến khi bạn hoàn tất báo cáo; sau đó chỉ dùng một thư mục chính, và sao lưu database trước các lần thay đổi lớn. Các bộ kiểm tra có ghi dữ liệu phải tiếp tục dùng database thử riêng.
