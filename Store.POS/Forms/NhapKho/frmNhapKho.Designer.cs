using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmNhapKho
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            dgvPhieuColumn1 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn2 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn3 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn4 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn5 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn6 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn7 = new DataGridViewTextBoxColumn();
            dgvPhieuColumn8 = new DataGridViewTextBoxColumn();
            posLayout = new TableLayoutPanel();
            stockHeader = new TableLayoutPanel();
            posHeading = new Label();
            excelActions = new FlowLayoutPanel();
            btnNhapExcel = new Button();
            btnMauExcel = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox2 = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            dgvPhieu = new DataGridView();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnXoaDong = new Button();
            btnXoaPhieu = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            groupBox6 = new GroupBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            tableLayoutPanel8 = new TableLayoutPanel();
            label9 = new Label();
            tableLayoutPanel10 = new TableLayoutPanel();
            cboNhaCungCap = new ComboBox();
            btnThemNCC = new Button();
            tableLayoutPanel11 = new TableLayoutPanel();
            label12 = new Label();
            txtGhiChu = new TextBox();
            lblNgayNhapGiaTri = new Label();
            groupBox13 = new GroupBox();
            tableLayoutPanel14 = new TableLayoutPanel();
            tableLayoutPanel15 = new TableLayoutPanel();
            tableLayoutPanel16 = new TableLayoutPanel();
            label17 = new Label();
            tableLayoutPanel18 = new TableLayoutPanel();
            cboThuoc = new ComboBox();
            btnTaoThuoc = new Button();
            tableLayoutPanel19 = new TableLayoutPanel();
            tableLayoutPanel20 = new TableLayoutPanel();
            label21 = new Label();
            txtSoLo = new TextBox();
            tableLayoutPanel22 = new TableLayoutPanel();
            tableLayoutPanel23 = new TableLayoutPanel();
            tableLayoutPanel24 = new TableLayoutPanel();
            label25 = new Label();
            dtpHanDung = new DateTimePicker();
            tableLayoutPanel26 = new TableLayoutPanel();
            tableLayoutPanel27 = new TableLayoutPanel();
            label28 = new Label();
            nudSoLuong = new NumericUpDown();
            tableLayoutPanel29 = new TableLayoutPanel();
            tableLayoutPanel30 = new TableLayoutPanel();
            label31 = new Label();
            nudGiaNhap = new NumericUpDown();
            tableLayoutPanel32 = new TableLayoutPanel();
            btnThemThuoc = new Button();
            btnCapNhatDong = new Button();
            tableLayoutPanel33 = new TableLayoutPanel();
            lblTongVonText = new Label();
            lblTongVon = new Label();
            tableLayoutPanel34 = new TableLayoutPanel();
            btnLuu = new Button();
            posLayout.SuspendLayout();
            stockHeader.SuspendLayout();
            excelActions.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((ISupportInitialize)dgvPhieu).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            groupBox6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            groupBox13.SuspendLayout();
            tableLayoutPanel14.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            tableLayoutPanel16.SuspendLayout();
            tableLayoutPanel18.SuspendLayout();
            tableLayoutPanel19.SuspendLayout();
            tableLayoutPanel20.SuspendLayout();
            tableLayoutPanel22.SuspendLayout();
            tableLayoutPanel23.SuspendLayout();
            tableLayoutPanel24.SuspendLayout();
            tableLayoutPanel26.SuspendLayout();
            tableLayoutPanel27.SuspendLayout();
            ((ISupportInitialize)nudSoLuong).BeginInit();
            tableLayoutPanel29.SuspendLayout();
            tableLayoutPanel30.SuspendLayout();
            ((ISupportInitialize)nudGiaNhap).BeginInit();
            tableLayoutPanel32.SuspendLayout();
            tableLayoutPanel33.SuspendLayout();
            tableLayoutPanel34.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPhieuColumn1
            // 
            dgvPhieuColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn1.DataPropertyName = "MaThuoc";
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            dgvPhieuColumn1.FillWeight = 48F;
            dgvPhieuColumn1.HeaderText = "Mã thuốc";
            dgvPhieuColumn1.MinimumWidth = 45;
            dgvPhieuColumn1.Name = "dgvPhieuColumn1";
            dgvPhieuColumn1.ReadOnly = true;
            dgvPhieuColumn1.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn1.Width = 115;
            // 
            // dgvPhieuColumn2
            // 
            dgvPhieuColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn2.DataPropertyName = "TenThuoc";
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPhieuColumn2.FillWeight = 220F;
            dgvPhieuColumn2.HeaderText = "Tên thuốc";
            dgvPhieuColumn2.MinimumWidth = 45;
            dgvPhieuColumn2.Name = "dgvPhieuColumn2";
            dgvPhieuColumn2.ReadOnly = true;
            dgvPhieuColumn2.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn2.Width = 180;
            // 
            // dgvPhieuColumn3
            // 
            dgvPhieuColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn3.DataPropertyName = "DonViTinh";
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            dgvPhieuColumn3.FillWeight = 48F;
            dgvPhieuColumn3.HeaderText = "Đơn vị tính";
            dgvPhieuColumn3.MinimumWidth = 45;
            dgvPhieuColumn3.Name = "dgvPhieuColumn3";
            dgvPhieuColumn3.ReadOnly = true;
            dgvPhieuColumn3.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn3.Width = 126;
            // 
            // dgvPhieuColumn4
            // 
            dgvPhieuColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn4.DataPropertyName = "SoLo";
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            dgvPhieuColumn4.FillWeight = 64F;
            dgvPhieuColumn4.HeaderText = "Số lô";
            dgvPhieuColumn4.MinimumWidth = 45;
            dgvPhieuColumn4.Name = "dgvPhieuColumn4";
            dgvPhieuColumn4.ReadOnly = true;
            dgvPhieuColumn4.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn4.Width = 110;
            // 
            // dgvPhieuColumn5
            // 
            dgvPhieuColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn5.DataPropertyName = "HanDung";
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            dgvPhieuColumn5.FillWeight = 88F;
            dgvPhieuColumn5.HeaderText = "Hạn sử dụng";
            dgvPhieuColumn5.MinimumWidth = 45;
            dgvPhieuColumn5.Name = "dgvPhieuColumn5";
            dgvPhieuColumn5.ReadOnly = true;
            dgvPhieuColumn5.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn5.Width = 140;
            // 
            // dgvPhieuColumn6
            // 
            dgvPhieuColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn6.DataPropertyName = "SoLuong";
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            dgvPhieuColumn6.FillWeight = 88F;
            dgvPhieuColumn6.HeaderText = "Số lượng";
            dgvPhieuColumn6.MinimumWidth = 45;
            dgvPhieuColumn6.Name = "dgvPhieuColumn6";
            dgvPhieuColumn6.ReadOnly = true;
            dgvPhieuColumn6.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn6.Width = 110;
            // 
            // dgvPhieuColumn7
            // 
            dgvPhieuColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn7.DataPropertyName = "GiaNhap";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn7.DefaultCellStyle = dataGridViewCellStyle7;
            dgvPhieuColumn7.FillWeight = 88F;
            dgvPhieuColumn7.HeaderText = "Giá nhập";
            dgvPhieuColumn7.MinimumWidth = 45;
            dgvPhieuColumn7.Name = "dgvPhieuColumn7";
            dgvPhieuColumn7.ReadOnly = true;
            dgvPhieuColumn7.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn7.Width = 111;
            // 
            // dgvPhieuColumn8
            // 
            dgvPhieuColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhieuColumn8.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvPhieuColumn8.DefaultCellStyle = dataGridViewCellStyle8;
            dgvPhieuColumn8.FillWeight = 104F;
            dgvPhieuColumn8.HeaderText = "Thành tiền";
            dgvPhieuColumn8.MinimumWidth = 45;
            dgvPhieuColumn8.Name = "dgvPhieuColumn8";
            dgvPhieuColumn8.ReadOnly = true;
            dgvPhieuColumn8.Resizable = DataGridViewTriState.False;
            dgvPhieuColumn8.Width = 124;
            // 
            // posLayout
            // 
            posLayout.BackColor = Color.FromArgb(242, 246, 248);
            posLayout.ColumnCount = 1;
            posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            posLayout.Controls.Add(stockHeader, 0, 0);
            posLayout.Controls.Add(tableLayoutPanel1, 0, 1);
            posLayout.Dock = DockStyle.Fill;
            posLayout.Font = new Font("Segoe UI", 10.2F);
            posLayout.ForeColor = Color.FromArgb(15, 23, 42);
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
            // stockHeader
            // 
            stockHeader.ColumnCount = 2;
            stockHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            stockHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            stockHeader.Controls.Add(posHeading, 0, 0);
            stockHeader.Controls.Add(excelActions, 1, 0);
            stockHeader.Dock = DockStyle.Fill;
            stockHeader.Location = new Point(12, 12);
            stockHeader.Margin = new Padding(0);
            stockHeader.Name = "stockHeader";
            stockHeader.RowCount = 1;
            stockHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            stockHeader.Size = new Size(1158, 48);
            stockHeader.TabIndex = 0;
            // 
            // posHeading
            // 
            posHeading.BackColor = Color.FromArgb(242, 246, 248);
            posHeading.Dock = DockStyle.Fill;
            posHeading.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            posHeading.ForeColor = Color.FromArgb(15, 23, 42);
            posHeading.Location = new Point(3, 0);
            posHeading.Name = "posHeading";
            posHeading.Size = new Size(902, 48);
            posHeading.TabIndex = 0;
            posHeading.Text = "Nhập kho";
            posHeading.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // excelActions
            // 
            excelActions.Controls.Add(btnNhapExcel);
            excelActions.Controls.Add(btnMauExcel);
            excelActions.Dock = DockStyle.Fill;
            excelActions.Location = new Point(908, 0);
            excelActions.Margin = new Padding(0);
            excelActions.Name = "excelActions";
            excelActions.Size = new Size(250, 48);
            excelActions.TabIndex = 1;
            excelActions.WrapContents = false;
            // 
            // btnNhapExcel
            // 
            btnNhapExcel.BackColor = Color.FromArgb(22, 101, 52);
            btnNhapExcel.Cursor = Cursors.Hand;
            btnNhapExcel.FlatAppearance.BorderSize = 0;
            btnNhapExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
            btnNhapExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnNhapExcel.FlatStyle = FlatStyle.Flat;
            btnNhapExcel.ForeColor = Color.White;
            btnNhapExcel.Location = new Point(3, 3);
            btnNhapExcel.Name = "btnNhapExcel";
            btnNhapExcel.Size = new Size(120, 38);
            btnNhapExcel.TabIndex = 0;
            btnNhapExcel.Text = "Nhập Excel";
            btnNhapExcel.UseVisualStyleBackColor = false;
            btnNhapExcel.Click += btnNhapExcel_Click;
            // 
            // btnMauExcel
            // 
            btnMauExcel.BackColor = Color.FromArgb(15, 118, 110);
            btnMauExcel.Cursor = Cursors.Hand;
            btnMauExcel.FlatAppearance.BorderSize = 0;
            btnMauExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnMauExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnMauExcel.FlatStyle = FlatStyle.Flat;
            btnMauExcel.ForeColor = Color.White;
            btnMauExcel.Location = new Point(129, 3);
            btnMauExcel.Name = "btnMauExcel";
            btnMauExcel.Size = new Size(120, 38);
            btnMauExcel.TabIndex = 1;
            btnMauExcel.Text = "File mẫu";
            btnMauExcel.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(242, 246, 248);
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 370F));
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel1.ForeColor = Color.FromArgb(15, 23, 42);
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
            groupBox2.BackColor = Color.White;
            groupBox2.Controls.Add(tableLayoutPanel3);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            groupBox2.ForeColor = Color.FromArgb(15, 23, 42);
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(0, 0, 12, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(12, 2, 12, 12);
            groupBox2.Size = new Size(776, 581);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chi tiết phiếu nhập";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.White;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(dgvPhieu, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel3.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel3.Location = new Point(12, 25);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel3.Size = new Size(752, 544);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // dgvPhieu
            // 
            dgvPhieu.AllowUserToAddRows = false;
            dgvPhieu.AllowUserToDeleteRows = false;
            dgvPhieu.AllowUserToResizeColumns = false;
            dgvPhieu.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(242, 246, 248);
            dgvPhieu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvPhieu.BackgroundColor = Color.White;
            dgvPhieu.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle10.ForeColor = Color.White;
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle10.SelectionForeColor = Color.White;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dgvPhieu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dgvPhieu.ColumnHeadersHeight = 43;
            dgvPhieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPhieu.Columns.AddRange(new DataGridViewColumn[] { dgvPhieuColumn2, dgvPhieuColumn6, dgvPhieuColumn7, dgvPhieuColumn8, dgvPhieuColumn4, dgvPhieuColumn5, dgvPhieuColumn3, dgvPhieuColumn1 });
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = Color.White;
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle11.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle11.Padding = new Padding(4);
            dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(204, 251, 241);
            dataGridViewCellStyle11.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dgvPhieu.DefaultCellStyle = dataGridViewCellStyle11;
            dgvPhieu.Dock = DockStyle.Fill;
            dgvPhieu.EnableHeadersVisualStyles = false;
            dgvPhieu.Font = new Font("Segoe UI", 10.2F);
            dgvPhieu.GridColor = Color.FromArgb(226, 232, 240);
            dgvPhieu.Location = new Point(3, 4);
            dgvPhieu.Margin = new Padding(3, 4, 3, 4);
            dgvPhieu.MultiSelect = false;
            dgvPhieu.Name = "dgvPhieu";
            dgvPhieu.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = Color.FromArgb(242, 246, 248);
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dgvPhieu.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dgvPhieu.RowHeadersVisible = false;
            dgvPhieu.RowHeadersWidth = 51;
            dgvPhieu.RowTemplate.Height = 34;
            dgvPhieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieu.Size = new Size(746, 488);
            dgvPhieu.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.BackColor = Color.White;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(btnXoaDong, 0, 0);
            tableLayoutPanel4.Controls.Add(btnXoaPhieu, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel4.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel4.Location = new Point(0, 496);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel4.Size = new Size(752, 48);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // btnXoaDong
            // 
            btnXoaDong.BackColor = Color.FromArgb(225, 29, 72);
            btnXoaDong.Cursor = Cursors.Hand;
            btnXoaDong.Dock = DockStyle.Fill;
            btnXoaDong.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnXoaDong.FlatAppearance.BorderSize = 0;
            btnXoaDong.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
            btnXoaDong.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btnXoaDong.FlatStyle = FlatStyle.Flat;
            btnXoaDong.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnXoaDong.ForeColor = Color.White;
            btnXoaDong.Location = new Point(0, 4);
            btnXoaDong.Margin = new Padding(0, 4, 4, 0);
            btnXoaDong.Name = "btnXoaDong";
            btnXoaDong.Size = new Size(372, 44);
            btnXoaDong.TabIndex = 1;
            btnXoaDong.Text = "Xóa dòng";
            btnXoaDong.UseVisualStyleBackColor = false;
            btnXoaDong.Click += btnXoaDong_Click;
            // 
            // btnXoaPhieu
            // 
            btnXoaPhieu.BackColor = Color.FromArgb(225, 29, 72);
            btnXoaPhieu.Cursor = Cursors.Hand;
            btnXoaPhieu.Dock = DockStyle.Fill;
            btnXoaPhieu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnXoaPhieu.FlatAppearance.BorderSize = 0;
            btnXoaPhieu.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
            btnXoaPhieu.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
            btnXoaPhieu.FlatStyle = FlatStyle.Flat;
            btnXoaPhieu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnXoaPhieu.ForeColor = Color.White;
            btnXoaPhieu.Location = new Point(380, 4);
            btnXoaPhieu.Margin = new Padding(4, 4, 0, 0);
            btnXoaPhieu.Name = "btnXoaPhieu";
            btnXoaPhieu.Size = new Size(372, 44);
            btnXoaPhieu.TabIndex = 2;
            btnXoaPhieu.Text = "Bỏ phiếu nháp";
            btnXoaPhieu.UseVisualStyleBackColor = false;
            btnXoaPhieu.Click += btnXoaPhieu_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.FromArgb(242, 246, 248);
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(groupBox6, 0, 0);
            tableLayoutPanel5.Controls.Add(groupBox13, 0, 1);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel33, 0, 2);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel5.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel5.Location = new Point(788, 0);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 306F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(370, 581);
            tableLayoutPanel5.TabIndex = 1;
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.White;
            groupBox6.Controls.Add(tableLayoutPanel7);
            groupBox6.Dock = DockStyle.Fill;
            groupBox6.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            groupBox6.ForeColor = Color.FromArgb(15, 23, 42);
            groupBox6.Location = new Point(0, 0);
            groupBox6.Margin = new Padding(0);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(12, 2, 12, 12);
            groupBox6.Size = new Size(370, 150);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "Thông tin phiếu nhập";
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.AutoSize = true;
            tableLayoutPanel7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel7.BackColor = Color.White;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(tableLayoutPanel8);
            tableLayoutPanel7.Controls.Add(tableLayoutPanel11);
            tableLayoutPanel7.Controls.Add(lblNgayNhapGiaTri);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel7.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel7.Location = new Point(12, 25);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel7.Size = new Size(346, 113);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.BackColor = Color.White;
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.Controls.Add(label9, 0, 0);
            tableLayoutPanel8.Controls.Add(tableLayoutPanel10, 0, 1);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel8.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel8.Location = new Point(0, 0);
            tableLayoutPanel8.Margin = new Padding(0);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 2;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel8.Size = new Size(346, 56);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // label9
            // 
            label9.BackColor = Color.White;
            label9.Dock = DockStyle.Fill;
            label9.Font = new Font("Segoe UI", 10.2F);
            label9.ForeColor = Color.FromArgb(15, 23, 42);
            label9.Location = new Point(3, 0);
            label9.Name = "label9";
            label9.Size = new Size(340, 22);
            label9.TabIndex = 0;
            label9.Text = "Nhà cung cấp";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.BackColor = Color.White;
            tableLayoutPanel10.ColumnCount = 2;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            tableLayoutPanel10.Controls.Add(cboNhaCungCap, 0, 0);
            tableLayoutPanel10.Controls.Add(btnThemNCC, 1, 0);
            tableLayoutPanel10.Dock = DockStyle.Top;
            tableLayoutPanel10.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel10.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel10.Location = new Point(0, 22);
            tableLayoutPanel10.Margin = new Padding(0);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Size = new Size(346, 32);
            tableLayoutPanel10.TabIndex = 1;
            // 
            // cboNhaCungCap
            // 
            cboNhaCungCap.BackColor = Color.White;
            cboNhaCungCap.Dock = DockStyle.Fill;
            cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhaCungCap.DropDownWidth = 302;
            cboNhaCungCap.Font = new Font("Segoe UI", 10.5F);
            cboNhaCungCap.ForeColor = Color.FromArgb(15, 23, 42);
            cboNhaCungCap.Location = new Point(0, 0);
            cboNhaCungCap.Margin = new Padding(0, 0, 6, 0);
            cboNhaCungCap.Name = "cboNhaCungCap";
            cboNhaCungCap.Size = new Size(302, 31);
            cboNhaCungCap.TabIndex = 1;
            // 
            // btnThemNCC
            // 
            btnThemNCC.AccessibleName = "Thêm nhà cung cấp";
            btnThemNCC.BackColor = Color.FromArgb(22, 101, 52);
            btnThemNCC.Cursor = Cursors.Hand;
            btnThemNCC.Dock = DockStyle.Fill;
            btnThemNCC.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnThemNCC.FlatAppearance.BorderSize = 0;
            btnThemNCC.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
            btnThemNCC.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnThemNCC.FlatStyle = FlatStyle.Flat;
            btnThemNCC.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnThemNCC.ForeColor = Color.White;
            btnThemNCC.Location = new Point(308, 0);
            btnThemNCC.Margin = new Padding(0);
            btnThemNCC.Name = "btnThemNCC";
            btnThemNCC.Padding = new Padding(0, 1, 0, 0);
            btnThemNCC.Size = new Size(38, 32);
            btnThemNCC.TabIndex = 2;
            btnThemNCC.Text = "+";
            btnThemNCC.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.BackColor = Color.White;
            tableLayoutPanel11.ColumnCount = 1;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Controls.Add(label12, 0, 0);
            tableLayoutPanel11.Controls.Add(txtGhiChu, 0, 1);
            tableLayoutPanel11.Dock = DockStyle.Fill;
            tableLayoutPanel11.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel11.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel11.Location = new Point(0, 56);
            tableLayoutPanel11.Margin = new Padding(0);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 2;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel11.Size = new Size(346, 56);
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
            label12.Size = new Size(340, 22);
            label12.TabIndex = 0;
            label12.Text = "Ghi chú phiếu nhập";
            label12.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGhiChu
            // 
            txtGhiChu.BackColor = Color.White;
            txtGhiChu.BorderStyle = BorderStyle.FixedSingle;
            txtGhiChu.Cursor = Cursors.IBeam;
            txtGhiChu.Dock = DockStyle.Top;
            txtGhiChu.Font = new Font("Segoe UI", 10.5F);
            txtGhiChu.ForeColor = Color.FromArgb(15, 23, 42);
            txtGhiChu.Location = new Point(0, 23);
            txtGhiChu.Margin = new Padding(0, 1, 0, 5);
            txtGhiChu.MaxLength = 250;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(346, 28);
            txtGhiChu.TabIndex = 5;
            // 
            // lblNgayNhapGiaTri
            // 
            lblNgayNhapGiaTri.BackColor = Color.White;
            lblNgayNhapGiaTri.Font = new Font("Segoe UI", 10.2F);
            lblNgayNhapGiaTri.ForeColor = Color.FromArgb(15, 23, 42);
            lblNgayNhapGiaTri.Location = new Point(3, 112);
            lblNgayNhapGiaTri.Name = "lblNgayNhapGiaTri";
            lblNgayNhapGiaTri.Size = new Size(103, 20);
            lblNgayNhapGiaTri.TabIndex = 3;
            lblNgayNhapGiaTri.Text = "dd/MM/yyyy";
            lblNgayNhapGiaTri.TextAlign = ContentAlignment.MiddleLeft;
            lblNgayNhapGiaTri.Visible = false;
            // 
            // groupBox13
            // 
            groupBox13.BackColor = Color.White;
            groupBox13.Controls.Add(tableLayoutPanel14);
            groupBox13.Dock = DockStyle.Fill;
            groupBox13.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            groupBox13.ForeColor = Color.FromArgb(15, 23, 42);
            groupBox13.Location = new Point(0, 150);
            groupBox13.Margin = new Padding(0);
            groupBox13.Name = "groupBox13";
            groupBox13.Padding = new Padding(12, 2, 12, 12);
            groupBox13.Size = new Size(370, 306);
            groupBox13.TabIndex = 1;
            groupBox13.TabStop = false;
            groupBox13.Text = "Thêm thuốc vào phiếu";
            // 
            // tableLayoutPanel14
            // 
            tableLayoutPanel14.BackColor = Color.White;
            tableLayoutPanel14.ColumnCount = 1;
            tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel14.Controls.Add(tableLayoutPanel15, 0, 0);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel19, 0, 1);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel22, 0, 2);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel29, 0, 3);
            tableLayoutPanel14.Controls.Add(tableLayoutPanel32, 0, 4);
            tableLayoutPanel14.Dock = DockStyle.Fill;
            tableLayoutPanel14.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel14.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel14.Location = new Point(12, 25);
            tableLayoutPanel14.Margin = new Padding(0);
            tableLayoutPanel14.Name = "tableLayoutPanel14";
            tableLayoutPanel14.RowCount = 5;
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel14.Size = new Size(346, 269);
            tableLayoutPanel14.TabIndex = 0;
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.AutoSize = true;
            tableLayoutPanel15.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel15.BackColor = Color.White;
            tableLayoutPanel15.ColumnCount = 1;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Controls.Add(tableLayoutPanel16);
            tableLayoutPanel15.Dock = DockStyle.Top;
            tableLayoutPanel15.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel15.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel15.Location = new Point(0, 0);
            tableLayoutPanel15.Margin = new Padding(0);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 1;
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel15.Size = new Size(346, 56);
            tableLayoutPanel15.TabIndex = 0;
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
            tableLayoutPanel16.Location = new Point(0, 0);
            tableLayoutPanel16.Margin = new Padding(0);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 2;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel16.Size = new Size(346, 56);
            tableLayoutPanel16.TabIndex = 0;
            // 
            // label17
            // 
            label17.BackColor = Color.White;
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("Segoe UI", 10.2F);
            label17.ForeColor = Color.FromArgb(15, 23, 42);
            label17.Location = new Point(3, 0);
            label17.Name = "label17";
            label17.Size = new Size(340, 22);
            label17.TabIndex = 0;
            label17.Text = "Thuốc";
            label17.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.BackColor = Color.White;
            tableLayoutPanel18.ColumnCount = 2;
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            tableLayoutPanel18.Controls.Add(cboThuoc, 0, 0);
            tableLayoutPanel18.Controls.Add(btnTaoThuoc, 1, 0);
            tableLayoutPanel18.Dock = DockStyle.Top;
            tableLayoutPanel18.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel18.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel18.Location = new Point(0, 22);
            tableLayoutPanel18.Margin = new Padding(0);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.RowCount = 1;
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.Size = new Size(346, 32);
            tableLayoutPanel18.TabIndex = 1;
            // 
            // cboThuoc
            // 
            cboThuoc.BackColor = Color.White;
            cboThuoc.Dock = DockStyle.Fill;
            cboThuoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuoc.DropDownWidth = 302;
            cboThuoc.Enabled = false;
            cboThuoc.Font = new Font("Segoe UI", 10.5F);
            cboThuoc.ForeColor = Color.FromArgb(15, 23, 42);
            cboThuoc.Location = new Point(0, 0);
            cboThuoc.Margin = new Padding(0, 0, 6, 0);
            cboThuoc.Name = "cboThuoc";
            cboThuoc.Size = new Size(302, 31);
            cboThuoc.TabIndex = 1;
            // 
            // btnTaoThuoc
            // 
            btnTaoThuoc.AccessibleName = "Thêm thuốc mới";
            btnTaoThuoc.BackColor = Color.FromArgb(22, 101, 52);
            btnTaoThuoc.Cursor = Cursors.Hand;
            btnTaoThuoc.Dock = DockStyle.Fill;
            btnTaoThuoc.Enabled = false;
            btnTaoThuoc.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnTaoThuoc.FlatAppearance.BorderSize = 0;
            btnTaoThuoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
            btnTaoThuoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnTaoThuoc.FlatStyle = FlatStyle.Flat;
            btnTaoThuoc.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnTaoThuoc.ForeColor = Color.White;
            btnTaoThuoc.Location = new Point(308, 0);
            btnTaoThuoc.Margin = new Padding(0);
            btnTaoThuoc.Name = "btnTaoThuoc";
            btnTaoThuoc.Padding = new Padding(0, 1, 0, 0);
            btnTaoThuoc.Size = new Size(38, 32);
            btnTaoThuoc.TabIndex = 2;
            btnTaoThuoc.Text = "+";
            btnTaoThuoc.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel19
            // 
            tableLayoutPanel19.AutoSize = true;
            tableLayoutPanel19.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel19.BackColor = Color.White;
            tableLayoutPanel19.ColumnCount = 1;
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel19.Controls.Add(tableLayoutPanel20);
            tableLayoutPanel19.Dock = DockStyle.Top;
            tableLayoutPanel19.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel19.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel19.Location = new Point(0, 56);
            tableLayoutPanel19.Margin = new Padding(0);
            tableLayoutPanel19.Name = "tableLayoutPanel19";
            tableLayoutPanel19.RowCount = 1;
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel19.Size = new Size(346, 56);
            tableLayoutPanel19.TabIndex = 1;
            // 
            // tableLayoutPanel20
            // 
            tableLayoutPanel20.BackColor = Color.White;
            tableLayoutPanel20.ColumnCount = 1;
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel20.Controls.Add(label21, 0, 0);
            tableLayoutPanel20.Controls.Add(txtSoLo, 0, 1);
            tableLayoutPanel20.Dock = DockStyle.Fill;
            tableLayoutPanel20.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel20.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel20.Location = new Point(0, 0);
            tableLayoutPanel20.Margin = new Padding(0);
            tableLayoutPanel20.Name = "tableLayoutPanel20";
            tableLayoutPanel20.RowCount = 2;
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel20.Size = new Size(346, 56);
            tableLayoutPanel20.TabIndex = 0;
            // 
            // label21
            // 
            label21.BackColor = Color.White;
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("Segoe UI", 10.2F);
            label21.ForeColor = Color.FromArgb(15, 23, 42);
            label21.Location = new Point(3, 0);
            label21.Name = "label21";
            label21.Size = new Size(340, 22);
            label21.TabIndex = 0;
            label21.Text = "Số lô";
            label21.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSoLo
            // 
            txtSoLo.BackColor = Color.White;
            txtSoLo.BorderStyle = BorderStyle.FixedSingle;
            txtSoLo.Cursor = Cursors.IBeam;
            txtSoLo.Dock = DockStyle.Top;
            txtSoLo.Enabled = false;
            txtSoLo.Font = new Font("Segoe UI", 10.5F);
            txtSoLo.ForeColor = Color.FromArgb(15, 23, 42);
            txtSoLo.Location = new Point(0, 23);
            txtSoLo.Margin = new Padding(0, 1, 0, 5);
            txtSoLo.MaxLength = 50;
            txtSoLo.Name = "txtSoLo";
            txtSoLo.Size = new Size(346, 28);
            txtSoLo.TabIndex = 3;
            // 
            // tableLayoutPanel22
            // 
            tableLayoutPanel22.BackColor = Color.White;
            tableLayoutPanel22.ColumnCount = 2;
            tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel22.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 122F));
            tableLayoutPanel22.Controls.Add(tableLayoutPanel23, 0, 0);
            tableLayoutPanel22.Controls.Add(tableLayoutPanel26, 1, 0);
            tableLayoutPanel22.Dock = DockStyle.Fill;
            tableLayoutPanel22.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel22.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel22.Location = new Point(0, 112);
            tableLayoutPanel22.Margin = new Padding(0);
            tableLayoutPanel22.Name = "tableLayoutPanel22";
            tableLayoutPanel22.RowCount = 1;
            tableLayoutPanel22.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel22.Size = new Size(346, 56);
            tableLayoutPanel22.TabIndex = 2;
            // 
            // tableLayoutPanel23
            // 
            tableLayoutPanel23.AutoSize = true;
            tableLayoutPanel23.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel23.BackColor = Color.White;
            tableLayoutPanel23.ColumnCount = 1;
            tableLayoutPanel23.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel23.Controls.Add(tableLayoutPanel24);
            tableLayoutPanel23.Dock = DockStyle.Fill;
            tableLayoutPanel23.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel23.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel23.Location = new Point(0, 0);
            tableLayoutPanel23.Margin = new Padding(0, 0, 12, 0);
            tableLayoutPanel23.Name = "tableLayoutPanel23";
            tableLayoutPanel23.RowCount = 1;
            tableLayoutPanel23.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel23.Size = new Size(212, 56);
            tableLayoutPanel23.TabIndex = 0;
            // 
            // tableLayoutPanel24
            // 
            tableLayoutPanel24.BackColor = Color.White;
            tableLayoutPanel24.ColumnCount = 1;
            tableLayoutPanel24.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel24.Controls.Add(label25, 0, 0);
            tableLayoutPanel24.Controls.Add(dtpHanDung, 0, 1);
            tableLayoutPanel24.Dock = DockStyle.Fill;
            tableLayoutPanel24.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel24.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel24.Location = new Point(0, 0);
            tableLayoutPanel24.Margin = new Padding(0);
            tableLayoutPanel24.Name = "tableLayoutPanel24";
            tableLayoutPanel24.RowCount = 2;
            tableLayoutPanel24.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel24.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel24.Size = new Size(212, 56);
            tableLayoutPanel24.TabIndex = 0;
            // 
            // label25
            // 
            label25.BackColor = Color.White;
            label25.Dock = DockStyle.Fill;
            label25.Font = new Font("Segoe UI", 10.2F);
            label25.ForeColor = Color.FromArgb(15, 23, 42);
            label25.Location = new Point(3, 0);
            label25.Name = "label25";
            label25.Size = new Size(206, 22);
            label25.TabIndex = 0;
            label25.Text = "Hạn dùng";
            label25.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpHanDung
            // 
            dtpHanDung.BackColor = SystemColors.Window;
            dtpHanDung.CustomFormat = "dd/MM/yyyy";
            dtpHanDung.Dock = DockStyle.Top;
            dtpHanDung.Font = new Font("Segoe UI", 10.5F);
            dtpHanDung.ForeColor = Color.FromArgb(15, 23, 42);
            dtpHanDung.Format = DateTimePickerFormat.Custom;
            dtpHanDung.Location = new Point(0, 23);
            dtpHanDung.Margin = new Padding(0, 1, 0, 5);
            dtpHanDung.Name = "dtpHanDung";
            dtpHanDung.Size = new Size(212, 31);
            dtpHanDung.TabIndex = 5;
            dtpHanDung.Value = new DateTime(2026, 10, 22, 0, 0, 0, 0);
            // 
            // tableLayoutPanel26
            // 
            tableLayoutPanel26.AutoSize = true;
            tableLayoutPanel26.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel26.BackColor = Color.White;
            tableLayoutPanel26.ColumnCount = 1;
            tableLayoutPanel26.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel26.Controls.Add(tableLayoutPanel27);
            tableLayoutPanel26.Dock = DockStyle.Fill;
            tableLayoutPanel26.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel26.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel26.Location = new Point(224, 0);
            tableLayoutPanel26.Margin = new Padding(0);
            tableLayoutPanel26.Name = "tableLayoutPanel26";
            tableLayoutPanel26.RowCount = 1;
            tableLayoutPanel26.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel26.Size = new Size(122, 56);
            tableLayoutPanel26.TabIndex = 1;
            // 
            // tableLayoutPanel27
            // 
            tableLayoutPanel27.BackColor = Color.White;
            tableLayoutPanel27.ColumnCount = 1;
            tableLayoutPanel27.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel27.Controls.Add(label28, 0, 0);
            tableLayoutPanel27.Controls.Add(nudSoLuong, 0, 1);
            tableLayoutPanel27.Dock = DockStyle.Fill;
            tableLayoutPanel27.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel27.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel27.Location = new Point(0, 0);
            tableLayoutPanel27.Margin = new Padding(0);
            tableLayoutPanel27.Name = "tableLayoutPanel27";
            tableLayoutPanel27.RowCount = 2;
            tableLayoutPanel27.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel27.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel27.Size = new Size(122, 56);
            tableLayoutPanel27.TabIndex = 0;
            // 
            // label28
            // 
            label28.BackColor = Color.White;
            label28.Dock = DockStyle.Fill;
            label28.Font = new Font("Segoe UI", 10.2F);
            label28.ForeColor = Color.FromArgb(15, 23, 42);
            label28.Location = new Point(3, 0);
            label28.Name = "label28";
            label28.Size = new Size(116, 22);
            label28.TabIndex = 0;
            label28.Text = "Số lượng";
            label28.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudSoLuong
            // 
            nudSoLuong.BackColor = Color.White;
            nudSoLuong.BorderStyle = BorderStyle.FixedSingle;
            nudSoLuong.Dock = DockStyle.Top;
            nudSoLuong.Font = new Font("Segoe UI", 10.5F);
            nudSoLuong.ForeColor = Color.FromArgb(15, 23, 42);
            nudSoLuong.Location = new Point(0, 23);
            nudSoLuong.Margin = new Padding(0, 1, 0, 5);
            nudSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(122, 31);
            nudSoLuong.TabIndex = 7;
            nudSoLuong.TextAlign = HorizontalAlignment.Right;
            nudSoLuong.ThousandsSeparator = true;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tableLayoutPanel29
            // 
            tableLayoutPanel29.AutoSize = true;
            tableLayoutPanel29.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel29.BackColor = Color.White;
            tableLayoutPanel29.ColumnCount = 1;
            tableLayoutPanel29.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel29.Controls.Add(tableLayoutPanel30);
            tableLayoutPanel29.Dock = DockStyle.Top;
            tableLayoutPanel29.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel29.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel29.Location = new Point(0, 168);
            tableLayoutPanel29.Margin = new Padding(0);
            tableLayoutPanel29.Name = "tableLayoutPanel29";
            tableLayoutPanel29.RowCount = 1;
            tableLayoutPanel29.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel29.Size = new Size(346, 56);
            tableLayoutPanel29.TabIndex = 3;
            // 
            // tableLayoutPanel30
            // 
            tableLayoutPanel30.BackColor = Color.White;
            tableLayoutPanel30.ColumnCount = 1;
            tableLayoutPanel30.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel30.Controls.Add(label31, 0, 0);
            tableLayoutPanel30.Controls.Add(nudGiaNhap, 0, 1);
            tableLayoutPanel30.Dock = DockStyle.Fill;
            tableLayoutPanel30.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel30.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel30.Location = new Point(0, 0);
            tableLayoutPanel30.Margin = new Padding(0);
            tableLayoutPanel30.Name = "tableLayoutPanel30";
            tableLayoutPanel30.RowCount = 2;
            tableLayoutPanel30.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel30.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel30.Size = new Size(346, 56);
            tableLayoutPanel30.TabIndex = 0;
            // 
            // label31
            // 
            label31.BackColor = Color.White;
            label31.Dock = DockStyle.Fill;
            label31.Font = new Font("Segoe UI", 10.2F);
            label31.ForeColor = Color.FromArgb(15, 23, 42);
            label31.Location = new Point(3, 0);
            label31.Name = "label31";
            label31.Size = new Size(340, 22);
            label31.TabIndex = 0;
            label31.Text = "Giá nhập / đơn vị";
            label31.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudGiaNhap
            // 
            nudGiaNhap.BackColor = Color.White;
            nudGiaNhap.BorderStyle = BorderStyle.FixedSingle;
            nudGiaNhap.Dock = DockStyle.Top;
            nudGiaNhap.Font = new Font("Segoe UI", 10.5F);
            nudGiaNhap.ForeColor = Color.FromArgb(15, 23, 42);
            nudGiaNhap.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudGiaNhap.Location = new Point(0, 23);
            nudGiaNhap.Margin = new Padding(0, 1, 0, 5);
            nudGiaNhap.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudGiaNhap.Name = "nudGiaNhap";
            nudGiaNhap.Size = new Size(346, 31);
            nudGiaNhap.TabIndex = 9;
            nudGiaNhap.TextAlign = HorizontalAlignment.Right;
            nudGiaNhap.ThousandsSeparator = true;
            // 
            // tableLayoutPanel32
            // 
            tableLayoutPanel32.AutoSize = true;
            tableLayoutPanel32.BackColor = Color.White;
            tableLayoutPanel32.ColumnCount = 2;
            tableLayoutPanel32.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel32.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel32.Controls.Add(btnThemThuoc, 0, 0);
            tableLayoutPanel32.Controls.Add(btnCapNhatDong, 1, 0);
            tableLayoutPanel32.Dock = DockStyle.Fill;
            tableLayoutPanel32.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel32.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel32.Location = new Point(0, 224);
            tableLayoutPanel32.Margin = new Padding(0);
            tableLayoutPanel32.Name = "tableLayoutPanel32";
            tableLayoutPanel32.RowCount = 1;
            tableLayoutPanel32.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel32.Size = new Size(346, 45);
            tableLayoutPanel32.TabIndex = 4;
            // 
            // btnThemThuoc
            // 
            btnThemThuoc.BackColor = Color.FromArgb(204, 251, 241);
            btnThemThuoc.Cursor = Cursors.Hand;
            btnThemThuoc.Dock = DockStyle.Fill;
            btnThemThuoc.Enabled = false;
            btnThemThuoc.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnThemThuoc.FlatAppearance.BorderSize = 0;
            btnThemThuoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(94, 234, 212);
            btnThemThuoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 246, 228);
            btnThemThuoc.FlatStyle = FlatStyle.Flat;
            btnThemThuoc.Font = new Font("Segoe UI Semibold", 10.2F);
            btnThemThuoc.ForeColor = Color.FromArgb(19, 78, 74);
            btnThemThuoc.Location = new Point(0, 4);
            btnThemThuoc.Margin = new Padding(0, 4, 4, 0);
            btnThemThuoc.Name = "btnThemThuoc";
            btnThemThuoc.Size = new Size(169, 41);
            btnThemThuoc.TabIndex = 10;
            btnThemThuoc.Text = "Thêm vào phiếu";
            btnThemThuoc.UseVisualStyleBackColor = false;
            btnThemThuoc.Click += btnThemThuoc_Click;
            // 
            // btnCapNhatDong
            // 
            btnCapNhatDong.BackColor = Color.FromArgb(15, 118, 110);
            btnCapNhatDong.Dock = DockStyle.Fill;
            btnCapNhatDong.Enabled = false;
            btnCapNhatDong.FlatAppearance.BorderSize = 0;
            btnCapNhatDong.FlatStyle = FlatStyle.Flat;
            btnCapNhatDong.Font = new Font("Segoe UI Semibold", 10.2F);
            btnCapNhatDong.ForeColor = Color.White;
            btnCapNhatDong.Location = new Point(177, 4);
            btnCapNhatDong.Margin = new Padding(4, 4, 0, 0);
            btnCapNhatDong.Name = "btnCapNhatDong";
            btnCapNhatDong.Size = new Size(169, 41);
            btnCapNhatDong.TabIndex = 11;
            btnCapNhatDong.Text = "Cập nhật dòng";
            btnCapNhatDong.UseVisualStyleBackColor = false;
            btnCapNhatDong.Click += btnCapNhatDong_Click;
            // 
            // tableLayoutPanel33
            // 
            tableLayoutPanel33.BackColor = Color.FromArgb(242, 246, 248);
            tableLayoutPanel33.ColumnCount = 1;
            tableLayoutPanel33.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel33.Controls.Add(lblTongVonText, 0, 0);
            tableLayoutPanel33.Controls.Add(lblTongVon, 0, 1);
            tableLayoutPanel33.Controls.Add(tableLayoutPanel34, 0, 2);
            tableLayoutPanel33.Dock = DockStyle.Fill;
            tableLayoutPanel33.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel33.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel33.Location = new Point(0, 456);
            tableLayoutPanel33.Margin = new Padding(0);
            tableLayoutPanel33.Name = "tableLayoutPanel33";
            tableLayoutPanel33.RowCount = 3;
            tableLayoutPanel33.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel33.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel33.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel33.Size = new Size(370, 125);
            tableLayoutPanel33.TabIndex = 2;
            // 
            // lblTongVonText
            // 
            lblTongVonText.BackColor = Color.FromArgb(242, 246, 248);
            lblTongVonText.Dock = DockStyle.Fill;
            lblTongVonText.Font = new Font("Segoe UI", 10.2F);
            lblTongVonText.ForeColor = Color.FromArgb(15, 23, 42);
            lblTongVonText.Location = new Point(3, 0);
            lblTongVonText.Name = "lblTongVonText";
            lblTongVonText.Size = new Size(364, 26);
            lblTongVonText.TabIndex = 3;
            lblTongVonText.Text = "Tổng tiền vốn:";
            lblTongVonText.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTongVon
            // 
            lblTongVon.BackColor = Color.FromArgb(242, 246, 248);
            lblTongVon.Dock = DockStyle.Fill;
            lblTongVon.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            lblTongVon.ForeColor = Color.FromArgb(15, 23, 42);
            lblTongVon.Location = new Point(3, 26);
            lblTongVon.Name = "lblTongVon";
            lblTongVon.Size = new Size(364, 34);
            lblTongVon.TabIndex = 4;
            lblTongVon.Text = "0 đ";
            lblTongVon.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel34
            // 
            tableLayoutPanel34.AutoSize = true;
            tableLayoutPanel34.BackColor = Color.FromArgb(242, 246, 248);
            tableLayoutPanel34.ColumnCount = 2;
            tableLayoutPanel34.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel34.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel34.Controls.Add(btnLuu, 0, 0);
            tableLayoutPanel34.Dock = DockStyle.Fill;
            tableLayoutPanel34.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel34.ForeColor = Color.FromArgb(15, 23, 42);
            tableLayoutPanel34.Location = new Point(0, 60);
            tableLayoutPanel34.Margin = new Padding(0);
            tableLayoutPanel34.Name = "tableLayoutPanel34";
            tableLayoutPanel34.RowCount = 1;
            tableLayoutPanel34.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel34.Size = new Size(370, 65);
            tableLayoutPanel34.TabIndex = 5;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(204, 251, 241);
            tableLayoutPanel34.SetColumnSpan(btnLuu, 2);
            btnLuu.Cursor = Cursors.Hand;
            btnLuu.Dock = DockStyle.Top;
            btnLuu.Enabled = false;
            btnLuu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatAppearance.MouseDownBackColor = Color.FromArgb(94, 234, 212);
            btnLuu.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 246, 228);
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI Semibold", 10.2F);
            btnLuu.ForeColor = Color.FromArgb(19, 78, 74);
            btnLuu.Location = new Point(0, 4);
            btnLuu.Margin = new Padding(0, 4, 0, 0);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(370, 44);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "LƯU PHIẾU";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // frmNhapKho
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1182, 653);
            Controls.Add(posLayout);
            Font = new Font("Segoe UI", 10.2F);
            ForeColor = Color.FromArgb(15, 23, 42);
            MinimumSize = new Size(1060, 700);
            Name = "frmNhapKho";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhập kho | Pharmacy";
            Load += frmNhapKho_Load;
            posLayout.ResumeLayout(false);
            stockHeader.ResumeLayout(false);
            excelActions.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((ISupportInitialize)dgvPhieu).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel11.ResumeLayout(false);
            groupBox13.ResumeLayout(false);
            tableLayoutPanel14.ResumeLayout(false);
            tableLayoutPanel14.PerformLayout();
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel16.ResumeLayout(false);
            tableLayoutPanel18.ResumeLayout(false);
            tableLayoutPanel19.ResumeLayout(false);
            tableLayoutPanel20.ResumeLayout(false);
            tableLayoutPanel22.ResumeLayout(false);
            tableLayoutPanel22.PerformLayout();
            tableLayoutPanel23.ResumeLayout(false);
            tableLayoutPanel24.ResumeLayout(false);
            tableLayoutPanel26.ResumeLayout(false);
            tableLayoutPanel27.ResumeLayout(false);
            ((ISupportInitialize)nudSoLuong).EndInit();
            tableLayoutPanel29.ResumeLayout(false);
            tableLayoutPanel30.ResumeLayout(false);
            ((ISupportInitialize)nudGiaNhap).EndInit();
            tableLayoutPanel32.ResumeLayout(false);
            tableLayoutPanel33.ResumeLayout(false);
            tableLayoutPanel33.PerformLayout();
            tableLayoutPanel34.ResumeLayout(false);
            ResumeLayout(false);
        }

        private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.DataGridView dgvPhieu;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn7;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvPhieuColumn8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.Button btnXoaDong;
    private global::System.Windows.Forms.Button btnXoaPhieu;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.Label label9;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.ComboBox cboNhaCungCap;
    private global::System.Windows.Forms.Button btnThemNCC;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
    private global::System.Windows.Forms.Label label12;
    private global::System.Windows.Forms.TextBox txtGhiChu;
    private global::System.Windows.Forms.Label lblNgayNhapGiaTri;
    private global::System.Windows.Forms.GroupBox groupBox13;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
    private global::System.Windows.Forms.Label label17;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
    private global::System.Windows.Forms.ComboBox cboThuoc;
    private global::System.Windows.Forms.Button btnTaoThuoc;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
    private global::System.Windows.Forms.Label label21;
    private global::System.Windows.Forms.TextBox txtSoLo;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel24;
    private global::System.Windows.Forms.Label label25;
    private global::System.Windows.Forms.DateTimePicker dtpHanDung;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel27;
    private global::System.Windows.Forms.Label label28;
    private global::System.Windows.Forms.NumericUpDown nudSoLuong;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel29;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel30;
    private global::System.Windows.Forms.Label label31;
    private global::System.Windows.Forms.NumericUpDown nudGiaNhap;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel32;
    private global::System.Windows.Forms.Button btnThemThuoc;
    private global::System.Windows.Forms.Button btnCapNhatDong;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel33;
    private global::System.Windows.Forms.Label lblTongVonText;
    private global::System.Windows.Forms.Label lblTongVon;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel34;
    private global::System.Windows.Forms.Button btnLuu;
    private FlowLayoutPanel excelActions;
    private Button btnNhapExcel,btnMauExcel;
    private TableLayoutPanel stockHeader;
}

}