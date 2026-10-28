using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
namespace Store.POS
{
    partial class frmBanHang
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
        this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.btnThemGioHang = new System.Windows.Forms.DataGridViewButtonColumn();
        this.noDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.btnAction = new System.Windows.Forms.DataGridViewButtonColumn();
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            salesLayout = new TableLayoutPanel();
            groupGioHang = new GroupBox();
            tableTimKiem = new DataGridView();
            
            medicineBindingSource = new BindingSource(components);
            cartLayout = new TableLayoutPanel();
            searchLayout = new TableLayoutPanel();
            labelTimKiem = new Label();
            panel1 = new Panel();
            boxTimKiem = new TextBox();
            btnXoaTimKiem = new Button();
            tableGioHang = new DataGridView();
            
            
            
            rowDataBindingSource = new BindingSource(components);
            entryLayout = new TableLayoutPanel();
            panel2 = new Panel();
            boxNhapSanPham = new TextBox();
            btnNhapSanPham = new Button();
            lblTinhTrangGioHang = new Label();
            groupThanhToan = new GroupBox();
            paymentScroll = new Panel();
            paymentLayout = new TableLayoutPanel();
            panelThongTinHoaDon = new Panel();
            labelNhanVien = new Label();
            lblDateTime = new Label();
            panel5 = new Panel();
            cardKhachHang = new Panel();
            lblSdtKhachHang = new Label();
            lblDiemKhachHang = new Label();
            lblTenKhachHang = new Label();
            btnHuyKhachHang = new Button();
            btnNhapKhachHang = new Button();
            labelMaKhachHang = new Label();
            panel4 = new Panel();
            boxMaKhachHang = new TextBox();
            panel3 = new Panel();
            cardVoucher = new Panel();
            lblThongTinVoucherCard = new Label();
            lblMaVoucherCard = new Label();
            btnHuyVoucher = new Button();
            btnNhapMaVoucher = new Button();
            lblMaVoucher = new Label();
            panel6 = new Panel();
            boxMaVoucher = new TextBox();
            panel7 = new Panel();
            labelTienGiamGia = new Label();
            labelGiamGia = new Label();
            labelTienTamTinh = new Label();
            labelTamTinh = new Label();
            panel8 = new Panel();
            labelTienTongTien = new Label();
            labelTongTien = new Label();
            panel13 = new Panel();
            radioChuyenKhoan = new RadioButton();
            radioTienMat = new RadioButton();
            panelTienMat = new Panel();
            panel12 = new Panel();
            boxTraLai = new TextBox();
            label2 = new Label();
            label1 = new Label();
            panel11 = new Panel();
            boxTienKhach = new TextBox();
            panel9 = new Panel();
            btnHuyHoaDon = new Button();
            btnThanhToan = new Button();
            btnLuuCho = new Button();
            panelAction = new Panel();
            lblActionSoLuong = new Label();
            btnSua = new Button();
            numericSoLuong = new NumericUpDown();
            btnXoa = new Button();
            bindingSource1 = new BindingSource(components);
            timerDateTime = new System.Windows.Forms.Timer(components);
            salesLayout.SuspendLayout();
            groupGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableTimKiem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)medicineBindingSource).BeginInit();
            cartLayout.SuspendLayout();
            searchLayout.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableGioHang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).BeginInit();
            entryLayout.SuspendLayout();
            panel2.SuspendLayout();
            groupThanhToan.SuspendLayout();
            paymentScroll.SuspendLayout();
            paymentLayout.SuspendLayout();
            panelThongTinHoaDon.SuspendLayout();
            panel5.SuspendLayout();
            cardKhachHang.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            cardVoucher.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel13.SuspendLayout();
            panelTienMat.SuspendLayout();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel9.SuspendLayout();
            panelAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // salesLayout
            // 
            salesLayout.ColumnCount = 2;
            salesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            salesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 365F));
            salesLayout.Controls.Add(groupGioHang, 0, 0);
            salesLayout.Controls.Add(groupThanhToan, 1, 0);
            salesLayout.Dock = DockStyle.Fill;
            salesLayout.Location = new Point(0, 0);
            salesLayout.Name = "salesLayout";
            salesLayout.Padding = new Padding(10);
            salesLayout.RowCount = 1;
            salesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            salesLayout.Size = new Size(1182, 653);
            salesLayout.TabIndex = 2;
            // 
            // groupGioHang
            // 
            groupGioHang.Controls.Add(tableTimKiem);
            groupGioHang.Controls.Add(cartLayout);
            groupGioHang.Dock = DockStyle.Fill;
            groupGioHang.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupGioHang.Location = new Point(10, 10);
            groupGioHang.Margin = new Padding(0, 0, 8, 0);
            groupGioHang.Name = "groupGioHang";
            groupGioHang.Padding = new Padding(8, 24, 8, 6);
            groupGioHang.Size = new Size(789, 633);
            groupGioHang.TabIndex = 0;
            groupGioHang.TabStop = false;
            groupGioHang.Text = "Tìm kiếm và Giỏ hàng";
            // 
            // tableTimKiem
            // 
            tableTimKiem.AllowUserToAddRows = false;
            tableTimKiem.AllowUserToDeleteRows = false;
            tableTimKiem.AllowUserToResizeColumns = false;
            tableTimKiem.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            tableTimKiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            
            tableTimKiem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            tableTimKiem.BackgroundColor = Color.White;
            tableTimKiem.BorderStyle = BorderStyle.None;
            tableTimKiem.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            tableTimKiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            tableTimKiem.ColumnHeadersHeight = 40;
            tableTimKiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tableTimKiem.ColumnHeadersVisible = false;
            tableTimKiem.DataSource = medicineBindingSource;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(204, 251, 241);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            tableTimKiem.DefaultCellStyle = dataGridViewCellStyle6;
            tableTimKiem.EnableHeadersVisualStyles = false;
            tableTimKiem.Location = new Point(125, 46);
            tableTimKiem.MultiSelect = false;
            tableTimKiem.Name = "tableTimKiem";
            tableTimKiem.ReadOnly = true;
            tableTimKiem.RowHeadersVisible = false;
            tableTimKiem.RowHeadersWidth = 51;
            tableTimKiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableTimKiem.Size = new Size(545, 30);
            tableTimKiem.TabIndex = 4;
            tableTimKiem.Visible = false;
            tableTimKiem.CellContentClick += tableTimKiem_CellContentClick;
            tableTimKiem.CellMouseEnter += tableTimKiem_CellMouseEnter;
            tableTimKiem.DataBindingComplete += tableTimKiem_DataBindingComplete;
            // 
            // idDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            
            
            
            // 
            // btnThemGioHang
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            
            
            
            
            
            
            // 
            // medicineBindingSource
            // 
            medicineBindingSource.DataSource = typeof(Shared.Models.Medicine);
            // 
            // cartLayout
            // 
            cartLayout.ColumnCount = 1;
            cartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cartLayout.Controls.Add(searchLayout, 0, 0);
            cartLayout.Controls.Add(tableGioHang, 0, 1);
            cartLayout.Controls.Add(entryLayout, 0, 2);
            cartLayout.Controls.Add(lblTinhTrangGioHang, 0, 3);
            cartLayout.Dock = DockStyle.Fill;
            cartLayout.Location = new Point(8, 48);
            cartLayout.Margin = new Padding(0);
            cartLayout.Name = "cartLayout";
            cartLayout.RowCount = 4;
            cartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            cartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            cartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            cartLayout.Size = new Size(773, 579);
            cartLayout.TabIndex = 5;
            // 
            // searchLayout
            // 
            searchLayout.ColumnCount = 3;
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 122F));
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            searchLayout.Controls.Add(labelTimKiem, 0, 0);
            searchLayout.Controls.Add(panel1, 1, 0);
            searchLayout.Controls.Add(btnXoaTimKiem, 2, 0);
            searchLayout.Dock = DockStyle.Fill;
            searchLayout.Location = new Point(3, 3);
            searchLayout.Name = "searchLayout";
            searchLayout.RowCount = 1;
            searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchLayout.Size = new Size(767, 42);
            searchLayout.TabIndex = 0;
            // 
            // labelTimKiem
            // 
            labelTimKiem.Dock = DockStyle.Fill;
            labelTimKiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTimKiem.Location = new Point(3, 0);
            labelTimKiem.Name = "labelTimKiem";
            labelTimKiem.Size = new Size(116, 42);
            labelTimKiem.TabIndex = 0;
            labelTimKiem.Text = "Tìm thuốc";
            labelTimKiem.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(boxTimKiem);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(125, 4);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(5);
            panel1.Size = new Size(547, 34);
            panel1.TabIndex = 3;
            // 
            // boxTimKiem
            // 
            boxTimKiem.BackColor = Color.White;
            boxTimKiem.BorderStyle = BorderStyle.None;
            boxTimKiem.Dock = DockStyle.Fill;
            boxTimKiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boxTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
            boxTimKiem.Location = new Point(5, 5);
            boxTimKiem.Name = "boxTimKiem";
            boxTimKiem.PlaceholderText = "Tìm tên thuốc, mã thuốc hoặc mã vạch";
            boxTimKiem.Size = new Size(535, 23);
            boxTimKiem.TabIndex = 2;
            boxTimKiem.TextChanged += boxTimKiem_TextChanged;
            // 
            // btnXoaTimKiem
            // 
            btnXoaTimKiem.BackColor = Color.FromArgb(225, 29, 72);
            btnXoaTimKiem.Cursor = Cursors.Hand;
            btnXoaTimKiem.Dock = DockStyle.Fill;
            btnXoaTimKiem.FlatAppearance.BorderSize = 0;
            btnXoaTimKiem.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
            btnXoaTimKiem.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btnXoaTimKiem.FlatStyle = FlatStyle.Flat;
            btnXoaTimKiem.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnXoaTimKiem.ForeColor = Color.White;
            btnXoaTimKiem.Location = new Point(678, 4);
            btnXoaTimKiem.Margin = new Padding(3, 4, 3, 4);
            btnXoaTimKiem.Name = "btnXoaTimKiem";
            btnXoaTimKiem.Size = new Size(86, 34);
            btnXoaTimKiem.TabIndex = 5;
            btnXoaTimKiem.Text = "Xóa lọc";
            btnXoaTimKiem.UseVisualStyleBackColor = false;
            btnXoaTimKiem.Click += btnXoaTimKiem_Click;
            // 
            // tableGioHang
            // 
            tableGioHang.AllowUserToAddRows = false;
            tableGioHang.AllowUserToDeleteRows = false;
            tableGioHang.AllowUserToResizeColumns = false;
            tableGioHang.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.ControlText;
            tableGioHang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            
            tableGioHang.BackgroundColor = Color.White;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            tableGioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            tableGioHang.ColumnHeadersHeight = 40;
            tableGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tableGioHang.DataSource = rowDataBindingSource;
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = Color.White;
            dataGridViewCellStyle16.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle16.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle16.SelectionBackColor = Color.FromArgb(204, 251, 241);
            dataGridViewCellStyle16.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle16.WrapMode = DataGridViewTriState.False;
            tableGioHang.DefaultCellStyle = dataGridViewCellStyle16;
            tableGioHang.Dock = DockStyle.Fill;
            tableGioHang.EnableHeadersVisualStyles = false;
            tableGioHang.Location = new Point(0, 48);
            tableGioHang.Margin = new Padding(0);
            tableGioHang.MultiSelect = false;
            tableGioHang.Name = "tableGioHang";
            tableGioHang.ReadOnly = true;
            tableGioHang.RowHeadersVisible = false;
            tableGioHang.RowHeadersWidth = 25;
            tableGioHang.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            tableGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableGioHang.Size = new Size(773, 467);
            tableGioHang.TabIndex = 1;
            tableGioHang.CellClick += tableGioHang_CellClick;
            tableGioHang.CellContentClick += tableGioHang_CellContentClick;
            tableGioHang.CellMouseEnter += tableGioHang_CellMouseEnter;
            tableGioHang.DataBindingComplete += tableGioHang_DataBindingComplete;
            tableGioHang.RowsAdded += tableGioHang_RowsAdded;
            tableGioHang.RowsRemoved += tableGioHang_RowsRemoved;
            tableGioHang.Leave += tableGioHang_Leave;
            // 
            // noDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // nameDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            
            
            
            // 
            // unitDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // priceDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // totalDataGridViewTextBoxColumn
            // 
            
            dataGridViewCellStyle14.Format = "N0";
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
            
            
            
            
            // 
            // btnAction
            // 
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = Color.White;
            dataGridViewCellStyle15.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle15.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
            
            
            
            
            
            // 
            // rowDataBindingSource
            // 
            rowDataBindingSource.DataSource = typeof(FormBanHang.RowData);
            // 
            // entryLayout
            // 
            entryLayout.ColumnCount = 2;
            entryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            entryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F));
            entryLayout.Controls.Add(panel2, 0, 0);
            entryLayout.Controls.Add(btnNhapSanPham, 1, 0);
            entryLayout.Dock = DockStyle.Fill;
            entryLayout.Location = new Point(3, 518);
            entryLayout.Name = "entryLayout";
            entryLayout.RowCount = 1;
            entryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            entryLayout.Size = new Size(767, 38);
            entryLayout.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(boxNhapSanPham);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 4);
            panel2.Margin = new Padding(0, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(5);
            panel2.Size = new Size(672, 30);
            panel2.TabIndex = 6;
            // 
            // boxNhapSanPham
            // 
            boxNhapSanPham.BackColor = Color.White;
            boxNhapSanPham.BorderStyle = BorderStyle.None;
            boxNhapSanPham.Dock = DockStyle.Fill;
            boxNhapSanPham.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boxNhapSanPham.ForeColor = Color.FromArgb(15, 23, 42);
            boxNhapSanPham.Location = new Point(5, 5);
            boxNhapSanPham.MaxLength = 50;
            boxNhapSanPham.Name = "boxNhapSanPham";
            boxNhapSanPham.PlaceholderText = "Mã thuốc hoặc mã vạch (quét rồi Enter)";
            boxNhapSanPham.Size = new Size(660, 23);
            boxNhapSanPham.TabIndex = 2;
            boxNhapSanPham.KeyDown += boxNhapSanPham_KeyDown;
            // 
            // btnNhapSanPham
            // 
            btnNhapSanPham.BackColor = Color.FromArgb(15, 118, 110);
            btnNhapSanPham.Cursor = Cursors.Hand;
            btnNhapSanPham.Dock = DockStyle.Fill;
            btnNhapSanPham.FlatAppearance.BorderSize = 0;
            btnNhapSanPham.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnNhapSanPham.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnNhapSanPham.FlatStyle = FlatStyle.Flat;
            btnNhapSanPham.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNhapSanPham.ForeColor = Color.White;
            btnNhapSanPham.Location = new Point(678, 4);
            btnNhapSanPham.Margin = new Padding(3, 4, 3, 4);
            btnNhapSanPham.Name = "btnNhapSanPham";
            btnNhapSanPham.Size = new Size(86, 30);
            btnNhapSanPham.TabIndex = 7;
            btnNhapSanPham.Text = "Nhập";
            btnNhapSanPham.UseVisualStyleBackColor = false;
            btnNhapSanPham.Click += btnNhapSanPham_Click;
            // 
            // lblTinhTrangGioHang
            // 
            lblTinhTrangGioHang.AutoEllipsis = true;
            lblTinhTrangGioHang.Dock = DockStyle.Fill;
            lblTinhTrangGioHang.Font = new Font("Segoe UI", 8F);
            lblTinhTrangGioHang.ForeColor = Color.Firebrick;
            lblTinhTrangGioHang.Location = new Point(0, 559);
            lblTinhTrangGioHang.Margin = new Padding(0);
            lblTinhTrangGioHang.Name = "lblTinhTrangGioHang";
            lblTinhTrangGioHang.Size = new Size(773, 20);
            lblTinhTrangGioHang.TabIndex = 0;
            // 
            // groupThanhToan
            // 
            groupThanhToan.Controls.Add(paymentScroll);
            groupThanhToan.Dock = DockStyle.Fill;
            groupThanhToan.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupThanhToan.Location = new Point(807, 10);
            groupThanhToan.Margin = new Padding(0);
            groupThanhToan.Name = "groupThanhToan";
            groupThanhToan.Padding = new Padding(4, 24, 4, 4);
            groupThanhToan.Size = new Size(365, 633);
            groupThanhToan.TabIndex = 2;
            groupThanhToan.TabStop = false;
            groupThanhToan.Text = "Thông tin thanh toán";
            // 
            // paymentScroll
            // 
            paymentScroll.AutoScroll = true;
            paymentScroll.Controls.Add(paymentLayout);
            paymentScroll.Dock = DockStyle.Fill;
            paymentScroll.Location = new Point(4, 47);
            paymentScroll.Name = "paymentScroll";
            paymentScroll.Size = new Size(357, 582);
            paymentScroll.TabIndex = 0;
            // 
            // paymentLayout
            // 
            paymentLayout.AutoSize = true;
            paymentLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            paymentLayout.ColumnCount = 1;
            paymentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            paymentLayout.Controls.Add(panelThongTinHoaDon, 0, 0);
            paymentLayout.Controls.Add(panel5, 0, 1);
            paymentLayout.Controls.Add(panel3, 0, 2);
            paymentLayout.Controls.Add(panel7, 0, 3);
            paymentLayout.Controls.Add(panel8, 0, 4);
            paymentLayout.Controls.Add(panel13, 0, 5);
            paymentLayout.Controls.Add(panelTienMat, 0, 6);
            paymentLayout.Controls.Add(panel9, 0, 7);
            paymentLayout.Dock = DockStyle.Top;
            paymentLayout.Location = new Point(0, 0);
            paymentLayout.Name = "paymentLayout";
            paymentLayout.RowCount = 8;
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.RowStyles.Add(new RowStyle());
            paymentLayout.Size = new Size(336, 766);
            paymentLayout.TabIndex = 0;
            // 
            // panelThongTinHoaDon
            // 
            panelThongTinHoaDon.Controls.Add(labelNhanVien);
            panelThongTinHoaDon.Controls.Add(lblDateTime);
            panelThongTinHoaDon.Dock = DockStyle.Top;
            panelThongTinHoaDon.Location = new Point(0, 0);
            panelThongTinHoaDon.Margin = new Padding(0);
            panelThongTinHoaDon.Name = "panelThongTinHoaDon";
            panelThongTinHoaDon.Size = new Size(336, 63);
            panelThongTinHoaDon.TabIndex = 1;
            // 
            // labelNhanVien
            // 
            labelNhanVien.AutoSize = true;
            labelNhanVien.Location = new Point(8, 7);
            labelNhanVien.Name = "labelNhanVien";
            labelNhanVien.Size = new Size(93, 23);
            labelNhanVien.TabIndex = 1;
            labelNhanVien.Text = "Nhân viên:";
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDateTime.Location = new Point(8, 30);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(80, 23);
            lblDateTime.TabIndex = 0;
            lblDateTime.Text = "Ngày giờ";
            // 
            // panel5
            // 
            panel5.Controls.Add(cardKhachHang);
            panel5.Controls.Add(btnHuyKhachHang);
            panel5.Controls.Add(btnNhapKhachHang);
            panel5.Controls.Add(labelMaKhachHang);
            panel5.Controls.Add(panel4);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 63);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(336, 140);
            panel5.TabIndex = 3;
            // 
            // cardKhachHang
            // 
            cardKhachHang.BackColor = Color.FromArgb(240, 253, 250);
            cardKhachHang.BorderStyle = BorderStyle.FixedSingle;
            cardKhachHang.Controls.Add(lblSdtKhachHang);
            cardKhachHang.Controls.Add(lblDiemKhachHang);
            cardKhachHang.Controls.Add(lblTenKhachHang);
            cardKhachHang.Location = new Point(8, 74);
            cardKhachHang.Margin = new Padding(0);
            cardKhachHang.Name = "cardKhachHang";
            cardKhachHang.Size = new Size(320, 60);
            cardKhachHang.TabIndex = 7;
            // 
            // lblSdtKhachHang
            // 
            lblSdtKhachHang.AutoEllipsis = true;
            lblSdtKhachHang.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSdtKhachHang.ForeColor = Color.FromArgb(15, 23, 42);
            lblSdtKhachHang.Location = new Point(5, 31);
            lblSdtKhachHang.Name = "lblSdtKhachHang";
            lblSdtKhachHang.Size = new Size(304, 20);
            lblSdtKhachHang.TabIndex = 2;
            lblSdtKhachHang.Text = "Số điện thoại: 0987654321";
            lblSdtKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDiemKhachHang
            // 
            lblDiemKhachHang.AutoEllipsis = true;
            lblDiemKhachHang.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblDiemKhachHang.ForeColor = Color.FromArgb(15, 23, 42);
            lblDiemKhachHang.Location = new Point(220, 3);
            lblDiemKhachHang.Name = "lblDiemKhachHang";
            lblDiemKhachHang.Size = new Size(88, 22);
            lblDiemKhachHang.TabIndex = 1;
            lblDiemKhachHang.Text = "Điểm: 0";
            lblDiemKhachHang.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTenKhachHang
            // 
            lblTenKhachHang.AutoEllipsis = true;
            lblTenKhachHang.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTenKhachHang.ForeColor = Color.FromArgb(15, 23, 42);
            lblTenKhachHang.Location = new Point(5, 4);
            lblTenKhachHang.Name = "lblTenKhachHang";
            lblTenKhachHang.Size = new Size(215, 22);
            lblTenKhachHang.TabIndex = 0;
            lblTenKhachHang.Text = "Tên khách hàng: Lê Thị Mai";
            lblTenKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnHuyKhachHang
            // 
            btnHuyKhachHang.BackColor = Color.FromArgb(100, 116, 139);
            btnHuyKhachHang.Cursor = Cursors.Hand;
            btnHuyKhachHang.FlatAppearance.BorderSize = 0;
            btnHuyKhachHang.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
            btnHuyKhachHang.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
            btnHuyKhachHang.FlatStyle = FlatStyle.Flat;
            btnHuyKhachHang.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHuyKhachHang.ForeColor = Color.White;
            btnHuyKhachHang.Location = new Point(266, 33);
            btnHuyKhachHang.Name = "btnHuyKhachHang";
            btnHuyKhachHang.Size = new Size(62, 35);
            btnHuyKhachHang.TabIndex = 6;
            btnHuyKhachHang.Text = "Hủy";
            btnHuyKhachHang.UseVisualStyleBackColor = false;
            // 
            // btnNhapKhachHang
            // 
            btnNhapKhachHang.BackColor = Color.FromArgb(15, 118, 110);
            btnNhapKhachHang.Cursor = Cursors.Hand;
            btnNhapKhachHang.FlatAppearance.BorderSize = 0;
            btnNhapKhachHang.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnNhapKhachHang.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnNhapKhachHang.FlatStyle = FlatStyle.Flat;
            btnNhapKhachHang.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNhapKhachHang.ForeColor = Color.White;
            btnNhapKhachHang.Location = new Point(198, 33);
            btnNhapKhachHang.Name = "btnNhapKhachHang";
            btnNhapKhachHang.Size = new Size(62, 35);
            btnNhapKhachHang.TabIndex = 5;
            btnNhapKhachHang.Text = "Nhập";
            btnNhapKhachHang.UseVisualStyleBackColor = false;
            btnNhapKhachHang.Click += btnNhapKhachHang_Click;
            // 
            // labelMaKhachHang
            // 
            labelMaKhachHang.AutoSize = true;
            labelMaKhachHang.Location = new Point(8, 9);
            labelMaKhachHang.Name = "labelMaKhachHang";
            labelMaKhachHang.Size = new Size(130, 23);
            labelMaKhachHang.TabIndex = 4;
            labelMaKhachHang.Text = "Mã khách hàng";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.Window;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(boxMaKhachHang);
            panel4.Location = new Point(8, 35);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(3, 4, 3, 3);
            panel4.Size = new Size(184, 33);
            panel4.TabIndex = 2;
            // 
            // boxMaKhachHang
            // 
            boxMaKhachHang.BackColor = Color.White;
            boxMaKhachHang.BorderStyle = BorderStyle.None;
            boxMaKhachHang.Font = new Font("Segoe UI", 10.5F);
            boxMaKhachHang.ForeColor = Color.FromArgb(15, 23, 42);
            boxMaKhachHang.Location = new Point(3, 4);
            boxMaKhachHang.MaxLength = 10;
            boxMaKhachHang.Name = "boxMaKhachHang";
            boxMaKhachHang.Size = new Size(176, 24);
            boxMaKhachHang.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(cardVoucher);
            panel3.Controls.Add(btnHuyVoucher);
            panel3.Controls.Add(btnNhapMaVoucher);
            panel3.Controls.Add(lblMaVoucher);
            panel3.Controls.Add(panel6);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 203);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(336, 140);
            panel3.TabIndex = 4;
            // 
            // cardVoucher
            // 
            cardVoucher.BackColor = Color.FromArgb(240, 253, 250);
            cardVoucher.BorderStyle = BorderStyle.FixedSingle;
            cardVoucher.Controls.Add(lblThongTinVoucherCard);
            cardVoucher.Controls.Add(lblMaVoucherCard);
            cardVoucher.Location = new Point(8, 74);
            cardVoucher.Margin = new Padding(0);
            cardVoucher.Name = "cardVoucher";
            cardVoucher.Size = new Size(320, 60);
            cardVoucher.TabIndex = 7;
            // 
            // lblThongTinVoucherCard
            // 
            lblThongTinVoucherCard.AutoEllipsis = true;
            lblThongTinVoucherCard.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThongTinVoucherCard.ForeColor = Color.FromArgb(15, 23, 42);
            lblThongTinVoucherCard.Location = new Point(5, 31);
            lblThongTinVoucherCard.Name = "lblThongTinVoucherCard";
            lblThongTinVoucherCard.Size = new Size(310, 20);
            lblThongTinVoucherCard.TabIndex = 1;
            lblThongTinVoucherCard.Text = "Thông tin: Giảm 10%";
            lblThongTinVoucherCard.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMaVoucherCard
            // 
            lblMaVoucherCard.AutoEllipsis = true;
            lblMaVoucherCard.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMaVoucherCard.ForeColor = Color.FromArgb(15, 23, 42);
            lblMaVoucherCard.Location = new Point(5, 4);
            lblMaVoucherCard.Name = "lblMaVoucherCard";
            lblMaVoucherCard.Size = new Size(310, 22);
            lblMaVoucherCard.TabIndex = 0;
            lblMaVoucherCard.Text = "Mã voucher: KM10";
            lblMaVoucherCard.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnHuyVoucher
            // 
            btnHuyVoucher.BackColor = Color.FromArgb(100, 116, 139);
            btnHuyVoucher.Cursor = Cursors.Hand;
            btnHuyVoucher.FlatAppearance.BorderSize = 0;
            btnHuyVoucher.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
            btnHuyVoucher.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
            btnHuyVoucher.FlatStyle = FlatStyle.Flat;
            btnHuyVoucher.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHuyVoucher.ForeColor = Color.White;
            btnHuyVoucher.Location = new Point(266, 33);
            btnHuyVoucher.Name = "btnHuyVoucher";
            btnHuyVoucher.Size = new Size(62, 35);
            btnHuyVoucher.TabIndex = 6;
            btnHuyVoucher.Text = "Hủy";
            btnHuyVoucher.UseVisualStyleBackColor = false;
            // 
            // btnNhapMaVoucher
            // 
            btnNhapMaVoucher.BackColor = Color.FromArgb(15, 118, 110);
            btnNhapMaVoucher.Cursor = Cursors.Hand;
            btnNhapMaVoucher.FlatAppearance.BorderSize = 0;
            btnNhapMaVoucher.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnNhapMaVoucher.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnNhapMaVoucher.FlatStyle = FlatStyle.Flat;
            btnNhapMaVoucher.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNhapMaVoucher.ForeColor = Color.White;
            btnNhapMaVoucher.Location = new Point(198, 33);
            btnNhapMaVoucher.Name = "btnNhapMaVoucher";
            btnNhapMaVoucher.Size = new Size(62, 35);
            btnNhapMaVoucher.TabIndex = 5;
            btnNhapMaVoucher.Text = "Nhập";
            btnNhapMaVoucher.UseVisualStyleBackColor = false;
            // 
            // lblMaVoucher
            // 
            lblMaVoucher.AutoSize = true;
            lblMaVoucher.Location = new Point(8, 9);
            lblMaVoucher.Name = "lblMaVoucher";
            lblMaVoucher.Size = new Size(102, 23);
            lblMaVoucher.TabIndex = 4;
            lblMaVoucher.Text = "Mã voucher";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.Window;
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(boxMaVoucher);
            panel6.Location = new Point(8, 35);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3, 4, 3, 3);
            panel6.Size = new Size(184, 33);
            panel6.TabIndex = 2;
            // 
            // boxMaVoucher
            // 
            boxMaVoucher.BackColor = Color.White;
            boxMaVoucher.BorderStyle = BorderStyle.None;
            boxMaVoucher.Font = new Font("Segoe UI", 10.5F);
            boxMaVoucher.ForeColor = Color.FromArgb(15, 23, 42);
            boxMaVoucher.Location = new Point(3, 4);
            boxMaVoucher.Name = "boxMaVoucher";
            boxMaVoucher.Size = new Size(176, 24);
            boxMaVoucher.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(labelTienGiamGia);
            panel7.Controls.Add(labelGiamGia);
            panel7.Controls.Add(labelTienTamTinh);
            panel7.Controls.Add(labelTamTinh);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 343);
            panel7.Margin = new Padding(0);
            panel7.Name = "panel7";
            panel7.Size = new Size(336, 78);
            panel7.TabIndex = 5;
            // 
            // labelTienGiamGia
            // 
            labelTienGiamGia.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienGiamGia.Location = new Point(175, 41);
            labelTienGiamGia.Name = "labelTienGiamGia";
            labelTienGiamGia.RightToLeft = RightToLeft.No;
            labelTienGiamGia.Size = new Size(153, 30);
            labelTienGiamGia.TabIndex = 3;
            labelTienGiamGia.Text = "đ";
            labelTienGiamGia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelGiamGia
            // 
            labelGiamGia.AutoSize = true;
            labelGiamGia.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGiamGia.Location = new Point(8, 41);
            labelGiamGia.Name = "labelGiamGia";
            labelGiamGia.Size = new Size(97, 28);
            labelGiamGia.TabIndex = 2;
            labelGiamGia.Text = "Giảm giá:";
            // 
            // labelTienTamTinh
            // 
            labelTienTamTinh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienTamTinh.Location = new Point(175, 8);
            labelTienTamTinh.Name = "labelTienTamTinh";
            labelTienTamTinh.RightToLeft = RightToLeft.No;
            labelTienTamTinh.Size = new Size(153, 30);
            labelTienTamTinh.TabIndex = 1;
            labelTienTamTinh.Text = "đ";
            labelTienTamTinh.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelTamTinh
            // 
            labelTamTinh.AutoSize = true;
            labelTamTinh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTamTinh.Location = new Point(8, 8);
            labelTamTinh.Name = "labelTamTinh";
            labelTamTinh.Size = new Size(98, 28);
            labelTamTinh.TabIndex = 0;
            labelTamTinh.Text = "Tạm tính:";
            // 
            // panel8
            // 
            panel8.Controls.Add(labelTienTongTien);
            panel8.Controls.Add(labelTongTien);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(0, 421);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Size = new Size(336, 50);
            panel8.TabIndex = 7;
            // 
            // labelTienTongTien
            // 
            labelTienTongTien.BackColor = Color.FromArgb(248, 250, 252);
            labelTienTongTien.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienTongTien.ForeColor = SystemColors.ControlText;
            labelTienTongTien.Location = new Point(175, 11);
            labelTienTongTien.Name = "labelTienTongTien";
            labelTienTongTien.RightToLeft = RightToLeft.No;
            labelTienTongTien.Size = new Size(153, 30);
            labelTienTongTien.TabIndex = 7;
            labelTienTongTien.Text = "đ";
            labelTienTongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelTongTien
            // 
            labelTongTien.AutoSize = true;
            labelTongTien.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTongTien.Location = new Point(8, 11);
            labelTongTien.Name = "labelTongTien";
            labelTongTien.Size = new Size(119, 31);
            labelTongTien.TabIndex = 6;
            labelTongTien.Text = "Tổng tiền:";
            // 
            // panel13
            // 
            panel13.Controls.Add(radioChuyenKhoan);
            panel13.Controls.Add(radioTienMat);
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(0, 471);
            panel13.Margin = new Padding(0);
            panel13.Name = "panel13";
            panel13.Size = new Size(336, 44);
            panel13.TabIndex = 7;
            // 
            // radioChuyenKhoan
            // 
            radioChuyenKhoan.AutoSize = true;
            radioChuyenKhoan.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioChuyenKhoan.Location = new Point(160, 6);
            radioChuyenKhoan.Name = "radioChuyenKhoan";
            radioChuyenKhoan.Size = new Size(165, 32);
            radioChuyenKhoan.TabIndex = 1;
            radioChuyenKhoan.Text = "Chuyển khoản";
            radioChuyenKhoan.UseVisualStyleBackColor = true;
            radioChuyenKhoan.CheckedChanged += radioChuyenKhoan_CheckedChanged;
            radioChuyenKhoan.Click += radioChuyenKhoan_Click;
            // 
            // radioTienMat
            // 
            radioTienMat.AutoSize = true;
            radioTienMat.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioTienMat.Location = new Point(12, 6);
            radioTienMat.Name = "radioTienMat";
            radioTienMat.Size = new Size(113, 32);
            radioTienMat.TabIndex = 0;
            radioTienMat.Text = "Tiền mặt";
            radioTienMat.UseVisualStyleBackColor = true;
            radioTienMat.CheckedChanged += radioTienMat_CheckedChanged;
            radioTienMat.Click += radioTienMat_Click;
            // 
            // panelTienMat
            // 
            panelTienMat.Controls.Add(panel12);
            panelTienMat.Controls.Add(label2);
            panelTienMat.Controls.Add(label1);
            panelTienMat.Controls.Add(panel11);
            panelTienMat.Dock = DockStyle.Top;
            panelTienMat.Location = new Point(0, 515);
            panelTienMat.Margin = new Padding(0);
            panelTienMat.Name = "panelTienMat";
            panelTienMat.Size = new Size(336, 141);
            panelTienMat.TabIndex = 6;
            panelTienMat.Visible = false;
            // 
            // panel12
            // 
            panel12.BackColor = Color.FromArgb(241, 245, 249);
            panel12.BorderStyle = BorderStyle.FixedSingle;
            panel12.Controls.Add(boxTraLai);
            panel12.Location = new Point(138, 50);
            panel12.Name = "panel12";
            panel12.Padding = new Padding(3, 4, 3, 3);
            panel12.Size = new Size(190, 33);
            panel12.TabIndex = 8;
            // 
            // boxTraLai
            // 
            boxTraLai.BackColor = Color.FromArgb(241, 245, 249);
            boxTraLai.BorderStyle = BorderStyle.None;
            boxTraLai.Enabled = false;
            boxTraLai.Font = new Font("Segoe UI", 10.5F);
            boxTraLai.ForeColor = Color.FromArgb(15, 23, 42);
            boxTraLai.Location = new Point(3, 4);
            boxTraLai.Name = "boxTraLai";
            boxTraLai.Size = new Size(182, 24);
            boxTraLai.TabIndex = 0;
            boxTraLai.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 55);
            label2.Name = "label2";
            label2.Size = new Size(94, 23);
            label2.TabIndex = 7;
            label2.Text = "Tiền trả lại:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 14);
            label1.Name = "label1";
            label1.Size = new Size(132, 23);
            label1.TabIndex = 4;
            label1.Text = "Tiền khách đưa:";
            // 
            // panel11
            // 
            panel11.BackColor = SystemColors.Window;
            panel11.BorderStyle = BorderStyle.FixedSingle;
            panel11.Controls.Add(boxTienKhach);
            panel11.Location = new Point(138, 9);
            panel11.Name = "panel11";
            panel11.Padding = new Padding(3, 4, 3, 3);
            panel11.Size = new Size(190, 33);
            panel11.TabIndex = 2;
            // 
            // boxTienKhach
            // 
            boxTienKhach.BackColor = Color.White;
            boxTienKhach.BorderStyle = BorderStyle.None;
            boxTienKhach.Enabled = false;
            boxTienKhach.Font = new Font("Segoe UI", 10.5F);
            boxTienKhach.ForeColor = Color.FromArgb(15, 23, 42);
            boxTienKhach.Location = new Point(3, 4);
            boxTienKhach.Name = "boxTienKhach";
            boxTienKhach.Size = new Size(182, 24);
            boxTienKhach.TabIndex = 0;
            boxTienKhach.TextAlign = HorizontalAlignment.Right;
            boxTienKhach.TextChanged += boxTienKhach_TextChanged;
            boxTienKhach.KeyPress += boxTienKhach_KeyPress;
            // 
            // panel9
            // 
            panel9.Controls.Add(btnHuyHoaDon);
            panel9.Controls.Add(btnThanhToan);
            panel9.Controls.Add(btnLuuCho);
            panel9.Dock = DockStyle.Top;
            panel9.Location = new Point(0, 656);
            panel9.Margin = new Padding(0);
            panel9.Name = "panel9";
            panel9.Size = new Size(336, 110);
            panel9.TabIndex = 6;
            // 
            // btnHuyHoaDon
            // 
            btnHuyHoaDon.BackColor = Color.FromArgb(225, 29, 72);
            btnHuyHoaDon.Cursor = Cursors.Hand;
            btnHuyHoaDon.FlatAppearance.BorderSize = 0;
            btnHuyHoaDon.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
            btnHuyHoaDon.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btnHuyHoaDon.FlatStyle = FlatStyle.Flat;
            btnHuyHoaDon.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHuyHoaDon.ForeColor = Color.White;
            btnHuyHoaDon.Location = new Point(172, 4);
            btnHuyHoaDon.Name = "btnHuyHoaDon";
            btnHuyHoaDon.Size = new Size(164, 52);
            btnHuyHoaDon.TabIndex = 1;
            btnHuyHoaDon.Text = "HỦY HÓA ĐƠN";
            btnHuyHoaDon.UseVisualStyleBackColor = false;
            btnHuyHoaDon.Click += btnHuyHoaDon_Click;
            // 
            // btnThanhToan
            // 
            btnThanhToan.BackColor = Color.FromArgb(22, 101, 52);
            btnThanhToan.Cursor = Cursors.Hand;
            btnThanhToan.FlatAppearance.BorderSize = 0;
            btnThanhToan.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
            btnThanhToan.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(0, 4);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(164, 52);
            btnThanhToan.TabIndex = 0;
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // btnLuuCho
            // 
            btnLuuCho.BackColor = Color.FromArgb(204, 251, 241);
            btnLuuCho.Cursor = Cursors.Hand;
            btnLuuCho.Dock = DockStyle.Bottom;
            btnLuuCho.Enabled = false;
            btnLuuCho.FlatAppearance.BorderSize = 0;
            btnLuuCho.FlatAppearance.MouseDownBackColor = Color.FromArgb(94, 234, 212);
            btnLuuCho.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 246, 228);
            btnLuuCho.FlatStyle = FlatStyle.Flat;
            btnLuuCho.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLuuCho.ForeColor = Color.FromArgb(19, 78, 74);
            btnLuuCho.Location = new Point(0, 68);
            btnLuuCho.Name = "btnLuuCho";
            btnLuuCho.Size = new Size(336, 42);
            btnLuuCho.TabIndex = 2;
            btnLuuCho.Text = "Lưu chờ thanh toán";
            btnLuuCho.UseVisualStyleBackColor = false;
            // 
            // panelAction
            // 
            panelAction.BorderStyle = BorderStyle.FixedSingle;
            panelAction.Controls.Add(lblActionSoLuong);
            panelAction.Controls.Add(btnSua);
            panelAction.Controls.Add(numericSoLuong);
            panelAction.Controls.Add(btnXoa);
            panelAction.Location = new Point(810, 110);
            panelAction.Margin = new Padding(0, 90, 8, 0);
            panelAction.Name = "panelAction";
            panelAction.Size = new Size(112, 140);
            panelAction.TabIndex = 1;
            panelAction.Visible = false;
            // 
            // lblActionSoLuong
            // 
            lblActionSoLuong.Font = new Font("Segoe UI", 9F);
            lblActionSoLuong.Location = new Point(5, 3);
            lblActionSoLuong.Name = "lblActionSoLuong";
            lblActionSoLuong.Size = new Size(100, 20);
            lblActionSoLuong.TabIndex = 0;
            lblActionSoLuong.Text = "Số lượng";
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSua.BackColor = Color.FromArgb(15, 118, 110);
            btnSua.Cursor = Cursors.Hand;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnSua.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSua.ForeColor = Color.White;
            btnSua.Location = new Point(5, 58);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 34);
            btnSua.TabIndex = 5;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // numericSoLuong
            // 
            numericSoLuong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numericSoLuong.BackColor = Color.White;
            numericSoLuong.BorderStyle = BorderStyle.FixedSingle;
            numericSoLuong.Font = new Font("Segoe UI", 10.5F);
            numericSoLuong.ForeColor = Color.FromArgb(15, 23, 42);
            numericSoLuong.Location = new Point(5, 25);
            numericSoLuong.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericSoLuong.Name = "numericSoLuong";
            numericSoLuong.Size = new Size(100, 31);
            numericSoLuong.TabIndex = 4;
            numericSoLuong.KeyDown += numericSoLuong_KeyDown;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnXoa.BackColor = Color.FromArgb(225, 29, 72);
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
            btnXoa.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(5, 98);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 34);
            btnXoa.TabIndex = 1;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // timerDateTime
            // 
            timerDateTime.Interval = 1000;
            timerDateTime.Tick += timerDateTime_Tick;
            // 
            // frmBanHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1182, 653);
            Controls.Add(panelAction);
            Controls.Add(salesLayout);
            ForeColor = Color.FromArgb(15, 23, 42);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmBanHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng | Hiệu thuốc ABC";
            FormClosing += frmBanHang_FormClosing;
            FormClosed += frmBanHang_FormClosed;
            KeyDown += frmBanHang_KeyDown;
            salesLayout.ResumeLayout(false);
            groupGioHang.ResumeLayout(false);
                    //
        // idDataGridViewTextBoxColumn
        //
        this.idDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
        this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
        this.idDataGridViewTextBoxColumn.FillWeight = 48F;
        this.idDataGridViewTextBoxColumn.HeaderText = "Mã thuốc";
        this.idDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
        this.idDataGridViewTextBoxColumn.ReadOnly = true;
        this.idDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.idDataGridViewTextBoxColumn.Width = 70;
        //
        // nameDataGridViewTextBoxColumn1
        //
        this.nameDataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
        this.nameDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
        this.nameDataGridViewTextBoxColumn1.FillWeight = 220F;
        this.nameDataGridViewTextBoxColumn1.HeaderText = "Tên thuốc";
        this.nameDataGridViewTextBoxColumn1.MinimumWidth = 5;
        this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
        this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
        this.nameDataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
        this.nameDataGridViewTextBoxColumn1.Width = 150;
        //
        // btnThemGioHang
        //
        this.btnThemGioHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.btnThemGioHang.DefaultCellStyle = dataGridViewCellStyle5;
        this.btnThemGioHang.FillWeight = 48F;
        this.btnThemGioHang.FlatStyle = FlatStyle.Flat;
        this.btnThemGioHang.HeaderText = "Thêm";
        this.btnThemGioHang.MinimumWidth = 5;
        this.btnThemGioHang.Name = "btnThemGioHang";
        this.btnThemGioHang.ReadOnly = true;
        this.btnThemGioHang.Resizable = DataGridViewTriState.False;
        this.btnThemGioHang.SortMode = DataGridViewColumnSortMode.Automatic;
        this.btnThemGioHang.Text = "+";
        this.btnThemGioHang.UseColumnTextForButtonValue = true;
        this.btnThemGioHang.Width = 42;
        //
        // noDataGridViewTextBoxColumn
        //
        this.noDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.noDataGridViewTextBoxColumn.DataPropertyName = "No";
        this.noDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
        this.noDataGridViewTextBoxColumn.FillWeight = 48F;
        this.noDataGridViewTextBoxColumn.HeaderText = "Số thứ tự";
        this.noDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.noDataGridViewTextBoxColumn.Name = "noDataGridViewTextBoxColumn";
        this.noDataGridViewTextBoxColumn.ReadOnly = true;
        this.noDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.noDataGridViewTextBoxColumn.Width = 72;
        //
        // nameDataGridViewTextBoxColumn
        //
        this.nameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
        this.nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
        this.nameDataGridViewTextBoxColumn.FillWeight = 220F;
        this.nameDataGridViewTextBoxColumn.HeaderText = "Tên thuốc";
        this.nameDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
        this.nameDataGridViewTextBoxColumn.ReadOnly = true;
        this.nameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.nameDataGridViewTextBoxColumn.Width = 150;
        //
        // unitDataGridViewTextBoxColumn
        //
        this.unitDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
        this.unitDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
        this.unitDataGridViewTextBoxColumn.FillWeight = 72F;
        this.unitDataGridViewTextBoxColumn.HeaderText = "Đơn vị tính";
        this.unitDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
        this.unitDataGridViewTextBoxColumn.ReadOnly = true;
        this.unitDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.unitDataGridViewTextBoxColumn.Width = 96;
        //
        // quantityDataGridViewTextBoxColumn
        //
        this.quantityDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
        this.quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
        this.quantityDataGridViewTextBoxColumn.FillWeight = 48F;
        this.quantityDataGridViewTextBoxColumn.HeaderText = "Số lượng";
        this.quantityDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
        this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
        this.quantityDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.quantityDataGridViewTextBoxColumn.Width = 62;
        //
        // priceDataGridViewTextBoxColumn
        //
        this.priceDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
        this.priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
        this.priceDataGridViewTextBoxColumn.FillWeight = 80F;
        this.priceDataGridViewTextBoxColumn.HeaderText = "Đơn giá";
        this.priceDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
        this.priceDataGridViewTextBoxColumn.ReadOnly = true;
        this.priceDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.priceDataGridViewTextBoxColumn.Width = 115;
        //
        // totalDataGridViewTextBoxColumn
        //
        this.totalDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
        this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
        this.totalDataGridViewTextBoxColumn.FillWeight = 104F;
        this.totalDataGridViewTextBoxColumn.HeaderText = "Thành tiền";
        this.totalDataGridViewTextBoxColumn.MinimumWidth = 5;
        this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
        this.totalDataGridViewTextBoxColumn.ReadOnly = true;
        this.totalDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
        this.totalDataGridViewTextBoxColumn.Width = 126;
        //
        // btnAction
        //
        this.btnAction.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.btnAction.DefaultCellStyle = dataGridViewCellStyle15;
        this.btnAction.FillWeight = 48F;
        this.btnAction.FlatStyle = FlatStyle.Flat;
        this.btnAction.HeaderText = "Thao tác";
        this.btnAction.MinimumWidth = 5;
        this.btnAction.Name = "btnAction";
        this.btnAction.ReadOnly = true;
        this.btnAction.Resizable = DataGridViewTriState.False;
        this.btnAction.Text = "+";
        this.btnAction.UseColumnTextForButtonValue = true;
        this.btnAction.Width = 42;

        this.tableTimKiem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.tableTimKiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.idDataGridViewTextBoxColumn, this.nameDataGridViewTextBoxColumn1, this.btnThemGioHang });

        this.tableGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.tableGioHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.noDataGridViewTextBoxColumn, this.nameDataGridViewTextBoxColumn, this.unitDataGridViewTextBoxColumn, this.quantityDataGridViewTextBoxColumn, this.priceDataGridViewTextBoxColumn, this.totalDataGridViewTextBoxColumn, this.btnAction });
        ((System.ComponentModel.ISupportInitialize)tableTimKiem).EndInit();
            ((System.ComponentModel.ISupportInitialize)medicineBindingSource).EndInit();
            cartLayout.ResumeLayout(false);
            searchLayout.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tableGioHang).EndInit();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).EndInit();
            entryLayout.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupThanhToan.ResumeLayout(false);
            paymentScroll.ResumeLayout(false);
            paymentScroll.PerformLayout();
            paymentLayout.ResumeLayout(false);
            panelThongTinHoaDon.ResumeLayout(false);
            panelThongTinHoaDon.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            cardKhachHang.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            cardVoucher.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panelTienMat.ResumeLayout(false);
            panelTienMat.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel9.ResumeLayout(false);
            panelAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label labelTimKiem;
        private BindingSource rowDataBindingSource;
        private DataGridView tableGioHang;
        private Panel panelAction;
        private Label lblActionSoLuong;
        private Button btnXoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn btnAction;
        private Panel panel1;
        private TextBox boxTimKiem;
        private DataGridView tableTimKiem;
        private BindingSource medicineBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn btnThemGioHang;
        private GroupBox groupThanhToan;
        private GroupBox groupGioHang;
        private Button btnXoaTimKiem;
        private NumericUpDown numericSoLuong;
        private Button btnSua;
        private BindingSource bindingSource1;
        private Panel panel2;
        private TextBox boxNhapSanPham;
        private Button btnNhapSanPham;
        private Label lblDateTime;
        private System.Windows.Forms.Timer timerDateTime;
        private Panel panelThongTinHoaDon;
        private Label labelNhanVien;
        private Panel panel4;
        private Panel panel5;
        private Panel cardKhachHang;
        private Label lblTenKhachHang;
        private Label lblDiemKhachHang;
        private Label lblSdtKhachHang;
        private Label labelMaKhachHang;
        private TextBox boxMaKhachHang;
        private Button btnNhapKhachHang;
        private Panel panel3;
        private Panel cardVoucher;
        private Label lblMaVoucherCard;
        private Label lblThongTinVoucherCard;
        private Button btnNhapMaVoucher;
        private Label lblMaVoucher;
        private Panel panel6;
        private TextBox boxMaVoucher;
        private Button btnHuyKhachHang;
        private Button btnHuyVoucher;
        private Panel panel7;
        private Label labelTienTamTinh;
        private Label labelTamTinh;
        private Label labelTienGiamGia;
        private Label labelGiamGia;
        private Label labelTongTien;
        private Panel panel8;
        private Label labelTienTongTien;
        private Panel panel9;
        private Button btnHuyHoaDon;
        private Button btnThanhToan;
        private Button btnLuuCho;
        private Panel panelTienMat;
        private Label label1;
        private Panel panel11;
        private TextBox boxTienKhach;
        private Panel panel12;
        private TextBox boxTraLai;
        private Label label2;
        private Panel panel13;
        private RadioButton radioTienMat;
        private RadioButton radioChuyenKhoan;
        private Label lblTinhTrangGioHang;
        private TableLayoutPanel salesLayout;
        private TableLayoutPanel cartLayout;
        private TableLayoutPanel searchLayout;
        private TableLayoutPanel entryLayout;
        private Panel paymentScroll;
        private TableLayoutPanel paymentLayout;
    }
}
