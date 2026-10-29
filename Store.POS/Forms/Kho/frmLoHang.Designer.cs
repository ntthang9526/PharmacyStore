using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmLoHang
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        colTrangThaiLo=new global::System.Windows.Forms.DataGridViewTextBoxColumn();
        colTonCoTheBan=new global::System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvLoHangColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
        btnDoiTrangThai = new global::System.Windows.Forms.Button();
        components = new global::System.ComponentModel.Container();
        posLayout = new global::System.Windows.Forms.TableLayoutPanel();
        posHeading = new global::System.Windows.Forms.Label();
        tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox2 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
        label4 = new global::System.Windows.Forms.Label();
        txtTimKiem = new global::System.Windows.Forms.TextBox();
        btnTimKiem = new global::System.Windows.Forms.Button();
        groupBox5 = new global::System.Windows.Forms.GroupBox();
        dgvLoHang = new global::System.Windows.Forms.DataGridView();
        
        
        
        
        tableLayoutPanel6 = new global::System.Windows.Forms.TableLayoutPanel();
        lblKetQua = new global::System.Windows.Forms.Label();
        tableLayoutPanel7 = new global::System.Windows.Forms.TableLayoutPanel();
        btnLamMoi = new global::System.Windows.Forms.Button();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        groupBox5.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvLoHang).BeginInit();
        tableLayoutPanel6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
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
        posLayout.ForeColor = global::System.Drawing.SystemColors.ControlText;
        posLayout.BackColor = Color.FromArgb(248,250,252);
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
        posHeading.Text = "Lô hàng";
        posHeading.Location = new global::System.Drawing.Point(15, 12);
        posHeading.Size = new global::System.Drawing.Size(1152, 48);
        posHeading.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        posHeading.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        posHeading.Dock = global::System.Windows.Forms.DockStyle.Fill;

        posHeading.AutoSize = false;
        posHeading.Font = new global::System.Drawing.Font("Segoe UI Semibold", 18F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        posHeading.ForeColor = global::System.Drawing.SystemColors.ControlText;
        posHeading.BackColor = Color.FromArgb(248,250,252);
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
        tableLayoutPanel1.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel1.BackColor = Color.FromArgb(248,250,252);
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
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 92F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 48F));

        // groupBox2
        groupBox2.Name = "groupBox2";
        groupBox2.Text = "Tìm lô hàng";
        groupBox2.Location = new global::System.Drawing.Point(0, 0);
        groupBox2.Size = new global::System.Drawing.Size(1158, 92);
        groupBox2.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox2.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox2.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox2.AutoSize = false;
        groupBox2.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox2.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox2.ForeColor = global::System.Drawing.SystemColors.ControlText;
        groupBox2.BackColor = Color.FromArgb(248,250,252);
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
        tableLayoutPanel3.Size = new global::System.Drawing.Size(1134, 55);
        tableLayoutPanel3.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 12);
        tableLayoutPanel3.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel3.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel3.AutoSize = false;
        tableLayoutPanel3.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel3.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel3.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel3.BackColor = Color.FromArgb(248,250,252);
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
        tableLayoutPanel3.ColumnCount = 3;
        tableLayoutPanel3.Enabled = true;
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label4
        label4.Name = "label4";
        label4.Text = "Từ khóa";
        label4.Location = new global::System.Drawing.Point(3, 0);
        label4.Size = new global::System.Drawing.Size(114, 55);
        label4.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label4.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label4.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label4.AutoSize = false;
        label4.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label4.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label4.BackColor = Color.FromArgb(248,250,252);
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

        // txtTimKiem
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.Text = "";
        txtTimKiem.Location = new global::System.Drawing.Point(120, 7);
        txtTimKiem.Size = new global::System.Drawing.Size(882, 30);
        txtTimKiem.Margin = new global::System.Windows.Forms.Padding(0, 7, 12, 0);
        txtTimKiem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        txtTimKiem.Dock = global::System.Windows.Forms.DockStyle.Top;

        txtTimKiem.AutoSize=false;
        txtTimKiem.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtTimKiem.ForeColor=Color.FromArgb(15,23,42);
        txtTimKiem.BackColor=Color.White;
        txtTimKiem.Cursor = global::System.Windows.Forms.Cursors.IBeam;
        txtTimKiem.TabIndex = 1;
        txtTimKiem.TabStop = true;
        txtTimKiem.AccessibleName = null;
        txtTimKiem.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        txtTimKiem.BorderStyle=BorderStyle.FixedSingle;
        txtTimKiem.Multiline = false;
        txtTimKiem.ReadOnly = false;
        txtTimKiem.MaxLength = 200;
        txtTimKiem.PlaceholderText = "Số lô, tên thuốc, nhà cung cấp, nhóm hoặc mã vạch";
        txtTimKiem.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Normal;
        txtTimKiem.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtTimKiem.Visible = true;
        txtTimKiem.Enabled = true;

        // btnTimKiem
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.Location = new global::System.Drawing.Point(1014, 2);
        btnTimKiem.Size = new global::System.Drawing.Size(120, 34);
        btnTimKiem.Margin = new global::System.Windows.Forms.Padding(0, 5, 0, 5);
        btnTimKiem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnTimKiem.Dock = global::System.Windows.Forms.DockStyle.Top;

        btnTimKiem.AutoSize = false;
        btnTimKiem.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnTimKiem.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnTimKiem.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnTimKiem.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnTimKiem.TabIndex = 2;
        btnTimKiem.TabStop = true;
        btnTimKiem.AccessibleName = null;
        btnTimKiem.AutoEllipsis = false;
        btnTimKiem.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnTimKiem.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnTimKiem.Visible = true;
        btnTimKiem.Enabled = true;
        btnTimKiem.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // groupBox5
        groupBox5.Name = "groupBox5";
        groupBox5.Text = "Danh sách lô hàng";
        groupBox5.Location = new global::System.Drawing.Point(0, 92);
        groupBox5.Size = new global::System.Drawing.Size(1158, 441);
        groupBox5.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox5.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox5.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox5.AutoSize = false;
        groupBox5.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox5.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox5.ForeColor = global::System.Drawing.SystemColors.ControlText;
        groupBox5.BackColor = Color.FromArgb(248,250,252);
        groupBox5.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox5.TabIndex = 1;
        groupBox5.TabStop = false;
        groupBox5.AccessibleName = null;
        groupBox5.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox5.Visible = true;
        groupBox5.Enabled = true;

        // dgvLoHang
        dgvLoHang.Name = "dgvLoHang";
        dgvLoHang.Text = "";
        dgvLoHang.Location = new global::System.Drawing.Point(12, 25);
        dgvLoHang.Size = new global::System.Drawing.Size(1134, 404);
        dgvLoHang.Margin = new global::System.Windows.Forms.Padding(3, 3, 3, 3);
        dgvLoHang.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHang.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvLoHang.AutoSize = false;
        dgvLoHang.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvLoHang.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvLoHang.BackColor = Color.FromArgb(248,250,252);
        dgvLoHang.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvLoHang.TabIndex = 0;
        dgvLoHang.TabStop = true;
        dgvLoHang.AccessibleName = null;
        dgvLoHang.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvLoHang.ReadOnly = true;
        dgvLoHang.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvLoHang.AllowUserToAddRows = false;
        dgvLoHang.AllowUserToDeleteRows = false;
        dgvLoHang.AllowUserToResizeRows = false;
        dgvLoHang.AllowUserToResizeColumns = true;
        dgvLoHang.RowHeadersVisible = false;
        dgvLoHang.MultiSelect = false;
        dgvLoHang.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvLoHang.ShowCellToolTips = true;
        dgvLoHang.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvLoHang.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
        dgvLoHang.BackgroundColor = global::System.Drawing.SystemColors.ScrollBar;
        dgvLoHang.GridColor = global::System.Drawing.SystemColors.ControlLight;
        dgvLoHang.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvLoHang.EnableHeadersVisualStyles = false;
        dgvLoHang.ColumnHeadersHeight = 82;
        dgvLoHang.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvLoHang.ColumnHeadersVisible = true;
        dgvLoHang.Visible = true;
        dgvLoHang.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvLoHangDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvLoHangDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvLoHangDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvLoHangDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvLoHangDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvLoHangDefaultCellStyle.Format = "";
        dgvLoHangDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvLoHangDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvLoHangDefaultCellStyle.NullValue = "";
        dgvLoHang.DefaultCellStyle = dgvLoHangDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvLoHangColumnHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvLoHangColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvLoHangColumnHeadersDefaultCellStyle.Format = "";
        dgvLoHangColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvLoHangColumnHeadersDefaultCellStyle.NullValue = "";
        dgvLoHang.ColumnHeadersDefaultCellStyle = dgvLoHangColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvLoHangAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangAlternatingRowsDefaultCellStyle.Font = null;
        dgvLoHangAlternatingRowsDefaultCellStyle.Format = "";
        dgvLoHangAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvLoHang.AlternatingRowsDefaultCellStyle = dgvLoHangAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvLoHangRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvLoHangRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvLoHangRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvLoHangRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvLoHangRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvLoHangRowHeadersDefaultCellStyle.Format = "";
        dgvLoHangRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvLoHangRowHeadersDefaultCellStyle.NullValue = "";
        dgvLoHang.RowHeadersDefaultCellStyle = dgvLoHangRowHeadersDefaultCellStyle;
        dgvLoHang.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn1Style.Font = null;
        dgvLoHangColumn1Style.Format = "";
        dgvLoHangColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn1Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn2Style.Font = null;
        dgvLoHangColumn2Style.Format = "";
        dgvLoHangColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn2Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn3Style.Font = null;
        dgvLoHangColumn3Style.Format = "";
        dgvLoHangColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn3Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn4Style.Font = null;
        dgvLoHangColumn4Style.Format = "";
        dgvLoHangColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvLoHangColumn4Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn5Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn5Style.Font = null;
        dgvLoHangColumn5Style.Format = "";
        dgvLoHangColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvLoHangColumn5Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn6Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn6Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn6Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn6Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn6Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn6Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn6Style.Font = null;
        dgvLoHangColumn6Style.Format = "dd/MM/yyyy";
        dgvLoHangColumn6Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn6Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn6Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn7Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn7Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        dgvLoHangColumn7Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn7Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn7Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn7Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn7Style.Font = null;
        dgvLoHangColumn7Style.Format = "";
        dgvLoHangColumn7Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn7Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn7Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn8Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn8Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        dgvLoHangColumn8Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn8Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn8Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn8Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn8Style.Font = null;
        dgvLoHangColumn8Style.Format = "";
        dgvLoHangColumn8Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn8Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn8Style.NullValue = "—";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvLoHangColumn9Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvLoHangColumn9Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvLoHangColumn9Style.BackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn9Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn9Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn9Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvLoHangColumn9Style.Font = null;
        dgvLoHangColumn9Style.Format = "dd/MM/yyyy HH:mm";
        dgvLoHangColumn9Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvLoHangColumn9Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvLoHangColumn9Style.NullValue = "—";
        
        
        
        

        // tableLayoutPanel6
        tableLayoutPanel6.Name = "tableLayoutPanel6";
        tableLayoutPanel6.Text = "";
        tableLayoutPanel6.Location = new global::System.Drawing.Point(0, 533);
        tableLayoutPanel6.Size = new global::System.Drawing.Size(1158, 48);
        tableLayoutPanel6.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel6.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel6.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel6.AutoSize = false;
        tableLayoutPanel6.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel6.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel6.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel6.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel6.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel6.TabIndex = 2;
        tableLayoutPanel6.TabStop = false;
        tableLayoutPanel6.AccessibleName = null;
        tableLayoutPanel6.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel6.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel6.RowCount = 1;
        tableLayoutPanel6.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel6.AutoScroll = false;
        tableLayoutPanel6.Visible = true;
        tableLayoutPanel6.ColumnCount = 2;
        tableLayoutPanel6.Enabled = true;
        tableLayoutPanel6.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel6.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 280F));
        tableLayoutPanel6.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // lblKetQua
        lblKetQua.Name = "lblKetQua";
        lblKetQua.Text = "";
        lblKetQua.Location = new global::System.Drawing.Point(0, 0);
        lblKetQua.Size = new global::System.Drawing.Size(866, 48);
        lblKetQua.Margin = new global::System.Windows.Forms.Padding(0, 0, 12, 0);
        lblKetQua.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        lblKetQua.Dock = global::System.Windows.Forms.DockStyle.Fill;

        lblKetQua.AutoSize = false;
        lblKetQua.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        lblKetQua.ForeColor = global::System.Drawing.SystemColors.ControlText;
        lblKetQua.BackColor = Color.FromArgb(248,250,252);
        lblKetQua.Cursor = global::System.Windows.Forms.Cursors.Default;
        lblKetQua.TabIndex = 0;
        lblKetQua.TabStop = false;
        lblKetQua.AccessibleName = null;
        lblKetQua.AutoEllipsis = false;
        lblKetQua.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        lblKetQua.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        lblKetQua.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        lblKetQua.Visible = true;
        lblKetQua.Enabled = true;

        // tableLayoutPanel7
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.Text = "";
        tableLayoutPanel7.Location = new global::System.Drawing.Point(878, 0);
        tableLayoutPanel7.Size = new global::System.Drawing.Size(280, 48);
        tableLayoutPanel7.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel7.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel7.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel7.AutoSize = true;
        tableLayoutPanel7.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel7.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel7.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel7.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel7.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel7.TabIndex = 1;
        tableLayoutPanel7.TabStop = false;
        tableLayoutPanel7.AccessibleName = null;
        tableLayoutPanel7.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel7.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel7.RowCount = 1;
        tableLayoutPanel7.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel7.AutoScroll = false;
        tableLayoutPanel7.Visible = true;
        tableLayoutPanel7.ColumnCount = 2;
        tableLayoutPanel7.Enabled = true;
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 140F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 44F));

        // btnLamMoi
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.Location = new global::System.Drawing.Point(0, 4);
        btnLamMoi.Size = new global::System.Drawing.Size(136, 44);
        btnLamMoi.Margin = new global::System.Windows.Forms.Padding(0, 4, 0, 4);
        btnLamMoi.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnLamMoi.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnLamMoi.AutoSize = false;
        btnLamMoi.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnLamMoi.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnLamMoi.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnLamMoi.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnLamMoi.TabIndex = 0;
        btnLamMoi.TabStop = true;
        btnLamMoi.AccessibleName = null;
        btnLamMoi.AutoEllipsis = false;
        btnLamMoi.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnLamMoi.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnLamMoi.Visible = true;
        btnLamMoi.Enabled = true;
        btnLamMoi.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        this.Controls.Add(posLayout);
        posLayout.Controls.Add(posHeading, 0, 0);
        posLayout.Controls.Add(tableLayoutPanel1, 0, 1);
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        groupBox2.Controls.Add(tableLayoutPanel3);
        tableLayoutPanel3.Controls.Add(label4, 0, 0);
        tableLayoutPanel3.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel3.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel1.Controls.Add(groupBox5, 0, 1);
        groupBox5.Controls.Add(dgvLoHang);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel6, 0, 2);
        tableLayoutPanel6.Controls.Add(lblKetQua, 0, 0);
        tableLayoutPanel6.Controls.Add(tableLayoutPanel7, 1, 0);
        tableLayoutPanel7.Controls.Add(btnLamMoi, 0, 0);
        tableLayoutPanel7.Controls.Add(btnDoiTrangThai, 1, 0);
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
        Text = "Lô hàng | Hiệu thuốc ABC";
        KeyPreview = false;
        ShowIcon = true;
        ShowInTaskbar = true;
        Name = "frmLoHang";
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        tableLayoutPanel6.ResumeLayout(false);
        tableLayoutPanel6.PerformLayout();
                //
        // dgvLoHangColumn1
        //
        this.dgvLoHangColumn1.Name = "MaLo";
        this.dgvLoHangColumn1.HeaderText = "Mã lô";
        this.dgvLoHangColumn1.Visible = true;
        this.dgvLoHangColumn1.ReadOnly = true;
        this.dgvLoHangColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn1.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn1.DataPropertyName = "MaLo";
        this.dgvLoHangColumn1.DefaultCellStyle = dgvLoHangColumn1Style;
        this.dgvLoHangColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn1.MinimumWidth = 5;
        this.dgvLoHangColumn1.Width = 110;
        this.dgvLoHangColumn1.FillWeight = 64F;
        //
        // dgvLoHangColumn2
        //
        this.dgvLoHangColumn2.Name = "SoLo";
        this.dgvLoHangColumn2.HeaderText = "Số lô";
        this.dgvLoHangColumn2.Visible = true;
        this.dgvLoHangColumn2.ReadOnly = true;
        this.dgvLoHangColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn2.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn2.DataPropertyName = "SoLo";
        this.dgvLoHangColumn2.DefaultCellStyle = dgvLoHangColumn2Style;
        this.dgvLoHangColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn2.MinimumWidth = 5;
        this.dgvLoHangColumn2.Width = 170;
        this.dgvLoHangColumn2.FillWeight = 64F;
        //
        // dgvLoHangColumn3
        //
        this.dgvLoHangColumn3.Name = "TenThuoc";
        this.dgvLoHangColumn3.HeaderText = "Tên thuốc";
        this.dgvLoHangColumn3.Visible = true;
        this.dgvLoHangColumn3.ReadOnly = true;
        this.dgvLoHangColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn3.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn3.DataPropertyName = "TenThuoc";
        this.dgvLoHangColumn3.DefaultCellStyle = dgvLoHangColumn3Style;
        this.dgvLoHangColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn3.MinimumWidth = 5;
        this.dgvLoHangColumn3.Width = 230;
        this.dgvLoHangColumn3.FillWeight = 220F;
        //
        // dgvLoHangColumn4
        //
        this.dgvLoHangColumn4.Name = "TenNCC";
        this.dgvLoHangColumn4.HeaderText = "Nhà cung cấp";
        this.dgvLoHangColumn4.Visible = true;
        this.dgvLoHangColumn4.ReadOnly = true;
        this.dgvLoHangColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn4.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn4.DataPropertyName = "TenNCC";
        this.dgvLoHangColumn4.DefaultCellStyle = dgvLoHangColumn4Style;
        this.dgvLoHangColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn4.MinimumWidth = 5;
        this.dgvLoHangColumn4.Width = 220;
        this.dgvLoHangColumn4.FillWeight = 220F;
        //
        // dgvLoHangColumn5
        //
        this.dgvLoHangColumn5.Name = "TenNhom";
        this.dgvLoHangColumn5.HeaderText = "Nhóm thuốc";
        this.dgvLoHangColumn5.Visible = true;
        this.dgvLoHangColumn5.ReadOnly = true;
        this.dgvLoHangColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn5.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn5.DataPropertyName = "TenNhom";
        this.dgvLoHangColumn5.DefaultCellStyle = dgvLoHangColumn5Style;
        this.dgvLoHangColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn5.MinimumWidth = 5;
        this.dgvLoHangColumn5.Width = 230;
        this.dgvLoHangColumn5.FillWeight = 220F;
        //
        // dgvLoHangColumn6
        //
        this.dgvLoHangColumn6.Name = "HanDung";
        this.dgvLoHangColumn6.HeaderText = "Hạn sử dụng";
        this.dgvLoHangColumn6.Visible = true;
        this.dgvLoHangColumn6.ReadOnly = true;
        this.dgvLoHangColumn6.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn6.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn6.DataPropertyName = "HanDung";
        this.dgvLoHangColumn6.DefaultCellStyle = dgvLoHangColumn6Style;
        this.dgvLoHangColumn6.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn6.MinimumWidth = 5;
        this.dgvLoHangColumn6.Width = 148;
        this.dgvLoHangColumn6.FillWeight = 88F;
        //
        // dgvLoHangColumn7
        //
        this.dgvLoHangColumn7.Name = "TonKho";
        this.dgvLoHangColumn7.HeaderText = "Tồn kho";
        this.dgvLoHangColumn7.Visible = true;
        this.dgvLoHangColumn7.ReadOnly = true;
        this.dgvLoHangColumn7.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn7.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn7.DataPropertyName = "TonKho";
        this.dgvLoHangColumn7.DefaultCellStyle = dgvLoHangColumn7Style;
        this.dgvLoHangColumn7.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn7.MinimumWidth = 5;
        this.dgvLoHangColumn7.Width = 112;
        this.dgvLoHangColumn7.FillWeight = 120F;
        //
        // dgvLoHangColumn8
        //
        this.dgvLoHangColumn8.Name = "SoLuongDaNhap";
        this.dgvLoHangColumn8.HeaderText = "Tổng đã nhập";
        this.dgvLoHangColumn8.Visible = true;
        this.dgvLoHangColumn8.ReadOnly = true;
        this.dgvLoHangColumn8.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn8.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn8.DataPropertyName = "SoLuongDaNhap";
        this.dgvLoHangColumn8.DefaultCellStyle = dgvLoHangColumn8Style;
        this.dgvLoHangColumn8.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn8.MinimumWidth = 5;
        this.dgvLoHangColumn8.Width = 157;
        this.dgvLoHangColumn8.FillWeight = 120F;
        //
        // dgvLoHangColumn9
        //
        this.dgvLoHangColumn9.Name = "NgayNhapGanNhat";
        this.dgvLoHangColumn9.HeaderText = "Nhập gần nhất";
        this.dgvLoHangColumn9.Visible = true;
        this.dgvLoHangColumn9.ReadOnly = true;
        this.dgvLoHangColumn9.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvLoHangColumn9.Resizable = DataGridViewTriState.False;
        this.dgvLoHangColumn9.DataPropertyName = "NgayNhapGanNhat";
        this.dgvLoHangColumn9.DefaultCellStyle = dgvLoHangColumn9Style;
        this.dgvLoHangColumn9.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvLoHangColumn9.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvLoHangColumn9.MinimumWidth = 5;
        this.dgvLoHangColumn9.Width = 175;
        this.dgvLoHangColumn9.FillWeight = 128F;
        //
        // colGiaNhap
        //
        this.colGiaNhap.Name = "GiaNhapGanNhat";
        this.colGiaNhap.DataPropertyName = "GiaNhapGanNhat";
        this.colGiaNhap.HeaderText = "Giá nhập gần nhất";
        this.colGiaNhap.ReadOnly = true;
        this.colGiaNhap.DefaultCellStyle.Format = "N0";
        this.colGiaNhap.DefaultCellStyle.NullValue = "—";
        this.colGiaNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.colGiaNhap.Resizable = DataGridViewTriState.False;
        this.colGiaNhap.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.colGiaNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colGiaNhap.MinimumWidth = 5;
        this.colGiaNhap.Width = 193;
        this.colGiaNhap.FillWeight = 192F;

        this.dgvLoHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvLoHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvLoHangColumn1, this.dgvLoHangColumn2, this.dgvLoHangColumn3, colTrangThaiLo, this.dgvLoHangColumn4, this.dgvLoHangColumn5, this.dgvLoHangColumn6, this.dgvLoHangColumn7, colTonCoTheBan, this.dgvLoHangColumn8, this.dgvLoHangColumn9, this.colGiaNhap });
        ((global::System.ComponentModel.ISupportInitialize)dgvLoHang).EndInit();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        posLayout.ResumeLayout(false);
        posLayout.PerformLayout();
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnTimKiem.BackColor=Color.FromArgb(15, 118, 110);
        btnTimKiem.FlatStyle = FlatStyle.Flat;
        btnTimKiem.FlatAppearance.BorderSize = 0;
        btnTimKiem.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnTimKiem.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnTimKiem.Cursor = Cursors.Hand;
        btnTimKiem.UseVisualStyleBackColor = false;btnTimKiem.ForeColor=Color.White;
        btnLamMoi.BackColor=Color.FromArgb(15, 118, 110);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.UseVisualStyleBackColor = false;btnLamMoi.ForeColor=Color.White;

        dgvLoHang.BackgroundColor=Color.White;dgvLoHang.EnableHeadersVisualStyles=false;
        dgvLoHang.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvLoHang.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvLoHang.DefaultCellStyle.BackColor=Color.White;dgvLoHang.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvLoHang.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvLoHang.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvLoHang.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvLoHang.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvLoHang.MultiSelect=false;
        dgvLoHang.AllowUserToResizeColumns=false;
        dgvLoHang.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvLoHang.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvLoHang.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvLoHang.ColumnHeadersDefaultCellStyle.BackColor;
        dgvLoHang.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvLoHang.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvLoHang.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvLoHang.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                dgvLoHang.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvLoHang.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvLoHang.ColumnHeadersHeight=43;
        
        
        
        
        
        
        
        
        
        
        colTrangThaiLo.Name="colTrangThaiLo";
        colTrangThaiLo.DataPropertyName="TrangThaiLo";
        colTrangThaiLo.HeaderText="Trạng thái lô";
        colTrangThaiLo.Width=190;
        colTrangThaiLo.MinimumWidth=50;
        colTrangThaiLo.AutoSizeMode=global::System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        colTrangThaiLo.ReadOnly=true;
        colTrangThaiLo.Resizable=global::System.Windows.Forms.DataGridViewTriState.False;
        colTonCoTheBan.Name="colTonCoTheBan";
        colTonCoTheBan.DataPropertyName="TonCoTheBan";
        colTonCoTheBan.HeaderText="Tồn có thể bán";
        colTonCoTheBan.Width=175;
        colTonCoTheBan.MinimumWidth=50;
        colTonCoTheBan.AutoSizeMode=global::System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        colTonCoTheBan.ReadOnly=true;
        colTonCoTheBan.Resizable=global::System.Windows.Forms.DataGridViewTriState.False;
        colTonCoTheBan.DefaultCellStyle.Format="N0";
        colTonCoTheBan.DefaultCellStyle.Alignment=global::System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        btnDoiTrangThai.Name="btnDoiTrangThai";
        btnDoiTrangThai.Text="Tạm khóa";
        btnDoiTrangThai.Size=new global::System.Drawing.Size(132,36);
        btnDoiTrangThai.Dock=DockStyle.Fill;
        btnDoiTrangThai.Margin=new Padding(4,4,0,4);
        btnDoiTrangThai.BackColor=Color.FromArgb(15,118,110);
        btnDoiTrangThai.ForeColor=Color.White;
        btnDoiTrangThai.FlatStyle=FlatStyle.Flat;
        btnDoiTrangThai.FlatAppearance.BorderSize=0;
        btnDoiTrangThai.Enabled=false;
        ResumeLayout(false);
        PerformLayout();
    }

    private global::System.Windows.Forms.Button btnDoiTrangThai;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn colTrangThaiLo;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn colTonCoTheBan;
    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.Label label4;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.GroupBox groupBox5;
    private global::System.Windows.Forms.DataGridView dgvLoHang;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn7;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn8;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvLoHangColumn9;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    private global::System.Windows.Forms.Label lblKetQua;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.Button btnLamMoi;
    private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
}

}