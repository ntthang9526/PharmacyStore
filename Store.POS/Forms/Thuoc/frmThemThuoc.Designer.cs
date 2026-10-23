#nullable disable
namespace Store.POS;

partial class frmThemThuoc
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        posLayout = new TableLayoutPanel();
        posHeading = new Label();
        groupBox1 = new GroupBox();
        tableLayoutPanel2 = new TableLayoutPanel();
        label3 = new Label();
        tableLayoutPanel4 = new TableLayoutPanel();
        tableLayoutPanel5 = new TableLayoutPanel();
        label6 = new Label();
        txtTen = new TextBox();
        tableLayoutPanel7 = new TableLayoutPanel();
        label8 = new Label();
        tableLayoutPanel9 = new TableLayoutPanel();
        cboNhom = new ComboBox();
        btnThemNhom = new Button();
        tableLayoutPanel10 = new TableLayoutPanel();
        label11 = new Label();
        txtDonVi = new TextBox();
        tableLayoutPanel12 = new TableLayoutPanel();
        label13 = new Label();
        nudGiaBan = new NumericUpDown();
        lblLoi = new Label();
        tableLayoutPanel14 = new TableLayoutPanel();
        btnLuu = new Button();
        btnHuy = new Button();
        posLayout.SuspendLayout();
        groupBox1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel9.SuspendLayout();
        tableLayoutPanel10.SuspendLayout();
        tableLayoutPanel12.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
        tableLayoutPanel14.SuspendLayout();
        SuspendLayout();
        // 
        // posLayout
        // 
        posLayout.BackColor = SystemColors.Control;
        posLayout.ColumnCount = 1;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(groupBox1, 0, 1);
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
        posLayout.Size = new Size(520, 490);
        posLayout.TabIndex = 0;
        // 
        // posHeading
        // 
        posHeading.BackColor = SystemColors.Control;
        posHeading.Dock = DockStyle.Fill;
        posHeading.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        posHeading.ForeColor = SystemColors.ControlText;
        posHeading.Location = new Point(15, 12);
        posHeading.Name = "posHeading";
        posHeading.Size = new Size(490, 48);
        posHeading.TabIndex = 0;
        posHeading.Text = "Thêm thuốc";
        posHeading.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // groupBox1
        // 
        groupBox1.BackColor = SystemColors.Control;
        groupBox1.Controls.Add(tableLayoutPanel2);
        groupBox1.Dock = DockStyle.Fill;
        groupBox1.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        groupBox1.ForeColor = SystemColors.ControlText;
        groupBox1.Location = new Point(12, 60);
        groupBox1.Margin = new Padding(0);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(12, 2, 12, 12);
        groupBox1.Size = new Size(496, 418);
        groupBox1.TabIndex = 1;
        groupBox1.TabStop = false;
        groupBox1.Text = "Thuốc mới — tồn kho khởi đầu bằng 0";
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.BackColor = SystemColors.Control;
        tableLayoutPanel2.ColumnCount = 1;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.Controls.Add(label3, 0, 0);
        tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 1);
        tableLayoutPanel2.Controls.Add(lblLoi, 0, 2);
        tableLayoutPanel2.Controls.Add(tableLayoutPanel14, 0, 3);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel2.ForeColor = SystemColors.ControlText;
        tableLayoutPanel2.Location = new Point(12, 25);
        tableLayoutPanel2.Margin = new Padding(0);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 4;
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 224F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableLayoutPanel2.Size = new Size(472, 381);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // label3
        // 
        label3.AutoEllipsis = true;
        label3.BackColor = SystemColors.Control;
        label3.Dock = DockStyle.Fill;
        label3.Font = new Font("Segoe UI", 10.2F);
        label3.ForeColor = SystemColors.ControlText;
        label3.Location = new Point(3, 0);
        label3.Name = "label3";
        label3.Size = new Size(466, 34);
        label3.TabIndex = 0;
        label3.Text = "NCC: Chưa chọn nhà cung cấp";
        // 
        // tableLayoutPanel4
        // 
        tableLayoutPanel4.AutoSize = true;
        tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutPanel4.BackColor = SystemColors.Control;
        tableLayoutPanel4.ColumnCount = 1;
        tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel4.Controls.Add(tableLayoutPanel5);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel7);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel10);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel12);
        tableLayoutPanel4.Dock = DockStyle.Top;
        tableLayoutPanel4.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel4.ForeColor = SystemColors.ControlText;
        tableLayoutPanel4.Location = new Point(0, 34);
        tableLayoutPanel4.Margin = new Padding(0);
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.RowCount = 4;
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutPanel4.Size = new Size(472, 224);
        tableLayoutPanel4.TabIndex = 1;
        // 
        // tableLayoutPanel5
        // 
        tableLayoutPanel5.BackColor = SystemColors.Control;
        tableLayoutPanel5.ColumnCount = 1;
        tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel5.Controls.Add(label6, 0, 0);
        tableLayoutPanel5.Controls.Add(txtTen, 0, 1);
        tableLayoutPanel5.Dock = DockStyle.Fill;
        tableLayoutPanel5.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel5.ForeColor = SystemColors.ControlText;
        tableLayoutPanel5.Location = new Point(0, 0);
        tableLayoutPanel5.Margin = new Padding(0);
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.RowCount = 2;
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel5.Size = new Size(472, 56);
        tableLayoutPanel5.TabIndex = 0;
        // 
        // label6
        // 
        label6.BackColor = SystemColors.Control;
        label6.Dock = DockStyle.Fill;
        label6.Font = new Font("Segoe UI", 10.2F);
        label6.ForeColor = SystemColors.ControlText;
        label6.Location = new Point(3, 0);
        label6.Name = "label6";
        label6.Size = new Size(466, 22);
        label6.TabIndex = 0;
        label6.Text = "Tên thuốc";
        label6.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTen
        // 
        txtTen.BackColor = Color.White;
        txtTen.BorderStyle = BorderStyle.FixedSingle;
        txtTen.Cursor = Cursors.IBeam;
        txtTen.Dock = DockStyle.Top;
        txtTen.Font = new Font("Segoe UI", 10.2F);
        txtTen.ForeColor = SystemColors.ControlText;
        txtTen.Location = new Point(0, 23);
        txtTen.Margin = new Padding(0, 1, 0, 5);
        txtTen.MaxLength = 150;
        txtTen.Name = "txtTen";
        txtTen.Size = new Size(472, 30);
        txtTen.TabIndex = 1;
        // 
        // tableLayoutPanel7
        // 
        tableLayoutPanel7.BackColor = SystemColors.Control;
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel7.Controls.Add(label8, 0, 0);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel9, 0, 1);
        tableLayoutPanel7.Dock = DockStyle.Fill;
        tableLayoutPanel7.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel7.ForeColor = SystemColors.ControlText;
        tableLayoutPanel7.Location = new Point(0, 56);
        tableLayoutPanel7.Margin = new Padding(0);
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.RowCount = 2;
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel7.Size = new Size(472, 56);
        tableLayoutPanel7.TabIndex = 1;
        // 
        // label8
        // 
        label8.BackColor = SystemColors.Control;
        label8.Dock = DockStyle.Fill;
        label8.Font = new Font("Segoe UI", 10.2F);
        label8.ForeColor = SystemColors.ControlText;
        label8.Location = new Point(3, 0);
        label8.Name = "label8";
        label8.Size = new Size(466, 22);
        label8.TabIndex = 0;
        label8.Text = "Nhóm thuốc";
        label8.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tableLayoutPanel9
        // 
        tableLayoutPanel9.BackColor = SystemColors.Control;
        tableLayoutPanel9.ColumnCount = 2;
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
        tableLayoutPanel9.Controls.Add(cboNhom, 0, 0);
        tableLayoutPanel9.Controls.Add(btnThemNhom, 1, 0);
        tableLayoutPanel9.Dock = DockStyle.Top;
        tableLayoutPanel9.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel9.ForeColor = SystemColors.ControlText;
        tableLayoutPanel9.Location = new Point(0, 22);
        tableLayoutPanel9.Margin = new Padding(0);
        tableLayoutPanel9.Name = "tableLayoutPanel9";
        tableLayoutPanel9.RowCount = 1;
        tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel9.Size = new Size(472, 32);
        tableLayoutPanel9.TabIndex = 1;
        // 
        // cboNhom
        // 
        cboNhom.BackColor = SystemColors.Window;
        cboNhom.Dock = DockStyle.Fill;
        cboNhom.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNhom.DropDownWidth = 428;
        cboNhom.Font = new Font("Segoe UI", 10.2F);
        cboNhom.ForeColor = SystemColors.ControlText;
        cboNhom.Location = new Point(0, 0);
        cboNhom.Margin = new Padding(0, 0, 6, 0);
        cboNhom.Name = "cboNhom";
        cboNhom.Size = new Size(428, 31);
        cboNhom.TabIndex = 0;
        // 
        // btnThemNhom
        // 
        btnThemNhom.AccessibleName = "Thêm nhóm thuốc";
        btnThemNhom.BackColor = Color.FromArgb(192, 255, 192);
        btnThemNhom.Cursor = Cursors.Hand;
        btnThemNhom.Dock = DockStyle.Fill;
        btnThemNhom.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnThemNhom.FlatStyle = FlatStyle.Flat;
        btnThemNhom.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnThemNhom.ForeColor = SystemColors.ControlText;
        btnThemNhom.Location = new Point(434, 0);
        btnThemNhom.Margin = new Padding(0);
        btnThemNhom.Name = "btnThemNhom";
        btnThemNhom.Size = new Size(38, 32);
        btnThemNhom.TabIndex = 1;
        btnThemNhom.Text = "+";
        btnThemNhom.UseVisualStyleBackColor = false;
        // 
        // tableLayoutPanel10
        // 
        tableLayoutPanel10.BackColor = SystemColors.Control;
        tableLayoutPanel10.ColumnCount = 1;
        tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel10.Controls.Add(label11, 0, 0);
        tableLayoutPanel10.Controls.Add(txtDonVi, 0, 1);
        tableLayoutPanel10.Dock = DockStyle.Fill;
        tableLayoutPanel10.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel10.ForeColor = SystemColors.ControlText;
        tableLayoutPanel10.Location = new Point(0, 112);
        tableLayoutPanel10.Margin = new Padding(0);
        tableLayoutPanel10.Name = "tableLayoutPanel10";
        tableLayoutPanel10.RowCount = 2;
        tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel10.Size = new Size(472, 56);
        tableLayoutPanel10.TabIndex = 2;
        // 
        // label11
        // 
        label11.BackColor = SystemColors.Control;
        label11.Dock = DockStyle.Fill;
        label11.Font = new Font("Segoe UI", 10.2F);
        label11.ForeColor = SystemColors.ControlText;
        label11.Location = new Point(3, 0);
        label11.Name = "label11";
        label11.Size = new Size(466, 22);
        label11.TabIndex = 0;
        label11.Text = "Đơn vị tính";
        label11.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDonVi
        // 
        txtDonVi.BackColor = Color.White;
        txtDonVi.BorderStyle = BorderStyle.FixedSingle;
        txtDonVi.Cursor = Cursors.IBeam;
        txtDonVi.Dock = DockStyle.Top;
        txtDonVi.Font = new Font("Segoe UI", 10.2F);
        txtDonVi.ForeColor = SystemColors.ControlText;
        txtDonVi.Location = new Point(0, 23);
        txtDonVi.Margin = new Padding(0, 1, 0, 5);
        txtDonVi.MaxLength = 30;
        txtDonVi.Name = "txtDonVi";
        txtDonVi.Size = new Size(472, 30);
        txtDonVi.TabIndex = 1;
        // 
        // tableLayoutPanel12
        // 
        tableLayoutPanel12.BackColor = SystemColors.Control;
        tableLayoutPanel12.ColumnCount = 1;
        tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel12.Controls.Add(label13, 0, 0);
        tableLayoutPanel12.Controls.Add(nudGiaBan, 0, 1);
        tableLayoutPanel12.Dock = DockStyle.Fill;
        tableLayoutPanel12.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel12.ForeColor = SystemColors.ControlText;
        tableLayoutPanel12.Location = new Point(0, 168);
        tableLayoutPanel12.Margin = new Padding(0);
        tableLayoutPanel12.Name = "tableLayoutPanel12";
        tableLayoutPanel12.RowCount = 2;
        tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
        tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        tableLayoutPanel12.Size = new Size(472, 56);
        tableLayoutPanel12.TabIndex = 3;
        // 
        // label13
        // 
        label13.BackColor = SystemColors.Control;
        label13.Dock = DockStyle.Fill;
        label13.Font = new Font("Segoe UI", 10.2F);
        label13.ForeColor = SystemColors.ControlText;
        label13.Location = new Point(3, 0);
        label13.Name = "label13";
        label13.Size = new Size(466, 22);
        label13.TabIndex = 0;
        label13.Text = "Giá bán / đơn vị (đ)";
        label13.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // nudGiaBan
        // 
        nudGiaBan.BackColor = Color.White;
        nudGiaBan.Dock = DockStyle.Top;
        nudGiaBan.Font = new Font("Segoe UI", 10.2F);
        nudGiaBan.ForeColor = SystemColors.ControlText;
        nudGiaBan.Location = new Point(0, 23);
        nudGiaBan.Margin = new Padding(0, 1, 0, 5);
        nudGiaBan.Maximum = new decimal(new int[] { -1486618625, 232830643, 0, 131072 });
        nudGiaBan.Name = "nudGiaBan";
        nudGiaBan.Size = new Size(472, 30);
        nudGiaBan.TabIndex = 1;
        nudGiaBan.ThousandsSeparator = true;
        // 
        // lblLoi
        // 
        lblLoi.BackColor = SystemColors.Control;
        lblLoi.Dock = DockStyle.Fill;
        lblLoi.Font = new Font("Segoe UI", 10.2F);
        lblLoi.ForeColor = Color.Firebrick;
        lblLoi.Location = new Point(3, 258);
        lblLoi.Name = "lblLoi";
        lblLoi.Size = new Size(466, 75);
        lblLoi.TabIndex = 2;
        // 
        // tableLayoutPanel14
        // 
        tableLayoutPanel14.AutoSize = true;
        tableLayoutPanel14.BackColor = SystemColors.Control;
        tableLayoutPanel14.ColumnCount = 2;
        tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel14.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel14.Controls.Add(btnLuu, 0, 0);
        tableLayoutPanel14.Controls.Add(btnHuy, 1, 0);
        tableLayoutPanel14.Dock = DockStyle.Fill;
        tableLayoutPanel14.Font = new Font("Segoe UI", 10.2F);
        tableLayoutPanel14.ForeColor = SystemColors.ControlText;
        tableLayoutPanel14.Location = new Point(0, 333);
        tableLayoutPanel14.Margin = new Padding(0);
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.RowCount = 1;
        tableLayoutPanel14.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tableLayoutPanel14.Size = new Size(472, 48);
        tableLayoutPanel14.TabIndex = 3;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(192, 255, 192);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.Dock = DockStyle.Fill;
        btnLuu.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnLuu.ForeColor = SystemColors.ControlText;
        btnLuu.Location = new Point(0, 4);
        btnLuu.Margin = new Padding(0, 4, 4, 0);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(232, 44);
        btnLuu.TabIndex = 0;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        // 
        // btnHuy
        // 
        btnHuy.BackColor = Color.FromArgb(255, 192, 192);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.DialogResult = DialogResult.Cancel;
        btnHuy.Dock = DockStyle.Fill;
        btnHuy.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
        btnHuy.ForeColor = SystemColors.ControlText;
        btnHuy.Location = new Point(240, 4);
        btnHuy.Margin = new Padding(4, 4, 0, 0);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(232, 44);
        btnHuy.TabIndex = 1;
        btnHuy.Text = "Hủy";
        btnHuy.UseVisualStyleBackColor = false;
        // 
        // frmThemThuoc
        // 
        AcceptButton = btnLuu;
        AutoScaleDimensions = new SizeF(120F, 120F);
        AutoScaleMode = AutoScaleMode.Dpi;
        BackColor = SystemColors.Control;
        CancelButton = btnHuy;
        ClientSize = new Size(520, 490);
        Controls.Add(posLayout);
        Font = new Font("Segoe UI", 10.2F);
        ForeColor = SystemColors.ControlText;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimumSize = new Size(538, 537);
        Name = "frmThemThuoc";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Thêm thuốc | Hiệu thuốc ABC";
        posLayout.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel9.ResumeLayout(false);
        tableLayoutPanel10.ResumeLayout(false);
        tableLayoutPanel10.PerformLayout();
        tableLayoutPanel12.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
        tableLayoutPanel14.ResumeLayout(false);
        ResumeLayout(false);
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.GroupBox groupBox1;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private global::System.Windows.Forms.Label label3;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.Label label6;
    private global::System.Windows.Forms.TextBox txtTen;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.Label label8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    private global::System.Windows.Forms.ComboBox cboNhom;
    private global::System.Windows.Forms.Button btnThemNhom;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.Label label11;
    private global::System.Windows.Forms.TextBox txtDonVi;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
    private global::System.Windows.Forms.Label label13;
    private global::System.Windows.Forms.NumericUpDown nudGiaBan;
    private global::System.Windows.Forms.Label lblLoi;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.Button btnLuu;
    private global::System.Windows.Forms.Button btnHuy;
}
