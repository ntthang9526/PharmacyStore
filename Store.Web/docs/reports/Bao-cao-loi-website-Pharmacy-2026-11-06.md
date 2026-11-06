# Báo cáo lỗi website Pharmacy

Ngày lập: **06/11/2026**  
Phạm vi: dự án **Store.Web**, các luồng kết nối dữ liệu chung với POS.  
Trạng thái: **ghi nhận lỗi, chưa sửa chức năng**.

## 1. Kết quả tổng hợp

Website có đủ nền tảng cho bài tập lớn nhưng cần sửa các luồng nhập kho, báo cáo, quản lý thuốc và bảo vệ nội dung trước khi mở rộng tính năng.

Lượt rà soát gần nhất đã build thành công và chạy **17/17 test đạt**. Tuy nhiên, bốn phép thử độc lập dùng chính DLL web và dữ liệu trong bộ nhớ vẫn tái hiện được các lỗi mất dòng phiếu nhập, lọc thiếu ngày cuối, truy xuất thuốc ẩn và bộ lọc HTML chưa đầy đủ. Các test hiện có chưa bao phủ những trường hợp này.

Không tạo đơn hàng, phiếu nhập hoặc thay đổi tồn kho trên database chính trong lượt kiểm tra. Lượt rà soát này kiểm tra mã nguồn và nghiệp vụ; chưa kiểm tra lại giao diện bằng trình duyệt hay chạy thử tải nhiều người dùng.

### Quy ước mức ưu tiên

- **P1:** cần ưu tiên vì ảnh hưởng tính đúng của dữ liệu hoặc bảo mật.
- **P2:** cần hoàn thiện vì ảnh hưởng thao tác, có cách đi vòng hoặc chỉ xuất hiện trong điều kiện nhất định.
- **Đã tái hiện:** có phép thử độc lập xác nhận kết quả sai.
- **Xác định qua mã:** đã đối chiếu luồng xử lý; chưa thực hiện lại đầy đủ tình huống qua trình duyệt.

| Mã | Nội dung | Mức | Bằng chứng |
|---|---|---|---|
| WEB-01 | Mất dòng phiếu nhập khi xóa dòng giữa | P1 | Đã tái hiện |
| WEB-02 | Báo cáo doanh thu bỏ sót ngày kết thúc | P1 | Đã tái hiện |
| WEB-03 | Thuốc ẩn mất khỏi danh sách Admin nhưng chi tiết công khai vẫn trả dữ liệu | P1 | Đã tái hiện và đối chiếu mã |
| WEB-04 | Form thuốc cũ có thể ghi đè thay đổi mới từ POS | P1 | Xác định qua mã |
| WEB-05 | Bộ lọc HTML còn cho qua URI nguy hiểm | P1 trước khi công khai | Đã tái hiện |
| WEB-06 | Danh sách Admin thiếu điều hướng phân trang | P2 | Xác định qua mã |
| WEB-07 | Nhập kho báo lỗi làm mất dữ liệu và danh sách chọn | P2 | Xác định qua mã |
| WEB-08 | Xác nhận chuyển khoản bị gộp với thu COD và hoàn tất giao hàng | P2 | Xác định qua mã |
| WEB-09 | Menu nhóm thuốc phụ thuộc ID cố định | P2 | Xác định qua mã; có bằng chứng trình duyệt ở lượt trước |
| WEB-10 | Cập nhật hồ sơ chưa kiểm tra và xử lý kết quả đầy đủ | P2 | Xác định qua mã |

## 2. Chi tiết lỗi

### WEB-01 Mất dòng phiếu nhập khi xóa dòng giữa

**Chức năng:** Admin lập phiếu nhập nhiều dòng.

**Cách tái hiện:**

1. Tạo ba dòng thuốc trên phiếu nhập.
2. Xóa dòng thứ hai.
3. Hai dòng còn lại được đặt tên `Lines[0]` và `Lines[2]`.
4. Gửi dữ liệu qua bộ nhận form của ASP.NET.

**Kết quả thực tế:** phép thử gửi hai dòng nhưng model chỉ nhận một dòng. Hàm `removeRow()` chỉ xóa phần tử HTML, không đánh lại chỉ số hoặc gửi `Lines.Index`.

**Kết quả mong đợi:** tất cả dòng còn trên màn hình đều được nhận và kiểm tra khi lưu. Tổng tiền trên màn hình phải khớp các dòng được ghi nhận.

**Ảnh hưởng:** có thể lưu thiếu thuốc và cộng thiếu tồn so với phiếu người dùng đang thấy. Không nên tiếp tục demo nhập nhiều dòng trước khi sửa.

**Hướng sửa:** đánh lại chỉ số liên tục trước khi gửi hoặc dùng trường `Lines.Index` cho từng dòng. Server tính lại tổng từ các dòng nhận được.

**Nghiệm thu:** thử xóa dòng đầu, giữa và cuối; thêm dòng sau khi xóa; xác nhận số dòng và tổng tiền đúng trong mọi trường hợp.

**Vị trí:** `Areas/Admin/Views/Inventory/StockIn.cshtml`, dòng 94–124.

### WEB-02 Báo cáo doanh thu bỏ sót ngày kết thúc

**Chức năng:** báo cáo doanh thu thực thu và xuất CSV.

**Cách tái hiện:** tạo dữ liệu thử có đơn được thu tiền lúc 12:00 ngày 05/11/2026 theo giờ Việt Nam; lọc từ 05/11/2026 đến 05/11/2026.

**Kết quả thực tế:** trả 0 đơn. Khi lấy đủ đến cuối ngày thì có 1 đơn. Ngày kết thúc từ trường chọn ngày có giờ 00:00, nhưng service dùng trực tiếp làm cận cuối.

**Kết quả mong đợi:** bao gồm toàn bộ giao dịch trong ngày kết thúc, theo ngày Việt Nam.

**Ảnh hưởng:** số liệu doanh thu bị thiếu. File CSV sử dụng cùng service nên cũng bị ảnh hưởng; màn hình mặc định và file xuất có thể lệch nhau khi đường dẫn xuất chỉ truyền phần ngày.

**Hướng sửa:** chuẩn hóa đầu ngày bắt đầu; lấy đầu ngày kế tiếp của ngày kết thúc làm cận trên, dùng điều kiện nhỏ hơn cận trên. Chặn khoảng ngày đảo ngược.

**Nghiệm thu:** cùng một ngày phải lấy được đơn lúc đầu ngày, giữa ngày và cuối ngày; đơn ngày tiếp theo không được tính; CSV khớp màn hình.

**Vị trí:** `Services/ReportService.cs`, dòng 129–137; `Areas/Admin/Views/Reports/Index.cshtml`, phần đường dẫn xuất CSV.

### WEB-03 Thuốc ẩn chưa được xử lý nhất quán

**Chức năng:** ẩn thuốc, quản trị danh mục, xem chi tiết công khai.

**Cách tái hiện:** tạo một thuốc thử có `IsActive=false`; gọi service danh mục mà Admin đang sử dụng và service chi tiết công khai.

**Kết quả thực tế:** danh mục trả 0 sản phẩm nhưng service chi tiết vẫn trả thuốc đó. Admin dùng chung `GetCatalogAsync()` với gian hàng, trong đó luôn lọc `IsActive=true`.

**Kết quả mong đợi:** Admin có thể tìm thuốc đã ẩn để bật lại; khách không xem được chi tiết sản phẩm không còn công khai.

**Ảnh hưởng:** thuốc vừa ẩn biến mất khỏi danh sách thao tác thông thường. Đồng thời, nhãn “Hiển thị trên web” không phản ánh đầy đủ ảnh hưởng: `IsActive` là cột chung, POS cũng dùng cờ này để lọc thuốc bán.

**Hướng sửa:** tách điều kiện truy vấn Admin và gian hàng; thêm bộ lọc đã ẩn; chặn chi tiết công khai khi inactive. Nếu chỉ muốn ẩn ở website thì dùng cờ riêng; nếu giữ cờ chung thì đổi nhãn và giải thích ảnh hưởng đúng.

**Nghiệm thu:** Admin ẩn, tìm lại, bật lại được; khách mở link cũ của thuốc ẩn không nhận chi tiết; hành vi POS đúng với lựa chọn của người quản trị.

**Vị trí:** `Services/ProductService.cs`, dòng 48 và 134–158; `Areas/Admin/Controllers/ProductsController.cs`, dòng 24. Đối chiếu `Store.DAL/Thuoc/MedicineDAL.cs`, dòng 18.

### WEB-04 Form thuốc cũ có thể ghi đè thay đổi mới

**Chức năng:** cập nhật thuốc dùng chung giữa POS và website.

**Tình huống theo luồng mã:**

1. Web mở form thuốc đang có giá 45.000 đồng.
2. Người khác ở POS đổi giá thành 50.000 đồng và lưu.
3. Người ở web chỉ sửa mô tả, rồi lưu form cũ vẫn chứa giá 45.000 đồng.

**Kết quả dự kiến từ mã:** service tải bản mới nhất rồi gán lại toàn bộ trường từ form cũ. `RowVersion` gửi lên không được dùng làm phiên bản gốc, nên giá mới có thể bị ghi đè mà không báo xung đột. Chưa thực hiện lại tình huống này trên hai cửa sổ với dữ liệu chính.

**Kết quả mong đợi:** phát hiện form đã cũ, yêu cầu tải lại trước khi lưu thay đổi.

**Hướng sửa:** dùng `RowVersion` từ form để kiểm tra phiên bản gốc và xử lý lỗi cập nhật đồng thời. Không chỉ dựa vào rowversion của lô hàng.

**Nghiệm thu:** hai ngữ cảnh mở cùng một thuốc; bản lưu sau từ form cũ bị từ chối rõ ràng, không làm mất giá hoặc thông tin vừa sửa ở POS.

**Vị trí:** `Services/ProductService.cs`, dòng 326–346; `Areas/Admin/Views/Products/Edit.cshtml`, dòng 21.

### WEB-05 Bộ lọc HTML chưa đầy đủ

**Chức năng:** soạn mô tả thuốc và hiển thị mô tả có định dạng.

**Phép kiểm tra:** đưa vào `HtmlSanitizerService` một thẻ liên kết dùng giao thức `javascript:` ở thuộc tính không có dấu nháy.

**Kết quả thực tế:** chuỗi URI nguy hiểm vẫn còn sau khi lọc. Phép thử chỉ kiểm tra chuỗi trong bộ nhớ, không thực thi mã trên website. Mô tả sau đó được view hiển thị bằng `Html.Raw`.

**Ảnh hưởng:** nội dung được Admin nhập hoặc dán từ bên ngoài có thể giữ lại thành phần không an toàn khi người xem tương tác. Đây là điểm cần sửa trước khi công khai website.

**Hướng sửa:** lọc HTML bằng bộ phân tích có danh sách thẻ, thuộc tính và giao thức được phép. Kiểm tra cả URI mã hóa, thuộc tính không có dấu nháy và nội dung lồng nhau. Mã hóa tên thuốc khi đưa vào chuỗi JavaScript của form nhập kho.

**Nghiệm thu:** nội dung định dạng hợp lệ được giữ; liên kết và thuộc tính nguy hiểm bị loại; có test đi qua chính service đang dùng.

**Vị trí:** `Services/HtmlSanitizerService.cs`, dòng 16–27; `Views/Products/Details.cshtml`, dòng 166; `Areas/Admin/Views/Inventory/StockIn.cshtml`, dòng 95.

### WEB-06 Danh sách Admin thiếu nút chuyển trang

**Chức năng:** quản lý thuốc, khách và đơn hàng.

**Bằng chứng mã:** truy vấn giới hạn số bản ghi theo trang nhưng các view danh sách tương ứng không dựng bộ điều hướng trang. Danh sách thuốc dùng mặc định 12 bản ghi; khách dùng 10 bản ghi.

**Ảnh hưởng:** không có nút để truy cập đầy đủ dữ liệu từ giao diện danh sách. Tìm từ khóa có thể giúp đi vòng, nhưng không thay thế phân trang.

**Hướng sửa:** dùng lại partial phân trang đang có ở gian hàng; giữ điều kiện lọc khi đổi trang; chuẩn hóa trang nhỏ nhất là 1.

**Nghiệm thu:** dữ liệu vượt kích thước trang phải có trang tiếp/trước; tìm kiếm và lọc vẫn giữ đúng khi chuyển trang.

**Vị trí:** `Areas/Admin/Controllers/ProductsController.cs`, dòng 24; `Areas/Admin/Controllers/CustomersController.cs`, dòng 40; các view `Products/Index`, `Customers/Index`, `Orders/Index` trong Admin.

### WEB-07 Form nhập kho không giữ đủ dữ liệu sau lỗi

**Chức năng:** lập phiếu nhập và chọn thuốc theo NCC.

**Tình huống theo mã:** nhập một phiếu có nhiều dòng, sau đó service từ chối vì thuốc không thuộc NCC hoặc dữ liệu lô không hợp lệ.

**Kết quả dự kiến:** nhánh service trả lỗi không nạp lại `ViewBag.Suppliers` và `ViewBag.Medicines`. View luôn tạo một dòng mặc định thay vì dựng lại `model.Lines`. Danh sách thuốc ban đầu chứa thuốc của nhiều NCC và chỉ lấy 100 thuốc active.

**Ảnh hưởng:** người nhập phải điền lại dữ liệu, có thể không còn danh sách để chọn sửa. Khi danh mục vượt 100 thuốc, một số thuốc có thể không xuất hiện trong lựa chọn.

**Hướng sửa:** nạp lại dữ liệu chọn ở mọi nhánh trả view; dựng dòng từ model; hiển thị lỗi cụ thể theo dòng. Lọc/tìm thuốc theo NCC ở giao diện và giữ kiểm tra tương ứng trên server.

**Nghiệm thu:** nhập sai ở một dòng vẫn giữ các dòng khác và thông tin phiếu; sửa rồi lưu được; không ghi phiếu hoặc tồn một phần khi lỗi.

**Vị trí:** `Areas/Admin/Controllers/InventoryController.cs`, dòng 47–77; `Areas/Admin/Views/Inventory/StockIn.cshtml`, phần khởi tạo danh sách và dòng thuốc.

### WEB-08 Chuyển khoản đang bị gộp với hoàn tất giao hàng

**Chức năng:** chọn thanh toán và Admin xác nhận thu tiền.

**Bằng chứng mã:** checkout cho chọn `BANK_TRANSFER`, nhưng trang kết quả chưa cung cấp ngân hàng, số tài khoản hoặc nội dung chuyển. Thao tác xác nhận tiền đang dùng lời hỏi thu tiền mặt, ghi lịch sử COD và chuyển đơn sang đã giao, hoàn tất.

**Ảnh hưởng:** không thể thể hiện đúng trường hợp đã nhận chuyển khoản nhưng chưa giao hàng. Người vận hành có thể hoàn tất đơn sớm chỉ để ghi nhận tiền.

**Hướng sửa:** nếu chưa làm đầy đủ thì giới hạn phạm vi demo ở COD. Nếu giữ chuyển khoản, thêm hướng dẫn nhận tiền và tách xác nhận thanh toán khỏi hoàn tất giao hàng; lịch sử phải ghi đúng phương thức.

**Nghiệm thu:** đơn chuyển khoản đã thu nhưng chưa giao vẫn giữ trạng thái giao phù hợp; đơn không bị thu tiền hoặc trừ tồn hai lần.

**Vị trí:** `Views/Checkout/Index.cshtml`, `Views/Checkout/OrderResult.cshtml`; `Areas/Admin/Views/Orders/Details.cshtml`, dòng 56; `Services/OrderService.cs`, dòng 560–612.

### WEB-09 Menu nhóm thuốc dùng ID cố định

**Chức năng:** điều hướng danh mục ở header, footer và trang chủ.

**Bằng chứng hiện tại:** các liên kết còn gắn cứng ID nhóm 3, 4, 6. Lượt kiểm tra trình duyệt trước ghi nhận “Vật tư y tế” trỏ ID 6 trả 0 sản phẩm, trong khi chọn nhóm thực ID 235 có 4 sản phẩm. Lượt lập báo cáo này không truy vấn lại database để khẳng định số lượng hiện tại.

**Ảnh hưởng:** khi dữ liệu nhóm POS có ID khác, menu có thể mở sai nhóm hoặc không có sản phẩm dù kho có hàng.

**Hướng sửa:** lấy nhóm từ dữ liệu hoặc cấu hình liên kết đã xác thực; xử lý nhóm trùng sau khi kiểm tra liên kết và sao lưu. Không tự xóa nhóm đang được POS sử dụng.

**Nghiệm thu:** mọi liên kết nhóm trên header, footer và trang chủ đều mở đúng nhóm; không dựa vào giả định ID của dữ liệu mẫu cũ.

**Vị trí:** `Views/Shared/_Header.cshtml`, dòng 178–184; `Views/Shared/_Footer.cshtml`, dòng 27–28; `Views/Home/Index.cshtml`, dòng 105–114.

### WEB-10 Cập nhật hồ sơ chưa kiểm tra đầy đủ

**Chức năng:** sửa họ tên, email và thêm địa chỉ nhận hàng.

**Bằng chứng mã:** `UpdateProfile` nhận chuỗi trực tiếp, không kiểm tra email trùng và bỏ qua `IdentityResult` của `UpdateAsync`. Sau đó vẫn đồng bộ hồ sơ và báo thành công. `AddAddress` chủ yếu kiểm tra rỗng, chưa xử lý đầy đủ `ModelState`. Identity đang cấu hình cho phép email không duy nhất.

**Ảnh hưởng:** có thể báo thành công khi cập nhật tài khoản thất bại; hồ sơ chung và tài khoản không đồng nhất. Email trùng có thể làm việc tìm tài khoản đăng nhập bằng email bị lỗi.

**Hướng sửa:** dùng model kiểm tra định dạng và độ dài; chặn email của tài khoản khác; kiểm tra kết quả Identity trước khi lưu hồ sơ chung hoặc báo thành công.

**Nghiệm thu:** email sai/trùng, tên quá dài, SĐT và địa chỉ không hợp lệ đều được báo rõ; lưu thất bại không tạo thông báo thành công hoặc cập nhật một phần.

**Vị trí:** `Controllers/AccountController.cs`, dòng 265–334; `Program.cs`, dòng 41.

## 3. Thứ tự xử lý đề xuất

1. **Sửa trước khi demo:** WEB-01, WEB-02, WEB-03, WEB-06, WEB-07, WEB-09. Đây là các lỗi dễ xuất hiện trong thao tác trình diễn.
2. **Sửa khi dùng chung nhiều máy hoặc trước khi công khai:** WEB-04 và WEB-05. Bảo vệ thay đổi dữ liệu và nội dung phải được hoàn tất trước khi sử dụng rộng.
3. **Hoàn thiện luồng người dùng:** WEB-08 và WEB-10. Nếu chưa triển khai chuyển khoản đầy đủ, mô tả rõ phạm vi COD trong bài demo.

Mỗi lỗi cần có ca kiểm tra đi qua chính service hoặc controller sau khi sửa. Chạy lại bộ test hiện có, rồi kiểm tra trình duyệt cho luồng đã thay đổi. Không chỉ thêm test sao chép công thức từ mã nguồn.

## 4. Phạm vi chưa kiểm tra

- Chưa chạy thử tải và nhiều phiên đồng thời trong lượt này.
- Chưa kiểm thử lại toàn bộ API qua HTTP, mọi luồng CRUD hoặc toàn bộ giao diện responsive.
- Chưa xác nhận lại dữ liệu nhóm và voucher trên database chính ở thời điểm lập báo cáo.
- Các mục đánh dấu “Xác định qua mã” cần được tái hiện trên môi trường kiểm thử trước khi nghiệm thu sửa lỗi.

Các vị trí mã nguồn tính từ thư mục `Store.Web`, trừ đường dẫn ghi rõ `Store.DAL`. Báo cáo phục vụ phân công sửa lỗi cho dự án môn học; không thay thế đánh giá nội dung dược hoặc pháp lý khi vận hành thực tế.
