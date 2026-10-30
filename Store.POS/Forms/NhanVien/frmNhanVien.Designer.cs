using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{
partial class frmNhanVien
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        this.colVaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colMaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colDienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colDangLam = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        posLayout = new TableLayoutPanel();
        posHeading = new Label();
        content = new TableLayoutPanel();
        search = new TableLayoutPanel();
        left = new TableLayoutPanel();
        editor = new TableLayoutPanel();
        details = new GroupBox();
        detailLayout=new TableLayoutPanel();editorScroll=new Panel();
        actions = new TableLayoutPanel();
        dgvNhanVien = new DataGridView();
        lblTrangThai = new Label();
        txtTimKiem = new TextBox();
        txtHoTen = new TextBox();
        txtTaiKhoan = new TextBox();
        txtDienThoai = new TextBox();
        txtMatKhau = new TextBox();
        btnTimKiem = new Button();
        btnLamMoi = new Button();
        btnThem = new Button();
        btnSua = new Button();
        btnLuu = new Button();
        btnHuy = new Button();
        btnKhoa = new Button();
        chkDangLam = new CheckBox();
        cboVaiTro = new ComboBox();lblVaiTro=new Label();
        lblField1 = new Label();
        lblField2 = new Label();
        lblField3 = new Label();
        lblField4 = new Label();
        lblField5 = new Label();
        lblField6 = new Label();
        
        
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
        components=new System.ComponentModel.Container();
        passwordHint=new ToolTip(components);passwordHint.SetToolTip(txtMatKhau,"Để trống để giữ mật khẩu cũ. Nhập mật khẩu mới khi cần đặt lại.");
        SuspendLayout();
        posLayout.Name="posLayout";posLayout.Dock=DockStyle.Fill;posLayout.Padding=new Padding(12);posLayout.ColumnCount=1;posLayout.RowCount=3;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,48F));posLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100F));posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,40F));
        posHeading.Name="posHeading";
        posHeading.Font = new Font("Segoe UI Semibold",16F,FontStyle.Bold);
        posHeading.ForeColor = Color.FromArgb(15,23,42);posHeading.Text="Quản lý nhân viên";posHeading.Dock=DockStyle.Fill;posHeading.Font=new Font("Segoe UI Semibold",16F,FontStyle.Bold);posHeading.TextAlign=ContentAlignment.MiddleLeft;
        content.Name="content";content.Dock=DockStyle.Fill;content.ColumnCount=2;content.RowCount=1;content.Margin=new Padding(0);
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,65F));content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,35F));content.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        left.Name="left";left.Dock=DockStyle.Fill;left.ColumnCount=1;left.RowCount=2;left.Margin=new Padding(0,0,12,0);
        left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));left.RowStyles.Add(new RowStyle(SizeType.Absolute,48F));left.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        search.Name="search";search.Dock=DockStyle.Fill;search.ColumnCount=3;search.RowCount=1;
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100F));search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100F));search.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        txtTimKiem.Name="txtTimKiem";txtTimKiem.Dock=DockStyle.Fill;txtTimKiem.MaxLength=200;txtTimKiem.PlaceholderText="Họ tên, tài khoản, điện thoại hoặc mã nhân viên";
        dgvNhanVien.Name="dgvNhanVien";
        dgvNhanVien.GridColor = Color.FromArgb(226, 232, 240);dgvNhanVien.Dock=DockStyle.Fill;
        dgvNhanVien.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvNhanVien.AllowUserToAddRows=false;dgvNhanVien.AllowUserToDeleteRows=false;dgvNhanVien.ReadOnly=true;dgvNhanVien.RowHeadersVisible=false;
        dgvNhanVien.MultiSelect=false;dgvNhanVien.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvNhanVien.ShowCellToolTips = true;
        dgvNhanVien.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvNhanVien.BackgroundColor=Color.White;dgvNhanVien.RowTemplate.Height=34;dgvNhanVien.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells;
        
        
        
        details.Name="details";details.Text="Thông tin nhân viên";details.Dock=DockStyle.Fill;details.Padding=new Padding(12);
        editor.Name="editor";editor.Dock=DockStyle.Top;editor.AutoSize=true;editor.AutoSizeMode=AutoSizeMode.GrowAndShrink;editor.ColumnCount=2;editor.RowCount=6;
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,40F));editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,60F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,36F));
        lblField1.Name="lblField1";lblField1.Text="Họ tên *";lblField1.Dock=DockStyle.Fill;lblField1.Font=new Font("Segoe UI",9F);
        txtHoTen.Name="txtHoTen";txtHoTen.Dock=DockStyle.Fill;
        editor.Controls.Add(lblField1,0,0);editor.Controls.Add(txtHoTen,1,0);
        lblField2.Name="lblField2";lblField2.Text="Tài khoản *";lblField2.Dock=DockStyle.Fill;lblField2.Font=new Font("Segoe UI",9F);
        txtTaiKhoan.Name="txtTaiKhoan";txtTaiKhoan.Dock=DockStyle.Fill;
        editor.Controls.Add(lblField2,0,1);editor.Controls.Add(txtTaiKhoan,1,1);
        lblField3.Name="lblField3";lblField3.Text="Số điện thoại";lblField3.Dock=DockStyle.Fill;lblField3.Font=new Font("Segoe UI",9F);
        txtDienThoai.Name="txtDienThoai";txtDienThoai.Dock=DockStyle.Fill;
        editor.Controls.Add(lblField3,0,2);editor.Controls.Add(txtDienThoai,1,2);
        lblField4.Name="lblField4";lblField4.Text="Mật khẩu mới";lblField4.Dock=DockStyle.Fill;lblField4.Font=new Font("Segoe UI",9F);
        txtMatKhau.Name="txtMatKhau";txtMatKhau.Dock=DockStyle.Fill;
        editor.Controls.Add(lblField4,0,3);editor.Controls.Add(txtMatKhau,1,3);
        lblField5.Name="lblField5";lblField5.Text="Trạng thái";lblField5.Dock=DockStyle.Fill;lblField5.Font=new Font("Segoe UI",9F);
        chkDangLam.Name="chkDangLam";chkDangLam.Dock=DockStyle.Fill;
        editor.Controls.Add(lblField5,0,4);editor.Controls.Add(chkDangLam,1,4);

        lblField6.Name="lblField6";lblField6.Text="Thao tác";lblField6.Dock=DockStyle.Fill;lblField6.Font=new Font("Segoe UI",9F);
        actions.Name="actions";actions.Dock=DockStyle.Fill;

        lblVaiTro.Name="lblVaiTro";lblVaiTro.Text="Vai trò *";lblVaiTro.Dock=DockStyle.Fill;
        cboVaiTro.Name="cboVaiTro";cboVaiTro.Dock=DockStyle.Fill;cboVaiTro.DropDownStyle=ComboBoxStyle.DropDownList;
        cboVaiTro.Items.AddRange(new object[]{"Nhân viên","Admin"});
        editor.Controls.Add(lblVaiTro,0,5);editor.Controls.Add(cboVaiTro,1,5);

        txtHoTen.MaxLength=100;txtTaiKhoan.MaxLength=50;txtDienThoai.MaxLength=10;
        txtDienThoai.PlaceholderText="10 chữ số; có thể bỏ trống";txtMatKhau.MaxLength=128;txtMatKhau.UseSystemPasswordChar=true;
        chkDangLam.Text="Hoạt động";
        actions.ColumnCount=2;actions.RowCount=3;
        actions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50F));actions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,50F));
        actions.RowStyles.Add(new RowStyle(SizeType.Absolute,42F));actions.RowStyles.Add(new RowStyle(SizeType.Absolute,42F));actions.RowStyles.Add(new RowStyle(SizeType.Absolute,42F));
        actions.Margin=new Padding(0);actions.Padding=new Padding(0);
        btnTimKiem.Name="btnTimKiem";btnTimKiem.Text="Tìm kiếm";btnTimKiem.Size=new Size(84,34);btnTimKiem.Margin=new Padding(3);
        btnTimKiem.Dock=DockStyle.Fill;
        btnLamMoi.Name="btnLamMoi";btnLamMoi.Text="Làm mới";btnLamMoi.Size=new Size(84,34);btnLamMoi.Margin=new Padding(3);
        btnLamMoi.Dock=DockStyle.Fill;
        btnThem.Name="btnThem";btnThem.Text="Thêm";btnThem.Dock=DockStyle.Fill;btnThem.Margin=new Padding(0,4,4,4);
        actions.Controls.Add(btnThem,0,0);
        btnSua.Name="btnSua";btnSua.Text="Sửa";btnSua.Dock=DockStyle.Fill;btnSua.Margin=new Padding(4,4,0,4);
        actions.Controls.Add(btnSua,1,0);
        btnLuu.Name="btnLuu";btnLuu.Text="Lưu";btnLuu.Dock=DockStyle.Fill;btnLuu.Margin=new Padding(0,4,4,4);
        actions.Controls.Add(btnLuu,0,1);
        btnHuy.Name="btnHuy";btnHuy.Text="Bỏ qua";btnHuy.Dock=DockStyle.Fill;btnHuy.Margin=new Padding(4,4,0,4);
        actions.Controls.Add(btnHuy,1,1);
        btnKhoa.Name="btnKhoa";btnKhoa.Text="Khóa";btnKhoa.Dock=DockStyle.Fill;btnKhoa.Margin=new Padding(0,4,0,4);
        actions.Controls.Add(btnKhoa,0,2);
        actions.SetColumnSpan(btnKhoa,2);
        search.Controls.Add(txtTimKiem,0,0);search.Controls.Add(btnTimKiem,1,0);search.Controls.Add(btnLamMoi,2,0);
        left.Controls.Add(search,0,0);left.Controls.Add(dgvNhanVien,0,1);
        detailLayout.Name="detailLayout";detailLayout.Dock=DockStyle.Fill;detailLayout.Margin=Padding.Empty;
        detailLayout.ColumnCount=1;detailLayout.RowCount=3;detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F));
        detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100F));
        detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,22F));detailLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,126F));
        editorScroll.Name="editorScroll";editorScroll.Dock=DockStyle.Fill;editorScroll.AutoScroll=true;editorScroll.Margin=Padding.Empty;
        editorScroll.Controls.Add(editor);detailLayout.Controls.Add(editorScroll,0,0);
        detailLayout.Controls.Add(lblField6,0,1);detailLayout.Controls.Add(actions,0,2);details.Controls.Add(detailLayout);
        content.Controls.Add(left,0,0);content.Controls.Add(details,1,0);
        lblTrangThai.Name="lblTrangThai";lblTrangThai.Dock=DockStyle.Fill;lblTrangThai.TextAlign=ContentAlignment.MiddleLeft;
        posLayout.Controls.Add(posHeading, 0, 0);posLayout.Controls.Add(content,0,1);posLayout.Controls.Add(lblTrangThai,0,2);
        AutoScaleDimensions=new SizeF(8F,20F);AutoScaleMode=AutoScaleMode.Font;Font=new Font("Segoe UI",10.2F);
        ClientSize=new Size(1182,700);MinimumSize=new Size(1150,739);StartPosition=FormStartPosition.CenterScreen;
        Name="frmNhanVien";Text="Quản lý nhân viên | Hiệu thuốc ABC";Controls.Add(posLayout);
                //
        // colVaiTro
        //
        this.colVaiTro.Name = "TenVaiTro";
        this.colVaiTro.HeaderText = "Vai trò";
        this.colVaiTro.DataPropertyName = "TenVaiTro";
        this.colVaiTro.ReadOnly = true;
        this.colVaiTro.Resizable = DataGridViewTriState.False;
        this.colVaiTro.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colVaiTro.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colVaiTro.MinimumWidth = 5;
        this.colVaiTro.Width = 175;
        this.colVaiTro.FillWeight = 220F;
        //
        // colMaNV
        //
        this.colMaNV.Name = "MaNV";
        this.colMaNV.HeaderText = "Mã nhân viên";
        this.colMaNV.DataPropertyName = "MaNV";
        this.colMaNV.ReadOnly = true;
        this.colMaNV.Resizable = DataGridViewTriState.False;
        this.colMaNV.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colMaNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colMaNV.MinimumWidth = 5;
        this.colMaNV.Width = 146;
        this.colMaNV.FillWeight = 64F;
        //
        // colHoTen
        //
        this.colHoTen.Name = "HoTen";
        this.colHoTen.HeaderText = "Họ tên";
        this.colHoTen.DataPropertyName = "HoTen";
        this.colHoTen.ReadOnly = true;
        this.colHoTen.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colHoTen.Resizable = DataGridViewTriState.False;
        this.colHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colHoTen.MinimumWidth = 5;
        this.colHoTen.Width = 230;
        this.colHoTen.FillWeight = 220F;
        //
        // colTaiKhoan
        //
        this.colTaiKhoan.Name = "TaiKhoan";
        this.colTaiKhoan.HeaderText = "Tài khoản";
        this.colTaiKhoan.DataPropertyName = "TaiKhoan";
        this.colTaiKhoan.ReadOnly = true;
        this.colTaiKhoan.Resizable = DataGridViewTriState.False;
        this.colTaiKhoan.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colTaiKhoan.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colTaiKhoan.MinimumWidth = 5;
        this.colTaiKhoan.Width = 150;
        this.colTaiKhoan.FillWeight = 96F;
        //
        // colDienThoai
        //
        this.colDienThoai.Name = "DienThoai";
        this.colDienThoai.HeaderText = "Số điện thoại";
        this.colDienThoai.DataPropertyName = "DienThoai";
        this.colDienThoai.ReadOnly = true;
        this.colDienThoai.Resizable = DataGridViewTriState.False;
        this.colDienThoai.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colDienThoai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colDienThoai.MinimumWidth = 5;
        this.colDienThoai.Width = 145;
        this.colDienThoai.FillWeight = 104F;
        //
        // colDangLam
        //
        this.colDangLam.Name = "DangLam";
        this.colDangLam.HeaderText = "Đang làm việc";
        this.colDangLam.DataPropertyName = "DangLam";
        this.colDangLam.ReadOnly = true;
        this.colDangLam.Resizable = DataGridViewTriState.False;
        this.colDangLam.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colDangLam.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colDangLam.MinimumWidth = 5;
        this.colDangLam.Width = 150;
        this.colDangLam.FillWeight = 88F;

        this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvNhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colMaNV, this.colHoTen, this.colTaiKhoan, this.colDienThoai, this.colVaiTro, this.colDangLam });
        ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnTimKiem.BackColor=Color.FromArgb(15, 118, 110);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnTimKiem.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.UseVisualStyleBackColor = false;btnTimKiem.ForeColor=Color.White;
        btnLamMoi.BackColor=Color.FromArgb(15, 118, 110);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.UseVisualStyleBackColor = false;btnLamMoi.ForeColor=Color.White;
        btnThem.BackColor=Color.FromArgb(22, 101, 52);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
        btnThem.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
        btnThem.Cursor = Cursors.Hand;
        btnThem.UseVisualStyleBackColor = false;btnThem.ForeColor=Color.White;
        btnSua.BackColor=Color.FromArgb(15, 118, 110);
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnSua.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnSua.Cursor = Cursors.Hand;
        btnSua.UseVisualStyleBackColor = false;btnSua.ForeColor=Color.White;
        btnLuu.BackColor=Color.FromArgb(22, 101, 52);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
        btnLuu.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.UseVisualStyleBackColor = false;btnLuu.ForeColor=Color.White;
        btnHuy.BackColor=Color.FromArgb(100, 116, 139);
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
        btnHuy.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.UseVisualStyleBackColor = false;btnHuy.ForeColor=Color.White;
        btnKhoa.BackColor=Color.FromArgb(225, 29, 72);
        btnKhoa.FlatStyle = FlatStyle.Flat;
        btnKhoa.FlatAppearance.BorderSize = 0;
        btnKhoa.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
        btnKhoa.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
        btnKhoa.Cursor = Cursors.Hand;
        btnKhoa.UseVisualStyleBackColor = false;btnKhoa.ForeColor=Color.White;
        dgvNhanVien.BackgroundColor=Color.White;dgvNhanVien.EnableHeadersVisualStyles=false;
        dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvNhanVien.DefaultCellStyle.BackColor=Color.White;dgvNhanVien.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvNhanVien.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvNhanVien.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(242, 246, 248);
        dgvNhanVien.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvNhanVien.MultiSelect=false;
        dgvNhanVien.AllowUserToResizeColumns=false;
        dgvNhanVien.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhanVien.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhanVien.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor;
        dgvNhanVien.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvNhanVien.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvNhanVien.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                txtTimKiem.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtTimKiem.ForeColor=Color.FromArgb(15,23,42);
        txtTimKiem.BackColor=Color.White;
        txtTimKiem.BorderStyle=BorderStyle.FixedSingle;
        txtHoTen.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtHoTen.ForeColor=Color.FromArgb(15,23,42);
        txtHoTen.BackColor=Color.White;
        txtHoTen.BorderStyle=BorderStyle.FixedSingle;
        txtTaiKhoan.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtTaiKhoan.ForeColor=Color.FromArgb(15,23,42);
        txtTaiKhoan.BackColor=Color.White;
        txtTaiKhoan.BorderStyle=BorderStyle.FixedSingle;
        txtDienThoai.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtDienThoai.ForeColor=Color.FromArgb(15,23,42);
        txtDienThoai.BackColor=Color.White;
        txtDienThoai.BorderStyle=BorderStyle.FixedSingle;
        txtMatKhau.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtMatKhau.ForeColor=Color.FromArgb(15,23,42);
        txtMatKhau.BackColor=Color.White;
        txtMatKhau.BorderStyle=BorderStyle.FixedSingle;
        cboVaiTro.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        cboVaiTro.ForeColor=Color.FromArgb(15,23,42);
        cboVaiTro.BackColor=Color.White;
        cboVaiTro.FlatStyle=FlatStyle.Standard;
                txtTimKiem.AutoSize=false;
        txtHoTen.AutoSize=false;
        txtTaiKhoan.AutoSize=false;
        txtDienThoai.AutoSize=false;
        txtMatKhau.AutoSize=false;
                dgvNhanVien.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhanVien.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvNhanVien.ColumnHeadersHeight=40;
        
        
        
        
        
        
        ResumeLayout(false);
    }
    private TableLayoutPanel posLayout;
    private Label posHeading;
    private TableLayoutPanel content;
    private TableLayoutPanel search;
    private TableLayoutPanel left;
    private TableLayoutPanel editor;
    private GroupBox details;
    private TableLayoutPanel actions;
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
    private System.Windows.Forms.DataGridViewTextBoxColumn colMaNV;
    private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTaiKhoan;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDienThoai;
    private System.Windows.Forms.DataGridViewCheckBoxColumn colDangLam;
    private TableLayoutPanel detailLayout;
    private Panel editorScroll;
    private ToolTip passwordHint;
    private ComboBox cboVaiTro;
    private Label lblVaiTro;
    private System.Windows.Forms.DataGridViewTextBoxColumn colVaiTro;
}

}