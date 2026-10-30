# Trạng thái bán thuốc và khóa lô hàng

## Đổi trạng thái thuốc (Admin)

1. Mở Quản lý thuốc, tìm và chọn thuốc.
2. Bấm Sửa → Trạng thái bán → Đang bán hoặc Ngừng bán → Lưu.
3. Muốn bán lại: chọn thuốc đang Ngừng bán, sửa thành Đang bán rồi lưu.

Thuốc ngừng bán vẫn có trong danh sách quản lý, vẫn giữ tồn và lịch sử. Thuốc không hiện khi tìm/quét để bán. Không dùng Xóa để tạm ngừng bán: Xóa chỉ ẩn danh mục và vẫn giữ dữ liệu lịch sử.

## Khóa hoặc mở khóa lô (Admin)

1. Mở Quản lý khác → Lô hàng, tìm rồi chọn đúng lô.
2. Bấm Tạm khóa hoặc Mở khóa và xác nhận.
3. Cột Trạng thái lô hiển thị Tạm khóa, Hết hạn, Thuốc ngừng bán, Thuốc đã ẩn hoặc Được xuất bán theo tình trạng thực tế. Nút chỉ đổi trạng thái khóa; mở khóa không làm lô hết hạn hay thuốc ngừng bán được xuất. Xem thêm cột Tồn có thể bán.

Nhân viên được xem trạng thái và các cột tồn, nhưng không được đổi trạng thái thuốc hay khóa/mở khóa lô. Các form đang mở tự làm mới theo cơ chế cập nhật dữ liệu của ứng dụng.

## Tồn kho và hóa đơn

- Tồn kho (thực tế): cộng số lượng còn trong tất cả các lô, gồm cả lô bị khóa/hết hạn và thuốc ngừng bán. Đổi trạng thái không cộng/trừ số lượng này.
- Tồn có thể bán: chỉ cộng lô không khóa, còn hạn (kể cả ngày hết hạn theo quy tắc hiện tại) của thuốc Đang bán và chưa bị xóa.
- FEFO: khi thanh toán, chọn lô được phép bán có hạn dùng gần nhất trước. Lô bị khóa được bỏ qua.
- Ngừng bán một thuốc chặn tất cả các lô của thuốc đó. Khóa một lô chỉ chặn lô đó.
- Hóa đơn chờ chưa giữ hàng. Lúc thu tiền, hệ thống kiểm tra lại trạng thái và tồn; nếu không hợp lệ, đơn vẫn chờ và không trừ kho. Bật bán/mở khóa rồi có thể thử thanh toán lại.
- Hóa đơn đã thanh toán và báo cáo doanh thu cũ không bị đổi bởi trạng thái mới.
- Nhập kho vẫn được thực hiện cho thuốc chưa xóa, kể cả thuốc ngừng bán hoặc lô bị khóa. Nhập bổ sung vào lô bị khóa không tự mở khóa lô.
- Cảnh báo nhập thêm chỉ xét thuốc Đang bán và tồn có thể bán. Cảnh báo hạn dùng vẫn giữ lô còn tồn để theo dõi xử lý.
- Xuất Excel thuốc có thêm trạng thái và hai loại tồn. Nhập Excel vẫn chỉ thêm thuốc mới (mặc định Đang bán); không dùng nhập Excel để sửa trạng thái thuốc cũ.

Ví dụ: thuốc có 10 hộp ở lô A bị khóa, 5 hộp ở lô B còn hạn và không khóa. Tồn thực tế là 15, tồn có thể bán là 5. Nếu ngừng bán thuốc thì tồn thực tế vẫn 15, tồn có thể bán về 0.

## Cấu trúc database

- Medicines.IsSelling (bit, mặc định 1): đang bán/ngừng bán. IsActive vẫn giữ vai trò xóa mềm cũ.
- Batches.IsBlocked (bit, mặc định 0): tạm khóa lô.
- Không thêm bảng hoặc tạo database mới. Script Store.DAL/Database/AddSellingStatus.sql chạy lại được và giữ nguyên dữ liệu hiện có.
- Database thử trên máy này đã được nâng cấp. Khi chuyển sang database khác, chạy script trên đúng database đó trước khi dùng phiên bản mới; không chạy InitDatabase.sql lên database đang có dữ liệu.
