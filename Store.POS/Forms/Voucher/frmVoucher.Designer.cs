#nullable disable
namespace Store.POS;

partial class frmVoucher
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
        tableLayoutPanel2 = new TableLayoutPanel();
        groupBox3 = new GroupBox();
        tableLayoutPanel4 = new TableLayoutPanel();
        tableLayoutPanel5 = new TableLayoutPanel();
        label6 = new Label();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        dgvVoucher = new DataGridView();
        groupBox7 = new GroupBox();
        tableLayoutPanel8 = new TableLayoutPanel();
        tableLayoutPanel9 = new TableLayoutPanel();
        tableLayoutPanel10 = new TableLayoutPanel();
        label11 = new Label();
        txtCode = new TextBox();
        tableLayoutPanel12 = new TableLayoutPanel();
        label13 = new Label();
        cboLoai = new ComboBox();
        tableLayoutPanel14 = new TableLayoutPanel();
        label15 = new Label();
        nudGiaTri = new NumericUpDown();
        tableLayoutPanel16 = new TableLayoutPanel();
        label17 = new Label();
        nudSoLuot = new NumericUpDown();
        tableLayoutPanel18 = new TableLayoutPanel();
        label19 = new Label();
        chkHoatDong = new CheckBox();
        tableLayoutPanel20 = new TableLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnLuu = new Button();
        btnHuy = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        btnDong = new Button();
        lblTrangThai = new Label();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        groupBox3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVoucher).BeginInit();
        groupBox7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel9.SuspendLayout();
        tableLayoutPanel10.SuspendLayout();
        tableLayoutPanel12.SuspendLayout();
        tableLayoutPanel14.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudGiaTri).BeginInit();
        tableLayoutPanel16.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudSoLuot).BeginInit();
        tableLayoutPanel18.SuspendLayout();
        tableLayoutPanel20.SuspendLayout();
        SuspendLayout();
        // 
        // posLayout
        // 
        posLayout.BackColor = Color.FromArgb(248, 250, 252);
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
        posLayout.Size = new Size(1330, 729);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.BackColor = Color.FromArgb(248, 250, 252);
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.ForeColor = SystemColors.ControlText;
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1300, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Quản lý voucher";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
        tableLayoutPanel1.Controls.Add(lblTrangThai, 0, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
        tableLayoutPanel1.Location = new Point(12, 60);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.Size = new Size(1306, 657);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 354F));
        tableLayoutPanel2.Controls.Add(groupBox3, 0, 0);
        tableLayoutPanel2.Controls.Add(groupBox7, 1, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel2.ForeColor = SystemColors.ControlText;
        tableLayoutPanel2.Location = new Point(0, 0);
        tableLayoutPanel2.Margin = new Padding(0);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Size = new Size(1306, 617);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // groupBox3
        // 
        groupBox3.BackColor = Color.FromArgb(248, 250, 252);
        groupBox3.Controls.Add(tableLayoutPanel4);
        groupBox3.Dock = DockStyle.Fill;
        groupBox3.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox3.ForeColor = SystemColors.ControlText;
        groupBox3.Location = new Point(0, 0);
        groupBox3.Margin = new Padding(0, 0, 12, 0);
        groupBox3.Name = "groupBox3";
        groupBox3.Padding = new Padding(12, 2, 12, 12);
        groupBox3.Size = new Size(940, 617);
        groupBox3.TabIndex = 0;
        groupBox3.TabStop = false;
        groupBox3.Text = "Danh sách voucher";
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel4.ColumnCount = 1;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
        tableLayoutPanel4.Controls.Add(dgvVoucher, 0, 1);
        tableLayoutPanel4.Dock = DockStyle.Fill;
        tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel4.ForeColor = SystemColors.ControlText;
        tableLayoutPanel4.Location = new Point(12, 30);
        tableLayoutPanel4.Margin = new Padding(0);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 2;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Size = new Size(916, 575);
        tableLayoutPanel4.TabIndex = 0;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel5.ColumnCount = 3;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutPanel5.Controls.Add(label6, 0, 0);
        tableLayoutPanel5.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel5.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel5.Dock = DockStyle.Fill;
        tableLayoutPanel5.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel5.ForeColor = SystemColors.ControlText;
        tableLayoutPanel5.Location = new Point(0, 0);
        tableLayoutPanel5.Margin = new Padding(0, 0, 0, 12);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 1;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.Size = new Size(916, 44);
        tableLayoutPanel5.TabIndex = 0;
        // 
        // label6
        // 
        label6.BackColor = Color.FromArgb(248, 250, 252);
        label6.Dock = DockStyle.Fill;
        label6.Font = new Font("Segoe UI", 10.2F);
        label6.ForeColor = SystemColors.ControlText;
        label6.Location = new Point(3, 0);
        label6.Name = "label6";
        label6.Size = new Size(114, 44);
        label6.TabIndex = 0;
        label6.Text = "Mã voucher";
        label6.TextAlign = ContentAlignment.MiddleLeft;
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
        txtTimKiem.PlaceholderText = "Nhập mã voucher";
        txtTimKiem.Size = new Size(664, 35);
        txtTimKiem.TabIndex = 1;
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
        btnTimKiem.Location = new Point(796, 2);
        btnTimKiem.Margin = new Padding(0, 2, 0, 2);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(120, 40);
        btnTimKiem.TabIndex = 2;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        // 
        // dgvVoucher
        // 
        dgvVoucher.AllowUserToAddRows = false;
        dgvVoucher.AllowUserToDeleteRows = false;
        dgvVoucher.AllowUserToResizeColumns = false;
        dgvVoucher.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
        dgvVoucher.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvVoucher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvVoucher.BackgroundColor = Color.White;
        dgvVoucher.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvVoucher.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvVoucher.ColumnHeadersHeight = 38;
        dgvVoucher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.Padding = new Padding(4);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvVoucher.DefaultCellStyle = dataGridViewCellStyle3;
        dgvVoucher.Dock = DockStyle.Fill;
        dgvVoucher.EnableHeadersVisualStyles = false;
        dgvVoucher.Font = new Font("Segoe UI", 10.2F);
        dgvVoucher.GridColor = SystemColors.ControlLight;
        dgvVoucher.Location = new Point(0, 56);
        dgvVoucher.Margin = new Padding(0);
        dgvVoucher.MultiSelect = false;
        dgvVoucher.Name = "dgvVoucher";
        dgvVoucher.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvVoucher.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvVoucher.RowHeadersVisible = false;
        dgvVoucher.RowHeadersWidth = 62;
        dgvVoucher.RowTemplate.Height = 34;
        dgvVoucher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVoucher.Size = new Size(916, 519);
        dgvVoucher.TabIndex = 1;
        // 
        // groupBox7
        // 
        groupBox7.BackColor = Color.FromArgb(248, 250, 252);
        groupBox7.Controls.Add(tableLayoutPanel8);
        groupBox7.Dock = DockStyle.Fill;
        groupBox7.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox7.ForeColor = SystemColors.ControlText;
        groupBox7.Location = new Point(952, 0);
        groupBox7.Margin = new Padding(0);
        groupBox7.Name = "groupBox7";
        groupBox7.Padding = new Padding(12, 2, 12, 12);
        groupBox7.Size = new Size(354, 617);
        groupBox7.TabIndex = 1;
        groupBox7.TabStop = false;
        groupBox7.Text = "Thông tin chi tiết";
        // 
        // tableLayoutPanel8
        // 
        tableLayoutPanel8.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 0);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel20, 0, 2);
        tableLayoutPanel8.Dock = DockStyle.Fill;
        tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel8.ForeColor = SystemColors.ControlText;
        tableLayoutPanel8.Location = new Point(12, 30);
        tableLayoutPanel8.Margin = new Padding(0);
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.RowCount = 3;
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 280F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 176F));
        tableLayoutPanel8.Size = new Size(330, 575);
        tableLayoutPanel8.TabIndex = 0;
        // 
        // tableLayoutPanel9
        // 
        tableLayoutPanel9.AutoSize = true;
        tableLayoutPanel9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel9.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel9.ColumnCount = 1;
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel9.Controls.Add(tableLayoutPanel10);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel12);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel14);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel16);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel18);
        tableLayoutPanel9.Dock = DockStyle.Top;
        tableLayoutPanel9.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel9.ForeColor = SystemColors.ControlText;
        tableLayoutPanel9.Location = new Point(0, 0);
        tableLayoutPanel9.Margin = new Padding(0);
        tableLayoutPanel9.Name = "tableLayoutPanel9";
        tableLayoutPanel9.RowCount = 5;
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel9.Size = new Size(330, 280);
        tableLayoutPanel9.TabIndex = 0;
        // 
        // tableLayoutPanel10
        // 
        tableLayoutPanel10.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel10.ColumnCount = 1;
        tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel10.Controls.Add(label11, 0, 0);
        tableLayoutPanel10.Controls.Add(txtCode, 0, 1);
        tableLayoutPanel10.Dock = DockStyle.Fill;
        tableLayoutPanel10.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel10.ForeColor = SystemColors.ControlText;
        tableLayoutPanel10.Location = new Point(0, 0);
        tableLayoutPanel10.Margin = new Padding(0);
        tableLayoutPanel10.Name = "tableLayoutPanel10";
        tableLayoutPanel10.RowCount = 2;
        tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel10.Size = new Size(330, 56);
        tableLayoutPanel10.TabIndex = 0;
        // 
        // label11
        // 
        label11.BackColor = Color.FromArgb(248, 250, 252);
        label11.Dock = DockStyle.Fill;
        label11.Font = new Font("Segoe UI", 10.2F);
        label11.ForeColor = SystemColors.ControlText;
        label11.Location = new Point(3, 0);
        label11.Name = "label11";
        label11.Size = new Size(324, 22);
        label11.TabIndex = 0;
        label11.Text = "Mã voucher";
        label11.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtCode
        // 
        txtCode.BackColor = Color.White;
        txtCode.BorderStyle = BorderStyle.FixedSingle;
        txtCode.CharacterCasing = CharacterCasing.Upper;
        txtCode.Cursor = Cursors.IBeam;
        txtCode.Dock = DockStyle.Top;
        txtCode.Enabled = false;
        txtCode.Font = new Font("Segoe UI", 10.2F);
        txtCode.ForeColor = SystemColors.ControlText;
        txtCode.Location = new Point(0, 23);
        txtCode.Margin = new Padding(0, 1, 0, 5);
        txtCode.MaxLength = 20;
        txtCode.Name = "txtCode";
        txtCode.Size = new Size(330, 35);
        txtCode.TabIndex = 1;
        // 
        // tableLayoutPanel12
        // 
        tableLayoutPanel12.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel12.ColumnCount = 1;
        tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel12.Controls.Add(label13, 0, 0);
        tableLayoutPanel12.Controls.Add(cboLoai, 0, 1);
        tableLayoutPanel12.Dock = DockStyle.Fill;
        tableLayoutPanel12.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel12.ForeColor = SystemColors.ControlText;
        tableLayoutPanel12.Location = new Point(0, 56);
        tableLayoutPanel12.Margin = new Padding(0);
        tableLayoutPanel12.Name = "tableLayoutPanel12";
        tableLayoutPanel12.RowCount = 2;
        tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel12.Size = new Size(330, 56);
        tableLayoutPanel12.TabIndex = 1;
        // 
        // label13
        // 
        label13.BackColor = Color.FromArgb(248, 250, 252);
        label13.Dock = DockStyle.Fill;
        label13.Font = new Font("Segoe UI", 10.2F);
        label13.ForeColor = SystemColors.ControlText;
        label13.Location = new Point(3, 0);
        label13.Name = "label13";
        label13.Size = new Size(324, 22);
        label13.TabIndex = 0;
        label13.Text = "Loại giảm giá";
        label13.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cboLoai
        // 
        cboLoai.BackColor = SystemColors.Window;
        cboLoai.Dock = DockStyle.Top;
        cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLoai.DropDownWidth = 330;
        cboLoai.Enabled = false;
        cboLoai.Font = new Font("Segoe UI", 10.2F);
        cboLoai.ForeColor = SystemColors.ControlText;
        cboLoai.Location = new Point(0, 23);
        cboLoai.Margin = new Padding(0, 1, 0, 5);
        cboLoai.Name = "cboLoai";
        cboLoai.Size = new Size(330, 36);
        cboLoai.TabIndex = 1;
        // 
        // tableLayoutPanel14
        // 
        tableLayoutPanel14.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel14.ColumnCount = 1;
        tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel14.Controls.Add(label15, 0, 0);
        tableLayoutPanel14.Controls.Add(nudGiaTri, 0, 1);
        tableLayoutPanel14.Dock = DockStyle.Fill;
        tableLayoutPanel14.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel14.ForeColor = SystemColors.ControlText;
        tableLayoutPanel14.Location = new Point(0, 112);
        tableLayoutPanel14.Margin = new Padding(0);
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.RowCount = 2;
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel14.Size = new Size(330, 56);
        tableLayoutPanel14.TabIndex = 2;
        // 
        // label15
        // 
        label15.BackColor = Color.FromArgb(248, 250, 252);
        label15.Dock = DockStyle.Fill;
        label15.Font = new Font("Segoe UI", 10.2F);
        label15.ForeColor = SystemColors.ControlText;
        label15.Location = new Point(3, 0);
        label15.Name = "label15";
        label15.Size = new Size(324, 22);
        label15.TabIndex = 0;
        label15.Text = "Giá trị giảm (đ hoặc %)";
        label15.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudGiaTri
        // 
        nudGiaTri.BackColor = Color.White;
        nudGiaTri.Dock = DockStyle.Top;
        nudGiaTri.Enabled = false;
        nudGiaTri.Font = new Font("Segoe UI", 10.2F);
        nudGiaTri.ForeColor = SystemColors.ControlText;
        nudGiaTri.Location = new Point(0, 23);
        nudGiaTri.Margin = new Padding(0, 1, 0, 5);
        nudGiaTri.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 131072 });
        nudGiaTri.Name = "nudGiaTri";
        nudGiaTri.Size = new Size(330, 35);
        nudGiaTri.TabIndex = 1;
        nudGiaTri.ThousandsSeparator = true;
        // 
        // tableLayoutPanel16
        // 
        tableLayoutPanel16.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel16.ColumnCount = 1;
        tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel16.Controls.Add(label17, 0, 0);
        tableLayoutPanel16.Controls.Add(nudSoLuot, 0, 1);
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
        label17.BackColor = Color.FromArgb(248, 250, 252);
        label17.Dock = DockStyle.Fill;
        label17.Font = new Font("Segoe UI", 10.2F);
        label17.ForeColor = SystemColors.ControlText;
        label17.Location = new Point(3, 0);
        label17.Name = "label17";
        label17.Size = new Size(324, 22);
        label17.TabIndex = 0;
        label17.Text = "Số lượt còn lại";
        label17.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSoLuot
        // 
        nudSoLuot.BackColor = Color.White;
        nudSoLuot.Dock = DockStyle.Top;
        nudSoLuot.Enabled = false;
        nudSoLuot.Font = new Font("Segoe UI", 10.2F);
        nudSoLuot.ForeColor = SystemColors.ControlText;
        nudSoLuot.Location = new Point(0, 23);
        nudSoLuot.Margin = new Padding(0, 1, 0, 5);
        nudSoLuot.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        nudSoLuot.Name = "nudSoLuot";
        nudSoLuot.Size = new Size(330, 35);
        nudSoLuot.TabIndex = 1;
        nudSoLuot.ThousandsSeparator = true;
        // 
        // tableLayoutPanel18
        // 
        tableLayoutPanel18.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel18.ColumnCount = 1;
        tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel18.Controls.Add(label19, 0, 0);
        tableLayoutPanel18.Controls.Add(chkHoatDong, 0, 1);
        tableLayoutPanel18.Dock = DockStyle.Fill;
        tableLayoutPanel18.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel18.ForeColor = SystemColors.ControlText;
        tableLayoutPanel18.Location = new Point(0, 224);
        tableLayoutPanel18.Margin = new Padding(0);
        tableLayoutPanel18.Name = "tableLayoutPanel18";
        tableLayoutPanel18.RowCount = 2;
        tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel18.Size = new Size(330, 56);
        tableLayoutPanel18.TabIndex = 4;
        // 
        // label19
        // 
        label19.BackColor = Color.FromArgb(248, 250, 252);
        label19.Dock = DockStyle.Fill;
        label19.Font = new Font("Segoe UI", 10.2F);
        label19.ForeColor = SystemColors.ControlText;
        label19.Location = new Point(3, 0);
        label19.Name = "label19";
        label19.Size = new Size(324, 22);
        label19.TabIndex = 0;
        label19.Text = "Trạng thái";
        label19.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // chkHoatDong
        // 
        chkHoatDong.BackColor = Color.FromArgb(248, 250, 252);
        chkHoatDong.Checked = true;
        chkHoatDong.CheckState = CheckState.Checked;
        chkHoatDong.Dock = DockStyle.Top;
        chkHoatDong.Enabled = false;
        chkHoatDong.Font = new Font("Segoe UI", 10.2F);
        chkHoatDong.ForeColor = SystemColors.ControlText;
        chkHoatDong.Location = new Point(0, 23);
        chkHoatDong.Margin = new Padding(0, 1, 0, 5);
        chkHoatDong.Name = "chkHoatDong";
        chkHoatDong.Size = new Size(330, 24);
        chkHoatDong.TabIndex = 1;
        chkHoatDong.Text = "Đang hoạt động";
        chkHoatDong.UseVisualStyleBackColor = true;
        // 
        // tableLayoutPanel20
        // 
        tableLayoutPanel20.AutoSize = true;
        tableLayoutPanel20.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel20.ColumnCount = 2;
        tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel20.Controls.Add(btnThem, 0, 0);
        tableLayoutPanel20.Controls.Add(btnSua, 1, 0);
        tableLayoutPanel20.Controls.Add(btnLuu, 0, 1);
        tableLayoutPanel20.Controls.Add(btnHuy, 1, 1);
        tableLayoutPanel20.Controls.Add(btnXoa, 0, 2);
        tableLayoutPanel20.Controls.Add(btnLamMoi, 1, 2);
        tableLayoutPanel20.Controls.Add(btnDong, 0, 3);
        tableLayoutPanel20.Dock = DockStyle.Fill;
        tableLayoutPanel20.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel20.ForeColor = SystemColors.ControlText;
        tableLayoutPanel20.Location = new Point(0, 399);
        tableLayoutPanel20.Margin = new Padding(0);
        tableLayoutPanel20.Name = "tableLayoutPanel20";
        tableLayoutPanel20.RowCount = 4;
        tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel20.Size = new Size(330, 176);
        tableLayoutPanel20.TabIndex = 1;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(22, 101, 52);
        btnThem.Cursor = Cursors.Hand;
        btnThem.Dock = DockStyle.Fill;
        btnThem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(0, 4);
        btnThem.Margin = new Padding(0, 4, 4, 0);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(161, 40);
        btnThem.TabIndex = 0;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = false;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(15, 118, 110);
        btnSua.Cursor = Cursors.Hand;
        btnSua.Dock = DockStyle.Fill;
        btnSua.Enabled = false;
        btnSua.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(169, 4);
        btnSua.Margin = new Padding(4, 4, 0, 0);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(161, 40);
        btnSua.TabIndex = 1;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = false;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(22, 101, 52);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Dock = DockStyle.Fill;
        btnLuu.Enabled = false;
        btnLuu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(0, 48);
        btnLuu.Margin = new Padding(0, 4, 4, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(161, 40);
        btnLuu.TabIndex = 2;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(100, 116, 139);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.Dock = DockStyle.Fill;
        btnHuy.Enabled = false;
        btnHuy.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnHuy.ForeColor = Color.White;
        btnHuy.Location = new Point(169, 48);
        btnHuy.Margin = new Padding(4, 4, 0, 0);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(161, 40);
        btnHuy.TabIndex = 3;
        btnHuy.Text = "Hủy";
        btnHuy.UseVisualStyleBackColor = false;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.FromArgb(225, 29, 72);
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.Dock = DockStyle.Fill;
        btnXoa.Enabled = false;
        btnXoa.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(0, 92);
        btnXoa.Margin = new Padding(0, 4, 4, 0);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(161, 40);
        btnXoa.TabIndex = 4;
        btnXoa.Text = "Xóa";
        btnXoa.UseVisualStyleBackColor = false;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(15, 118, 110);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.Dock = DockStyle.Fill;
        btnLamMoi.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(169, 92);
        btnLamMoi.Margin = new Padding(4, 4, 0, 0);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(161, 40);
        btnLamMoi.TabIndex = 5;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        // 
        // btnDong
        // 
        btnDong.BackColor = Color.FromArgb(100, 116, 139);
        tableLayoutPanel20.SetColumnSpan(btnDong, 2);
        btnDong.Cursor = Cursors.Hand;
        btnDong.Dock = DockStyle.Fill;
        btnDong.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnDong.FlatStyle = FlatStyle.Flat;
        btnDong.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnDong.ForeColor = Color.White;
        btnDong.Location = new Point(0, 136);
        btnDong.Margin = new Padding(0, 4, 4, 0);
        btnDong.Name = "btnDong";
        btnDong.Size = new Size(326, 40);
        btnDong.TabIndex = 6;
        btnDong.Text = "Đóng";
        btnDong.UseVisualStyleBackColor = false;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoEllipsis = true;
        lblTrangThai.BackColor = Color.FromArgb(248, 250, 252);
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Font = new Font("Segoe UI", 10.2F);
        lblTrangThai.ForeColor = SystemColors.ControlText;
        lblTrangThai.Location = new Point(3, 617);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(1300, 40);
        lblTrangThai.TabIndex = 1;
        // 
        // frmVoucher
        // 
        AutoScaleDimensions = new SizeF(144F, 144F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1330, 729);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = Color.FromArgb(15, 23, 42);
        MinimumSize = new Size(1060, 700);
        Name = "frmVoucher";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý voucher | Hiệu thuốc ABC";
        posLayout.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVoucher).EndInit();
        groupBox7.ResumeLayout(false);
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel8.PerformLayout();
        tableLayoutPanel9.ResumeLayout(false);
        tableLayoutPanel10.ResumeLayout(false);
        tableLayoutPanel10.PerformLayout();
        tableLayoutPanel12.ResumeLayout(false);
        tableLayoutPanel14.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudGiaTri).EndInit();
        tableLayoutPanel16.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudSoLuot).EndInit();
        tableLayoutPanel18.ResumeLayout(false);
        tableLayoutPanel20.ResumeLayout(false);
        ResumeLayout(false);
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private global::System.Windows.Forms.GroupBox groupBox3;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.Label label6;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.DataGridView dgvVoucher;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn6;
    private global::System.Windows.Forms.DataGridViewCheckBoxColumn dgvVoucherColumn7;
    private global::System.Windows.Forms.GroupBox groupBox7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.Label label11;
    private global::System.Windows.Forms.TextBox txtCode;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
    private global::System.Windows.Forms.Label label13;
    private global::System.Windows.Forms.ComboBox cboLoai;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.Label label15;
    private global::System.Windows.Forms.NumericUpDown nudGiaTri;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
    private global::System.Windows.Forms.Label label17;
    private global::System.Windows.Forms.NumericUpDown nudSoLuot;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
    private global::System.Windows.Forms.Label label19;
    private global::System.Windows.Forms.CheckBox chkHoatDong;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnLuu;
    private global::System.Windows.Forms.Button btnHuy;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLamMoi;
    private global::System.Windows.Forms.Button btnDong;
    private global::System.Windows.Forms.Label lblTrangThai;
}
