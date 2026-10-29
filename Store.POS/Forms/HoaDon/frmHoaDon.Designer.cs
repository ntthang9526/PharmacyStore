using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmHoaDon
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
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle25 = new DataGridViewCellStyle();
            dgvChiTietColumn1 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn2 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn3 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn4 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn5 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn6 = new DataGridViewTextBoxColumn();
            dgvChiTietColumn7 = new DataGridViewTextBoxColumn();
            posLayout = new TableLayoutPanel();
            posHeading = new Label();
            excelActions = new FlowLayoutPanel();
            btnXuatExcel = new Button();
            btnXuatChiTietExcel = new Button();
            btnThanhToanCho = new Button();
            cboHinhThuc = new ComboBox();
            chkChiCho = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label3 = new Label();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            label8 = new Label();
            dtpTuNgay = new DateTimePicker();
            tableLayoutPanel9 = new TableLayoutPanel();
            tableLayoutPanel10 = new TableLayoutPanel();
            label11 = new Label();
            dtpDenNgay = new DateTimePicker();
            tableLayoutPanel12 = new TableLayoutPanel();
            btnLamMoi = new Button();
            receiptColumns = new TableLayoutPanel();
            groupBox13 = new GroupBox();
            dgvHoaDon = new DataGridView();
            dgvHoaDonColumn1 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn2 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn3 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn4 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn5 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn6 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn7 = new DataGridViewTextBoxColumn();
            dgvHoaDonColumn8 = new DataGridViewTextBoxColumn();
            colPaymentMethod = new DataGridViewTextBoxColumn();
            colPaymentStatus = new DataGridViewTextBoxColumn();
            groupBox14 = new GroupBox();
            tableLayoutPanel15 = new TableLayoutPanel();
            lblChiTiet = new Label();
            dgvChiTiet = new DataGridView();
            lblTrangThai = new Label();
            posLayout.SuspendLayout();
            excelActions.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel10.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            receiptColumns.SuspendLayout();
            groupBox13.SuspendLayout();
            ((ISupportInitialize)dgvHoaDon).BeginInit();
            groupBox14.SuspendLayout();
            tableLayoutPanel15.SuspendLayout();
            ((ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();
            // 
            // dgvChiTietColumn1
            // 
            dgvChiTietColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn1.DataPropertyName = "TenThuoc";
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            dgvChiTietColumn1.FillWeight = 220F;
            dgvChiTietColumn1.HeaderText = "Tên thuốc";
            dgvChiTietColumn1.MinimumWidth = 6;
            dgvChiTietColumn1.Name = "dgvChiTietColumn1";
            dgvChiTietColumn1.ReadOnly = true;
            dgvChiTietColumn1.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn1.Width = 200;
            // 
            // dgvChiTietColumn2
            // 
            dgvChiTietColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn2.DataPropertyName = "DonViTinh";
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            dgvChiTietColumn2.FillWeight = 48F;
            dgvChiTietColumn2.HeaderText = "Đơn vị tính";
            dgvChiTietColumn2.MinimumWidth = 6;
            dgvChiTietColumn2.Name = "dgvChiTietColumn2";
            dgvChiTietColumn2.ReadOnly = true;
            dgvChiTietColumn2.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn2.Width = 134;
            // 
            // dgvChiTietColumn3
            // 
            dgvChiTietColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn3.DataPropertyName = "SoLo";
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            dgvChiTietColumn3.FillWeight = 64F;
            dgvChiTietColumn3.HeaderText = "Số lô";
            dgvChiTietColumn3.MinimumWidth = 6;
            dgvChiTietColumn3.Name = "dgvChiTietColumn3";
            dgvChiTietColumn3.ReadOnly = true;
            dgvChiTietColumn3.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn3.Width = 140;
            // 
            // dgvChiTietColumn4
            // 
            dgvChiTietColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn4.DataPropertyName = "SoLuong";
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            dgvChiTietColumn4.FillWeight = 48F;
            dgvChiTietColumn4.HeaderText = "Số lượng";
            dgvChiTietColumn4.MinimumWidth = 6;
            dgvChiTietColumn4.Name = "dgvChiTietColumn4";
            dgvChiTietColumn4.ReadOnly = true;
            dgvChiTietColumn4.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn4.Width = 118;
            // 
            // dgvChiTietColumn5
            // 
            dgvChiTietColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn5.DataPropertyName = "DonGia";
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn5.DefaultCellStyle = dataGridViewCellStyle5;
            dgvChiTietColumn5.FillWeight = 80F;
            dgvChiTietColumn5.HeaderText = "Đơn giá";
            dgvChiTietColumn5.MinimumWidth = 6;
            dgvChiTietColumn5.Name = "dgvChiTietColumn5";
            dgvChiTietColumn5.ReadOnly = true;
            dgvChiTietColumn5.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn5.Width = 115;
            // 
            // dgvChiTietColumn6
            // 
            dgvChiTietColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn6.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            dgvChiTietColumn6.FillWeight = 104F;
            dgvChiTietColumn6.HeaderText = "Thành tiền";
            dgvChiTietColumn6.MinimumWidth = 6;
            dgvChiTietColumn6.Name = "dgvChiTietColumn6";
            dgvChiTietColumn6.ReadOnly = true;
            dgvChiTietColumn6.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn6.Width = 132;
            // 
            // dgvChiTietColumn7
            // 
            dgvChiTietColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvChiTietColumn7.DataPropertyName = "HuongDan";
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgvChiTietColumn7.DefaultCellStyle = dataGridViewCellStyle7;
            dgvChiTietColumn7.FillWeight = 96F;
            dgvChiTietColumn7.HeaderText = "Hướng dẫn sử dụng";
            dgvChiTietColumn7.MinimumWidth = 6;
            dgvChiTietColumn7.Name = "dgvChiTietColumn7";
            dgvChiTietColumn7.ReadOnly = true;
            dgvChiTietColumn7.Resizable = DataGridViewTriState.False;
            dgvChiTietColumn7.Width = 203;
            // 
            // posLayout
            // 
            posLayout.BackColor = Color.FromArgb(248, 250, 252);
            posLayout.ColumnCount = 1;
            posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            posLayout.Controls.Add(posHeading, 0, 0);
            posLayout.Controls.Add(excelActions, 0, 1);
            posLayout.Controls.Add(tableLayoutPanel1, 0, 2);
            posLayout.Dock = DockStyle.Fill;
            posLayout.Font = new Font("Segoe UI", 10.2F);
            posLayout.ForeColor = SystemColors.ControlText;
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
            posHeading.BackColor = Color.FromArgb(248, 250, 252);
            posHeading.Dock = DockStyle.Fill;
            posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            posHeading.ForeColor = SystemColors.ControlText;
            posHeading.Location = new Point(15, 12);
            posHeading.Name = "posHeading";
            posHeading.Size = new Size(1152, 48);
            posHeading.TabIndex = 0;
            posHeading.Text = "Xem hóa đơn";
            posHeading.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // excelActions
            // 
            excelActions.Controls.Add(btnXuatExcel);
            excelActions.Controls.Add(btnXuatChiTietExcel);
            excelActions.Controls.Add(btnThanhToanCho);
            excelActions.Controls.Add(cboHinhThuc);
            excelActions.Controls.Add(chkChiCho);
            excelActions.Dock = DockStyle.Fill;
            excelActions.Location = new Point(12, 60);
            excelActions.Margin = new Padding(0);
            excelActions.Name = "excelActions";
            excelActions.Size = new Size(1158, 46);
            excelActions.TabIndex = 1;
            excelActions.WrapContents = false;
            // 
            // btnXuatExcel
            // 
            btnXuatExcel.BackColor = Color.FromArgb(15, 118, 110);
            btnXuatExcel.Cursor = Cursors.Hand;
            btnXuatExcel.FlatAppearance.BorderSize = 0;
            btnXuatExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnXuatExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnXuatExcel.FlatStyle = FlatStyle.Flat;
            btnXuatExcel.ForeColor = Color.White;
            btnXuatExcel.Location = new Point(3, 3);
            btnXuatExcel.Name = "btnXuatExcel";
            btnXuatExcel.Size = new Size(130, 38);
            btnXuatExcel.TabIndex = 0;
            btnXuatExcel.Text = "Xuất DS Excel";
            btnXuatExcel.UseVisualStyleBackColor = false;
            // 
            // btnXuatChiTietExcel
            // 
            btnXuatChiTietExcel.BackColor = Color.FromArgb(15, 118, 110);
            btnXuatChiTietExcel.Cursor = Cursors.Hand;
            btnXuatChiTietExcel.FlatAppearance.BorderSize = 0;
            btnXuatChiTietExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnXuatChiTietExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnXuatChiTietExcel.FlatStyle = FlatStyle.Flat;
            btnXuatChiTietExcel.ForeColor = Color.White;
            btnXuatChiTietExcel.Location = new Point(139, 3);
            btnXuatChiTietExcel.Name = "btnXuatChiTietExcel";
            btnXuatChiTietExcel.Size = new Size(130, 38);
            btnXuatChiTietExcel.TabIndex = 1;
            btnXuatChiTietExcel.Text = "Xuất chi tiết";
            btnXuatChiTietExcel.UseVisualStyleBackColor = false;
            // 
            // btnThanhToanCho
            // 
            btnThanhToanCho.BackColor = Color.FromArgb(22, 101, 52);
            btnThanhToanCho.Cursor = Cursors.Hand;
            btnThanhToanCho.FlatAppearance.BorderSize = 0;
            btnThanhToanCho.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
            btnThanhToanCho.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
            btnThanhToanCho.FlatStyle = FlatStyle.Flat;
            btnThanhToanCho.ForeColor = Color.White;
            btnThanhToanCho.Location = new Point(275, 3);
            btnThanhToanCho.Name = "btnThanhToanCho";
            btnThanhToanCho.Size = new Size(120, 38);
            btnThanhToanCho.TabIndex = 2;
            btnThanhToanCho.Text = "Thanh toán";
            btnThanhToanCho.UseVisualStyleBackColor = false;
            // 
            // cboHinhThuc
            // 
            cboHinhThuc.BackColor = Color.White;
            cboHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThuc.Font = new Font("Segoe UI", 10.5F);
            cboHinhThuc.ForeColor = Color.FromArgb(15, 23, 42);
            cboHinhThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản QR" });
            cboHinhThuc.Location = new Point(401, 3);
            cboHinhThuc.Name = "cboHinhThuc";
            cboHinhThuc.Size = new Size(160, 31);
            cboHinhThuc.TabIndex = 3;
            // 
            // chkChiCho
            // 
            chkChiCho.Location = new Point(567, 3);
            chkChiCho.Name = "chkChiCho";
            chkChiCho.Size = new Size(100, 38);
            chkChiCho.TabIndex = 4;
            chkChiCho.Text = "Chỉ chờ";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel1.Controls.Add(receiptColumns, 0, 2);
            tableLayoutPanel1.Controls.Add(lblTrangThai, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel1.ForeColor = SystemColors.ControlText;
            tableLayoutPanel1.Location = new Point(12, 106);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.Size = new Size(1158, 535);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(txtTimKiem, 1, 0);
            tableLayoutPanel2.Controls.Add(btnTimKiem, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel2.ForeColor = SystemColors.ControlText;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0, 0, 0, 12);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1158, 44);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(248, 250, 252);
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 10.2F);
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(114, 44);
            label3.TabIndex = 0;
            label3.Text = "Từ khóa";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTimKiem
            // 
            txtTimKiem.BackColor = Color.White;
            txtTimKiem.BorderStyle = BorderStyle.FixedSingle;
            txtTimKiem.Cursor = Cursors.IBeam;
            txtTimKiem.Dock = DockStyle.Top;
            txtTimKiem.Font = new Font("Segoe UI", 10.5F);
            txtTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
            txtTimKiem.Location = new Point(120, 5);
            txtTimKiem.Margin = new Padding(0, 5, 8, 5);
            txtTimKiem.MaxLength = 200;
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Mã hóa đơn, nhân viên, khách hàng, điện thoại hoặc voucher";
            txtTimKiem.Size = new Size(910, 31);
            txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.FromArgb(15, 118, 110);
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.Dock = DockStyle.Fill;
            btnTimKiem.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnTimKiem.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.Location = new Point(1038, 5);
            btnTimKiem.Margin = new Padding(0, 5, 0, 5);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(120, 34);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel12, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel4.ForeColor = SystemColors.ControlText;
            tableLayoutPanel4.Location = new Point(0, 56);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(1158, 58);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 265F));
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel9, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel5.ForeColor = SystemColors.ControlText;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(0, 0, 12, 0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(1026, 58);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel7);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel6.ForeColor = SystemColors.ControlText;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Margin = new Padding(0, 0, 12, 0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel6.Size = new Size(749, 58);
            tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(label8, 0, 0);
            tableLayoutPanel7.Controls.Add(dtpTuNgay, 0, 1);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel7.ForeColor = SystemColors.ControlText;
            tableLayoutPanel7.Location = new Point(0, 0);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel7.Size = new Size(749, 58);
            tableLayoutPanel7.TabIndex = 0;
            // 
            // label8
            // 
            label8.BackColor = Color.FromArgb(248, 250, 252);
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Segoe UI", 10.2F);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(3, 0);
            label8.Name = "label8";
            label8.Size = new Size(743, 22);
            label8.TabIndex = 0;
            label8.Text = "Từ ngày (tích để lọc)";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.BackColor = SystemColors.Window;
            dtpTuNgay.Checked = false;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Dock = DockStyle.Top;
            dtpTuNgay.Font = new Font("Segoe UI", 10.5F);
            dtpTuNgay.ForeColor = Color.FromArgb(15, 23, 42);
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(0, 23);
            dtpTuNgay.Margin = new Padding(0, 1, 0, 5);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.ShowCheckBox = true;
            dtpTuNgay.Size = new Size(749, 31);
            dtpTuNgay.TabIndex = 1;
            dtpTuNgay.Value = new DateTime(2026, 9, 22, 0, 0, 0, 0);
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.AutoSize = true;
            tableLayoutPanel9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel9.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Controls.Add(tableLayoutPanel10);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel9.ForeColor = SystemColors.ControlText;
            tableLayoutPanel9.Location = new Point(761, 0);
            tableLayoutPanel9.Margin = new Padding(0);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.RowCount = 1;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel9.Size = new Size(265, 58);
            tableLayoutPanel9.TabIndex = 1;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel10.Controls.Add(label11, 0, 0);
            tableLayoutPanel10.Controls.Add(dtpDenNgay, 0, 1);
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel10.ForeColor = SystemColors.ControlText;
            tableLayoutPanel10.Location = new Point(0, 0);
            tableLayoutPanel10.Margin = new Padding(0);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 2;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel10.Size = new Size(265, 58);
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
            label11.Size = new Size(259, 22);
            label11.TabIndex = 0;
            label11.Text = "Đến ngày (tích để lọc)";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.BackColor = SystemColors.Window;
            dtpDenNgay.Checked = false;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Dock = DockStyle.Top;
            dtpDenNgay.Font = new Font("Segoe UI", 10.5F);
            dtpDenNgay.ForeColor = Color.FromArgb(15, 23, 42);
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(0, 23);
            dtpDenNgay.Margin = new Padding(0, 1, 0, 5);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.ShowCheckBox = true;
            dtpDenNgay.Size = new Size(265, 31);
            dtpDenNgay.TabIndex = 1;
            dtpDenNgay.Value = new DateTime(2026, 10, 22, 0, 0, 0, 0);
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.Anchor = AnchorStyles.Left;
            tableLayoutPanel12.AutoSize = true;
            tableLayoutPanel12.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel12.ColumnCount = 2;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel12.Controls.Add(btnLamMoi, 0, 0);
            tableLayoutPanel12.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel12.ForeColor = SystemColors.ControlText;
            tableLayoutPanel12.Location = new Point(1038, 7);
            tableLayoutPanel12.Margin = new Padding(0);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.RowCount = 1;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel12.Size = new Size(120, 44);
            tableLayoutPanel12.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Left;
            btnLamMoi.BackColor = Color.FromArgb(15, 118, 110);
            tableLayoutPanel12.SetColumnSpan(btnLamMoi, 2);
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
            btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Location = new Point(0, 5);
            btnLamMoi.Margin = new Padding(0, 5, 0, 5);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(120, 34);
            btnLamMoi.TabIndex = 0;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // receiptColumns
            // 
            receiptColumns.ColumnCount = 2;
            receiptColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            receiptColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            receiptColumns.Controls.Add(groupBox13, 0, 0);
            receiptColumns.Controls.Add(groupBox14, 1, 0);
            receiptColumns.Dock = DockStyle.Fill;
            receiptColumns.Location = new Point(0, 114);
            receiptColumns.Margin = new Padding(0);
            receiptColumns.Name = "receiptColumns";
            receiptColumns.RowCount = 1;
            receiptColumns.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            receiptColumns.Size = new Size(1158, 389);
            receiptColumns.TabIndex = 2;
            // 
            // groupBox13
            // 
            groupBox13.BackColor = Color.FromArgb(248, 250, 252);
            groupBox13.Controls.Add(dgvHoaDon);
            groupBox13.Dock = DockStyle.Fill;
            groupBox13.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            groupBox13.ForeColor = SystemColors.ControlText;
            groupBox13.Location = new Point(0, 0);
            groupBox13.Margin = new Padding(0);
            groupBox13.Name = "groupBox13";
            groupBox13.Padding = new Padding(12, 2, 12, 12);
            groupBox13.Size = new Size(521, 389);
            groupBox13.TabIndex = 2;
            groupBox13.TabStop = false;
            groupBox13.Text = "Hóa đơn đã lưu";
            // 
            // dgvHoaDon
            // 
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.AllowUserToResizeColumns = false;
            dgvHoaDon.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(248, 250, 252);
            dgvHoaDon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            dgvHoaDon.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvHoaDon.BackgroundColor = Color.White;
            dgvHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle9.ForeColor = Color.White;
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dgvHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dgvHoaDon.ColumnHeadersHeight = 43;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHoaDon.Columns.AddRange(new DataGridViewColumn[] { dgvHoaDonColumn1, dgvHoaDonColumn2, dgvHoaDonColumn3, dgvHoaDonColumn4, dgvHoaDonColumn5, dgvHoaDonColumn6, dgvHoaDonColumn7, dgvHoaDonColumn8, colPaymentMethod, colPaymentStatus });
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = Color.White;
            dataGridViewCellStyle20.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle20.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle20.Padding = new Padding(4);
            dataGridViewCellStyle20.SelectionBackColor = Color.FromArgb(204, 251, 241);
            dataGridViewCellStyle20.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle20.WrapMode = DataGridViewTriState.False;
            dgvHoaDon.DefaultCellStyle = dataGridViewCellStyle20;
            dgvHoaDon.Dock = DockStyle.Fill;
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.Font = new Font("Segoe UI", 10.2F);
            dgvHoaDon.GridColor = SystemColors.ControlLight;
            dgvHoaDon.Location = new Point(12, 25);
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.ReadOnly = true;
            dataGridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle21.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle21.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = DataGridViewTriState.True;
            dgvHoaDon.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.RowHeadersWidth = 51;
            dgvHoaDon.RowTemplate.Height = 34;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.Size = new Size(497, 352);
            dgvHoaDon.TabIndex = 0;
            // 
            // dgvHoaDonColumn1
            // 
            dgvHoaDonColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn1.DataPropertyName = "MaHD";
            dataGridViewCellStyle10.Format = "HD00000";
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            dgvHoaDonColumn1.FillWeight = 64F;
            dgvHoaDonColumn1.HeaderText = "Mã HĐ";
            dgvHoaDonColumn1.MinimumWidth = 6;
            dgvHoaDonColumn1.Name = "dgvHoaDonColumn1";
            dgvHoaDonColumn1.ReadOnly = true;
            dgvHoaDonColumn1.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn1.Width = 103;
            // 
            // dgvHoaDonColumn2
            // 
            dgvHoaDonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn2.DataPropertyName = "NgayLap";
            dataGridViewCellStyle11.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn2.DefaultCellStyle = dataGridViewCellStyle11;
            dgvHoaDonColumn2.FillWeight = 88F;
            dgvHoaDonColumn2.HeaderText = "Ngày lập";
            dgvHoaDonColumn2.MinimumWidth = 6;
            dgvHoaDonColumn2.Name = "dgvHoaDonColumn2";
            dgvHoaDonColumn2.ReadOnly = true;
            dgvHoaDonColumn2.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn2.Width = 140;
            // 
            // dgvHoaDonColumn3
            // 
            dgvHoaDonColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn3.DataPropertyName = "NhanVien";
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn3.DefaultCellStyle = dataGridViewCellStyle12;
            dgvHoaDonColumn3.FillWeight = 220F;
            dgvHoaDonColumn3.HeaderText = "Nhân viên";
            dgvHoaDonColumn3.MinimumWidth = 6;
            dgvHoaDonColumn3.Name = "dgvHoaDonColumn3";
            dgvHoaDonColumn3.ReadOnly = true;
            dgvHoaDonColumn3.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn3.Width = 160;
            // 
            // dgvHoaDonColumn4
            // 
            dgvHoaDonColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn4.DataPropertyName = "KhachHang";
            dataGridViewCellStyle13.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn4.DefaultCellStyle = dataGridViewCellStyle13;
            dgvHoaDonColumn4.FillWeight = 220F;
            dgvHoaDonColumn4.HeaderText = "Khách hàng";
            dgvHoaDonColumn4.MinimumWidth = 6;
            dgvHoaDonColumn4.Name = "dgvHoaDonColumn4";
            dgvHoaDonColumn4.ReadOnly = true;
            dgvHoaDonColumn4.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn4.Width = 160;
            // 
            // dgvHoaDonColumn5
            // 
            dgvHoaDonColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn5.DataPropertyName = "Voucher";
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn5.DefaultCellStyle = dataGridViewCellStyle14;
            dgvHoaDonColumn5.FillWeight = 80F;
            dgvHoaDonColumn5.HeaderText = "Mã voucher";
            dgvHoaDonColumn5.MinimumWidth = 6;
            dgvHoaDonColumn5.Name = "dgvHoaDonColumn5";
            dgvHoaDonColumn5.ReadOnly = true;
            dgvHoaDonColumn5.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn5.Width = 140;
            // 
            // dgvHoaDonColumn6
            // 
            dgvHoaDonColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn6.DataPropertyName = "TongTien";
            dataGridViewCellStyle15.Format = "N0";
            dataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn6.DefaultCellStyle = dataGridViewCellStyle15;
            dgvHoaDonColumn6.FillWeight = 96F;
            dgvHoaDonColumn6.HeaderText = "Tổng tiền";
            dgvHoaDonColumn6.MinimumWidth = 6;
            dgvHoaDonColumn6.Name = "dgvHoaDonColumn6";
            dgvHoaDonColumn6.ReadOnly = true;
            dgvHoaDonColumn6.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn6.Width = 126;
            // 
            // dgvHoaDonColumn7
            // 
            dgvHoaDonColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn7.DataPropertyName = "GiamGia";
            dataGridViewCellStyle16.Format = "N0";
            dataGridViewCellStyle16.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn7.DefaultCellStyle = dataGridViewCellStyle16;
            dgvHoaDonColumn7.FillWeight = 88F;
            dgvHoaDonColumn7.HeaderText = "Giảm giá";
            dgvHoaDonColumn7.MinimumWidth = 6;
            dgvHoaDonColumn7.Name = "dgvHoaDonColumn7";
            dgvHoaDonColumn7.ReadOnly = true;
            dgvHoaDonColumn7.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn7.Width = 118;
            // 
            // dgvHoaDonColumn8
            // 
            dgvHoaDonColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvHoaDonColumn8.DataPropertyName = "ThucThu";
            dataGridViewCellStyle17.Format = "N0";
            dataGridViewCellStyle17.WrapMode = DataGridViewTriState.False;
            dgvHoaDonColumn8.DefaultCellStyle = dataGridViewCellStyle17;
            dgvHoaDonColumn8.FillWeight = 88F;
            dgvHoaDonColumn8.HeaderText = "Thực thu";
            dgvHoaDonColumn8.MinimumWidth = 6;
            dgvHoaDonColumn8.Name = "dgvHoaDonColumn8";
            dgvHoaDonColumn8.ReadOnly = true;
            dgvHoaDonColumn8.Resizable = DataGridViewTriState.False;
            dgvHoaDonColumn8.Width = 118;
            // 
            // colPaymentMethod
            // 
            colPaymentMethod.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPaymentMethod.DataPropertyName = "HinhThucThanhToan";
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.False;
            colPaymentMethod.DefaultCellStyle = dataGridViewCellStyle18;
            colPaymentMethod.FillWeight = 104F;
            colPaymentMethod.HeaderText = "Hình thức thanh toán";
            colPaymentMethod.MinimumWidth = 6;
            colPaymentMethod.Name = "colPaymentMethod";
            colPaymentMethod.ReadOnly = true;
            colPaymentMethod.Resizable = DataGridViewTriState.False;
            colPaymentMethod.Width = 215;
            // 
            // colPaymentStatus
            // 
            colPaymentStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPaymentStatus.DataPropertyName = "TrangThai";
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
            colPaymentStatus.DefaultCellStyle = dataGridViewCellStyle19;
            colPaymentStatus.FillWeight = 104F;
            colPaymentStatus.HeaderText = "Trạng thái";
            colPaymentStatus.MinimumWidth = 6;
            colPaymentStatus.Name = "colPaymentStatus";
            colPaymentStatus.ReadOnly = true;
            colPaymentStatus.Resizable = DataGridViewTriState.False;
            colPaymentStatus.Width = 127;
            // 
            // groupBox14
            // 
            groupBox14.BackColor = Color.FromArgb(248, 250, 252);
            groupBox14.Controls.Add(tableLayoutPanel15);
            groupBox14.Dock = DockStyle.Fill;
            groupBox14.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            groupBox14.ForeColor = SystemColors.ControlText;
            groupBox14.Location = new Point(521, 0);
            groupBox14.Margin = new Padding(0);
            groupBox14.Name = "groupBox14";
            groupBox14.Padding = new Padding(12, 2, 12, 12);
            groupBox14.Size = new Size(637, 389);
            groupBox14.TabIndex = 3;
            groupBox14.TabStop = false;
            groupBox14.Text = "Chi tiết hóa đơn";
            // 
            // tableLayoutPanel15
            // 
            tableLayoutPanel15.BackColor = Color.FromArgb(248, 250, 252);
            tableLayoutPanel15.ColumnCount = 1;
            tableLayoutPanel15.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Controls.Add(lblChiTiet, 0, 0);
            tableLayoutPanel15.Controls.Add(dgvChiTiet, 0, 1);
            tableLayoutPanel15.Dock = DockStyle.Fill;
            tableLayoutPanel15.Font = new Font("Segoe UI", 10.2F);
            tableLayoutPanel15.ForeColor = SystemColors.ControlText;
            tableLayoutPanel15.Location = new Point(12, 25);
            tableLayoutPanel15.Margin = new Padding(0);
            tableLayoutPanel15.Name = "tableLayoutPanel15";
            tableLayoutPanel15.RowCount = 2;
            tableLayoutPanel15.RowStyles.Add(new RowStyle());
            tableLayoutPanel15.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel15.Size = new Size(613, 352);
            tableLayoutPanel15.TabIndex = 0;
            // 
            // lblChiTiet
            // 
            lblChiTiet.AutoSize = true;
            lblChiTiet.BackColor = Color.FromArgb(248, 250, 252);
            lblChiTiet.Dock = DockStyle.Fill;
            lblChiTiet.Font = new Font("Segoe UI", 10.2F);
            lblChiTiet.ForeColor = SystemColors.ControlText;
            lblChiTiet.Location = new Point(3, 0);
            lblChiTiet.Name = "lblChiTiet";
            lblChiTiet.Size = new Size(607, 23);
            lblChiTiet.TabIndex = 0;
            lblChiTiet.Text = "Chọn hóa đơn trong bảng để xem các thuốc đã bán.";
            // 
            // dgvChiTiet
            // 
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.AllowUserToResizeColumns = false;
            dgvChiTiet.AllowUserToResizeRows = false;
            dataGridViewCellStyle22.BackColor = Color.FromArgb(248, 250, 252);
            dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle22;
            dgvChiTiet.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvChiTiet.BackgroundColor = Color.White;
            dgvChiTiet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle23.BackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle23.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle23.ForeColor = Color.White;
            dataGridViewCellStyle23.SelectionBackColor = Color.FromArgb(15, 118, 110);
            dataGridViewCellStyle23.SelectionForeColor = Color.White;
            dataGridViewCellStyle23.WrapMode = DataGridViewTriState.False;
            dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            dgvChiTiet.ColumnHeadersHeight = 43;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { dgvChiTietColumn1, dgvChiTietColumn2, dgvChiTietColumn3, dgvChiTietColumn4, dgvChiTietColumn5, dgvChiTietColumn6, dgvChiTietColumn7 });
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = Color.White;
            dataGridViewCellStyle24.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle24.ForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle24.Padding = new Padding(4);
            dataGridViewCellStyle24.SelectionBackColor = Color.FromArgb(204, 251, 241);
            dataGridViewCellStyle24.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle24.WrapMode = DataGridViewTriState.False;
            dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle24;
            dgvChiTiet.Dock = DockStyle.Fill;
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.Font = new Font("Segoe UI", 10.2F);
            dgvChiTiet.GridColor = SystemColors.ControlLight;
            dgvChiTiet.Location = new Point(3, 26);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dataGridViewCellStyle25.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle25.Font = new Font("Segoe UI", 10.2F);
            dataGridViewCellStyle25.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = DataGridViewTriState.True;
            dgvChiTiet.RowHeadersDefaultCellStyle = dataGridViewCellStyle25;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.RowHeadersWidth = 51;
            dgvChiTiet.RowTemplate.Height = 34;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(607, 323);
            dgvChiTiet.TabIndex = 1;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoEllipsis = true;
            lblTrangThai.BackColor = Color.FromArgb(248, 250, 252);
            lblTrangThai.Dock = DockStyle.Fill;
            lblTrangThai.Font = new Font("Segoe UI", 10.2F);
            lblTrangThai.ForeColor = SystemColors.ControlText;
            lblTrangThai.Location = new Point(3, 503);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(1152, 32);
            lblTrangThai.TabIndex = 4;
            // 
            // frmHoaDon
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1182, 653);
            Controls.Add(posLayout);
            Font = new Font("Segoe UI", 10.2F);
            ForeColor = Color.FromArgb(15, 23, 42);
            MinimumSize = new Size(1060, 700);
            Name = "frmHoaDon";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xem hóa đơn | Hiệu thuốc ABC";
            posLayout.ResumeLayout(false);
            excelActions.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel10.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            receiptColumns.ResumeLayout(false);
            groupBox13.ResumeLayout(false);
            ((ISupportInitialize)dgvHoaDon).EndInit();
            groupBox14.ResumeLayout(false);
            tableLayoutPanel15.ResumeLayout(false);
            tableLayoutPanel15.PerformLayout();
            ((ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
        }

        private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private TableLayoutPanel receiptColumns;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private global::System.Windows.Forms.Label label3;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.Label label8;
    private global::System.Windows.Forms.DateTimePicker dtpTuNgay;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.Label label11;
    private global::System.Windows.Forms.DateTimePicker dtpDenNgay;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
    private global::System.Windows.Forms.Button btnLamMoi;
    private global::System.Windows.Forms.GroupBox groupBox13;
    private global::System.Windows.Forms.DataGridView dgvHoaDon;
    private global::System.Windows.Forms.GroupBox groupBox14;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
    private global::System.Windows.Forms.Label lblChiTiet;
    private global::System.Windows.Forms.DataGridView dgvChiTiet;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvChiTietColumn7;
    private global::System.Windows.Forms.Label lblTrangThai;
    private FlowLayoutPanel excelActions;
    private Button btnXuatExcel,btnXuatChiTietExcel,btnThanhToanCho;
    private CheckBox chkChiCho;
    private ComboBox cboHinhThuc;
        private DataGridViewTextBoxColumn dgvHoaDonColumn1;
        private DataGridViewTextBoxColumn dgvHoaDonColumn2;
        private DataGridViewTextBoxColumn dgvHoaDonColumn3;
        private DataGridViewTextBoxColumn dgvHoaDonColumn4;
        private DataGridViewTextBoxColumn dgvHoaDonColumn5;
        private DataGridViewTextBoxColumn dgvHoaDonColumn6;
        private DataGridViewTextBoxColumn dgvHoaDonColumn7;
        private DataGridViewTextBoxColumn dgvHoaDonColumn8;
        private DataGridViewTextBoxColumn colPaymentMethod;
        private DataGridViewTextBoxColumn colPaymentStatus;
    }

}