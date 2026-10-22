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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            tableTimKiem = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            btnThemGioHang = new DataGridViewButtonColumn();
            medicineBindingSource = new BindingSource(components);
            panel1 = new Panel();
            boxTimKiem = new TextBox();
            tableGioHang = new DataGridView();
            noDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            unitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            quantityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            totalDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            btnAction = new DataGridViewButtonColumn();
            rowDataBindingSource = new BindingSource(components);
            labelTimKiem = new Label();
            panelAction = new Panel();
            btnSua = new Button();
            numericSoLuong = new NumericUpDown();
            btnXoa = new Button();
            groupThanhToan = new GroupBox();
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
            panel7 = new Panel();
            labelTienGiamGia = new Label();
            labelGiamGia = new Label();
            labelTienTamTinh = new Label();
            labelTamTinh = new Label();
            panel3 = new Panel();
            btnHuyVoucher = new Button();
            btnNhapMaVoucher = new Button();
            lblMaVoucher = new Label();
            panel6 = new Panel();
            boxMaVoucher = new TextBox();
            panel5 = new Panel();
            btnHuyKhachHang = new Button();
            btnNhapKhachHang = new Button();
            labelMaKhachHang = new Label();
            panel4 = new Panel();
            boxMaKhachHang = new TextBox();
            panelThongTinHoaDon = new Panel();
            labelNhanVien = new Label();
            lblDateTime = new Label();
            panel9 = new Panel();
            btnHuyHoaDon = new Button();
            btnThanhToan = new Button();
            groupGioHang = new GroupBox();
            btnNhapSanPham = new Button();
            panel2 = new Panel();
            boxNhapSanPham = new TextBox();
            btnXoaTimKiem = new Button();
            bindingSource1 = new BindingSource(components);
            timerDateTime = new System.Windows.Forms.Timer(components);
            labelTongTien = new Label();
            panel8 = new Panel();
            labelTienTongTien = new Label();
            ((System.ComponentModel.ISupportInitialize)tableTimKiem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)medicineBindingSource).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tableGioHang).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).BeginInit();
            panelAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericSoLuong).BeginInit();
            groupThanhToan.SuspendLayout();
            panel13.SuspendLayout();
            panelTienMat.SuspendLayout();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel7.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panelThongTinHoaDon.SuspendLayout();
            panel9.SuspendLayout();
            groupGioHang.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // tableTimKiem
            // 
            tableTimKiem.AllowUserToAddRows = false;
            tableTimKiem.AllowUserToDeleteRows = false;
            tableTimKiem.AllowUserToResizeColumns = false;
            tableTimKiem.AllowUserToResizeRows = false;
            tableTimKiem.AutoGenerateColumns = false;
            tableTimKiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableTimKiem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            tableTimKiem.BackgroundColor = SystemColors.ButtonFace;
            tableTimKiem.BorderStyle = BorderStyle.None;
            tableTimKiem.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            tableTimKiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableTimKiem.ColumnHeadersVisible = false;
            tableTimKiem.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn1, btnThemGioHang });
            tableTimKiem.DataSource = medicineBindingSource;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            tableTimKiem.DefaultCellStyle = dataGridViewCellStyle6;
            tableTimKiem.Location = new Point(215, 83);
            tableTimKiem.Name = "tableTimKiem";
            tableTimKiem.ReadOnly = true;
            tableTimKiem.RowHeadersVisible = false;
            tableTimKiem.RowHeadersWidth = 51;
            tableTimKiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableTimKiem.Size = new Size(430, 30);
            tableTimKiem.TabIndex = 4;
            tableTimKiem.Visible = false;
            tableTimKiem.CellContentClick += tableTimKiem_CellContentClick;
            tableTimKiem.CellMouseEnter += tableTimKiem_CellMouseEnter;
            tableTimKiem.DataBindingComplete += tableTimKiem_DataBindingComplete;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.FillWeight = 15F;
            idDataGridViewTextBoxColumn.HeaderText = "Mã";
            idDataGridViewTextBoxColumn.MinimumWidth = 6;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn1.FillWeight = 75F;
            nameDataGridViewTextBoxColumn1.HeaderText = "Tên thuốc";
            nameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // btnThemGioHang
            // 
            btnThemGioHang.FillWeight = 10F;
            btnThemGioHang.FlatStyle = FlatStyle.Flat;
            btnThemGioHang.HeaderText = "";
            btnThemGioHang.MinimumWidth = 6;
            btnThemGioHang.Name = "btnThemGioHang";
            btnThemGioHang.ReadOnly = true;
            btnThemGioHang.Resizable = DataGridViewTriState.True;
            btnThemGioHang.SortMode = DataGridViewColumnSortMode.Automatic;
            btnThemGioHang.Text = "+";
            btnThemGioHang.UseColumnTextForButtonValue = true;
            // 
            // medicineBindingSource
            // 
            medicineBindingSource.DataSource = typeof(Shared.Models.Medicine);
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(boxTimKiem);
            panel1.Location = new Point(215, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(430, 34);
            panel1.TabIndex = 3;
            // 
            // boxTimKiem
            // 
            boxTimKiem.BorderStyle = BorderStyle.None;
            boxTimKiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boxTimKiem.Location = new Point(6, 4);
            boxTimKiem.Name = "boxTimKiem";
            boxTimKiem.PlaceholderText = "Nhập tên thuốc hoặc mã thuốc cần tìm";
            boxTimKiem.Size = new Size(408, 23);
            boxTimKiem.TabIndex = 2;
            boxTimKiem.TextChanged += boxTimKiem_TextChanged;
            // 
            // tableGioHang
            // 
            tableGioHang.AllowUserToAddRows = false;
            tableGioHang.AllowUserToDeleteRows = false;
            tableGioHang.AllowUserToResizeColumns = false;
            tableGioHang.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.ControlText;
            tableGioHang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            tableGioHang.AutoGenerateColumns = false;
            tableGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableGioHang.BackgroundColor = SystemColors.ScrollBar;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            tableGioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            tableGioHang.ColumnHeadersHeight = 30;
            tableGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tableGioHang.Columns.AddRange(new DataGridViewColumn[] { noDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, unitDataGridViewTextBoxColumn, quantityDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, totalDataGridViewTextBoxColumn, btnAction });
            tableGioHang.DataSource = rowDataBindingSource;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Window;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle10.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            tableGioHang.DefaultCellStyle = dataGridViewCellStyle10;
            tableGioHang.EnableHeadersVisualStyles = false;
            tableGioHang.Location = new Point(0, 100);
            tableGioHang.MultiSelect = false;
            tableGioHang.Name = "tableGioHang";
            tableGioHang.ReadOnly = true;
            tableGioHang.RowHeadersVisible = false;
            tableGioHang.RowHeadersWidth = 25;
            tableGioHang.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            tableGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableGioHang.Size = new Size(801, 454);
            tableGioHang.TabIndex = 1;
            tableGioHang.CellContentClick += tableGioHang_CellContentClick;
            tableGioHang.CellMouseEnter += tableGioHang_CellMouseEnter;
            tableGioHang.CellMouseLeave += AnActionNeuChuotRaNgoai;
            tableGioHang.DataBindingComplete += tableGioHang_DataBindingComplete;
            tableGioHang.RowsAdded += tableGioHang_RowsAdded;
            tableGioHang.RowsRemoved += tableGioHang_RowsRemoved;
            tableGioHang.Leave += tableGioHang_Leave;
            // 
            // noDataGridViewTextBoxColumn
            // 
            noDataGridViewTextBoxColumn.DataPropertyName = "No";
            noDataGridViewTextBoxColumn.FillWeight = 6F;
            noDataGridViewTextBoxColumn.HeaderText = "STT";
            noDataGridViewTextBoxColumn.MinimumWidth = 6;
            noDataGridViewTextBoxColumn.Name = "noDataGridViewTextBoxColumn";
            noDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.FillWeight = 41F;
            nameDataGridViewTextBoxColumn.HeaderText = "Tên thuốc";
            nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            unitDataGridViewTextBoxColumn.FillWeight = 12F;
            unitDataGridViewTextBoxColumn.HeaderText = "Đơn vị";
            unitDataGridViewTextBoxColumn.MinimumWidth = 6;
            unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            unitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            quantityDataGridViewTextBoxColumn.FillWeight = 6F;
            quantityDataGridViewTextBoxColumn.HeaderText = "SL";
            quantityDataGridViewTextBoxColumn.MinimumWidth = 6;
            quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            quantityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            priceDataGridViewTextBoxColumn.FillWeight = 15F;
            priceDataGridViewTextBoxColumn.HeaderText = "Đơn giá";
            priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // totalDataGridViewTextBoxColumn
            // 
            totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            totalDataGridViewTextBoxColumn.FillWeight = 15F;
            totalDataGridViewTextBoxColumn.HeaderText = "Thành tiền";
            totalDataGridViewTextBoxColumn.MinimumWidth = 6;
            totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            totalDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btnAction
            // 
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.GradientActiveCaption;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.ControlText;
            btnAction.DefaultCellStyle = dataGridViewCellStyle9;
            btnAction.FillWeight = 5F;
            btnAction.FlatStyle = FlatStyle.Flat;
            btnAction.HeaderText = "";
            btnAction.MinimumWidth = 6;
            btnAction.Name = "btnAction";
            btnAction.ReadOnly = true;
            btnAction.Text = "+";
            btnAction.UseColumnTextForButtonValue = true;
            // 
            // rowDataBindingSource
            // 
            rowDataBindingSource.DataSource = typeof(FormBanHang.RowData);
            // 
            // labelTimKiem
            // 
            labelTimKiem.AutoSize = true;
            labelTimKiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTimKiem.Location = new Point(81, 49);
            labelTimKiem.Name = "labelTimKiem";
            labelTimKiem.Size = new Size(132, 23);
            labelTimKiem.TabIndex = 0;
            labelTimKiem.Text = "Tìm kiếm thuốc:";
            // 
            // panelAction
            // 
            panelAction.Controls.Add(btnSua);
            panelAction.Controls.Add(numericSoLuong);
            panelAction.Controls.Add(btnXoa);
            panelAction.Location = new Point(782, -92);
            panelAction.Name = "panelAction";
            panelAction.Size = new Size(115, 100);
            panelAction.TabIndex = 1;
            panelAction.Visible = false;
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSua.BackColor = Color.FromArgb(192, 255, 192);
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 255, 192);
            btnSua.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 255, 128);
            btnSua.FlatStyle = FlatStyle.Popup;
            btnSua.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSua.ForeColor = SystemColors.ControlText;
            btnSua.Location = new Point(33, 30);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(80, 34);
            btnSua.TabIndex = 5;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // numericSoLuong
            // 
            numericSoLuong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numericSoLuong.BorderStyle = BorderStyle.FixedSingle;
            numericSoLuong.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            numericSoLuong.Location = new Point(33, 2);
            numericSoLuong.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericSoLuong.Name = "numericSoLuong";
            numericSoLuong.Size = new Size(82, 27);
            numericSoLuong.TabIndex = 4;
            numericSoLuong.KeyDown += numericSoLuong_KeyDown;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoa.BackColor = Color.FromArgb(255, 192, 192);
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192);
            btnXoa.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 128, 128);
            btnXoa.FlatStyle = FlatStyle.Popup;
            btnXoa.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoa.ForeColor = SystemColors.ControlText;
            btnXoa.Location = new Point(33, 64);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(80, 34);
            btnXoa.TabIndex = 1;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // groupThanhToan
            // 
            groupThanhToan.Controls.Add(panel13);
            groupThanhToan.Controls.Add(panelTienMat);
            groupThanhToan.Controls.Add(panel7);
            groupThanhToan.Controls.Add(panel3);
            groupThanhToan.Controls.Add(panel5);
            groupThanhToan.Controls.Add(panelThongTinHoaDon);
            groupThanhToan.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupThanhToan.Location = new Point(820, 12);
            groupThanhToan.Name = "groupThanhToan";
            groupThanhToan.Size = new Size(351, 629);
            groupThanhToan.TabIndex = 2;
            groupThanhToan.TabStop = false;
            groupThanhToan.Text = "Thông tin thanh toán";
            // 
            // panel13
            // 
            panel13.Controls.Add(radioChuyenKhoan);
            panel13.Controls.Add(radioTienMat);
            panel13.Location = new Point(1, 367);
            panel13.Name = "panel13";
            panel13.Size = new Size(349, 44);
            panel13.TabIndex = 7;
            // 
            // radioChuyenKhoan
            // 
            radioChuyenKhoan.AutoSize = true;
            radioChuyenKhoan.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            radioChuyenKhoan.Location = new Point(179, 6);
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
            panelTienMat.Location = new Point(1, 409);
            panelTienMat.Name = "panelTienMat";
            panelTienMat.Size = new Size(349, 141);
            panelTienMat.TabIndex = 6;
            panelTienMat.Visible = false;
            // 
            // panel12
            // 
            panel12.BackColor = SystemColors.Menu;
            panel12.BorderStyle = BorderStyle.FixedSingle;
            panel12.Controls.Add(boxTraLai);
            panel12.Location = new Point(146, 50);
            panel12.Name = "panel12";
            panel12.Size = new Size(198, 35);
            panel12.TabIndex = 8;
            // 
            // boxTraLai
            // 
            boxTraLai.BorderStyle = BorderStyle.None;
            boxTraLai.Enabled = false;
            boxTraLai.Location = new Point(3, 5);
            boxTraLai.Name = "boxTraLai";
            boxTraLai.Size = new Size(190, 23);
            boxTraLai.TabIndex = 0;
            boxTraLai.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 56);
            label2.Name = "label2";
            label2.Size = new Size(94, 23);
            label2.TabIndex = 7;
            label2.Text = "Tiền trả lại:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 16);
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
            panel11.Location = new Point(146, 8);
            panel11.Name = "panel11";
            panel11.Size = new Size(198, 36);
            panel11.TabIndex = 2;
            // 
            // boxTienKhach
            // 
            boxTienKhach.BackColor = SystemColors.Window;
            boxTienKhach.BorderStyle = BorderStyle.None;
            boxTienKhach.Enabled = false;
            boxTienKhach.Location = new Point(3, 5);
            boxTienKhach.Name = "boxTienKhach";
            boxTienKhach.Size = new Size(190, 23);
            boxTienKhach.TabIndex = 0;
            boxTienKhach.TextAlign = HorizontalAlignment.Right;
            boxTienKhach.TextChanged += boxTienKhach_TextChanged;
            boxTienKhach.KeyPress += boxTienKhach_KeyPress;
            // 
            // panel7
            // 
            panel7.Controls.Add(labelTienGiamGia);
            panel7.Controls.Add(labelGiamGia);
            panel7.Controls.Add(labelTienTamTinh);
            panel7.Controls.Add(labelTamTinh);
            panel7.Location = new Point(1, 241);
            panel7.Name = "panel7";
            panel7.Size = new Size(349, 78);
            panel7.TabIndex = 5;
            // 
            // labelTienGiamGia
            // 
            labelTienGiamGia.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienGiamGia.Location = new Point(198, 41);
            labelTienGiamGia.Name = "labelTienGiamGia";
            labelTienGiamGia.RightToLeft = RightToLeft.No;
            labelTienGiamGia.Size = new Size(146, 25);
            labelTienGiamGia.TabIndex = 3;
            labelTienGiamGia.Text = "đ";
            labelTienGiamGia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelGiamGia
            // 
            labelGiamGia.AutoSize = true;
            labelGiamGia.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelGiamGia.Location = new Point(8, 38);
            labelGiamGia.Name = "labelGiamGia";
            labelGiamGia.Size = new Size(97, 28);
            labelGiamGia.TabIndex = 2;
            labelGiamGia.Text = "Giảm giá:";
            // 
            // labelTienTamTinh
            // 
            labelTienTamTinh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienTamTinh.Location = new Point(198, 10);
            labelTienTamTinh.Name = "labelTienTamTinh";
            labelTienTamTinh.RightToLeft = RightToLeft.No;
            labelTienTamTinh.Size = new Size(146, 25);
            labelTienTamTinh.TabIndex = 1;
            labelTienTamTinh.Text = "đ";
            labelTienTamTinh.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelTamTinh
            // 
            labelTamTinh.AutoSize = true;
            labelTamTinh.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTamTinh.Location = new Point(8, 10);
            labelTamTinh.Name = "labelTamTinh";
            labelTamTinh.Size = new Size(98, 28);
            labelTamTinh.TabIndex = 0;
            labelTamTinh.Text = "Tạm tính:";
            // 
            // panel3
            // 
            panel3.Controls.Add(btnHuyVoucher);
            panel3.Controls.Add(btnNhapMaVoucher);
            panel3.Controls.Add(lblMaVoucher);
            panel3.Controls.Add(panel6);
            panel3.Location = new Point(1, 166);
            panel3.Name = "panel3";
            panel3.Size = new Size(349, 76);
            panel3.TabIndex = 4;
            // 
            // btnHuyVoucher
            // 
            btnHuyVoucher.Location = new Point(274, 33);
            btnHuyVoucher.Name = "btnHuyVoucher";
            btnHuyVoucher.Size = new Size(70, 38);
            btnHuyVoucher.TabIndex = 6;
            btnHuyVoucher.Text = "Hủy";
            btnHuyVoucher.UseVisualStyleBackColor = true;
            // 
            // btnNhapMaVoucher
            // 
            btnNhapMaVoucher.Location = new Point(198, 33);
            btnNhapMaVoucher.Name = "btnNhapMaVoucher";
            btnNhapMaVoucher.Size = new Size(70, 38);
            btnNhapMaVoucher.TabIndex = 5;
            btnNhapMaVoucher.Text = "Nhập";
            btnNhapMaVoucher.UseVisualStyleBackColor = true;
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
            panel6.Size = new Size(184, 35);
            panel6.TabIndex = 2;
            // 
            // boxMaVoucher
            // 
            boxMaVoucher.BorderStyle = BorderStyle.None;
            boxMaVoucher.Location = new Point(3, 5);
            boxMaVoucher.Name = "boxMaVoucher";
            boxMaVoucher.Size = new Size(176, 23);
            boxMaVoucher.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Controls.Add(btnHuyKhachHang);
            panel5.Controls.Add(btnNhapKhachHang);
            panel5.Controls.Add(labelMaKhachHang);
            panel5.Controls.Add(panel4);
            panel5.Location = new Point(1, 91);
            panel5.Name = "panel5";
            panel5.Size = new Size(349, 76);
            panel5.TabIndex = 3;
            // 
            // btnHuyKhachHang
            // 
            btnHuyKhachHang.Location = new Point(274, 33);
            btnHuyKhachHang.Name = "btnHuyKhachHang";
            btnHuyKhachHang.Size = new Size(70, 38);
            btnHuyKhachHang.TabIndex = 6;
            btnHuyKhachHang.Text = "Hủy";
            btnHuyKhachHang.UseVisualStyleBackColor = true;
            // 
            // btnNhapKhachHang
            // 
            btnNhapKhachHang.Location = new Point(198, 33);
            btnNhapKhachHang.Name = "btnNhapKhachHang";
            btnNhapKhachHang.Size = new Size(70, 38);
            btnNhapKhachHang.TabIndex = 5;
            btnNhapKhachHang.Text = "Nhập";
            btnNhapKhachHang.UseVisualStyleBackColor = true;
            btnNhapKhachHang.Click += btnNhapKhachHang_Click;
            // 
            // labelMaKhachHang
            // 
            labelMaKhachHang.AutoSize = true;
            labelMaKhachHang.Location = new Point(8, 9);
            labelMaKhachHang.Name = "labelMaKhachHang";
            labelMaKhachHang.Size = new Size(130, 23);
            labelMaKhachHang.TabIndex = 4;
            labelMaKhachHang.Text = "SĐT khách hàng";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.Window;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(boxMaKhachHang);
            panel4.Location = new Point(8, 35);
            panel4.Name = "panel4";
            panel4.Size = new Size(184, 35);
            panel4.TabIndex = 2;
            // 
            // boxMaKhachHang
            // 
            boxMaKhachHang.BorderStyle = BorderStyle.None;
            boxMaKhachHang.Location = new Point(3, 5);
            boxMaKhachHang.Name = "boxMaKhachHang";
            boxMaKhachHang.Size = new Size(176, 23);
            boxMaKhachHang.TabIndex = 0;
            // 
            // panelThongTinHoaDon
            // 
            panelThongTinHoaDon.Controls.Add(labelNhanVien);
            panelThongTinHoaDon.Controls.Add(lblDateTime);
            panelThongTinHoaDon.Location = new Point(1, 29);
            panelThongTinHoaDon.Name = "panelThongTinHoaDon";
            panelThongTinHoaDon.Size = new Size(349, 63);
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
            // panel9
            // 
            panel9.Controls.Add(btnHuyHoaDon);
            panel9.Controls.Add(btnThanhToan);
            panel9.Location = new Point(821, 572);
            panel9.Name = "panel9";
            panel9.Size = new Size(349, 63);
            panel9.TabIndex = 6;
            // 
            // btnHuyHoaDon
            // 
            btnHuyHoaDon.Location = new Point(179, 3);
            btnHuyHoaDon.Name = "btnHuyHoaDon";
            btnHuyHoaDon.Size = new Size(165, 57);
            btnHuyHoaDon.TabIndex = 1;
            btnHuyHoaDon.Text = "HỦY HÓA ĐƠN";
            btnHuyHoaDon.UseVisualStyleBackColor = true;
            btnHuyHoaDon.Click += btnHuyHoaDon_Click;
            // 
            // btnThanhToan
            // 
            btnThanhToan.Location = new Point(5, 3);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(165, 57);
            btnThanhToan.TabIndex = 0;
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = true;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // groupGioHang
            // 
            groupGioHang.Controls.Add(btnNhapSanPham);
            groupGioHang.Controls.Add(panel2);
            groupGioHang.Controls.Add(btnXoaTimKiem);
            groupGioHang.Controls.Add(tableTimKiem);
            groupGioHang.Controls.Add(panel1);
            groupGioHang.Controls.Add(labelTimKiem);
            groupGioHang.Controls.Add(tableGioHang);
            groupGioHang.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupGioHang.Location = new Point(12, 12);
            groupGioHang.Name = "groupGioHang";
            groupGioHang.Size = new Size(803, 630);
            groupGioHang.TabIndex = 0;
            groupGioHang.TabStop = false;
            groupGioHang.Text = "Tìm kiếm và Giỏ hàng";
            // 
            // btnNhapSanPham
            // 
            btnNhapSanPham.BackColor = Color.FromArgb(192, 255, 192);
            btnNhapSanPham.FlatStyle = FlatStyle.Popup;
            btnNhapSanPham.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNhapSanPham.Location = new Point(565, 575);
            btnNhapSanPham.Name = "btnNhapSanPham";
            btnNhapSanPham.Size = new Size(89, 34);
            btnNhapSanPham.TabIndex = 7;
            btnNhapSanPham.Text = "Nhập";
            btnNhapSanPham.UseVisualStyleBackColor = false;
            btnNhapSanPham.Click += btnNhapSanPham_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(boxNhapSanPham);
            panel2.Location = new Point(120, 575);
            panel2.Name = "panel2";
            panel2.Size = new Size(430, 34);
            panel2.TabIndex = 6;
            // 
            // boxNhapSanPham
            // 
            boxNhapSanPham.BorderStyle = BorderStyle.None;
            boxNhapSanPham.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boxNhapSanPham.Location = new Point(6, 3);
            boxNhapSanPham.Name = "boxNhapSanPham";
            boxNhapSanPham.PlaceholderText = "Mã sản phẩm";
            boxNhapSanPham.Size = new Size(408, 23);
            boxNhapSanPham.TabIndex = 2;
            boxNhapSanPham.KeyDown += boxNhapSanPham_KeyDown;
            // 
            // btnXoaTimKiem
            // 
            btnXoaTimKiem.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnXoaTimKiem.Location = new Point(650, 43);
            btnXoaTimKiem.Name = "btnXoaTimKiem";
            btnXoaTimKiem.Size = new Size(89, 35);
            btnXoaTimKiem.TabIndex = 5;
            btnXoaTimKiem.Text = "Xóa bộ lọc";
            btnXoaTimKiem.UseVisualStyleBackColor = true;
            btnXoaTimKiem.Click += btnXoaTimKiem_Click;
            // 
            // timerDateTime
            // 
            timerDateTime.Interval = 1000;
            timerDateTime.Tick += timerDateTime_Tick;
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
            // panel8
            // 
            panel8.Controls.Add(labelTienTongTien);
            panel8.Controls.Add(labelTongTien);
            panel8.Location = new Point(821, 330);
            panel8.Name = "panel8";
            panel8.Size = new Size(349, 50);
            panel8.TabIndex = 7;
            // 
            // labelTienTongTien
            // 
            labelTienTongTien.BackColor = SystemColors.Control;
            labelTienTongTien.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTienTongTien.ForeColor = SystemColors.ControlText;
            labelTienTongTien.Location = new Point(200, 11);
            labelTienTongTien.Name = "labelTienTongTien";
            labelTienTongTien.RightToLeft = RightToLeft.No;
            labelTienTongTien.Size = new Size(146, 25);
            labelTienTongTien.TabIndex = 7;
            labelTienTongTien.Text = "đ";
            labelTienTongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmBanHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1182, 653);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(groupGioHang);
            Controls.Add(panelAction);
            Controls.Add(groupThanhToan);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "frmBanHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            FormClosing += frmBanHang_FormClosing;
            FormClosed += frmBanHang_FormClosed;
            KeyDown += frmBanHang_KeyDown;
            ((System.ComponentModel.ISupportInitialize)tableTimKiem).EndInit();
            ((System.ComponentModel.ISupportInitialize)medicineBindingSource).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tableGioHang).EndInit();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).EndInit();
            panelAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericSoLuong).EndInit();
            groupThanhToan.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panelTienMat.ResumeLayout(false);
            panelTienMat.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panelThongTinHoaDon.ResumeLayout(false);
            panelThongTinHoaDon.PerformLayout();
            panel9.ResumeLayout(false);
            groupGioHang.ResumeLayout(false);
            groupGioHang.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label labelTimKiem;
        private BindingSource rowDataBindingSource;
        private DataGridView tableGioHang;
        private Panel panelAction;
        private Button btnXoa;
        private DataGridViewTextBoxColumn noDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn btnAction;
        private Panel panel1;
        private TextBox boxTimKiem;
        private DataGridView tableTimKiem;
        private BindingSource medicineBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private DataGridViewButtonColumn btnThemGioHang;
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
        private Label labelMaKhachHang;
        private TextBox boxMaKhachHang;
        private Button btnNhapKhachHang;
        private Panel panel3;
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
    }
}
