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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        posLayout = new TableLayoutPanel();
        posHeading = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        tableLayoutPanel4 = new TableLayoutPanel();
        label5 = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvThuoc = new DataGridView();
        groupBox6 = new GroupBox();
        tableLayoutPanel7 = new TableLayoutPanel();
        tableLayoutPanel8 = new TableLayoutPanel();
        tableLayoutPanel9 = new TableLayoutPanel();
        label10 = new Label();
        txtMaThuoc = new TextBox();
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
        tableLayoutPanel25 = new TableLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLuu = new Button();
        btnBoQua = new Button();
        btnThoat = new Button();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
        groupBox6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel9.SuspendLayout();
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
        posLayout.BackColor = SystemColors.Control;
        posLayout.ColumnCount = 1;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(tableLayoutPanel1, 0, 1);
        posLayout.Dock = DockStyle.Fill;
        posLayout.Font = new Font("Segoe UI", 10.2F);
        posLayout.ForeColor = SystemColors.ControlText;
        posLayout.Location = new Point(0, 0);
        posLayout.Margin = new Padding(0);
        posLayout.Name = "posLayout";
        posLayout.Padding = new Padding(12);
        posLayout.RowCount = 2;
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        posLayout.Size = new Size(1182, 653);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.BackColor = SystemColors.Control;
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.ForeColor = SystemColors.ControlText;
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1152, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Quản lý thuốc";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = SystemColors.Control;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 354F));
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox6, 1, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
        tableLayoutPanel1.Location = new Point(12, 60);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(1158, 581);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // groupBox2
        // 
        groupBox2.BackColor = SystemColors.Control;
        groupBox2.Controls.Add(tableLayoutPanel3);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox2.ForeColor = SystemColors.ControlText;
        groupBox2.Location = new Point(0, 0);
        groupBox2.Margin = new Padding(0, 0, 12, 0);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(12, 2, 12, 12);
        groupBox2.Size = new Size(792, 581);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Tìm kiếm và Danh sách thuốc";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.BackColor = SystemColors.Control;
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel3.Controls.Add(dgvThuoc, 0, 1);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel3.ForeColor = SystemColors.ControlText;
        tableLayoutPanel3.Location = new Point(12, 25);
        tableLayoutPanel3.Margin = new Padding(0);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Size = new Size(768, 544);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.BackColor = SystemColors.Control;
        tableLayoutPanel4.ColumnCount = 3;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel4.Controls.Add(label5, 0, 0);
        tableLayoutPanel4.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel4.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel4.ForeColor = SystemColors.ControlText;
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
        label5.BackColor = SystemColors.Control;
        label5.Dock = DockStyle.Fill;
        label5.Font = new Font("Segoe UI", 10.2F);
        label5.ForeColor = SystemColors.ControlText;
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
        txtTimKiem.ForeColor = SystemColors.ControlText;
        txtTimKiem.Location = new Point(120, 7);
        txtTimKiem.Margin = new Padding(0, 7, 12, 0);
        txtTimKiem.MaxLength = 200;
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tên thuốc, nhóm, NCC hoặc mã vạch";
        txtTimKiem.Size = new Size(516, 30);
        txtTimKiem.TabIndex = 3;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = SystemColors.ControlLight;
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.Dock = DockStyle.Fill;
        btnTimKiem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnTimKiem.ForeColor = SystemColors.ControlText;
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
        dgvThuoc.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.White;
        dgvThuoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvThuoc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvThuoc.BackgroundColor = SystemColors.ScrollBar;
        dgvThuoc.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = SystemColors.Control;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.Padding = new Padding(4);
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvThuoc.DefaultCellStyle = dataGridViewCellStyle3;
        dgvThuoc.Dock = DockStyle.Fill;
        dgvThuoc.EnableHeadersVisualStyles = false;
        dgvThuoc.Font = new Font("Segoe UI", 10.2F);
        dgvThuoc.GridColor = SystemColors.ControlLight;
        dgvThuoc.Location = new Point(0, 56);
        dgvThuoc.Margin = new Padding(0);
        dgvThuoc.MultiSelect = false;
        dgvThuoc.Name = "dgvThuoc";
        dgvThuoc.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = SystemColors.Control;
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvThuoc.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvThuoc.RowHeadersVisible = false;
        dgvThuoc.RowHeadersWidth = 51;
        dgvThuoc.RowTemplate.Height = 34;
        dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvThuoc.Size = new Size(768, 488);
        dgvThuoc.TabIndex = 18;
        dgvThuoc.CellClick += dgvThuoc_CellClick;
        // 
        // groupBox6
        // 
        groupBox6.BackColor = SystemColors.Control;
        groupBox6.Controls.Add(tableLayoutPanel7);
        groupBox6.Dock = DockStyle.Fill;
        groupBox6.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox6.ForeColor = SystemColors.ControlText;
        groupBox6.Location = new Point(804, 0);
        groupBox6.Margin = new Padding(0);
        groupBox6.Name = "groupBox6";
        groupBox6.Padding = new Padding(12, 2, 12, 12);
        groupBox6.Size = new Size(354, 581);
        groupBox6.TabIndex = 1;
        groupBox6.TabStop = false;
        groupBox6.Text = "Thông tin chi tiết";
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.BackColor = SystemColors.Control;
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 0);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel25, 0, 2);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel7.ForeColor = SystemColors.ControlText;
        tableLayoutPanel7.Location = new Point(12, 25);
        tableLayoutPanel7.Margin = new Padding(0);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 3;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 392F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
        tableLayoutPanel7.Size = new Size(330, 544);
        tableLayoutPanel7.TabIndex = 0;
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.AutoSize = true;
        tableLayoutPanel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel8.BackColor = SystemColors.Control;
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Controls.Add(tableLayoutPanel9);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel11);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel13);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel16);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel19);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel21);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel23);
        tableLayoutPanel8.Dock = DockStyle.Top;
        tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel8.ForeColor = SystemColors.ControlText;
        tableLayoutPanel8.Location = new Point(0, 0);
        tableLayoutPanel8.Margin = new Padding(0);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.RowCount = 7;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.Size = new Size(330, 392);
        tableLayoutPanel8.TabIndex = 0;
        // 
        // tableLayoutPanel9
        // 
        tableLayoutPanel9.BackColor = SystemColors.Control;
        tableLayoutPanel9.ColumnCount = 1;
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel9.Controls.Add(label10, 0, 0);
        tableLayoutPanel9.Controls.Add(txtMaThuoc, 0, 1);
        tableLayoutPanel9.Dock = DockStyle.Fill;
        tableLayoutPanel9.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel9.ForeColor = SystemColors.ControlText;
        tableLayoutPanel9.Location = new Point(0, 0);
        tableLayoutPanel9.Margin = new Padding(0);
        tableLayoutPanel9.Name = "tableLayoutPanel9";
        tableLayoutPanel9.RowCount = 2;
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel9.Size = new Size(330, 56);
        tableLayoutPanel9.TabIndex = 0;
        // 
        // label10
        // 
        label10.BackColor = SystemColors.Control;
        label10.Dock = DockStyle.Fill;
        label10.Font = new Font("Segoe UI", 10.2F);
        label10.ForeColor = SystemColors.ControlText;
        label10.Location = new Point(3, 0);
        label10.Name = "label10";
        label10.Size = new Size(324, 22);
        label10.TabIndex = 0;
        label10.Text = "Mã thuốc (tự động)";
        label10.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtMaThuoc
        // 
        txtMaThuoc.BackColor = Color.White;
        txtMaThuoc.BorderStyle = BorderStyle.FixedSingle;
        txtMaThuoc.Cursor = Cursors.IBeam;
        txtMaThuoc.Dock = DockStyle.Top;
        txtMaThuoc.Enabled = false;
        txtMaThuoc.Font = new Font("Segoe UI", 10.2F);
        txtMaThuoc.ForeColor = SystemColors.ControlText;
        txtMaThuoc.Location = new Point(0, 23);
        txtMaThuoc.Margin = new Padding(0, 1, 0, 5);
        txtMaThuoc.Name = "txtMaThuoc";
        txtMaThuoc.Size = new Size(330, 30);
        txtMaThuoc.TabIndex = 7;
        // 
        // tableLayoutPanel11
        // 
        tableLayoutPanel11.BackColor = SystemColors.Control;
        tableLayoutPanel11.ColumnCount = 1;
        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel11.Controls.Add(label12, 0, 0);
        tableLayoutPanel11.Controls.Add(txtTenThuoc, 0, 1);
        tableLayoutPanel11.Dock = DockStyle.Fill;
        tableLayoutPanel11.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel11.ForeColor = SystemColors.ControlText;
        tableLayoutPanel11.Location = new Point(0, 56);
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
        label12.BackColor = SystemColors.Control;
        label12.Dock = DockStyle.Fill;
        label12.Font = new Font("Segoe UI", 10.2F);
        label12.ForeColor = SystemColors.ControlText;
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
        txtTenThuoc.ForeColor = SystemColors.ControlText;
        txtTenThuoc.Location = new Point(0, 23);
        txtTenThuoc.Margin = new Padding(0, 1, 0, 5);
        txtTenThuoc.Name = "txtTenThuoc";
        txtTenThuoc.Size = new Size(330, 30);
        txtTenThuoc.TabIndex = 8;
        // 
        // tableLayoutPanel13
        // 
        tableLayoutPanel13.BackColor = SystemColors.Control;
        tableLayoutPanel13.ColumnCount = 1;
        tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel13.Controls.Add(label14, 0, 0);
        tableLayoutPanel13.Controls.Add(tableLayoutPanel15, 0, 1);
        tableLayoutPanel13.Dock = DockStyle.Fill;
        tableLayoutPanel13.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel13.ForeColor = SystemColors.ControlText;
        tableLayoutPanel13.Location = new Point(0, 112);
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
        label14.BackColor = SystemColors.Control;
        label14.Dock = DockStyle.Fill;
        label14.Font = new Font("Segoe UI", 10.2F);
        label14.ForeColor = SystemColors.ControlText;
        label14.Location = new Point(3, 0);
        label14.Name = "label14";
        label14.Size = new Size(324, 22);
        label14.TabIndex = 0;
        label14.Text = "Nhóm thuốc";
        label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel15
        // 
        tableLayoutPanel15.BackColor = SystemColors.Control;
        tableLayoutPanel15.ColumnCount = 2;
        tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
        tableLayoutPanel15.Controls.Add(cboNhomThuoc, 0, 0);
        tableLayoutPanel15.Controls.Add(btnThemNhom, 1, 0);
        tableLayoutPanel15.Dock = DockStyle.Top;
        tableLayoutPanel15.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel15.ForeColor = SystemColors.ControlText;
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
        cboNhomThuoc.ForeColor = SystemColors.ControlText;
        cboNhomThuoc.Location = new Point(0, 0);
        cboNhomThuoc.Margin = new Padding(0, 0, 6, 0);
        cboNhomThuoc.Name = "cboNhomThuoc";
        cboNhomThuoc.Size = new Size(286, 31);
        cboNhomThuoc.TabIndex = 11;
        // 
        // btnThemNhom
        // 
        btnThemNhom.AccessibleName = "Thêm nhóm thuốc";
        btnThemNhom.BackColor = Color.FromArgb(192, 255, 192);
        btnThemNhom.Cursor = Cursors.Hand;
        btnThemNhom.Dock = DockStyle.Fill;
        btnThemNhom.Enabled = false;
        btnThemNhom.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThemNhom.FlatStyle = FlatStyle.Flat;
        btnThemNhom.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThemNhom.ForeColor = SystemColors.ControlText;
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
        tableLayoutPanel16.BackColor = SystemColors.Control;
        tableLayoutPanel16.ColumnCount = 1;
        tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel16.Controls.Add(label17, 0, 0);
        tableLayoutPanel16.Controls.Add(tableLayoutPanel18, 0, 1);
        tableLayoutPanel16.Dock = DockStyle.Fill;
        tableLayoutPanel16.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel16.ForeColor = SystemColors.ControlText;
        tableLayoutPanel16.Location = new Point(0, 168);
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
        label17.BackColor = SystemColors.Control;
        label17.Dock = DockStyle.Fill;
        label17.Font = new Font("Segoe UI", 10.2F);
        label17.ForeColor = SystemColors.ControlText;
        label17.Location = new Point(3, 0);
        label17.Name = "label17";
        label17.Size = new Size(324, 22);
        label17.TabIndex = 0;
        label17.Text = "Nhà cung cấp";
        label17.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel18
        // 
        tableLayoutPanel18.BackColor = SystemColors.Control;
        tableLayoutPanel18.ColumnCount = 2;
        tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
        tableLayoutPanel18.Controls.Add(cboNhaCungCap, 0, 0);
        tableLayoutPanel18.Controls.Add(btnThemNCC, 1, 0);
        tableLayoutPanel18.Dock = DockStyle.Top;
        tableLayoutPanel18.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel18.ForeColor = SystemColors.ControlText;
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
        cboNhaCungCap.ForeColor = SystemColors.ControlText;
        cboNhaCungCap.Location = new Point(0, 0);
        cboNhaCungCap.Margin = new Padding(0, 0, 6, 0);
        cboNhaCungCap.Name = "cboNhaCungCap";
        cboNhaCungCap.Size = new Size(286, 31);
        cboNhaCungCap.TabIndex = 0;
        // 
        // btnThemNCC
        // 
        btnThemNCC.AccessibleName = "Thêm nhà cung cấp";
        btnThemNCC.BackColor = Color.FromArgb(192, 255, 192);
        btnThemNCC.Cursor = Cursors.Hand;
        btnThemNCC.Dock = DockStyle.Fill;
        btnThemNCC.Enabled = false;
        btnThemNCC.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThemNCC.FlatStyle = FlatStyle.Flat;
        btnThemNCC.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThemNCC.ForeColor = SystemColors.ControlText;
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
        tableLayoutPanel19.BackColor = SystemColors.Control;
        tableLayoutPanel19.ColumnCount = 1;
        tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel19.Controls.Add(label20, 0, 0);
        tableLayoutPanel19.Controls.Add(cboDonViTinh, 0, 1);
        tableLayoutPanel19.Dock = DockStyle.Fill;
        tableLayoutPanel19.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel19.ForeColor = SystemColors.ControlText;
        tableLayoutPanel19.Location = new Point(0, 224);
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
        label20.BackColor = SystemColors.Control;
        label20.Dock = DockStyle.Fill;
        label20.Font = new Font("Segoe UI", 10.2F);
        label20.ForeColor = SystemColors.ControlText;
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
        cboDonViTinh.ForeColor = SystemColors.ControlText;
        cboDonViTinh.Location = new Point(0, 23);
        cboDonViTinh.Margin = new Padding(0, 1, 0, 5);
        cboDonViTinh.Name = "cboDonViTinh";
        cboDonViTinh.Size = new Size(330, 31);
        cboDonViTinh.TabIndex = 12;
        // 
        // tableLayoutPanel21
        // 
        tableLayoutPanel21.BackColor = SystemColors.Control;
        tableLayoutPanel21.ColumnCount = 1;
        tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel21.Controls.Add(label22, 0, 0);
        tableLayoutPanel21.Controls.Add(txtDonGia, 0, 1);
        tableLayoutPanel21.Dock = DockStyle.Fill;
        tableLayoutPanel21.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel21.ForeColor = SystemColors.ControlText;
        tableLayoutPanel21.Location = new Point(0, 280);
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
        label22.BackColor = SystemColors.Control;
        label22.Dock = DockStyle.Fill;
        label22.Font = new Font("Segoe UI", 10.2F);
        label22.ForeColor = SystemColors.ControlText;
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
        txtDonGia.ForeColor = SystemColors.ControlText;
        txtDonGia.Location = new Point(0, 23);
        txtDonGia.Margin = new Padding(0, 1, 0, 5);
        txtDonGia.Name = "txtDonGia";
        txtDonGia.Size = new Size(330, 30);
        txtDonGia.TabIndex = 9;
        // 
        // tableLayoutPanel23
        // 
        tableLayoutPanel23.BackColor = SystemColors.Control;
        tableLayoutPanel23.ColumnCount = 1;
        tableLayoutPanel23.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel23.Controls.Add(label24, 0, 0);
        tableLayoutPanel23.Controls.Add(txtSoLuongTon, 0, 1);
        tableLayoutPanel23.Dock = DockStyle.Fill;
        tableLayoutPanel23.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel23.ForeColor = SystemColors.ControlText;
        tableLayoutPanel23.Location = new Point(0, 336);
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
        label24.BackColor = SystemColors.Control;
        label24.Dock = DockStyle.Fill;
        label24.Font = new Font("Segoe UI", 10.2F);
        label24.ForeColor = SystemColors.ControlText;
        label24.Location = new Point(3, 0);
        label24.Name = "label24";
        label24.Size = new Size(324, 22);
        label24.TabIndex = 0;
        label24.Text = "Tồn kho (cập nhật qua nhập kho)";
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
        txtSoLuongTon.ForeColor = SystemColors.ControlText;
        txtSoLuongTon.Location = new Point(0, 23);
        txtSoLuongTon.Margin = new Padding(0, 1, 0, 5);
        txtSoLuongTon.Name = "txtSoLuongTon";
        txtSoLuongTon.Size = new Size(330, 30);
        txtSoLuongTon.TabIndex = 10;
        // 
        // tableLayoutPanel25
        // 
        tableLayoutPanel25.AutoSize = true;
        tableLayoutPanel25.BackColor = SystemColors.Control;
        tableLayoutPanel25.ColumnCount = 2;
        tableLayoutPanel25.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel25.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel25.Controls.Add(btnThem, 0, 0);
        tableLayoutPanel25.Controls.Add(btnSua, 1, 0);
        tableLayoutPanel25.Controls.Add(btnXoa, 0, 1);
        tableLayoutPanel25.Controls.Add(btnLuu, 1, 1);
        tableLayoutPanel25.Controls.Add(btnBoQua, 0, 2);
        tableLayoutPanel25.Controls.Add(btnThoat, 1, 2);
        tableLayoutPanel25.Dock = DockStyle.Fill;
        tableLayoutPanel25.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel25.ForeColor = SystemColors.ControlText;
        tableLayoutPanel25.Location = new Point(0, 412);
        tableLayoutPanel25.Margin = new Padding(0);
        tableLayoutPanel25.Name = "tableLayoutPanel25";
        tableLayoutPanel25.RowCount = 3;
        tableLayoutPanel25.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel25.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel25.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel25.Size = new Size(330, 132);
        tableLayoutPanel25.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(192, 255, 192);
        btnThem.Cursor = Cursors.Hand;
        btnThem.Dock = DockStyle.Fill;
        btnThem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThem.ForeColor = SystemColors.ControlText;
        btnThem.Location = new Point(0, 4);
        btnThem.Margin = new Padding(0, 4, 4, 0);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(161, 40);
        btnThem.TabIndex = 13;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = SystemColors.ControlLight;
        btnSua.Cursor = Cursors.Hand;
        btnSua.Dock = DockStyle.Fill;
        btnSua.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnSua.ForeColor = SystemColors.ControlText;
        btnSua.Location = new Point(169, 4);
        btnSua.Margin = new Padding(4, 4, 0, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(161, 40);
        btnSua.TabIndex = 14;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.FromArgb(255, 192, 192);
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.Dock = DockStyle.Fill;
        btnXoa.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnXoa.ForeColor = SystemColors.ControlText;
        btnXoa.Location = new Point(0, 48);
        btnXoa.Margin = new Padding(0, 4, 4, 0);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(161, 40);
        btnXoa.TabIndex = 15;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(192, 255, 192);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Dock = DockStyle.Fill;
        btnLuu.Enabled = false;
        btnLuu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLuu.ForeColor = SystemColors.ControlText;
        btnLuu.Location = new Point(169, 48);
        btnLuu.Margin = new Padding(4, 4, 0, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(161, 40);
        btnLuu.TabIndex = 16;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // btnBoQua
        // 
        btnBoQua.BackColor = SystemColors.ControlLight;
        btnBoQua.Cursor = Cursors.Hand;
        btnBoQua.Dock = DockStyle.Fill;
        btnBoQua.Enabled = false;
        btnBoQua.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnBoQua.FlatStyle = FlatStyle.Flat;
        btnBoQua.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnBoQua.ForeColor = SystemColors.ControlText;
        btnBoQua.Location = new Point(0, 92);
        btnBoQua.Margin = new Padding(0, 4, 4, 0);
        btnBoQua.Name = "btnBoQua";
        btnBoQua.Size = new Size(161, 40);
        btnBoQua.TabIndex = 17;
        btnBoQua.Text = "Bỏ qua";
        btnBoQua.UseVisualStyleBackColor = false;
        btnBoQua.Click += btnBoQua_Click;
        // 
        // btnThoat
        // 
        btnThoat.BackColor = SystemColors.ControlLight;
        btnThoat.Cursor = Cursors.Hand;
        btnThoat.Dock = DockStyle.Fill;
        btnThoat.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThoat.FlatStyle = FlatStyle.Flat;
        btnThoat.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThoat.ForeColor = SystemColors.ControlText;
        btnThoat.Location = new Point(169, 92);
        btnThoat.Margin = new Padding(4, 4, 0, 0);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(161, 40);
        btnThoat.TabIndex = 21;
        btnThoat.Text = "Thoát";
        btnThoat.UseVisualStyleBackColor = false;
        btnThoat.Click += btnThoat_Click;
        // 
        // frmQuanLyThuoc
        // 
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = SystemColors.Control;
        ClientSize = new Size(1182, 653);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = SystemColors.ControlText;
        MinimumSize = new Size(1060, 700);
        Name = "frmQuanLyThuoc";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý thuốc | Hiệu thuốc ABC";
        Load += frmQuanLyThuoc_Load;
        posLayout.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
        groupBox6.ResumeLayout(false);
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel9.ResumeLayout(false);
        tableLayoutPanel9.PerformLayout();
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
        ResumeLayout(false);
    }

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
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn7;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn8;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvThuocColumn9;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    private global::System.Windows.Forms.Label label10;
    private global::System.Windows.Forms.TextBox txtMaThuoc;
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
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLuu;
    private global::System.Windows.Forms.Button btnBoQua;
    private global::System.Windows.Forms.Button btnThoat;
}
