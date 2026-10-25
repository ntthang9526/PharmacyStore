using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class NhomThuocBLL
    {
        private NhomThuocDAL dal = new NhomThuocDAL();

        public bool ThemNhanhNhom(string name, out int id, out string error)
        {
            id = 0;
            error = DanhMucValidation.NameError(name, 100) ?? "";
            if (error.Length > 0) { error = "Tên nhóm: " + error; return false; }
            try { id = dal.ThemNhanh(DanhMucValidation.NormalizeName(name)); return true; }
            catch (Exception ex) { error = ex.Message; return false; }
        }

        // 1. Lấy danh sách nhóm thuốc
        public DataTable TimKiemNhom(string keyword) => dal.TimKiemNhomThuoc(keyword);

        public DataTable GetAllNhomThuoc()
        {
            return dal.GetAllNhomThuoc();
        }

        // 2. Thêm nhóm thuốc (Kiểm tra mã và tên không để trống)
        public bool ThemMoiNhom(string maNhom, string tenNhom, out string error)
        {
            return ThemNhanhNhom(tenNhom, out _, out error);
        }

        // 3. Sửa nhóm thuốc
        public bool SuaNhom(string maNhom, string tenNhom, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maNhom) || string.IsNullOrWhiteSpace(tenNhom))
            {
                error = "Mã nhóm và Tên nhóm không được để trống!";
                return false;
            }

            try
            {
                return dal.UpdateNhomThuoc(maNhom.Trim(), tenNhom.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 4. Xóa nhóm thuốc
        public bool XoaNhom(string maNhom, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maNhom))
            {
                error = "Vui lòng chọn nhóm thuốc cần xóa!";
                return false;
            }

            try
            {
                return dal.DeleteNhomThuoc(maNhom.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
