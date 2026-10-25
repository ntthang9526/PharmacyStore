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

            if (denNgay.Date == DateTime.MaxValue.Date)
            {
                error = "Đến ngày phải nhỏ hơn 31/12/9999.";
                return false;
            }

            try
            {
                hoaDon = dal.LayDanhSachHoaDon(tuNgay, denNgay);
                theoNgay.Columns.Add("Ngay", typeof(DateTime));
                theoNgay.Columns.Add("SoHoaDon", typeof(int));
                theoNgay.Columns.Add("TongTien", typeof(decimal));
                theoNgay.Columns.Add("GiamGia", typeof(decimal));
                theoNgay.Columns.Add("DoanhThu", typeof(decimal));
                foreach (var ngay in hoaDon.AsEnumerable().GroupBy(r => r.Field<DateTime>("NgayLap").Date).OrderBy(g => g.Key))
                {
                    decimal tien = ngay.Sum(r => r.Field<decimal>("TongTien"));
                    decimal giam = ngay.Sum(r => r.Field<decimal>("GiamGia"));
                    decimal thu = ngay.Sum(r => r.Field<decimal>("ThucThu"));
                    theoNgay.Rows.Add(ngay.Key, ngay.Count(), tien, giam, thu);
                    soHoaDon += ngay.Count();
                    tongTien += tien;
                    giamGia += giam;
                    doanhThu += thu;
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
