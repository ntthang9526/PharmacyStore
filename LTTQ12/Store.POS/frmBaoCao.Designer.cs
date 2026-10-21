namespace Store.POS
{
    partial class frmBaoCao
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
            grpLoc = new GroupBox();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            btnXem = new Button();
            grpTongHop = new GroupBox();
            lblSoHDText = new Label();
            lblSoHD = new Label();
            lblTongTienText = new Label();
            lblTongTien = new Label();
            lblGiamGiaText = new Label();
            lblGiamGia = new Label();
            lblDoanhThuText = new Label();
            lblDoanhThu = new Label();
            grpTheoNgay = new GroupBox();
            dgvTheoNgay = new DataGridView();
            grpHoaDon = new GroupBox();
            dgvHoaDon = new DataGridView();
            grpLoc.SuspendLayout();
            grpTongHop.SuspendLayout();
            grpTheoNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTheoNgay).BeginInit();
            grpHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            SuspendLayout();
            // 
            // grpLoc
            // 
            grpLoc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpLoc.Controls.Add(lblTuNgay);
            grpLoc.Controls.Add(dtpTuNgay);
            grpLoc.Controls.Add(lblDenNgay);
            grpLoc.Controls.Add(dtpDenNgay);
            grpLoc.Controls.Add(btnXem);
            grpLoc.Location = new Point(12, 12);
            grpLoc.Name = "grpLoc";
            grpLoc.Size = new Size(1076, 62);
            grpLoc.TabIndex = 0;
            grpLoc.TabStop = false;
            grpLoc.Text = "Lọc theo thời gian";
            // 
            // lblTuNgay
            // 
            lblTuNgay.Location = new Point(12, 28);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(65, 23);
            lblTuNgay.TabIndex = 0;
            lblTuNgay.Text = "Từ ngày:";
            lblTuNgay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Short;
            dtpTuNgay.Location = new Point(80, 28);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(130, 23);
            dtpTuNgay.TabIndex = 1;
            // 
            // lblDenNgay
            // 
            lblDenNgay.Location = new Point(235, 28);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(70, 23);
            lblDenNgay.TabIndex = 2;
            lblDenNgay.Text = "Đến ngày:";
            lblDenNgay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Short;
            dtpDenNgay.Location = new Point(310, 28);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(130, 23);
            dtpDenNgay.TabIndex = 3;
            // 
            // btnXem
            // 
            btnXem.Location = new Point(465, 24);
            btnXem.Name = "btnXem";
            btnXem.Size = new Size(120, 30);
            btnXem.TabIndex = 4;
            btnXem.Text = "Xem báo cáo";
            btnXem.UseVisualStyleBackColor = true;
            btnXem.Click += btnXem_Click;
            // 
            // grpTongHop
            // 
            grpTongHop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpTongHop.Controls.Add(lblSoHDText);
            grpTongHop.Controls.Add(lblSoHD);
            grpTongHop.Controls.Add(lblTongTienText);
            grpTongHop.Controls.Add(lblTongTien);
            grpTongHop.Controls.Add(lblGiamGiaText);
            grpTongHop.Controls.Add(lblGiamGia);
            grpTongHop.Controls.Add(lblDoanhThuText);
            grpTongHop.Controls.Add(lblDoanhThu);
            grpTongHop.Location = new Point(12, 80);
            grpTongHop.Name = "grpTongHop";
            grpTongHop.Size = new Size(1076, 62);
            grpTongHop.TabIndex = 1;
            grpTongHop.TabStop = false;
            grpTongHop.Text = "Tổng hợp";
            // 
            // lblSoHDText
            // 
            lblSoHDText.Location = new Point(12, 26);
            lblSoHDText.Name = "lblSoHDText";
            lblSoHDText.Size = new Size(85, 23);
            lblSoHDText.TabIndex = 0;
            lblSoHDText.Text = "Số hóa đơn:";
            lblSoHDText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSoHD
            // 
            lblSoHD.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblSoHD.Location = new Point(100, 26);
            lblSoHD.Name = "lblSoHD";
            lblSoHD.Size = new Size(70, 23);
            lblSoHD.TabIndex = 1;
            lblSoHD.Text = "0";
            lblSoHD.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTongTienText
            // 
            lblTongTienText.Location = new Point(200, 26);
            lblTongTienText.Name = "lblTongTienText";
            lblTongTienText.Size = new Size(105, 23);
            lblTongTienText.TabIndex = 2;
            lblTongTienText.Text = "Tổng tiền hàng:";
            lblTongTienText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTongTien
            // 
            lblTongTien.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblTongTien.Location = new Point(308, 26);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(150, 23);
            lblTongTien.TabIndex = 3;
            lblTongTien.Text = "0 đ";
            lblTongTien.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGiamGiaText
            // 
            lblGiamGiaText.Location = new Point(480, 26);
            lblGiamGiaText.Name = "lblGiamGiaText";
            lblGiamGiaText.Size = new Size(70, 23);
            lblGiamGiaText.TabIndex = 4;
            lblGiamGiaText.Text = "Giảm giá:";
            lblGiamGiaText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGiamGia
            // 
            lblGiamGia.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblGiamGia.ForeColor = Color.Firebrick;
            lblGiamGia.Location = new Point(552, 26);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new Size(140, 23);
            lblGiamGia.TabIndex = 5;
            lblGiamGia.Text = "0 đ";
            lblGiamGia.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDoanhThuText
            // 
            lblDoanhThuText.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblDoanhThuText.Location = new Point(720, 26);
            lblDoanhThuText.Name = "lblDoanhThuText";
            lblDoanhThuText.Size = new Size(150, 23);
            lblDoanhThuText.TabIndex = 6;
            lblDoanhThuText.Text = "DOANH THU THỰC:";
            lblDoanhThuText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDoanhThu
            // 
            lblDoanhThu.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDoanhThu.ForeColor = Color.DarkGreen;
            lblDoanhThu.Location = new Point(872, 24);
            lblDoanhThu.Name = "lblDoanhThu";
            lblDoanhThu.Size = new Size(190, 27);
            lblDoanhThu.TabIndex = 7;
            lblDoanhThu.Text = "0 đ";
            lblDoanhThu.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpTheoNgay
            // 
            grpTheoNgay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpTheoNgay.Controls.Add(dgvTheoNgay);
            grpTheoNgay.Location = new Point(12, 150);
            grpTheoNgay.Name = "grpTheoNgay";
            grpTheoNgay.Size = new Size(460, 518);
            grpTheoNgay.TabIndex = 2;
            grpTheoNgay.TabStop = false;
            grpTheoNgay.Text = "Doanh thu theo ngày";
            // 
            // dgvTheoNgay
            // 
            dgvTheoNgay.AllowUserToAddRows = false;
            dgvTheoNgay.AllowUserToDeleteRows = false;
            dgvTheoNgay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTheoNgay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTheoNgay.Dock = DockStyle.Fill;
            dgvTheoNgay.Name = "dgvTheoNgay";
            dgvTheoNgay.ReadOnly = true;
            dgvTheoNgay.RowHeadersVisible = false;
            dgvTheoNgay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTheoNgay.TabIndex = 0;
            // 
            // grpHoaDon
            // 
            grpHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpHoaDon.Controls.Add(dgvHoaDon);
            grpHoaDon.Location = new Point(480, 150);
            grpHoaDon.Name = "grpHoaDon";
            grpHoaDon.Size = new Size(608, 518);
            grpHoaDon.TabIndex = 3;
            grpHoaDon.TabStop = false;
            grpHoaDon.Text = "Danh sách hóa đơn";
            // 
            // dgvHoaDon
            // 
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon.Dock = DockStyle.Fill;
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.TabIndex = 0;
            // 
            // frmBaoCao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(grpHoaDon);
            Controls.Add(grpTheoNgay);
            Controls.Add(grpTongHop);
            Controls.Add(grpLoc);
            Name = "frmBaoCao";
            Text = "Báo cáo doanh thu";
            Load += frmBaoCao_Load;
            grpLoc.ResumeLayout(false);
            grpTongHop.ResumeLayout(false);
            grpTheoNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTheoNgay).EndInit();
            grpHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpLoc;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Button btnXem;
        private GroupBox grpTongHop;
        private Label lblSoHDText;
        private Label lblSoHD;
        private Label lblTongTienText;
        private Label lblTongTien;
        private Label lblGiamGiaText;
        private Label lblGiamGia;
        private Label lblDoanhThuText;
        private Label lblDoanhThu;
        private GroupBox grpTheoNgay;
        private DataGridView dgvTheoNgay;
        private GroupBox grpHoaDon;
        private DataGridView dgvHoaDon;
    }
}
