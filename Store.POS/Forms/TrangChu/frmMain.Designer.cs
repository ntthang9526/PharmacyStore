using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

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
        this.dgvSapHetColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvGanNhatColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colRecentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
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
        btnDangXuat = new Button();
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
        shellLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        shellLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        shellLayout.Controls.Add(panelChucNang, 0, 0);
        shellLayout.Controls.Add(lblTitle, 1, 0);
        shellLayout.Controls.Add(contentHost, 1, 1);
        shellLayout.Dock = DockStyle.Fill;
        shellLayout.Location = new Point(0, 0);
        shellLayout.Margin = new Padding(0);
        shellLayout.Name = "shellLayout";
        shellLayout.RowCount = 2;
        shellLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        shellLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        shellLayout.Size = new Size(1500, 820);
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
        panelChucNang.Size = new Size(220, 820);
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
        sideButtons.Controls.Add(btnDangXuat, 0, 8);
        sideButtons.Dock = DockStyle.Fill;
        sideButtons.Location = new Point(0, 0);
        sideButtons.Name = "sideButtons";
        sideButtons.Padding = new Padding(12, 14, 12, 0);
        sideButtons.RowCount = 9;
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        sideButtons.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
        sideButtons.Size = new Size(220, 820);
        sideButtons.TabIndex = 0;
        // 
        // btnTrangChu
        // 
        btnTrangChu.BackColor = Color.FromArgb(30, 41, 59);
        btnTrangChu.Cursor = Cursors.Hand;
        btnTrangChu.Dock = DockStyle.Fill;
        btnTrangChu.FlatAppearance.BorderSize = 0;
        btnTrangChu.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnTrangChu.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnTrangChu.FlatStyle = FlatStyle.Flat;
        btnTrangChu.Font = new Font("Segoe UI Semibold", 10.2F);
        btnTrangChu.ForeColor = Color.FromArgb(241, 245, 249);
        btnTrangChu.Location = new Point(15, 17);
        btnTrangChu.Name = "btnTrangChu";
        btnTrangChu.Size = new Size(190, 52);
        btnTrangChu.TabIndex = 0;
        btnTrangChu.Text = "Trang Chủ";
        btnTrangChu.UseVisualStyleBackColor = false;
        btnTrangChu.Click += btnTrangChu_Click;
        // 
        // btnBanHang
        // 
        btnBanHang.BackColor = Color.FromArgb(30, 41, 59);
        btnBanHang.Cursor = Cursors.Hand;
        btnBanHang.Dock = DockStyle.Fill;
        btnBanHang.FlatAppearance.BorderSize = 0;
        btnBanHang.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnBanHang.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnBanHang.FlatStyle = FlatStyle.Flat;
        btnBanHang.Font = new Font("Segoe UI Semibold", 10.2F);
        btnBanHang.ForeColor = Color.FromArgb(241, 245, 249);
        btnBanHang.Location = new Point(15, 75);
        btnBanHang.Name = "btnBanHang";
        btnBanHang.Size = new Size(190, 52);
        btnBanHang.TabIndex = 0;
        btnBanHang.Text = "Bán hàng (F1)";
        btnBanHang.UseVisualStyleBackColor = false;
        btnBanHang.Click += btnBanHang_Click;
        // 
        // btnQuanLyThuoc
        // 
        btnQuanLyThuoc.BackColor = Color.FromArgb(30, 41, 59);
        btnQuanLyThuoc.Cursor = Cursors.Hand;
        btnQuanLyThuoc.Dock = DockStyle.Fill;
        btnQuanLyThuoc.FlatAppearance.BorderSize = 0;
        btnQuanLyThuoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnQuanLyThuoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnQuanLyThuoc.FlatStyle = FlatStyle.Flat;
        btnQuanLyThuoc.Font = new Font("Segoe UI Semibold", 10.2F);
        btnQuanLyThuoc.ForeColor = Color.FromArgb(241, 245, 249);
        btnQuanLyThuoc.Location = new Point(15, 133);
        btnQuanLyThuoc.Name = "btnQuanLyThuoc";
        btnQuanLyThuoc.Size = new Size(190, 52);
        btnQuanLyThuoc.TabIndex = 1;
        btnQuanLyThuoc.Text = "Quản lý thuốc (F2)";
        btnQuanLyThuoc.UseVisualStyleBackColor = false;
        btnQuanLyThuoc.Click += btnQuanLyThuoc_Click;
        // 
        // btnQuanLyKhac
        // 
        btnQuanLyKhac.BackColor = Color.FromArgb(30, 41, 59);
        btnQuanLyKhac.Cursor = Cursors.Hand;
        btnQuanLyKhac.Dock = DockStyle.Fill;
        btnQuanLyKhac.FlatAppearance.BorderSize = 0;
        btnQuanLyKhac.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnQuanLyKhac.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnQuanLyKhac.FlatStyle = FlatStyle.Flat;
        btnQuanLyKhac.Font = new Font("Segoe UI Semibold", 10.2F);
        btnQuanLyKhac.ForeColor = Color.FromArgb(241, 245, 249);
        btnQuanLyKhac.Location = new Point(15, 191);
        btnQuanLyKhac.Name = "btnQuanLyKhac";
        btnQuanLyKhac.Size = new Size(190, 52);
        btnQuanLyKhac.TabIndex = 4;
        btnQuanLyKhac.Text = "Quản lý khác (F3)";
        btnQuanLyKhac.UseVisualStyleBackColor = false;
        btnQuanLyKhac.Click += btnQuanLyKhac_Click;
        // 
        // btnNhapKho
        // 
        btnNhapKho.BackColor = Color.FromArgb(30, 41, 59);
        btnNhapKho.Cursor = Cursors.Hand;
        btnNhapKho.Dock = DockStyle.Fill;
        btnNhapKho.FlatAppearance.BorderSize = 0;
        btnNhapKho.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnNhapKho.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnNhapKho.FlatStyle = FlatStyle.Flat;
        btnNhapKho.Font = new Font("Segoe UI Semibold", 10.2F);
        btnNhapKho.ForeColor = Color.FromArgb(241, 245, 249);
        btnNhapKho.Location = new Point(15, 249);
        btnNhapKho.Name = "btnNhapKho";
        btnNhapKho.Size = new Size(190, 52);
        btnNhapKho.TabIndex = 5;
        btnNhapKho.Text = "Nhập kho (F4)";
        btnNhapKho.UseVisualStyleBackColor = false;
        btnNhapKho.Click += btnNhapKho_Click;
        // 
        // btnCanhBao
        // 
        btnCanhBao.BackColor = Color.FromArgb(30, 41, 59);
        btnCanhBao.Cursor = Cursors.Hand;
        btnCanhBao.Dock = DockStyle.Fill;
        btnCanhBao.FlatAppearance.BorderSize = 0;
        btnCanhBao.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnCanhBao.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnCanhBao.FlatStyle = FlatStyle.Flat;
        btnCanhBao.Font = new Font("Segoe UI Semibold", 10.2F);
        btnCanhBao.ForeColor = Color.FromArgb(241, 245, 249);
        btnCanhBao.Location = new Point(15, 307);
        btnCanhBao.Name = "btnCanhBao";
        btnCanhBao.Size = new Size(190, 52);
        btnCanhBao.TabIndex = 6;
        btnCanhBao.Text = "Cảnh báo (F5)";
        btnCanhBao.UseVisualStyleBackColor = false;
        btnCanhBao.Click += btnCanhBao_Click;
        // 
        // btnBaoCao
        // 
        btnBaoCao.BackColor = Color.FromArgb(30, 41, 59);
        btnBaoCao.Cursor = Cursors.Hand;
        btnBaoCao.Dock = DockStyle.Fill;
        btnBaoCao.FlatAppearance.BorderSize = 0;
        btnBaoCao.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 23, 42);
        btnBaoCao.FlatAppearance.MouseOverBackColor = Color.FromArgb(51, 65, 85);
        btnBaoCao.FlatStyle = FlatStyle.Flat;
        btnBaoCao.Font = new Font("Segoe UI Semibold", 10.2F);
        btnBaoCao.ForeColor = Color.FromArgb(241, 245, 249);
        btnBaoCao.Location = new Point(15, 365);
        btnBaoCao.Name = "btnBaoCao";
        btnBaoCao.Size = new Size(190, 52);
        btnBaoCao.TabIndex = 7;
        btnBaoCao.Text = "Báo cáo (F6)";
        btnBaoCao.UseVisualStyleBackColor = false;
        btnBaoCao.Click += btnBaoCao_Click;
        // 
        // btnDangXuat
        // 
        btnDangXuat.BackColor = Color.FromArgb(190, 18, 60);
        btnDangXuat.Cursor = Cursors.Hand;
        btnDangXuat.FlatAppearance.BorderSize = 0;
        btnDangXuat.FlatAppearance.MouseDownBackColor = Color.FromArgb(159, 18, 57);
        btnDangXuat.FlatAppearance.MouseOverBackColor = Color.FromArgb(225, 29, 72);
        btnDangXuat.FlatStyle = FlatStyle.Flat;
        btnDangXuat.Font = new Font("Segoe UI Semibold", 10.2F);
        btnDangXuat.ForeColor = Color.White;
        btnDangXuat.Location = new Point(12, 756);
        btnDangXuat.Margin = new Padding(0, 0, 0, 14);
        btnDangXuat.Name = "btnDangXuat";
        btnDangXuat.Size = new Size(180, 48);
        btnDangXuat.TabIndex = 8;
        btnDangXuat.Text = "Đăng xuất";
        btnDangXuat.UseVisualStyleBackColor = false;
        btnDangXuat.Click += btnDangXuat_Click;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.FromArgb(15, 118, 110);
        lblTitle.Dock = DockStyle.Fill;
        lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(220, 0);
        lblTitle.Margin = new Padding(0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(1280, 60);
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
        contentHost.Location = new Point(220, 60);
        contentHost.Margin = new Padding(0);
        contentHost.Name = "contentHost";
        contentHost.Size = new Size(1280, 760);
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
        dashboard.Padding = new Padding(16, 8, 16, 10);
        dashboard.RowCount = 5;
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        dashboard.Size = new Size(1280, 760);
        dashboard.TabIndex = 4;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        tableLayoutPanel1.Controls.Add(lblTieuDeTongQuan, 0, 0);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
        tableLayoutPanel1.Location = new Point(16, 8);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1248, 48);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // lblTieuDeTongQuan
        // 
        lblTieuDeTongQuan.BackColor = Color.FromArgb(248, 250, 252);
        lblTieuDeTongQuan.Dock = DockStyle.Fill;
        lblTieuDeTongQuan.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        lblTieuDeTongQuan.ForeColor = SystemColors.ControlText;
        lblTieuDeTongQuan.Location = new Point(0, 0);
        lblTieuDeTongQuan.Margin = new Padding(0, 0, 12, 0);
        lblTieuDeTongQuan.Name = "lblTieuDeTongQuan";
        lblTieuDeTongQuan.Size = new Size(1096, 48);
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
        tableLayoutPanel3.Location = new Point(1108, 0);
        tableLayoutPanel3.Margin = new Padding(0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel3.Size = new Size(140, 48);
        tableLayoutPanel3.TabIndex = 1;
        // 
        // btnLamMoiTongQuan
        // 
        btnLamMoiTongQuan.BackColor = Color.FromArgb(15, 118, 110);
        tableLayoutPanel3.SetColumnSpan(btnLamMoiTongQuan, 2);
        btnLamMoiTongQuan.Cursor = Cursors.Hand;
        btnLamMoiTongQuan.Dock = DockStyle.Fill;
        btnLamMoiTongQuan.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLamMoiTongQuan.FlatAppearance.BorderSize = 0;
        btnLamMoiTongQuan.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLamMoiTongQuan.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLamMoiTongQuan.FlatStyle = FlatStyle.Flat;
        btnLamMoiTongQuan.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLamMoiTongQuan.ForeColor = Color.White;
        btnLamMoiTongQuan.Location = new Point(0, 4);
        btnLamMoiTongQuan.Margin = new Padding(0, 4, 0, 0);
        btnLamMoiTongQuan.Name = "btnLamMoiTongQuan";
        btnLamMoiTongQuan.Size = new Size(140, 44);
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
        tableLayoutPanel4.Location = new Point(16, 56);
        tableLayoutPanel4.Margin = new Padding(0);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Size = new Size(1248, 118);
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
        tableLayoutPanel5.Location = new Point(0, 6);
        tableLayoutPanel5.Margin = new Padding(0, 6, 8, 8);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.Padding = new Padding(12, 6, 10, 6);
        tableLayoutPanel5.RowCount = 3;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
        tableLayoutPanel5.Size = new Size(304, 104);
        tableLayoutPanel5.TabIndex = 0;
        // 
        // label6
        // 
        label6.BackColor = Color.Transparent;
        label6.Cursor = Cursors.Hand;
        label6.Dock = DockStyle.Fill;
        label6.Font = new Font("Segoe UI", 10.2F);
        label6.ForeColor = Color.FromArgb(21, 128, 61);
        label6.Location = new Point(15, 6);
        label6.Name = "label6";
        label6.Size = new Size(276, 26);
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
        lblDoanhThu.Location = new Point(15, 32);
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Size = new Size(276, 43);
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
        label7.Location = new Point(15, 75);
        label7.Name = "label7";
        label7.Size = new Size(276, 23);
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
        tableLayoutPanel8.Location = new Point(312, 6);
        tableLayoutPanel8.Margin = new Padding(0, 6, 8, 8);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.Padding = new Padding(12, 6, 10, 6);
        tableLayoutPanel8.RowCount = 3;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
        tableLayoutPanel8.Size = new Size(304, 104);
        tableLayoutPanel8.TabIndex = 1;
        // 
        // label9
        // 
        label9.BackColor = Color.Transparent;
        label9.Cursor = Cursors.Hand;
        label9.Dock = DockStyle.Fill;
        label9.Font = new Font("Segoe UI", 10.2F);
        label9.ForeColor = Color.FromArgb(29, 78, 216);
        label9.Location = new Point(15, 6);
        label9.Name = "label9";
        label9.Size = new Size(276, 26);
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
        lblSoHoaDon.Location = new Point(15, 32);
        lblSoHoaDon.Name = "lblSoHoaDon";
        lblSoHoaDon.Size = new Size(276, 43);
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
        label10.Location = new Point(15, 75);
        label10.Name = "label10";
        label10.Size = new Size(276, 23);
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
        tableLayoutPanel11.Location = new Point(624, 6);
        tableLayoutPanel11.Margin = new Padding(0, 6, 8, 8);
        tableLayoutPanel11.Name = "tableLayoutPanel11";
        tableLayoutPanel11.Padding = new Padding(12, 6, 10, 6);
        tableLayoutPanel11.RowCount = 3;
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
        tableLayoutPanel11.Size = new Size(304, 104);
        tableLayoutPanel11.TabIndex = 2;
        // 
        // label12
        // 
        label12.BackColor = Color.Transparent;
        label12.Cursor = Cursors.Hand;
        label12.Dock = DockStyle.Fill;
        label12.Font = new Font("Segoe UI", 10.2F);
        label12.ForeColor = Color.FromArgb(180, 83, 9);
        label12.Location = new Point(15, 6);
        label12.Name = "label12";
        label12.Size = new Size(276, 26);
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
        lblSapHet.Location = new Point(15, 32);
        lblSapHet.Name = "lblSapHet";
        lblSapHet.Size = new Size(276, 43);
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
        label13.Location = new Point(15, 75);
        label13.Name = "label13";
        label13.Size = new Size(276, 23);
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
        tableLayoutPanel14.Location = new Point(936, 6);
        tableLayoutPanel14.Margin = new Padding(0, 6, 8, 8);
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.Padding = new Padding(12, 6, 10, 6);
        tableLayoutPanel14.RowCount = 3;
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
        tableLayoutPanel14.Size = new Size(304, 104);
        tableLayoutPanel14.TabIndex = 3;
        // 
        // label15
        // 
        label15.BackColor = Color.Transparent;
        label15.Cursor = Cursors.Hand;
        label15.Dock = DockStyle.Fill;
        label15.Font = new Font("Segoe UI", 10.2F);
        label15.ForeColor = Color.FromArgb(190, 18, 60);
        label15.Location = new Point(15, 6);
        label15.Name = "label15";
        label15.Size = new Size(276, 26);
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
        lblSapHetHan.Location = new Point(15, 32);
        lblSapHetHan.Name = "lblSapHetHan";
        lblSapHetHan.Size = new Size(276, 43);
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
        label16.Location = new Point(15, 75);
        label16.Name = "label16";
        label16.Size = new Size(276, 23);
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
        tableLayoutPanel17.Location = new Point(16, 174);
        tableLayoutPanel17.Margin = new Padding(0, 0, 0, 8);
        tableLayoutPanel17.Name = "tableLayoutPanel17";
        tableLayoutPanel17.RowCount = 1;
        tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel17.Size = new Size(1248, 265);
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
        groupBox18.Margin = new Padding(0, 0, 8, 0);
        groupBox18.Name = "groupBox18";
        groupBox18.Padding = new Padding(12, 2, 12, 12);
        groupBox18.Size = new Size(616, 265);
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
        panel19.Location = new Point(12, 25);
        panel19.Name = "panel19";
        panel19.Size = new Size(592, 228);
        panel19.TabIndex = 0;
        // 
        // dgvSapHet
        // 
        dgvSapHet.AllowUserToAddRows = false;
        dgvSapHet.AllowUserToDeleteRows = false;
        dgvSapHet.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvSapHet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvSapHet.BackgroundColor = Color.White;
        dgvSapHet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvSapHet.ColumnHeadersHeight = 38;
        dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.Padding = new Padding(4);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvSapHet.DefaultCellStyle = dataGridViewCellStyle3;
        dgvSapHet.Dock = DockStyle.Fill;
        dgvSapHet.EnableHeadersVisualStyles = false;
        dgvSapHet.Font = new Font("Segoe UI", 10.2F);
        dgvSapHet.GridColor = SystemColors.ControlLight;
        dgvSapHet.Location = new Point(0, 0);
        dgvSapHet.MultiSelect = false;
        dgvSapHet.Name = "dgvSapHet";
        dgvSapHet.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvSapHet.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvSapHet.RowHeadersVisible = false;
        dgvSapHet.RowHeadersWidth = 51;
        dgvSapHet.RowTemplate.Height = 34;
        dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHet.Size = new Size(592, 228);
        dgvSapHet.TabIndex = 0;
        // 
        // lblKhongSapHet
        // 
        lblKhongSapHet.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongSapHet.Dock = DockStyle.Fill;
        lblKhongSapHet.Font = new Font("Segoe UI", 10.2F);
        lblKhongSapHet.ForeColor = SystemColors.ControlText;
        lblKhongSapHet.Location = new Point(0, 0);
        lblKhongSapHet.Name = "lblKhongSapHet";
        lblKhongSapHet.Size = new Size(592, 228);
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
        groupBox20.Location = new Point(624, 0);
        groupBox20.Margin = new Padding(0);
        groupBox20.Name = "groupBox20";
        groupBox20.Padding = new Padding(12, 2, 12, 12);
        groupBox20.Size = new Size(624, 265);
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
        panel21.Location = new Point(12, 25);
        panel21.Name = "panel21";
        panel21.Size = new Size(600, 228);
        panel21.TabIndex = 0;
        // 
        // dgvSapHetHan
        // 
        dgvSapHetHan.AllowUserToAddRows = false;
        dgvSapHetHan.AllowUserToDeleteRows = false;
        dgvSapHetHan.AllowUserToResizeColumns = false;
        dgvSapHetHan.AllowUserToResizeRows = false;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHetHan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
        dgvSapHetHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvSapHetHan.BackgroundColor = Color.White;
        dgvSapHetHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle6.ForeColor = Color.White;
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle6.SelectionForeColor = Color.White;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvSapHetHan.ColumnHeadersHeight = 38;
        dgvSapHetHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle7.BackColor = Color.White;
        dataGridViewCellStyle7.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle7.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle7.Padding = new Padding(4);
        dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.DefaultCellStyle = dataGridViewCellStyle7;
        dgvSapHetHan.Dock = DockStyle.Fill;
        dgvSapHetHan.EnableHeadersVisualStyles = false;
        dgvSapHetHan.Font = new Font("Segoe UI", 10.2F);
        dgvSapHetHan.GridColor = SystemColors.ControlLight;
        dgvSapHetHan.Location = new Point(0, 0);
        dgvSapHetHan.MultiSelect = false;
        dgvSapHetHan.Name = "dgvSapHetHan";
        dgvSapHetHan.ReadOnly = true;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle8.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
        dgvSapHetHan.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
        dgvSapHetHan.RowHeadersVisible = false;
        dgvSapHetHan.RowHeadersWidth = 51;
        dgvSapHetHan.RowTemplate.Height = 34;
        dgvSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHetHan.Size = new Size(600, 228);
        dgvSapHetHan.TabIndex = 0;
        // 
        // lblKhongSapHetHan
        // 
        lblKhongSapHetHan.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongSapHetHan.Dock = DockStyle.Fill;
        lblKhongSapHetHan.Font = new Font("Segoe UI", 10.2F);
        lblKhongSapHetHan.ForeColor = SystemColors.ControlText;
        lblKhongSapHetHan.Location = new Point(0, 0);
        lblKhongSapHetHan.Name = "lblKhongSapHetHan";
        lblKhongSapHetHan.Size = new Size(600, 228);
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
        groupBox22.Location = new Point(16, 447);
        groupBox22.Margin = new Padding(0);
        groupBox22.Name = "groupBox22";
        groupBox22.Padding = new Padding(12, 2, 12, 12);
        groupBox22.Size = new Size(1248, 273);
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
        panel23.Location = new Point(12, 25);
        panel23.Name = "panel23";
        panel23.Size = new Size(1224, 236);
        panel23.TabIndex = 0;
        // 
        // dgvGanNhat
        // 
        dgvGanNhat.AllowUserToAddRows = false;
        dgvGanNhat.AllowUserToDeleteRows = false;
        dgvGanNhat.AllowUserToResizeColumns = false;
        dgvGanNhat.AllowUserToResizeRows = false;
        dataGridViewCellStyle9.BackColor = Color.FromArgb(248, 250, 252);
        dgvGanNhat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
        dgvGanNhat.BackgroundColor = Color.White;
        dgvGanNhat.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle10.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle10.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle10.ForeColor = Color.White;
        dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle10.SelectionForeColor = Color.White;
        dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
        dgvGanNhat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
        dgvGanNhat.ColumnHeadersHeight = 38;
        dgvGanNhat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle11.BackColor = Color.White;
        dataGridViewCellStyle11.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle11.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle11.Padding = new Padding(4);
        dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle11.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
        dgvGanNhat.DefaultCellStyle = dataGridViewCellStyle11;
        dgvGanNhat.Dock = DockStyle.Fill;
        dgvGanNhat.EnableHeadersVisualStyles = false;
        dgvGanNhat.Font = new Font("Segoe UI", 10.2F);
        dgvGanNhat.GridColor = SystemColors.ControlLight;
        dgvGanNhat.Location = new Point(0, 0);
        dgvGanNhat.MultiSelect = false;
        dgvGanNhat.Name = "dgvGanNhat";
        dgvGanNhat.ReadOnly = true;
        dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle12.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle12.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
        dgvGanNhat.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
        dgvGanNhat.RowHeadersVisible = false;
        dgvGanNhat.RowHeadersWidth = 51;
        dgvGanNhat.RowTemplate.Height = 34;
        dgvGanNhat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGanNhat.Size = new Size(1224, 236);
        dgvGanNhat.TabIndex = 0;
        // 
        // lblKhongHoaDon
        // 
        lblKhongHoaDon.BackColor = Color.FromArgb(248, 250, 252);
        lblKhongHoaDon.Dock = DockStyle.Fill;
        lblKhongHoaDon.Font = new Font("Segoe UI", 10.2F);
        lblKhongHoaDon.ForeColor = SystemColors.ControlText;
        lblKhongHoaDon.Location = new Point(0, 0);
        lblKhongHoaDon.Name = "lblKhongHoaDon";
        lblKhongHoaDon.Size = new Size(1224, 236);
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
        lblTongQuan.Location = new Point(19, 720);
        lblTongQuan.Name = "lblTongQuan";
        lblTongQuan.Size = new Size(1242, 30);
        lblTongQuan.TabIndex = 4;
        // 
        // frmMain
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1500, 820);
        Controls.Add(shellLayout);
        Font = new Font("Segoe UI", 9F);
        ForeColor = Color.FromArgb(15, 23, 42);
        KeyPreview = true;
        MinimumSize = new Size(1100, 700);
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
                //
        // dgvSapHetColumn1
        //
        this.dgvSapHetColumn1.Name = "MaThuoc";
        this.dgvSapHetColumn1.DataPropertyName = "MaThuoc";
        this.dgvSapHetColumn1.HeaderText = "Mã thuốc";
        this.dgvSapHetColumn1.Width = 123;
        this.dgvSapHetColumn1.MinimumWidth = 5;
        this.dgvSapHetColumn1.ReadOnly = true;
        this.dgvSapHetColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetColumn2
        //
        this.dgvSapHetColumn2.Name = "TenThuoc";
        this.dgvSapHetColumn2.DataPropertyName = "TenThuoc";
        this.dgvSapHetColumn2.HeaderText = "Tên thuốc";
        this.dgvSapHetColumn2.Width = 200;
        this.dgvSapHetColumn2.MinimumWidth = 5;
        this.dgvSapHetColumn2.ReadOnly = true;
        this.dgvSapHetColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetColumn3
        //
        this.dgvSapHetColumn3.Name = "DonViTinh";
        this.dgvSapHetColumn3.DataPropertyName = "DonViTinh";
        this.dgvSapHetColumn3.HeaderText = "Đơn vị tính";
        this.dgvSapHetColumn3.Width = 99;
        this.dgvSapHetColumn3.MinimumWidth = 5;
        this.dgvSapHetColumn3.ReadOnly = true;
        this.dgvSapHetColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetColumn4
        //
        this.dgvSapHetColumn4.Name = "NhaCungCap";
        this.dgvSapHetColumn4.DataPropertyName = "NhaCungCap";
        this.dgvSapHetColumn4.HeaderText = "Nhà cung cấp";
        this.dgvSapHetColumn4.Width = 180;
        this.dgvSapHetColumn4.MinimumWidth = 5;
        this.dgvSapHetColumn4.ReadOnly = true;
        this.dgvSapHetColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetColumn5
        //
        this.dgvSapHetColumn5.Name = "TonKho";
        this.dgvSapHetColumn5.DataPropertyName = "TonKho";
        this.dgvSapHetColumn5.HeaderText = "Tồn còn hạn";
        this.dgvSapHetColumn5.Width = 146;
        this.dgvSapHetColumn5.MinimumWidth = 5;
        this.dgvSapHetColumn5.ReadOnly = true;
        this.dgvSapHetColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn5.DefaultCellStyle.Format = "N0";
        this.dgvSapHetColumn5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // dgvSapHetHanColumn1
        //
        this.dgvSapHetHanColumn1.Name = "MaThuoc";
        this.dgvSapHetHanColumn1.DataPropertyName = "MaThuoc";
        this.dgvSapHetHanColumn1.HeaderText = "Mã thuốc";
        this.dgvSapHetHanColumn1.Width = 123;
        this.dgvSapHetHanColumn1.MinimumWidth = 5;
        this.dgvSapHetHanColumn1.ReadOnly = true;
        this.dgvSapHetHanColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetHanColumn2
        //
        this.dgvSapHetHanColumn2.Name = "TenThuoc";
        this.dgvSapHetHanColumn2.DataPropertyName = "TenThuoc";
        this.dgvSapHetHanColumn2.HeaderText = "Tên thuốc";
        this.dgvSapHetHanColumn2.Width = 200;
        this.dgvSapHetHanColumn2.MinimumWidth = 5;
        this.dgvSapHetHanColumn2.ReadOnly = true;
        this.dgvSapHetHanColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetHanColumn3
        //
        this.dgvSapHetHanColumn3.Name = "SoLo";
        this.dgvSapHetHanColumn3.DataPropertyName = "SoLo";
        this.dgvSapHetHanColumn3.HeaderText = "Số lô";
        this.dgvSapHetHanColumn3.Width = 120;
        this.dgvSapHetHanColumn3.MinimumWidth = 5;
        this.dgvSapHetHanColumn3.ReadOnly = true;
        this.dgvSapHetHanColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetHanColumn4
        //
        this.dgvSapHetHanColumn4.Name = "HanDung";
        this.dgvSapHetHanColumn4.DataPropertyName = "HanDung";
        this.dgvSapHetHanColumn4.HeaderText = "Hạn sử dụng";
        this.dgvSapHetHanColumn4.Width = 126;
        this.dgvSapHetHanColumn4.MinimumWidth = 5;
        this.dgvSapHetHanColumn4.ReadOnly = true;
        this.dgvSapHetHanColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn4.DefaultCellStyle.Format = "dd/MM/yyyy";
        //
        // dgvSapHetHanColumn5
        //
        this.dgvSapHetHanColumn5.Name = "SoNgayConLai";
        this.dgvSapHetHanColumn5.DataPropertyName = "SoNgayConLai";
        this.dgvSapHetHanColumn5.HeaderText = "Số ngày còn lại";
        this.dgvSapHetHanColumn5.Width = 166;
        this.dgvSapHetHanColumn5.MinimumWidth = 5;
        this.dgvSapHetHanColumn5.ReadOnly = true;
        this.dgvSapHetHanColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvSapHetHanColumn6
        //
        this.dgvSapHetHanColumn6.Name = "TonLo";
        this.dgvSapHetHanColumn6.DataPropertyName = "TonLo";
        this.dgvSapHetHanColumn6.HeaderText = "Tồn của lô";
        this.dgvSapHetHanColumn6.Width = 130;
        this.dgvSapHetHanColumn6.MinimumWidth = 5;
        this.dgvSapHetHanColumn6.ReadOnly = true;
        this.dgvSapHetHanColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn6.DefaultCellStyle.Format = "N0";
        this.dgvSapHetHanColumn6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // dgvSapHetHanColumn7
        //
        this.dgvSapHetHanColumn7.Name = "TrangThai";
        this.dgvSapHetHanColumn7.DataPropertyName = "TrangThai";
        this.dgvSapHetHanColumn7.HeaderText = "Trạng thái";
        this.dgvSapHetHanColumn7.Width = 140;
        this.dgvSapHetHanColumn7.MinimumWidth = 5;
        this.dgvSapHetHanColumn7.ReadOnly = true;
        this.dgvSapHetHanColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvGanNhatColumn1
        //
        this.dgvGanNhatColumn1.Name = "MaHD";
        this.dgvGanNhatColumn1.DataPropertyName = "MaHD";
        this.dgvGanNhatColumn1.HeaderText = "Mã hóa đơn";
        this.dgvGanNhatColumn1.Width = 143;
        this.dgvGanNhatColumn1.MinimumWidth = 5;
        this.dgvGanNhatColumn1.ReadOnly = true;
        this.dgvGanNhatColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvGanNhatColumn2
        //
        this.dgvGanNhatColumn2.Name = "NgayLap";
        this.dgvGanNhatColumn2.DataPropertyName = "NgayLap";
        this.dgvGanNhatColumn2.HeaderText = "Ngày lập";
        this.dgvGanNhatColumn2.Width = 180;
        this.dgvGanNhatColumn2.MinimumWidth = 5;
        this.dgvGanNhatColumn2.ReadOnly = true;
        this.dgvGanNhatColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvGanNhatColumn2.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        //
        // dgvGanNhatColumn3
        //
        this.dgvGanNhatColumn3.Name = "KhachHang";
        this.dgvGanNhatColumn3.DataPropertyName = "KhachHang";
        this.dgvGanNhatColumn3.HeaderText = "Khách hàng";
        this.dgvGanNhatColumn3.Width = 180;
        this.dgvGanNhatColumn3.MinimumWidth = 5;
        this.dgvGanNhatColumn3.ReadOnly = true;
        this.dgvGanNhatColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvGanNhatColumn4
        //
        this.dgvGanNhatColumn4.Name = "NhanVien";
        this.dgvGanNhatColumn4.DataPropertyName = "NhanVien";
        this.dgvGanNhatColumn4.HeaderText = "Nhân viên";
        this.dgvGanNhatColumn4.Width = 180;
        this.dgvGanNhatColumn4.MinimumWidth = 5;
        this.dgvGanNhatColumn4.ReadOnly = true;
        this.dgvGanNhatColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        //
        // dgvGanNhatColumn5
        //
        this.dgvGanNhatColumn5.Name = "TongTien";
        this.dgvGanNhatColumn5.DataPropertyName = "TongTien";
        this.dgvGanNhatColumn5.HeaderText = "Tổng tiền";
        this.dgvGanNhatColumn5.Width = 126;
        this.dgvGanNhatColumn5.MinimumWidth = 5;
        this.dgvGanNhatColumn5.ReadOnly = true;
        this.dgvGanNhatColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvGanNhatColumn5.DefaultCellStyle.Format = "N0";
        this.dgvGanNhatColumn5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // dgvGanNhatColumn6
        //
        this.dgvGanNhatColumn6.Name = "GiamGia";
        this.dgvGanNhatColumn6.DataPropertyName = "GiamGia";
        this.dgvGanNhatColumn6.HeaderText = "Giảm giá";
        this.dgvGanNhatColumn6.Width = 118;
        this.dgvGanNhatColumn6.MinimumWidth = 5;
        this.dgvGanNhatColumn6.ReadOnly = true;
        this.dgvGanNhatColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvGanNhatColumn6.DefaultCellStyle.Format = "N0";
        this.dgvGanNhatColumn6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // dgvGanNhatColumn7
        //
        this.dgvGanNhatColumn7.Name = "CanThanhToan";
        this.dgvGanNhatColumn7.DataPropertyName = "CanThanhToan";
        this.dgvGanNhatColumn7.HeaderText = "Cần thanh toán";
        this.dgvGanNhatColumn7.Width = 170;
        this.dgvGanNhatColumn7.MinimumWidth = 5;
        this.dgvGanNhatColumn7.ReadOnly = true;
        this.dgvGanNhatColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvGanNhatColumn7.DefaultCellStyle.Format = "N0";
        this.dgvGanNhatColumn7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // dgvGanNhatColumn8
        //
        this.dgvGanNhatColumn8.Name = "ThucThu";
        this.dgvGanNhatColumn8.DataPropertyName = "ThucThu";
        this.dgvGanNhatColumn8.HeaderText = "Thực thu";
        this.dgvGanNhatColumn8.Width = 120;
        this.dgvGanNhatColumn8.MinimumWidth = 5;
        this.dgvGanNhatColumn8.ReadOnly = true;
        this.dgvGanNhatColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvGanNhatColumn8.DefaultCellStyle.Format = "N0";
        this.dgvGanNhatColumn8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //
        // colRecentStatus
        //
        this.colRecentStatus.Name = "TrangThai";
        this.colRecentStatus.DataPropertyName = "TrangThai";
        this.colRecentStatus.HeaderText = "Trạng thái";
        this.colRecentStatus.MinimumWidth = 5;
        this.colRecentStatus.Width = 145;
        this.colRecentStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colRecentStatus.ReadOnly = true;
        this.colRecentStatus.DisplayIndex = 1;

        this.dgvSapHet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvSapHet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvSapHetColumn1, this.dgvSapHetColumn2, this.dgvSapHetColumn3, this.dgvSapHetColumn4, this.dgvSapHetColumn5 });

        this.dgvSapHetHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvSapHetHan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvSapHetHanColumn1, this.dgvSapHetHanColumn2, this.dgvSapHetHanColumn3, this.dgvSapHetHanColumn4, this.dgvSapHetHanColumn5, this.dgvSapHetHanColumn6, this.dgvSapHetHanColumn7 });

        this.dgvGanNhat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvGanNhat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvGanNhatColumn1, this.dgvGanNhatColumn2, this.dgvGanNhatColumn3, this.dgvGanNhatColumn4, this.dgvGanNhatColumn5, this.dgvGanNhatColumn6, this.dgvGanNhatColumn7, this.dgvGanNhatColumn8, this.colRecentStatus });
        ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
        groupBox20.ResumeLayout(false);
        panel21.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).EndInit();
        groupBox22.ResumeLayout(false);
        panel23.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvGanNhat).EndInit();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                dgvGanNhat.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvGanNhat.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvGanNhat.ColumnHeadersHeight=43;
        dgvSapHet.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHet.ColumnHeadersHeight=43;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHetHan.ColumnHeadersHeight=43;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
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
    private global::System.Windows.Forms.Button btnDangXuat;
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
    private System.Windows.Forms.DataGridViewTextBoxColumn colRecentStatus;
}

}