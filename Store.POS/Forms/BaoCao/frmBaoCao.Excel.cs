using System.Data;
using Store.POS.Common;
namespace Store.POS;
public partial class frmBaoCao
{
    private void XuatExcel()
    {
        try
        {
            if(dgvTheoNgay.DataSource is not DataTable daily || dgvHoaDon.DataSource is not DataTable orders) return;
            if(!bll.LayBaoCao(dtpTuNgay.Value,dtpDenNgay.Value,out daily,out orders,out int count,out decimal total,out decimal discount,out decimal revenue,out string error))
                throw new InvalidOperationException(error);
            ExcelHelper.XuatBaoCaoDoanhThu(dtpTuNgay.Value,dtpDenNgay.Value,count,total,discount,revenue,daily,orders);
        }
        catch(Exception ex){MessageBox.Show(this,ex.Message,"Xuất Excel");}
    }
}
