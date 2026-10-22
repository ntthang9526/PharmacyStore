using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class KhachHangBLL
    {
        private KhachHangDAL dal = new KhachHangDAL();

        public DataTable TimKiemKhachHang(string keyword) => dal.TimKiemKhachHang(keyword);

        public DataTable GetAllKhachHang()
        {
            return dal.GetAllKhachHang();
        }

        public bool ThemMoiKhachHang(string maKH, string tenKH, string dienThoai, string diem, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(tenKH))
            {
                error = "Mã khách hàng và Tên khách hàng không được để trống!";
                return false;
            }

            try
            {
                return dal.InsertKhachHang(maKH.Trim(), tenKH.Trim(), dienThoai.Trim(), diem.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool SuaKhachHang(string maKH, string tenKH, string dienThoai, string diem, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maKH) || string.IsNullOrWhiteSpace(tenKH))
            {
                error = "Vui lòng chọn khách hàng cần sửa và nhập tên!";
                return false;
            }

            try
            {
                return dal.UpdateKhachHang(maKH.Trim(), tenKH.Trim(), dienThoai.Trim(), diem.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool XoaKhachHang(string maKH, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maKH))
            {
                error = "Vui lòng chọn khách hàng cần xóa!";
                return false;
            }

            try
            {
                return dal.DeleteKhachHang(maKH.Trim());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
