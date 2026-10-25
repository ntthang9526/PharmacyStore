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
            btnDangNhap = new Button();
            SuspendLayout();
            // 
            // lblTieuDeDangNhap
            // 
            lblTieuDeDangNhap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTieuDeDangNhap.AutoSize = true;
            lblTieuDeDangNhap.Font = new Font("Microsoft Sans Serif", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDeDangNhap.ForeColor = Color.Navy;
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
            txtBoxTaiKhoan.BorderStyle = BorderStyle.FixedSingle;
            txtBoxTaiKhoan.Font = new Font("Segoe UI", 14F);
            txtBoxTaiKhoan.Location = new Point(95, 117);
            txtBoxTaiKhoan.Name = "txtBoxTaiKhoan";
            txtBoxTaiKhoan.Size = new Size(400, 39);
            txtBoxTaiKhoan.TabIndex = 3;
            // 
            // txtBoxMatKhau
            // 
            txtBoxMatKhau.BorderStyle = BorderStyle.FixedSingle;
            txtBoxMatKhau.Font = new Font("Segoe UI", 14F);
            txtBoxMatKhau.Location = new Point(95, 209);
            txtBoxMatKhau.Name = "txtBoxMatKhau";
            txtBoxMatKhau.Size = new Size(400, 39);
            txtBoxMatKhau.TabIndex = 4;
            txtBoxMatKhau.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangNhap.Location = new Point(185, 271);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(230, 55);
            btnDangNhap.TabIndex = 5;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // frmDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 353);
            Controls.Add(btnDangNhap);
            Controls.Add(txtBoxMatKhau);
            Controls.Add(txtBoxTaiKhoan);
            Controls.Add(lblMatKhau);
            Controls.Add(lblTaiKhoan);
            Controls.Add(lblTieuDeDangNhap);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.Navy;
            MaximizeBox = false;
            Name = "frmDangNhap";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hiệu thuốc ABC";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDeDangNhap;
        private Label lblTaiKhoan;
        private Label lblMatKhau;
        private TextBox txtBoxTaiKhoan;
        private TextBox txtBoxMatKhau;
        private Button btnDangNhap;
    }
}
