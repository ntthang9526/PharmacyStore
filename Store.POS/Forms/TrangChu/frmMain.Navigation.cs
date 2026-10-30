namespace Store.POS;
public partial class frmMain
{
    private void btnHoaDon_Click(object? sender,EventArgs e)=>MoForm<frmHoaDon>();
    private void CapNhatMenu(Form? form=null)
    {
        Button selected=form switch
        {
            null=>btnTrangChu,
            frmBanHang=>btnBanHang,
            frmHoaDon=>btnHoaDon,
            frmQuanLyThuoc=>btnQuanLyThuoc,
            frmNhapKho=>btnNhapKho,
            frmCanhBao=>btnCanhBao,
            frmBaoCao=>btnBaoCao,
            _=>btnQuanLyKhac
        };
        foreach(var button in new[]{btnTrangChu,btnBanHang,btnHoaDon,btnQuanLyThuoc,btnQuanLyKhac,btnNhapKho,btnCanhBao,btnBaoCao})
        {
            bool active=button==selected;
            button.BackColor=active?Color.FromArgb(15,118,110):Color.FromArgb(30,41,59);
            button.FlatAppearance.MouseOverBackColor=active?Color.FromArgb(13,148,136):Color.FromArgb(51,65,85);
        }
        lblTitle.Text="NHÀ THUỐC  /  "+(form==null?"Tổng quan":form.Text.Split('|')[0].Trim());
    }
}

