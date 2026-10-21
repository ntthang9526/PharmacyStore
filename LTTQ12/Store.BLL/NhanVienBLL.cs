using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL dal = new NhanVienDAL();

        public bool DangNhap(string username, string password, out int maNV, out string hoTen, out string error)
        {
            maNV = 0;
            hoTen = "";
            error = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                error = "Vui lòng nhập tài khoản!";
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                error = "Vui lòng nhập mật khẩu!";
                return false;
            }

            try
            {
                DataTable dt = dal.DangNhap(username.Trim(), password);
                if (dt.Rows.Count == 0)
                {
                    error = "Sai tài khoản hoặc mật khẩu, hoặc tài khoản đã bị khóa!";
                    return false;
                }

                maNV = Convert.ToInt32(dt.Rows[0]["Id"]);
                hoTen = dt.Rows[0]["FullName"].ToString() ?? "";
                return true;
            }
            catch (Exception ex)
            {
                error = "Không kết nối được cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }
    }
}
