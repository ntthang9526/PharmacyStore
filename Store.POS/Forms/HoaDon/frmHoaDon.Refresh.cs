namespace Store.POS;
public partial class frmHoaDon
{
 public async void LamMoiDuLieu()=>await LamMoiDuLieuAsync();
 public Task<bool> LamMoiDuLieuAsync()=>TimKiemAsync();
 public void MoHoaDon(int id){hoaDonDangXem=id;chkChiCho.Checked=false;txtTimKiem.Text="HD"+id.ToString("D5");dtpTuNgay.Checked=dtpDenNgay.Checked=false;TimKiem();}
}
