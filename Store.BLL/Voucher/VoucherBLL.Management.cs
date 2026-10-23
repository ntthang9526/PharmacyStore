using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
namespace Store.BLL;
public partial class VoucherBLL
{
    public DataTable TimKiem(string keyword) => dal.TimKiem(keyword);

    public bool Luu(int? id, string code, string type, decimal value, int quantity, bool active,
                    int? originalQuantity, out string error)
    {
        error = "";
        code = code.Trim().ToUpperInvariant();
        if (!Regex.IsMatch(code, @"\A[A-Z0-9_-]{1,20}\z"))
            error = "Mã voucher gồm 1–20 chữ cái không dấu, chữ số, dấu _ hoặc -.";
        else if (type is not ("FIXED" or "PERCENT")) error = "Vui lòng chọn loại giảm giá.";
        else if (value <= 0 || value > 9999999999999999.99m || decimal.Round(value,2) != value)
            error = "Giá trị giảm phải lớn hơn 0, tối đa 2 chữ số thập phân.";
        else if (type == "PERCENT" && value > 100) error = "Phần trăm giảm không được vượt quá 100%.";
        else if (quantity < 0) error = "Số lượt còn lại không được âm.";
        else if (id.HasValue && (id <= 0 || originalQuantity is null or < 0)) error = "Vui lòng chọn lại voucher cần sửa.";
        if (error.Length > 0) return false;
        if (type == "FIXED") value = Store.Shared.TienTe.LamTron(value);
        if (value <= 0) { error = "Giá trị giảm sau khi làm tròn phải từ 1 đồng."; return false; }
        try
        {
            if (dal.Luu(id,code,type,value,quantity,active,originalQuantity)) return true;
            error = "Voucher đã thay đổi hoặc vừa được sử dụng. Bấm Làm mới rồi chọn lại để sửa.";
        }
        catch (SqlException ex) when (ex.Number is 2601 or 2627) { error = "Mã voucher đã tồn tại."; }
        catch (Exception ex) { error = "Không lưu được voucher: " + ex.Message; }
        return false;
    }

    public bool Xoa(int id, out string error)
    {
        error = "";
        if (id <= 0) { error = "Vui lòng chọn voucher."; return false; }
        try { return dal.Xoa(id); }
        catch (SqlException ex) when (ex.Number == 547)
        { error = "Voucher đã có trong hóa đơn. Bỏ chọn Đang hoạt động để khóa voucher."; }
        catch (Exception ex) { error = "Không xóa được voucher: " + ex.Message; }
        return false;
    }
}
