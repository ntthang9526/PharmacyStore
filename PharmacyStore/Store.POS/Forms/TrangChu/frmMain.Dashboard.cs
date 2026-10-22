using Store.BLL;
using System.Data;
namespace Store.POS;
public partial class frmMain
{

    private void ConfigureDashboard()
    {
        PosFormStyle.ConfigureGridSelection(this, dashboard, null);
        btnLamMoiTongQuan.Click += (_, _) => TaiTongQuan();
        void Wire(Control c, Action action)
        {
            c.Click += (_, _) => action();
            foreach (Control child in c.Controls) Wire(child, action);
        }
        Wire(lblDoanhThu.Parent!, () => MoForm<frmBaoCao>());
        Wire(lblSoHoaDon.Parent!, () => MoForm<frmHoaDon>());
        Wire(lblSapHet.Parent!, () => MoForm<frmCanhBao>());
        Wire(lblSapHetHan.Parent!, () => MoForm<frmCanhBao>());
        dgvSapHet.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) MoForm<frmCanhBao>(); };
        dgvSapHetHan.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) MoForm<frmCanhBao>(); };
        dgvGanNhat.CellDoubleClick+=(_,e)=>
        {
            if(e.RowIndex<0) return;
            using var dialog=new frmHoaDon(Convert.ToInt32(dgvGanNhat.Rows[e.RowIndex].Cells["MaHD"].Value));
            dialog.ShowDialog(this);TaiTongQuan();
        };
        Shown+=(_,_)=>TaiTongQuan();
    }

    private void TaiTongQuan()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        lblTieuDeTongQuan.Text = "Tổng quan · " + DateTime.Today.ToString("dd/MM/yyyy");
        try
        {
            var bills=new HoaDonBLL();var alerts=new CanhBaoBLL();
            var summary=bills.ThongKeHomNay().Rows[0];var low=alerts.LayThuocSapHet();var expiry=alerts.LayLoSapHetHan();var recent=bills.LayGanNhat();
            lblDoanhThu.Text=$"{summary["DoanhThu"]:#,##0.##} đ";lblSoHoaDon.Text=summary["SoHoaDon"].ToString();
            lblSapHet.Text=low.Rows.Count.ToString();lblSapHetHan.Text=expiry.Rows.Count.ToString();
            dgvSapHet.DataSource=low;dgvSapHetHan.DataSource=expiry;dgvGanNhat.DataSource=recent;
            dgvSapHet.Visible=low.Rows.Count>0;lblKhongSapHet.Visible=!dgvSapHet.Visible;
            dgvSapHetHan.Visible=expiry.Rows.Count>0;lblKhongSapHetHan.Visible=!dgvSapHetHan.Visible;
            dgvGanNhat.Visible=recent.Rows.Count>0;lblKhongHoaDon.Visible=!dgvGanNhat.Visible;
            lblTongQuan.ForeColor=Color.DimGray;
            lblTongQuan.Text=$"{(string.IsNullOrWhiteSpace(PhienDangNhap.HoTen)?"Nhân viên":PhienDangNhap.HoTen)} · Cập nhật lúc {DateTime.Now:HH:mm:ss}";
        }
        catch(Exception ex) {lblTongQuan.ForeColor=Color.Firebrick;lblTongQuan.Text="Chưa tải được tổng quan. Bấm Làm mới để thử lại. "+ex.Message;}
    }
}
