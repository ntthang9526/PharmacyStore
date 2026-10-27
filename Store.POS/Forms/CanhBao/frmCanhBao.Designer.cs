#nullable disable
namespace Store.POS;

partial class frmCanhBao
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        posLayout = new TableLayoutPanel();
        posHeading = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        groupBox2 = new GroupBox();
        tableLayoutPanel3 = new TableLayoutPanel();
        label4 = new Label();
        nudNguongTon = new NumericUpDown();
        label5 = new Label();
        nudSoNgay = new NumericUpDown();
        btnLoc = new Button();
        grpSapHet = new GroupBox();
        dgvSapHet = new DataGridView();
        grpSapHetHan = new GroupBox();
        dgvSapHetHan = new DataGridView();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudNguongTon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudSoNgay).BeginInit();
        grpSapHet.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
        grpSapHetHan.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).BeginInit();
        SuspendLayout();
        // 
        // posLayout
        // 
        posLayout.BackColor = Color.FromArgb(248, 250, 252);
        posLayout.ColumnCount = 1;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(tableLayoutPanel1, 0, 1);
        posLayout.Dock = DockStyle.Fill;
        posLayout.Font = new Font("Segoe UI", 10.2F);
        posLayout.ForeColor = SystemColors.ControlText;
        posLayout.Location = new Point(0, 0);
        posLayout.Margin = new Padding(0);
        posLayout.Name = "posLayout";
        posLayout.Padding = new Padding(12);
        posLayout.RowCount = 2;
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        posLayout.Size = new Size(1182, 653);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.BackColor = Color.FromArgb(248, 250, 252);
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.ForeColor = SystemColors.ControlText;
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(1152, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Cảnh báo tồn kho và hạn dùng";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        tableLayoutPanel1.Controls.Add(grpSapHet, 0, 1);
        tableLayoutPanel1.Controls.Add(grpSapHetHan, 0, 2);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel1.ForeColor = SystemColors.ControlText;
        tableLayoutPanel1.Location = new Point(12, 60);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(1158, 581);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // groupBox2
        // 
        groupBox2.BackColor = Color.FromArgb(248, 250, 252);
        groupBox2.Controls.Add(tableLayoutPanel3);
        groupBox2.Dock = DockStyle.Fill;
        groupBox2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox2.ForeColor = SystemColors.ControlText;
        groupBox2.Location = new Point(0, 0);
        groupBox2.Margin = new Padding(0);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(12, 2, 12, 12);
        groupBox2.Size = new Size(1158, 86);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Điều kiện cảnh báo";
        // 
        // tableLayoutPanel3
        // 
        tableLayoutPanel3.BackColor = Color.FromArgb(248, 250, 252);
        tableLayoutPanel3.ColumnCount = 6;
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
        tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Controls.Add(label4, 0, 0);
        tableLayoutPanel3.Controls.Add(nudNguongTon, 1, 0);
        tableLayoutPanel3.Controls.Add(label5, 2, 0);
        tableLayoutPanel3.Controls.Add(nudSoNgay, 3, 0);
        tableLayoutPanel3.Controls.Add(btnLoc, 4, 0);
        tableLayoutPanel3.Dock = DockStyle.Fill;
        tableLayoutPanel3.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel3.ForeColor = SystemColors.ControlText;
        tableLayoutPanel3.Location = new Point(12, 30);
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel3.Size = new Size(1134, 44);
        tableLayoutPanel3.TabIndex = 0;
        // 
        // label4
        // 
        label4.BackColor = Color.FromArgb(248, 250, 252);
        label4.Dock = DockStyle.Top;
        label4.Font = new Font("Segoe UI", 10.2F);
        label4.ForeColor = SystemColors.ControlText;
        label4.Location = new Point(0, 2);
        label4.Margin = new Padding(0, 2, 12, 2);
        label4.Name = "label4";
        label4.Size = new Size(178, 23);
        label4.TabIndex = 0;
        label4.Text = "Tồn kho dưới";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudNguongTon
        // 
        nudNguongTon.BackColor = Color.White;
        nudNguongTon.Dock = DockStyle.Top;
        nudNguongTon.Font = new Font("Segoe UI", 10.2F);
        nudNguongTon.ForeColor = SystemColors.ControlText;
        nudNguongTon.Location = new Point(190, 2);
        nudNguongTon.Margin = new Padding(0, 2, 12, 2);
        nudNguongTon.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        nudNguongTon.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudNguongTon.Name = "nudNguongTon";
        nudNguongTon.Size = new Size(88, 35);
        nudNguongTon.TabIndex = 1;
        nudNguongTon.TextAlign = HorizontalAlignment.Right;
        nudNguongTon.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // label5
        // 
        label5.BackColor = Color.FromArgb(248, 250, 252);
        label5.Dock = DockStyle.Top;
        label5.Font = new Font("Segoe UI", 10.2F);
        label5.ForeColor = SystemColors.ControlText;
        label5.Location = new Point(290, 2);
        label5.Margin = new Padding(0, 2, 12, 2);
        label5.Name = "label5";
        label5.Size = new Size(218, 23);
        label5.TabIndex = 2;
        label5.Text = "Hạn còn dưới (ngày)";
        label5.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudSoNgay
        // 
        nudSoNgay.BackColor = Color.White;
        nudSoNgay.Dock = DockStyle.Top;
        nudSoNgay.Font = new Font("Segoe UI", 10.2F);
        nudSoNgay.ForeColor = SystemColors.ControlText;
        nudSoNgay.Location = new Point(520, 2);
        nudSoNgay.Margin = new Padding(0, 2, 12, 2);
        nudSoNgay.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        nudSoNgay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudSoNgay.Name = "nudSoNgay";
        nudSoNgay.Size = new Size(88, 35);
        nudSoNgay.TabIndex = 3;
        nudSoNgay.TextAlign = HorizontalAlignment.Right;
        nudSoNgay.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // btnLoc
        // 
        btnLoc.BackColor = Color.FromArgb(15, 118, 110);
        btnLoc.Cursor = Cursors.Hand;
        btnLoc.Dock = DockStyle.Fill;
        btnLoc.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLoc.FlatStyle = FlatStyle.Flat;
        btnLoc.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLoc.ForeColor = Color.White;
        btnLoc.Location = new Point(620, 2);
        btnLoc.Margin = new Padding(0, 2, 12, 2);
        btnLoc.Name = "btnLoc";
        btnLoc.Size = new Size(138, 40);
        btnLoc.TabIndex = 5;
        btnLoc.Text = "Lọc / Làm mới";
        btnLoc.UseVisualStyleBackColor = false;
        btnLoc.Click += btnLoc_Click;
        // 
        // grpSapHet
        // 
        grpSapHet.BackColor = Color.FromArgb(248, 250, 252);
        grpSapHet.Controls.Add(dgvSapHet);
        grpSapHet.Dock = DockStyle.Fill;
        grpSapHet.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        grpSapHet.ForeColor = SystemColors.ControlText;
        grpSapHet.Location = new Point(3, 90);
        grpSapHet.Margin = new Padding(3, 4, 3, 4);
        grpSapHet.Name = "grpSapHet";
        grpSapHet.Padding = new Padding(12, 2, 12, 12);
        grpSapHet.Size = new Size(1152, 239);
        grpSapHet.TabIndex = 1;
        grpSapHet.TabStop = false;
        grpSapHet.Text = "Thuốc sắp hết hàng";
        // 
        // dgvSapHet
        // 
        dgvSapHet.AllowUserToAddRows = false;
        dgvSapHet.AllowUserToDeleteRows = false;
        dgvSapHet.AllowUserToResizeColumns = false;
        dgvSapHet.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvSapHet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSapHet.BackgroundColor = Color.White;
        dgvSapHet.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle2.ForeColor = Color.White;
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle2.SelectionForeColor = Color.White;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvSapHet.ColumnHeadersHeight = 38;
        dgvSapHet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.White;
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.Padding = new Padding(4);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
        dgvSapHet.DefaultCellStyle = dataGridViewCellStyle3;
        dgvSapHet.Dock = DockStyle.Fill;
        dgvSapHet.EnableHeadersVisualStyles = false;
        dgvSapHet.Font = new Font("Segoe UI", 10.2F);
        dgvSapHet.GridColor = SystemColors.ControlLight;
        dgvSapHet.Location = new Point(12, 30);
        dgvSapHet.Margin = new Padding(3, 4, 3, 4);
        dgvSapHet.MultiSelect = false;
        dgvSapHet.Name = "dgvSapHet";
        dgvSapHet.ReadOnly = true;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvSapHet.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dgvSapHet.RowHeadersVisible = false;
        dgvSapHet.RowHeadersWidth = 62;
        dgvSapHet.RowTemplate.Height = 34;
        dgvSapHet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHet.Size = new Size(1128, 197);
        dgvSapHet.TabIndex = 0;
        dgvSapHet.DataBindingComplete += dgvSapHet_DataBindingComplete;
        // 
        // grpSapHetHan
        // 
        grpSapHetHan.BackColor = Color.FromArgb(248, 250, 252);
        grpSapHetHan.Controls.Add(dgvSapHetHan);
        grpSapHetHan.Dock = DockStyle.Fill;
        grpSapHetHan.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        grpSapHetHan.ForeColor = SystemColors.ControlText;
        grpSapHetHan.Location = new Point(3, 337);
        grpSapHetHan.Margin = new Padding(3, 4, 3, 4);
        grpSapHetHan.Name = "grpSapHetHan";
        grpSapHetHan.Padding = new Padding(12, 2, 12, 12);
        grpSapHetHan.Size = new Size(1152, 240);
        grpSapHetHan.TabIndex = 2;
        grpSapHetHan.TabStop = false;
        grpSapHetHan.Text = "Lô sắp hết hạn";
        // 
        // dgvSapHetHan
        // 
        dgvSapHetHan.AllowUserToAddRows = false;
        dgvSapHetHan.AllowUserToDeleteRows = false;
        dgvSapHetHan.AllowUserToResizeColumns = false;
        dgvSapHetHan.AllowUserToResizeRows = false;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(248, 250, 252);
        dgvSapHetHan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
        dgvSapHetHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSapHetHan.BackgroundColor = Color.White;
        dgvSapHetHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle6.ForeColor = Color.White;
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(15, 118, 110);
        dataGridViewCellStyle6.SelectionForeColor = Color.White;
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
        dgvSapHetHan.ColumnHeadersHeight = 38;
        dgvSapHetHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle7.BackColor = Color.White;
        dataGridViewCellStyle7.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle7.ForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle7.Padding = new Padding(4);
        dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(204, 251, 241);
        dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(15, 23, 42);
        dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.DefaultCellStyle = dataGridViewCellStyle7;
        dgvSapHetHan.Dock = DockStyle.Fill;
        dgvSapHetHan.EnableHeadersVisualStyles = false;
        dgvSapHetHan.Font = new Font("Segoe UI", 10.2F);
        dgvSapHetHan.GridColor = SystemColors.ControlLight;
        dgvSapHetHan.Location = new Point(12, 30);
        dgvSapHetHan.Margin = new Padding(3, 4, 3, 4);
        dgvSapHetHan.MultiSelect = false;
        dgvSapHetHan.Name = "dgvSapHetHan";
        dgvSapHetHan.ReadOnly = true;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = Color.FromArgb(248, 250, 252);
        dataGridViewCellStyle8.Font = new Font("Segoe UI", 10.2F);
        dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
        dgvSapHetHan.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
        dgvSapHetHan.RowHeadersVisible = false;
        dgvSapHetHan.RowHeadersWidth = 62;
        dgvSapHetHan.RowTemplate.Height = 34;
        dgvSapHetHan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSapHetHan.Size = new Size(1128, 198);
        dgvSapHetHan.TabIndex = 0;
        dgvSapHetHan.CellContentClick += dgvSapHetHan_CellContentClick;
        dgvSapHetHan.DataBindingComplete += dgvSapHetHan_DataBindingComplete;
        // 
        // frmCanhBao
        // 
        AutoScaleDimensions = new SizeF(144F, 144F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(248, 250, 252);
        ClientSize = new Size(1182, 653);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = Color.FromArgb(15, 23, 42);
        MinimumSize = new Size(1060, 700);
        Name = "frmCanhBao";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cảnh báo tồn kho và hạn dùng | Hiệu thuốc ABC";
        Load += frmCanhBao_Load;
        posLayout.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        tableLayoutPanel3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudNguongTon).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudSoNgay).EndInit();
        grpSapHet.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
        grpSapHetHan.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSapHetHan).EndInit();
        ResumeLayout(false);
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.Label label4;
    private global::System.Windows.Forms.NumericUpDown nudNguongTon;
    private global::System.Windows.Forms.Label label5;
    private global::System.Windows.Forms.NumericUpDown nudSoNgay;
    private global::System.Windows.Forms.Button btnLoc;
    private global::System.Windows.Forms.GroupBox grpSapHet;
    private global::System.Windows.Forms.DataGridView dgvSapHet;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetColumn5;
    private global::System.Windows.Forms.GroupBox grpSapHetHan;
    private global::System.Windows.Forms.DataGridView dgvSapHetHan;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvSapHetHanColumn7;
}
