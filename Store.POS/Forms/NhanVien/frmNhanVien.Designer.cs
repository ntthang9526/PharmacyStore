#nullable disable
namespace Store.POS;
partial class frmNhanVien
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        posLayout = new TableLayoutPanel();
        posHeading = new Label();
        content = new TableLayoutPanel();
        left = new TableLayoutPanel();
        search = new TableLayoutPanel();
        txtTimKiem = new TextBox();
        btnTimKiem = new Button();
        btnLamMoi = new Button();
        dgvNhanVien = new DataGridView();
        details = new GroupBox();
        detailLayout = new TableLayoutPanel();
        editorScroll = new Panel();
        editor = new TableLayoutPanel();
        lblField1 = new Label();
        txtHoTen = new TextBox();
        lblField2 = new Label();
        txtTaiKhoan = new TextBox();
        lblField3 = new Label();
        txtDienThoai = new TextBox();
        lblField4 = new Label();
        txtMatKhau = new TextBox();
        lblField5 = new Label();
        chkDangLam = new CheckBox();
        lblVaiTro = new Label();
        cboVaiTro = new ComboBox();
        lblField6 = new Label();
        actions = new FlowLayoutPanel();
        btnThem = new Button();
        btnSua = new Button();
        btnLuu = new Button();
        btnHuy = new Button();
        btnKhoa = new Button();
        lblTrangThai = new Label();
        passwordHint = new ToolTip(components);
        posLayout.SuspendLayout();
        content.SuspendLayout();
        left.SuspendLayout();
        search.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
        details.SuspendLayout();
        detailLayout.SuspendLayout();
        editorScroll.SuspendLayout();
        editor.SuspendLayout();
        actions.SuspendLayout();
        SuspendLayout();
        // 
        // posLayout
        // 
        posLayout.ColumnCount = 1;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(content, 0, 1);
        posLayout.Controls.Add(lblTrangThai, 0, 2);
        posLayout.Dock = DockStyle.Fill;
        posLayout.Location = new Point(0, 0);
        posLayout.Name = "posLayout";
        posLayout.Padding = new Padding(12);
        posLayout.RowCount = 3;
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        posLayout.Size = new Size(1182, 700);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1152, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Quản lý nhân viên";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // content
        // 
        content.ColumnCount = 2;
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        content.Controls.Add(left, 0, 0);
        content.Controls.Add(details, 1, 0);
        content.Dock = DockStyle.Fill;
        content.Location = new Point(12, 60);
        content.Margin = new Padding(0);
        content.Name = "content";
        content.RowCount = 1;
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        content.Size = new Size(1158, 588);
        content.TabIndex = 1;
        // 
        // left
        // 
        left.ColumnCount = 1;
        left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        left.Controls.Add(search, 0, 0);
        left.Controls.Add(dgvNhanVien, 0, 1);
        left.Dock = DockStyle.Fill;
        left.Location = new Point(0, 0);
        left.Margin = new Padding(0, 0, 12, 0);
        left.Name = "left";
        left.RowCount = 2;
        left.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        left.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        left.Size = new Size(740, 588);
        left.TabIndex = 0;
        // 
        // search
        // 
        search.ColumnCount = 3;
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        search.Controls.Add(txtTimKiem, 0, 0);
        search.Controls.Add(btnTimKiem, 1, 0);
        search.Controls.Add(btnLamMoi, 2, 0);
        search.Dock = DockStyle.Fill;
        search.Location = new Point(3, 3);
        search.Name = "search";
        search.RowCount = 1;
        search.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        search.Size = new Size(734, 42);
        search.TabIndex = 0;
        // 
        // txtTimKiem
        // 
        txtTimKiem.Dock = DockStyle.Fill;
        txtTimKiem.Location = new Point(3, 3);
        txtTimKiem.MaxLength = 200;
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Họ tên, tài khoản, điện thoại hoặc mã nhân viên";
        txtTimKiem.Size = new Size(528, 35);
        txtTimKiem.TabIndex = 0;
        // 
        // btnTimKiem
        // 
        btnTimKiem.BackColor = Color.FromArgb(15, 118, 110);
        btnTimKiem.Dock = DockStyle.Fill;
        btnTimKiem.ForeColor = Color.White;
        btnTimKiem.Location = new Point(537, 3);
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Size = new Size(94, 36);
        btnTimKiem.TabIndex = 1;
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.UseVisualStyleBackColor = false;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(15, 118, 110);
        btnLamMoi.Dock = DockStyle.Fill;
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(637, 3);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(94, 36);
        btnLamMoi.TabIndex = 2;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        // 
        // dgvNhanVien
        // 
        dgvNhanVien.AllowUserToAddRows = false;
        dgvNhanVien.AllowUserToDeleteRows = false;
        dgvNhanVien.AllowUserToResizeColumns = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
        dgvNhanVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        dgvNhanVien.BackgroundColor = Color.White;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvNhanVien.ColumnHeadersHeight = 38;
        dgvNhanVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvNhanVien.DefaultCellStyle = dataGridViewCellStyle3;
        dgvNhanVien.Dock = DockStyle.Fill;
        dgvNhanVien.EnableHeadersVisualStyles = false;
        dgvNhanVien.Location = new Point(3, 51);
        dgvNhanVien.MultiSelect = false;
        dgvNhanVien.Name = "dgvNhanVien";
        dgvNhanVien.ReadOnly = true;
        dgvNhanVien.RowHeadersVisible = false;
        dgvNhanVien.RowHeadersWidth = 62;
        dgvNhanVien.RowTemplate.Height = 34;
        dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvNhanVien.Size = new Size(734, 534);
        dgvNhanVien.TabIndex = 1;
        dgvNhanVien.CellContentClick += dgvNhanVien_CellContentClick;
        // 
        // details
        // 
        details.Controls.Add(detailLayout);
        details.Dock = DockStyle.Fill;
        details.Location = new Point(755, 3);
        details.Name = "details";
        details.Padding = new Padding(12);
        details.Size = new Size(400, 582);
        details.TabIndex = 1;
        details.TabStop = false;
        details.Text = "Thông tin nhân viên";
        // 
        // detailLayout
        // 
        detailLayout.ColumnCount = 1;
        detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        detailLayout.Controls.Add(editorScroll, 0, 0);
        detailLayout.Controls.Add(lblField6, 0, 1);
        detailLayout.Controls.Add(actions, 0, 2);
        detailLayout.Dock = DockStyle.Fill;
        detailLayout.Location = new Point(12, 40);
        detailLayout.Margin = new Padding(0);
        detailLayout.Name = "detailLayout";
        detailLayout.RowCount = 3;
        detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
        detailLayout.Size = new Size(376, 530);
        detailLayout.TabIndex = 0;
        // 
        // editorScroll
        // 
        editorScroll.AutoScroll = true;
        editorScroll.Controls.Add(editor);
        editorScroll.Dock = DockStyle.Fill;
        editorScroll.Location = new Point(0, 0);
        editorScroll.Margin = new Padding(0);
        editorScroll.Name = "editorScroll";
        editorScroll.Size = new Size(376, 388);
        editorScroll.TabIndex = 0;
        // 
        // editor
        // 
        editor.AutoSize = true;
        editor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        editor.ColumnCount = 2;
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        editor.Controls.Add(lblField1, 0, 0);
        editor.Controls.Add(txtHoTen, 1, 0);
        editor.Controls.Add(lblField2, 0, 1);
        editor.Controls.Add(txtTaiKhoan, 1, 1);
        editor.Controls.Add(lblField3, 0, 2);
        editor.Controls.Add(txtDienThoai, 1, 2);
        editor.Controls.Add(lblField4, 0, 3);
        editor.Controls.Add(txtMatKhau, 1, 3);
        editor.Controls.Add(lblField5, 0, 4);
        editor.Controls.Add(chkDangLam, 1, 4);
        editor.Controls.Add(lblVaiTro, 0, 5);
        editor.Controls.Add(cboVaiTro, 1, 5);
        editor.Dock = DockStyle.Top;
        editor.Location = new Point(0, 0);
        editor.Name = "editor";
        editor.RowCount = 6;
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        editor.Size = new Size(376, 216);
        editor.TabIndex = 0;
        // 
        // lblField1
        // 
        lblField1.Dock = DockStyle.Fill;
        lblField1.Font = new Font("Segoe UI", 9F);
        lblField1.Location = new Point(3, 0);
        lblField1.Name = "lblField1";
        lblField1.Size = new Size(144, 36);
        lblField1.TabIndex = 0;
        lblField1.Text = "Họ tên *";
        // 
        // txtHoTen
        // 
        txtHoTen.Dock = DockStyle.Fill;
        txtHoTen.Location = new Point(153, 3);
        txtHoTen.MaxLength = 100;
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(220, 35);
        txtHoTen.TabIndex = 1;
        // 
        // lblField2
        // 
        lblField2.Dock = DockStyle.Fill;
        lblField2.Font = new Font("Segoe UI", 9F);
        lblField2.Location = new Point(3, 36);
        lblField2.Name = "lblField2";
        lblField2.Size = new Size(144, 36);
        lblField2.TabIndex = 2;
        lblField2.Text = "Tài khoản *";
        // 
        // txtTaiKhoan
        // 
        txtTaiKhoan.Dock = DockStyle.Fill;
        txtTaiKhoan.Location = new Point(153, 39);
        txtTaiKhoan.MaxLength = 50;
        txtTaiKhoan.Name = "txtTaiKhoan";
        txtTaiKhoan.Size = new Size(220, 35);
        txtTaiKhoan.TabIndex = 3;
        // 
        // lblField3
        // 
        lblField3.Dock = DockStyle.Fill;
        lblField3.Font = new Font("Segoe UI", 9F);
        lblField3.Location = new Point(3, 72);
        lblField3.Name = "lblField3";
        lblField3.Size = new Size(144, 36);
        lblField3.TabIndex = 4;
        lblField3.Text = "Số điện thoại";
        // 
        // txtDienThoai
        // 
        txtDienThoai.Dock = DockStyle.Fill;
        txtDienThoai.Location = new Point(153, 75);
        txtDienThoai.MaxLength = 10;
        txtDienThoai.Name = "txtDienThoai";
        txtDienThoai.PlaceholderText = "10 chữ số; có thể bỏ trống";
        txtDienThoai.Size = new Size(220, 35);
        txtDienThoai.TabIndex = 5;
        // 
        // lblField4
        // 
        lblField4.Dock = DockStyle.Fill;
        lblField4.Font = new Font("Segoe UI", 9F);
        lblField4.Location = new Point(3, 108);
        lblField4.Name = "lblField4";
        lblField4.Size = new Size(144, 36);
        lblField4.TabIndex = 6;
        lblField4.Text = "Mật khẩu mới";
        // 
        // txtMatKhau
        // 
        txtMatKhau.Dock = DockStyle.Fill;
        txtMatKhau.Location = new Point(153, 111);
        txtMatKhau.MaxLength = 128;
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.Size = new Size(220, 35);
        txtMatKhau.TabIndex = 7;
        passwordHint.SetToolTip(txtMatKhau, "Để trống để giữ mật khẩu cũ. Nhập mật khẩu mới khi cần đặt lại.");
        txtMatKhau.UseSystemPasswordChar = true;
        // 
        // lblField5
        // 
        lblField5.Dock = DockStyle.Fill;
        lblField5.Font = new Font("Segoe UI", 9F);
        lblField5.Location = new Point(3, 144);
        lblField5.Name = "lblField5";
        lblField5.Size = new Size(144, 36);
        lblField5.TabIndex = 8;
        lblField5.Text = "Trạng thái";
        // 
        // chkDangLam
        // 
        chkDangLam.Dock = DockStyle.Fill;
        chkDangLam.Location = new Point(153, 147);
        chkDangLam.Name = "chkDangLam";
        chkDangLam.Size = new Size(220, 30);
        chkDangLam.TabIndex = 9;
        chkDangLam.Text = "Hoạt động";
        // 
        // lblVaiTro
        // 
        lblVaiTro.Dock = DockStyle.Fill;
        lblVaiTro.Location = new Point(3, 180);
        lblVaiTro.Name = "lblVaiTro";
        lblVaiTro.Size = new Size(144, 36);
        lblVaiTro.TabIndex = 10;
        lblVaiTro.Text = "Vai trò *";
        // 
        // cboVaiTro
        // 
        cboVaiTro.Dock = DockStyle.Fill;
        cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
        cboVaiTro.Items.AddRange(new object[] { "Nhân viên", "Admin" });
        cboVaiTro.Location = new Point(153, 183);
        cboVaiTro.Name = "cboVaiTro";
        cboVaiTro.Size = new Size(220, 36);
        cboVaiTro.TabIndex = 11;
        // 
        // lblField6
        // 
        lblField6.Dock = DockStyle.Fill;
        lblField6.Font = new Font("Segoe UI", 9F);
        lblField6.Location = new Point(3, 388);
        lblField6.Name = "lblField6";
        lblField6.Size = new Size(370, 22);
        lblField6.TabIndex = 1;
        lblField6.Text = "Thao tác";
        // 
        // actions
        // 
        actions.Controls.Add(btnThem);
        actions.Controls.Add(btnSua);
        actions.Controls.Add(btnLuu);
        actions.Controls.Add(btnHuy);
        actions.Controls.Add(btnKhoa);
        actions.Dock = DockStyle.Fill;
        actions.Location = new Point(0, 410);
        actions.Margin = new Padding(0);
        actions.Name = "actions";
        actions.Size = new Size(376, 120);
        actions.TabIndex = 2;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(22, 101, 52);
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(3, 3);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(84, 34);
        btnThem.TabIndex = 0;
        btnThem.Text = "Thêm";
        btnThem.UseVisualStyleBackColor = false;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(15, 118, 110);
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(93, 3);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(84, 34);
        btnSua.TabIndex = 1;
        btnSua.Text = "Sửa";
        btnSua.UseVisualStyleBackColor = false;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(22, 101, 52);
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(183, 3);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(84, 34);
        btnLuu.TabIndex = 2;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(100, 116, 139);
        btnHuy.ForeColor = Color.White;
        btnHuy.Location = new Point(273, 3);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(84, 34);
        btnHuy.TabIndex = 3;
        btnHuy.Text = "Bỏ qua";
        btnHuy.UseVisualStyleBackColor = false;
        // 
        // btnKhoa
        // 
        btnKhoa.BackColor = Color.FromArgb(225, 29, 72);
        btnKhoa.ForeColor = Color.White;
        btnKhoa.Location = new Point(3, 43);
        btnKhoa.Name = "btnKhoa";
        btnKhoa.Size = new Size(84, 34);
        btnKhoa.TabIndex = 4;
        btnKhoa.Text = "Khóa";
        btnKhoa.UseVisualStyleBackColor = false;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Dock = DockStyle.Fill;
        lblTrangThai.Location = new Point(15, 648);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(1152, 40);
        lblTrangThai.TabIndex = 2;
        lblTrangThai.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // frmNhanVien
        // 
        AutoScaleDimensions = new SizeF(11F, 28F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1182, 700);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = Color.FromArgb(15, 23, 42);
        MinimumSize = new Size(1150, 739);
        Name = "frmNhanVien";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý nhân viên | Hiệu thuốc ABC";
        posLayout.ResumeLayout(false);
        content.ResumeLayout(false);
        left.ResumeLayout(false);
        search.ResumeLayout(false);
        search.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
        details.ResumeLayout(false);
        detailLayout.ResumeLayout(false);
        editorScroll.ResumeLayout(false);
        editorScroll.PerformLayout();
        editor.ResumeLayout(false);
        editor.PerformLayout();
        actions.ResumeLayout(false);
        ResumeLayout(false);
    }
    private TableLayoutPanel posLayout;
    private Label posHeading;
    private TableLayoutPanel content;
    private TableLayoutPanel search;
    private TableLayoutPanel left;
    private TableLayoutPanel editor;
    private GroupBox details;
    private FlowLayoutPanel actions;
    private DataGridView dgvNhanVien;
    private Label lblTrangThai;
    private TextBox txtTimKiem;
    private TextBox txtHoTen;
    private TextBox txtTaiKhoan;
    private TextBox txtDienThoai;
    private TextBox txtMatKhau;
    private Button btnTimKiem;
    private Button btnLamMoi;
    private Button btnThem;
    private Button btnSua;
    private Button btnLuu;
    private Button btnHuy;
    private Button btnKhoa;
    private CheckBox chkDangLam;
    private Label lblField1;
    private Label lblField2;
    private Label lblField3;
    private Label lblField4;
    private Label lblField5;
    private Label lblField6;
    private DataGridViewTextBoxColumn colMaNV;
    private DataGridViewTextBoxColumn colHoTen;
    private DataGridViewTextBoxColumn colTaiKhoan;
    private DataGridViewTextBoxColumn colDienThoai;
    private DataGridViewCheckBoxColumn colDangLam;
    private TableLayoutPanel detailLayout;
    private Panel editorScroll;
    private ToolTip passwordHint;
    private ComboBox cboVaiTro;
    private Label lblVaiTro;
    private DataGridViewTextBoxColumn colVaiTro;
}
