namespace Store.POS
{
    partial class frmNhapKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpPhieu = new GroupBox();
            lblNhaCungCap = new Label();
            cboNhaCungCap = new ComboBox();
            lblNgayNhap = new Label();
            lblNgayNhapGiaTri = new Label();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            grpThem = new GroupBox();
            lblThuoc = new Label();
            cboThuoc = new ComboBox();
            lblSoLo = new Label();
            txtSoLo = new TextBox();
            lblHanDung = new Label();
            dtpHanDung = new DateTimePicker();
            lblSoLuong = new Label();
            nudSoLuong = new NumericUpDown();
            lblGiaNhap = new Label();
            nudGiaNhap = new NumericUpDown();
            btnThemThuoc = new Button();
            grpChiTiet = new GroupBox();
            dgvPhieu = new DataGridView();
            btnXoaDong = new Button();
            btnXoaPhieu = new Button();
            lblTongVonText = new Label();
            lblTongVon = new Label();
            btnLuu = new Button();
            grpPhieu.SuspendLayout();
            grpThem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudGiaNhap).BeginInit();
            grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPhieu).BeginInit();
            SuspendLayout();
            // 
            // grpPhieu
            // 
            grpPhieu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpPhieu.Controls.Add(lblNhaCungCap);
            grpPhieu.Controls.Add(cboNhaCungCap);
            grpPhieu.Controls.Add(lblNgayNhap);
            grpPhieu.Controls.Add(lblNgayNhapGiaTri);
            grpPhieu.Controls.Add(lblGhiChu);
            grpPhieu.Controls.Add(txtGhiChu);
            grpPhieu.Location = new Point(12, 12);
            grpPhieu.Name = "grpPhieu";
            grpPhieu.Size = new Size(1076, 70);
            grpPhieu.TabIndex = 0;
            grpPhieu.TabStop = false;
            grpPhieu.Text = "Thông tin phiếu nhập";
            // 
            // lblNhaCungCap
            // 
            lblNhaCungCap.Location = new Point(12, 30);
            lblNhaCungCap.Name = "lblNhaCungCap";
            lblNhaCungCap.Size = new Size(95, 23);
            lblNhaCungCap.TabIndex = 0;
            lblNhaCungCap.Text = "Nhà cung cấp:";
            lblNhaCungCap.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboNhaCungCap
            // 
            cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhaCungCap.Location = new Point(110, 30);
            cboNhaCungCap.Name = "cboNhaCungCap";
            cboNhaCungCap.Size = new Size(300, 23);
            cboNhaCungCap.TabIndex = 1;
            cboNhaCungCap.SelectionChangeCommitted += cboNhaCungCap_SelectionChangeCommitted;
            // 
            // lblNgayNhap
            // 
            lblNgayNhap.Location = new Point(430, 30);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new Size(75, 23);
            lblNgayNhap.TabIndex = 2;
            lblNgayNhap.Text = "Ngày nhập:";
            lblNgayNhap.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblNgayNhapGiaTri
            // 
            lblNgayNhapGiaTri.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNgayNhapGiaTri.Location = new Point(510, 30);
            lblNgayNhapGiaTri.Name = "lblNgayNhapGiaTri";
            lblNgayNhapGiaTri.Size = new Size(90, 23);
            lblNgayNhapGiaTri.TabIndex = 3;
            lblNgayNhapGiaTri.Text = "dd/MM/yyyy";
            lblNgayNhapGiaTri.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGhiChu
            // 
            lblGhiChu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGhiChu.Location = new Point(640, 30);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(60, 23);
            lblGhiChu.TabIndex = 4;
            lblGhiChu.Text = "Ghi chú:";
            lblGhiChu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtGhiChu.Location = new Point(705, 30);
            txtGhiChu.MaxLength = 250;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(355, 23);
            txtGhiChu.TabIndex = 5;
            // 
            // grpThem
            // 
            grpThem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpThem.Controls.Add(lblThuoc);
            grpThem.Controls.Add(cboThuoc);
            grpThem.Controls.Add(lblSoLo);
            grpThem.Controls.Add(txtSoLo);
            grpThem.Controls.Add(lblHanDung);
            grpThem.Controls.Add(dtpHanDung);
            grpThem.Controls.Add(lblSoLuong);
            grpThem.Controls.Add(nudSoLuong);
            grpThem.Controls.Add(lblGiaNhap);
            grpThem.Controls.Add(nudGiaNhap);
            grpThem.Controls.Add(btnThemThuoc);
            grpThem.Location = new Point(12, 90);
            grpThem.Name = "grpThem";
            grpThem.Size = new Size(1076, 110);
            grpThem.TabIndex = 1;
            grpThem.TabStop = false;
            grpThem.Text = "Thêm thuốc vào phiếu (chỉ hiện thuốc của nhà cung cấp đã chọn)";
            // 
            // lblThuoc
            // 
            lblThuoc.Location = new Point(12, 30);
            lblThuoc.Name = "lblThuoc";
            lblThuoc.Size = new Size(55, 23);
            lblThuoc.TabIndex = 0;
            lblThuoc.Text = "Thuốc:";
            lblThuoc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboThuoc
            // 
            cboThuoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThuoc.Location = new Point(72, 30);
            cboThuoc.Name = "cboThuoc";
            cboThuoc.Size = new Size(330, 23);
            cboThuoc.TabIndex = 1;
            // 
            // lblSoLo
            // 
            lblSoLo.Location = new Point(420, 30);
            lblSoLo.Name = "lblSoLo";
            lblSoLo.Size = new Size(50, 23);
            lblSoLo.TabIndex = 2;
            lblSoLo.Text = "Số lô:";
            lblSoLo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSoLo
            // 
            txtSoLo.Location = new Point(475, 30);
            txtSoLo.MaxLength = 50;
            txtSoLo.Name = "txtSoLo";
            txtSoLo.Size = new Size(150, 23);
            txtSoLo.TabIndex = 3;
            // 
            // lblHanDung
            // 
            lblHanDung.Location = new Point(645, 30);
            lblHanDung.Name = "lblHanDung";
            lblHanDung.Size = new Size(70, 23);
            lblHanDung.TabIndex = 4;
            lblHanDung.Text = "Hạn dùng:";
            lblHanDung.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpHanDung
            // 
            dtpHanDung.Format = DateTimePickerFormat.Short;
            dtpHanDung.Location = new Point(720, 30);
            dtpHanDung.Name = "dtpHanDung";
            dtpHanDung.Size = new Size(130, 23);
            dtpHanDung.TabIndex = 5;
            // 
            // lblSoLuong
            // 
            lblSoLuong.Location = new Point(12, 68);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(70, 23);
            lblSoLuong.TabIndex = 6;
            lblSoLuong.Text = "Số lượng:";
            lblSoLuong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Location = new Point(88, 68);
            nudSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(90, 23);
            nudSoLuong.TabIndex = 7;
            nudSoLuong.TextAlign = HorizontalAlignment.Right;
            nudSoLuong.ThousandsSeparator = true;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblGiaNhap
            // 
            lblGiaNhap.Location = new Point(200, 68);
            lblGiaNhap.Name = "lblGiaNhap";
            lblGiaNhap.Size = new Size(110, 23);
            lblGiaNhap.TabIndex = 8;
            lblGiaNhap.Text = "Giá nhập (đ/ĐVT):";
            lblGiaNhap.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudGiaNhap
            // 
            nudGiaNhap.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nudGiaNhap.Location = new Point(315, 68);
            nudGiaNhap.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudGiaNhap.Name = "nudGiaNhap";
            nudGiaNhap.Size = new Size(140, 23);
            nudGiaNhap.TabIndex = 9;
            nudGiaNhap.TextAlign = HorizontalAlignment.Right;
            nudGiaNhap.ThousandsSeparator = true;
            // 
            // btnThemThuoc
            // 
            btnThemThuoc.Location = new Point(480, 64);
            btnThemThuoc.Name = "btnThemThuoc";
            btnThemThuoc.Size = new Size(160, 32);
            btnThemThuoc.TabIndex = 10;
            btnThemThuoc.Text = "Thêm vào phiếu";
            btnThemThuoc.UseVisualStyleBackColor = true;
            btnThemThuoc.Click += btnThemThuoc_Click;
            // 
            // grpChiTiet
            // 
            grpChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpChiTiet.Controls.Add(dgvPhieu);
            grpChiTiet.Controls.Add(btnXoaDong);
            grpChiTiet.Controls.Add(btnXoaPhieu);
            grpChiTiet.Controls.Add(lblTongVonText);
            grpChiTiet.Controls.Add(lblTongVon);
            grpChiTiet.Controls.Add(btnLuu);
            grpChiTiet.Location = new Point(12, 208);
            grpChiTiet.Name = "grpChiTiet";
            grpChiTiet.Size = new Size(1076, 460);
            grpChiTiet.TabIndex = 2;
            grpChiTiet.TabStop = false;
            grpChiTiet.Text = "Chi tiết phiếu nhập";
            // 
            // dgvPhieu
            // 
            dgvPhieu.AllowUserToAddRows = false;
            dgvPhieu.AllowUserToDeleteRows = false;
            dgvPhieu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPhieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhieu.Location = new Point(12, 24);
            dgvPhieu.MultiSelect = false;
            dgvPhieu.Name = "dgvPhieu";
            dgvPhieu.ReadOnly = true;
            dgvPhieu.RowHeadersVisible = false;
            dgvPhieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieu.Size = new Size(1052, 350);
            dgvPhieu.TabIndex = 0;
            // 
            // btnXoaDong
            // 
            btnXoaDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaDong.Location = new Point(12, 388);
            btnXoaDong.Name = "btnXoaDong";
            btnXoaDong.Size = new Size(110, 30);
            btnXoaDong.TabIndex = 1;
            btnXoaDong.Text = "Xóa dòng";
            btnXoaDong.UseVisualStyleBackColor = true;
            btnXoaDong.Click += btnXoaDong_Click;
            // 
            // btnXoaPhieu
            // 
            btnXoaPhieu.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaPhieu.Location = new Point(130, 388);
            btnXoaPhieu.Name = "btnXoaPhieu";
            btnXoaPhieu.Size = new Size(110, 30);
            btnXoaPhieu.TabIndex = 2;
            btnXoaPhieu.Text = "Xóa phiếu";
            btnXoaPhieu.UseVisualStyleBackColor = true;
            btnXoaPhieu.Click += btnXoaPhieu_Click;
            // 
            // lblTongVonText
            // 
            lblTongVonText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTongVonText.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblTongVonText.Location = new Point(500, 388);
            lblTongVonText.Name = "lblTongVonText";
            lblTongVonText.Size = new Size(180, 32);
            lblTongVonText.TabIndex = 3;
            lblTongVonText.Text = "TỔNG TIỀN VỐN:";
            lblTongVonText.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTongVon
            // 
            lblTongVon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTongVon.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTongVon.ForeColor = Color.DarkGreen;
            lblTongVon.Location = new Point(684, 386);
            lblTongVon.Name = "lblTongVon";
            lblTongVon.Size = new Size(230, 34);
            lblTongVon.TabIndex = 4;
            lblTongVon.Text = "0 đ";
            lblTongVon.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLuu
            // 
            btnLuu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLuu.BackColor = Color.SeaGreen;
            btnLuu.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(924, 386);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(140, 36);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "LƯU PHIẾU";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // frmNhapKho
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(grpChiTiet);
            Controls.Add(grpThem);
            Controls.Add(grpPhieu);
            Name = "frmNhapKho";
            Text = "Nhập kho";
            Load += frmNhapKho_Load;
            grpPhieu.ResumeLayout(false);
            grpPhieu.PerformLayout();
            grpThem.ResumeLayout(false);
            grpThem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudGiaNhap).EndInit();
            grpChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPhieu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpPhieu;
        private Label lblNhaCungCap;
        private ComboBox cboNhaCungCap;
        private Label lblNgayNhap;
        private Label lblNgayNhapGiaTri;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private GroupBox grpThem;
        private Label lblThuoc;
        private ComboBox cboThuoc;
        private Label lblSoLo;
        private TextBox txtSoLo;
        private Label lblHanDung;
        private DateTimePicker dtpHanDung;
        private Label lblSoLuong;
        private NumericUpDown nudSoLuong;
        private Label lblGiaNhap;
        private NumericUpDown nudGiaNhap;
        private Button btnThemThuoc;
        private GroupBox grpChiTiet;
        private DataGridView dgvPhieu;
        private Button btnXoaDong;
        private Button btnXoaPhieu;
        private Label lblTongVonText;
        private Label lblTongVon;
        private Button btnLuu;
    }
}
