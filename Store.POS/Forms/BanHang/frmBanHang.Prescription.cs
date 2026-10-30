using Store.Shared;
using Store.POS.FormBanHang;
namespace Store.POS;
public partial class frmBanHang
{
    private DonThuocInfo? donThuoc;
    private (int Id,int Quantity,string Dosage)[] thuocKeDonTruoc=Array.Empty<(int,int,string)>();
    private (int Id,int Quantity,string Dosage)[] ThuocKeDonHienTai()=>gioHang.Where(r=>r.IsPrescription).OrderBy(r=>r.Id).Select(r=>(r.Id,r.Quantity,r.HuongDan)).ToArray();
    private void GhiNhanThuocKeDon()=>thuocKeDonTruoc=ThuocKeDonHienTai();
    private void KiemTraThayDoiThuocKeDon()
    {
        var current=ThuocKeDonHienTai();
        if(!current.SequenceEqual(thuocKeDonTruoc) && donThuoc!=null) donThuoc.DaKiemTra=false;
        thuocKeDonTruoc=current;
        if(gioHang.Count==0) donThuoc=null;
        CapNhatNutDonThuoc();
    }
    private bool NhapDonThuoc()
    {
        if(!gioHang.Any(r=>r.IsPrescription)) {MessageBox.Show(this,"Giỏ hàng chưa có thuốc kê đơn.","Đơn thuốc");return false;}
        using var dialog=new frmDonThuoc(donThuoc,gioHang);
        if(dialog.ShowDialog(this)!=DialogResult.OK) return false;
        foreach(var row in gioHang) if(dialog.HuongDan.TryGetValue(row.Id,out string? dosage)) row.HuongDan=dosage;
        gioHang.ResetBindings();donThuoc=dialog.DonThuoc;GhiNhanThuocKeDon();CapNhatNutDonThuoc();return true;
    }
    private bool ChuanBiDonThuoc()
    {
        if(!gioHang.Any(r=>r.IsPrescription)) return true;
        if(donThuoc?.KiemTra()==null && donThuoc!=null && gioHang.Where(r=>r.IsPrescription).All(r=>!string.IsNullOrWhiteSpace(r.HuongDan) && r.HuongDan.Length<=500)) return true;
        if(!NhapDonThuoc()) return false;
        if(donThuoc?.KiemTra()==null && donThuoc!=null && gioHang.Where(r=>r.IsPrescription).All(r=>!string.IsNullOrWhiteSpace(r.HuongDan) && r.HuongDan.Length<=500)) return true;
        MessageBox.Show(this,"Đã giữ thông tin nháp. Cần nhập đủ và xác nhận kiểm tra đơn thuốc trước khi thanh toán.","Đơn thuốc chưa kiểm tra");
        return false;
    }
    private void CapNhatNutDonThuoc()
    {
        bool required=gioHang.Any(r=>r.IsPrescription);
        btnDonThuoc.Enabled=required;
        btnDonThuoc.Text=required && donThuoc?.DaKiemTra!=true?"Đơn thuốc *":"Đơn thuốc";
    }
    private void btnDonThuoc_Click(object? sender,EventArgs e)=>NhapDonThuoc();
}
