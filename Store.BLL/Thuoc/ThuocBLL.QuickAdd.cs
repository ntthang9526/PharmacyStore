namespace Store.BLL;
public partial class ThuocBLL
{
    public bool ThemNhanhThuoc(string name, int categoryId, int supplierId, string unit, decimal price,
                               out int id, out string error,string? barcode=null)
    {
        id=0;
        error= DanhMucValidation.NameError(name,150) ?? DanhMucValidation.NameError(unit,30) ?? "";
        if (error.Length==0 && (categoryId<=0 || supplierId<=0)) error="Vui lòng chọn nhóm thuốc và nhà cung cấp.";
        if (error.Length==0 && (price<0 || price>9999999999999999.99m || decimal.Round(price,2)!=price))
            error="Giá bán không hợp lệ, chỉ nhập tối đa 2 chữ số thập phân.";
        barcode=string.IsNullOrWhiteSpace(barcode)?null:barcode.Trim();
        if(error.Length==0 && barcode!=null && (barcode.Length>50 || barcode.Any(char.IsControl))) error="Mã vạch tối đa 50 ký tự, không có ký tự điều khiển.";
        if(error.Length>0) return false;
        try { id=dal.ThemNhanh(name.Trim(),categoryId,supplierId,unit.Trim(),price,barcode);return true; }
        catch(Exception ex) {error="Không thêm được thuốc: "+ex.Message;return false;}
    }
}
