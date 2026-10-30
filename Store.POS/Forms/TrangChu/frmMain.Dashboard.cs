using Store.BLL;
using System.Data;
namespace Store.POS;
public partial class frmMain
{

    private void ConfigureDashboard()
    {
        PosFormStyle.ConfigureGridSelection(this, dashboard, null);
        btnLamMoiTongQuan.Click += (_, _) => DongBoDuLieu(true);
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
            int id=Convert.ToInt32(dgvGanNhat.Rows[e.RowIndex].Cells["MaHD"].Value);
            MoCuaSo(typeof(frmHoaDon),()=>new frmHoaDon(id));
            if(cuaSoDangMo.TryGetValue(typeof(frmHoaDon),out var form)) ((frmHoaDon)form).MoHoaDon(id);
        };
        Shown+=(_,_)=>TaiTongQuan();
    }

    private void TaiTongQuan()
    {
        if (PosFormStyle.IsInDesigner(this) || !dashboard.Visible) return;
        lblTieuDeTongQuan.Text = "Tổng quan · " + DateTime.Today.ToString("dd/MM/yyyy");
        try
        {
            var bills=new HoaDonBLL();var alerts=new CanhBaoBLL();
            var summary=bills.ThongKeHomNay().Rows[0];var low=alerts.LayThuocSapHet(CanhBaoSettings.NguongTon);var expiry=alerts.LayLoSapHetHan();var recent=bills.LayGanNhat();
            lblDoanhThu.Text=$"{summary["DoanhThu"]:N0} đ";lblSoHoaDon.Text=summary["SoHoaDon"].ToString();
            label13.Text=$"Có thể bán dưới {CanhBaoSettings.NguongTon}";
            lblSapHet.Text=low.Rows.Count.ToString();
            // Đã xem chỉ là nhắc việc trong phiên; các lô vẫn được tính vào cảnh báo.
            lblSapHetHan.Text=expiry.Rows.Count.ToString();
            dgvSapHet.DataSource=low;dgvSapHetHan.DataSource=expiry;dgvGanNhat.DataSource=recent;
            dgvSapHet.Visible=low.Rows.Count>0;lblKhongSapHet.Visible=!dgvSapHet.Visible;
            // Tô xám các hàng đã xem trong dgvSapHetHan trang chủ
            foreach(DataGridViewRow r in dgvSapHetHan.Rows){if(r.IsNewRow)continue;int ma=Convert.ToInt32(r.Cells["MaThuoc"].Value);string lo=r.Cells["SoLo"].Value?.ToString()??"";bool dxl=frmCanhBao.LoXuLyDaXuLy.Contains(ma+"|"+lo);r.DefaultCellStyle.ForeColor=dxl?Color.Gray:Color.FromArgb(15,23,42);r.DefaultCellStyle.BackColor=dxl?Color.LightGray:(Convert.ToInt32(r.Cells["SoNgayConLai"].Value)<0?Color.LightCoral:Color.LemonChiffon);}
            dgvSapHetHan.Visible=expiry.Rows.Count>0;lblKhongSapHetHan.Visible=!dgvSapHetHan.Visible;
            dgvGanNhat.Visible=recent.Rows.Count>0;lblKhongHoaDon.Visible=!dgvGanNhat.Visible;
            lblTongQuan.ForeColor=Color.DimGray;
            lblTongQuan.Text=$"{(string.IsNullOrWhiteSpace(PhienDangNhap.HoTen)?"Nhân viên":PhienDangNhap.HoTen)} · {(PhienDangNhap.LaAdmin?"Admin":"Nhân viên")} · Cập nhật lúc {DateTime.Now:HH:mm:ss}";
        }
        catch(Exception ex) {lblTongQuan.ForeColor=Color.Firebrick;lblTongQuan.Text="Chưa tải được tổng quan. Bấm Làm mới để thử lại. "+ex.Message;}
    }
}
