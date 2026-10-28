namespace Store.POS.FormBanHang
{
    partial class frmThanhToanQR
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
            picQR = new PictureBox();
            lblTongTien = new Label();
            btnHoanTat = new Button();
            btnHuy = new Button();
            ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
            SuspendLayout();
            // 
            // picQR
            // 
            picQR.Location = new Point(12, 12);
            picQR.Name = "picQR";
            picQR.Size = new Size(358, 400);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.TabIndex = 0;
            picQR.TabStop = false;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.BackColor = Color.White;
            lblTongTien.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTongTien.Location = new Point(74, 415);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(235, 31);
            lblTongTien.TabIndex = 1;
            lblTongTien.Text = "Thanh toán: 000000đ";
            // 
            // btnHoanTat
            // 
            btnHoanTat.BackColor = Color.FromArgb(15, 118, 110);
            btnHoanTat.ForeColor = Color.White;
            
            btnHoanTat.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHoanTat.Location = new Point(12, 458);
            btnHoanTat.Name = "btnHoanTat";
            btnHoanTat.Size = new Size(175, 47);
            btnHoanTat.TabIndex = 2;
            btnHoanTat.Text = "Hoàn tất";
            
            btnHoanTat.Click += btnHoanTat_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(100, 116, 139);
            btnHuy.ForeColor = Color.White;
            
            btnHuy.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnHuy.Location = new Point(195, 458);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(175, 47);
            btnHuy.TabIndex = 3;
            btnHuy.Text = "Hủy";
            
            btnHuy.Click += btnHuy_Click;
            // 
            // frmThanhToanQR
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(382, 518);
            Controls.Add(btnHuy);
            Controls.Add(btnHoanTat);
            Controls.Add(lblTongTien);
            Controls.Add(picQR);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmThanhToanQR";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmThanhToanQR";
            ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
            
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnHoanTat.BackColor=Color.FromArgb(15, 118, 110);
        btnHoanTat.FlatStyle = FlatStyle.Flat;
        btnHoanTat.FlatAppearance.BorderSize = 0;
        btnHoanTat.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnHoanTat.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnHoanTat.Cursor = Cursors.Hand;
        btnHoanTat.UseVisualStyleBackColor = false;btnHoanTat.ForeColor=Color.White;
        btnHuy.BackColor=Color.FromArgb(100, 116, 139);
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
        btnHuy.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.UseVisualStyleBackColor = false;btnHuy.ForeColor=Color.White;
ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picQR;
        private Label lblTongTien;
        private Button btnHoanTat;
        private Button btnHuy;
    }
}
