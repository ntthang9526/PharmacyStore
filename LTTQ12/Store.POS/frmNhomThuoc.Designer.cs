namespace Store.POS
{
    partial class frmNhomThuoc
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
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnThoat = new Button();
            txtTenNhom = new TextBox();
            txtMaNhom = new TextBox();
            dgvNhomThuoc = new DataGridView();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvNhomThuoc).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 80);
            label1.Name = "label1";
            label1.Size = new Size(120, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã nhóm thuốc";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(326, 80);
            label2.Name = "label2";
            label2.Size = new Size(123, 20);
            label2.TabIndex = 1;
            label2.Text = "Tên nhóm thuốc";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(28, 329);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(160, 329);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = SystemColors.ControlLightLight;
            btnXoa.Location = new Point(290, 329);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(425, 329);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(563, 329);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 29);
            btnThoat.TabIndex = 6;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // txtTenNhom
            // 
            txtTenNhom.Location = new Point(455, 77);
            txtTenNhom.Name = "txtTenNhom";
            txtTenNhom.Size = new Size(174, 27);
            txtTenNhom.TabIndex = 8;
            // 
            // txtMaNhom
            // 
            txtMaNhom.Location = new Point(154, 77);
            txtMaNhom.Name = "txtMaNhom";
            txtMaNhom.Size = new Size(125, 27);
            txtMaNhom.TabIndex = 9;
            // 
            // dgvNhomThuoc
            // 
            dgvNhomThuoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhomThuoc.Location = new Point(28, 124);
            dgvNhomThuoc.Name = "dgvNhomThuoc";
            dgvNhomThuoc.ReadOnly = true;
            dgvNhomThuoc.RowHeadersWidth = 51;
            dgvNhomThuoc.Size = new Size(629, 183);
            dgvNhomThuoc.TabIndex = 10;
            dgvNhomThuoc.CellClick += dgvNhomThuoc_CellClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Snap ITC", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkBlue;
            label3.Location = new Point(204, 9);
            label3.Name = "label3";
            label3.Size = new Size(274, 39);
            label3.TabIndex = 11;
            label3.Text = "NHÓM THUỐC";
            // 
            // frmNhomThuoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOrange;
            ClientSize = new Size(696, 370);
            Controls.Add(label3);
            Controls.Add(dgvNhomThuoc);
            Controls.Add(txtMaNhom);
            Controls.Add(txtTenNhom);
            Controls.Add(btnThoat);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmNhomThuoc";
            Text = "frmNhomThuoc";
            Load += frmNhomThuoc_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNhomThuoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnThoat;
        private TextBox txtTenNhom;
        private TextBox txtMaNhom;
        private DataGridView dgvNhomThuoc;
        private Label label3;
    }
}