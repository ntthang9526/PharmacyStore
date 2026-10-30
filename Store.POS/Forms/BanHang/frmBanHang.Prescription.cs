using Store.Shared;
using Store.POS.FormBanHang;
namespace Store.POS;
public partial class frmBanHang
{
    private DonThuocInfo? donThuoc;
    private bool NhapDonThuoc()
    {
        if(!gioHang.Any(r=>r.IsPrescription)) {MessageBox.Show(this,"Giỏ hàng chưa có thuốc kê đơn.","Đơn thuốc");return false;}
        using var dialog=new frmDonThuoc(donThuoc,gioHang);
        if(dialog.ShowDialog(this)!=DialogResult.OK) return false;
        donThuoc=dialog.DonThuoc;
        foreach(var row in gioHang) if(dialog.HuongDan.TryGetValue(row.Id,out string? dosage)) row.HuongDan=dosage;
        gioHang.ResetBindings();CapNhatNutDonThuoc();return true;
    }
    private bool ChuanBiDonThuoc()
    {
        if(!gioHang.Any(r=>r.IsPrescription)) return true;
        if(donThuoc?.KiemTra()==null && donThuoc!=null && gioHang.Where(r=>r.IsPrescription).All(r=>!string.IsNullOrWhiteSpace(r.HuongDan) && r.HuongDan.Length<=500)) return true;
        return NhapDonThuoc();
    }
    private void CapNhatNutDonThuoc()
    {
        bool required=gioHang.Any(r=>r.IsPrescription);
        btnDonThuoc.Enabled=required;
        btnDonThuoc.Text=required && donThuoc==null?"Đơn thuốc *":"Đơn thuốc";
    }
    private void btnDonThuoc_Click(object? sender,EventArgs e)=>NhapDonThuoc();
}
