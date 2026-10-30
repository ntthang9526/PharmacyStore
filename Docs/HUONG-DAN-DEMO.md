# Bộ dữ liệu demo nhà thuốc

Đã nạp vào **PharmacyStoreDB** trên `.\SQLEXPRESS`, dùng cho bản **PharmacyStore**. Ngày nạp: **30/10/2026**. Dữ liệu mang dấu **DEMO** là giả định phục vụ bài tập lớn, không phải giao dịch thực tế. Giá, thành phần mẫu và hướng dẫn trong đơn không dùng để tư vấn hoặc điều trị.

## Đã chuẩn bị

| Loại | Số lượng thêm mới |
|---|---:|
| Thuốc | 16 |
| Lô hàng | 17 |
| Nhóm thuốc | 4 |
| Nhà cung cấp | 4 |
| Khách hàng | 8 |
| Voucher | 4 |
| Nhân viên demo | 1 |
| Phiếu nhập đã lưu | 4 |
| Hóa đơn đã thu | 12 |
| Hóa đơn chờ | 5 |
| Hóa đơn đã hủy | 1 |

Các hóa đơn đã thu mẫu có tổng sau giảm **522.800 đ**, phân bố trong sáu ngày, để báo cáo có dữ liệu. Dữ liệu cũ được giữ nguyên. Doanh thu trên màn hình sẽ gồm cả dữ liệu cũ và dữ liệu DEMO mới thêm.

## Tài khoản và tìm dữ liệu

- Dùng tài khoản Admin đang có để quản lý danh mục/khóa lô.
- Tài khoản nhân viên mới: **demo_nv2026** — mật khẩu: **Demo@123456**. Không thay mật khẩu hoặc quyền tài khoản cũ.
- Tìm **DEMO** trong danh sách thuốc, khách hàng hoặc hóa đơn để nhận biết các bản mẫu. Thuốc đã ẩn sẽ không hiện trong danh mục đang bán.
- SĐT khách mẫu: **0907001001** đến **0907001008**. Chỉ là dữ liệu giả định; không dùng để liên hệ.
- Sau khi nạp, mở lại phần mềm để cập nhật toàn bộ dữ liệu đang nhớ.

## Các tình huống trình diễn

### 1. Bán tiền mặt

Vào **Bán hàng (F2)** → tìm `DEMO - Vitamin C` hoặc `DEMO - Paracetamol` → thêm thuốc, sửa số lượng → tra khách `0907001001` → chọn tiền mặt → nhập tiền khách đưa → xem tiền trả lại. Nếu bấm Thanh toán sẽ tạo thêm hóa đơn demo và trừ tồn thật trong database bài tập.

Mã vạch Paracetamol mẫu: **9991030000001**; Vitamin C mẫu: **9991030000002**. Có thể nhập vào ô mã thuốc/mã vạch rồi Enter để minh họa dù chưa có máy quét.

### 2. Áp dụng voucher

| Mã | Tình huống |
|---|---|
| DEMO10 | Giảm 10%, còn lượt |
| DEMO5K | Giảm 5.000 đ, còn lượt; mức giảm không vượt tiền hàng |
| DEMOHET | Đã hết lượt, phải bị từ chối |
| DEMOKHOA | Đang bị khóa, phải bị từ chối |

Lượt còn lại đã trừ phần dùng trên các hóa đơn đã thu mẫu. Đơn chờ chưa dùng lượt.

### 3. Sửa đơn chờ thường

Vào **Hóa đơn (F3)** → tìm **HD00816** → Sửa → đổi số lượng hoặc thêm/bớt thuốc → Lưu thay đổi. Mã hóa đơn phải giữ nguyên, chưa trừ tồn. Sau đó có thể chọn hình thức thanh toán để thu tiền.

### 4. Hủy đơn chờ

Có thể dùng HD00816 sau khi demo sửa, hoặc tự tạo một đơn chờ mới. Chọn Hủy → nhập lý do → xác nhận. Hóa đơn vẫn có trong lịch sử với trạng thái Đã hủy; chưa xuất hàng thì tồn không đổi.

Đã có sẵn **HD00821** ở trạng thái Đã hủy, kèm lý do để xem trước.

### 5. Đơn thuốc nhập dở

Mở **HD00817** → thử thanh toán: phải yêu cầu bổ sung đơn thuốc. Chọn Sửa → Đơn thuốc → xem số đơn/người bệnh đã có, nhập tiếp thông tin giả định còn thiếu và hướng dẫn mẫu → xác nhận kiểm tra → Lưu thay đổi.

Có thể bấm **Lưu nháp** trước khi nhập đủ, sau đó lưu hóa đơn chờ. Lưu nháp không đồng nghĩa đã kiểm tra và chưa được thu tiền.

### 6. Đơn thuốc đã kiểm tra

**HD00818** đã có đầy đủ thông tin giả định và hướng dẫn mẫu, có thể dùng để minh họa thu tiền đơn chờ. Nếu mở Sửa rồi đổi số lượng thuốc kê đơn, phải kiểm tra/xác nhận lại trước khi thanh toán.

**HD00815** là hóa đơn kê đơn đã thu, dùng để xem lại liên kết Đơn thuốc và xuất PDF mà không phải thanh toán thêm.

### 7. Đơn chờ thiếu hàng

**HD00819** yêu cầu 10 đơn vị `DEMO - Thuốc sắp hết hàng`, nhưng tồn có thể bán chỉ có **3**. Thử thu tiền phải bị chặn. Sửa số lượng xuống không quá tồn rồi lưu để xử lý tiếp. Đây là tình huống đơn đã lưu nhưng hàng hiện tại không đủ.

### 8. Đơn chờ có voucher hết lượt

**HD00820** dùng `DEMOHET`, hiện còn 0 lượt. Thử thu tiền phải bị chặn. Chọn Sửa để bỏ voucher hoặc áp dụng một mã còn dùng được rồi lưu lại.

### 9. Cảnh báo tồn kho và hạn dùng

Vào **Cảnh báo (F7)**, ngưỡng tồn dưới 10 và hạn còn dưới 30 ngày:

- `DEMO - Thuốc sắp hết hàng`: còn 3.
- `DEMO - Thuốc hết hàng`: còn 0.
- Lô **DEMO26-FEFO** của Paracetamol: còn 2, hết hạn **06/11/2026** sau các hóa đơn mẫu.
- `DEMO - Thuốc có lô hết hạn`: còn 12, hết hạn **25/10/2026**.
- `DEMO - Thuốc đã ẩn sắp hết hạn`: còn 8, hết hạn **09/11/2026**; phải còn cảnh báo dù không bán/không hiện ở danh mục bán.
- Tích “Đã xem” chỉ ghi nhận đã đọc trong phiên, không khóa lô hoặc làm giảm tồn.

Các ngày hạn dùng được tính tại lúc nạp bộ mẫu. Nếu trình diễn vào ngày khác, kiểm tra lại các mốc trên; chạy lại script không tự đặt lại hạn/tồn.

### 10. Khóa lô và ngừng bán

Vào **Quản lý khác (F5) → Lô hàng**, tìm `DEMO`:

- **DEMO26-13**: lô còn hạn, tồn 18 nhưng đã khóa → không được xuất bán.
- **DEMO26-14**: lô hết hạn → không được xuất bán.
- `DEMO - Thuốc ngừng bán`: còn tồn 10 nhưng đang ngừng bán → không có trong danh sách thuốc bán.

Có thể dùng tài khoản Admin mở khóa DEMO26-13 để minh họa lượng có thể bán tăng trở lại. Không cần thay đổi các lô cũ.

### 11. Nhập kho

Vào **Nhập kho (F6)** → chọn một `DEMO - Nhà cung cấp` → thêm thuốc vào phiếu → chọn dòng → sửa số lượng/giá và bấm **Cập nhật dòng** → xem tổng vốn. Đọc file Excel hoặc thêm dòng vào phiếu chưa làm tăng tồn; chỉ **Lưu phiếu** mới ghi nhập kho.

Đã có 4 phiếu nhập mẫu với ghi chú `[DEMO20261030]`, xem ở **Quản lý khác → Phiếu nhập hàng**.

### 12. Báo cáo và in

Vào **Báo cáo (F8)** → chọn từ **25/10/2026** đến **30/10/2026**. Bộ mẫu có cả tiền mặt, chuyển khoản và voucher, người lập là Admin còn người thu là nhân viên DEMO. Đơn chờ và đơn hủy không được cộng doanh thu.

Mở một hóa đơn đã thu trong khoảng **HD00804–HD00815** để xuất PDF/Excel. QR của phần mềm vẫn là xác nhận thủ công; không cần chuyển tiền thật khi demo.

## Kiểm tra đã thực hiện

- Các bản ghi cũ ở 12 bảng nghiệp vụ được so sánh nguyên trạng trước/sau trong transaction; nếu bị thay đổi thì toàn bộ lần nạp bị hủy.
- Tổng từng hóa đơn bằng tổng chi tiết, tổng phiếu nhập bằng chi tiết nhập.
- Với từng lô mẫu: lượng nhập = tồn hiện tại + lượng đã bán.
- Hóa đơn đã thu không lấy lô khóa hoặc hết hạn.
- Chạy lại script không tạo thêm dữ liệu và không đặt lại các thao tác bạn đã thực hiện.

Script **ThemDuLieuDemo.sql** chỉ dùng cho PharmacyStoreDB; không phải script xóa/khởi tạo lại database. Không chỉnh số hóa đơn bằng tay và không dùng InitDatabase.sql để làm mới bộ mẫu.
