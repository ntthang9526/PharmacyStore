#nullable disable
namespace Store.POS;

partial class frmMain
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
        shellLayout = new TableLayoutPanel();
        panelChucNang = new Panel();
        sideButtons = new TableLayoutPanel();
        btnTrangChu = new Button();
        btnBanHang = new Button();
        btnQuanLyThuoc = new Button();
        btnQuanLyKhac = new Button();
        btnNhapKho = new Button();
        btnCanhBao = new Button();
        btnBaoCao = new Button();
        lblTitle = new Label();
        contentHost = new Panel();
        dashboard = new TableLayoutPanel();
        tableLayoutPanel1 = new TableLayoutPanel();
        lblTieuDeTongQuan = new Label();
        tableLayoutPanel3 = new TableLayoutPanel();
        btnLamMoiTongQuan = new Button();
        tableLayoutPanel4 = new TableLayoutPanel();
        tableLayoutPanel5 = new TableLayoutPanel();
        label6 = new Label();
        lblDoanhThu = new Label();
        label7 = new Label();
        tableLayoutPanel8 = new TableLayoutPanel();
        label9 = new Label();
        lblSoHoaDon = new Label();
        label10 = new Label();
        tableLayoutPanel11 = new TableLayoutPanel();
        label12 = new Label();
        lblSapHet = new Label();
        label13 = new Label();
        tableLayoutPanel14 = new TableLayoutPanel();
        label15 = new Label();
        lblSapHetHan = new Label();
        label16 = new Label();
        tableLayoutPanel17 = new TableLayoutPanel();
        groupBox18 = new GroupBox();
        panel19 = new Panel();
        dgvSapHet = new DataGridView();
        lblKhongSapHet = new Label();
        groupBox20 = new GroupBox();
        panel21 = new Panel();
        dgvSapHetHan = new DataGridView();
        lblKhongSapHetHan = new Label();
        groupBox22 = new GroupBox();
        panel23 = new Panel();
        dgvGanNhat = new DataGridView();
        lblKhongHoaDon = new Label();
        lblTongQuan = new Label();
        shellLayout.SuspendLayout();
        panelChucNang.SuspendLayout();
        sideButtons.SuspendLayout();
        contentHost.SuspendLayout();
        dashboard.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel11.SuspendLayout();
        tableLayoutPanel14.SuspendLayout();
        tableLayoutPanel17.SuspendLayout();
        groupBox18.SuspendLayout();
        panel19.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
        groupBox20.SuspendLayout();
        panel21.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).BeginInit();
        groupBox22.SuspendLayout();
        panel23.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvGanNhat).BeginInit();
        SuspendLayout();
        // 
        // shellLayout
        // 
        shellLayout.ColumnCount = 2;
        shellLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 275F));
        shellLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        shellLayout.Controls.Add(panelChucNang, 0, 0);
        shellLayout.Controls.Add(lblTitle, 1, 0);
        shellLayout.Controls.Add(contentHost, 1, 1);
        shellLayout.Dock = DockStyle.Fill;
        shellLayout.Location = new Point(0, 0);
        shellLayout.Margin = new Padding(0);
        shellLayout.Name = "shellLayout";
        shellLayout.RowCount = 2;
        shellLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
        shellLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        shellLayout.Size = new Size(1875, 1025);
        shellLayout.TabIndex = 0;
        // 
        // panelChucNang
        // 
        panelChucNang.BackColor = Color.FromArgb(15, 23, 42);
        panelChucNang.Controls.Add(sideButtons);
        panelChucNang.Dock = DockStyle.Fill;
        panelChucNang.Font = new Font("Segoe UI", 9F);
        panelChucNang.ForeColor = SystemColors.ControlText;
        panelChucNang.Location = new Point(0, 0);
        panelChucNang.Margin = new Padding(0);
        panelChucNang.Name = "panelChucNang";
        shellLayout.SetRowSpan(panelChucNang, 2);
        panelChucNang.Size = new Size(275, 1025);
        panelChucNang.TabIndex = 3;
        // 
        // sideButtons
        // 
        sideButtons.BackColor = Color.FromArgb(15, 23, 42);
        sideButtons.ColumnCount = 1;
        sideButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        sideButtons.Controls.Add(btnTrangChu, 0, 0);
        sideButtons.Controls.Add(btnBanHang, 0, 1);
        sideButtons.Controls.Add(btnQuanLyThuoc, 0, 2);
        sideButtons.Controls.Add(btnQuanLyKhac, 0, 3);
        sideButtons.Controls.Add(btnNhapKho, 0, 4);
        sideButtons.Controls.Add(btnCanhBao, 0, 5);
        sideButtons.Controls.Add(btnBaoCao, 0, 6);
        sideButtons.Dock = DockStyle.Fill;
        sideButtons.Location = new Point(0, 0);
        sideButtons.Margin = new Padding(4);
        sideButtons.Name = "sideButtons";
        sideButtons.Padding = new Padding(15, 18, 15, 0);
        sideButtons.RowCount = 8;
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        sideButtons.Size = new Size(275, 1025);
        sideButtons.TabIndex = 0;
        // 
        // btnTrangChu
        // 
        btnTrangChu.BackColor = Color.FromArgb(30, 41, 59);
        btnTrangChu.Cursor = Cursors.Hand;
        btnTrangChu.Dock = DockStyle.Fill;
        btnTrangChu.FlatAppearance.BorderSize = 0;
        btnTrangChu.FlatAppearance.MouseDownBackColor = SystemColors.ActiveBorder;
        btnTrangChu.FlatStyle = FlatStyle.Flat;
        btnTrangChu.Font = new Font("Segoe UI", 10.2F);
        btnTrangChu.ForeColor = Color.FromArgb(241, 245, 249);
        btnTrangChu.Location = new Point(15, 24);
        btnTrangChu.Margin = new Padding(0, 6, 0, 6);
        btnTrangChu.Name = "btnTrangChu";
        btnTrangChu.Size = new Size(245, 60);
        btnTrangChu.TabIndex = 0;
        btnTrangChu.Text = "Trang Chủ";
        btnTrangChu.UseVisualStyleBackColor = false;
        btnTrangChu.Click += btnTrangChu_Click;
        // 
        // btnBanHang
        // 
        btnBanHang.BackColor = Color.FromArgb(30, 41, 59);
        btnBanHang.Dock = DockStyle.Fill;
        btnBanHang.FlatAppearance.BorderSize = 0;
        btnBanHang.FlatStyle = FlatStyle.Flat;
        btnBanHang.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnBanHang.ForeColor = Color.FromArgb(241, 245, 249);
        btnBanHang.Location = new Point(15, 96);
        btnBanHang.Margin = new Padding(0, 6, 0, 6);
        btnBanHang.Name = "btnBanHang";
        btnBanHang.Size = new Size(245, 60);
        btnBanHang.TabIndex = 0;
        btnBanHang.Text = "Bán hàng (F1)";
        btnBanHang.UseVisualStyleBackColor = false;
        btnBanHang.Click += btnBanHang_Click;
        // 
        // btnQuanLyThuoc
        // 
        btnQuanLyThuoc.BackColor = Color.FromArgb(30, 41, 59);
        btnQuanLyThuoc.Dock = DockStyle.Fill;
        btnQuanLyThuoc.FlatAppearance.BorderSize = 0;
        btnQuanLyThuoc.FlatStyle = FlatStyle.Flat;
        btnQuanLyThuoc.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnQuanLyThuoc.ForeColor = Color.FromArgb(241, 245, 249);
        btnQuanLyThuoc.Location = new Point(15, 168);
        btnQuanLyThuoc.Margin = new Padding(0, 6, 0, 6);
        btnQuanLyThuoc.Name = "btnQuanLyThuoc";
        btnQuanLyThuoc.Size = new Size(245, 60);
        btnQuanLyThuoc.TabIndex = 1;
        btnQuanLyThuoc.Text = "Quản lý thuốc (F2)";
        btnQuanLyThuoc.UseVisualStyleBackColor = false;
        btnQuanLyThuoc.Click += btnQuanLyThuoc_Click;
        // 
        // btnQuanLyKhac
        // 
        btnQuanLyKhac.BackColor = Color.FromArgb(30, 41, 59);
        btnQuanLyKhac.Dock = DockStyle.Fill;
        btnQuanLyKhac.FlatAppearance.BorderSize = 0;
        btnQuanLyKhac.FlatStyle = FlatStyle.Flat;
        btnQuanLyKhac.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnQuanLyKhac.ForeColor = Color.FromArgb(241, 245, 249);
        btnQuanLyKhac.Location = new Point(15, 240);
        btnQuanLyKhac.Margin = new Padding(0, 6, 0, 6);
        btnQuanLyKhac.Name = "btnQuanLyKhac";
        btnQuanLyKhac.Size = new Size(245, 60);
        btnQuanLyKhac.TabIndex = 4;
        btnQuanLyKhac.Text = "Quản lý khác (F3)";
        btnQuanLyKhac.UseVisualStyleBackColor = false;
        btnQuanLyKhac.Click += btnQuanLyKhac_Click;
        // 
        // btnNhapKho
        // 
        btnNhapKho.BackColor = Color.FromArgb(30, 41, 59);
        btnNhapKho.Dock = DockStyle.Fill;
        btnNhapKho.FlatAppearance.BorderSize = 0;
        btnNhapKho.FlatStyle = FlatStyle.Flat;
        btnNhapKho.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnNhapKho.ForeColor = Color.FromArgb(241, 245, 249);
        btnNhapKho.Location = new Point(15, 312);
        btnNhapKho.Margin = new Padding(0, 6, 0, 6);
        btnNhapKho.Name = "btnNhapKho";
        btnNhapKho.Size = new Size(245, 60);
        btnNhapKho.TabIndex = 5;
        btnNhapKho.Text = "Nhập kho (F4)";
        btnNhapKho.UseVisualStyleBackColor = false;
        btnNhapKho.Click += btnNhapKho_Click;
        // 
        // btnCanhBao
        // 
        btnCanhBao.BackColor = Color.FromArgb(30, 41, 59);
        btnCanhBao.Dock = DockStyle.Fill;
        btnCanhBao.FlatAppearance.BorderSize = 0;
        btnCanhBao.FlatStyle = FlatStyle.Flat;
        btnCanhBao.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnCanhBao.ForeColor = Color.FromArgb(241, 245, 249);
        btnCanhBao.Location = new Point(15, 384);
        btnCanhBao.Margin = new Padding(0, 6, 0, 6);
        btnCanhBao.Name = "btnCanhBao";
        btnCanhBao.Size = new Size(245, 60);
        btnCanhBao.TabIndex = 6;
        btnCanhBao.Text = "Cảnh báo (F5)";
        btnCanhBao.UseVisualStyleBackColor = false;
        btnCanhBao.Click += btnCanhBao_Click;
        // 
        // btnBaoCao
        // 
        btnBaoCao.BackColor = Color.FromArgb(30, 41, 59);
        btnBaoCao.Dock = DockStyle.Fill;
        btnBaoCao.FlatAppearance.BorderSize = 0;
        btnBaoCao.FlatStyle = FlatStyle.Flat;
        btnBaoCao.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnBaoCao.ForeColor = Color.FromArgb(241, 245, 249);
        btnBaoCao.Location = new Point(15, 456);
        btnBaoCao.Margin = new Padding(0, 6, 0, 6);
        btnBaoCao.Name = "btnBaoCao";
        btnBaoCao.Size = new Size(245, 60);
        btnBaoCao.TabIndex = 7;
        btnBaoCao.Text = "Báo cáo (F6)";
        btnBaoCao.UseVisualStyleBackColor = false;
        btnBaoCao.Click += btnBaoCao_Click;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.FromArgb(15, 118, 110);
        lblTitle.Dock = DockStyle.Fill;
        lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(275, 0);
        lblTitle.Margin = new Padding(0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(1600, 75);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "HỆ THỐNG QUẢN LÍ NHÀ THUỐC";
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblTitle.UseCompatibleTextRendering = true;
        // 
        // contentHost
        // 
        contentHost.AutoScroll = true;
        contentHost.Controls.Add(dashboard);
        contentHost.Dock = DockStyle.Fill;
        contentHost.Location = new Point(275, 75);
        contentHost.Margin = new Padding(0);
        contentHost.Name = "contentHost";
        contentHost.Size = new Size(1600, 950);
        contentHost.TabIndex = 4;
        // 
        // dashboard
        // 
        dashboard.BackColor = Color.FromArgb(248, 250, 252);
        dashboard.ColumnCount = 1;
        dashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        dashboard.Controls.Add(tableLayoutPanel1, 0, 0);
        dashboard.Controls.Add(tableLayoutPanel4, 0, 1);
        dashboard.Controls.Add(tableLayoutPanel17, 0, 2);
        dashboard.Controls.Add(groupBox22, 0, 3);
        dashboard.Controls.Add(lblTongQuan, 0, 4);
        dashboard.Dock = DockStyle.Fill;
        dashboard.Font = new Font("Segoe UI", 9F);
        dashboard.ForeColor = SystemColors.ControlText;
        dashboard.Location = new Point(0, 0);
        dashboard.Margin = new Padding(0);
        dashboard.Name = "dashboard";
        dashboard.Padding = new Padding(20, 10, 20, 12);
        dashboard.RowCount = 5;
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 148F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        dashboard.Size = new Size(1600, 950);
        dashboard.TabIndex = 4;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 175F));
        tableLayoutPanel1.Controls.Add(lblTieuDeTongQuan, 0, 0);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
        tableLayoutPanel1.Location = new Point(20, 10);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1560, 60);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lblTieuDeTongQuan
        // 
        lblTieuDeTongQuan.BackColor = Color.FromArgb(248, 250, 252);
        lblTieuDeTongQuan.Dock = DockStyle.Fill;
        lblTieuDeTongQuan.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        lblTieuDeTongQuan.ForeColor = SystemColors.ControlText;
        lblTieuDeTongQuan.Location = new Point(0, 0);
        lblTieuDeTongQuan.Margin = new Padding(0, 0, 15, 0);
        lblTieuDeTongQuan.Name = "lblTieuDeTongQuan";
        lblTieuDeTongQuan.Size = new Size(1370, 60);
        lblTieuDeTongQuan.TabIndex = 0;
        lblTieuDeTongQuan.Text = "Tổng quan";
        lblTieuDeTongQuan.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.AutoSize = true;
        tableLayoutPanel3.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel3.ColumnCount = 2;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel3.Controls.Add(btnLamMoiTongQuan, 0, 0);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel3.ForeColor = SystemColors.ControlText;
        tableLayoutPanel3.Location = new Point(1385, 0);
        tableLayoutPanel3.Margin = new Padding(0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tableLayoutPanel3.Size = new Size(175, 60);
        tableLayoutPanel3.TabIndex = 1;
        // 
        // btnLamMoiTongQuan
        // 
        btnLamMoiTongQuan.BackColor = Color.FromArgb(15, 118, 110);
        tableLayoutPanel3.SetColumnSpan(btnLamMoiTongQuan, 2);
        btnLamMoiTongQuan.Cursor = Cursors.Hand;
        btnLamMoiTongQuan.Dock = DockStyle.Fill;
        btnLamMoiTongQuan.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLamMoiTongQuan.FlatStyle = FlatStyle.Flat;
        btnLamMoiTongQuan.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLamMoiTongQuan.ForeColor = Color.White;
        btnLamMoiTongQuan.Location = new Point(0, 5);
        btnLamMoiTongQuan.Margin = new Padding(0, 5, 5, 0);
        btnLamMoiTongQuan.Name = "btnLamMoiTongQuan";
        btnLamMoiTongQuan.Size = new Size(170, 55);
        btnLamMoiTongQuan.TabIndex = 0;
        btnLamMoiTongQuan.Text = "Làm mới";
        btnLamMoiTongQuan.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel4.ColumnCount = 4;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel8, 1, 0);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel11, 2, 0);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel14, 3, 0);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel4.ForeColor = SystemColors.ControlText;
        tableLayoutPanel4.Location = new Point(20, 70);
        tableLayoutPanel4.Margin = new Padding(0);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Size = new Size(1560, 148);
        tableLayoutPanel4.TabIndex = 1;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.BackColor = Color.FromArgb(240, 253, 244);
        tableLayoutPanel5.ColumnCount = 1;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.Controls.Add(label6, 0, 0);
        tableLayoutPanel5.Controls.Add(lblDoanhThu, 0, 1);
        tableLayoutPanel5.Controls.Add(label7, 0, 2);
        tableLayoutPanel5.Cursor = Cursors.Hand;
        tableLayoutPanel5.Dock = DockStyle.Fill;
        tableLayoutPanel5.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel5.ForeColor = SystemColors.ControlText;
        tableLayoutPanel5.Location = new Point(0, 8);
        tableLayoutPanel5.Margin = new Padding(0, 8, 10, 10);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.Padding = new Padding(15, 8, 12, 8);
        tableLayoutPanel5.RowCount = 3;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        tableLayoutPanel5.Size = new Size(380, 130);
        tableLayoutPanel5.TabIndex = 0;
        // 
        // label6
        // 
        label6.BackColor = Color.Transparent;
        label6.Cursor = Cursors.Hand;
        label6.Dock = DockStyle.Fill;
        label6.Font = new Font("Segoe UI", 10.2F);
        label6.ForeColor = Color.FromArgb(21, 128, 61);
        label6.Location = new Point(19, 8);
        label6.Margin = new Padding(4, 0, 4, 0);
        label6.Name = "label6";
        label6.Size = new Size(345, 32);
        label6.TabIndex = 0;
        label6.Text = "Doanh thu hôm nay";
        // 
        // lblDoanhThu
        // 
        lblDoanhThu.BackColor = Color.Transparent;
        lblDoanhThu.Cursor = Cursors.Hand;
        lblDoanhThu.Dock = DockStyle.Fill;
        lblDoanhThu.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblDoanhThu.ForeColor = Color.FromArgb(4, 120, 87);
        lblDoanhThu.Location = new Point(19, 40);
        lblDoanhThu.Margin = new Padding(4, 0, 4, 0);
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Size = new Size(345, 53);
        lblDoanhThu.TabIndex = 1;
        lblDoanhThu.Text = "—";
        // 
        // label7
        // 
        label7.BackColor = Color.Transparent;
        label7.Cursor = Cursors.Hand;
        label7.Dock = DockStyle.Fill;
        label7.Font = new Font("Segoe UI", 10.2F);
        label7.ForeColor = SystemColors.ControlText;
        label7.Location = new Point(19, 93);
        label7.Margin = new Padding(4, 0, 4, 0);
        label7.Name = "label7";
        label7.Size = new Size(345, 29);
        label7.TabIndex = 2;
        label7.Text = "Tiền hàng trừ giảm giá";
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.BackColor = Color.FromArgb(239, 246, 255);
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Controls.Add(label9, 0, 0);
        tableLayoutPanel8.Controls.Add(lblSoHoaDon, 0, 1);
        tableLayoutPanel8.Controls.Add(label10, 0, 2);
        tableLayoutPanel8.Cursor = Cursors.Hand;
        tableLayoutPanel8.Dock = DockStyle.Fill;
        tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel8.ForeColor = SystemColors.ControlText;
        tableLayoutPanel8.Location = new Point(390, 8);
        tableLayoutPanel8.Margin = new Padding(0, 8, 10, 10);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.Padding = new Padding(15, 8, 12, 8);
        tableLayoutPanel8.RowCount = 3;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        tableLayoutPanel8.Size = new Size(380, 130);
        tableLayoutPanel8.TabIndex = 1;
        // 
        // label9
        // 
        label9.BackColor = Color.Transparent;
        label9.Cursor = Cursors.Hand;
        label9.Dock = DockStyle.Fill;
        label9.Font = new Font("Segoe UI", 10.2F);
        label9.ForeColor = Color.FromArgb(29, 78, 216);
        label9.Location = new Point(19, 8);
        label9.Margin = new Padding(4, 0, 4, 0);
        label9.Name = "label9";
        label9.Size = new Size(345, 32);
        label9.TabIndex = 0;
        label9.Text = "Đã thanh toán hôm nay";
        // 
        // lblSoHoaDon
        // 
        lblSoHoaDon.BackColor = Color.Transparent;
        lblSoHoaDon.Cursor = Cursors.Hand;
        lblSoHoaDon.Dock = DockStyle.Fill;
        lblSoHoaDon.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblSoHoaDon.ForeColor = Color.FromArgb(30, 64, 175);
        lblSoHoaDon.Location = new Point(19, 40);
        lblSoHoaDon.Margin = new Padding(4, 0, 4, 0);
        lblSoHoaDon.Name = "lblSoHoaDon";
        lblSoHoaDon.Size = new Size(345, 53);
        lblSoHoaDon.TabIndex = 1;
        lblSoHoaDon.Text = "—";
        // 
        // label10
        // 
        label10.BackColor = Color.Transparent;
        label10.Cursor = Cursors.Hand;
        label10.Dock = DockStyle.Fill;
        label10.Font = new Font("Segoe UI", 10.2F);
        label10.ForeColor = SystemColors.ControlText;
        label10.Location = new Point(19, 93);
        label10.Margin = new Padding(4, 0, 4, 0);
        label10.Name = "label10";
        label10.Size = new Size(345, 29);
        label10.TabIndex = 2;
        label10.Text = "Số hóa đơn đã thu tiền";
        // 
        // tableLayoutPanel11
        // 
        tableLayoutPanel11.BackColor = Color.FromArgb(254, 243, 199);
        tableLayoutPanel11.ColumnCount = 1;
        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel11.Controls.Add(label12, 0, 0);
        tableLayoutPanel11.Controls.Add(lblSapHet, 0, 1);
        tableLayoutPanel11.Controls.Add(label13, 0, 2);
        tableLayoutPanel11.Cursor = Cursors.Hand;
        tableLayoutPanel11.Dock = DockStyle.Fill;
        tableLayoutPanel11.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel11.ForeColor = SystemColors.ControlText;
        tableLayoutPanel11.Location = new Point(780, 8);
        tableLayoutPanel11.Margin = new Padding(0, 8, 10, 10);
        tableLayoutPanel11.Name = "tableLayoutPanel11";
        tableLayoutPanel11.Padding = new Padding(15, 8, 12, 8);
        tableLayoutPanel11.RowCount = 3;
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        tableLayoutPanel11.Size = new Size(380, 130);
        tableLayoutPanel11.TabIndex = 2;
        // 
        // label12
        // 
        label12.BackColor = Color.Transparent;
        label12.Cursor = Cursors.Hand;
        label12.Dock = DockStyle.Fill;
        label12.Font = new Font("Segoe UI", 10.2F);
        label12.ForeColor = Color.FromArgb(180, 83, 9);
        label12.Location = new Point(19, 8);
        label12.Margin = new Padding(4, 0, 4, 0);
        label12.Name = "label12";
        label12.Size = new Size(345, 32);
        label12.TabIndex = 0;
        label12.Text = "Thuốc sắp hết";
        // 
        // lblSapHet
        // 
        lblSapHet.BackColor = Color.Transparent;
        lblSapHet.Cursor = Cursors.Hand;
        lblSapHet.Dock = DockStyle.Fill;
        lblSapHet.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblSapHet.ForeColor = Color.FromArgb(180, 83, 9);
        lblSapHet.Location = new Point(19, 40);
        lblSapHet.Margin = new Padding(4, 0, 4, 0);
        lblSapHet.Name = "lblSapHet";
        lblSapHet.Size = new Size(345, 53);
        lblSapHet.TabIndex = 1;
        lblSapHet.Text = "—";
        // 
        // label13
        // 
        label13.BackColor = Color.Transparent;
        label13.Cursor = Cursors.Hand;
        label13.Dock = DockStyle.Fill;
        label13.Font = new Font("Segoe UI", 10.2F);
        label13.ForeColor = SystemColors.ControlText;
        label13.Location = new Point(19, 93);
        label13.Margin = new Padding(4, 0, 4, 0);
        label13.Name = "label13";
        label13.Size = new Size(345, 29);
        label13.TabIndex = 2;
        label13.Text = "Tồn còn hạn dưới 10";
        // 
        // tableLayoutPanel14
        // 
        tableLayoutPanel14.BackColor = Color.FromArgb(255, 241, 242);
        tableLayoutPanel14.ColumnCount = 1;
        tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel14.Controls.Add(label15, 0, 0);
        tableLayoutPanel14.Controls.Add(lblSapHetHan, 0, 1);
        tableLayoutPanel14.Controls.Add(label16, 0, 2);
        tableLayoutPanel14.Cursor = Cursors.Hand;
        tableLayoutPanel14.Dock = DockStyle.Fill;
        tableLayoutPanel14.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel14.ForeColor = SystemColors.ControlText;
        tableLayoutPanel14.Location = new Point(1170, 8);
        tableLayoutPanel14.Margin = new Padding(0, 8, 10, 10);
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.Padding = new Padding(15, 8, 12, 8);
        tableLayoutPanel14.RowCount = 3;
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        tableLayoutPanel14.Size = new Size(380, 130);
        tableLayoutPanel14.TabIndex = 3;
        // 
        // label15
        // 
        label15.BackColor = Color.Transparent;
        label15.Cursor = Cursors.Hand;
        label15.Dock = DockStyle.Fill;
        label15.Font = new Font("Segoe UI", 10.2F);
        label15.ForeColor = Color.FromArgb(190, 18, 60);
        label15.Location = new Point(19, 8);
        label15.Margin = new Padding(4, 0, 4, 0);
        label15.Name = "label15";
        label15.Size = new Size(345, 32);
        label15.TabIndex = 0;
        label15.Text = "Lô sắp hết hạn";
        // 
        // lblSapHetHan
        // 
        lblSapHetHan.BackColor = Color.Transparent;
        lblSapHetHan.Cursor = Cursors.Hand;
        lblSapHetHan.Dock = DockStyle.Fill;
        lblSapHetHan.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        lblSapHetHan.ForeColor = Color.FromArgb(190, 18, 60);
        lblSapHetHan.Location = new Point(19, 40);
        lblSapHetHan.Margin = new Padding(4, 0, 4, 0);
        lblSapHetHan.Name = "lblSapHetHan";
        lblSapHetHan.Size = new Size(345, 53);
        lblSapHetHan.TabIndex = 1;
        lblSapHetHan.Text = "—";
        // 
        // label16
        // 
        label16.BackColor = Color.Transparent;
        label16.Cursor = Cursors.Hand;
        label16.Dock = DockStyle.Fill;
        label16.Font = new Font("Segoe UI", 10.2F);
        label16.ForeColor = SystemColors.ControlText;
        label16.Location = new Point(19, 93);
        label16.Margin = new Padding(4, 0, 4, 0);
        label16.Name = "label16";
        label16.Size = new Size(345, 29);
        label16.TabIndex = 2;
        label16.Text = "Còn hàng, dưới 30 ngày";
        // 
        // tableLayoutPanel17
        // 
        tableLayoutPanel17.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel17.ColumnCount = 2;
        tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel17.Controls.Add(groupBox18, 0, 0);
        tableLayoutPanel17.Controls.Add(groupBox20, 1, 0);
        tableLayoutPanel17.Dock = DockStyle.Fill;
        tableLayoutPanel17.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel17.ForeColor = SystemColors.ControlText;
        tableLayoutPanel17.Location = new Point(20, 218);
        tableLayoutPanel17.Margin = new Padding(0, 0, 0, 10);
        tableLayoutPanel17.Name = "tableLayoutPanel17";
        tableLayoutPanel17.RowCount = 1;
        tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel17.Size = new Size(1560, 331);
        tableLayoutPanel17.TabIndex = 2;
        // 
        // groupBox18
        // 
        groupBox18.BackColor = Color.FromArgb(248, 250, 252);
        groupBox18.Controls.Add(panel19);
        groupBox18.Dock = DockStyle.Fill;
        groupBox18.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox18.ForeColor = SystemColors.ControlText;
        groupBox18.Location = new Point(0, 0);
        groupBox18.Margin = new Padding(0, 0, 10, 0);
        groupBox18.Name = "groupBox18";
        groupBox18.Padding = new Padding(15, 2, 15, 15);
        groupBox18.Size = new Size(770, 331);
        groupBox18.TabIndex = 0;
        groupBox18.TabStop = false;
        groupBox18.Text = "Thuốc cần nhập thêm";
        // 
        // panel19
        // 
        panel19.BackColor = Color.FromArgb(248, 250, 252);
        panel19.Controls.Add(dgvSapHet);
        panel19.Controls.Add(lblKhongSapHet);
        panel19.Dock = DockStyle.Fill;
        panel19.Font = new Font("Segoe UI", 10.2F);
        panel19.ForeColor = SystemColors.ControlText;
        panel19.Location = new Point(15, 30);
        panel19.Margin = new Padding(4);
        panel19.Name = "panel19";
        panel19.Size = new Size(740, 286);
        panel19.TabIndex = 0;
        // 
        // dgvSapHet
        // 
        dgvSapHet.AllowUserToAddRows = false;
        dgvSapHet.AllowUserToDeleteRows = false;
        dgvSapHet.AllowUserToResizeColumns = false;
        dgvSapHet.AllowUserToResizeRows = false;
        dataGridViewCellStyle13.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
        dgvSapHet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSapHet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvSapHet.BackgroundColor = Color.White;
        dgvSapHet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle14.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle14.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle14.ForeColor = Color.White;
        dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle14.SelectionForeColor = Color.White;
        dataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
        dgvSapHet.ColumnHeadersHeight = 38;
        dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle15.BackColor = Color.White;
        dataGridViewCellStyle15.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle15.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle15.Padding = new Padding(4);
        dataGridViewCellStyle15.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle15.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
        dgvSapHet.DefaultCellStyle = dataGridViewCellStyle15;
        dgvSapHet.Dock = DockStyle.Fill;
        dgvSapHet.EnableHeadersVisualStyles = false;
        dgvSapHet.Font = new Font("Segoe UI", 10.2F);
        dgvSapHet.GridColor = SystemColors.ControlLight;
        dgvSapHet.Location = new Point(0, 0);
        dgvSapHet.Margin = new Padding(4);
        dgvSapHet.MultiSelect = false;
        dgvSapHet.Name = "dgvSapHet";
        dgvSapHet.ReadOnly = true;
        dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle16.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle16.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle16.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle16.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle16.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle16.WrapMode = DataGridViewTriState.True;
        dgvSapHet.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
        dgvSapHet.RowHeadersVisible = false;
        dgvSapHet.RowHeadersWidth = 62;
        dgvSapHet.RowTemplate.Height = 34;
        dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHet.Size = new Size(740, 286);
        dgvSapHet.TabIndex = 0;
        // 
        // lblKhongSapHet
        // 
        lblKhongSapHet.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongSapHet.Dock = DockStyle.Fill;
        lblKhongSapHet.Font = new Font("Segoe UI", 10.2F);
        lblKhongSapHet.ForeColor = SystemColors.ControlText;
        lblKhongSapHet.Location = new Point(0, 0);
        lblKhongSapHet.Margin = new Padding(4, 0, 4, 0);
        lblKhongSapHet.Name = "lblKhongSapHet";
        lblKhongSapHet.Size = new Size(740, 286);
        lblKhongSapHet.TabIndex = 1;
        lblKhongSapHet.Text = "Chưa có thuốc dưới ngưỡng tồn.";
        lblKhongSapHet.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // groupBox20
        // 
        groupBox20.BackColor = Color.FromArgb(248, 250, 252);
        groupBox20.Controls.Add(panel21);
        groupBox20.Dock = DockStyle.Fill;
        groupBox20.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox20.ForeColor = SystemColors.ControlText;
        groupBox20.Location = new Point(780, 0);
        groupBox20.Margin = new Padding(0);
        groupBox20.Name = "groupBox20";
        groupBox20.Padding = new Padding(15, 2, 15, 15);
        groupBox20.Size = new Size(780, 331);
        groupBox20.TabIndex = 1;
        groupBox20.TabStop = false;
        groupBox20.Text = "Lô sắp hết hạn";
        // 
        // panel21
        // 
        panel21.BackColor = Color.FromArgb(248, 250, 252);
        panel21.Controls.Add(dgvSapHetHan);
        panel21.Controls.Add(lblKhongSapHetHan);
        panel21.Dock = DockStyle.Fill;
        panel21.Font = new Font("Segoe UI", 10.2F);
        panel21.ForeColor = SystemColors.ControlText;
        panel21.Location = new Point(15, 30);
        panel21.Margin = new Padding(4);
        panel21.Name = "panel21";
        panel21.Size = new Size(750, 286);
        panel21.TabIndex = 0;
        // 
        // dgvSapHetHan
        // 
        dgvSapHetHan.AllowUserToAddRows = false;
        dgvSapHetHan.AllowUserToDeleteRows = false;
        dgvSapHetHan.AllowUserToResizeColumns = false;
        dgvSapHetHan.AllowUserToResizeRows = false;
        dataGridViewCellStyle17.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHetHan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
        dgvSapHetHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSapHetHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvSapHetHan.BackgroundColor = Color.White;
        dgvSapHetHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle18.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle18.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle18.ForeColor = Color.White;
        dataGridViewCellStyle18.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle18.SelectionForeColor = Color.White;
        dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
        dgvSapHetHan.ColumnHeadersHeight = 38;
        dgvSapHetHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle19.BackColor = Color.White;
        dataGridViewCellStyle19.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle19.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle19.Padding = new Padding(4);
        dataGridViewCellStyle19.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle19.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.DefaultCellStyle = dataGridViewCellStyle19;
        dgvSapHetHan.Dock = DockStyle.Fill;
        dgvSapHetHan.EnableHeadersVisualStyles = false;
        dgvSapHetHan.Font = new Font("Segoe UI", 10.2F);
        dgvSapHetHan.GridColor = SystemColors.ControlLight;
        dgvSapHetHan.Location = new Point(0, 0);
        dgvSapHetHan.Margin = new Padding(4);
        dgvSapHetHan.MultiSelect = false;
        dgvSapHetHan.Name = "dgvSapHetHan";
        dgvSapHetHan.ReadOnly = true;
        dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle20.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle20.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle20.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle20.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle20.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle20.WrapMode = DataGridViewTriState.True;
        dgvSapHetHan.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
        dgvSapHetHan.RowHeadersVisible = false;
        dgvSapHetHan.RowHeadersWidth = 62;
        dgvSapHetHan.RowTemplate.Height = 34;
        dgvSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHetHan.Size = new Size(750, 286);
        dgvSapHetHan.TabIndex = 0;
        // 
        // lblKhongSapHetHan
        // 
        lblKhongSapHetHan.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongSapHetHan.Dock = DockStyle.Fill;
        lblKhongSapHetHan.Font = new Font("Segoe UI", 10.2F);
        lblKhongSapHetHan.ForeColor = SystemColors.ControlText;
        lblKhongSapHetHan.Location = new Point(0, 0);
        lblKhongSapHetHan.Margin = new Padding(4, 0, 4, 0);
        lblKhongSapHetHan.Name = "lblKhongSapHetHan";
        lblKhongSapHetHan.Size = new Size(750, 286);
        lblKhongSapHetHan.TabIndex = 1;
        lblKhongSapHetHan.Text = "Chưa có lô sắp hết hạn.";
        lblKhongSapHetHan.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // groupBox22
        // 
        groupBox22.BackColor = Color.FromArgb(248, 250, 252);
        groupBox22.Controls.Add(panel23);
        groupBox22.Dock = DockStyle.Fill;
        groupBox22.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox22.ForeColor = SystemColors.ControlText;
        groupBox22.Location = new Point(20, 559);
        groupBox22.Margin = new Padding(0);
        groupBox22.Name = "groupBox22";
        groupBox22.Padding = new Padding(15, 2, 15, 15);
        groupBox22.Size = new Size(1560, 341);
        groupBox22.TabIndex = 3;
        groupBox22.TabStop = false;
        groupBox22.Text = "5 hóa đơn gần nhất — nhấp đúp để xem chi tiết";
        // 
        // panel23
        // 
        panel23.BackColor = Color.FromArgb(248, 250, 252);
        panel23.Controls.Add(dgvGanNhat);
        panel23.Controls.Add(lblKhongHoaDon);
        panel23.Dock = DockStyle.Fill;
        panel23.Font = new Font("Segoe UI", 10.2F);
        panel23.ForeColor = SystemColors.ControlText;
        panel23.Location = new Point(15, 30);
        panel23.Margin = new Padding(4);
        panel23.Name = "panel23";
        panel23.Size = new Size(1530, 296);
        panel23.TabIndex = 0;
        // 
        // dgvGanNhat
        // 
        dgvGanNhat.AllowUserToAddRows = false;
        dgvGanNhat.AllowUserToDeleteRows = false;
        dgvGanNhat.AllowUserToResizeColumns = false;
        dgvGanNhat.AllowUserToResizeRows = false;
        dataGridViewCellStyle21.BackColor = Color.FromArgb(248, 250, 252);
        dgvGanNhat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
        dgvGanNhat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvGanNhat.BackgroundColor = Color.White;
        dgvGanNhat.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle22.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle22.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle22.ForeColor = Color.White;
        dataGridViewCellStyle22.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle22.SelectionForeColor = Color.White;
        dataGridViewCellStyle22.WrapMode = DataGridViewTriState.False;
        dgvGanNhat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
        dgvGanNhat.ColumnHeadersHeight = 38;
        dgvGanNhat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle23.BackColor = Color.White;
        dataGridViewCellStyle23.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle23.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle23.Padding = new Padding(4);
        dataGridViewCellStyle23.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle23.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle23.WrapMode = DataGridViewTriState.False;
        dgvGanNhat.DefaultCellStyle = dataGridViewCellStyle23;
        dgvGanNhat.Dock = DockStyle.Fill;
        dgvGanNhat.EnableHeadersVisualStyles = false;
        dgvGanNhat.Font = new Font("Segoe UI", 10.2F);
        dgvGanNhat.GridColor = SystemColors.ControlLight;
        dgvGanNhat.Location = new Point(0, 0);
        dgvGanNhat.Margin = new Padding(4);
        dgvGanNhat.MultiSelect = false;
        dgvGanNhat.Name = "dgvGanNhat";
        dgvGanNhat.ReadOnly = true;
        dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle24.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle24.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle24.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle24.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle24.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle24.WrapMode = DataGridViewTriState.True;
        dgvGanNhat.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
        dgvGanNhat.RowHeadersVisible = false;
        dgvGanNhat.RowHeadersWidth = 62;
        dgvGanNhat.RowTemplate.Height = 34;
        dgvGanNhat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGanNhat.Size = new Size(1530, 296);
        dgvGanNhat.TabIndex = 0;
        dgvGanNhat.CellContentClick += dgvGanNhat_CellContentClick;
        // 
        // lblKhongHoaDon
        // 
        lblKhongHoaDon.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongHoaDon.Dock = DockStyle.Fill;
        lblKhongHoaDon.Font = new Font("Segoe UI", 10.2F);
        lblKhongHoaDon.ForeColor = SystemColors.ControlText;
        lblKhongHoaDon.Location = new Point(0, 0);
        lblKhongHoaDon.Margin = new Padding(4, 0, 4, 0);
        lblKhongHoaDon.Name = "lblKhongHoaDon";
        lblKhongHoaDon.Size = new Size(1530, 296);
        lblKhongHoaDon.TabIndex = 1;
        lblKhongHoaDon.Text = "Chưa có hóa đơn bán hàng.";
        lblKhongHoaDon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblTongQuan
        // 
        lblTongQuan.AutoEllipsis = true;
        lblTongQuan.BackColor = Color.FromArgb(248, 250, 252);
        lblTongQuan.Dock = DockStyle.Fill;
        lblTongQuan.Font = new Font("Segoe UI", 10.2F);
        lblTongQuan.ForeColor = SystemColors.ControlText;
        lblTongQuan.Location = new Point(24, 900);
        lblTongQuan.Margin = new Padding(4, 0, 4, 0);
        lblTongQuan.Name = "lblTongQuan";
        lblTongQuan.Size = new Size(1552, 38);
        lblTongQuan.TabIndex = 4;
        // 
        // frmMain
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1875, 1025);
        Controls.Add(shellLayout);
        Font = new Font("Segoe UI", 9F);
        ForeColor = Color.FromArgb(15, 23, 42);
        KeyPreview = true;
        Margin = new Padding(4);
        MinimumSize = new Size(1370, 861);
        Name = "frmMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "QUẢN LÝ HIỆU THUỐC";
        KeyDown += frmMain_KeyDown;
        shellLayout.ResumeLayout(false);
        panelChucNang.ResumeLayout(false);
        sideButtons.ResumeLayout(false);
        contentHost.ResumeLayout(false);
        dashboard.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel11.ResumeLayout(false);
        tableLayoutPanel14.ResumeLayout(false);
        tableLayoutPanel17.ResumeLayout(false);
        groupBox18.ResumeLayout(false);
        panel19.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
        groupBox20.ResumeLayout(false);
        panel21.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).EndInit();
        groupBox22.ResumeLayout(false);
        panel23.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvGanNhat).EndInit();
        ResumeLayout(false);
    }

    private global::System.Windows.Forms.Panel panelChucNang;
    private global::System.Windows.Forms.Button btnBaoCao;
    private global::System.Windows.Forms.Button btnCanhBao;
    private global::System.Windows.Forms.Button btnNhapKho;
    private global::System.Windows.Forms.Button btnQuanLyKhac;
    private global::System.Windows.Forms.Button btnQuanLyThuoc;
    private global::System.Windows.Forms.Button btnBanHang;
    private global::System.Windows.Forms.Label lblTitle;
    private global::System.Windows.Forms.Button btnTrangChu;
    private global::System.Windows.Forms.TableLayoutPanel dashboard;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.Label lblTieuDeTongQuan;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.Button btnLamMoiTongQuan;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.Label label6;
    private global::System.Windows.Forms.Label lblDoanhThu;
    private global::System.Windows.Forms.Label label7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.Label label9;
    private global::System.Windows.Forms.Label lblSoHoaDon;
    private global::System.Windows.Forms.Label label10;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
    private global::System.Windows.Forms.Label label12;
    private global::System.Windows.Forms.Label lblSapHet;
    private global::System.Windows.Forms.Label label13;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.Label label15;
    private global::System.Windows.Forms.Label lblSapHetHan;
    private global::System.Windows.Forms.Label label16;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
    private global::System.Windows.Forms.GroupBox groupBox18;
    private global::System.Windows.Forms.Panel panel19;
    private global::System.Windows.Forms.DataGridView dgvSapHet;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn5;
    private global::System.Windows.Forms.Label lblKhongSapHet;
    private global::System.Windows.Forms.GroupBox groupBox20;
    private global::System.Windows.Forms.Panel panel21;
    private global::System.Windows.Forms.DataGridView dgvSapHetHan;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn7;
    private global::System.Windows.Forms.Label lblKhongSapHetHan;
    private global::System.Windows.Forms.GroupBox groupBox22;
    private global::System.Windows.Forms.Panel panel23;
    private global::System.Windows.Forms.DataGridView dgvGanNhat;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn7;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvGanNhatColumn8;
    private global::System.Windows.Forms.Label lblKhongHoaDon;
    private global::System.Windows.Forms.Label lblTongQuan;
    private System.Windows.Forms.TableLayoutPanel shellLayout;
    private System.Windows.Forms.Panel contentHost;
    private System.Windows.Forms.TableLayoutPanel sideButtons;
    private DataGridViewTextBoxColumn colRecentStatus;
}
