namespace Store.POS
{
    partial class frmLogin
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
            lblTieuDe = new Label();
            lblTaiKhoan = new Label();
            lblMatKhau = new Label();
            txtTaiKhoan = new TextBox();
            txtMatKhau = new TextBox();
            chkHienMatKhau = new CheckBox();
            btnDangNhap = new Button();
            btnThoat = new Button();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTieuDe.Location = new Point(12, 15);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(376, 40);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTaiKhoan
            // 
            lblTaiKhoan.Location = new Point(40, 80);
            lblTaiKhoan.Name = "lblTaiKhoan";
            lblTaiKhoan.Size = new Size(80, 23);
            lblTaiKhoan.TabIndex = 1;
            lblTaiKhoan.Text = "Tài khoản:";
            lblTaiKhoan.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Location = new Point(40, 118);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(80, 23);
            lblMatKhau.TabIndex = 3;
            lblMatKhau.Text = "Mật khẩu:";
            lblMatKhau.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.Location = new Point(125, 80);
            txtTaiKhoan.MaxLength = 50;
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.Size = new Size(235, 23);
            txtTaiKhoan.TabIndex = 2;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(125, 118);
            txtMatKhau.MaxLength = 255;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(235, 23);
            txtMatKhau.TabIndex = 4;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Location = new Point(125, 150);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(105, 19);
            chkHienMatKhau.TabIndex = 5;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new Point(125, 190);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(110, 34);
            btnDangNhap.TabIndex = 6;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnThoat
            // 
            btnThoat.Location = new Point(250, 190);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(110, 34);
            btnThoat.TabIndex = 7;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // frmLogin
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnThoat;
            ClientSize = new Size(400, 250);
            Controls.Add(btnThoat);
            Controls.Add(btnDangNhap);
            Controls.Add(chkHienMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTaiKhoan);
            Controls.Add(lblMatKhau);
            Controls.Add(lblTaiKhoan);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - PharmacyStore";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblTaiKhoan;
        private Label lblMatKhau;
        private TextBox txtTaiKhoan;
        private TextBox txtMatKhau;
        private CheckBox chkHienMatKhau;
        private Button btnDangNhap;
        private Button btnThoat;
    }
}
