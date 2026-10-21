namespace Store.POS
{
    partial class frmKhachHang
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
            txtMaKH = new TextBox();
            txtTenKH = new TextBox();
            dgvKhachHang = new DataGridView();
            label3 = new Label();
            label4 = new Label();
            txtDiem = new TextBox();
            txtDienThoai = new TextBox();
            label5 = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamLai = new Button();
            btnThoat = new Button();
            groupBox1 = new GroupBox();
            cboMaKH = new ComboBox();
            label6 = new Label();
            btnTimKiem = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 66);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã khách hàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 115);
            label2.Name = "label2";
            label2.Size = new Size(111, 20);
            label2.TabIndex = 1;
            label2.Text = "Tên khách hàng";
            // 
            // txtMaKH
            // 
            txtMaKH.Location = new Point(142, 63);
            txtMaKH.Name = "txtMaKH";
            txtMaKH.Size = new Size(125, 27);
            txtMaKH.TabIndex = 2;
            // 
            // txtTenKH
            // 
            txtTenKH.Location = new Point(142, 112);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(125, 27);
            txtTenKH.TabIndex = 3;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachHang.Location = new Point(27, 169);
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.Size = new Size(499, 184);
            dgvKhachHang.TabIndex = 4;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(362, 66);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 5;
            label3.Text = "Số điện thoại";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(362, 115);
            label4.Name = "label4";
            label4.Size = new Size(96, 20);
            label4.TabIndex = 6;
            label4.Text = "Điểm tích lũy";
            // 
            // txtDiem
            // 
            txtDiem.Location = new Point(464, 112);
            txtDiem.Name = "txtDiem";
            txtDiem.Size = new Size(125, 27);
            txtDiem.TabIndex = 7;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(464, 59);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(125, 27);
            txtDienThoai.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Snap ITC", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Crimson;
            label5.Location = new Point(182, 9);
            label5.Name = "label5";
            label5.Size = new Size(273, 39);
            label5.TabIndex = 9;
            label5.Text = "KHÁCH HÀNG";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(548, 169);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 10;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(548, 222);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 11;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(548, 277);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 12;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamLai
            // 
            btnLamLai.Location = new Point(548, 324);
            btnLamLai.Name = "btnLamLai";
            btnLamLai.Size = new Size(94, 29);
            btnLamLai.TabIndex = 13;
            btnLamLai.Text = "Làm lại";
            btnLamLai.UseVisualStyleBackColor = true;
            btnLamLai.Click += btnLamLai_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(548, 409);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 14;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboMaKH);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(btnTimKiem);
            groupBox1.Location = new Point(27, 371);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(432, 67);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tìm kiếm khách hàng";
            // 
            // cboMaKH
            // 
            cboMaKH.FormattingEnabled = true;
            cboMaKH.Location = new Point(130, 25);
            cboMaKH.Name = "cboMaKH";
            cboMaKH.Size = new Size(151, 28);
            cboMaKH.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 28);
            label6.Name = "label6";
            label6.Size = new Size(109, 20);
            label6.TabIndex = 1;
            label6.Text = "Mã khách hàng";
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(303, 25);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 0;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // frmKhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cyan;
            ClientSize = new Size(667, 450);
            Controls.Add(groupBox1);
            Controls.Add(btnThoat);
            Controls.Add(btnLamLai);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(label5);
            Controls.Add(txtDienThoai);
            Controls.Add(txtDiem);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dgvKhachHang);
            Controls.Add(txtTenKH);
            Controls.Add(txtMaKH);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmKhachHang";
            Text = "frmKhachHang";
            Load += frmKhachHang_Load;
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtMaKH;
        private TextBox txtTenKH;
        private DataGridView dgvKhachHang;
        private Label label3;
        private Label label4;
        private TextBox txtDiem;
        private TextBox txtDienThoai;
        private Label label5;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamLai;
        private Button btnThoat;
        private GroupBox groupBox1;
        private ComboBox cboMaKH;
        private Label label6;
        private Button btnTimKiem;
    }
}