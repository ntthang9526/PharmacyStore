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
            btnHoanTat.BackColor = Color.FromArgb(192, 255, 192);
            btnHoanTat.FlatStyle = FlatStyle.Popup;
            btnHoanTat.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHoanTat.Location = new Point(12, 458);
            btnHoanTat.Name = "btnHoanTat";
            btnHoanTat.Size = new Size(175, 47);
            btnHoanTat.TabIndex = 2;
            btnHoanTat.Text = "Hoàn tất";
            btnHoanTat.UseVisualStyleBackColor = false;
            btnHoanTat.Click += btnHoanTat_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.FromArgb(255, 192, 192);
            btnHuy.FlatStyle = FlatStyle.Popup;
            btnHuy.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHuy.Location = new Point(195, 459);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(175, 47);
            btnHuy.TabIndex = 3;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
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
        btnHoanTat.BackColor=Color.FromArgb(15, 118, 110);btnHoanTat.ForeColor=Color.White;btnHoanTat.UseVisualStyleBackColor=false;
        btnHuy.BackColor=Color.FromArgb(100, 116, 139);btnHuy.ForeColor=Color.White;btnHuy.UseVisualStyleBackColor=false;
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