using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Store.DAL;
namespace Store.BLL;

public class NhanVienBLL
{
    private readonly NhanVienDAL dal=new();
    public DataTable TimKiem(string keyword) => dal.TimKiem(keyword);
    public bool Luu(int? id,string name,string username,string password,string phone,bool active,int operatorId,
        out int savedId,out string error)
    {
        savedId=0;error="";name=name.Trim();username=username.Trim();phone=phone.Trim();
        error=DanhMucValidation.NameError(name,100)??"";
        if(error.Length==0 && !Regex.IsMatch(username,@"\A[A-Za-z0-9_.-]{3,50}\z"))
            error="Tài khoản gồm 3–50 chữ cái không dấu, chữ số, dấu _, . hoặc -.";
        if(error.Length==0 && (!id.HasValue || password.Length>0) && (password.Length<6 || password.Length>128 || string.IsNullOrWhiteSpace(password) || password.Any(char.IsControl)))
            error="Mật khẩu gồm 6–128 ký tự, không chỉ có khoảng trắng hoặc ký tự điều khiển.";
        if(error.Length==0 && phone.Length>0 && DanhMucValidation.CustomerPhoneError(phone)!=null)
            error="Số điện thoại phải gồm đúng 10 chữ số, bắt đầu bằng 0.";
        if(error.Length==0 && (operatorId<=0 || id is <=0)) error="Vui lòng đăng nhập và chọn nhân viên hợp lệ.";
        if(error.Length>0) return false;
        try
        {
            savedId=dal.Luu(id,DanhMucValidation.NormalizeName(name),username,password.Length==0?null:MatKhauNhanVien.Tao(password),
                phone.Length==0?null:phone,active,operatorId);
            return true;
        }
        catch(SqlException ex) when(ex.Number is 2601 or 2627) {error="Tài khoản đăng nhập đã tồn tại.";}
        catch(Exception ex) {error="Không lưu được nhân viên: "+ex.Message;}
        return false;
    }
    public bool Khoa(int id,int operatorId,out string error)
    {
        error="";
        if(id<=0 || operatorId<=0) {error="Vui lòng đăng nhập và chọn nhân viên.";return false;}
        try {dal.Khoa(id,operatorId);return true;}
        catch(Exception ex) {error=ex.Message;return false;}
    }
}
