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
            grpThuoc = new GroupBox();
            txtTimKiem = new TextBox();
            btnTimKiem = new Button();
            dgvThuoc = new DataGridView();
            lblSoLuong = new Label();
            nudSoLuong = new NumericUpDown();
            btnThemGio = new Button();
            grpGioHang = new GroupBox();
            dgvGioHang = new DataGridView();
            btnXoaDong = new Button();
            btnXoaGio = new Button();
            grpThanhToan = new GroupBox();
            lblVoucher = new Label();
            txtVoucher = new TextBox();
            btnApDung = new Button();
            lblVoucherThongBao = new Label();
            lblTongTienText = new Label();
            lblTongTien = new Label();
            lblGiamGiaText = new Label();
            lblGiamGia = new Label();
            lblPhaiTraText = new Label();
            lblPhaiTra = new Label();
            btnThanhToan = new Button();
            grpThuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            grpGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
            grpThanhToan.SuspendLayout();
            SuspendLayout();
            // 
            // grpThuoc
            // 
            grpThuoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpThuoc.Controls.Add(txtTimKiem);
            grpThuoc.Controls.Add(btnTimKiem);
            grpThuoc.Controls.Add(dgvThuoc);
            grpThuoc.Controls.Add(lblSoLuong);
            grpThuoc.Controls.Add(nudSoLuong);
            grpThuoc.Controls.Add(btnThemGio);
            grpThuoc.Location = new Point(12, 12);
            grpThuoc.Name = "grpThuoc";
            grpThuoc.Size = new Size(560, 656);
            grpThuoc.TabIndex = 0;
            grpThuoc.TabStop = false;
            grpThuoc.Text = "Tìm kiếm thuốc (tên / mã / barcode / hoạt chất)";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(12, 28);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(380, 23);
            txtTimKiem.TabIndex = 0;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(400, 26);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(90, 28);
            btnTimKiem.TabIndex = 1;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // dgvThuoc
            // 
            dgvThuoc.AllowUserToAddRows = false;
            dgvThuoc.AllowUserToDeleteRows = false;
            dgvThuoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThuoc.Location = new Point(12, 64);
            dgvThuoc.MultiSelect = false;
            dgvThuoc.Name = "dgvThuoc";
            dgvThuoc.ReadOnly = true;
            dgvThuoc.RowHeadersVisible = false;
            dgvThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThuoc.Size = new Size(536, 520);
            dgvThuoc.TabIndex = 2;
            dgvThuoc.CellDoubleClick += dgvThuoc_CellDoubleClick;
            // 
            // lblSoLuong
            // 
            lblSoLuong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSoLuong.Location = new Point(12, 604);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(70, 23);
            lblSoLuong.TabIndex = 3;
            lblSoLuong.Text = "Số lượng:";
            lblSoLuong.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudSoLuong
            // 
            nudSoLuong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            nudSoLuong.Location = new Point(88, 604);
            nudSoLuong.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(80, 23);
            nudSoLuong.TabIndex = 4;
            nudSoLuong.TextAlign = HorizontalAlignment.Right;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnThemGio
            // 
            btnThemGio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThemGio.Location = new Point(184, 600);
            btnThemGio.Name = "btnThemGio";
            btnThemGio.Size = new Size(150, 32);
            btnThemGio.TabIndex = 5;
            btnThemGio.Text = "Thêm vào giỏ";
            btnThemGio.UseVisualStyleBackColor = true;
            btnThemGio.Click += btnThemGio_Click;
            // 
            // grpGioHang
            // 
            grpGioHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpGioHang.Controls.Add(dgvGioHang);
            grpGioHang.Controls.Add(btnXoaDong);
            grpGioHang.Controls.Add(btnXoaGio);
            grpGioHang.Location = new Point(584, 12);
            grpGioHang.Name = "grpGioHang";
            grpGioHang.Size = new Size(504, 436);
            grpGioHang.TabIndex = 1;
            grpGioHang.TabStop = false;
            grpGioHang.Text = "Giỏ hàng (có thể sửa số lượng trực tiếp)";
            // 
            // dgvGioHang
            // 
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGioHang.Location = new Point(12, 24);
            dgvGioHang.MultiSelect = false;
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.Size = new Size(480, 350);
            dgvGioHang.TabIndex = 0;
            dgvGioHang.CellEndEdit += dgvGioHang_CellEndEdit;
            dgvGioHang.CellValidating += dgvGioHang_CellValidating;
            dgvGioHang.DataError += dgvGioHang_DataError;
            // 
            // btnXoaDong
            // 
            btnXoaDong.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaDong.Location = new Point(12, 390);
            btnXoaDong.Name = "btnXoaDong";
            btnXoaDong.Size = new Size(110, 30);
            btnXoaDong.TabIndex = 1;
            btnXoaDong.Text = "Xóa dòng";
            btnXoaDong.UseVisualStyleBackColor = true;
            btnXoaDong.Click += btnXoaDong_Click;
            // 
            // btnXoaGio
            // 
            btnXoaGio.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaGio.Location = new Point(130, 390);
            btnXoaGio.Name = "btnXoaGio";
            btnXoaGio.Size = new Size(110, 30);
            btnXoaGio.TabIndex = 2;
            btnXoaGio.Text = "Xóa giỏ";
            btnXoaGio.UseVisualStyleBackColor = true;
            btnXoaGio.Click += btnXoaGio_Click;
            // 
            // grpThanhToan
            // 
            grpThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpThanhToan.Controls.Add(lblVoucher);
            grpThanhToan.Controls.Add(txtVoucher);
            grpThanhToan.Controls.Add(btnApDung);
            grpThanhToan.Controls.Add(lblVoucherThongBao);
            grpThanhToan.Controls.Add(lblTongTienText);
            grpThanhToan.Controls.Add(lblTongTien);
            grpThanhToan.Controls.Add(lblGiamGiaText);
            grpThanhToan.Controls.Add(lblGiamGia);
            grpThanhToan.Controls.Add(lblPhaiTraText);
            grpThanhToan.Controls.Add(lblPhaiTra);
            grpThanhToan.Controls.Add(btnThanhToan);
            grpThanhToan.Location = new Point(584, 456);
            grpThanhToan.Name = "grpThanhToan";
            grpThanhToan.Size = new Size(504, 212);
            grpThanhToan.TabIndex = 2;
            grpThanhToan.TabStop = false;
            grpThanhToan.Text = "Thanh toán";
            // 
            // lblVoucher
            // 
            lblVoucher.Location = new Point(12, 30);
            lblVoucher.Name = "lblVoucher";
            lblVoucher.Size = new Size(70, 23);
            lblVoucher.TabIndex = 0;
            lblVoucher.Text = "Voucher:";
            lblVoucher.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtVoucher
            // 
            txtVoucher.CharacterCasing = CharacterCasing.Upper;
            txtVoucher.Location = new Point(88, 30);
            txtVoucher.MaxLength = 20;
            txtVoucher.Name = "txtVoucher";
            txtVoucher.Size = new Size(150, 23);
            txtVoucher.TabIndex = 1;
            txtVoucher.TextChanged += txtVoucher_TextChanged;
            // 
            // btnApDung
            // 
            btnApDung.Location = new Point(246, 28);
            btnApDung.Name = "btnApDung";
            btnApDung.Size = new Size(90, 28);
            btnApDung.TabIndex = 2;
            btnApDung.Text = "Áp dụng";
            btnApDung.UseVisualStyleBackColor = true;
            btnApDung.Click += btnApDung_Click;
            // 
            // lblVoucherThongBao
            // 
            lblVoucherThongBao.Location = new Point(12, 60);
            lblVoucherThongBao.Name = "lblVoucherThongBao";
            lblVoucherThongBao.Size = new Size(480, 20);
            lblVoucherThongBao.TabIndex = 3;
            lblVoucherThongBao.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTongTienText
            // 
            lblTongTienText.Location = new Point(12, 86);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Size = new Size(140, 23);
            lblTongTienText.TabIndex = 4;
            lblTongTienText.Text = "Tổng tiền hàng:";
            lblTongTienText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTongTien
            // 
            lblTongTien.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongTien.Location = new Point(242, 86);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(250, 23);
            lblTongTien.TabIndex = 5;
            lblTongTien.Text = "0 đ";
            lblTongTien.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblGiamGiaText
            // 
            lblGiamGiaText.Location = new Point(12, 112);
            lblGiamGiaText.Name = "lblGiamGiaText";
            lblGiamGiaText.Size = new Size(140, 23);
            lblGiamGiaText.TabIndex = 6;
            lblGiamGiaText.Text = "Giảm giá (voucher):";
            lblGiamGiaText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGiamGia
            // 
            lblGiamGia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGiamGia.ForeColor = Color.Firebrick;
            lblGiamGia.Location = new Point(242, 112);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new Size(250, 23);
            lblGiamGia.TabIndex = 7;
            lblGiamGia.Text = "- 0 đ";
            lblGiamGia.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPhaiTraText
            // 
            lblPhaiTraText.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            lblPhaiTraText.Location = new Point(12, 142);
            lblPhaiTraText.Name = "lblPhaiTraText";
            lblPhaiTraText.Size = new Size(160, 28);
            lblPhaiTraText.TabIndex = 8;
            lblPhaiTraText.Text = "PHẢI THANH TOÁN:";
            lblPhaiTraText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPhaiTra
            // 
            lblPhaiTra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPhaiTra.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblPhaiTra.ForeColor = Color.DarkGreen;
            lblPhaiTra.Location = new Point(182, 140);
            lblPhaiTra.Name = "lblPhaiTra";
            lblPhaiTra.Size = new Size(310, 32);
            lblPhaiTra.TabIndex = 9;
            lblPhaiTra.Text = "0 đ";
            lblPhaiTra.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnThanhToan
            // 
            btnThanhToan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnThanhToan.BackColor = Color.SeaGreen;
            btnThanhToan.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.Location = new Point(262, 172);
            btnThanhToan.Name = "btnThanhToan";
            btnThanhToan.Size = new Size(230, 34);
            btnThanhToan.TabIndex = 10;
            btnThanhToan.Text = "THANH TOÁN";
            btnThanhToan.UseVisualStyleBackColor = false;
            btnThanhToan.Click += btnThanhToan_Click;
            // 
            // frmBanHang
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(grpThanhToan);
            Controls.Add(grpGioHang);
            Controls.Add(grpThuoc);
            Name = "frmBanHang";
            Text = "Bán hàng";
            Load += frmBanHang_Load;
            grpThuoc.ResumeLayout(false);
            grpThuoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            grpGioHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
            grpThanhToan.ResumeLayout(false);
            grpThanhToan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpThuoc;
        private TextBox txtTimKiem;
        private Button btnTimKiem;
        private DataGridView dgvThuoc;
        private Label lblSoLuong;
        private NumericUpDown nudSoLuong;
        private Button btnThemGio;
        private GroupBox grpGioHang;
        private DataGridView dgvGioHang;
        private Button btnXoaDong;
        private Button btnXoaGio;
        private GroupBox grpThanhToan;
        private Label lblVoucher;
        private TextBox txtVoucher;
        private Button btnApDung;
        private Label lblVoucherThongBao;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Label lblGiamGiaText;
        private Label lblGiamGia;
        private Label lblPhaiTraText;
        private Label lblPhaiTra;
        private Button btnThanhToan;
    }
}
