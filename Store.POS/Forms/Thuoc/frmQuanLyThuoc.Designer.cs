using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS;

partial class frmQuanLyThuoc
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        colTrangThaiBan=new global::System.Windows.Forms.DataGridViewTextBoxColumn();
        colTonCoTheBan=new global::System.Windows.Forms.DataGridViewTextBoxColumn();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        medicineCodesLayout = new TableLayoutPanel();
        ingredientLayout = new TableLayoutPanel();
        lblHoatChat = new Label();
        txtHoatChat = new TextBox();
        colHoatChat = new DataGridViewTextBoxColumn();
        barcodeLayout = new TableLayoutPanel();
        unitPriceLayout = new TableLayoutPanel();
        lblMaVach = new Label();
        txtMaVach = new TextBox();
        stockStatusLayout = new TableLayoutPanel();
        statusLayout = new TableLayoutPanel();
        lblTrangThaiBan = new Label();
        cboTrangThaiBan = new ComboBox();
        posLayout = new TableLayoutPanel();
        chkKeDon=new CheckBox();chkKeDon.Name="chkKeDon";chkKeDon.Text="Thuốc kê đơn";chkKeDon.Margin=new Padding(0);chkKeDon.Font=new Font("Segoe UI",9.5F);chkKeDon.Dock=DockStyle.Fill;chkKeDon.Enabled=false;
        posHeading = new Label();
        excelActions = new FlowLayoutPanel();
        btnNhapExcel = new Button();
        btnXuatExcel = new Button();
        btnMauExcel = new Button();
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        tableLayoutPanel4 = new TableLayoutPanel();
        label5 = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvThuoc = new DataGridView();
        MaThuoc = new DataGridViewTextBoxColumn();
        TenThuoc = new DataGridViewTextBoxColumn();
        MaNhom = new DataGridViewTextBoxColumn();
        MaNCC = new DataGridViewTextBoxColumn();
        TenNhom = new DataGridViewTextBoxColumn();
        DonViTinh = new DataGridViewTextBoxColumn();
        DonGia = new DataGridViewTextBoxColumn();
        SoLuongTon = new DataGridViewTextBoxColumn();
        TenNCC = new DataGridViewTextBoxColumn();
        colMaVach = new DataGridViewTextBoxColumn();
        groupBox6 = new GroupBox();
        tableLayoutPanel7 = new TableLayoutPanel();
        tableLayoutPanel8 = new TableLayoutPanel();
        tableLayoutPanel11 = new TableLayoutPanel();
        label12 = new Label();
        txtTenThuoc = new TextBox();
        tableLayoutPanel13 = new TableLayoutPanel();
        label14 = new Label();
        tableLayoutPanel15 = new TableLayoutPanel();
        cboNhomThuoc = new ComboBox();
        btnThemNhom = new Button();
        tableLayoutPanel16 = new TableLayoutPanel();
        label17 = new Label();
        tableLayoutPanel18 = new TableLayoutPanel();
        cboNhaCungCap = new ComboBox();
        btnThemNCC = new Button();
        tableLayoutPanel19 = new TableLayoutPanel();
        label20 = new Label();
        cboDonViTinh = new ComboBox();
        tableLayoutPanel21 = new TableLayoutPanel();
        label22 = new Label();
        txtDonGia = new TextBox();
        tableLayoutPanel23 = new TableLayoutPanel();
        label24 = new Label();
        txtSoLuongTon = new TextBox();
        tableLayoutPanel25 = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLuu = new Button();
        btnBoQua = new Button();
        btnLamMoi = new Button();
        posLayout.SuspendLayout();
        excelActions.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        ((ISupportInitialize)dgvThuoc).BeginInit();
        groupBox6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel11.SuspendLayout();
        tableLayoutPanel13.SuspendLayout();
        tableLayoutPanel15.SuspendLayout();
        tableLayoutPanel16.SuspendLayout();
        tableLayoutPanel18.SuspendLayout();
        tableLayoutPanel19.SuspendLayout();
        tableLayoutPanel21.SuspendLayout();
        tableLayoutPanel23.SuspendLayout();
        tableLayoutPanel25.SuspendLayout();
        SuspendLayout();
        // 
        // posLayout
        // 
        posLayout.BackColor = Color.FromArgb(242, 246, 248);
        posLayout.ColumnCount = 1;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(excelActions, 0, 1);
        posLayout.Controls.Add(tableLayoutPanel1, 0, 2);
        posLayout.Dock = DockStyle.Fill;
        posLayout.Font = new Font("Segoe UI", 10.2F);
        posLayout.ForeColor = Color.FromArgb(15, 23, 42);
        posLayout.Location = new Point(0, 0);
        posLayout.Margin = new Padding(0);
        posLayout.Name = "posLayout";
        posLayout.Padding = new Padding(12);
        posLayout.RowCount = 3;
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        posLayout.Size = new Size(1182, 653);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.BackColor = Color.FromArgb(242, 246, 248);
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold",16F,FontStyle.Bold);
        posHeading.ForeColor = Color.FromArgb(15,23,42);
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1152, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Quản lý thuốc";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // excelActions
        // 
        excelActions.Controls.Add(btnNhapExcel);
        excelActions.Controls.Add(btnXuatExcel);
        excelActions.Controls.Add(btnMauExcel);
        excelActions.Dock = DockStyle.Fill;
        excelActions.Location = new Point(12, 60);
        excelActions.Margin = new Padding(0);
        excelActions.Name = "excelActions";
        excelActions.Size = new Size(1158, 46);
        excelActions.TabIndex = 1;
        excelActions.WrapContents = false;
        // 
        // btnNhapExcel
        // 
        btnNhapExcel.BackColor = Color.FromArgb(22, 101, 52);
        btnNhapExcel.ForeColor = Color.White;
        btnNhapExcel.Location = new Point(3, 3);
        btnNhapExcel.Name = "btnNhapExcel";
        btnNhapExcel.Size = new Size(120, 38);
        btnNhapExcel.TabIndex = 0;
        btnNhapExcel.Text = "Nhập Excel";
        btnNhapExcel.UseVisualStyleBackColor = false;
        // 
        // btnXuatExcel
        // 
        btnXuatExcel.BackColor = Color.FromArgb(15, 118, 110);
        btnXuatExcel.ForeColor = Color.White;
        btnXuatExcel.Location = new Point(129, 3);
        btnXuatExcel.Name = "btnXuatExcel";
        btnXuatExcel.Size = new Size(120, 38);
        btnXuatExcel.TabIndex = 1;
        btnXuatExcel.Text = "Xuất Excel";
        btnXuatExcel.UseVisualStyleBackColor = false;
        // 
        // btnMauExcel
        // 
        btnMauExcel.BackColor = Color.FromArgb(15, 118, 110);
        btnMauExcel.ForeColor = Color.White;
        btnMauExcel.Location = new Point(255, 3);
        btnMauExcel.Name = "btnMauExcel";
        btnMauExcel.Size = new Size(110, 38);
        btnMauExcel.TabIndex = 2;
        btnMauExcel.Text = "File mẫu";
        btnMauExcel.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(242, 246, 248);
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 354F));
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox6, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel1.Location = new Point(12, 106);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1158, 535);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // groupBox2
        // 
        groupBox2.BackColor = Color.White;
        groupBox2.Controls.Add(tableLayoutPanel3);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox2.ForeColor = Color.FromArgb(15, 23, 42);
        groupBox2.Location = new Point(0, 0);
        groupBox2.Margin = new Padding(0, 0, 12, 0);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(12, 2, 12, 12);
        groupBox2.Size = new Size(792, 535);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Tìm kiếm và Danh sách thuốc";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.BackColor = Color.White;
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel3.Controls.Add(dgvThuoc, 0, 1);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel3.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel3.Location = new Point(12, 25);
        tableLayoutPanel3.Margin = new Padding(0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Size = new Size(768, 498);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.BackColor = Color.White;
        tableLayoutPanel4.ColumnCount = 3;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel4.Controls.Add(label5, 0, 0);
        tableLayoutPanel4.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel4.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel4.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel4.Location = new Point(0, 0);
        tableLayoutPanel4.Margin = new Padding(0, 0, 0, 12);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Size = new Size(768, 44);
        tableLayoutPanel4.TabIndex = 0;
        // 
        // label5
        // 
        label5.BackColor = Color.White;
        label5.Dock = DockStyle.Fill;
        label5.Font = new Font("Segoe UI", 10.2F);
        label5.ForeColor = Color.FromArgb(15, 23, 42);
        label5.Location = new Point(3, 0);
        label5.Name = "label5";
        label5.Size = new Size(114, 44);
        label5.TabIndex = 0;
        label5.Text = "Tìm thuốc";
        label5.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTimKiem
        // 
        txtTimKiem.BackColor = Color.White;
        txtTimKiem.BorderStyle = BorderStyle.FixedSingle;
        txtTimKiem.Cursor = Cursors.IBeam;
        txtTimKiem.Dock = DockStyle.Top;
        txtTimKiem.Font = new Font("Segoe UI", 10.2F);
        txtTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
        txtTimKiem.Location = new Point(120, 7);
        txtTimKiem.Margin = new Padding(0, 7, 12, 0);
        txtTimKiem.MaxLength = 200;
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tên thuốc, hoạt chất, nhóm, NCC hoặc mã vạch";
        txtTimKiem.Size = new Size(516, 30);
        txtTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = Color.FromArgb(15, 118, 110);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.Dock = DockStyle.Fill;
        btnTimKiem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(648, 2);
        btnTimKiem.Margin = new Padding(0, 2, 0, 2);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(120, 40);
        btnTimKiem.TabIndex = 2;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // dgvThuoc
        // 
        dgvThuoc.AllowUserToAddRows = false;
        dgvThuoc.AllowUserToDeleteRows = false;
        dgvThuoc.AllowUserToResizeColumns = false;
        dgvThuoc.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(242, 246, 248);
        dgvThuoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvThuoc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvThuoc.BackgroundColor = Color.White;
        dgvThuoc.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvThuoc.ColumnHeadersHeight = 43;
        dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvThuoc.Columns.AddRange(new DataGridViewColumn[] { MaThuoc, TenThuoc, colHoatChat, DonGia, DonViTinh, colTonCoTheBan, colTrangThaiBan, MaNhom, MaNCC, TenNhom, SoLuongTon, TenNCC, colMaVach });
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = Color.White;
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle5.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle5.Padding = new Padding(4);
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
        dgvThuoc.DefaultCellStyle = dataGridViewCellStyle5;
        dgvThuoc.Dock = DockStyle.Fill;
        dgvThuoc.EnableHeadersVisualStyles = false;
        dgvThuoc.Font = new Font("Segoe UI", 10.2F);
        dgvThuoc.GridColor = Color.FromArgb(226, 232, 240);
        dgvThuoc.Location = new Point(0, 56);
        dgvThuoc.Margin = new Padding(0);
        dgvThuoc.MultiSelect = false;
        dgvThuoc.Name = "dgvThuoc";
        dgvThuoc.ReadOnly = true;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(242, 246, 248);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        dgvThuoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvThuoc.RowHeadersVisible = false;
        dgvThuoc.RowHeadersWidth = 51;
        dgvThuoc.RowTemplate.Height = 34;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.Size = new Size(768, 442);
        dgvThuoc.TabIndex = 18;
        dgvThuoc.CellClick += dgvThuoc_CellClick;
        // 
        // MaThuoc
        // 
        MaThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        MaThuoc.DataPropertyName = "MaThuoc";
        MaThuoc.HeaderText = "Mã thuốc";
        MaThuoc.MinimumWidth = 45;
        MaThuoc.Name = "MaThuoc";
        MaThuoc.ReadOnly = true;
        MaThuoc.Resizable = DataGridViewTriState.False;
        MaThuoc.Width = 115;
        // 
        // TenThuoc
        // 
        TenThuoc.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        TenThuoc.DataPropertyName = "TenThuoc";
        TenThuoc.HeaderText = "Tên thuốc";
        TenThuoc.MinimumWidth = 45;
        TenThuoc.Name = "TenThuoc";
        TenThuoc.ReadOnly = true;
        TenThuoc.Resizable = DataGridViewTriState.False;
        TenThuoc.Width = 200;
        // 
        // MaNhom
        // 
        MaNhom.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        MaNhom.DataPropertyName = "MaNhom";
        MaNhom.HeaderText = "Mã nhóm thuốc";
        MaNhom.MinimumWidth = 6;
        MaNhom.Name = "MaNhom";
        MaNhom.ReadOnly = true;
        MaNhom.Resizable = DataGridViewTriState.False;
        MaNhom.Visible = false;
        MaNhom.Width = 173;
        // 
        // MaNCC
        // 
        MaNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        MaNCC.DataPropertyName = "MaNCC";
        MaNCC.HeaderText = "Mã nhà cung cấp";
        MaNCC.MinimumWidth = 6;
        MaNCC.Name = "MaNCC";
        MaNCC.ReadOnly = true;
        MaNCC.Resizable = DataGridViewTriState.False;
        MaNCC.Visible = false;
        MaNCC.Width = 183;
        // 
        // TenNhom
        // 
        TenNhom.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        TenNhom.DataPropertyName = "TenNhom";
        TenNhom.HeaderText = "Nhóm thuốc";
        TenNhom.MinimumWidth = 6;
        TenNhom.Name = "TenNhom";
        TenNhom.ReadOnly = true;
        TenNhom.Resizable = DataGridViewTriState.False;
        TenNhom.Width = 230;
        // 
        // DonViTinh
        // 
        DonViTinh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        DonViTinh.DataPropertyName = "DonViTinh";
        DonViTinh.HeaderText = "Đơn vị";
        DonViTinh.MinimumWidth = 45;
        DonViTinh.Name = "DonViTinh";
        DonViTinh.ReadOnly = true;
        DonViTinh.Resizable = DataGridViewTriState.False;
        DonViTinh.Width = 91;
        // 
        // DonGia
        // 
        DonGia.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        DonGia.DataPropertyName = "DonGia";
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle3.Format = "N0";
        DonGia.DefaultCellStyle = dataGridViewCellStyle3;
        DonGia.HeaderText = "Đơn giá";
        DonGia.MinimumWidth = 45;
        DonGia.Name = "DonGia";
        DonGia.ReadOnly = true;
        DonGia.Resizable = DataGridViewTriState.False;
        DonGia.Width = 105;
        // 
        // SoLuongTon
        // 
        SoLuongTon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        SoLuongTon.DataPropertyName = "SoLuongTon";
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle4.Format = "N0";
        SoLuongTon.DefaultCellStyle = dataGridViewCellStyle4;
        SoLuongTon.HeaderText = "Tồn kho";
        SoLuongTon.MinimumWidth = 45;
        SoLuongTon.Name = "SoLuongTon";
        SoLuongTon.ReadOnly = true;
        SoLuongTon.Resizable = DataGridViewTriState.False;
        SoLuongTon.Width = 104;
        // 
        // TenNCC
        // 
        TenNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        TenNCC.DataPropertyName = "TenNCC";
        TenNCC.HeaderText = "Nhà cung cấp";
        TenNCC.MinimumWidth = 6;
        TenNCC.Name = "TenNCC";
        TenNCC.ReadOnly = true;
        TenNCC.Resizable = DataGridViewTriState.False;
        TenNCC.Width = 220;
        // 
        // colMaVach
        // 
        colMaVach.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        colMaVach.DataPropertyName = "MaVach";
        colMaVach.HeaderText = "Mã vạch";
        colMaVach.MinimumWidth = 6;
        colMaVach.Name = "colMaVach";
        colMaVach.ReadOnly = true;
        colMaVach.Width = 160;
        // 
        // groupBox6
        // 
        groupBox6.BackColor = Color.White;
        groupBox6.Controls.Add(tableLayoutPanel7);
        groupBox6.Dock = DockStyle.Fill;
        groupBox6.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox6.ForeColor = Color.FromArgb(15, 23, 42);
        groupBox6.Location = new Point(804, 0);
        groupBox6.Margin = new Padding(0);
        groupBox6.Name = "groupBox6";
        groupBox6.Padding = new Padding(12, 2, 12, 12);
        groupBox6.Size = new Size(354, 535);
        groupBox6.TabIndex = 1;
        groupBox6.TabStop = false;
        groupBox6.Text = "Thông tin chi tiết";
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.BackColor = Color.White;
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 0);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel25, 0, 2);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel7.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel7.Location = new Point(12, 25);
        tableLayoutPanel7.Margin = new Padding(0);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 3;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 336F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.AutoSize));
        tableLayoutPanel7.Size = new Size(330, 498);
        tableLayoutPanel7.TabIndex = 0;
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.AutoSize = true;
        tableLayoutPanel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel8.BackColor = Color.White;
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Controls.Add(tableLayoutPanel11,0,0);
        tableLayoutPanel8.Controls.Add(medicineCodesLayout,0,1);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel13,0,2);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel16,0,3);
        tableLayoutPanel8.Controls.Add(unitPriceLayout,0,4);
        tableLayoutPanel8.Controls.Add(stockStatusLayout,0,5);
        tableLayoutPanel8.Dock = DockStyle.Top;
        tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel8.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel8.Location = new Point(0, 0);
        tableLayoutPanel8.Margin = new Padding(0);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.RowCount = 6;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.Size = new Size(330, 336);
        tableLayoutPanel8.TabIndex = 0;
        // 
        // tableLayoutPanel11
        // 
        tableLayoutPanel11.BackColor = Color.White;
        tableLayoutPanel11.ColumnCount = 2;
        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50F));
        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50F));
        tableLayoutPanel11.Controls.Add(chkKeDon,1,0);
        tableLayoutPanel11.SetColumnSpan(txtTenThuoc,2);
        tableLayoutPanel11.Controls.Add(label12, 0, 0);
        tableLayoutPanel11.Controls.Add(txtTenThuoc, 0, 1);
        tableLayoutPanel11.Dock = DockStyle.Fill;
        tableLayoutPanel11.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel11.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel11.Location = new Point(0, 0);
        tableLayoutPanel11.Margin = new Padding(0);
        tableLayoutPanel11.Name = "tableLayoutPanel11";
        tableLayoutPanel11.RowCount = 2;
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel11.Size = new Size(330, 56);
        tableLayoutPanel11.TabIndex = 1;
        // 
        // label12
        // 
        label12.BackColor = Color.White;
        label12.Dock = DockStyle.Fill;
        label12.Font = new Font("Segoe UI", 10.2F);
        label12.ForeColor = Color.FromArgb(15, 23, 42);
        label12.Location = new Point(3, 0);
        label12.Name = "label12";
        label12.Size = new Size(324, 22);
        label12.TabIndex = 0;
        label12.Text = "Tên thuốc";
        label12.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTenThuoc
        // 
        txtTenThuoc.BackColor = Color.White;
        txtTenThuoc.BorderStyle = BorderStyle.FixedSingle;
        txtTenThuoc.Cursor = Cursors.IBeam;
        txtTenThuoc.Dock = DockStyle.Top;
        txtTenThuoc.Enabled = false;
        txtTenThuoc.Font = new Font("Segoe UI", 10.2F);
        txtTenThuoc.ForeColor = Color.FromArgb(15, 23, 42);
        txtTenThuoc.Location = new Point(0, 23);
        txtTenThuoc.Margin = new Padding(0, 1, 0, 5);
        txtTenThuoc.Name = "txtTenThuoc";
        txtTenThuoc.Size = new Size(330, 30);
        txtTenThuoc.TabIndex = 8;
        // 
        // tableLayoutPanel13
        // 
        tableLayoutPanel13.BackColor = Color.White;
        tableLayoutPanel13.ColumnCount = 1;
        tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel13.Controls.Add(label14, 0, 0);
        tableLayoutPanel13.Controls.Add(tableLayoutPanel15, 0, 1);
        tableLayoutPanel13.Dock = DockStyle.Fill;
        tableLayoutPanel13.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel13.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel13.Location = new Point(0, 56);
        tableLayoutPanel13.Margin = new Padding(0);
        tableLayoutPanel13.Name = "tableLayoutPanel13";
        tableLayoutPanel13.RowCount = 2;
        tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel13.Size = new Size(330, 56);
        tableLayoutPanel13.TabIndex = 2;
        // 
        // label14
        // 
        label14.BackColor = Color.White;
        label14.Dock = DockStyle.Fill;
        label14.Font = new Font("Segoe UI", 10.2F);
        label14.ForeColor = Color.FromArgb(15, 23, 42);
        label14.Location = new Point(3, 0);
        label14.Name = "label14";
        label14.Size = new Size(324, 22);
        label14.TabIndex = 0;
        label14.Text = "Nhóm thuốc";
        label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel15
        // 
        tableLayoutPanel15.BackColor = Color.White;
        tableLayoutPanel15.ColumnCount = 2;
        tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
        tableLayoutPanel15.Controls.Add(cboNhomThuoc, 0, 0);
        tableLayoutPanel15.Controls.Add(btnThemNhom, 1, 0);
        tableLayoutPanel15.Dock = DockStyle.Top;
        tableLayoutPanel15.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel15.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel15.Location = new Point(0, 22);
        tableLayoutPanel15.Margin = new Padding(0);
        tableLayoutPanel15.Name = "tableLayoutPanel15";
        tableLayoutPanel15.RowCount = 1;
        tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel15.Size = new Size(330, 32);
        tableLayoutPanel15.TabIndex = 1;
        // 
        // cboNhomThuoc
        // 
        cboNhomThuoc.BackColor = SystemColors.Window;
        cboNhomThuoc.Dock = DockStyle.Fill;
        cboNhomThuoc.DropDownWidth = 286;
        cboNhomThuoc.Enabled = false;
        cboNhomThuoc.Font = new Font("Segoe UI", 10.2F);
        cboNhomThuoc.ForeColor = Color.FromArgb(15, 23, 42);
        cboNhomThuoc.Location = new Point(0, 0);
        cboNhomThuoc.Margin = new Padding(0, 0, 6, 0);
        cboNhomThuoc.Name = "cboNhomThuoc";
        cboNhomThuoc.Size = new Size(286, 31);
        cboNhomThuoc.TabIndex = 11;
        // 
        // btnThemNhom
        // 
        btnThemNhom.AccessibleName = "Thêm nhóm thuốc";
        btnThemNhom.BackColor = Color.FromArgb(22, 101, 52);
        btnThemNhom.Cursor = Cursors.Hand;
        btnThemNhom.Dock = DockStyle.Fill;
        btnThemNhom.Enabled = false;
        btnThemNhom.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThemNhom.FlatStyle = FlatStyle.Flat;
        btnThemNhom.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThemNhom.ForeColor = Color.White;
        btnThemNhom.Location = new Point(292, 0);
        btnThemNhom.Margin = new Padding(0);
        btnThemNhom.Name = "btnThemNhom";
        btnThemNhom.Size = new Size(38, 32);
        btnThemNhom.TabIndex = 12;
        btnThemNhom.Text = "+";
        btnThemNhom.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel16
        // 
        tableLayoutPanel16.BackColor = Color.White;
        tableLayoutPanel16.ColumnCount = 1;
        tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel16.Controls.Add(label17, 0, 0);
        tableLayoutPanel16.Controls.Add(tableLayoutPanel18, 0, 1);
        tableLayoutPanel16.Dock = DockStyle.Fill;
        tableLayoutPanel16.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel16.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel16.Location = new Point(0, 112);
        tableLayoutPanel16.Margin = new Padding(0);
        tableLayoutPanel16.Name = "tableLayoutPanel16";
        tableLayoutPanel16.RowCount = 2;
        tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel16.Size = new Size(330, 56);
        tableLayoutPanel16.TabIndex = 3;
        // 
        // label17
        // 
        label17.BackColor = Color.White;
        label17.Dock = DockStyle.Fill;
        label17.Font = new Font("Segoe UI", 10.2F);
        label17.ForeColor = Color.FromArgb(15, 23, 42);
        label17.Location = new Point(3, 0);
        label17.Name = "label17";
        label17.Size = new Size(324, 22);
        label17.TabIndex = 0;
        label17.Text = "Nhà cung cấp";
        label17.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel18
        // 
        tableLayoutPanel18.BackColor = Color.White;
        tableLayoutPanel18.ColumnCount = 2;
        tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
        tableLayoutPanel18.Controls.Add(cboNhaCungCap, 0, 0);
        tableLayoutPanel18.Controls.Add(btnThemNCC, 1, 0);
        tableLayoutPanel18.Dock = DockStyle.Top;
        tableLayoutPanel18.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel18.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel18.Location = new Point(0, 22);
        tableLayoutPanel18.Margin = new Padding(0);
        tableLayoutPanel18.Name = "tableLayoutPanel18";
        tableLayoutPanel18.RowCount = 1;
        tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel18.Size = new Size(330, 32);
        tableLayoutPanel18.TabIndex = 1;
        // 
        // cboNhaCungCap
        // 
        cboNhaCungCap.BackColor = SystemColors.Window;
        cboNhaCungCap.Dock = DockStyle.Fill;
        cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhaCungCap.DropDownWidth = 286;
        cboNhaCungCap.Enabled = false;
        cboNhaCungCap.Font = new Font("Segoe UI", 10.2F);
        cboNhaCungCap.ForeColor = Color.FromArgb(15, 23, 42);
        cboNhaCungCap.Location = new Point(0, 0);
        cboNhaCungCap.Margin = new Padding(0, 0, 6, 0);
        cboNhaCungCap.Name = "cboNhaCungCap";
        cboNhaCungCap.Size = new Size(286, 31);
        cboNhaCungCap.TabIndex = 0;
        // 
        // btnThemNCC
        // 
        btnThemNCC.AccessibleName = "Thêm nhà cung cấp";
        btnThemNCC.BackColor = Color.FromArgb(22, 101, 52);
        btnThemNCC.Cursor = Cursors.Hand;
        btnThemNCC.Dock = DockStyle.Fill;
        btnThemNCC.Enabled = false;
        btnThemNCC.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThemNCC.FlatStyle = FlatStyle.Flat;
        btnThemNCC.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThemNCC.ForeColor = Color.White;
        btnThemNCC.Location = new Point(292, 0);
        btnThemNCC.Margin = new Padding(0);
        btnThemNCC.Name = "btnThemNCC";
        btnThemNCC.Size = new Size(38, 32);
        btnThemNCC.TabIndex = 1;
        btnThemNCC.Text = "+";
        btnThemNCC.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel19
        // 
        tableLayoutPanel19.BackColor = Color.White;
        tableLayoutPanel19.ColumnCount = 1;
        tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel19.Controls.Add(label20, 0, 0);
        tableLayoutPanel19.Controls.Add(cboDonViTinh, 0, 1);
        tableLayoutPanel19.Dock = DockStyle.Fill;
        tableLayoutPanel19.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel19.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel19.Location = new Point(0, 168);
        tableLayoutPanel19.Margin = new Padding(0);
        tableLayoutPanel19.Name = "tableLayoutPanel19";
        tableLayoutPanel19.RowCount = 2;
        tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel19.Size = new Size(330, 56);
        tableLayoutPanel19.TabIndex = 4;
        // 
        // label20
        // 
        label20.BackColor = Color.White;
        label20.Dock = DockStyle.Fill;
        label20.Font = new Font("Segoe UI", 10.2F);
        label20.ForeColor = Color.FromArgb(15, 23, 42);
        label20.Location = new Point(3, 0);
        label20.Name = "label20";
        label20.Size = new Size(324, 22);
        label20.TabIndex = 0;
        label20.Text = "Đơn vị tính";
        label20.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboDonViTinh
        // 
        cboDonViTinh.BackColor = SystemColors.Window;
        cboDonViTinh.Dock = DockStyle.Top;
        cboDonViTinh.DropDownWidth = 330;
        cboDonViTinh.Enabled = false;
        cboDonViTinh.Font = new Font("Segoe UI", 10.2F);
        cboDonViTinh.ForeColor = Color.FromArgb(15, 23, 42);
        cboDonViTinh.Location = new Point(0, 23);
        cboDonViTinh.Margin = new Padding(0, 1, 0, 5);
        cboDonViTinh.Name = "cboDonViTinh";
        cboDonViTinh.Size = new Size(330, 31);
        cboDonViTinh.TabIndex = 12;
        // 
        // tableLayoutPanel21
        // 
        tableLayoutPanel21.BackColor = Color.White;
        tableLayoutPanel21.ColumnCount = 1;
        tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel21.Controls.Add(label22, 0, 0);
        tableLayoutPanel21.Controls.Add(txtDonGia, 0, 1);
        tableLayoutPanel21.Dock = DockStyle.Fill;
        tableLayoutPanel21.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel21.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel21.Location = new Point(0, 224);
        tableLayoutPanel21.Margin = new Padding(0);
        tableLayoutPanel21.Name = "tableLayoutPanel21";
        tableLayoutPanel21.RowCount = 2;
        tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel21.Size = new Size(330, 56);
        tableLayoutPanel21.TabIndex = 5;
        // 
        // label22
        // 
        label22.BackColor = Color.White;
        label22.Dock = DockStyle.Fill;
        label22.Font = new Font("Segoe UI", 10.2F);
        label22.ForeColor = Color.FromArgb(15, 23, 42);
        label22.Location = new Point(3, 0);
        label22.Name = "label22";
        label22.Size = new Size(324, 22);
        label22.TabIndex = 0;
        label22.Text = "Đơn giá bán";
        label22.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDonGia
        // 
        txtDonGia.BackColor = Color.White;
        txtDonGia.BorderStyle = BorderStyle.FixedSingle;
        txtDonGia.Cursor = Cursors.IBeam;
        txtDonGia.Dock = DockStyle.Top;
        txtDonGia.Enabled = false;
        txtDonGia.Font = new Font("Segoe UI", 10.2F);
        txtDonGia.ForeColor = Color.FromArgb(15, 23, 42);
        txtDonGia.Location = new Point(0, 23);
        txtDonGia.Margin = new Padding(0, 1, 0, 5);
        txtDonGia.Name = "txtDonGia";
        txtDonGia.Size = new Size(330, 30);
        txtDonGia.TabIndex = 9;
        // 
        // tableLayoutPanel23
        // 
        tableLayoutPanel23.BackColor = Color.White;
        tableLayoutPanel23.ColumnCount = 1;
        tableLayoutPanel23.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel23.Controls.Add(label24, 0, 0);
        tableLayoutPanel23.Controls.Add(txtSoLuongTon, 0, 1);
        tableLayoutPanel23.Dock = DockStyle.Fill;
        tableLayoutPanel23.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel23.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel23.Location = new Point(0, 280);
        tableLayoutPanel23.Margin = new Padding(0);
        tableLayoutPanel23.Name = "tableLayoutPanel23";
        tableLayoutPanel23.RowCount = 2;
        tableLayoutPanel23.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel23.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel23.Size = new Size(330, 56);
        tableLayoutPanel23.TabIndex = 6;
        // 
        // label24
        // 
        label24.BackColor = Color.White;
        label24.Dock = DockStyle.Fill;
        label24.Font = new Font("Segoe UI", 10.2F);
        label24.ForeColor = Color.FromArgb(15, 23, 42);
        label24.Location = new Point(3, 0);
        label24.Name = "label24";
        label24.Size = new Size(324, 22);
        label24.TabIndex = 0;
        label24.Text = "Tồn thực tế";
        label24.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtSoLuongTon
        // 
        txtSoLuongTon.BackColor = Color.White;
        txtSoLuongTon.BorderStyle = BorderStyle.FixedSingle;
        txtSoLuongTon.Cursor = Cursors.IBeam;
        txtSoLuongTon.Dock = DockStyle.Top;
        txtSoLuongTon.Enabled = false;
        txtSoLuongTon.Font = new Font("Segoe UI", 10.2F);
        txtSoLuongTon.ForeColor = Color.FromArgb(15, 23, 42);
        txtSoLuongTon.Location = new Point(0, 23);
        txtSoLuongTon.Margin = new Padding(0, 1, 0, 5);
        txtSoLuongTon.Name = "txtSoLuongTon";
        txtSoLuongTon.Size = new Size(330, 30);
        txtSoLuongTon.TabIndex = 10;
        // 
        // tableLayoutPanel25
        // 
        tableLayoutPanel25.AutoSize = true;
        tableLayoutPanel25.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanel25.FlowDirection = global::System.Windows.Forms.FlowDirection.LeftToRight;
        tableLayoutPanel25.WrapContents = true;
        tableLayoutPanel25.BackColor = Color.White;
        tableLayoutPanel25.Controls.Add(btnThem);
        tableLayoutPanel25.Controls.Add(btnSua);
        tableLayoutPanel25.Controls.Add(btnXoa);
        tableLayoutPanel25.Controls.Add(btnLuu);
        tableLayoutPanel25.Controls.Add(btnBoQua);
        tableLayoutPanel25.Controls.Add(btnLamMoi);
        tableLayoutPanel25.Dock = global::System.Windows.Forms.DockStyle.Bottom;
        tableLayoutPanel25.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel25.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel25.Location = new Point(0, 366);
        tableLayoutPanel25.Margin = new Padding(0);
        tableLayoutPanel25.Name = "tableLayoutPanel25";
        tableLayoutPanel25.Size = new Size(330, 132);
        tableLayoutPanel25.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(22, 101, 52);
        btnThem.Cursor = Cursors.Hand;
        btnThem.Dock = global::System.Windows.Forms.DockStyle.None;
        btnThem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(0, 4);
        btnThem.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnThem.Name = "btnThem";
        btnThem.Size = new global::System.Drawing.Size(156, 38);
        btnThem.TabIndex = 13;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(15, 118, 110);
        btnSua.Cursor = Cursors.Hand;
        btnSua.Dock = global::System.Windows.Forms.DockStyle.None;
        btnSua.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(169, 4);
        btnSua.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new global::System.Drawing.Size(156, 38);
        btnSua.TabIndex = 14;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.FromArgb(225, 29, 72);
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.Dock = global::System.Windows.Forms.DockStyle.None;
        btnXoa.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(0, 48);
        btnXoa.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new global::System.Drawing.Size(156, 38);
        btnXoa.TabIndex = 15;
        btnXoa.Text = "Ẩn thuốc";
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(22, 101, 52);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Dock = global::System.Windows.Forms.DockStyle.None;
        btnLuu.Enabled = false;
        btnLuu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(169, 48);
        btnLuu.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new global::System.Drawing.Size(156, 38);
        btnLuu.TabIndex = 16;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnBoQua
        // 
        btnBoQua.BackColor = Color.FromArgb(100, 116, 139);
        btnBoQua.Cursor = Cursors.Hand;
        btnBoQua.Dock = global::System.Windows.Forms.DockStyle.None;
        btnBoQua.Enabled = false;
        btnBoQua.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnBoQua.FlatStyle = FlatStyle.Flat;
        btnBoQua.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnBoQua.ForeColor = Color.White;
        btnBoQua.Location = new Point(0, 92);
        btnBoQua.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnBoQua.Name = "btnBoQua";
        btnBoQua.Size = new global::System.Drawing.Size(156, 38);
        btnBoQua.TabIndex = 17;
        btnBoQua.Text = "Bỏ qua";
        btnBoQua.UseVisualStyleBackColor = false;
        btnBoQua.Click += btnBoQua_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(15, 118, 110);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.Dock = global::System.Windows.Forms.DockStyle.None;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(169, 92);
        btnLamMoi.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new global::System.Drawing.Size(156, 38);
        btnLamMoi.TabIndex = 21;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // frmQuanLyThuoc
        // 
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1182, 653);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = Color.FromArgb(15, 23, 42);
        MinimumSize = new Size(1060, 700);
        Name = "frmQuanLyThuoc";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý thuốc | Pharmacy";
        Load += frmQuanLyThuoc_Load;
        posLayout.ResumeLayout(false);
        excelActions.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        ((ISupportInitialize)dgvThuoc).EndInit();
        groupBox6.ResumeLayout(false);
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel11.ResumeLayout(false);
        tableLayoutPanel11.PerformLayout();
        tableLayoutPanel13.ResumeLayout(false);
        tableLayoutPanel15.ResumeLayout(false);
        tableLayoutPanel16.ResumeLayout(false);
        tableLayoutPanel18.ResumeLayout(false);
        tableLayoutPanel19.ResumeLayout(false);
        tableLayoutPanel21.ResumeLayout(false);
        tableLayoutPanel21.PerformLayout();
        tableLayoutPanel23.ResumeLayout(false);
        tableLayoutPanel23.PerformLayout();
        tableLayoutPanel25.ResumeLayout(false);
        colTrangThaiBan.Name="colTrangThaiBan";
        colTrangThaiBan.DataPropertyName="TrangThaiBan";
        colTrangThaiBan.HeaderText="Trạng thái bán";
        colTrangThaiBan.Width=153;
        colTrangThaiBan.MinimumWidth=45;
        colTrangThaiBan.AutoSizeMode=global::System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        colTrangThaiBan.ReadOnly=true;
        colTrangThaiBan.Resizable=global::System.Windows.Forms.DataGridViewTriState.False;
        colTonCoTheBan.Name="colTonCoTheBan";
        colTonCoTheBan.DataPropertyName="TonCoTheBan";
        colTonCoTheBan.HeaderText="Tồn có thể bán";
        colTonCoTheBan.Width=158;
        colTonCoTheBan.MinimumWidth=45;
        colTonCoTheBan.AutoSizeMode=global::System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        colTonCoTheBan.ReadOnly=true;
        colTonCoTheBan.Resizable=global::System.Windows.Forms.DataGridViewTriState.False;
        colTonCoTheBan.DefaultCellStyle.Format="N0";
        colTonCoTheBan.DefaultCellStyle.Alignment=global::System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        stockStatusLayout.Name="stockStatusLayout";
        stockStatusLayout.Dock=DockStyle.Fill;
        stockStatusLayout.Margin=new Padding(0);
        stockStatusLayout.ColumnCount=2;
        stockStatusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45F));
        stockStatusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55F));
        stockStatusLayout.RowCount=1;
        stockStatusLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        stockStatusLayout.Controls.Add(tableLayoutPanel23,0,0);
        stockStatusLayout.Controls.Add(statusLayout,1,0);
        tableLayoutPanel23.Margin=new Padding(0,0,6,0);
        statusLayout.Name="statusLayout";
        statusLayout.Dock=DockStyle.Fill;
        statusLayout.Margin=new Padding(0);
        statusLayout.ColumnCount=1;
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));
        statusLayout.RowCount=2;
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,22F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,34F));
        statusLayout.Controls.Add(lblTrangThaiBan,0,0);
        statusLayout.Controls.Add(cboTrangThaiBan,0,1);
        lblTrangThaiBan.Name="lblTrangThaiBan";
        lblTrangThaiBan.Text="Trạng thái bán";
        lblTrangThaiBan.Dock=DockStyle.Fill;
        lblTrangThaiBan.Font=new Font("Segoe UI",10.2F);
        cboTrangThaiBan.Name="cboTrangThaiBan";
        cboTrangThaiBan.Dock=DockStyle.Top;
        cboTrangThaiBan.DropDownStyle=ComboBoxStyle.DropDownList;
        cboTrangThaiBan.Font=new Font("Segoe UI",10.5F);
        cboTrangThaiBan.Enabled=false;
        cboTrangThaiBan.Items.AddRange(new object[]{"Đang bán","Ngừng bán"});
        cboTrangThaiBan.SelectedIndex=0;
        medicineCodesLayout.Name="medicineCodesLayout";
        medicineCodesLayout.Dock=DockStyle.Fill;
        medicineCodesLayout.Margin=new Padding(0);
        medicineCodesLayout.ColumnCount=2;
        medicineCodesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45F));
        medicineCodesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55F));
        medicineCodesLayout.RowCount=1;
        medicineCodesLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        medicineCodesLayout.Controls.Add(barcodeLayout,0,0);
        medicineCodesLayout.Controls.Add(ingredientLayout,1,0);
        medicineCodesLayout.TabIndex=1;
        ingredientLayout.Name="ingredientLayout";
        ingredientLayout.Dock=DockStyle.Fill;
        ingredientLayout.Margin=new Padding(6,0,0,0);
        ingredientLayout.ColumnCount=1;
        ingredientLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));
        ingredientLayout.RowCount=2;
        ingredientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,22F));
        ingredientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,34F));
        ingredientLayout.Controls.Add(lblHoatChat,0,0);
        ingredientLayout.Controls.Add(txtHoatChat,0,1);
        ingredientLayout.TabIndex=1;
        lblHoatChat.Name="lblHoatChat";
        lblHoatChat.Text="Hoạt chất";
        lblHoatChat.Dock=DockStyle.Fill;
        lblHoatChat.TextAlign=ContentAlignment.MiddleLeft;
        lblHoatChat.Font=new Font("Segoe UI",10.2F);
        txtHoatChat.Name="txtHoatChat";
        txtHoatChat.Dock=DockStyle.Top;
        txtHoatChat.AutoSize=false;
        txtHoatChat.Size=new Size(175,30);
        txtHoatChat.Margin=new Padding(0,1,0,5);
        txtHoatChat.Font=new Font("Segoe UI",10.2F);
        txtHoatChat.ForeColor=Color.FromArgb(15,23,42);
        txtHoatChat.BackColor=Color.White;
        txtHoatChat.BorderStyle=BorderStyle.FixedSingle;
        txtHoatChat.MaxLength=150;
        txtHoatChat.PlaceholderText="Có thể để trống";
        txtHoatChat.Enabled=false;
        colHoatChat.Name="colHoatChat";
        colHoatChat.DataPropertyName="HoatChat";
        colHoatChat.HeaderText="Hoạt chất";
        colHoatChat.AutoSizeMode=DataGridViewAutoSizeColumnMode.None;
        colHoatChat.Width=180;
        colHoatChat.ReadOnly=true;
        barcodeLayout.Name="barcodeLayout";
        barcodeLayout.Dock=DockStyle.Fill;
        barcodeLayout.Margin=new Padding(0);
        barcodeLayout.ColumnCount=1;
        barcodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));
        barcodeLayout.RowCount=2;
        barcodeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,22F));
        barcodeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,34F));
        barcodeLayout.Controls.Add(lblMaVach,0,0);
        barcodeLayout.Controls.Add(txtMaVach,0,1);
        barcodeLayout.TabIndex=1;
        lblMaVach.Name="lblMaVach";
        lblMaVach.Text="Mã vạch";
        lblMaVach.Dock=DockStyle.Fill;
        lblMaVach.TextAlign=ContentAlignment.MiddleLeft;
        lblMaVach.Font=new Font("Segoe UI",10.2F);
        txtMaVach.Name="txtMaVach";
        txtMaVach.Dock=DockStyle.Top;
        txtMaVach.AutoSize=false;
        txtMaVach.Size=new Size(330,30);
        txtMaVach.Margin=new Padding(0,1,0,5);
        txtMaVach.Font=new Font("Segoe UI",10.2F);
        txtMaVach.ForeColor=Color.FromArgb(15,23,42);
        txtMaVach.BackColor=Color.White;
        txtMaVach.BorderStyle=BorderStyle.FixedSingle;
        txtMaVach.MaxLength=50;
        txtMaVach.PlaceholderText="Nhập hoặc quét mã vạch";
        txtMaVach.Enabled=false;
        unitPriceLayout.Name="unitPriceLayout";
        unitPriceLayout.Dock=DockStyle.Fill;
        unitPriceLayout.Margin=new Padding(0);
        unitPriceLayout.ColumnCount=2;
        unitPriceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45F));
        unitPriceLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55F));
        unitPriceLayout.RowCount=1;
        unitPriceLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        unitPriceLayout.Controls.Add(tableLayoutPanel19,0,0);
        unitPriceLayout.Controls.Add(tableLayoutPanel21,1,0);
        tableLayoutPanel19.Margin=new Padding(0,0,6,0);
        ResumeLayout(false);
    }

    private TableLayoutPanel medicineCodesLayout;
    private TableLayoutPanel ingredientLayout;
    private Label lblHoatChat;
    private TextBox txtHoatChat;
    private DataGridViewTextBoxColumn colHoatChat;
    private TableLayoutPanel barcodeLayout;
    private TableLayoutPanel unitPriceLayout;
    private Label lblMaVach;
    private TextBox txtMaVach;
    private TableLayoutPanel stockStatusLayout;
    private TableLayoutPanel statusLayout;
    private Label lblTrangThaiBan;
    private ComboBox cboTrangThaiBan;
    private CheckBox chkKeDon;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiBan;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn colTonCoTheBan;
    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.Label label5;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.DataGridView dgvThuoc;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
    private global::System.Windows.Forms.Label label12;
    private global::System.Windows.Forms.TextBox txtTenThuoc;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
    private global::System.Windows.Forms.Label label14;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
    private global::System.Windows.Forms.ComboBox cboNhomThuoc;
    private global::System.Windows.Forms.Button btnThemNhom;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
    private global::System.Windows.Forms.Label label17;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
    private global::System.Windows.Forms.ComboBox cboNhaCungCap;
    private global::System.Windows.Forms.Button btnThemNCC;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
    private global::System.Windows.Forms.Label label20;
    private global::System.Windows.Forms.ComboBox cboDonViTinh;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
    private global::System.Windows.Forms.Label label22;
    private global::System.Windows.Forms.TextBox txtDonGia;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
    private global::System.Windows.Forms.Label label24;
    private global::System.Windows.Forms.TextBox txtSoLuongTon;
    private global::System.Windows.Forms.FlowLayoutPanel tableLayoutPanel25;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLuu;
    private global::System.Windows.Forms.Button btnBoQua;
    private global::System.Windows.Forms.Button btnLamMoi;
    private FlowLayoutPanel excelActions;
    private Button btnNhapExcel,btnXuatExcel,btnMauExcel;
    private DataGridViewTextBoxColumn MaThuoc;
    private DataGridViewTextBoxColumn TenThuoc;
    private DataGridViewTextBoxColumn MaNhom;
    private DataGridViewTextBoxColumn MaNCC;
    private DataGridViewTextBoxColumn TenNhom;
    private DataGridViewTextBoxColumn DonViTinh;
    private DataGridViewTextBoxColumn DonGia;
    private DataGridViewTextBoxColumn SoLuongTon;
    private DataGridViewTextBoxColumn TenNCC;
    private DataGridViewTextBoxColumn colMaVach;
}
