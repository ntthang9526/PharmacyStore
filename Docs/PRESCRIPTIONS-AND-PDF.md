# Đơn thuốc và hóa đơn PDF

## Cập nhật cơ sở dữ liệu

Với database đã có dữ liệu, chạy `Store.DAL/Database/AddPrescriptions.sql` trước khi dùng bản chương trình mới. Script chỉ thêm cột và mở rộng hướng dẫn dùng thành 500 ký tự, chạy lại được, không xóa dữ liệu. `InitDatabase.sql` cũng đã bao gồm cập nhật này khi tạo database mới.

Chứng từ trước khi cập nhật không được tự điền lại thông tin đơn thuốc. Tên/đơn vị/cờ kê đơn của dòng mới được lưu tại thời điểm giao dịch; bản in cũ chưa có bản lưu lịch sử có ghi chú về việc dùng danh mục hiện tại.

## Quản lý và bán thuốc kê đơn

1. Admin đánh dấu **Thuốc kê đơn** khi tạo/sửa thuốc hoặc trong cửa sổ thêm nhanh. File mẫu Excel có cột **Kê đơn (0/1)**; hỗ trợ cả True/False. File cũ thiếu cột này được hiểu là không kê đơn, vì vậy cần rà lại phân loại sau khi nhập danh mục. Dữ liệu sai giá trị trong cột này bị báo lỗi.
2. Ở Bán hàng, kết quả tìm kiếm và giỏ có nhãn **[Kê đơn]**. Bấm **Đơn thuốc** ở dưới giỏ để nhập mã/số đơn, người bệnh, người kê, ngày kê và hướng dẫn dùng cho từng thuốc.
3. Người bán xác nhận đã kiểm tra đơn thuốc. Chương trình kiểm tra trường bắt buộc, độ dài và ngày tương lai; không tự đề xuất liều dùng hoặc tự xác nhận nội dung chuyên môn.
4. Có thể lưu đơn chờ chưa đủ thông tin; đơn chưa trừ kho. Trước khi thu tiền, phải hoàn thiện thông tin và hướng dẫn dùng. Ở lịch sử chọn **Sửa đơn → Đơn thuốc → Lưu thông tin → Lưu thay đổi**, rồi thanh toán lại.
5. Khi thanh toán, lớp lưu dữ liệu đọc lại phân loại thuốc và kiểm tra thông tin trong cùng giao dịch với xuất kho. Cờ kê đơn và hướng dẫn dùng được giữ trên từng dòng xuất lô. Đổi phân loại danh mục sau đó không làm mất yêu cầu kê đơn đã lưu trong đơn chờ.
6. Chứng từ lưu mã nhân viên và thời điểm xác nhận. Trong lịch sử, bấm phần thông tin có chữ **Đơn thuốc (bấm xem)** để xem lại; hóa đơn đã thanh toán được xem ở chế độ chỉ đọc.

Phạm vi hiện tại: một đơn thuốc cho một hóa đơn; hướng dẫn dùng do người bán nhập theo đơn. Chưa có ảnh đính kèm, quản lý đơn nhiều lần mua hoặc kiểm tra tương tác thuốc.

## In và in lại

Mở **Hóa đơn (F7)**, chọn hóa đơn **Đã thanh toán**, bấm **In / PDF**, chọn nơi lưu rồi mở file để xem hoặc in bằng trình đọc PDF. Có thể xuất lại nhiều lần mà không tạo hóa đơn mới, không trừ kho hoặc dùng voucher thêm lần nữa.

Bản in A5 có mã hóa đơn, ngày lập/thu, nhân viên lập/thu, hình thức thanh toán, tên thuốc, đơn vị, số lô, số lượng, giá, giảm giá và thực thu. Nếu có đơn thuốc, bản in kèm thông tin người bệnh/người kê và hướng dẫn dùng. Hóa đơn dài tự sang trang, lặp tiêu đề bảng và đánh số trang. Đơn chờ/hủy không được in dưới dạng hóa đơn đã thanh toán.

Bản PDF lấy lại dữ liệu đã lưu từ database tại thời điểm bấm in, không lấy giỏ hàng hoặc bảng có thể đã cũ. Lỗi chọn nơi lưu, xuất file hoặc mở trình đọc PDF không thay đổi trạng thái thanh toán.

## Kiểm tra

`Store.SmokeTests.exe --prescriptions-pdf` dùng dữ liệu riêng trên PharmacyStoreTrialDB và dọn các bản ghi đã tạo. Bao gồm kiểm tra từ chối thiếu thông tin, gọi thẳng DAL, đơn chờ, tách lô, bản lưu lịch sử, in đơn thường/kê đơn/nhiều trang, trạng thái nút và mẫu Excel. Các bộ kiểm tra đơn chờ, Excel/thanh toán, bố cục, Designer và phân quyền cũng cần đạt.
