namespace Store.POS
{
    partial class frmBanHang
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
            components = new System.ComponentModel.Container();
            groupThanhToan = new GroupBox();
            rowDataBindingSource = new BindingSource(components);
            textBox1 = new TextBox();
            lblTimKiem = new Label();
            groupThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).BeginInit();
            SuspendLayout();
            // 
            // groupThanhToan
            // 
            groupThanhToan.Anchor = AnchorStyles.Left;
            groupThanhToan.Controls.Add(textBox1);
            groupThanhToan.Controls.Add(lblTimKiem);
            groupThanhToan.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupThanhToan.Location = new Point(12, 12);
            groupThanhToan.Name = "groupThanhToan";
            groupThanhToan.Size = new Size(800, 630);
            groupThanhToan.TabIndex = 0;
            groupThanhToan.TabStop = false;
            groupThanhToan.Text = "Tìm kiếm và Giỏ hàng";
            // 
            // rowDataBindingSource
            // 
            rowDataBindingSource.DataSource = typeof(FormBanHang.RowData);
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(216, 46);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(450, 30);
            textBox1.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTimKiem.Location = new Point(81, 49);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(132, 23);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm kiếm thuốc:";
            // 
            // frmBanHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(groupThanhToan);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmBanHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng";
            FormClosing += frmBanHang_FormClosing;
            FormClosed += frmBanHang_FormClosed;
            KeyDown += frmBanHang_KeyDown;
            groupThanhToan.ResumeLayout(false);
            groupThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rowDataBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupThanhToan;
        private TextBox textBox1;
        private Label lblTimKiem;
        private BindingSource rowDataBindingSource;
    }
}