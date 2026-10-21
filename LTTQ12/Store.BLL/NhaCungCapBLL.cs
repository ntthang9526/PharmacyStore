
using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class NhaCungCapBLL
    {
        private NhaCungCapDAL dal = new NhaCungCapDAL();

        public DataTable GetAllNhaCungCap()
        {
            return dal.GetAllNhaCungCap();
        }

        public bool ThemMoiNCC(string maNCC, string tenNCC, string dienThoai, string diaChi, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maNCC) || string.IsNullOrWhiteSpace(tenNCC))
            {
                error = "Mã nhà cung cấp và Tên nhà cung cấp không được để trống!";
                return false;
            }

            try
            {
                return dal.InsertNhaCungCap(maNCC.Trim(), tenNCC.Trim(), dienThoai.Trim(), diaChi.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool SuaNCC(string maNCC, string tenNCC, string dienThoai, string diaChi, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maNCC) || string.IsNullOrWhiteSpace(tenNCC))
            {
                error = "Vui lòng chọn nhà cung cấp cần sửa và nhập tên!";
                return false;
            }

            try
            {
                return dal.UpdateNhaCungCap(maNCC.Trim(), tenNCC.Trim(), dienThoai.Trim(), diaChi.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaNCC(string maNCC, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maNCC))
            {
                error = "Vui lòng chọn nhà cung cấp cần xóa!";
                return false;
            }

            try
            {
                return dal.DeleteNhaCungCap(maNCC.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}