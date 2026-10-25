#nullable disable
namespace Store.POS;

partial class frmKhachHang
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
        posHeading = new Label();
        posLayout = new TableLayoutPanel();
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        tableLayoutPanel4 = new TableLayoutPanel();
        label5 = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvKhachHang = new DataGridView();
        MaKH = new DataGridViewTextBoxColumn();
        TenKH = new DataGridViewTextBoxColumn();
        DienThoai = new DataGridViewTextBoxColumn();
        Diem = new DataGridViewTextBoxColumn();

        groupBox6 = new GroupBox();
        tableLayoutPanel7 = new TableLayoutPanel();
        tableLayoutPanel8 = new TableLayoutPanel();
        tableLayoutPanel11 = new TableLayoutPanel();
        label12 = new Label();
        txtTenKH = new TextBox();
        tableLayoutPanel13 = new TableLayoutPanel();
        label14 = new Label();
        txtDienThoai = new TextBox();
        tableLayoutPanel15 = new TableLayoutPanel();
        label16 = new Label();
        txtDiem = new TextBox();
        tableLayoutPanel17 = new TableLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamLai = new Button();
        btnThoat = new Button();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
        groupBox6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel11.SuspendLayout();
        tableLayoutPanel13.SuspendLayout();
        tableLayoutPanel15.SuspendLayout();
        tableLayoutPanel17.SuspendLayout();
        SuspendLayout();
        // 
        // 
        // 
        // posHeading
        // 
        posHeading.BackColor = Color.FromArgb(248,250,252);
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.ForeColor = SystemColors.ControlText;
        posHeading.Location = new Point(3, 0);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1026, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Khách hàng";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // 
        // 
        // posLayout
        // 
        posLayout.BackColor = Color.FromArgb(248,250,252);
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
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(248,250,252);
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
        groupBox2.BackColor = Color.FromArgb(248,250,252);
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
        groupBox2.Text = "Danh sách khách hàng";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel3.Controls.Add(dgvKhachHang, 0, 1);
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
        tableLayoutPanel4.BackColor = Color.FromArgb(248,250,252);
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
        label5.BackColor = Color.FromArgb(248,250,252);
        label5.Dock = DockStyle.Fill;
        label5.Font = new Font("Segoe UI", 10.2F);
        label5.ForeColor = SystemColors.ControlText;
        label5.Location = new Point(3, 0);
        label5.Name = "label5";
        label5.Size = new Size(114, 44);
        label5.TabIndex = 0;
        label5.Text = "Khách hàng";
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
        txtTimKiem.PlaceholderText = "Tên hoặc số điện thoại";
        txtTimKiem.Size = new Size(516, 30);
        txtTimKiem.TabIndex = 2;
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
        btnTimKiem.TabIndex = 0;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        btnTimKiem.Click += btnTimKiem_Click;
        // 
        // dgvKhachHang
        // 
        dgvKhachHang.AllowUserToAddRows = false;
        dgvKhachHang.AllowUserToDeleteRows = false;
        dgvKhachHang.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.White;
        dgvKhachHang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvKhachHang.AutoGenerateColumns = false;
        dgvKhachHang.Columns.AddRange(new DataGridViewColumn[] { MaKH, TenKH, DienThoai, Diem });
        MaKH.Name="MaKH";MaKH.DataPropertyName="MaKH";MaKH.HeaderText="Mã khách hàng";MaKH.ReadOnly=true;MaKH.FillWeight=80F;
        TenKH.Name="TenKH";TenKH.DataPropertyName="TenKH";TenKH.HeaderText="Tên khách hàng";TenKH.ReadOnly=true;TenKH.FillWeight=180F;
        DienThoai.Name="DienThoai";DienThoai.DataPropertyName="DienThoai";DienThoai.HeaderText="Điện thoại";DienThoai.ReadOnly=true;DienThoai.FillWeight=120F;
        Diem.Name="Diem";Diem.DataPropertyName="Diem";Diem.HeaderText="Điểm";Diem.ReadOnly=true;Diem.FillWeight=65F;
        dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvKhachHang.BackgroundColor = SystemColors.ScrollBar;
        dgvKhachHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(248,250,252);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvKhachHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.Padding = new Padding(4);
        dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientActiveCaption;
        dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvKhachHang.DefaultCellStyle = dataGridViewCellStyle3;
        dgvKhachHang.Dock = DockStyle.Fill;
        dgvKhachHang.EnableHeadersVisualStyles = false;
        dgvKhachHang.Font = new Font("Segoe UI", 10.2F);
        dgvKhachHang.GridColor = SystemColors.ControlLight;
        dgvKhachHang.Location = new Point(0, 56);
        dgvKhachHang.Margin = new Padding(0);
        dgvKhachHang.MultiSelect = false;
        dgvKhachHang.Name = "dgvKhachHang";
        dgvKhachHang.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(248,250,252);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvKhachHang.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvKhachHang.RowHeadersVisible = false;
        dgvKhachHang.RowHeadersWidth = 51;
        dgvKhachHang.RowTemplate.Height = 34;
        dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKhachHang.Size = new Size(768, 488);
        dgvKhachHang.TabIndex = 4;
        dgvKhachHang.CellClick += dgvKhachHang_CellClick;
        // 
        // groupBox6
        // 
        groupBox6.BackColor = Color.FromArgb(248,250,252);
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
        tableLayoutPanel7.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 0);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel17, 0, 2);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel7.ForeColor = SystemColors.ControlText;
        tableLayoutPanel7.Location = new Point(12, 25);
        tableLayoutPanel7.Margin = new Padding(0);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 3;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 168F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
        tableLayoutPanel7.Size = new Size(330, 544);
        tableLayoutPanel7.TabIndex = 0;
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.AutoSize = true;
        tableLayoutPanel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel8.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Controls.Add(tableLayoutPanel11);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel13);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel15);
        tableLayoutPanel8.Dock = DockStyle.Top;
        tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel8.ForeColor = SystemColors.ControlText;
        tableLayoutPanel8.Location = new Point(0, 0);
        tableLayoutPanel8.Margin = new Padding(0);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.RowCount = 3;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel8.Size = new Size(330, 224);
        tableLayoutPanel8.TabIndex = 0;
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // tableLayoutPanel11
        // 
        tableLayoutPanel11.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel11.ColumnCount = 1;
        tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel11.Controls.Add(label12, 0, 0);
        tableLayoutPanel11.Controls.Add(txtTenKH, 0, 1);
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
        label12.BackColor = Color.FromArgb(248,250,252);
        label12.Dock = DockStyle.Fill;
        label12.Font = new Font("Segoe UI", 10.2F);
        label12.ForeColor = SystemColors.ControlText;
        label12.Location = new Point(3, 0);
        label12.Name = "label12";
        label12.Size = new Size(324, 22);
        label12.TabIndex = 0;
        label12.Text = "Tên khách hàng";
        label12.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTenKH
        // 
        txtTenKH.BackColor = Color.White;
        txtTenKH.BorderStyle = BorderStyle.FixedSingle;
        txtTenKH.Cursor = Cursors.IBeam;
        txtTenKH.Dock = DockStyle.Top;
        txtTenKH.Enabled = false;
        txtTenKH.Font = new Font("Segoe UI", 10.2F);
        txtTenKH.ForeColor = SystemColors.ControlText;
        txtTenKH.Location = new Point(0, 23);
        txtTenKH.Margin = new Padding(0, 1, 0, 5);
        txtTenKH.Name = "txtTenKH";
        txtTenKH.Size = new Size(330, 30);
        txtTenKH.TabIndex = 3;
        // 
        // tableLayoutPanel13
        // 
        tableLayoutPanel13.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel13.ColumnCount = 1;
        tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel13.Controls.Add(label14, 0, 0);
        tableLayoutPanel13.Controls.Add(txtDienThoai, 0, 1);
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
        label14.BackColor = Color.FromArgb(248,250,252);
        label14.Dock = DockStyle.Fill;
        label14.Font = new Font("Segoe UI", 10.2F);
        label14.ForeColor = SystemColors.ControlText;
        label14.Location = new Point(3, 0);
        label14.Name = "label14";
        label14.Size = new Size(324, 22);
        label14.TabIndex = 0;
        label14.Text = "Số điện thoại";
        label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDienThoai
        // 
        txtDienThoai.BackColor = Color.White;
        txtDienThoai.BorderStyle = BorderStyle.FixedSingle;
        txtDienThoai.Cursor = Cursors.IBeam;
        txtDienThoai.Dock = DockStyle.Top;
        txtDienThoai.Enabled = false;
        txtDienThoai.Font = new Font("Segoe UI", 10.2F);
        txtDienThoai.ForeColor = SystemColors.ControlText;
        txtDienThoai.Location = new Point(0, 23);
        txtDienThoai.Margin = new Padding(0, 1, 0, 5);
        txtDienThoai.MaxLength = 10;
        txtDienThoai.Name = "txtDienThoai";
        txtDienThoai.Size = new Size(330, 30);
        txtDienThoai.TabIndex = 8;
        // 
        // tableLayoutPanel15
        // 
        tableLayoutPanel15.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel15.ColumnCount = 1;
        tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel15.Controls.Add(label16, 0, 0);
        tableLayoutPanel15.Controls.Add(txtDiem, 0, 1);
        tableLayoutPanel15.Dock = DockStyle.Fill;
        tableLayoutPanel15.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel15.ForeColor = SystemColors.ControlText;
        tableLayoutPanel15.Location = new Point(0, 168);
        tableLayoutPanel15.Margin = new Padding(0);
        tableLayoutPanel15.Name = "tableLayoutPanel15";
        tableLayoutPanel15.RowCount = 2;
        tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel15.Size = new Size(330, 56);
        tableLayoutPanel15.TabIndex = 3;
        // 
        // label16
        // 
        label16.BackColor = Color.FromArgb(248,250,252);
        label16.Dock = DockStyle.Fill;
        label16.Font = new Font("Segoe UI", 10.2F);
        label16.ForeColor = SystemColors.ControlText;
        label16.Location = new Point(3, 0);
        label16.Name = "label16";
        label16.Size = new Size(324, 22);
        label16.TabIndex = 0;
        label16.Text = "Điểm tích lũy";
        label16.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDiem
        // 
        txtDiem.BackColor = Color.White;
        txtDiem.BorderStyle = BorderStyle.FixedSingle;
        txtDiem.Cursor = Cursors.IBeam;
        txtDiem.Dock = DockStyle.Top;
        txtDiem.Enabled = false;
        txtDiem.Font = new Font("Segoe UI", 10.2F);
        txtDiem.ForeColor = SystemColors.ControlText;
        txtDiem.Location = new Point(0, 23);
        txtDiem.Margin = new Padding(0, 1, 0, 5);
        txtDiem.Name = "txtDiem";
        txtDiem.Size = new Size(330, 30);
        txtDiem.TabIndex = 7;
        // 
        // tableLayoutPanel17
        // 
        tableLayoutPanel17.AutoSize = true;
        tableLayoutPanel17.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel17.ColumnCount = 2;
        tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel17.Controls.Add(btnThem, 0, 0);
        tableLayoutPanel17.Controls.Add(btnSua, 1, 0);
        tableLayoutPanel17.Controls.Add(btnXoa, 0, 1);
        tableLayoutPanel17.Controls.Add(btnLamLai, 1, 1);
        tableLayoutPanel17.Controls.Add(btnThoat, 0, 2);
        tableLayoutPanel17.Dock = DockStyle.Fill;
        tableLayoutPanel17.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel17.ForeColor = SystemColors.ControlText;
        tableLayoutPanel17.Location = new Point(0, 412);
        tableLayoutPanel17.Margin = new Padding(0);
        tableLayoutPanel17.Name = "tableLayoutPanel17";
        tableLayoutPanel17.RowCount = 3;
        tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel17.Size = new Size(330, 132);
        tableLayoutPanel17.TabIndex = 1;
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
        btnThem.TabIndex = 10;
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
        btnSua.TabIndex = 11;
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
        btnXoa.TabIndex = 12;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnLamLai
        // 
        btnLamLai.BackColor = SystemColors.ControlLight;
        btnLamLai.Cursor = Cursors.Hand;
        btnLamLai.Dock = DockStyle.Fill;
        btnLamLai.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLamLai.FlatStyle = FlatStyle.Flat;
        btnLamLai.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLamLai.ForeColor = SystemColors.ControlText;
        btnLamLai.Location = new Point(169, 48);
        btnLamLai.Margin = new Padding(4, 4, 0, 0);
        btnLamLai.Name = "btnLamLai";
        btnLamLai.Size = new Size(161, 40);
        btnLamLai.TabIndex = 13;
        btnLamLai.Text = "Làm lại";
        btnLamLai.UseVisualStyleBackColor = false;
        btnLamLai.Click += btnLamLai_Click;
        // 
        // btnThoat
        // 
        btnThoat.BackColor = SystemColors.ControlLight;
        tableLayoutPanel17.SetColumnSpan(btnThoat, 2);
        btnThoat.Cursor = Cursors.Hand;
        btnThoat.Dock = DockStyle.Fill;
        btnThoat.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThoat.FlatStyle = FlatStyle.Flat;
        btnThoat.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThoat.ForeColor = SystemColors.ControlText;
        btnThoat.Location = new Point(0, 92);
        btnThoat.Margin = new Padding(0, 4, 4, 0);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(326, 40);
        btnThoat.TabIndex = 14;
        btnThoat.Text = "Thoát";
        btnThoat.UseVisualStyleBackColor = false;
        btnThoat.Click += btnThoat_Click;
        // 
        // frmKhachHang
        // 
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(248,250,252);
        ClientSize = new Size(1182, 653);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = SystemColors.ControlText;
        MinimumSize = new Size(1060, 700);
        Name = "frmKhachHang";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Khách hàng | Hiệu thuốc ABC";
        Load += frmKhachHang_Load;
        posLayout.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
        groupBox6.ResumeLayout(false);
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel11.ResumeLayout(false);
        tableLayoutPanel11.PerformLayout();
        tableLayoutPanel13.ResumeLayout(false);
        tableLayoutPanel13.PerformLayout();
        tableLayoutPanel15.ResumeLayout(false);
        tableLayoutPanel15.PerformLayout();
        tableLayoutPanel17.ResumeLayout(false);
        
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnTimKiem.BackColor=Color.FromArgb(15, 118, 110);btnTimKiem.ForeColor=Color.White;btnTimKiem.UseVisualStyleBackColor=false;
        btnThem.BackColor=Color.FromArgb(22, 101, 52);btnThem.ForeColor=Color.White;btnThem.UseVisualStyleBackColor=false;
        btnSua.BackColor=Color.FromArgb(15, 118, 110);btnSua.ForeColor=Color.White;btnSua.UseVisualStyleBackColor=false;
        btnXoa.BackColor=Color.FromArgb(225, 29, 72);btnXoa.ForeColor=Color.White;btnXoa.UseVisualStyleBackColor=false;
        btnLamLai.BackColor=Color.FromArgb(15, 118, 110);btnLamLai.ForeColor=Color.White;btnLamLai.UseVisualStyleBackColor=false;
        btnThoat.BackColor=Color.FromArgb(100, 116, 139);btnThoat.ForeColor=Color.White;btnThoat.UseVisualStyleBackColor=false;
        dgvKhachHang.BackgroundColor=Color.White;dgvKhachHang.EnableHeadersVisualStyles=false;
        dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvKhachHang.DefaultCellStyle.BackColor=Color.White;dgvKhachHang.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvKhachHang.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvKhachHang.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
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
    private global::System.Windows.Forms.DataGridView dgvKhachHang;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn MaKH;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn DienThoai;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn Diem;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
    private global::System.Windows.Forms.Label label12;
    private global::System.Windows.Forms.TextBox txtTenKH;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
    private global::System.Windows.Forms.Label label14;
    private global::System.Windows.Forms.TextBox txtDienThoai;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
    private global::System.Windows.Forms.Label label16;
    private global::System.Windows.Forms.TextBox txtDiem;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLamLai;
    private global::System.Windows.Forms.Button btnThoat;
}
