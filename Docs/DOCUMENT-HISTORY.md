# Đơn vị và lịch sử tên thuốc

- Đơn vị chỉ được chọn lúc thêm thuốc. Khi sửa thuốc đã tạo, ô đơn vị bị khóa; tầng lưu dữ liệu cũng từ chối thay đổi. Chưa có cơ chế quy đổi nhiều đơn vị.
- Hóa đơn mới (kể cả đơn chờ) và phiếu nhập mới lưu tên/đơn vị từ danh mục tại thời điểm lập, cùng giao dịch lưu chứng từ. Đổi tên danh mục sau đó không đổi nội dung chứng từ đã lưu.
- Khi thu tiền đơn chờ, tên/đơn vị đã lưu lúc lập đơn được giữ khi phân lô; giá và số lượng vẫn theo đơn chờ. Tồn và trạng thái thuốc/lô vẫn được kiểm tra lúc thanh toán.
- Cột Nguồn tên/đơn vị được ẩn ở hai bảng chi tiết Hóa đơn và Phiếu nhập. Dữ liệu nguồn vẫn được lưu và có trong Excel chi tiết hóa đơn, phân biệt "Lưu lúc lập chứng từ" với "Chưa có lịch sử; danh mục hiện tại".
- Dữ liệu cũ chưa lưu tên/đơn vị không thể khôi phục chính xác chỉ từ danh mục hôm nay. Các trường lịch sử của dòng cũ được để NULL, chỉ hiển thị danh mục hiện tại làm thông tin tham khảo và ghi rõ nguồn. Đơn chờ cũ khi thanh toán vẫn giữ dấu hiệu thiếu lịch sử, không tự coi tên hiện tại là tên gốc.
- Database hiện tại bổ sung 6 cột nullable (tên và đơn vị ở OrderDetails, OrderPendingItems, StockInDetails), không tạo bảng/database mới và không điền suy đoán vào chứng từ cũ. Khi dùng database khác, chạy Store.DAL/Database/AddDocumentSnapshots.sql trước; script chạy lại được.

## Nhà cung cấp và nguồn cung lô

- Thuốc chưa có lô được đổi nhà cung cấp. Khi đã có lô (kể cả hết tồn hoặc lô cũ chưa có phiếu nhập), giao diện khóa ô NCC/nút thêm NCC và tầng lưu cũng từ chối đổi NCC. Đổi tên/giá/trạng thái thuốc vẫn được thực hiện như trước; đơn vị vẫn khóa sau khi tạo.
- Màn Lô hàng lấy NCC từ các phiếu nhập liên kết với chính lô, không lấy NCC hiện tại trong danh mục thuốc. Lô có nhiều nguồn nhập trong dữ liệu cũ hiển thị đầy đủ các NCC, phân cách bằng dấu chấm phẩy; tìm được theo từng NCC.
- Lô chưa có phiếu nhập hiển thị "Chưa xác định", không suy đoán nguồn từ danh mục hiện tại. Phiếu nhập vẫn giữ SupplierId gốc để tra cứu nguồn hàng.
- Chưa mở rộng nghiệp vụ nhập cùng một thuốc từ nhiều NCC mới; phạm vi ứng dụng hiện tại vẫn là một thuốc–một NCC. Không thêm cột hoặc bảng database cho thay đổi này.
