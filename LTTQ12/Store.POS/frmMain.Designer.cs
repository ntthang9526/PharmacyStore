namespace Store.POS
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            mnuHeThong = new ToolStripMenuItem();
            mnuDangXuat = new ToolStripMenuItem();
            mnuThoat = new ToolStripMenuItem();
            mnuBanHang = new ToolStripMenuItem();
            mnuQuanLy = new ToolStripMenuItem();
            mnuQuanLyThuoc = new ToolStripMenuItem();
            mnuNhomThuoc = new ToolStripMenuItem();
            mnuNhaCungCap = new ToolStripMenuItem();
            mnuKhachHang = new ToolStripMenuItem();
            mnuKho = new ToolStripMenuItem();
            mnuNhapKho = new ToolStripMenuItem();
            mnuCanhBao = new ToolStripMenuItem();
            mnuBaoCao = new ToolStripMenuItem();
            mnuBaoCaoDoanhThu = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblNguoiDung = new ToolStripStatusLabel();
            lblNgay = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuBanHang, mnuQuanLy, mnuKho, mnuBaoCao });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1184, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDangXuat, mnuThoat });
            mnuHeThong.Name = "mnuHeThong";
            mnuHeThong.Size = new Size(73, 20);
            mnuHeThong.Text = "Hệ thống";
            // 
            // mnuDangXuat
            // 
            mnuDangXuat.Name = "mnuDangXuat";
            mnuDangXuat.Size = new Size(140, 22);
            mnuDangXuat.Text = "Đăng xuất";
            mnuDangXuat.Click += mnuDangXuat_Click;
            // 
            // mnuThoat
            // 
            mnuThoat.Name = "mnuThoat";
            mnuThoat.Size = new Size(140, 22);
            mnuThoat.Text = "Thoát";
            mnuThoat.Click += mnuThoat_Click;
            // 
            // mnuBanHang
            // 
            mnuBanHang.Name = "mnuBanHang";
            mnuBanHang.ShortcutKeys = Keys.F2;
            mnuBanHang.Size = new Size(95, 20);
            mnuBanHang.Text = "Bán hàng (F2)";
            mnuBanHang.Click += mnuBanHang_Click;
            // 
            // mnuQuanLy
            // 
            mnuQuanLy.DropDownItems.AddRange(new ToolStripItem[] { mnuQuanLyThuoc, mnuNhomThuoc, mnuNhaCungCap, mnuKhachHang });
            mnuQuanLy.Name = "mnuQuanLy";
            mnuQuanLy.Size = new Size(60, 20);
            mnuQuanLy.Text = "Quản lý";
            // 
            // mnuQuanLyThuoc
            // 
            mnuQuanLyThuoc.Name = "mnuQuanLyThuoc";
            mnuQuanLyThuoc.Size = new Size(160, 22);
            mnuQuanLyThuoc.Text = "Thuốc";
            mnuQuanLyThuoc.Click += mnuQuanLyThuoc_Click;
            // 
            // mnuNhomThuoc
            // 
            mnuNhomThuoc.Name = "mnuNhomThuoc";
            mnuNhomThuoc.Size = new Size(160, 22);
            mnuNhomThuoc.Text = "Nhóm thuốc";
            mnuNhomThuoc.Click += mnuNhomThuoc_Click;
            // 
            // mnuNhaCungCap
            // 
            mnuNhaCungCap.Name = "mnuNhaCungCap";
            mnuNhaCungCap.Size = new Size(160, 22);
            mnuNhaCungCap.Text = "Nhà cung cấp";
            mnuNhaCungCap.Click += mnuNhaCungCap_Click;
            // 
            // mnuKhachHang
            // 
            mnuKhachHang.Name = "mnuKhachHang";
            mnuKhachHang.Size = new Size(160, 22);
            mnuKhachHang.Text = "Khách hàng";
            mnuKhachHang.Click += mnuKhachHang_Click;
            // 
            // mnuKho
            // 
            mnuKho.DropDownItems.AddRange(new ToolStripItem[] { mnuNhapKho, mnuCanhBao });
            mnuKho.Name = "mnuKho";
            mnuKho.Size = new Size(38, 20);
            mnuKho.Text = "Kho";
            // 
            // mnuNhapKho
            // 
            mnuNhapKho.Name = "mnuNhapKho";
            mnuNhapKho.Size = new Size(190, 22);
            mnuNhapKho.Text = "Nhập kho";
            mnuNhapKho.Click += mnuNhapKho_Click;
            // 
            // mnuCanhBao
            // 
            mnuCanhBao.Name = "mnuCanhBao";
            mnuCanhBao.Size = new Size(190, 22);
            mnuCanhBao.Text = "Cảnh báo tồn kho / hạn dùng";
            mnuCanhBao.Click += mnuCanhBao_Click;
            // 
            // mnuBaoCao
            // 
            mnuBaoCao.DropDownItems.AddRange(new ToolStripItem[] { mnuBaoCaoDoanhThu });
            mnuBaoCao.Name = "mnuBaoCao";
            mnuBaoCao.Size = new Size(60, 20);
            mnuBaoCao.Text = "Báo cáo";
            // 
            // mnuBaoCaoDoanhThu
            // 
            mnuBaoCaoDoanhThu.Name = "mnuBaoCaoDoanhThu";
            mnuBaoCaoDoanhThu.Size = new Size(190, 22);
            mnuBaoCaoDoanhThu.Text = "Báo cáo doanh thu";
            mnuBaoCaoDoanhThu.Click += mnuBaoCaoDoanhThu_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblNguoiDung, lblNgay });
            statusStrip1.Location = new Point(0, 639);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1184, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblNguoiDung
            // 
            lblNguoiDung.Name = "lblNguoiDung";
            lblNguoiDung.Size = new Size(1100, 17);
            lblNguoiDung.Spring = true;
            lblNguoiDung.Text = "Nhân viên:";
            lblNguoiDung.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNgay
            // 
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(69, 17);
            lblNgay.Text = "dd/MM/yyyy";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 661);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PharmacyStore - Hệ thống quản lý hiệu thuốc";
            WindowState = FormWindowState.Maximized;
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuHeThong;
        private ToolStripMenuItem mnuDangXuat;
        private ToolStripMenuItem mnuThoat;
        private ToolStripMenuItem mnuBanHang;
        private ToolStripMenuItem mnuQuanLy;
        private ToolStripMenuItem mnuQuanLyThuoc;
        private ToolStripMenuItem mnuNhomThuoc;
        private ToolStripMenuItem mnuNhaCungCap;
        private ToolStripMenuItem mnuKhachHang;
        private ToolStripMenuItem mnuKho;
        private ToolStripMenuItem mnuNhapKho;
        private ToolStripMenuItem mnuCanhBao;
        private ToolStripMenuItem mnuBaoCao;
        private ToolStripMenuItem mnuBaoCaoDoanhThu;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblNguoiDung;
        private ToolStripStatusLabel lblNgay;
    }
}
