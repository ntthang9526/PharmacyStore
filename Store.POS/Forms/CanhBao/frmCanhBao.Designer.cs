using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmCanhBao
{
    private System.Windows.Forms.DataGridViewCheckBoxColumn colDaXuLy;
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dgvSapHetColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvSapHetHanColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colDaXuLy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        components = new global::System.ComponentModel.Container();
        posLayout = new global::System.Windows.Forms.TableLayoutPanel();
        posHeading = new global::System.Windows.Forms.Label();
        tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox2 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
        label4 = new global::System.Windows.Forms.Label();
        nudNguongTon = new global::System.Windows.Forms.NumericUpDown();
        label5 = new global::System.Windows.Forms.Label();
        nudSoNgay = new global::System.Windows.Forms.NumericUpDown();
        btnLoc = new global::System.Windows.Forms.Button();
        grpSapHet = new global::System.Windows.Forms.GroupBox();
        dgvSapHet = new global::System.Windows.Forms.DataGridView();
        
        
        grpSapHetHan = new global::System.Windows.Forms.GroupBox();
        dgvSapHetHan = new global::System.Windows.Forms.DataGridView();
        
        
        
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudNguongTon).BeginInit();
        ((global::System.ComponentModel.ISupportInitialize)nudSoNgay).BeginInit();
        grpSapHet.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvSapHet).BeginInit();
        grpSapHetHan.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvSapHetHan).BeginInit();
        SuspendLayout();

        // posLayout
        posLayout.Name = "posLayout";
        posLayout.Text = "";
        posLayout.Location = new global::System.Drawing.Point(0, 0);
        posLayout.Size = new global::System.Drawing.Size(1182, 653);
        posLayout.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        posLayout.Padding = new global::System.Windows.Forms.Padding(12, 12, 12, 12);
        posLayout.Dock = global::System.Windows.Forms.DockStyle.Fill;

        posLayout.AutoSize = false;
        posLayout.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        posLayout.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        posLayout.ForeColor = Color.FromArgb(15, 23, 42);
        posLayout.BackColor = Color.FromArgb(242, 246, 248);
        posLayout.Cursor = global::System.Windows.Forms.Cursors.Default;
        posLayout.TabIndex = 0;
        posLayout.TabStop = false;
        posLayout.AccessibleName = null;
        posLayout.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        posLayout.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        posLayout.RowCount = 2;
        posLayout.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        posLayout.AutoScroll = false;
        posLayout.Visible = true;
        posLayout.ColumnCount = 1;
        posLayout.Enabled = true;
        posLayout.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        posLayout.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // posHeading
        posHeading.Name = "posHeading";
        posHeading.Text = "Cảnh báo tồn kho và hạn dùng";
        posHeading.Location = new global::System.Drawing.Point(15, 12);
        posHeading.Size = new global::System.Drawing.Size(1152, 48);
        posHeading.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        posHeading.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        posHeading.Dock = global::System.Windows.Forms.DockStyle.Fill;

        posHeading.AutoSize = false;
        posHeading.Font = new Font("Segoe UI Semibold",16F,FontStyle.Bold);
        posHeading.ForeColor = Color.FromArgb(15,23,42);
        posHeading.BackColor = Color.FromArgb(242, 246, 248);
        posHeading.Cursor = global::System.Windows.Forms.Cursors.Default;
        posHeading.TabIndex = 0;
        posHeading.TabStop = false;
        posHeading.AccessibleName = null;
        posHeading.AutoEllipsis = false;
        posHeading.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        posHeading.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        posHeading.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        posHeading.Visible = true;
        posHeading.Enabled = true;

        // tableLayoutPanel1
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Text = "";
        tableLayoutPanel1.Location = new global::System.Drawing.Point(12, 60);
        tableLayoutPanel1.Size = new global::System.Drawing.Size(1158, 581);
        tableLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel1.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel1.AutoSize = false;
        tableLayoutPanel1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel1.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel1.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel1.BackColor = Color.FromArgb(242, 246, 248);
        tableLayoutPanel1.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel1.TabIndex = 1;
        tableLayoutPanel1.TabStop = false;
        tableLayoutPanel1.AccessibleName = null;
        tableLayoutPanel1.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel1.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel1.RowCount = 3;
        tableLayoutPanel1.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel1.AutoScroll = false;
        tableLayoutPanel1.Visible = true;
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.Enabled = true;
        tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 86F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50F));

        // groupBox2
        groupBox2.Name = "groupBox2";
        groupBox2.Text = "Điều kiện cảnh báo";
        groupBox2.Location = new global::System.Drawing.Point(0, 0);
        groupBox2.Size = new global::System.Drawing.Size(1158, 86);
        groupBox2.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox2.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox2.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox2.AutoSize = false;
        groupBox2.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox2.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox2.ForeColor = Color.FromArgb(15, 23, 42);
        groupBox2.BackColor = Color.White;
        groupBox2.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.AccessibleName = null;
        groupBox2.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox2.Visible = true;
        groupBox2.Enabled = true;

        // tableLayoutPanel3
        tableLayoutPanel3.Name = "tableLayoutPanel3";
        tableLayoutPanel3.Text = "";
        tableLayoutPanel3.Location = new global::System.Drawing.Point(12, 25);
        tableLayoutPanel3.Size = new global::System.Drawing.Size(1134, 49);
        tableLayoutPanel3.Margin = new global::System.Windows.Forms.Padding(3, 3, 3, 3);
        tableLayoutPanel3.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel3.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel3.AutoSize = false;
        tableLayoutPanel3.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel3.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel3.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel3.BackColor = Color.White;
        tableLayoutPanel3.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel3.TabIndex = 0;
        tableLayoutPanel3.TabStop = false;
        tableLayoutPanel3.AccessibleName = null;
        tableLayoutPanel3.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel3.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel3.RowCount = 1;
        tableLayoutPanel3.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel3.AutoScroll = false;
        tableLayoutPanel3.Visible = true;
        tableLayoutPanel3.ColumnCount = 6;
        tableLayoutPanel3.Enabled = true;
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 190F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 230F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 150F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label4
        label4.Name = "label4";
        label4.Text = "Tồn kho dưới";
        label4.Location = new global::System.Drawing.Point(0, 2);
        label4.Size = new global::System.Drawing.Size(178, 23);
        label4.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        label4.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label4.Dock = global::System.Windows.Forms.DockStyle.Top;

        label4.AutoSize = false;
        label4.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label4.ForeColor = Color.FromArgb(15, 23, 42);
        label4.BackColor = Color.White;
        label4.Cursor = global::System.Windows.Forms.Cursors.Default;
        label4.TabIndex = 0;
        label4.TabStop = false;
        label4.AccessibleName = null;
        label4.AutoEllipsis = false;
        label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label4.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label4.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label4.Visible = true;
        label4.Enabled = true;

        // nudNguongTon
        nudNguongTon.Minimum = 1M;
        nudNguongTon.Maximum = 100000M;
        nudNguongTon.DecimalPlaces = 0;
        nudNguongTon.Name = "nudNguongTon";
        nudNguongTon.Text = "10";
        nudNguongTon.Location = new global::System.Drawing.Point(190, 2);
        nudNguongTon.Size = new global::System.Drawing.Size(88, 30);
        nudNguongTon.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        nudNguongTon.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        nudNguongTon.Dock = global::System.Windows.Forms.DockStyle.Top;

        nudNguongTon.AutoSize = false;
        nudNguongTon.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        nudNguongTon.ForeColor=Color.FromArgb(15,23,42);
        nudNguongTon.BackColor=Color.White;
        nudNguongTon.Cursor = global::System.Windows.Forms.Cursors.Default;
        nudNguongTon.TabIndex = 1;
        nudNguongTon.TabStop = true;
        nudNguongTon.AccessibleName = null;
        nudNguongTon.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
        nudNguongTon.BorderStyle=BorderStyle.FixedSingle;
        nudNguongTon.ReadOnly = false;
        nudNguongTon.ThousandsSeparator = false;
        nudNguongTon.Increment = 1M;
        nudNguongTon.AutoScroll = false;
        nudNguongTon.Visible = true;
        nudNguongTon.Enabled = true;
        nudNguongTon.Value = 10M;

        // label5
        label5.Name = "label5";
        label5.Text = "Hạn còn dưới (ngày)";
        label5.Location = new global::System.Drawing.Point(290, 2);
        label5.Size = new global::System.Drawing.Size(218, 23);
        label5.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        label5.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label5.Dock = global::System.Windows.Forms.DockStyle.Top;

        label5.AutoSize = false;
        label5.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label5.ForeColor = Color.FromArgb(15, 23, 42);
        label5.BackColor = Color.White;
        label5.Cursor = global::System.Windows.Forms.Cursors.Default;
        label5.TabIndex = 2;
        label5.TabStop = false;
        label5.AccessibleName = null;
        label5.AutoEllipsis = false;
        label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label5.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label5.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label5.Visible = true;
        label5.Enabled = true;

        // nudSoNgay
        nudSoNgay.Minimum = 1M;
        nudSoNgay.Maximum = 3650M;
        nudSoNgay.DecimalPlaces = 0;
        nudSoNgay.Name = "nudSoNgay";
        nudSoNgay.Text = "30";
        nudSoNgay.Location = new global::System.Drawing.Point(520, 2);
        nudSoNgay.Size = new global::System.Drawing.Size(88, 30);
        nudSoNgay.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        nudSoNgay.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        nudSoNgay.Dock = global::System.Windows.Forms.DockStyle.Top;

        nudSoNgay.AutoSize = false;
        nudSoNgay.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        nudSoNgay.ForeColor=Color.FromArgb(15,23,42);
        nudSoNgay.BackColor=Color.White;
        nudSoNgay.Cursor = global::System.Windows.Forms.Cursors.Default;
        nudSoNgay.TabIndex = 3;
        nudSoNgay.TabStop = true;
        nudSoNgay.AccessibleName = null;
        nudSoNgay.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
        nudSoNgay.BorderStyle=BorderStyle.FixedSingle;
        nudSoNgay.ReadOnly = false;
        nudSoNgay.ThousandsSeparator = false;
        nudSoNgay.Increment = 1M;
        nudSoNgay.AutoScroll = false;
        nudSoNgay.Visible = true;
        nudSoNgay.Enabled = true;
        nudSoNgay.Value = 30M;

        // btnLoc
        btnLoc.Name = "btnLoc";
        btnLoc.Text = "Lọc / Làm mới";
        btnLoc.Location = new global::System.Drawing.Point(620, 2);
        btnLoc.Size = new global::System.Drawing.Size(138, 32);
        btnLoc.Margin = new global::System.Windows.Forms.Padding(0, 1, 12, 2);
        btnLoc.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnLoc.Dock = global::System.Windows.Forms.DockStyle.Top;

        btnLoc.AutoSize = false;
        btnLoc.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnLoc.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnLoc.ForeColor = Color.FromArgb(15, 23, 42);
        btnLoc.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnLoc.TabIndex = 5;
        btnLoc.TabStop = true;
        btnLoc.AccessibleName = null;
        btnLoc.AutoEllipsis = false;
        btnLoc.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnLoc.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnLoc.Visible = true;
        btnLoc.Enabled = true;
        btnLoc.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // grpSapHet
        grpSapHet.Name = "grpSapHet";
        grpSapHet.Text = "Thuốc sắp hết hàng";
        grpSapHet.Location = new global::System.Drawing.Point(3, 90);
        grpSapHet.Size = new global::System.Drawing.Size(1152, 239);
        grpSapHet.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        grpSapHet.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        grpSapHet.Dock = global::System.Windows.Forms.DockStyle.Fill;

        grpSapHet.AutoSize = false;
        grpSapHet.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        grpSapHet.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        grpSapHet.ForeColor = Color.FromArgb(15, 23, 42);
        grpSapHet.BackColor = Color.White;
        grpSapHet.Cursor = global::System.Windows.Forms.Cursors.Default;
        grpSapHet.TabIndex = 1;
        grpSapHet.TabStop = false;
        grpSapHet.AccessibleName = null;
        grpSapHet.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        grpSapHet.Visible = true;
        grpSapHet.Enabled = true;

        // dgvSapHet
        dgvSapHet.Name = "dgvSapHet";
        dgvSapHet.Text = "";
        dgvSapHet.Location = new global::System.Drawing.Point(12, 25);
        dgvSapHet.Size = new global::System.Drawing.Size(1128, 202);
        dgvSapHet.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        dgvSapHet.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHet.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvSapHet.AutoSize = false;
        dgvSapHet.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHet.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHet.BackColor = Color.White;
        dgvSapHet.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvSapHet.TabIndex = 0;
        dgvSapHet.TabStop = true;
        dgvSapHet.AccessibleName = null;
        dgvSapHet.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvSapHet.ReadOnly = true;
        dgvSapHet.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvSapHet.AllowUserToAddRows = false;
        dgvSapHet.AllowUserToDeleteRows = false;
        dgvSapHet.AllowUserToResizeRows = false;
        dgvSapHet.AllowUserToResizeColumns = true;
        dgvSapHet.RowHeadersVisible = false;
        dgvSapHet.MultiSelect = false;
        dgvSapHet.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvSapHet.ShowCellToolTips = true;
        dgvSapHet.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvSapHet.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
        dgvSapHet.BackgroundColor = Color.White;
        dgvSapHet.GridColor = Color.FromArgb(226, 232, 240);
        dgvSapHet.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvSapHet.EnableHeadersVisualStyles = false;
        dgvSapHet.ColumnHeadersHeight = 36;
        dgvSapHet.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSapHet.ColumnHeadersVisible = true;
        dgvSapHet.Visible = true;
        dgvSapHet.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvSapHetDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvSapHetDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHetDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvSapHetDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvSapHetDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetDefaultCellStyle.Format = "";
        dgvSapHetDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvSapHetDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvSapHetDefaultCellStyle.NullValue = "";
        dgvSapHet.DefaultCellStyle = dgvSapHetDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvSapHetColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHetColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetColumnHeadersDefaultCellStyle.Format = "";
        dgvSapHetColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvSapHetColumnHeadersDefaultCellStyle.NullValue = "";
        dgvSapHet.ColumnHeadersDefaultCellStyle = dgvSapHetColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvSapHetAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetAlternatingRowsDefaultCellStyle.Font = null;
        dgvSapHetAlternatingRowsDefaultCellStyle.Format = "";
        dgvSapHetAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvSapHet.AlternatingRowsDefaultCellStyle = dgvSapHetAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvSapHetRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvSapHetRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvSapHetRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvSapHetRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvSapHetRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetRowHeadersDefaultCellStyle.Format = "";
        dgvSapHetRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvSapHetRowHeadersDefaultCellStyle.NullValue = "";
        dgvSapHet.RowHeadersDefaultCellStyle = dgvSapHetRowHeadersDefaultCellStyle;
        dgvSapHet.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn1Style.Font = null;
        dgvSapHetColumn1Style.Format = "";
        dgvSapHetColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn2Style.Font = null;
        dgvSapHetColumn2Style.Format = "";
        dgvSapHetColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn3Style.Font = null;
        dgvSapHetColumn3Style.Format = "";
        dgvSapHetColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn4Style.Font = null;
        dgvSapHetColumn4Style.Format = "";
        dgvSapHetColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetColumn4Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetColumn5Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetColumn5Style.Font = null;
        dgvSapHetColumn5Style.Format = "";
        dgvSapHetColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetColumn5Style.NullValue = "";
        

        // grpSapHetHan
        grpSapHetHan.Name = "grpSapHetHan";
        grpSapHetHan.Text = "Lô sắp hết hạn";
        grpSapHetHan.Location = new global::System.Drawing.Point(3, 337);
        grpSapHetHan.Size = new global::System.Drawing.Size(1152, 240);
        grpSapHetHan.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        grpSapHetHan.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        grpSapHetHan.Dock = global::System.Windows.Forms.DockStyle.Fill;

        grpSapHetHan.AutoSize = false;
        grpSapHetHan.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        grpSapHetHan.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        grpSapHetHan.ForeColor = Color.FromArgb(15, 23, 42);
        grpSapHetHan.BackColor = Color.White;
        grpSapHetHan.Cursor = global::System.Windows.Forms.Cursors.Default;
        grpSapHetHan.TabIndex = 2;
        grpSapHetHan.TabStop = false;
        grpSapHetHan.AccessibleName = null;
        grpSapHetHan.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        grpSapHetHan.Visible = true;
        grpSapHetHan.Enabled = true;

        // dgvSapHetHan
        dgvSapHetHan.Name = "dgvSapHetHan";
        dgvSapHetHan.Text = "";
        dgvSapHetHan.Location = new global::System.Drawing.Point(12, 25);
        dgvSapHetHan.Size = new global::System.Drawing.Size(1128, 203);
        dgvSapHetHan.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        dgvSapHetHan.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHan.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvSapHetHan.AutoSize = false;
        dgvSapHetHan.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetHan.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHetHan.BackColor = Color.White;
        dgvSapHetHan.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvSapHetHan.TabIndex = 0;
        dgvSapHetHan.TabStop = true;
        dgvSapHetHan.AccessibleName = null;
        dgvSapHetHan.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvSapHetHan.ReadOnly = true;
        dgvSapHetHan.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvSapHetHan.AllowUserToAddRows = false;
        dgvSapHetHan.AllowUserToDeleteRows = false;
        dgvSapHetHan.AllowUserToResizeRows = false;
        dgvSapHetHan.AllowUserToResizeColumns = true;
        dgvSapHetHan.RowHeadersVisible = false;
        dgvSapHetHan.MultiSelect = false;
        dgvSapHetHan.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvSapHetHan.ShowCellToolTips = true;
        dgvSapHetHan.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvSapHetHan.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
        dgvSapHetHan.BackgroundColor = Color.White;
        dgvSapHetHan.GridColor = Color.FromArgb(226, 232, 240);
        dgvSapHetHan.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvSapHetHan.EnableHeadersVisualStyles = false;
        dgvSapHetHan.ColumnHeadersHeight = 36;
        dgvSapHetHan.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSapHetHan.ColumnHeadersVisible = true;
        dgvSapHetHan.Visible = true;
        dgvSapHetHan.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvSapHetHanDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvSapHetHanDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHetHanDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvSapHetHanDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvSapHetHanDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetHanDefaultCellStyle.Format = "";
        dgvSapHetHanDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvSapHetHanDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvSapHetHanDefaultCellStyle.NullValue = "";
        dgvSapHetHan.DefaultCellStyle = dgvSapHetHanDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvSapHetHanColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvSapHetHanColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetHanColumnHeadersDefaultCellStyle.Format = "";
        dgvSapHetHanColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvSapHetHanColumnHeadersDefaultCellStyle.NullValue = "";
        dgvSapHetHan.ColumnHeadersDefaultCellStyle = dgvSapHetHanColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.Font = null;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.Format = "";
        dgvSapHetHanAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvSapHetHan.AlternatingRowsDefaultCellStyle = dgvSapHetHanAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvSapHetHanRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvSapHetHanRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvSapHetHanRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvSapHetHanRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvSapHetHanRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvSapHetHanRowHeadersDefaultCellStyle.Format = "";
        dgvSapHetHanRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvSapHetHanRowHeadersDefaultCellStyle.NullValue = "";
        dgvSapHetHan.RowHeadersDefaultCellStyle = dgvSapHetHanRowHeadersDefaultCellStyle;
        dgvSapHetHan.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn1Style.Font = null;
        dgvSapHetHanColumn1Style.Format = "";
        dgvSapHetHanColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn2Style.Font = null;
        dgvSapHetHanColumn2Style.Format = "";
        dgvSapHetHanColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn3Style.Font = null;
        dgvSapHetHanColumn3Style.Format = "";
        dgvSapHetHanColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn4Style.Font = null;
        dgvSapHetHanColumn4Style.Format = "dd/MM/yyyy";
        dgvSapHetHanColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn4Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn5Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn5Style.Font = null;
        dgvSapHetHanColumn5Style.Format = "";
        dgvSapHetHanColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn5Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn6Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn6Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn6Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn6Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn6Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn6Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn6Style.Font = null;
        dgvSapHetHanColumn6Style.Format = "";
        dgvSapHetHanColumn6Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn6Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn6Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvSapHetHanColumn7Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvSapHetHanColumn7Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvSapHetHanColumn7Style.BackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn7Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn7Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn7Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvSapHetHanColumn7Style.Font = null;
        dgvSapHetHanColumn7Style.Format = "";
        dgvSapHetHanColumn7Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvSapHetHanColumn7Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvSapHetHanColumn7Style.NullValue = "";
        
        this.Controls.Add(posLayout);
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(tableLayoutPanel1, 0, 1);
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        groupBox2.Controls.Add(tableLayoutPanel3);
        tableLayoutPanel3.Controls.Add(label4, 0, 0);
        tableLayoutPanel3.Controls.Add(nudNguongTon, 1, 0);
        tableLayoutPanel3.Controls.Add(label5, 2, 0);
        tableLayoutPanel3.Controls.Add(nudSoNgay, 3, 0);
        tableLayoutPanel3.Controls.Add(btnLoc, 4, 0);
        tableLayoutPanel1.Controls.Add(grpSapHet, 0, 1);
        grpSapHet.Controls.Add(dgvSapHet);
        tableLayoutPanel1.Controls.Add(grpSapHetHan, 0, 2);
        grpSapHetHan.Controls.Add(dgvSapHetHan);
        AutoScaleDimensions = new global::System.Drawing.SizeF(120F, 120F);
        AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Dpi;
        BackColor = Color.FromArgb(248,250,252);
        ForeColor = global::System.Drawing.SystemColors.ControlText;
        Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        ClientSize = new global::System.Drawing.Size(1182, 653);
        MinimumSize = new global::System.Drawing.Size(1060, 700);
        MaximumSize = new global::System.Drawing.Size(0, 0);
        FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimizeBox = true;
        StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
        Text = "Cảnh báo tồn kho và hạn dùng | Pharmacy";
        KeyPreview = false;
        ShowIcon = true;
        ShowInTaskbar = true;
        Name = "frmCanhBao";
        btnLoc.Click += btnLoc_Click;
        dgvSapHet.DataBindingComplete += dgvSapHet_DataBindingComplete;
        dgvSapHetHan.DataBindingComplete += dgvSapHetHan_DataBindingComplete;
        Load += frmCanhBao_Load;
        ((global::System.ComponentModel.ISupportInitialize)dgvSapHetHan).EndInit();
        grpSapHetHan.ResumeLayout(false);
        grpSapHetHan.PerformLayout();
                //
        // dgvSapHetColumn1
        //
        this.dgvSapHetColumn1.Name = "MaThuoc";
        this.dgvSapHetColumn1.HeaderText = "Mã thuốc";
        this.dgvSapHetColumn1.Visible = true;
        this.dgvSapHetColumn1.ReadOnly = true;
        this.dgvSapHetColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetColumn1.Resizable = DataGridViewTriState.False;
        this.dgvSapHetColumn1.DataPropertyName = "MaThuoc";
        this.dgvSapHetColumn1.DefaultCellStyle = dgvSapHetColumn1Style;
        this.dgvSapHetColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn1.MinimumWidth = 5;
        this.dgvSapHetColumn1.Width = 125;
        this.dgvSapHetColumn1.FillWeight = 48F;
        //
        // dgvSapHetColumn2
        //
        this.dgvSapHetColumn2.Name = "TenThuoc";
        this.dgvSapHetColumn2.HeaderText = "Tên thuốc";
        this.dgvSapHetColumn2.Visible = true;
        this.dgvSapHetColumn2.ReadOnly = true;
        this.dgvSapHetColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetColumn2.Resizable = DataGridViewTriState.False;
        this.dgvSapHetColumn2.DataPropertyName = "TenThuoc";
        this.dgvSapHetColumn2.DefaultCellStyle = dgvSapHetColumn2Style;
        this.dgvSapHetColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn2.MinimumWidth = 5;
        this.dgvSapHetColumn2.Width = 230;
        this.dgvSapHetColumn2.FillWeight = 220F;
        //
        // dgvSapHetColumn3
        //
        this.dgvSapHetColumn3.Name = "DonViTinh";
        this.dgvSapHetColumn3.HeaderText = "Đơn vị tính";
        this.dgvSapHetColumn3.Visible = true;
        this.dgvSapHetColumn3.ReadOnly = true;
        this.dgvSapHetColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetColumn3.Resizable = DataGridViewTriState.False;
        this.dgvSapHetColumn3.DataPropertyName = "DonViTinh";
        this.dgvSapHetColumn3.DefaultCellStyle = dgvSapHetColumn3Style;
        this.dgvSapHetColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn3.MinimumWidth = 5;
        this.dgvSapHetColumn3.Width = 134;
        this.dgvSapHetColumn3.FillWeight = 48F;
        //
        // dgvSapHetColumn4
        //
        this.dgvSapHetColumn4.Name = "NhaCungCap";
        this.dgvSapHetColumn4.HeaderText = "Nhà cung cấp";
        this.dgvSapHetColumn4.Visible = true;
        this.dgvSapHetColumn4.ReadOnly = true;
        this.dgvSapHetColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetColumn4.Resizable = DataGridViewTriState.False;
        this.dgvSapHetColumn4.DataPropertyName = "NhaCungCap";
        this.dgvSapHetColumn4.DefaultCellStyle = dgvSapHetColumn4Style;
        this.dgvSapHetColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn4.MinimumWidth = 5;
        this.dgvSapHetColumn4.Width = 220;
        this.dgvSapHetColumn4.FillWeight = 220F;
        //
        // dgvSapHetColumn5
        //
        this.dgvSapHetColumn5.Name = "TonKho";
        this.dgvSapHetColumn5.HeaderText = "Tồn có thể bán";
        this.dgvSapHetColumn5.Visible = true;
        this.dgvSapHetColumn5.ReadOnly = true;
        this.dgvSapHetColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetColumn5.Resizable = DataGridViewTriState.False;
        this.dgvSapHetColumn5.DataPropertyName = "TonKho";
        this.dgvSapHetColumn5.DefaultCellStyle = dgvSapHetColumn5Style;
        this.dgvSapHetColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetColumn5.MinimumWidth = 5;
        this.dgvSapHetColumn5.Width = 175;
        this.dgvSapHetColumn5.FillWeight = 112F;
        //
        // dgvSapHetHanColumn1
        //
        this.dgvSapHetHanColumn1.Name = "MaThuoc";
        this.dgvSapHetHanColumn1.HeaderText = "Mã thuốc";
        this.dgvSapHetHanColumn1.Visible = true;
        this.dgvSapHetHanColumn1.ReadOnly = true;
        this.dgvSapHetHanColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn1.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn1.DataPropertyName = "MaThuoc";
        this.dgvSapHetHanColumn1.DefaultCellStyle = dgvSapHetHanColumn1Style;
        this.dgvSapHetHanColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn1.MinimumWidth = 5;
        this.dgvSapHetHanColumn1.Width = 125;
        this.dgvSapHetHanColumn1.FillWeight = 48F;
        //
        // dgvSapHetHanColumn2
        //
        this.dgvSapHetHanColumn2.Name = "TenThuoc";
        this.dgvSapHetHanColumn2.HeaderText = "Tên thuốc";
        this.dgvSapHetHanColumn2.Visible = true;
        this.dgvSapHetHanColumn2.ReadOnly = true;
        this.dgvSapHetHanColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn2.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn2.DataPropertyName = "TenThuoc";
        this.dgvSapHetHanColumn2.DefaultCellStyle = dgvSapHetHanColumn2Style;
        this.dgvSapHetHanColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn2.MinimumWidth = 5;
        this.dgvSapHetHanColumn2.Width = 230;
        this.dgvSapHetHanColumn2.FillWeight = 220F;
        //
        // dgvSapHetHanColumn3
        //
        this.dgvSapHetHanColumn3.Name = "SoLo";
        this.dgvSapHetHanColumn3.HeaderText = "Số lô";
        this.dgvSapHetHanColumn3.Visible = true;
        this.dgvSapHetHanColumn3.ReadOnly = true;
        this.dgvSapHetHanColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn3.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn3.DataPropertyName = "SoLo";
        this.dgvSapHetHanColumn3.DefaultCellStyle = dgvSapHetHanColumn3Style;
        this.dgvSapHetHanColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn3.MinimumWidth = 5;
        this.dgvSapHetHanColumn3.Width = 170;
        this.dgvSapHetHanColumn3.FillWeight = 64F;
        //
        // dgvSapHetHanColumn4
        //
        this.dgvSapHetHanColumn4.Name = "HanDung";
        this.dgvSapHetHanColumn4.HeaderText = "Hạn sử dụng";
        this.dgvSapHetHanColumn4.Visible = true;
        this.dgvSapHetHanColumn4.ReadOnly = true;
        this.dgvSapHetHanColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn4.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn4.DataPropertyName = "HanDung";
        this.dgvSapHetHanColumn4.DefaultCellStyle = dgvSapHetHanColumn4Style;
        this.dgvSapHetHanColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn4.MinimumWidth = 5;
        this.dgvSapHetHanColumn4.Width = 148;
        this.dgvSapHetHanColumn4.FillWeight = 88F;
        //
        // dgvSapHetHanColumn5
        //
        this.dgvSapHetHanColumn5.Name = "SoNgayConLai";
        this.dgvSapHetHanColumn5.HeaderText = "Số ngày còn lại";
        this.dgvSapHetHanColumn5.Visible = true;
        this.dgvSapHetHanColumn5.ReadOnly = true;
        this.dgvSapHetHanColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn5.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn5.DataPropertyName = "SoNgayConLai";
        this.dgvSapHetHanColumn5.DefaultCellStyle = dgvSapHetHanColumn5Style;
        this.dgvSapHetHanColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn5.MinimumWidth = 5;
        this.dgvSapHetHanColumn5.Width = 166;
        this.dgvSapHetHanColumn5.FillWeight = 104F;
        //
        // dgvSapHetHanColumn6
        //
        this.dgvSapHetHanColumn6.Name = "TonLo";
        this.dgvSapHetHanColumn6.HeaderText = "Tồn của lô";
        this.dgvSapHetHanColumn6.Visible = true;
        this.dgvSapHetHanColumn6.ReadOnly = true;
        this.dgvSapHetHanColumn6.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn6.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn6.DataPropertyName = "TonLo";
        this.dgvSapHetHanColumn6.DefaultCellStyle = dgvSapHetHanColumn6Style;
        this.dgvSapHetHanColumn6.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn6.MinimumWidth = 5;
        this.dgvSapHetHanColumn6.Width = 130;
        this.dgvSapHetHanColumn6.FillWeight = 104F;
        //
        // dgvSapHetHanColumn7
        //
        this.dgvSapHetHanColumn7.Name = "TrangThai";
        this.dgvSapHetHanColumn7.HeaderText = "Trạng thái";
        this.dgvSapHetHanColumn7.Visible = true;
        this.dgvSapHetHanColumn7.ReadOnly = true;
        this.dgvSapHetHanColumn7.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvSapHetHanColumn7.Resizable = DataGridViewTriState.False;
        this.dgvSapHetHanColumn7.DataPropertyName = "TrangThai";
        this.dgvSapHetHanColumn7.DefaultCellStyle = dgvSapHetHanColumn7Style;
        this.dgvSapHetHanColumn7.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvSapHetHanColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvSapHetHanColumn7.MinimumWidth = 5;
        this.dgvSapHetHanColumn7.Width = 220;
        this.dgvSapHetHanColumn7.FillWeight = 104F;
        //
        // colDaXuLy
        //
        this.colDaXuLy.Name = "DaXuLy";
        this.colDaXuLy.HeaderText = "Đã xem";
        this.colDaXuLy.ToolTipText = "Đánh dấu đã xem trong phiên này; không khóa lô hoặc thay đổi tồn kho.";
        this.colDaXuLy.ReadOnly = false;
        this.colDaXuLy.FalseValue = false;
        this.colDaXuLy.TrueValue = true;
        this.colDaXuLy.ThreeState = false;
        this.colDaXuLy.Width = 111;
        this.colDaXuLy.MinimumWidth = 5;
        this.colDaXuLy.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colDaXuLy.Resizable = DataGridViewTriState.False;

        this.dgvSapHet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvSapHet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvSapHetColumn1, this.dgvSapHetColumn2, this.dgvSapHetColumn3, this.dgvSapHetColumn4, this.dgvSapHetColumn5 });

        this.dgvSapHetHan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvSapHetHan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvSapHetHanColumn1, this.dgvSapHetHanColumn2, this.dgvSapHetHanColumn3, this.dgvSapHetHanColumn4, this.dgvSapHetHanColumn5, this.dgvSapHetHanColumn6, this.dgvSapHetHanColumn7, this.colDaXuLy });
        ((global::System.ComponentModel.ISupportInitialize)dgvSapHet).EndInit();
        grpSapHet.ResumeLayout(false);
        grpSapHet.PerformLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudSoNgay).EndInit();
        ((global::System.ComponentModel.ISupportInitialize)nudNguongTon).EndInit();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        posLayout.ResumeLayout(false);
        posLayout.PerformLayout();
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnLoc.BackColor=Color.FromArgb(15, 118, 110);
        btnLoc.FlatStyle = FlatStyle.Flat;
        btnLoc.FlatAppearance.BorderSize = 0;
        btnLoc.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLoc.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLoc.Cursor = Cursors.Hand;
        btnLoc.UseVisualStyleBackColor = false;btnLoc.ForeColor=Color.White;
        dgvSapHet.BackgroundColor=Color.White;dgvSapHet.EnableHeadersVisualStyles=false;
        dgvSapHet.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvSapHet.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvSapHet.DefaultCellStyle.BackColor=Color.White;dgvSapHet.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvSapHet.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvSapHet.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvSapHet.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(242, 246, 248);
        dgvSapHetHan.BackgroundColor=Color.White;dgvSapHetHan.EnableHeadersVisualStyles=false;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvSapHetHan.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvSapHetHan.DefaultCellStyle.BackColor=Color.White;dgvSapHetHan.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvSapHetHan.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvSapHetHan.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvSapHetHan.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(242, 246, 248);
        dgvSapHet.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvSapHet.MultiSelect=false;
        dgvSapHet.AllowUserToResizeColumns=false;
        dgvSapHet.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHet.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvSapHet.ColumnHeadersDefaultCellStyle.BackColor;
        dgvSapHet.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvSapHet.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvSapHet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHet.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        dgvSapHetHan.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvSapHetHan.MultiSelect=false;
        dgvSapHetHan.AllowUserToResizeColumns=false;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHetHan.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvSapHetHan.ColumnHeadersDefaultCellStyle.BackColor;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvSapHetHan.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvSapHetHan.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHetHan.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        dgvSapHetHan.ReadOnly=false;
        dgvSapHet.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHet.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHet.ColumnHeadersHeight=43;
        dgvSapHetHan.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvSapHetHan.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvSapHetHan.ColumnHeadersHeight=43;
        
        
        
        
        
        
        
        
        
        
        
        
        
        ResumeLayout(false);
        PerformLayout();
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

}
