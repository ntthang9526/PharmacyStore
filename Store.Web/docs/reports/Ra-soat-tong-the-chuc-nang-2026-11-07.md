# Rà soát tổng thể chức năng website Pharmacy

**Ngày:** 07/11/2026  
**Phạm vi:** Mã nguồn Store.Web, các controller/service/view liên quan và bộ test hiện có.  
**Kết quả thực thi:** Build thành công; **47/47 test đạt**, 0 lỗi, 0 bỏ qua. Hai phép kiểm tra bổ sung dùng database và tài khoản giả xác nhận hai thiếu sót ở nghiệp vụ tư vấn.

## 1. Kết luận

Danh sách chức năng hiện tại đủ rộng cho bài tập lớn. Nên giữ phạm vi và hoàn thiện các luồng đang có, không cần bổ sung hệ thống thanh toán thật, chat, AI hoặc giao vận bên ngoài.

Hai lỗi KT-01 và KT-02 của lượt trước đã có sửa trong mã và có test hồi quy: chặn hoàn tiền đơn đang giao; mã hóa các trường văn bản khi xuất CSV. Không tiếp tục tính chúng là lỗi chưa sửa của bản hiện tại.

Những điểm còn cần xử lý tập trung vào kiểm soát tài khoản bị khóa, kiểm tra trạng thái tư vấn và xử lý đơn giao thất bại. Không sửa mã chức năng hoặc dữ liệu kinh doanh trong lượt rà soát này.

## 2. Kết quả theo từng nhóm chức năng

| Nhóm | Phần đã có và được đối chiếu | Đánh giá trong phạm vi rà soát |
|---|---|---|
| Danh mục và tra cứu | Tên/hoạt chất, nhóm, khoảng giá, tồn, sắp xếp, phân trang, chi tiết và sản phẩm liên quan | Đủ chức năng cơ bản; phân trang vẫn xử lý sau khi tải toàn bộ kết quả/lô |
| Giỏ hàng | Session, thêm/sửa/xóa, kiểm tra thuốc kê đơn và tồn, voucher, phí giao hàng | Có kiểm tra tại server; giỏ chưa giữ tồn là quy tắc hiện tại |
| Đặt đơn | Lấy lại giá/tồn, FEFO, giữ hàng, transaction, khóa chống tạo đơn lặp | Có kiểm thử nghiệp vụ; chưa chạy lại giao dịch ghi trên SQL Server trong lượt này |
| Thanh toán và giao hàng | COD, nhận chuyển khoản tách khỏi hoàn tất, kiểm tra lô khi hoàn tất | Đã có các test hồi quy; cần thêm nhánh xử lý khi giao thất bại |
| Hủy và hoàn tiền | Hủy trước giao, giải phóng phần giữ, hoàn tiền có lịch sử, chặn thao tác lặp và chặn hoàn tiền khi SHIPPING | Hai lỗi gần nhất đã được bổ sung sửa và test |
| Quản trị thuốc | Thêm/sửa, ảnh, editor textarea, trạng thái dùng chung POS, thuốc ẩn, concurrency form | Chức năng hiện có đáp ứng phạm vi đồ án |
| Nhóm và nhà cung cấp | Thêm/sửa/xóa có kiểm tra liên kết | Cần giữ nguyên ràng buộc khi xóa dữ liệu có chứng từ |
| Kho và nhập thuốc | Lô, hạn, khóa lô, phiếu nhiều dòng, kiểm tra NCC, trùng lô và dữ liệu dòng | Có xử lý nghiệp vụ và transaction; chưa kiểm tra lại form trong trình duyệt ở lượt này |
| Voucher | Tạo/sửa/bật tắt, điều kiện áp dụng, trừ/hoàn lượt theo đơn | Có xử lý tại server; không tự dùng chung voucher POS |
| Báo cáo | Theo ngày, khoản thu/hoàn/thực còn lại, CSV | Ngày cuối và CSV đã có test hồi quy; chưa đối chiếu lại chứng từ thực tế trên SQL Server |
| Tài khoản và địa chỉ | Đăng ký, đăng nhập, đổi mật khẩu, sửa hồ sơ, địa chỉ, kích hoạt khách POS và khóa tài khoản | Khóa tài khoản chưa được kiểm tra đồng nhất ở mọi luồng |
| Tư vấn đơn thuốc | Upload ảnh riêng tư, lịch sử yêu cầu, Admin phản hồi, quyền tải ảnh theo chủ sở hữu/Admin | Có hai thiếu sót đã tái hiện ở mục 3 |
| API và phân quyền | Controller Admin có quyền Admin, form có CSRF, API sản phẩm/giỏ/đơn/tư vấn | Đã đọc mã các đường xử lý liên quan; không coi là đã kiểm thử HTTP toàn bộ |
| VI/EN và responsive | Có tài nguyên ngôn ngữ, layout và các view responsive | Có triển khai; chưa nghiệm thu trực quan mọi màn hình trong lượt này |

## 3. Các điểm còn cần xử lý

### TT-01 Khách bị khóa vẫn gửi được yêu cầu tư vấn

**Mức:** P2, nên sửa trước khi dùng chức năng khóa tài khoản để quản lý truy cập thực tế.

**Đã tái hiện:** tạo hồ sơ thử có `IsLocked=true`, gửi một ảnh PNG thử hợp lệ vào `PrescriptionService.CreateRequestAsync`. Service vẫn trả thành công:

```text
LockedCustomerUploadAccepted=True
```

**Nguyên nhân:** service chỉ kiểm tra hồ sơ tồn tại, không kiểm tra cờ khóa. Controller upload kiểm tra người dùng và CustomerProfileId nhưng không kiểm tra lại trạng thái khóa. Admin ToggleLock đổi cờ, chưa có cơ chế cập nhật security stamp/kiểm tra tài khoản bị khóa để làm mất hiệu lực phiên đã đăng nhập.

Đăng nhập mới và checkout có kiểm tra khóa; không kết luận rằng mọi chức năng đều bỏ qua trạng thái này. Lượt thử xác nhận trực tiếp nhánh service tư vấn; chưa thực hiện lại tình huống giữ cookie trên hai trình duyệt.

**Ảnh hưởng:** một người đang có phiên đăng nhập có thể tiếp tục gửi yêu cầu tư vấn sau khi bị khóa. Thông báo “đã khóa tài khoản” dễ khiến Admin hiểu rằng mọi thao tác đã bị ngăn.

**Hướng sửa:** thống nhất hiệu lực của khóa tài khoản. Kiểm tra trạng thái khóa tại các thao tác cần cấm, đặc biệt gửi tư vấn; nếu muốn ngắt phiên thì bổ sung cơ chế kiểm tra cookie/security stamp phù hợp. Không chỉ chặn nút ở giao diện.

**Nghiệm thu:** người đã đăng nhập bị khóa không gửi được yêu cầu mới; không tạo file/bản ghi mới khi bị chặn. Mở khóa hoạt động lại đúng quy tắc; checkout vẫn chặn tài khoản bị khóa.

**Vị trí:** `Services/PrescriptionService.cs` — `CreateRequestAsync`; `Controllers/PrescriptionRequestController.cs` — `Upload`; `Areas/Admin/Controllers/CustomersController.cs` — `ToggleLock`; `Program.cs` — cấu hình Identity/cookie.

### TT-02 Phản hồi tư vấn chấp nhận trạng thái không hợp lệ

**Mức:** P2. Chỉ liên quan thao tác Admin; không phải quyền cập nhật dành cho khách thông thường.

**Đã tái hiện:** gọi `RespondRequestAsync` với nội dung phản hồi hợp lệ và trạng thái `INVALID_STATUS`:

```text
InvalidStatusAccepted=True
StoredStatus=INVALID_STATUS
```

**Nguyên nhân:** controller kiểm tra nội dung phản hồi không rỗng, nhưng service gán trực tiếp `newStatus` vào bản ghi, chưa đối chiếu tập trạng thái cho phép. Dropdown không thay thế kiểm tra server khi có POST trực tiếp hoặc yêu cầu sai từ client.

**Ảnh hưởng:** dữ liệu có trạng thái mà bộ lọc hoặc giao diện không nhận diện được; yêu cầu tư vấn có thể biến mất khỏi các nhóm theo dõi dự kiến hoặc hiển thị sai nhãn.

**Hướng sửa:** khai báo tập trạng thái nghiệp vụ dùng chung và chỉ chấp nhận giá trị thuộc tập này. Xác định chuyển trạng thái nào được phép; dữ liệu sai phải trả thông báo và không lưu phản hồi một phần.

**Nghiệm thu:** trạng thái hợp lệ lưu được; chuỗi lạ/rỗng bị từ chối; trạng thái và nội dung cũ không đổi khi lỗi. Kiểm tra qua cả service và action Respond.

**Vị trí:** `Services/PrescriptionService.cs` — `RespondRequestAsync`; `Areas/Admin/Controllers/PrescriptionsController.cs` — `Respond`.

### TT-03 Chưa có luồng kết thúc đơn giao thất bại

**Mức:** P2, khoảng trống chức năng xác định qua mã; chưa tái hiện giao hàng thực tế.

Trạng thái SHIPPING hiện chỉ chuyển tiếp sang COMPLETED. Hoàn tiền trực tiếp khi SHIPPING đã được chặn đúng theo sửa lỗi KT-01, nhưng chưa có bước giao thất bại/nhận hàng trả để giải quyết đơn không giao được.

**Ảnh hưởng:** đơn bị giao thất bại không có thao tác đúng nghiệp vụ để kết thúc; phần giữ tồn có thể tồn tại kéo dài. Không được đánh dấu giao thành công giả chỉ để đưa đơn ra khỏi danh sách chờ.

**Hướng sửa tối thiểu cho đồ án:** bổ sung xử lý giao thất bại có lý do và bước xác nhận hàng đã nhận lại. Chỉ giải phóng hoặc điều chỉnh tồn khi hàng thực tế đã được kiểm tra. Sau đó xử lý hủy/hoàn tiền theo trạng thái thanh toán. Chưa cần tích hợp đơn vị vận chuyển hoặc tự động hoàn tiền qua ngân hàng.

**Nghiệm thu:** đơn đang giao không được giải phóng tồn chỉ bằng thông báo giao thất bại; xác nhận nhận lại hàng xử lý đúng một lần; đơn đã thu tiền có lịch sử hoàn rõ ràng; không làm thay đổi đơn đã giao thành công.

**Vị trí:** `Services/OrderService.cs` — `UpdateOrderStatusByAdminAsync`, `RefundOrderAsync`; `Areas/Admin/Views/Orders/Details.cshtml`.

## 4. Điểm có thể để sau

- Catalog hiện tải toàn bộ kết quả và các lô trước khi cắt trang. Có thể đưa tổng hợp tồn, lọc và phân trang xuống SQL khi dữ liệu lớn; chưa cần đổi kiến trúc cho quy mô đồ án hiện tại.
- Chưa có thời hạn tự giải phóng tồn cho đơn bỏ dở. Trước mắt cần quy trình Admin rà đơn chờ; cơ chế tự động có thể bổ sung sau khi quy tắc hết hạn đơn được thống nhất.
- Ảnh upload đang kiểm tra một phần chữ ký file và có thể bỏ qua file sai trong tập nhiều ảnh. Nên báo rõ file nào bị từ chối, kiểm tra chữ ký/khả năng đọc ảnh đầy đủ và giới hạn số ảnh khi hoàn thiện.
- Không cần thêm chat, AI tư vấn, nhiều chi nhánh hoặc cổng thanh toán thật để đáp ứng phạm vi chức năng môn học hiện tại.

Các điểm này là giới hạn hoặc cải tiến đề xuất; không tính thành lỗi đã tái hiện trong lượt này.

## 5. Phạm vi kiểm chứng và bước chốt

1. Đã build và chạy 47 test hiện có. Các test nghiệp vụ chủ yếu dùng SQLite trong bộ nhớ; test mapping SQL Server không tương đương giao dịch thật trên SQL Server.
2. Đã bổ sung hai phép kiểm tra tư vấn độc lập bằng DLL hiện tại, dữ liệu giả và ảnh thử trong thư mục kiểm thử riêng. Không ghi dữ liệu chính.
3. Đã đọc các đường xử lý của danh mục, giỏ, đặt đơn, thanh toán, hủy/hoàn, kho, báo cáo, tài khoản và tư vấn. Không coi việc đọc mã là kiểm thử toàn bộ endpoint hoặc giao diện.
4. Chưa chạy lại luồng ghi nhiều máy trên SQL Server, toàn bộ UI/responsive, xử lý mất mạng hoặc kiểm thử tải trong lượt này.
5. Trước khi chốt bản demo: sửa TT-01 và TT-02, quyết định phạm vi TT-03, rồi kiểm tra trọn luồng trên database SQL Server thử riêng và trên trình duyệt.

Đường dẫn trong từng mục tính từ `Store.Web`, trừ tên dự án được ghi rõ. Báo cáo phản ánh bản mã nguồn tại thời điểm kiểm tra.
