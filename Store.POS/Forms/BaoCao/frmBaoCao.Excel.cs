using System.Data;
using Store.POS.Common;
namespace Store.POS;
public partial class frmBaoCao
{
    private async void XuatExcel()
    {
        try
        {
            if(!await XemBaoCaoAsync(false)) return;
            var daily=(DataTable)dgvTheoNgay.DataSource!;
            var orders=(DataTable)dgvHoaDon.DataSource!;
            int count=orders.Rows.Count;
            decimal total=orders.AsEnumerable().Sum(r=>r.Field<decimal>("TongTien"));
            decimal discount=orders.AsEnumerable().Sum(r=>r.Field<decimal>("GiamGia"));
            decimal revenue=orders.AsEnumerable().Sum(r=>r.Field<decimal>("ThucThu"));
            ExcelHelper.XuatBaoCaoDoanhThu(dtpTuNgay.Value,dtpDenNgay.Value,count,total,discount,revenue,daily,orders);
        }
        catch(Exception ex){MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
}
