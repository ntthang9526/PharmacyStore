using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class NhaCungCapBLL
    {
        private NhaCungCapDAL dal = new NhaCungCapDAL();

        public bool ThemNhanhNCC(string name, string phone, string address, out int id, out string error)
        {
            id = 0;
            error = DanhMucValidation.NameError(name, 150) is string nameError ? "Tên nhà cung cấp: " + nameError
                : DanhMucValidation.PhoneError(phone) is string phoneError ? phoneError
                : DanhMucValidation.NameError(address, 250) is string addressError ? "Địa chỉ: " + addressError : "";
            if (error.Length > 0) return false;
            try { id = dal.ThemNhanh(DanhMucValidation.NormalizeName(name), phone.Trim(), address.Trim()); return true; }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        public DataTable TimKiemNCC(string keyword) => dal.TimKiemNhaCungCap(keyword);

        public DataTable GetAllNhaCungCap()
        {
            return dal.GetAllNhaCungCap();
        }

        public bool ThemMoiNCC(string maNCC, string tenNCC, string dienThoai, string diaChi, out string error)
        {
            return ThemNhanhNCC(tenNCC, dienThoai, diaChi, out _, out error);
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
