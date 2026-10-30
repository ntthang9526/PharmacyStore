using Store.POS.Common;
using Store.POS.FormBanHang;
using Store.BLL;
using System.Data;
namespace Store.POS;
public partial class frmHoaDon
{
    private void InHoaDon()
    {
        if(!hoaDonDangXem.HasValue) return;
        try {PdfInvoiceHelper.XuatHoaDon(bll.LayBanIn(hoaDonDangXem.Value),this);}
        catch(Exception ex) {MessageBox.Show(this,"Không xuất được hóa đơn: "+ex.Message,"In / PDF");}
    }
    private void XemDonThuoc()
    {
        var row=DongDangXem();
        if(row==null || row.IsNull("PrescriptionNumber") || dgvChiTiet.DataSource is not DataTable details) return;
        var items=details.Rows.Cast<DataRow>().Where(r=>Convert.ToBoolean(r["KeDon"]))
            .Select((r,index)=>new RowData {Id=index+1,Name=(string)r["TenThuoc"]+" · Lô "+r["SoLo"],IsPrescription=true,HuongDan=(string)r["HuongDan"]});
        using var dialog=new frmDonThuoc(HoaDonBLL.DocDonThuoc(row),items,true);dialog.ShowDialog(this);
    }
}
