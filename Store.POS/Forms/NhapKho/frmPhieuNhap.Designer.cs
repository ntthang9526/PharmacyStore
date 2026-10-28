using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{
partial class frmPhieuNhap
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        this.dgvPhieuNhapCol0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvPhieuNhapCol1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvPhieuNhapCol2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvPhieuNhapCol3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvPhieuNhapCol4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvPhieuNhapCol5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvChiTietCol6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        posLayout=new TableLayoutPanel();posLayout.Name="posLayout";
        search=new TableLayoutPanel();search.Name="search";
        details=new TableLayoutPanel();details.Name="details";
        posHeading=new Label();posHeading.Name="posHeading";
        lblTu=new Label();lblTu.Name="lblTu";
        lblDen=new Label();lblDen.Name="lblDen";
        lblChiTiet=new Label();lblChiTiet.Name="lblChiTiet";
        lblTrangThai=new Label();lblTrangThai.Name="lblTrangThai";
        txtTimKiem=new TextBox();txtTimKiem.Name="txtTimKiem";
        btnTimKiem=new Button();btnTimKiem.Name="btnTimKiem";
        btnLamMoi=new Button();btnLamMoi.Name="btnLamMoi";
        dtpTuNgay=new DateTimePicker();dtpTuNgay.Name="dtpTuNgay";
        dtpDenNgay=new DateTimePicker();dtpDenNgay.Name="dtpDenNgay";
        dgvPhieuNhap=new DataGridView();dgvPhieuNhap.Name="dgvPhieuNhap";
        dgvChiTiet=new DataGridView();dgvChiTiet.Name="dgvChiTiet";
        ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();SuspendLayout();
        posLayout.Dock=DockStyle.Fill;posLayout.Padding=new Padding(12);posLayout.ColumnCount=1;posLayout.RowCount=4;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,48));posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,48));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100));posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,32));
        posHeading.Text="Phiếu nhập hàng";posHeading.Dock=DockStyle.Fill;posHeading.Font=new Font("Segoe UI",18,FontStyle.Bold);
        search.Dock=DockStyle.Fill;search.ColumnCount=7;search.RowCount=1;
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,40));search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,155));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,50));search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,155));
        search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));search.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));
        search.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        txtTimKiem.Dock=DockStyle.Fill;txtTimKiem.MaxLength=200;txtTimKiem.PlaceholderText="Mã phiếu, NCC, nhân viên, thuốc, số lô";
        lblTu.Text="Từ";lblTu.Dock=DockStyle.Fill;lblTu.TextAlign=ContentAlignment.MiddleCenter;
        lblDen.Margin=Padding.Empty;lblDen.Text="Đến";lblDen.Dock=DockStyle.Fill;lblDen.TextAlign=ContentAlignment.MiddleCenter;
        dtpTuNgay.Format=DateTimePickerFormat.Custom;dtpTuNgay.CustomFormat="dd/MM/yyyy";dtpTuNgay.ShowCheckBox=true;dtpTuNgay.Checked=false;dtpTuNgay.Dock=DockStyle.Fill;
        dtpDenNgay.Format=DateTimePickerFormat.Custom;dtpDenNgay.CustomFormat="dd/MM/yyyy";dtpDenNgay.ShowCheckBox=true;dtpDenNgay.Checked=false;dtpDenNgay.Dock=DockStyle.Fill;
        btnTimKiem.Text="Tìm kiếm";btnTimKiem.Dock=DockStyle.Fill;btnLamMoi.Text="Làm mới";btnLamMoi.Dock=DockStyle.Fill;
        search.Controls.Add(txtTimKiem,0,0);search.Controls.Add(lblTu,1,0);search.Controls.Add(dtpTuNgay,2,0);search.Controls.Add(lblDen,3,0);
        search.Controls.Add(dtpDenNgay,4,0);search.Controls.Add(btnTimKiem,5,0);search.Controls.Add(btnLamMoi,6,0);
        details.Dock=DockStyle.Fill;details.ColumnCount=1;details.RowCount=2;details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        details.RowStyles.Add(new RowStyle(SizeType.AutoSize));details.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        lblChiTiet.AutoSize=true;lblChiTiet.Padding=new Padding(0,6,0,6);lblChiTiet.Dock=DockStyle.Fill;lblChiTiet.Text="Chọn phiếu để xem chi tiết nhập hàng.";lblChiTiet.TextAlign=ContentAlignment.MiddleLeft;
        lblTrangThai.Dock=DockStyle.Fill;lblTrangThai.TextAlign=ContentAlignment.MiddleLeft;
        dgvPhieuNhap.Dock=DockStyle.Fill;
        dgvPhieuNhap.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;dgvPhieuNhap.ReadOnly=true;
        dgvPhieuNhap.AllowUserToAddRows=false;dgvPhieuNhap.AllowUserToDeleteRows=false;dgvPhieuNhap.MultiSelect=false;dgvPhieuNhap.RowHeadersVisible=false;
        dgvPhieuNhap.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvPhieuNhap.ShowCellToolTips = true;
        dgvPhieuNhap.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvPhieuNhap.BackgroundColor=SystemColors.ScrollBar;dgvPhieuNhap.RowTemplate.Height=32;
        
        
        
        
        
        dgvChiTiet.Dock=DockStyle.Fill;
        dgvChiTiet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;dgvChiTiet.ReadOnly=true;
        dgvChiTiet.AllowUserToAddRows=false;dgvChiTiet.AllowUserToDeleteRows=false;dgvChiTiet.MultiSelect=false;dgvChiTiet.RowHeadersVisible=false;
        dgvChiTiet.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvChiTiet.ShowCellToolTips = true;
        dgvChiTiet.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvChiTiet.BackgroundColor=SystemColors.ScrollBar;dgvChiTiet.RowTemplate.Height=32;
        
        
        
        
        
        
        
        details.Controls.Add(lblChiTiet,0,0);details.Controls.Add(dgvChiTiet,0,1);
        posLayout.Controls.Add(posHeading,0,0);posLayout.Controls.Add(search,0,1);
        receiptColumns=new TableLayoutPanel();receiptColumns.Name="receiptColumns";receiptColumns.Dock=DockStyle.Fill;
        receiptColumns.Margin=Padding.Empty;receiptColumns.ColumnCount=2;receiptColumns.RowCount=1;
        receiptColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,45));receiptColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,55));
        receiptColumns.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        grpPhieu=new GroupBox();grpPhieu.Name="grpPhieu";grpPhieu.Text="Phiếu nhập hàng";grpPhieu.Dock=DockStyle.Fill;grpPhieu.Padding=new Padding(10);
        grpChiTiet=new GroupBox();grpChiTiet.Name="grpChiTiet";grpChiTiet.Text="Chi tiết phiếu nhập";grpChiTiet.Dock=DockStyle.Fill;grpChiTiet.Padding=new Padding(10);
        grpPhieu.Controls.Add(dgvPhieuNhap);grpChiTiet.Controls.Add(details);
        receiptColumns.Controls.Add(grpPhieu,0,0);receiptColumns.Controls.Add(grpChiTiet,1,0);
        posLayout.Controls.Add(receiptColumns,0,2);
        posLayout.Controls.Add(lblTrangThai,0,3);
        AutoScaleDimensions=new SizeF(8,20);AutoScaleMode=AutoScaleMode.Font;Font=new Font("Segoe UI",10.2F);
        ClientSize=new Size(1182,653);MinimumSize=new Size(1000,620);Name="frmPhieuNhap";Text="Phiếu nhập hàng";Controls.Add(posLayout);
                //
        // dgvPhieuNhapCol0
        //
        this.dgvPhieuNhapCol0.Name = "MaPhieu";
        this.dgvPhieuNhapCol0.DataPropertyName = "MaPhieu";
        this.dgvPhieuNhapCol0.HeaderText = "Mã phiếu nhập";
        this.dgvPhieuNhapCol0.ReadOnly = true;
        this.dgvPhieuNhapCol0.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol0.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol0.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol0.MinimumWidth = 5;
        this.dgvPhieuNhapCol0.Width = 114;
        this.dgvPhieuNhapCol0.FillWeight = 88F;
        //
        // dgvPhieuNhapCol1
        //
        this.dgvPhieuNhapCol1.Name = "NgayNhap";
        this.dgvPhieuNhapCol1.DataPropertyName = "NgayNhap";
        this.dgvPhieuNhapCol1.HeaderText = "Ngày nhập";
        this.dgvPhieuNhapCol1.ReadOnly = true;
        this.dgvPhieuNhapCol1.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        this.dgvPhieuNhapCol1.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol1.MinimumWidth = 5;
        this.dgvPhieuNhapCol1.Width = 140;
        this.dgvPhieuNhapCol1.FillWeight = 96F;
        //
        // dgvPhieuNhapCol2
        //
        this.dgvPhieuNhapCol2.Name = "TenNCC";
        this.dgvPhieuNhapCol2.DataPropertyName = "TenNCC";
        this.dgvPhieuNhapCol2.HeaderText = "Nhà cung cấp";
        this.dgvPhieuNhapCol2.ReadOnly = true;
        this.dgvPhieuNhapCol2.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol2.MinimumWidth = 5;
        this.dgvPhieuNhapCol2.Width = 165;
        this.dgvPhieuNhapCol2.FillWeight = 220F;
        //
        // dgvPhieuNhapCol3
        //
        this.dgvPhieuNhapCol3.Name = "NhanVien";
        this.dgvPhieuNhapCol3.DataPropertyName = "NhanVien";
        this.dgvPhieuNhapCol3.HeaderText = "Nhân viên";
        this.dgvPhieuNhapCol3.ReadOnly = true;
        this.dgvPhieuNhapCol3.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol3.MinimumWidth = 5;
        this.dgvPhieuNhapCol3.Width = 164;
        this.dgvPhieuNhapCol3.FillWeight = 220F;
        //
        // dgvPhieuNhapCol4
        //
        this.dgvPhieuNhapCol4.Name = "TongTien";
        this.dgvPhieuNhapCol4.DataPropertyName = "TongTien";
        this.dgvPhieuNhapCol4.HeaderText = "Tổng tiền";
        this.dgvPhieuNhapCol4.ReadOnly = true;
        this.dgvPhieuNhapCol4.DefaultCellStyle.Format = "N0";
        this.dgvPhieuNhapCol4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.dgvPhieuNhapCol4.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol4.MinimumWidth = 5;
        this.dgvPhieuNhapCol4.Width = 115;
        this.dgvPhieuNhapCol4.FillWeight = 96F;
        //
        // dgvPhieuNhapCol5
        //
        this.dgvPhieuNhapCol5.Name = "GhiChu";
        this.dgvPhieuNhapCol5.DataPropertyName = "GhiChu";
        this.dgvPhieuNhapCol5.HeaderText = "Ghi chú";
        this.dgvPhieuNhapCol5.ReadOnly = true;
        this.dgvPhieuNhapCol5.Resizable = DataGridViewTriState.False;
        this.dgvPhieuNhapCol5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvPhieuNhapCol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvPhieuNhapCol5.MinimumWidth = 5;
        this.dgvPhieuNhapCol5.Width = 145;
        this.dgvPhieuNhapCol5.FillWeight = 180F;
        //
        // dgvChiTietCol0
        //
        this.dgvChiTietCol0.Name = "TenThuoc";
        this.dgvChiTietCol0.DataPropertyName = "TenThuoc";
        this.dgvChiTietCol0.HeaderText = "Tên thuốc";
        this.dgvChiTietCol0.ReadOnly = true;
        this.dgvChiTietCol0.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol0.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol0.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol0.MinimumWidth = 5;
        this.dgvChiTietCol0.Width = 200;
        this.dgvChiTietCol0.FillWeight = 220F;
        //
        // dgvChiTietCol1
        //
        this.dgvChiTietCol1.Name = "DonVi";
        this.dgvChiTietCol1.DataPropertyName = "DonVi";
        this.dgvChiTietCol1.HeaderText = "Đơn vị tính";
        this.dgvChiTietCol1.ReadOnly = true;
        this.dgvChiTietCol1.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol1.MinimumWidth = 5;
        this.dgvChiTietCol1.Width = 79;
        this.dgvChiTietCol1.FillWeight = 72F;
        //
        // dgvChiTietCol2
        //
        this.dgvChiTietCol2.Name = "SoLo";
        this.dgvChiTietCol2.DataPropertyName = "SoLo";
        this.dgvChiTietCol2.HeaderText = "Số lô";
        this.dgvChiTietCol2.ReadOnly = true;
        this.dgvChiTietCol2.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol2.MinimumWidth = 5;
        this.dgvChiTietCol2.Width = 115;
        this.dgvChiTietCol2.FillWeight = 64F;
        //
        // dgvChiTietCol3
        //
        this.dgvChiTietCol3.Name = "HanDung";
        this.dgvChiTietCol3.DataPropertyName = "HanDung";
        this.dgvChiTietCol3.HeaderText = "Hạn sử dụng";
        this.dgvChiTietCol3.ReadOnly = true;
        this.dgvChiTietCol3.DefaultCellStyle.Format = "dd/MM/yyyy";
        this.dgvChiTietCol3.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol3.MinimumWidth = 5;
        this.dgvChiTietCol3.Width = 118;
        this.dgvChiTietCol3.FillWeight = 88F;
        //
        // dgvChiTietCol4
        //
        this.dgvChiTietCol4.Name = "SoLuong";
        this.dgvChiTietCol4.DataPropertyName = "SoLuong";
        this.dgvChiTietCol4.HeaderText = "Số lượng";
        this.dgvChiTietCol4.ReadOnly = true;
        this.dgvChiTietCol4.DefaultCellStyle.Format = "N0";
        this.dgvChiTietCol4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.dgvChiTietCol4.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol4.MinimumWidth = 5;
        this.dgvChiTietCol4.Width = 110;
        this.dgvChiTietCol4.FillWeight = 88F;
        //
        // dgvChiTietCol5
        //
        this.dgvChiTietCol5.Name = "GiaNhap";
        this.dgvChiTietCol5.DataPropertyName = "GiaNhap";
        this.dgvChiTietCol5.HeaderText = "Giá nhập";
        this.dgvChiTietCol5.ReadOnly = true;
        this.dgvChiTietCol5.DefaultCellStyle.Format = "N0";
        this.dgvChiTietCol5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.dgvChiTietCol5.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol5.MinimumWidth = 5;
        this.dgvChiTietCol5.Width = 115;
        this.dgvChiTietCol5.FillWeight = 88F;
        //
        // dgvChiTietCol6
        //
        this.dgvChiTietCol6.Name = "ThanhTien";
        this.dgvChiTietCol6.DataPropertyName = "ThanhTien";
        this.dgvChiTietCol6.HeaderText = "Thành tiền";
        this.dgvChiTietCol6.ReadOnly = true;
        this.dgvChiTietCol6.DefaultCellStyle.Format = "N0";
        this.dgvChiTietCol6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.dgvChiTietCol6.Resizable = DataGridViewTriState.False;
        this.dgvChiTietCol6.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvChiTietCol6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvChiTietCol6.MinimumWidth = 5;
        this.dgvChiTietCol6.Width = 125;
        this.dgvChiTietCol6.FillWeight = 136F;

        this.dgvPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvPhieuNhapCol0, this.dgvPhieuNhapCol1, this.dgvPhieuNhapCol2, this.dgvPhieuNhapCol3, this.dgvPhieuNhapCol4, this.dgvPhieuNhapCol5 });

        this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvChiTietCol0, this.dgvChiTietCol1, this.dgvChiTietCol2, this.dgvChiTietCol3, this.dgvChiTietCol4, this.dgvChiTietCol5, this.dgvChiTietCol6 });
        ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).EndInit();((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
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
        dgvPhieuNhap.BackgroundColor=Color.White;dgvPhieuNhap.EnableHeadersVisualStyles=false;
        dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvPhieuNhap.DefaultCellStyle.BackColor=Color.White;dgvPhieuNhap.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvPhieuNhap.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvPhieuNhap.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvPhieuNhap.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvChiTiet.BackgroundColor=Color.White;dgvChiTiet.EnableHeadersVisualStyles=false;
        dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvChiTiet.DefaultCellStyle.BackColor=Color.White;dgvChiTiet.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvChiTiet.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvChiTiet.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvChiTiet.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvPhieuNhap.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvPhieuNhap.MultiSelect=false;
        dgvPhieuNhap.AllowUserToResizeColumns=false;
        dgvPhieuNhap.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvPhieuNhap.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvPhieuNhap.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor;
        dgvPhieuNhap.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvPhieuNhap.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvPhieuNhap.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        dgvChiTiet.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvChiTiet.MultiSelect=false;
        dgvChiTiet.AllowUserToResizeColumns=false;
        dgvChiTiet.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvChiTiet.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvChiTiet.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor;
        dgvChiTiet.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvChiTiet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvChiTiet.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                txtTimKiem.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtTimKiem.ForeColor=Color.FromArgb(15,23,42);
        txtTimKiem.BackColor=Color.White;
        txtTimKiem.BorderStyle=BorderStyle.FixedSingle;
        dtpTuNgay.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        dtpTuNgay.ForeColor=Color.FromArgb(15,23,42);
        dtpDenNgay.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        dtpDenNgay.ForeColor=Color.FromArgb(15,23,42);
                txtTimKiem.AutoSize=false;
                dgvChiTiet.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvChiTiet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvChiTiet.ColumnHeadersHeight=40;
        dgvPhieuNhap.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvPhieuNhap.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvPhieuNhap.ColumnHeadersHeight=40;
        
        
        
        
        
        
        
        
        
        
        
        
        
        ResumeLayout(false);
    }
    private TableLayoutPanel posLayout,search,details,receiptColumns;
    private GroupBox grpPhieu,grpChiTiet;
    private Label posHeading,lblTu,lblDen,lblChiTiet,lblTrangThai;
    private TextBox txtTimKiem;
    private Button btnTimKiem,btnLamMoi;
    private DateTimePicker dtpTuNgay,dtpDenNgay;
    private DataGridView dgvPhieuNhap,dgvChiTiet;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuNhapCol0,dgvPhieuNhapCol1,dgvPhieuNhapCol2,dgvPhieuNhapCol3,dgvPhieuNhapCol4,dgvPhieuNhapCol5,dgvChiTietCol0,dgvChiTietCol1,dgvChiTietCol2,dgvChiTietCol3,dgvChiTietCol4,dgvChiTietCol5,dgvChiTietCol6;
}

}