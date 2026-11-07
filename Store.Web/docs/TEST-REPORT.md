# BÁO CÁO TỔNG HỢP KẾT QUẢ KIỂM THỬ HỆ THỐNG PHARMACY STORE WEB

**Ngày cập nhật:** 07/11/2026  
**Phiên bản / Nhánh:** `main` (Store.Web & tests/PharmacyWeb.Tests)  
**Môi trường thực thi:** .NET 8.0 SDK (x64), Microsoft Windows 11  
**Tập lệnh kiểm thử:** `dotnet test tests/PharmacyWeb.Tests/PharmacyWeb.Tests.csproj --no-restore -p:GitRepositoryConfigurationScope=local --logger "trx;LogFileName=test_results_20261107.trx"`  
**Kết quả kiểm thử tự động:** **47/47 Passed (100%)**, 0 Failed, 0 Skipped  
**Tệp nhật ký TRX lưu trữ:** `tests/PharmacyWeb.Tests/TestResults/test_results_20261107.trx`  

---

## 1. Dòng Thời Gian và Lịch Sử Kiểm Thử (Chronological Test Progression)

Để đảm bảo tính minh bạch và thống nhất giữa các tài liệu môn học, kết quả kiểm thử được phân theo các mốc phát triển:

| Mốc thời gian | Số ca kiểm thử | Phạm vi kiểm tra & Kết quả | Tài liệu liên quan |
|---|:---:|---|---|
| **05/11/2026 (Đợt 1)** | **15/15 Passed** | Kiểm thử lõi ban đầu: Xuất kho FEFO, IdempotencyKey, hủy đơn trả tồn & voucher, xác nhận thu COD, bộ lọc XSS. | `REQUIREMENTS-MAPPING.md`, `DEMO-SCRIPT.md` (bản gốc) |
| **05/11/2026 (Đợt 2)** | **17/17 Passed** (xUnit) + **28 SQL Checks** | Tích hợp ban đầu SQL Server chung POS: schema chia tách `Web*` vs POS, 28 truy vấn SQL xác thực giữ tồn và không ghi đè dữ liệu POS. | `SQLSERVER-POS-INTEGRATION.md` |
| **06/11/2026 (Sáng)** | **22/22 Passed** | Rà soát lỗi đợt 1: Bổ sung kiểm tra báo cáo doanh thu cuối ngày (WEB02), hiển thị danh mục ẩn (WEB03), xử lý đồng thời RowVersion (WEB04). | `Bao-cao-loi-website-Pharmacy-2026-11-06.md` |
| **06/11/2026 (Tối)** | **39/39 Passed** | Sửa lỗi bổ sung BS-01 -> BS-05: luồng hoàn tiền, chặn xuất lô khóa/hết hạn, làm sạch SVG XSS, nhãn POS/Web, ngân hàng linh hoạt. | `Bao-cao-loi-bo-sung-website-Pharmacy-2026-11-06.md` |
| **07/11/2026 (Sáng)** | **43/43 Passed** | Hoàn thiện theo báo cáo hướng dẫn: Kiểm tra cấu trúc hoàn tiền trên SQL Server thật (HT-01), kiểm thử đa ngôn ngữ VI/EN và mặc định tiếng Việt (HT-02), đồng bộ tài liệu (HT-03). | `Huong-dan-sua-cac-diem-con-lai-2026-11-07.md` |
| **07/11/2026 (Hiện tại)** | **47/47 Passed** | Sửa 2 lỗi kiểm tra bổ sung: Chặn hoàn tiền đơn đang giao giải phóng tồn quá sớm (KT-01) và chuẩn hóa RFC 4180 escape nháy kép cho 13 cột xuất CSV (KT-02). | `Bao-cao-kiem-tra-bo-sung-2026-11-07.md`, `TEST-REPORT.md` |

---

## 2. Phân Tách Ba Loại Bằng Chứng Kiểm Thử (Evidence Categories)

Hệ thống phân biệt rõ ràng 3 cấp độ kiểm chứng, không dùng một con số chung để đánh đồng giữa môi trường bộ nhớ và cơ sở dữ liệu thật:

### Cấp 1: Kiểm thử tự động trên SQLite In-Memory (42 ca)
- **Mục tiêu:** Kiểm tra logic nghiệp vụ tầng Service và Controller, thuật toán phân bổ FEFO, trừ tồn kho, tính giá voucher, phân quyền, sanitization HTML, chặn hoàn tiền đơn đang giao (KT-01) và mã hóa chuẩn xuất CSV 13 cột (KT-02).
- **Đặc điểm:** Tự tạo schema trong RAM bằng SQLite In-Memory, chạy nhanh, hoàn toàn độc lập và không ảnh hưởng đến database kinh doanh thật.

### Cấp 2: Kiểm thử Mapping Schema & Configuration không mở kết nối (5 ca)
- **Mục tiêu:** Kiểm tra DbContext Model mapping của EF Core đối với SQL Server (`PharmacyWebDbContext`), cấu hình RequestLocalization và tài nguyên bản dịch `.resx`.
- **Bằng chứng:** 
  - `SharedPosModelTests.SharedCatalogAndWebTransactionsMustNotUseTheSameOrderTablesAsPos`: Xác nhận `Medicines`, `Customers`, `Batches` trỏ vào bảng POS, trong khi `WebOrders`, `WebUsers` trỏ vào bảng Web riêng.
  - `LocalizationAndRefundModelTests.WebOrdersEntity_ShouldMapRefundProperties`: Xác nhận 5 thuộc tính hoàn tiền (`IsRefunded`, `RefundAmount`, `RefundedAt`, `RefundedByEmployee`, `RefundReason`) được ánh xạ đúng kiểu dữ liệu.
  - `LocalizationAndRefundModelTests.SharedPosDatabase_GenerateIntegrationScript_ShouldContainRefundColumns`: Kiểm tra script tự động có chứa các cột hoàn tiền.
  - `LocalizationAndRefundModelTests.SharedResource_ShouldProvideVietnameseAndEnglishTranslations`: Xác nhận bộ tài nguyên `.resx` trả về bản dịch chuẩn xác cho cả tiếng Việt và tiếng Anh.
  - `LocalizationAndRefundModelTests.RequestLocalization_DefaultCulture_ShouldBeVietnamese`: Xác nhận văn hóa mặc định là `vi-VN`.

### Cấp 3: Kiểm chứng Cấu trúc & Dữ liệu trên SQL Server Local thật (`.\SQLEXPRESS` / `PharmacyStoreDB`)
- **Mục tiêu:** Đảm bảo database local thật phục vụ demo chạy thông suốt, không phát sinh lỗi thiếu cột khi truy vấn.
- **Thực thi ngày 07/11/2026:**
  1. Đã sao lưu toàn vẹn database: `PharmacyStoreDB_backup_20261107.bak` (1058 pages processed).
  2. Áp dụng script `Store.Web/Database/IntegratePosDatabase.sql`.
  3. Kết quả truy vấn `sys.columns` trên `dbo.WebOrders`:
     - `IsRefunded`: `bit`, `NOT NULL`, Default 0
     - `RefundAmount`: `decimal(18,2)`, `NOT NULL`, Default 0
     - `RefundedAt`: `datetime2(7)`, `NULL`
     - `RefundedByEmployee`: `nvarchar(100)`, `NULL`
     - `RefundReason`: `nvarchar(500)`, `NULL`
  4. Xác nhận số lượng bản ghi POS không đổi: 73 thuốc, 74 lô hàng, 25 khách hàng.

---

## 3. Bảng Chi Tiết 43 Ca Kiểm Thử Tự Động (Test Matrix)

| STT | Mã / Tên Ca Kiểm Thử | Nhóm kiểm thử | Đầu vào / Kịch bản | Kết quả dự kiến | Thực tế | Trạng thái |
|:---:|---|---|---|---|---|:---:|
| 1 | `CancelOrder_ShouldReleaseReservedStockAndReturnVoucher` | Order & FEFO | Đơn hàng chờ chưa thu tiền, có giữ lô và dùng voucher | Lô hàng giải phóng `ReservedQuantity`, voucher hoàn lượt sử dụng | Khớp dự kiến | **PASS** |
| 2 | `Checkout_ShouldReserveBatchUsingFefoOrder` | Order & FEFO | Đặt mua sản phẩm có nhiều lô với hạn dùng khác nhau | Hệ thống giữ chỗ lô có hạn dùng gần nhất trước (FEFO) | Khớp dự kiến | **PASS** |
| 3 | `Checkout_WithIdempotencyKey_ShouldNotDuplicateOrderOrReservedStock` | Order & FEFO | Gửi 2 yêu cầu checkout cùng `IdempotencyKey` | Đơn chỉ tạo 1 lần, tồn kho chỉ giữ 1 lần | Khớp dự kiến | **PASS** |
| 4 | `ConfirmPayment_ShouldPermanentlyDeductStock` | Order & FEFO | Đơn hàng chuyển sang trạng thái đã thu tiền (PAID) | Tồn thực trừ vĩnh viễn, giải phóng lượng giữ tương ứng | Khớp dự kiến | **PASS** |
| 5 | `ReportService_GetDashboardMetrics_ShouldExecuteWithoutSQLiteDecimalSum` | Order & FEFO | Gọi hàm tính chỉ số KPI Dashboard | Truy vấn tổng doanh thu không lỗi kiểu dữ liệu | Khớp dự kiến | **PASS** |
| 6 | `Medicine_AvailableStock_ShouldBeZero_WhenNotSellingOrInactive` | Unit Test | Thuốc có cờ ngừng kinh doanh hoặc inactive | Tồn khả dụng hiển thị bằng 0 | Khớp dự kiến | **PASS** |
| 7 | `Batch_AvailableStock_ShouldBeZero_WhenBlockedOrExpired` | Unit Test | Lô thuốc bị khóa hoặc quá hạn sử dụng | Lô không được tính vào tồn có thể bán | Khớp dự kiến | **PASS** |
| 8-11| `ShippingFee_Rule_ShouldCalculateCorrectly` (4 cases) | Unit Test | Tổng tiền giỏ hàng: 0đ, 85.000đ, 100.000đ, 120.000đ | Dưới 100k tính 15k; từ 100k trở lên hoặc 0đ tính 0đ | Khớp dự kiến | **PASS** |
| 12 | `Voucher_FixedDiscount_CannotExceedSubtotal` | Unit Test | Voucher giảm tiền mặt lớn hơn giá trị đơn | Tiền giảm tối đa bằng tổng tiền hàng, không âm | Khớp dự kiến | **PASS** |
| 13 | `Voucher_PercentDiscount_ShouldRespectMaxDiscountCap` | Unit Test | Voucher giảm % có cấu hình mức trần tối đa | Tiền giảm không vượt quá trần quy định | Khớp dự kiến | **PASS** |
| 14 | `HtmlSanitizer_ShouldRemoveScriptTags` | Unit Test | Đoạn văn bản chứa thẻ `<script>` | Toàn bộ thẻ script và nội dung bị loại bỏ hoàn toàn | Khớp dự kiến | **PASS** |
| 15 | `HtmlSanitizer_ShouldRemoveOnEventAttributes` | Unit Test | Thẻ HTML chứa sự kiện `onerror`, `onload` | Các thuộc tính sự kiện bị gỡ bỏ | Khớp dự kiến | **PASS** |
| 16 | `WEB02_RevenueReport_ShouldIncludeTransactionsThroughoutEndOfDay` | Bug Fix | Lọc báo cáo theo ngày kết thúc | Lấy đủ giao dịch đến `23:59:59.999` của ngày chọn | Khớp dự kiến | **PASS** |
| 17 | `WEB03_ProductCatalog_StorefrontShouldHideInactive_AdminShouldAccessAll` | Bug Fix | Khách và Admin cùng xem danh mục | Khách không thấy thuốc ẩn; Admin quản lý đủ | Khớp dự kiến | **PASS** |
| 18 | `WEB04_ProductService_UpdateProduct_ShouldThrowConcurrency_OnStaleRowVersion` | Bug Fix | Hai người cùng sửa 1 bản ghi thuốc | Người lưu sau bị chặn bởi `DbUpdateConcurrencyException` | Khớp dự kiến | **PASS** |
| 19 | `WEB05_HtmlSanitizer_ShouldNeutralizeUnquotedJavascriptUri_AndDangerousSchemes` | Bug Fix | Thuộc tính href chứa `javascript:` không nháy | Scheme nguy hiểm bị vô hiệu hóa an toàn | Khớp dự kiến | **PASS** |
| 20 | `WEB08_OrderService_BankTransfer_ShouldConfirmPaymentWithoutPrematureCompletion` | Bug Fix | Xác nhận thanh toán chuyển khoản | Trạng thái chuyển `PAID`, đơn giữ `CONFIRMED` chưa tự nhảy hoàn tất | Khớp dự kiến | **PASS** |
| 21 | `BS01_DirectCancellation_OnPaidOrder_MustBeBlocked` | Bổ sung BS | Thử hủy trực tiếp đơn hàng đã thu tiền | Hệ thống từ chối hủy, yêu cầu xử lý hoàn tiền | Khớp dự kiến | **PASS** |
| 22 | `BS01_UnpaidOrder_CanBeCancelled_AndReleasesBatchReservation` | Bổ sung BS | Hủy đơn chưa thanh toán | Cho phép hủy và giải phóng tồn đã giữ | Khớp dự kiến | **PASS** |
| 23 | `BS01_RefundOrder_Succeeds_MarksCancelledAndRefunded_PreservesOriginalPaidData` | Bổ sung BS | Thực hiện hoàn tiền đơn đủ điều kiện | Lưu số tiền hoàn, lý do, người hoàn, giữ lịch sử thu | Khớp dự kiến | **PASS** |
| 24 | `BS01_RefundOrder_CannotRefundTwice` | Bổ sung BS | Gửi yêu cầu hoàn tiền lần 2 cho đơn đã hoàn | Hệ thống từ chối thao tác lặp | Khớp dự kiến | **PASS** |
| 25 | `BS01_RevenueReport_ReconcilesCollected_Refunded_AndNetRevenue` | Bổ sung BS | Xem báo cáo doanh thu khi có đơn hoàn tiền | Hiển thị rõ tổng thực thu, tổng hoàn tiền và doanh thu thuần | Khớp dự kiến | **PASS** |
| 26 | `BS02_CompletingOrder_WithExpiredBatch_MustFailAndNotDeductStock` | Bổ sung BS | Hoàn tất đơn mà lô hàng đã hết hạn | Chặn giao hàng, không trừ tồn kho sai | Khớp dự kiến | **PASS** |
| 27 | `BS02_CompletingOrder_WithBlockedBatch_MustFailAndNotDeductStock` | Bổ sung BS | Hoàn tất đơn có lô bị khóa trong kho | Chặn giao hàng, yêu cầu kiểm tra lô | Khớp dự kiến | **PASS** |
| 28 | `BS02_CompletingOrder_ValidBatches_DeductsOnce_AndRejectsRepeatAttempt` | Bổ sung BS | Hoàn tất đơn hợp lệ rồi gửi lại | Trừ tồn chính xác 1 lần, từ chối gọi lặp | Khớp dự kiến | **PASS** |
| 29 | `BS02_CompletingOrder_MultipleAllocations_OneInvalid_RollsBackAll` | Bổ sung BS | Đơn có nhiều lô, trong đó 1 lô không hợp lệ | Rollback toàn bộ transaction, không trừ dở dang | Khớp dự kiến | **PASS** |
| 30 | `BS03_HtmlSanitizer_ShouldPreserveValidEditorFormatting` | Bổ sung BS | Soạn thảo rich text in đậm, nghiêng, danh sách | Giữ nguyên các định dạng hợp lệ | Khớp dự kiến | **PASS** |
| 31 | `BS03_HtmlSanitizer_ShouldEliminateSvgWithSlashOnload` | Bổ sung BS | Mã SVG chứa thuộc tính `/onload=` | Bị loại bỏ, không cho phép bypass script | Khớp dự kiến | **PASS** |
| 32 | `BS03_HtmlSanitizer_ShouldEliminateMathAndDangerousContainers` | Bổ sung BS | Thẻ `<math>` và container nguy hiểm | Loại bỏ triệt để các vector nhúng mã độc | Khớp dự kiến | **PASS** |
| 33 | `BS04_ProductFormViewModel_LabelsIndicateSharedWebAndPosScope` | Bổ sung BS | Form quản trị sản phẩm | Nhãn phân biệt rõ trường dùng chung POS và trường riêng Web | Khớp dự kiến | **PASS** |
| 34 | `BS05_BankTransfer_DisabledInConfig_BlocksBankTransferCheckout` | Bổ sung BS | Cấu hình tắt chuyển khoản ngân hàng | Khách không thể chọn phương thức chuyển khoản | Khớp dự kiến | **PASS** |
| 35 | `BS05_BankTransfer_OrderResult_ReturnsConfiguredBankDetails` | Bổ sung BS | Khách đặt đơn chuyển khoản thành công | Trang kết quả hiện đúng thông tin ngân hàng trong cấu hình | Khớp dự kiến | **PASS** |
| 36 | `ConfirmPassword_ShouldHaveVietnameseRequiredErrorMessage` | Bổ sung BS | Validation trường xác nhận mật khẩu | Thông báo lỗi tiếng Việt rõ ràng, chuẩn ngữ cảnh | Khớp dự kiến | **PASS** |
| 37 | `FullLifecycle_BankTransfer_Checkout_Confirm_Paid_Ship_Complete` | Bổ sung BS | Trọn vòng đời đơn chuyển khoản giao thành công | Đi qua đủ 5 trạng thái đơn và thanh toán chuẩn xác | Khớp dự kiến | **PASS** |
| 38 | `FullLifecycle_BankTransfer_Checkout_Confirm_Paid_CancelAndRefund` | Bổ sung BS | Trọn vòng đời đơn chuyển khoản bị hủy và hoàn tiền | Hoàn tiền chuẩn, tồn giữ giải phóng đúng | Khớp dự kiến | **PASS** |
| 39 | `SharedCatalogAndWebTransactionsMustNotUseTheSameOrderTablesAsPos` | Shared POS | Kiểm tra bảng trong DbContext Model | Danh mục dùng chung, đơn hàng tách riêng biệt | Khớp dự kiến | **PASS** |
| 40 | `WebOrdersEntity_ShouldMapRefundProperties` | Đa ngữ & DB | Đối chiếu Entity `Order` với 5 cột hoàn tiền | Ánh xạ đủ 5 cột `IsRefunded`, `RefundAmount`... | Khớp dự kiến | **PASS** |
| 41 | `SharedPosDatabase_GenerateIntegrationScript_ShouldContainRefundColumns` | Đa ngữ & DB | Tạo script tích hợp POS tự động | Script có chứa các lệnh ALTER TABLE bổ sung cột hoàn tiền | Khớp dự kiến | **PASS** |
| 42 | `SharedResource_ShouldProvideVietnameseAndEnglishTranslations` | Đa ngữ & DB | Đọc tài nguyên `SharedResource` theo Culture | `vi-VN` trả tiếng Việt, `en-US` trả tiếng Anh chuẩn | Khớp dự kiến | **PASS** |
| 43 | `RequestLocalization_DefaultCulture_ShouldBeVietnamese` | Đa ngữ & DB | Kiểm tra cấu hình pipeline localization | Mặc định là `vi-VN`, hỗ trợ Cookie và QueryString | Khớp dự kiến | **PASS** |
| 44 | `KT01_RefundOrder_OnShippingOrder_MustBeBlocked_AndPreserveStockAndStatus` | Bổ sung KT | Thử hoàn tiền khi đơn đang ở trạng thái SHIPPING | Chặn hoàn tiền, không giải phóng tồn kho khi hàng đang giao | Khớp dự kiến | **PASS** |
| 45 | `KT01_RefundOrder_OnCompletedOrder_MustBeBlocked` | Bổ sung KT | Thử hoàn tiền đơn đã hoàn tất (COMPLETED) | Chặn hoàn tiền theo luồng hủy trước giao | Khớp dự kiến | **PASS** |
| 46 | `KT01_RefundOrder_OnConfirmedOrder_ShouldSucceed_AndReleaseStock` | Bổ sung KT | Hoàn tiền đơn CONFIRMED hợp lệ còn tại kho | Chấp nhận hoàn tiền, ghi nhận 5 cột hoàn, giải phóng tồn giữ | Khớp dự kiến | **PASS** |
| 47 | `KT02_RevenueCsv_SpecialCharactersAndQuotes_ShouldPreserveColumnsAndValues` | Bổ sung KT | Xuất CSV dữ liệu chứa nháy kép `A",B`, dấu phẩy, tiếng Việt | Chuẩn RFC 4180 escape `""`, đủ đúng 13 cột, không lệch dữ liệu | Khớp dự kiến | **PASS** |

---

## 4. Kết Luận Nghiệm Thu

1. **Về chức năng hoàn tiền & quản lý tồn (HT-01 & KT-01):** Mã nguồn, model EF Core, script tích hợp và database SQL Server local `PharmacyStoreDB` đã đồng bộ 100% cấu trúc 5 cột hoàn tiền. Luồng hoàn tiền được bảo vệ chặt chẽ: chỉ cho phép với đơn chưa xuất giao (`NEW`, `CONFIRMED`), chặn đứng thao tác hoàn tiền khi đơn đang ở trạng thái `SHIPPING` nhằm ngăn ngừa giải phóng tồn kho khi hàng đang trên đường giao.
2. **Về xuất báo cáo CSV đối chiếu (KT-02):** Toàn bộ 13 cột dữ liệu chuỗi trong file CSV được chuẩn hóa escape nháy kép theo chuẩn RFC 4180 (`EscapeCsvField`), đảm bảo các trường tên khách, người thu, người hoàn, lý do hoàn chứa ký tự đặc biệt, dấu phẩy, nháy kép hay tiếng Việt đều hiển thị trọn vẹn và không gây lệch cột.
3. **Về đa ngôn ngữ & Mặc định Tiếng Việt (HT-02):** Toàn bộ luồng khách hàng (Danh mục, Chi tiết, Giỏ hàng, Đặt hàng, Kết quả đơn, Đơn của tôi, Đơn thuốc tư vấn, Đăng nhập, Đăng ký, Header, Footer) đã đồng bộ ngôn ngữ qua tài nguyên `.resx` và bộ chuyển đổi VI/EN. Mặc định trang web luôn là **Tiếng Việt (`vi-VN`)** không phụ thuộc ngôn ngữ trình duyệt của người dùng.
4. **Về tài liệu (HT-03):** Toàn bộ số liệu lịch sử (15, 17, 22, 28, 39, 43) đã được định vị chính xác theo mốc thời gian; số liệu nghiệm thu hiện tại được chốt là **47/47 tests đạt 100%**.
