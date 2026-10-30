using Store.DAL;
namespace Store.BLL;
public class MatKhauBLL
{
    public bool Doi(string oldPassword,string newPassword,string confirm,out string error)
    {
        error="";
        if(string.IsNullOrEmpty(oldPassword)) error="Nhập mật khẩu hiện tại.";
        else if(newPassword.Length<6 || newPassword.Length>128 || string.IsNullOrWhiteSpace(newPassword) || newPassword.Any(char.IsControl))
            error="Mật khẩu mới gồm 6–128 ký tự, không chỉ có khoảng trắng hoặc ký tự điều khiển.";
        else if(newPassword!=confirm) error="Nhập lại mật khẩu mới chưa khớp.";
        else if(newPassword==oldPassword) error="Mật khẩu mới phải khác mật khẩu hiện tại.";
        if(error.Length>0) return false;
        try
        {
            var dal=new MatKhauDAL();string old=dal.LayHash();
            if(!MatKhauNhanVien.KiemTra(oldPassword,old)) {error="Mật khẩu hiện tại không đúng.";return false;}
            if(!dal.Doi(old,MatKhauNhanVien.Tao(newPassword))) {error="Mật khẩu đã thay đổi. Hãy nhập lại mật khẩu hiện tại.";return false;}
            return true;
        }
        catch(Exception ex) {error=ex.Message;return false;}
    }
}
