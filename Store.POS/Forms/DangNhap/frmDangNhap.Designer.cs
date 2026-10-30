namespace Store.POS
{
    partial class frmDangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTieuDeDangNhap = new Label();
            lblTaiKhoan = new Label();
            lblMatKhau = new Label();
            txtBoxTaiKhoan = new TextBox();
            txtBoxMatKhau = new TextBox();
            chkHienMatKhau = new CheckBox();
            btnDangNhap = new Button();
            SuspendLayout();
            // 
            // lblTieuDeDangNhap
            // 
            lblTieuDeDangNhap.AutoSize = true;
            lblTieuDeDangNhap.Font = new Font("Microsoft Sans Serif", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDeDangNhap.ForeColor = Color.FromArgb(15, 118, 110);
            lblTieuDeDangNhap.Location = new Point(185, 19);
            lblTieuDeDangNhap.Name = "lblTieuDeDangNhap";
            lblTieuDeDangNhap.Size = new Size(230, 39);
            lblTieuDeDangNhap.TabIndex = 0;
            lblTieuDeDangNhap.Text = "ĐĂNG NHẬP";
            lblTieuDeDangNhap.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTaiKhoan
            // 
            lblTaiKhoan.AutoSize = true;
            lblTaiKhoan.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTaiKhoan.Location = new Point(91, 83);
            lblTaiKhoan.Name = "lblTaiKhoan";
            lblTaiKhoan.Size = new Size(123, 31);
            lblTaiKhoan.TabIndex = 1;
            lblTaiKhoan.Text = "Tài khoản:";
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMatKhau.Location = new Point(89, 175);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(122, 31);
            lblMatKhau.TabIndex = 2;
            lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtBoxTaiKhoan
            // 
            txtBoxTaiKhoan.BorderStyle=BorderStyle.FixedSingle;
            txtBoxTaiKhoan.Font = new Font("Segoe UI", 14F);
            txtBoxTaiKhoan.Location = new Point(95, 117);
            txtBoxTaiKhoan.Name = "txtBoxTaiKhoan";
            txtBoxTaiKhoan.Size = new Size(400, 39);
            txtBoxTaiKhoan.TabIndex = 3;
            // 
            // txtBoxMatKhau
            // 
            txtBoxMatKhau.BorderStyle=BorderStyle.FixedSingle;
            txtBoxMatKhau.Font = new Font("Segoe UI", 14F);
            txtBoxMatKhau.Location = new Point(95, 209);
            txtBoxMatKhau.Name = "txtBoxMatKhau";
            txtBoxMatKhau.Size = new Size(400, 39);
            txtBoxMatKhau.TabIndex = 4;
            txtBoxMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            chkHienMatKhau.AutoSize = true;
            chkHienMatKhau.Cursor = Cursors.Hand;
            chkHienMatKhau.Font = new Font("Segoe UI", 10F);
            chkHienMatKhau.ForeColor = Color.FromArgb(71, 85, 105);
            chkHienMatKhau.Location = new Point(95, 255);
            chkHienMatKhau.Name = "chkHienMatKhau";
            chkHienMatKhau.Size = new Size(147, 27);
            chkHienMatKhau.TabIndex = 5;
            chkHienMatKhau.Text = "Hiện mật khẩu";
            chkHienMatKhau.UseVisualStyleBackColor = true;
            chkHienMatKhau.CheckedChanged += chkHienMatKhau_CheckedChanged;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangNhap.Location = new Point(185, 292);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(230, 44);
            btnDangNhap.TabIndex = 6;
            btnDangNhap.Text = "Đăng nhập";
            
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // frmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 380);
            Controls.Add(chkHienMatKhau);
            Controls.Add(btnDangNhap);
            Controls.Add(txtBoxMatKhau);
            Controls.Add(txtBoxTaiKhoan);
            Controls.Add(lblMatKhau);
            Controls.Add(lblTaiKhoan);
            Controls.Add(lblTieuDeDangNhap);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(15, 118, 110);
            MaximizeBox = false;
            Name = "frmDangNhap";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hiệu thuốc ABC";
            
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnDangNhap.BackColor=Color.FromArgb(15, 118, 110);
        btnDangNhap.FlatStyle = FlatStyle.Flat;
        btnDangNhap.FlatAppearance.BorderSize = 0;
        btnDangNhap.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnDangNhap.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnDangNhap.Cursor = Cursors.Hand;
        btnDangNhap.UseVisualStyleBackColor = false;btnDangNhap.ForeColor=Color.White;
        txtBoxTaiKhoan.ForeColor=Color.FromArgb(15,23,42);
        txtBoxTaiKhoan.BackColor=Color.White;
        txtBoxMatKhau.ForeColor=Color.FromArgb(15,23,42);
        txtBoxMatKhau.BackColor=Color.White;
                ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDeDangNhap;
        private Label lblTaiKhoan;
        private Label lblMatKhau;
        private TextBox txtBoxTaiKhoan;
        private TextBox txtBoxMatKhau;
        private CheckBox chkHienMatKhau;
        private Button btnDangNhap;
    }
}
