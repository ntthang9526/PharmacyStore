using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public partial class ThuocBLL
    {
        private ThuocDAL dal = new ThuocDAL();

        public DataTable GetAllThuoc()
        {
            return dal.GetAllThuoc();
        }

        public bool ThemMoiThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, string strDonGia, string strSoLuong, int supplierId, out string error, bool dangBan=true,bool? keDon=null,string? barcode=null,string? hoatChat=null)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(tenThuoc))
            {
                error = "Mã thuốc và Tên thuốc không được để trống!";
                return false;
            }

            if (!decimal.TryParse(strDonGia, out decimal donGia) || donGia < 0 ||
                !int.TryParse(strSoLuong, out int soLuong) || soLuong < 0)
            { error = "Đơn giá hoặc số lượng không hợp lệ."; return false; }

            return dal.InsertThuoc(maThuoc.Trim(), tenThuoc.Trim(), maNhom, dvt, donGia, soLuong, supplierId, out error, dangBan,keDon,barcode,hoatChat);
        }

        public bool SuaThuoc(string maThuoc, string tenThuoc, string maNhom, string dvt, string strDonGia, string strSoLuong, int supplierId, out string error, bool dangBan=true,bool? keDon=null,string? barcode=null,string? hoatChat=null)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maThuoc) || string.IsNullOrWhiteSpace(tenThuoc))
            {
                error = "Mã thuốc và Tên thuốc không được để trống!";
                return false;
            }

            if (!decimal.TryParse(strDonGia, out decimal donGia) || donGia < 0 ||
                !int.TryParse(strSoLuong, out int soLuong) || soLuong < 0)
            { error = "Đơn giá hoặc số lượng không hợp lệ."; return false; }

            return dal.UpdateThuoc(maThuoc, tenThuoc, maNhom, dvt, donGia, soLuong, supplierId, out error, dangBan,keDon,barcode,hoatChat);
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
