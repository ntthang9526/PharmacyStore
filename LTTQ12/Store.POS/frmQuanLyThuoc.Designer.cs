namespace Store.POS
{
    partial class frmQuanLyThuoc
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtMaThuoc = new TextBox();
            txtTenThuoc = new TextBox();
            txtDonGia = new TextBox();
            txtSoLuongTon = new TextBox();
            cboNhomThuoc = new ComboBox();
            cboDonViTinh = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLuu = new Button();
            btnBoQua = new Button();
            dgvThuoc = new DataGridView();
            label7 = new Label();
            groupBox1 = new GroupBox();
            cboMaThuoc = new ComboBox();
            btnTimKiem = new Button();
            label8 = new Label();
            btnThoat = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvThuoc).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 83);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã thuốc";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(348, 83);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 1;
            label2.Text = "Tên thuốc";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 128);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 2;
            label3.Text = "Nhóm thuốc";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(193, 128);
            label4.Name = "label4";
            label4.Size = new Size(81, 20);
            label4.TabIndex = 3;
            label4.Text = "Đơn vị tính";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(348, 128);
            label5.Name = "label5";
            label5.Size = new Size(62, 20);
            label5.TabIndex = 4;
            label5.Text = "Đơn giá";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(503, 128);
            label6.Name = "label6";
            label6.Size = new Size(95, 20);
            label6.TabIndex = 5;
            label6.Text = "Số lượng tồn";
            // 
            // txtMaThuoc
            // 
            txtMaThuoc.Location = new Point(91, 80);
            txtMaThuoc.Name = "txtMaThuoc";
            txtMaThuoc.Size = new Size(151, 27);
            txtMaThuoc.TabIndex = 7;
            // 
            // txtTenThuoc
            // 
            txtTenThuoc.Location = new Point(427, 80);
            txtTenThuoc.Name = "txtTenThuoc";
            txtTenThuoc.Size = new Size(171, 27);
            txtTenThuoc.TabIndex = 8;
            // 
            // txtDonGia
            // 
            txtDonGia.Location = new Point(348, 162);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new Size(125, 27);
            txtDonGia.TabIndex = 9;
            // 
            // txtSoLuongTon
            // 
            txtSoLuongTon.Location = new Point(503, 160);
            txtSoLuongTon.Name = "txtSoLuongTon";
            txtSoLuongTon.Size = new Size(125, 27);
            txtSoLuongTon.TabIndex = 10;
            // 
            // cboNhomThuoc
            // 
            cboNhomThuoc.FormattingEnabled = true;
            cboNhomThuoc.Location = new Point(14, 160);
            cboNhomThuoc.Name = "cboNhomThuoc";
            cboNhomThuoc.Size = new Size(151, 28);
            cboNhomThuoc.TabIndex = 11;
            // 
            // cboDonViTinh
            // 
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Location = new Point(193, 161);
            cboDonViTinh.Name = "cboDonViTinh";
            cboDonViTinh.Size = new Size(125, 28);
            cboDonViTinh.TabIndex = 12;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(14, 409);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 13;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(135, 409);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 14;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(260, 409);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 15;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(379, 409);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(94, 29);
            btnLuu.TabIndex = 16;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnBoQua
            // 
            btnBoQua.Location = new Point(504, 409);
            btnBoQua.Name = "btnBoQua";
            btnBoQua.Size = new Size(94, 29);
            btnBoQua.TabIndex = 17;
            btnBoQua.Text = "Bỏ qua";
            btnBoQua.UseVisualStyleBackColor = true;
            btnBoQua.Click += btnBoQua_Click;
            // 
            // dgvThuoc
            // 
            dgvThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThuoc.Location = new Point(12, 213);
            dgvThuoc.Name = "dgvThuoc";
            dgvThuoc.ReadOnly = true;
            dgvThuoc.RowHeadersWidth = 51;
            dgvThuoc.Size = new Size(616, 170);
            dgvThuoc.TabIndex = 18;
            dgvThuoc.CellClick += dgvThuoc_CellClick;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Snap ITC", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.Desktop;
            label7.Location = new Point(150, 9);
            label7.Name = "label7";
            label7.Size = new Size(402, 48);
            label7.TabIndex = 19;
            label7.Text = "QUẢN LÝ THUỐC";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboMaThuoc);
            groupBox1.Controls.Add(btnTimKiem);
            groupBox1.Controls.Add(label8);
            groupBox1.Location = new Point(14, 460);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(396, 73);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm thuốc";
            // 
            // cboMaThuoc
            // 
            cboMaThuoc.FormattingEnabled = true;
            cboMaThuoc.Location = new Point(92, 26);
            cboMaThuoc.Name = "cboMaThuoc";
            cboMaThuoc.Size = new Size(151, 28);
            cboMaThuoc.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(263, 25);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 30);
            label8.Name = "label8";
            label8.Size = new Size(71, 20);
            label8.TabIndex = 0;
            label8.Text = "Mã thuốc";
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(504, 504);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 21;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // frmQuanLyThuoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CornflowerBlue;
            ClientSize = new Size(657, 556);
            Controls.Add(btnThoat);
            Controls.Add(groupBox1);
            Controls.Add(label7);
            Controls.Add(dgvThuoc);
            Controls.Add(btnBoQua);
            Controls.Add(btnLuu);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(cboDonViTinh);
            Controls.Add(cboNhomThuoc);
            Controls.Add(txtSoLuongTon);
            Controls.Add(txtDonGia);
            Controls.Add(txtTenThuoc);
            Controls.Add(txtMaThuoc);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmQuanLyThuoc";
            Text = "frmQuanLyThuoc";
            Load += frmQuanLyThuoc_Load;
            ((System.ComponentModel.ISupportInitialize)dgvThuoc).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtMaThuoc;
        private TextBox txtTenThuoc;
        private TextBox txtDonGia;
        private TextBox txtSoLuongTon;
        private ComboBox cboNhomThuoc;
        private ComboBox cboDonViTinh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLuu;
        private Button btnBoQua;
        private DataGridView dgvThuoc;
        private Label label7;
        private GroupBox groupBox1;
        private ComboBox cboMaThuoc;
        private Button btnTimKiem;
        private Label label8;
        private Button btnThoat;
    }
}