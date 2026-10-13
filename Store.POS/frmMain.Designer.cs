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
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenuBar = new Panel();
            btnTrangChu = new Button();
            panelTieuDe = new Panel();
            lblTitle = new Label();
            panelChucNang = new Panel();
            btnBaoCao = new Button();
            btnCanhBao = new Button();
            btnNhapKho = new Button();
            btnQuanLyKhac = new Button();
            btnQuanLyThuoc = new Button();
            btnBanHang = new Button();
            panelMenuBar.SuspendLayout();
            panelTieuDe.SuspendLayout();
            panelChucNang.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenuBar
            // 
            panelMenuBar.Anchor = AnchorStyles.None;
            panelMenuBar.BackColor = SystemColors.ControlLight;
            panelMenuBar.Controls.Add(btnTrangChu);
            panelMenuBar.Location = new Point(0, 0);
            panelMenuBar.Name = "panelMenuBar";
            panelMenuBar.Size = new Size(1383, 35);
            panelMenuBar.TabIndex = 1;
            // 
            // btnTrangChu
            // 
            btnTrangChu.BackColor = SystemColors.ControlLight;
            btnTrangChu.BackgroundImageLayout = ImageLayout.None;
            btnTrangChu.Cursor = Cursors.Hand;
            btnTrangChu.FlatAppearance.BorderSize = 0;
            btnTrangChu.FlatAppearance.MouseDownBackColor = SystemColors.ActiveBorder;
            btnTrangChu.FlatStyle = FlatStyle.Flat;
            btnTrangChu.Font = new Font("Segoe UI", 10.2F);
            btnTrangChu.Location = new Point(0, 0);
            btnTrangChu.Margin = new Padding(0);
            btnTrangChu.Name = "btnTrangChu";
            btnTrangChu.Size = new Size(107, 35);
            btnTrangChu.TabIndex = 0;
            btnTrangChu.Text = "Trang Chủ";
            btnTrangChu.UseVisualStyleBackColor = false;
            btnTrangChu.Click += btnTrangChu_Click;
            // 
            // panelTieuDe
            // 
            panelTieuDe.Anchor = AnchorStyles.None;
            panelTieuDe.BackColor = SystemColors.MenuHighlight;
            panelTieuDe.Controls.Add(lblTitle);
            panelTieuDe.Location = new Point(0, 35);
            panelTieuDe.Name = "panelTieuDe";
            panelTieuDe.Size = new Size(1383, 60);
            panelTieuDe.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(466, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(500, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HỆ THỐNG QUẢN LÍ NHÀ THUỐC";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelChucNang
            // 
            panelChucNang.Anchor = AnchorStyles.Left;
            panelChucNang.BackColor = SystemColors.HotTrack;
            panelChucNang.Controls.Add(btnBaoCao);
            panelChucNang.Controls.Add(btnCanhBao);
            panelChucNang.Controls.Add(btnNhapKho);
            panelChucNang.Controls.Add(btnQuanLyKhac);
            panelChucNang.Controls.Add(btnQuanLyThuoc);
            panelChucNang.Controls.Add(btnBanHang);
            panelChucNang.Location = new Point(0, 95);
            panelChucNang.Name = "panelChucNang";
            panelChucNang.Size = new Size(220, 660);
            panelChucNang.TabIndex = 3;
            // 
            // btnBaoCao
            // 
            btnBaoCao.Anchor = AnchorStyles.None;
            btnBaoCao.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBaoCao.Location = new Point(20, 370);
            btnBaoCao.Name = "btnBaoCao";
            btnBaoCao.Size = new Size(180, 50);
            btnBaoCao.TabIndex = 7;
            btnBaoCao.Text = "Báo cáo (F6)";
            btnBaoCao.UseVisualStyleBackColor = true;
            btnBaoCao.Click += btnBaoCao_Click;
            // 
            // btnCanhBao
            // 
            btnCanhBao.Anchor = AnchorStyles.None;
            btnCanhBao.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCanhBao.Location = new Point(20, 300);
            btnCanhBao.Name = "btnCanhBao";
            btnCanhBao.Size = new Size(180, 50);
            btnCanhBao.TabIndex = 6;
            btnCanhBao.Text = "Cảnh báo (F5)";
            btnCanhBao.UseVisualStyleBackColor = true;
            btnCanhBao.Click += btnCanhBao_Click;
            // 
            // btnNhapKho
            // 
            btnNhapKho.Anchor = AnchorStyles.None;
            btnNhapKho.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNhapKho.Location = new Point(20, 230);
            btnNhapKho.Name = "btnNhapKho";
            btnNhapKho.Size = new Size(180, 50);
            btnNhapKho.TabIndex = 5;
            btnNhapKho.Text = "Nhập kho (F4)";
            btnNhapKho.UseVisualStyleBackColor = true;
            btnNhapKho.Click += btnNhapKho_Click;
            // 
            // btnQuanLyKhac
            // 
            btnQuanLyKhac.Anchor = AnchorStyles.None;
            btnQuanLyKhac.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuanLyKhac.Location = new Point(20, 160);
            btnQuanLyKhac.Name = "btnQuanLyKhac";
            btnQuanLyKhac.Size = new Size(180, 50);
            btnQuanLyKhac.TabIndex = 4;
            btnQuanLyKhac.Text = "Quản lý khác (F3)";
            btnQuanLyKhac.UseVisualStyleBackColor = true;
            btnQuanLyKhac.Click += btnQuanLyKhac_Click;
            // 
            // btnQuanLyThuoc
            // 
            btnQuanLyThuoc.Anchor = AnchorStyles.None;
            btnQuanLyThuoc.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuanLyThuoc.Location = new Point(20, 90);
            btnQuanLyThuoc.Name = "btnQuanLyThuoc";
            btnQuanLyThuoc.Size = new Size(180, 50);
            btnQuanLyThuoc.TabIndex = 1;
            btnQuanLyThuoc.Text = "Quản lý thuốc (F2)";
            btnQuanLyThuoc.UseVisualStyleBackColor = true;
            btnQuanLyThuoc.Click += btnQuanLyThuoc_Click;
            // 
            // btnBanHang
            // 
            btnBanHang.Anchor = AnchorStyles.None;
            btnBanHang.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBanHang.Location = new Point(20, 20);
            btnBanHang.Name = "btnBanHang";
            btnBanHang.Size = new Size(180, 50);
            btnBanHang.TabIndex = 0;
            btnBanHang.Text = "Bán hàng (F1)";
            btnBanHang.UseVisualStyleBackColor = true;
            btnBanHang.Click += btnBanHang_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 753);
            Controls.Add(panelChucNang);
            Controls.Add(panelTieuDe);
            Controls.Add(panelMenuBar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ HIỆU THUỐC";
            KeyDown += frmMain_KeyDown;
            panelMenuBar.ResumeLayout(false);
            panelTieuDe.ResumeLayout(false);
            panelChucNang.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenuBar;
        private Button btnTrangChu;
        private Panel panelTieuDe;
        private Label lblTitle;
        private Panel panelChucNang;
        private Button btnBanHang;
        private Button btnQuanLyThuoc;
        private Button btnBaoCao;
        private Button btnCanhBao;
        private Button btnNhapKho;
        private Button btnQuanLyKhac;
    }
}