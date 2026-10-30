namespace Store.POS;
public partial class frmHoaDon
{
 public void LamMoiDuLieu(){if(!PosFormStyle.IsInDesigner(this)) TimKiem();}
 public void MoHoaDon(int id){hoaDonDangXem=id;chkChiCho.Checked=false;txtTimKiem.Text="HD"+id.ToString("D5");dtpTuNgay.Checked=dtpDenNgay.Checked=false;TimKiem();}
}
