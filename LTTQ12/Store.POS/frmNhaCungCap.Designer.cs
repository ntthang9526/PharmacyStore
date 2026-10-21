namespace Store.POS
{
    partial class frmNhaCungCap
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
            txtMaNCC = new TextBox();
            txtTenNCC = new TextBox();
            txtDiaChi = new TextBox();
            txtDienThoai = new TextBox();
            dgvNhaCungCap = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnThoat = new Button();
            label6 = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            cboMaNCC = new ComboBox();
            btnTimKiem = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNhaCungCap).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Snap ITC", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.CornflowerBlue;
            label1.Location = new Point(188, 9);
            label1.Name = "label1";
            label1.Size = new Size(308, 39);
            label1.TabIndex = 0;
            label1.Text = "NHÀ CUNG CẤP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 67);
            label2.Name = "label2";
            label2.Size = new Size(122, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã nhà cung cấp";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 119);
            label3.Name = "label3";
            label3.Size = new Size(124, 20);
            label3.TabIndex = 2;
            label3.Text = "Tên nhà cung cấp";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(370, 67);
            label4.Name = "label4";
            label4.Size = new Size(78, 20);
            label4.TabIndex = 3;
            label4.Text = "Điện thoại";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(350, 119);
            label5.Name = "label5";
            label5.Size = new Size(0, 20);
            label5.TabIndex = 4;
            // 
            // txtMaNCC
            // 
            txtMaNCC.Location = new Point(155, 60);
            txtMaNCC.Name = "txtMaNCC";
            txtMaNCC.Size = new Size(167, 27);
            txtMaNCC.TabIndex = 5;
            // 
            // txtTenNCC
            // 
            txtTenNCC.Location = new Point(155, 116);
            txtTenNCC.Name = "txtTenNCC";
            txtTenNCC.Size = new Size(167, 27);
            txtTenNCC.TabIndex = 6;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(454, 116);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(144, 27);
            txtDiaChi.TabIndex = 7;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(454, 64);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(144, 27);
            txtDienThoai.TabIndex = 8;
            // 
            // dgvNhaCungCap
            // 
            dgvNhaCungCap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhaCungCap.Location = new Point(27, 178);
            dgvNhaCungCap.Name = "dgvNhaCungCap";
            dgvNhaCungCap.ReadOnly = true;
            dgvNhaCungCap.RowHeadersWidth = 51;
            dgvNhaCungCap.Size = new Size(571, 192);
            dgvNhaCungCap.TabIndex = 9;
            dgvNhaCungCap.CellClick += dgvNhaCungCap_CellClick;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(639, 178);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 10;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(639, 235);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 11;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(639, 288);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(639, 341);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 13;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(639, 420);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 14;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(370, 119);
            label6.Name = "label6";
            label6.Size = new Size(55, 20);
            label6.TabIndex = 15;
            label6.Text = "Địa chỉ";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(cboMaNCC);
            groupBox1.Controls.Add(btnTimKiem);
            groupBox1.Location = new Point(27, 389);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(428, 73);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm nhà cung cấp";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 35);
            label7.Name = "label7";
            label7.Size = new Size(122, 20);
            label7.TabIndex = 2;
            label7.Text = "Mã nhà cung cấp";
            // 
            // cboMaNCC
            // 
            cboMaNCC.FormattingEnabled = true;
            cboMaNCC.Location = new Point(147, 31);
            cboMaNCC.Name = "cboMaNCC";
            cboMaNCC.Size = new Size(159, 28);
            cboMaNCC.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(323, 31);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 0;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // frmNhaCungCap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(758, 461);
            Controls.Add(groupBox1);
            Controls.Add(label6);
            Controls.Add(btnThoat);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvNhaCungCap);
            Controls.Add(txtDienThoai);
            Controls.Add(txtDiaChi);
            Controls.Add(txtTenNCC);
            Controls.Add(txtMaNCC);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmNhaCungCap";
            Text = "Nhà cung cấp";
            Load += frmNhaCungCap_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNhaCungCap).EndInit();
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
        private TextBox txtMaNCC;
        private TextBox txtTenNCC;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private DataGridView dgvNhaCungCap;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnThoat;
        private Label label6;
        private GroupBox groupBox1;
        private Label label7;
        private ComboBox cboMaNCC;
        private Button btnTimKiem;
    }
}