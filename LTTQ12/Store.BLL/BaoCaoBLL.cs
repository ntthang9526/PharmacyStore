using System.Data;
using Store.DAL;

namespace Store.BLL
{
    public class BaoCaoBLL
    {
        private BaoCaoDAL dal = new BaoCaoDAL();

        public bool LayBaoCao(DateTime tuNgay, DateTime denNgay,
                              out DataTable theoNgay, out DataTable hoaDon,
                              out int soHoaDon, out decimal tongTien, out decimal giamGia, out decimal doanhThu,
                              out string error)
        {
            theoNgay = new DataTable();
            hoaDon = new DataTable();
            soHoaDon = 0;
            tongTien = 0;
            giamGia = 0;
            doanhThu = 0;
            error = "";

            if (tuNgay.Date > denNgay.Date)
            {
                error = "Từ ngày không được lớn hơn Đến ngày!";
                return false;
            }

            try
            {
                theoNgay = dal.LayDoanhThuTheoNgay(tuNgay, denNgay);
                hoaDon = dal.LayDanhSachHoaDon(tuNgay, denNgay);

                foreach (DataRow r in theoNgay.Rows)
                {
                    soHoaDon += Convert.ToInt32(r["SoHoaDon"]);
                    tongTien += Convert.ToDecimal(r["TongTien"]);
                    giamGia += Convert.ToDecimal(r["GiamGia"]);
                    doanhThu += Convert.ToDecimal(r["DoanhThu"]);
                }
                return true;
            }
            catch (Exception ex)
            {
                error = "Không lấy được dữ liệu báo cáo: " + ex.Message;
                return false;
            }
        }
    }
}
