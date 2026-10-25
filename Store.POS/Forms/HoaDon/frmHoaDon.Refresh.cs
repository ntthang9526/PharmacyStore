using System.Data;
using Store.BLL;
using Store.DAL;
namespace Store.POS;
public partial class frmHoaDon
{

    public void LamMoiDuLieu()
    {
        if(PosFormStyle.IsInDesigner(this)) return;
        int? id=hoaDonDangXem;TimKiem();
        if(id.HasValue)
        {
            var row=dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["MaHD"].Value==id);
            if(row!=null) HienChiTiet(row);
        }
    }
    public void MoHoaDon(int id)
    {
        txtTimKiem.Text="HD"+id.ToString("D5");dtpTuNgay.Checked=dtpDenNgay.Checked=false;TimKiem();
        var row=dgvHoaDon.Rows.Cast<DataGridViewRow>().FirstOrDefault(r=>(int)r.Cells["MaHD"].Value==id);
        if(row!=null) HienChiTiet(row);
    }
}
