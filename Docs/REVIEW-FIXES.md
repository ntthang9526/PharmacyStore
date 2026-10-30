# Sửa bốn vấn đề sau rà soát — 30/10/2026

## Cập nhật database đang có

Chạy `Store.DAL/Database/AddOrderHeaderSnapshots.sql` trước khi chạy bản chương trình mới. Script chỉ thêm các cột lưu thông tin đầu hóa đơn; chạy lại được và không điền lại dữ liệu lịch sử. `InitDatabase.sql` đã gồm phần này cho database tạo mới.

## 1. Dòng chọn và đối tượng thao tác

Các màn Thuốc, Nhóm thuốc, NCC, Khách hàng, Voucher, Nhân viên, Hóa đơn và Phiếu nhập dùng chung xử lý chuyển dòng bằng bàn phím, gồm Page Up/Down và Enter. Xử lý khi rời bảng hoàn tất lựa chọn trước khi bảng bỏ tô chọn. Dòng trong phiếu nhập nháp cũng áp dụng cơ chế này.

Khi đang thêm/sửa danh mục, bảng danh sách được khóa cho đến khi lưu hoặc thoát chế độ sửa. Bấm/chuyển dòng không tự thay thông tin đang nhập. Bỏ chọn bằng vùng trống vẫn hoạt động ở chế độ xem. Các cấu hình cột, kích thước, màu và bố cục vẫn nằm trong Designer.

## 2. Xem lại đơn thuốc

Phần “Đơn thuốc (bấm xem)” ở chi tiết hóa đơn là một liên kết thực sự, có thể dùng bàn phím. Nhấp liên kết giữ mã hóa đơn đang xem và mở đúng thông tin đơn thuốc ở chế độ chỉ đọc. Bấm vùng trống bên ngoài vẫn bỏ chọn như trước.

## 3. Cảnh báo thuốc đã ẩn

Cảnh báo hạn theo mọi lô còn tồn, bao gồm thuốc đã ẩn. Các dòng này có nhãn **[Đã ẩn]** ở tên và trạng thái; không đưa thuốc trở lại danh mục bán. Thuốc đã ẩn vẫn không được thanh toán/xuất bán. Quy tắc thuốc sắp hết hàng cần nhập thêm không thay đổi.

## 4. Thông tin đầu hóa đơn

- Hóa đơn mới lưu tên/số điện thoại khách hàng, tên người lập, mã voucher; nếu thu ngay thì lưu cả tên người thu. Khách lẻ và không dùng voucher được lưu là các giá trị hợp lệ.
- Khi sửa đơn chờ, cập nhật tên/số điện thoại khách và mã voucher theo bản sửa; giữ tên người lập ban đầu, lưu tên người sửa tại thời điểm sửa.
- Khi thanh toán đơn chờ, giữ thông tin khách/voucher đã chốt trong bản đã lưu và ghi tên người thu tại thời điểm thu. Đơn chờ cũ thiếu thông tin thì phần có thể ghi nhận tại lúc thu được bổ sung, không suy đoán tên người lập lúc trước.
- Khi hủy đơn chờ, lưu tên người hủy tại thời điểm thao tác. Các mã nhân viên/khách/voucher vẫn giữ để truy vết.
- Danh sách, chi tiết, trang chủ, báo cáo, Excel và PDF ưu tiên các thông tin đã lưu này. Tìm kiếm vẫn tìm được bằng tên, số điện thoại hoặc mã voucher đã lưu dù danh mục sau đó đổi.
- Chứng từ cũ chưa có đủ bản lưu tên vẫn đọc được; phần thiếu dùng danh mục hiện tại và có nhãn nguồn thông tin. Giao diện, PDF và Excel ghi rõ điều này, không coi dữ liệu vừa lấy từ danh mục là dữ liệu lịch sử gốc.

## Kiểm tra

`Store.SmokeTests.exe --review-fixes` kiểm tra thao tác phím trên tám màn hình, bảo vệ editor, MouseDown rồi mở liên kết đơn thuốc, cảnh báo lô của thuốc đã ẩn, giữ thông tin qua lập/sửa/thu/hủy, tìm kiếm lịch sử, báo cáo và các bản xuất. Dùng dữ liệu thử riêng trên PharmacyStoreTrialDB và dọn sau mỗi lần chạy.

Các bộ kiểm tra đơn chờ, kê đơn/PDF, phân quyền, lịch sử chứng từ, bảng/phiếu/báo cáo và Designer cần tiếp tục đạt sau thay đổi.
