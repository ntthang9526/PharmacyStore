# Bộ dữ liệu thuốc tham khảo Long Châu — 07/11/2026

Nguồn: [Danh mục thuốc Nhà thuốc FPT Long Châu](https://nhathuoclongchau.com.vn/thuoc). Đường dẫn sản phẩm và ảnh gốc được ghi riêng cho từng bản ghi trong `catalog.json`.

## Nội dung

- 50 sản phẩm mới, có tên thương mại, thành phần, quy cách, nhóm thuốc và ảnh.
- Bổ sung thông tin còn thiếu cho Panadol Extra, Hapacol 250, Augmentin 1g, Panadol Cảm Cúm và Klamentin 500/125; có ảnh đối chiếu cho 4 sản phẩm, riêng Hapacol chưa lấy được ảnh.
- 54 ảnh WebP lưu tại `Store.Web/wwwroot/images/medicines/longchau`, tổng khoảng 4 MB. Không phụ thuộc tải ảnh Long Châu khi khách mở website.
- Chỉ lấy dữ kiện sản phẩm; mô tả được biên soạn ngắn, không chép toàn bộ bài viết, liều dùng hoặc nội dung tư vấn.

| Nhóm trong database | Sản phẩm mới |
|---|---:|
| Vitamin & Khoáng chất | 10 |
| Thuốc giảm đau - hạ sốt | 1 |
| Thuốc dị ứng | 11 |
| Thuốc da liễu | 2 |
| Thuốc tiêu hoá & gan mật | 11 |
| Thuốc hô hấp | 9 |
| Thuốc Mắt, Tai, Mũi, Họng | 6 |
| **Tổng** | **50** |

Giữ và dùng lại nhóm cũ khi phù hợp, thêm 5 nhóm mới. Không gộp hay xóa những nhóm dữ liệu cũ khác.

## Những điểm cần hiểu trước khi dùng

1. Đây là dữ liệu tham khảo phục vụ bài tập. Giá được chụp tại thời điểm thu thập, có thể thay đổi; đơn vị bán có thể là viên, gói, chai hoặc hộp. Giá cũ của các thuốc đã có được giữ nguyên.
2. Farzincol, Klenzit MS và Differin không có giá công khai trong phần thông tin sản phẩm đã đọc. Ba bản ghi này để **ẩn/ngừng bán**, giá tạm 0 trong database; phải nhập giá hợp lệ rồi mới bật bán. Có 47 thuốc mới được bật hiển thị.
3. Không tạo lô, hạn dùng, tồn kho, phiếu nhập hay đơn hàng. Thuốc mới có tồn bằng 0; muốn thử mua hàng cần nhập kho theo chức năng hiện có.
4. Nhà cung cấp cho thuốc mới là **Chưa xác định nhà cung cấp**. Long Châu là nguồn tham khảo, không tự nhận là nhà cung cấp hay nhà sản xuất. Chọn NCC thực tế trước khi nhập kho.
5. `LCREF-001`…`LCREF-050` là mã quản lý nội bộ của bộ nhập, không phải mã EAN/GS1 trên hộp thuốc. Phần mềm hiện lưu chúng trong cột BarCode; khi có hàng thật cần thay bằng mã vạch trên bao bì.
6. Không ghép ảnh biệt dược vào các thuốc chỉ có tên hoạt chất chung hoặc chưa rõ hàm lượng/nhãn. Những thuốc đó giữ ảnh minh họa hiện có.
7. Klamentin 500/125 được sửa cờ kê đơn thành có kê đơn theo ghi chú tại [trang nguồn](https://nhathuoclongchau.com.vn/thuoc/klamentin-625mg-3333.html). Không tự bỏ cờ kê đơn của thuốc cũ khác.
8. Ảnh là tài sản của nguồn/nhãn hàng; bộ này không cấp giấy phép sử dụng thương mại. Giữ đường dẫn nguồn và kiểm tra quyền sử dụng trước khi công khai website ngoài phạm vi học tập.

## Áp dụng trên máy thành viên

Máy hiện tại đã được áp dụng. Các thành viên dùng database riêng cần chạy lại các bước dưới đây. Git chỉ mang mã nguồn và ảnh, không mang các thay đổi SQL Server.

1. Khôi phục database POS `PharmacyStoreDB`, chạy `Store.Web/Database/IntegratePosDatabase.sql` theo hướng dẫn cài đặt website.
2. Sao lưu database trước khi nhập bộ dữ liệu này.
3. Giữ nguyên cấu trúc thư mục ảnh đi kèm mã nguồn. Nếu thiếu ảnh, chạy từ thư mục gốc repository:

   ```powershell
   & .\Store.Web\Database\Catalog\LongChau-20261107\Download-Images.ps1
   ```

4. Mở `Apply-Catalog.sql` trong SSMS, kiểm tra tiếng Việt hiển thị đúng, chọn database `PharmacyStoreDB`, chạy toàn bộ file. Nếu SSMS hỏi encoding, chọn UTF-8.

   Hoặc dùng sqlcmd, **bắt buộc có `-f 65001` để đọc đúng UTF-8**:

   ```powershell
   sqlcmd -S .\SQLEXPRESS -E -C -d PharmacyStoreDB -b -f 65001 -i .\Store.Web\Database\Catalog\LongChau-20261107\Apply-Catalog.sql
   ```

5. Mở lại website, tìm `Berocca`, `Efferalgan` hoặc `Gaviscon`. Vào quản trị để thấy cả 3 thuốc đang ẩn.

Script dùng transaction, chỉ bổ sung trường rỗng của 5 thuốc cũ (ngoại lệ có ghi rõ: cờ kê đơn Klamentin), không thay giá cũ và có kiểm tra tồn kho/số đơn. Chạy lại không tạo thêm thuốc trùng theo mã bộ nhập hoặc tên đầy đủ. Không tự sửa những trường đã được người dùng điền sau lần nhập đầu.

## Kiểm tra đã thực hiện trên máy hiện tại

### Tồn kho mẫu bổ sung theo yêu cầu

Sau lần nhập danh mục ban đầu, đã thêm tồn mở đầu phục vụ bài tập: mỗi thuốc mới 1 lô `LC-INIT-20261107-xxx`, số lượng **100 theo đơn vị bán của thuốc**, hạn dùng giả định **07/11/2028**. Tổng 50 lô / 5.000 đơn vị. Lô và hạn dùng này là dữ liệu tự tạo cho bài tập, không lấy từ Long Châu. Không tạo phiếu nhập hay ghi nhận đã mua hàng từ nhà cung cấp thật.

Thành viên muốn cùng bộ tồn mẫu chạy thêm sau `Apply-Catalog.sql`:

```powershell
sqlcmd -S .\SQLEXPRESS -E -C -d PharmacyStoreDB -b -f 65001 -i .\Store.Web\Database\Catalog\LongChau-20261107\Seed-SampleStock.sql
```

Script chỉ tạo lô cho thuốc LCREF chưa có lô; chạy lại không cộng thêm tồn và không đặt lại số lượng đã bán. Ba thuốc chưa có giá vẫn ẩn/ngừng bán dù có tồn mẫu. Mật khẩu tài khoản thử được giao riêng, không lưu trong repository.

- Trước nhập: 73 thuốc; sau nhập: 123 thuốc, gồm đúng 50 mã LCREF.
- 5 thuốc cũ được đối chiếu, 4 có thêm ảnh; 54 file ảnh hợp lệ, không thiếu file trong manifest.
- So sánh với bản sao lưu trước nhập: các trường nghiệp vụ cũ (tên, giá, đơn vị, mã, nhóm, NCC, trạng thái bán/ẩn), tồn kho và các đơn hàng được giữ nguyên. Cờ kê đơn Klamentin là thay đổi chủ động đã nêu trên.
- Chạy lại script không thêm thuốc trùng: `NewMedicines = 0`.
- Trang danh sách, chi tiết Berocca, API tìm kiếm và ảnh local trả HTTP 200.

Backup an toàn trên máy tạo dữ liệu: `C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\Backup\PharmacyStoreDB_before_LongChau_20261107141826.bak`. Backup này không nằm trong Git.
