# BÁO CÁO KẾT QUẢ KIỂM THỬ (TEST REPORT)
## DỰ ÁN PHARMACY WEB — ASP.NET CORE MVC (STORE.WEB)
*Cập nhật ngày 05/11/2026 sau khi rà soát và khắc phục toàn bộ các điểm đối chiếu với Prototype*

---

## 1. Kết quả Chạy Kiểm thử Tự động (Automated Test Suite)

Lệnh thực thi kiểm thử:
```powershell
dotnet test "E:\PharmacyStoreSolution\PharmacyStore\PharmacyWeb\tests\PharmacyWeb.Tests\PharmacyWeb.Tests.csproj"
```

**Kết quả thực tế từ hệ thống:**
- **Tổng số test:** 16 test cases.
- **Thành công:** **16 / 16 Passed (100%)**.
- **Thất bại:** 0.
- **Bỏ qua (Skipped):** 0.
- **Thời gian chạy:** ~1–2 giây.

```text
Passed!  - Failed: 0, Passed: 16, Skipped: 0, Total: 16, Duration: 1 s - PharmacyWeb.Tests.dll (net8.0)
```

### Danh mục 16 Ca Kiểm thử Tự động:

| Nhóm Kiểm thử | Tên Test Case | Mục đích Kiểm tra | Kết quả |
| :--- | :--- | :--- | :---: |
| **Bảo mật & XSS** | `HtmlSanitizer_ShouldRemoveScriptTags` | Loại bỏ hoàn toàn `<script>` độc hại khỏi nội dung Rich Text | **PASSED** |
| **Bảo mật & XSS** | `HtmlSanitizer_ShouldRemoveOnEventAttributes` | Loại bỏ các thuộc tính bắt sự kiện (`onerror`, `onload`) | **PASSED** |
| **Nghiệp vụ Kho** | `Batch_AvailableStock_ShouldBeZero_WhenBlockedOrExpired` | Lô hàng bị khóa hoặc hết hạn có tồn có thể bán bằng 0 | **PASSED** |
| **Nghiệp vụ Thuốc** | `Medicine_AvailableStock_ShouldBeZero_WhenNotSellingOrInactive` | Thuốc ngừng bán hoặc đã ẩn không được phép bán ra | **PASSED** |
| **Phí Vận chuyển** | `ShippingFee_Rule_ShouldCalculateCorrectly` | Miễn phí giao hàng cho đơn từ 100k, thu 15k cho đơn dưới 100k | **PASSED** |
| **Voucher** | `Voucher_FixedDiscount_CannotExceedSubtotal` | Tiền giảm cố định không vượt quá tổng giá trị hàng | **PASSED** |
| **Voucher** | `Voucher_PercentDiscount_ShouldRespectMaxDiscountCap` | Giảm phần trăm tuân thủ đúng mức trần tối đa `MaxDiscountCap` | **PASSED** |
| **FEFO Xuất kho** | `Checkout_ShouldReserveBatchUsingFefoOrder` | Tự động giữ chỗ lô có hạn dùng gần nhất còn hạn trước | **PASSED** |
| **Chống Trùng Đơn** | `Checkout_WithIdempotencyKey_ShouldNotDuplicateOrderOrReservedStock` | POST lặp lại cùng `IdempotencyKey` chỉ tạo đúng 1 đơn, không nhân đôi tồn giữ | **PASSED** |
| **Hủy Đơn & Hoàn Tồn** | `CancelOrder_ShouldReleaseReservedStockAndReturnVoucher` | Khách hủy đơn hoàn trả chính xác tồn giữ trên lô và trả lại lượt voucher | **PASSED** |
| **Thanh toán COD** | `ConfirmPayment_ShouldPermanentlyDeductStock` | Admin xác nhận thu tiền COD: trừ tồn thực tế vĩnh viễn và đổi trạng thái sang COMPLETED | **PASSED** |
| **Báo cáo & Tổng hợp** | `ReportService_GetDashboardMetrics_ShouldExecuteWithoutSQLiteDecimalSumException` | Tính toán doanh thu thực thu KPI trên SQLite và SQL Server không bị lỗi cú pháp `Sum(decimal)` | **PASSED** |

---

## 2. Các Lỗi & Điểm Lệch Giao Diện Đã Khắc Phục (Theo Báo cáo Đối chiếu)

| Điểm đối chiếu | Hiện tượng ban đầu | Giải pháp khắc phục thực tế | Bằng chứng kiểm tra |
| :--- | :--- | :--- | :---: |
| **1. Admin nội dung bị đẩy xuống** | Body có `flex-direction: column`, thiếu wrapper `.admin-shell` khiến sidebar chiếm hết chiều ngang trên, main bị đẩy xuống $y=729.6\text{px}$. | Khôi phục wrapper `.admin-shell`, `.admin-sidebar` (nền tối `#0F172A`), `.admin-main-wrapper`, `.admin-topbar` và `.admin-content-area`. Hỗ trợ nút thu gọn desktop và mobile drawer. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |
| **2. Dashboard lỗi SQLite Sum(decimal)** | `SQLite cannot apply aggregate operator 'Sum' on expressions of type 'decimal'` khi Admin mở `/Admin/Dashboard`. | Chuyển câu lệnh tổng hợp tiền `paidToday` và `paidMonth` sang nạp danh sách `decimal` và tính `Sum()` trong bộ nhớ; bổ sung bài test tự động bảo đảm đường này luôn chạy đạt. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |
| **3. Header & Bố cục Trang chủ** | Header thiếu từ khóa gợi ý; Trang chủ thiếu khối banner mint và dải ribbon teal của 3 nhóm sản phẩm. | Đồng bộ hoàn toàn `.store-header`, `.lc-top-strip`, `.lc-main-header`, `.search-box-wrapper` kèm từ khóa gợi ý (`Panadol Extra`, `Oresol`, `Vitamin C`); Hero đôi chuẩn banner mint + hộp 3 bước Rx; 6 ô tiện ích `.lc-quick-tile`; 3 khối sản phẩm `.lc-section-card` có dải `.lc-section-ribbon`. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |
| **4. Thẻ sản phẩm & Ảnh bao bì** | Card dùng icon thay vì ảnh bao bì, khác cấu trúc `.product-card`. | Cấu trúc chuẩn `.product-card`, `.product-card-media`, `.product-card-body`, `.lc-unit-pill-row`, `.product-card-price-row`, nút `.btn-ph`. Tích hợp `MedicineSvgHelper` tự động tạo artwork bao bì dược phẩm SVG tinh tế cho mọi sản phẩm khi chưa có ảnh tải lên. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |
| **5. Responsive Catalog Filter** | Mobile màn hình nhỏ bị tràn bộ lọc trước danh sách sản phẩm. | Tách bộ lọc desktop sang `d-none d-lg-block` và bổ sung thanh mở Offcanvas Drawer `#mobileFilterDrawer` chuyên biệt cho màn hình mobile/tablet dưới 992px. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |
| **6. Quản lý Hồ sơ & Sổ địa chỉ** | Trang Profile chỉ cho xem, thiếu chức năng chỉnh sửa thông tin và quản lý địa chỉ. | Bổ sung action `UpdateProfile`, `AddAddress`, `DeleteAddress`, `SetDefaultAddress` và các modal giao diện tương ứng trên `Views/Account/Profile.cshtml`. | **ĐÃ KHẮC PHỤC TRIỆT ĐỂ** |

---

## 3. Kịch bản Kiểm tra Trình duyệt Trực quan (Manual Browser Test Matrix)

| Kịch bản kiểm thử | Thao tác thực hiện | Kết quả thực tế |
| :--- | :--- | :---: |
| **1. Khám phá Trang chủ** | Mở `/`: Xem Header 3 tầng, Hero đôi, 6 ô tiện ích, 3 dải ribbon sản phẩm | Hiển thị chuẩn xác từng component và token màu POS Teal/Mint |
| **2. Catalog & Drawer Mobile** | Mở `/Products`, thu nhỏ màn hình hoặc bật mobile view | Có nút bấm mở Drawer Offcanvas lọc thuốc, sản phẩm xếp 2 cột/hàng |
| **3. Quản lý Địa chỉ** | Đăng nhập khách hàng &rarr; Vào `/Account/Profile` &rarr; Bấm "Thêm địa chỉ" | Thêm địa chỉ mới, chọn làm mặc định hoặc xóa địa chỉ thành công |
| **4. Đăng nhập Admin** | Đăng nhập tài khoản `admin` / `Admin@123456` &rarr; Vào `/Admin/Dashboard` | Sidebar tối bên trái, nội dung nằm ngang bên phải; các chỉ số KPI tải mượt mà không có exception |
| **5. Thu gọn Sidebar** | Bấm nút menu cạnh logo Pharmacy Admin trên desktop | Sidebar thu gọn sang 72px chỉ hiện icon; bấm lại mở rộng 256px |
