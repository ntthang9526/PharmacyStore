using Store.BLL;
using System.Data;
namespace Store.POS;
public partial class frmMain
{

    private void ConfigureDashboard()
    {
        PosFormStyle.ConfigureGridSelection(this, dashboard, null);
        StyleMainLayout();
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

    private void StyleMainLayout()
    {
        if (PosFormStyle.IsInDesigner(this)) return;

        // 1. Header & Thanh tiêu đề
        lblTitle.BackColor = Color.FromArgb(15, 118, 110); // Teal 700
        lblTitle.ForeColor = Color.White;
        lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

        // Nút Trang chủ & Cửa sổ
        var topButtons = new[] { btnTrangChu, btnCuaSo };
        foreach (var b in topButtons)
        {
            if (b == null) continue;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Color.FromArgb(204, 251, 241); // Teal 100
            b.ForeColor = Color.FromArgb(15, 118, 110);  // Teal 700
            b.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 246, 228);
        }

        // 2. Sidebar Menu
        panelChucNang.BackColor = Color.FromArgb(15, 23, 42); // Slate 900 cao cấp
        var menuButtons = new[] { btnBanHang, btnQuanLyThuoc, btnQuanLyKhac, btnNhapKho, btnCanhBao, btnBaoCao };
        foreach (var btn in menuButtons)
        {
            if (btn == null) continue;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(30, 41, 59); // Slate 800
            btn.ForeColor = Color.FromArgb(241, 245, 249);
            btn.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 118, 110); // Teal 700
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(13, 148, 136);
        }

        // 3. Dashboard background & Title
        dashboard.BackColor = Color.FromArgb(248, 250, 252);
        lblTieuDeTongQuan.BackColor = Color.Transparent;
        lblTieuDeTongQuan.ForeColor = Color.FromArgb(15, 23, 42);
        lblTieuDeTongQuan.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

        btnLamMoiTongQuan.FlatStyle = FlatStyle.Flat;
        btnLamMoiTongQuan.FlatAppearance.BorderSize = 1;
        btnLamMoiTongQuan.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        btnLamMoiTongQuan.BackColor = Color.White;
        btnLamMoiTongQuan.ForeColor = Color.FromArgb(15, 118, 110);
        btnLamMoiTongQuan.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        btnLamMoiTongQuan.Cursor = Cursors.Hand;

        // 4. Style 4 thẻ KPI thống kê
        void StyleCard(TableLayoutPanel card, Label title, Label value, Label sub, Color bg, Color textCol, Color valCol)
        {
            card.BackColor = bg;
            card.Cursor = Cursors.Hand;
            title.BackColor = Color.Transparent;
            title.ForeColor = textCol;
            title.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            title.Cursor = Cursors.Hand;

            value.BackColor = Color.Transparent;
            value.ForeColor = valCol;
            value.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            value.Cursor = Cursors.Hand;

            sub.BackColor = Color.Transparent;
            sub.ForeColor = Color.FromArgb(100, 116, 139);
            sub.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            sub.Cursor = Cursors.Hand;
        }

        // Thẻ 1: Doanh thu (Emerald)
        StyleCard(tableLayoutPanel5, label6, lblDoanhThu, label7,
            Color.FromArgb(240, 253, 244), Color.FromArgb(21, 128, 61), Color.FromArgb(4, 120, 87));

        // Thẻ 2: Hóa đơn (Blue)
        StyleCard(tableLayoutPanel8, label9, lblSoHoaDon, label10,
            Color.FromArgb(239, 246, 255), Color.FromArgb(29, 78, 216), Color.FromArgb(30, 64, 175));

        // Thẻ 3: Thuốc sắp hết (Amber)
        StyleCard(tableLayoutPanel11, label12, lblSapHet, label13,
            Color.FromArgb(254, 243, 199), Color.FromArgb(180, 83, 9), Color.FromArgb(180, 83, 9));

        // Thẻ 4: Thuốc sắp hết hạn (Rose)
        StyleCard(tableLayoutPanel14, label15, lblSapHetHan, label16,
            Color.FromArgb(255, 241, 242), Color.FromArgb(190, 18, 60), Color.FromArgb(190, 18, 60));

        // 5. Style 3 DataGridView trên Dashboard
        PosFormStyle.StyleGrid(dgvSapHet);
        PosFormStyle.StyleGrid(dgvSapHetHan);
        PosFormStyle.StyleGrid(dgvGanNhat);
    }

    private void TaiTongQuan()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        lblTieuDeTongQuan.Text = "Tổng quan · " + DateTime.Today.ToString("dd/MM/yyyy");
        try
        {
            var bills=new HoaDonBLL();var alerts=new CanhBaoBLL();
            var summary=bills.ThongKeHomNay().Rows[0];var low=alerts.LayThuocSapHet();var expiry=alerts.LayLoSapHetHan();var recent=bills.LayGanNhat();
            lblDoanhThu.Text=$"{summary["DoanhThu"]:N0} đ";lblSoHoaDon.Text=summary["SoHoaDon"].ToString();
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
