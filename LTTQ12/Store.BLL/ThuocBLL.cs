
using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class ThuocBLL
    {
        private ThuocDAL dal = new ThuocDAL();

        public DataTable GetAllThuoc()
        {
            return dal.GetAllThuoc();
        }

        public bool ThemMoiThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, string strDonGia, string strSoLuong, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maThuoc) || string.IsNullOrWhiteSpace(tenThuoc))
            {
                error = "Mã thuốc và Tên thuốc không được để trống!";
                return false;
            }

            decimal.TryParse(strDonGia, out decimal donGia);
            int.TryParse(strSoLuong, out int soLuong);

            return dal.InsertThuoc(maThuoc.Trim(), tenThuoc.Trim(), maNhom, dvt, donGia, soLuong, out error);
        }

        public bool SuaThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, string strDonGia, string strSoLuong, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maThuoc) || string.IsNullOrWhiteSpace(tenThuoc))
            {
                error = "Mã thuốc và Tên thuốc không được để trống!";
                return false;
            }

            decimal.TryParse(strDonGia, out decimal donGia);
            int.TryParse(strSoLuong, out int soLuong);

            return dal.UpdateThuoc(maThuoc, tenThuoc, maNhom, dvt, donGia, soLuong, out error);
        }

        public bool XoaThuoc(string maThuoc, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maThuoc))
            {
                error = "Vui lòng chọn thuốc cần xóa!";
                return false;
            }
            return dal.DeleteThuoc(maThuoc, out error);
        }

        public DataTable TimKiemThuoc(string tuKhoa)
        {
            return dal.TimKiemThuoc(tuKhoa);
        }
    }
}