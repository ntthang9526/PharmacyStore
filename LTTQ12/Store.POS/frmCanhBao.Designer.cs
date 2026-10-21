namespace Store.POS
{
    partial class frmCanhBao
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
            grpLoc = new GroupBox();
            lblNguongTon = new Label();
            nudNguongTon = new NumericUpDown();
            lblSoNgay = new Label();
            nudSoNgay = new NumericUpDown();
            lblNgay = new Label();
            btnLoc = new Button();
            grpSapHet = new GroupBox();
            dgvSapHet = new DataGridView();
            grpSapHetHan = new GroupBox();
            dgvSapHetHan = new DataGridView();
            grpLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNguongTon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoNgay).BeginInit();
            grpSapHet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
            grpSapHetHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).BeginInit();
            SuspendLayout();
            // 
            // grpLoc
            // 
            grpLoc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpLoc.Controls.Add(lblNguongTon);
            grpLoc.Controls.Add(nudNguongTon);
            grpLoc.Controls.Add(lblSoNgay);
            grpLoc.Controls.Add(nudSoNgay);
            grpLoc.Controls.Add(lblNgay);
            grpLoc.Controls.Add(btnLoc);
            grpLoc.Location = new Point(12, 12);
            grpLoc.Name = "grpLoc";
            grpLoc.Size = new Size(1076, 60);
            grpLoc.TabIndex = 0;
            grpLoc.TabStop = false;
            grpLoc.Text = "Điều kiện cảnh báo";
            // 
            // lblNguongTon
            // 
            lblNguongTon.Location = new Point(12, 26);
            lblNguongTon.Name = "lblNguongTon";
            lblNguongTon.Size = new Size(125, 23);
            lblNguongTon.TabIndex = 0;
            lblNguongTon.Text = "Cảnh báo khi tồn <";
            lblNguongTon.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudNguongTon
            // 
            nudNguongTon.Location = new Point(140, 26);
            nudNguongTon.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudNguongTon.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNguongTon.Name = "nudNguongTon";
            nudNguongTon.Size = new Size(80, 23);
            nudNguongTon.TabIndex = 1;
            nudNguongTon.TextAlign = HorizontalAlignment.Right;
            nudNguongTon.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblSoNgay
            // 
            lblSoNgay.Location = new Point(260, 26);
            lblSoNgay.Name = "lblSoNgay";
            lblSoNgay.Size = new Size(165, 23);
            lblSoNgay.TabIndex = 2;
            lblSoNgay.Text = "Cảnh báo khi hạn còn dưới";
            lblSoNgay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudSoNgay
            // 
            nudSoNgay.Location = new Point(430, 26);
            nudSoNgay.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            nudSoNgay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoNgay.Name = "nudSoNgay";
            nudSoNgay.Size = new Size(80, 23);
            nudSoNgay.TabIndex = 3;
            nudSoNgay.TextAlign = HorizontalAlignment.Right;
            nudSoNgay.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblNgay
            // 
            lblNgay.Location = new Point(515, 26);
            lblNgay.Name = "lblNgay";
            lblNgay.Size = new Size(40, 23);
            lblNgay.TabIndex = 4;
            lblNgay.Text = "ngày";
            lblNgay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnLoc
            // 
            btnLoc.Location = new Point(580, 22);
            btnLoc.Name = "btnLoc";
            btnLoc.Size = new Size(130, 30);
            btnLoc.TabIndex = 5;
            btnLoc.Text = "Lọc / Làm mới";
            btnLoc.UseVisualStyleBackColor = true;
            btnLoc.Click += btnLoc_Click;
            // 
            // grpSapHet
            // 
            grpSapHet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSapHet.Controls.Add(dgvSapHet);
            grpSapHet.Location = new Point(12, 80);
            grpSapHet.Name = "grpSapHet";
            grpSapHet.Size = new Size(1076, 280);
            grpSapHet.TabIndex = 1;
            grpSapHet.TabStop = false;
            grpSapHet.Text = "Thuốc sắp hết hàng";
            // 
            // dgvSapHet
            // 
            dgvSapHet.AllowUserToAddRows = false;
            dgvSapHet.AllowUserToDeleteRows = false;
            dgvSapHet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSapHet.Dock = DockStyle.Fill;
            dgvSapHet.Name = "dgvSapHet";
            dgvSapHet.ReadOnly = true;
            dgvSapHet.RowHeadersVisible = false;
            dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSapHet.TabIndex = 0;
            dgvSapHet.DataBindingComplete += dgvSapHet_DataBindingComplete;
            // 
            // grpSapHetHan
            // 
            grpSapHetHan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpSapHetHan.Controls.Add(dgvSapHetHan);
            grpSapHetHan.Location = new Point(12, 368);
            grpSapHetHan.Name = "grpSapHetHan";
            grpSapHetHan.Size = new Size(1076, 300);
            grpSapHetHan.TabIndex = 2;
            grpSapHetHan.TabStop = false;
            grpSapHetHan.Text = "Lô sắp hết hạn";
            // 
            // dgvSapHetHan
            // 
            dgvSapHetHan.AllowUserToAddRows = false;
            dgvSapHetHan.AllowUserToDeleteRows = false;
            dgvSapHetHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSapHetHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSapHetHan.Dock = DockStyle.Fill;
            dgvSapHetHan.Name = "dgvSapHetHan";
            dgvSapHetHan.ReadOnly = true;
            dgvSapHetHan.RowHeadersVisible = false;
            dgvSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSapHetHan.TabIndex = 0;
            dgvSapHetHan.DataBindingComplete += dgvSapHetHan_DataBindingComplete;
            // 
            // frmCanhBao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(grpSapHetHan);
            Controls.Add(grpSapHet);
            Controls.Add(grpLoc);
            Name = "frmCanhBao";
            Text = "Cảnh báo tồn kho và hạn dùng";
            Load += frmCanhBao_Load;
            grpLoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudNguongTon).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoNgay).EndInit();
            grpSapHet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
            grpSapHetHan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpLoc;
        private Label lblNguongTon;
        private NumericUpDown nudNguongTon;
        private Label lblSoNgay;
        private NumericUpDown nudSoNgay;
        private Label lblNgay;
        private Button btnLoc;
        private GroupBox grpSapHet;
        private DataGridView dgvSapHet;
        private GroupBox grpSapHetHan;
        private DataGridView dgvSapHetHan;
    }
}
