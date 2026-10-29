using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmNhaCungCap
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dgvNhaCungCapColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvNhaCungCapColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvNhaCungCapColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvNhaCungCapColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        components = new global::System.ComponentModel.Container();
        posLayout = new global::System.Windows.Forms.TableLayoutPanel();
        posHeading = new global::System.Windows.Forms.Label();
        tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox2 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel4 = new global::System.Windows.Forms.TableLayoutPanel();
        label5 = new global::System.Windows.Forms.Label();
        txtTimKiem = new global::System.Windows.Forms.TextBox();
        btnTimKiem = new global::System.Windows.Forms.Button();
        dgvNhaCungCap = new global::System.Windows.Forms.DataGridView();
        
        
        groupBox6 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel7 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel8 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel11 = new global::System.Windows.Forms.TableLayoutPanel();
        label12 = new global::System.Windows.Forms.Label();
        txtTenNCC = new global::System.Windows.Forms.TextBox();
        tableLayoutPanel13 = new global::System.Windows.Forms.TableLayoutPanel();
        label14 = new global::System.Windows.Forms.Label();
        txtDienThoai = new global::System.Windows.Forms.TextBox();
        tableLayoutPanel15 = new global::System.Windows.Forms.TableLayoutPanel();
        label16 = new global::System.Windows.Forms.Label();
        txtDiaChi = new global::System.Windows.Forms.TextBox();
        tableLayoutPanel17 = new global::System.Windows.Forms.TableLayoutPanel();
        btnThem = new global::System.Windows.Forms.Button();
        btnSua = new global::System.Windows.Forms.Button();
        btnXoa = new global::System.Windows.Forms.Button();
        btnLamMoi = new global::System.Windows.Forms.Button();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvNhaCungCap).BeginInit();
        groupBox6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel11.SuspendLayout();
        tableLayoutPanel13.SuspendLayout();
        tableLayoutPanel15.SuspendLayout();
        tableLayoutPanel17.SuspendLayout();
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
        posHeading.Text = "Nhà cung cấp";
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
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel1.AutoScroll = false;
        tableLayoutPanel1.Visible = true;
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.Enabled = true;
        tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 354F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // groupBox2
        groupBox2.Name = "groupBox2";
        groupBox2.Text = "Danh sách nhà cung cấp";
        groupBox2.Location = new global::System.Drawing.Point(0, 0);
        groupBox2.Size = new global::System.Drawing.Size(792, 581);
        groupBox2.Margin = new global::System.Windows.Forms.Padding(0, 0, 12, 0);
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
        tableLayoutPanel3.Size = new global::System.Drawing.Size(768, 544);
        tableLayoutPanel3.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
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
        tableLayoutPanel3.RowCount = 2;
        tableLayoutPanel3.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel3.AutoScroll = false;
        tableLayoutPanel3.Visible = true;
        tableLayoutPanel3.ColumnCount = 1;
        tableLayoutPanel3.Enabled = true;
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // tableLayoutPanel4
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.Text = "";
        tableLayoutPanel4.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel4.Size = new global::System.Drawing.Size(768, 44);
        tableLayoutPanel4.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 12);
        tableLayoutPanel4.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel4.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel4.AutoSize = false;
        tableLayoutPanel4.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel4.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel4.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel4.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel4.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel4.TabIndex = 0;
        tableLayoutPanel4.TabStop = false;
        tableLayoutPanel4.AccessibleName = null;
        tableLayoutPanel4.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel4.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel4.RowCount = 1;
        tableLayoutPanel4.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel4.AutoScroll = false;
        tableLayoutPanel4.Visible = true;
        tableLayoutPanel4.ColumnCount = 3;
        tableLayoutPanel4.Enabled = true;
        tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel4.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label5
        label5.Name = "label5";
        label5.Text = "Từ khóa";
        label5.Location = new global::System.Drawing.Point(3, 0);
        label5.Size = new global::System.Drawing.Size(114, 44);
        label5.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label5.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label5.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label5.AutoSize = false;
        label5.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label5.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label5.BackColor = Color.FromArgb(248,250,252);
        label5.Cursor = global::System.Windows.Forms.Cursors.Default;
        label5.TabIndex = 0;
        label5.TabStop = false;
        label5.AccessibleName = null;
        label5.AutoEllipsis = false;
        label5.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label5.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label5.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label5.Visible = true;
        label5.Enabled = true;

        // txtTimKiem
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.Text = "";
        txtTimKiem.Location = new global::System.Drawing.Point(120, 7);
        txtTimKiem.Size = new global::System.Drawing.Size(516, 30);
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
        txtTimKiem.PlaceholderText = "Tên, điện thoại hoặc địa chỉ";
        txtTimKiem.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Normal;
        txtTimKiem.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtTimKiem.Visible = true;
        txtTimKiem.Enabled = true;

        // btnTimKiem
        btnTimKiem.Name = "btnTimKiem";
        btnTimKiem.Text = "Tìm kiếm";
        btnTimKiem.Location = new global::System.Drawing.Point(648, 2);
        btnTimKiem.Size = new global::System.Drawing.Size(120, 40);
        btnTimKiem.Margin = new global::System.Windows.Forms.Padding(0, 2, 0, 2);
        btnTimKiem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnTimKiem.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnTimKiem.AutoSize = false;
        btnTimKiem.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnTimKiem.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnTimKiem.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnTimKiem.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnTimKiem.TabIndex = 0;
        btnTimKiem.TabStop = true;
        btnTimKiem.AccessibleName = null;
        btnTimKiem.AutoEllipsis = false;
        btnTimKiem.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnTimKiem.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnTimKiem.Visible = true;
        btnTimKiem.Enabled = true;
        btnTimKiem.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // dgvNhaCungCap
        dgvNhaCungCap.Name = "dgvNhaCungCap";
        dgvNhaCungCap.Text = "";
        dgvNhaCungCap.Location = new global::System.Drawing.Point(0, 56);
        dgvNhaCungCap.Size = new global::System.Drawing.Size(768, 488);
        dgvNhaCungCap.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCap.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCap.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvNhaCungCap.AutoSize = false;
        dgvNhaCungCap.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvNhaCungCap.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvNhaCungCap.BackColor = Color.FromArgb(248,250,252);
        dgvNhaCungCap.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvNhaCungCap.TabIndex = 9;
        dgvNhaCungCap.TabStop = true;
        dgvNhaCungCap.AccessibleName = null;
        dgvNhaCungCap.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvNhaCungCap.ReadOnly = true;
        dgvNhaCungCap.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvNhaCungCap.AllowUserToAddRows = false;
        dgvNhaCungCap.AllowUserToDeleteRows = false;
        dgvNhaCungCap.AllowUserToResizeRows = false;
        dgvNhaCungCap.AllowUserToResizeColumns = true;
        dgvNhaCungCap.RowHeadersVisible = false;
        dgvNhaCungCap.MultiSelect = false;
        dgvNhaCungCap.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvNhaCungCap.ShowCellToolTips = true;
        dgvNhaCungCap.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvNhaCungCap.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
        dgvNhaCungCap.BackgroundColor = global::System.Drawing.SystemColors.ScrollBar;
        dgvNhaCungCap.GridColor = global::System.Drawing.SystemColors.ControlLight;
        dgvNhaCungCap.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvNhaCungCap.EnableHeadersVisualStyles = false;
        dgvNhaCungCap.ColumnHeadersHeight = 59;
        dgvNhaCungCap.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvNhaCungCap.ColumnHeadersVisible = true;
        dgvNhaCungCap.Visible = true;
        dgvNhaCungCap.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvNhaCungCapDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvNhaCungCapDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvNhaCungCapDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvNhaCungCapDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvNhaCungCapDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvNhaCungCapDefaultCellStyle.Format = "";
        dgvNhaCungCapDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvNhaCungCapDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvNhaCungCapDefaultCellStyle.NullValue = "";
        dgvNhaCungCap.DefaultCellStyle = dgvNhaCungCapDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvNhaCungCapColumnHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvNhaCungCapColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvNhaCungCapColumnHeadersDefaultCellStyle.Format = "";
        dgvNhaCungCapColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvNhaCungCapColumnHeadersDefaultCellStyle.NullValue = "";
        dgvNhaCungCap.ColumnHeadersDefaultCellStyle = dgvNhaCungCapColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.Font = null;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.Format = "";
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvNhaCungCapAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvNhaCungCap.AlternatingRowsDefaultCellStyle = dgvNhaCungCapAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvNhaCungCapRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvNhaCungCapRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvNhaCungCapRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvNhaCungCapRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvNhaCungCapRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvNhaCungCapRowHeadersDefaultCellStyle.Format = "";
        dgvNhaCungCapRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvNhaCungCapRowHeadersDefaultCellStyle.NullValue = "";
        dgvNhaCungCap.RowHeadersDefaultCellStyle = dgvNhaCungCapRowHeadersDefaultCellStyle;
        dgvNhaCungCap.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn1Style.Font = null;
        dgvNhaCungCapColumn1Style.Format = "";
        dgvNhaCungCapColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvNhaCungCapColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn2Style.Font = null;
        dgvNhaCungCapColumn2Style.Format = "";
        dgvNhaCungCapColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvNhaCungCapColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn3Style.Font = null;
        dgvNhaCungCapColumn3Style.Format = "";
        dgvNhaCungCapColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvNhaCungCapColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvNhaCungCapColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvNhaCungCapColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvNhaCungCapColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvNhaCungCapColumn4Style.Font = null;
        dgvNhaCungCapColumn4Style.Format = "";
        dgvNhaCungCapColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvNhaCungCapColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvNhaCungCapColumn4Style.NullValue = "";
        

        // groupBox6
        groupBox6.Name = "groupBox6";
        groupBox6.Text = "Thông tin chi tiết";
        groupBox6.Location = new global::System.Drawing.Point(804, 0);
        groupBox6.Size = new global::System.Drawing.Size(354, 581);
        groupBox6.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox6.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox6.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox6.AutoSize = false;
        groupBox6.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox6.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox6.ForeColor = global::System.Drawing.SystemColors.ControlText;
        groupBox6.BackColor = Color.FromArgb(248,250,252);
        groupBox6.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox6.TabIndex = 1;
        groupBox6.TabStop = false;
        groupBox6.AccessibleName = null;
        groupBox6.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox6.Visible = true;
        groupBox6.Enabled = true;

        // tableLayoutPanel7
        tableLayoutPanel7.Name = "tableLayoutPanel7";
        tableLayoutPanel7.Text = "";
        tableLayoutPanel7.Location = new global::System.Drawing.Point(12, 25);
        tableLayoutPanel7.Size = new global::System.Drawing.Size(330, 544);
        tableLayoutPanel7.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel7.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel7.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel7.AutoSize = false;
        tableLayoutPanel7.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel7.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel7.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel7.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel7.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel7.TabIndex = 0;
        tableLayoutPanel7.TabStop = false;
        tableLayoutPanel7.AccessibleName = null;
        tableLayoutPanel7.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel7.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel7.RowCount = 3;
        tableLayoutPanel7.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel7.AutoScroll = false;
        tableLayoutPanel7.Visible = true;
        tableLayoutPanel7.ColumnCount = 1;
        tableLayoutPanel7.Enabled = true;
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 168F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 88F));

        // tableLayoutPanel8
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.Text = "";
        tableLayoutPanel8.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel8.Size = new global::System.Drawing.Size(330, 224);
        tableLayoutPanel8.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Dock = global::System.Windows.Forms.DockStyle.Top;

        tableLayoutPanel8.AutoSize = true;
        tableLayoutPanel8.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanel8.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel8.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel8.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel8.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel8.TabIndex = 0;
        tableLayoutPanel8.TabStop = false;
        tableLayoutPanel8.AccessibleName = null;
        tableLayoutPanel8.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel8.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel8.RowCount = 3;
        tableLayoutPanel8.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel8.AutoScroll = false;
        tableLayoutPanel8.Visible = true;
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.Enabled = true;
        tableLayoutPanel8.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));

        // tableLayoutPanel11
        tableLayoutPanel11.Name = "tableLayoutPanel11";
        tableLayoutPanel11.Text = "";
        tableLayoutPanel11.Location = new global::System.Drawing.Point(0, 56);
        tableLayoutPanel11.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel11.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel11.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel11.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel11.AutoSize = false;
        tableLayoutPanel11.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel11.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel11.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel11.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel11.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel11.TabIndex = 1;
        tableLayoutPanel11.TabStop = false;
        tableLayoutPanel11.AccessibleName = null;
        tableLayoutPanel11.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel11.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel11.RowCount = 2;
        tableLayoutPanel11.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel11.AutoScroll = false;
        tableLayoutPanel11.Visible = true;
        tableLayoutPanel11.ColumnCount = 1;
        tableLayoutPanel11.Enabled = true;
        tableLayoutPanel11.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel11.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel11.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label12
        label12.Name = "label12";
        label12.Text = "Tên nhà cung cấp";
        label12.Location = new global::System.Drawing.Point(3, 0);
        label12.Size = new global::System.Drawing.Size(324, 22);
        label12.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label12.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label12.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label12.AutoSize = false;
        label12.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label12.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label12.BackColor = Color.FromArgb(248,250,252);
        label12.Cursor = global::System.Windows.Forms.Cursors.Default;
        label12.TabIndex = 0;
        label12.TabStop = false;
        label12.AccessibleName = null;
        label12.AutoEllipsis = false;
        label12.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label12.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label12.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label12.Visible = true;
        label12.Enabled = true;

        // txtTenNCC
        txtTenNCC.Name = "txtTenNCC";
        txtTenNCC.Text = "";
        txtTenNCC.Location = new global::System.Drawing.Point(0, 23);
        txtTenNCC.Size = new global::System.Drawing.Size(330, 30);
        txtTenNCC.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        txtTenNCC.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        txtTenNCC.Dock = global::System.Windows.Forms.DockStyle.Top;

        txtTenNCC.AutoSize=false;
        txtTenNCC.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtTenNCC.ForeColor=Color.FromArgb(15,23,42);
        txtTenNCC.BackColor=Color.White;
        txtTenNCC.Cursor = global::System.Windows.Forms.Cursors.IBeam;
        txtTenNCC.TabIndex = 6;
        txtTenNCC.TabStop = true;
        txtTenNCC.AccessibleName = null;
        txtTenNCC.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        txtTenNCC.BorderStyle=BorderStyle.FixedSingle;
        txtTenNCC.Multiline = false;
        txtTenNCC.ReadOnly = false;
        txtTenNCC.MaxLength = 32767;
        txtTenNCC.PlaceholderText = "";
        txtTenNCC.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Normal;
        txtTenNCC.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtTenNCC.Visible = true;
        txtTenNCC.Enabled = false;

        // tableLayoutPanel13
        tableLayoutPanel13.Name = "tableLayoutPanel13";
        tableLayoutPanel13.Text = "";
        tableLayoutPanel13.Location = new global::System.Drawing.Point(0, 112);
        tableLayoutPanel13.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel13.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel13.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel13.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel13.AutoSize = false;
        tableLayoutPanel13.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel13.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel13.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel13.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel13.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel13.TabIndex = 2;
        tableLayoutPanel13.TabStop = false;
        tableLayoutPanel13.AccessibleName = null;
        tableLayoutPanel13.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel13.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel13.RowCount = 2;
        tableLayoutPanel13.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel13.AutoScroll = false;
        tableLayoutPanel13.Visible = true;
        tableLayoutPanel13.ColumnCount = 1;
        tableLayoutPanel13.Enabled = true;
        tableLayoutPanel13.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel13.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel13.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label14
        label14.Name = "label14";
        label14.Text = "Số điện thoại";
        label14.Location = new global::System.Drawing.Point(3, 0);
        label14.Size = new global::System.Drawing.Size(324, 22);
        label14.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label14.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label14.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label14.AutoSize = false;
        label14.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label14.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label14.BackColor = Color.FromArgb(248,250,252);
        label14.Cursor = global::System.Windows.Forms.Cursors.Default;
        label14.TabIndex = 0;
        label14.TabStop = false;
        label14.AccessibleName = null;
        label14.AutoEllipsis = false;
        label14.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label14.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label14.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label14.Visible = true;
        label14.Enabled = true;

        // txtDienThoai
        txtDienThoai.Name = "txtDienThoai";
        txtDienThoai.Text = "";
        txtDienThoai.Location = new global::System.Drawing.Point(0, 23);
        txtDienThoai.Size = new global::System.Drawing.Size(330, 30);
        txtDienThoai.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        txtDienThoai.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        txtDienThoai.Dock = global::System.Windows.Forms.DockStyle.Top;

        txtDienThoai.AutoSize=false;
        txtDienThoai.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtDienThoai.ForeColor=Color.FromArgb(15,23,42);
        txtDienThoai.BackColor=Color.White;
        txtDienThoai.Cursor = global::System.Windows.Forms.Cursors.IBeam;
        txtDienThoai.TabIndex = 8;
        txtDienThoai.TabStop = true;
        txtDienThoai.AccessibleName = null;
        txtDienThoai.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        txtDienThoai.BorderStyle=BorderStyle.FixedSingle;
        txtDienThoai.Multiline = false;
        txtDienThoai.ReadOnly = false;
        txtDienThoai.MaxLength = 32767;
        txtDienThoai.PlaceholderText = "";
        txtDienThoai.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Normal;
        txtDienThoai.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtDienThoai.Visible = true;
        txtDienThoai.Enabled = false;

        // tableLayoutPanel15
        tableLayoutPanel15.Name = "tableLayoutPanel15";
        tableLayoutPanel15.Text = "";
        tableLayoutPanel15.Location = new global::System.Drawing.Point(0, 168);
        tableLayoutPanel15.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel15.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel15.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel15.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel15.AutoSize = false;
        tableLayoutPanel15.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel15.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel15.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel15.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel15.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel15.TabIndex = 3;
        tableLayoutPanel15.TabStop = false;
        tableLayoutPanel15.AccessibleName = null;
        tableLayoutPanel15.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel15.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel15.RowCount = 2;
        tableLayoutPanel15.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel15.AutoScroll = false;
        tableLayoutPanel15.Visible = true;
        tableLayoutPanel15.ColumnCount = 1;
        tableLayoutPanel15.Enabled = true;
        tableLayoutPanel15.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel15.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel15.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label16
        label16.Name = "label16";
        label16.Text = "Địa chỉ";
        label16.Location = new global::System.Drawing.Point(3, 0);
        label16.Size = new global::System.Drawing.Size(324, 22);
        label16.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label16.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label16.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label16.AutoSize = false;
        label16.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label16.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label16.BackColor = Color.FromArgb(248,250,252);
        label16.Cursor = global::System.Windows.Forms.Cursors.Default;
        label16.TabIndex = 0;
        label16.TabStop = false;
        label16.AccessibleName = null;
        label16.AutoEllipsis = false;
        label16.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label16.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label16.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label16.Visible = true;
        label16.Enabled = true;

        // txtDiaChi
        txtDiaChi.Name = "txtDiaChi";
        txtDiaChi.Text = "";
        txtDiaChi.Location = new global::System.Drawing.Point(0, 23);
        txtDiaChi.Size = new global::System.Drawing.Size(330, 30);
        txtDiaChi.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        txtDiaChi.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        txtDiaChi.Dock = global::System.Windows.Forms.DockStyle.Top;

        txtDiaChi.AutoSize=false;
        txtDiaChi.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtDiaChi.ForeColor=Color.FromArgb(15,23,42);
        txtDiaChi.BackColor=Color.White;
        txtDiaChi.Cursor = global::System.Windows.Forms.Cursors.IBeam;
        txtDiaChi.TabIndex = 7;
        txtDiaChi.TabStop = true;
        txtDiaChi.AccessibleName = null;
        txtDiaChi.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        txtDiaChi.BorderStyle=BorderStyle.FixedSingle;
        txtDiaChi.Multiline = false;
        txtDiaChi.ReadOnly = false;
        txtDiaChi.MaxLength = 32767;
        txtDiaChi.PlaceholderText = "";
        txtDiaChi.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Normal;
        txtDiaChi.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtDiaChi.Visible = true;
        txtDiaChi.Enabled = false;

        // tableLayoutPanel17
        tableLayoutPanel17.Name = "tableLayoutPanel17";
        tableLayoutPanel17.Text = "";
        tableLayoutPanel17.Location = new global::System.Drawing.Point(0, 412);
        tableLayoutPanel17.Size = new global::System.Drawing.Size(330, 88);
        tableLayoutPanel17.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel17.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel17.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel17.AutoSize = true;
        tableLayoutPanel17.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel17.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel17.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel17.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel17.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel17.TabIndex = 1;
        tableLayoutPanel17.TabStop = false;
        tableLayoutPanel17.AccessibleName = null;
        tableLayoutPanel17.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel17.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel17.RowCount = 2;
        tableLayoutPanel17.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel17.AutoScroll = false;
        tableLayoutPanel17.Visible = true;
        tableLayoutPanel17.ColumnCount = 2;
        tableLayoutPanel17.Enabled = true;
        tableLayoutPanel17.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel17.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel17.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel17.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50F));

        // btnThem
        btnThem.Name = "btnThem";
        btnThem.Text = "Thêm";
        btnThem.Location = new global::System.Drawing.Point(0, 4);
        btnThem.Size = new global::System.Drawing.Size(161, 40);
        btnThem.Margin = new global::System.Windows.Forms.Padding(0, 4, 4, 4);
        btnThem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnThem.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnThem.AutoSize = false;
        btnThem.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnThem.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnThem.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnThem.BackColor = global::System.Drawing.Color.FromArgb(255, 192, 255, 192);
        btnThem.TabIndex = 10;
        btnThem.TabStop = true;
        btnThem.AccessibleName = null;
        btnThem.AutoEllipsis = false;
        btnThem.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnThem.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnThem.Visible = true;
        btnThem.Enabled = true;
        btnThem.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnSua
        btnSua.Name = "btnSua";
        btnSua.Text = "Sửa";
        btnSua.Location = new global::System.Drawing.Point(169, 4);
        btnSua.Size = new global::System.Drawing.Size(161, 40);
        btnSua.Margin = new global::System.Windows.Forms.Padding(4, 4, 0, 4);
        btnSua.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnSua.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnSua.AutoSize = false;
        btnSua.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnSua.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnSua.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnSua.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnSua.TabIndex = 11;
        btnSua.TabStop = true;
        btnSua.AccessibleName = null;
        btnSua.AutoEllipsis = false;
        btnSua.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnSua.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnSua.Visible = true;
        btnSua.Enabled = true;
        btnSua.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnXoa
        btnXoa.Name = "btnXoa";
        btnXoa.Text = "Xóa";
        btnXoa.Location = new global::System.Drawing.Point(0, 48);
        btnXoa.Size = new global::System.Drawing.Size(161, 40);
        btnXoa.Margin = new global::System.Windows.Forms.Padding(0, 4, 4, 4);
        btnXoa.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnXoa.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnXoa.AutoSize = false;
        btnXoa.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnXoa.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnXoa.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnXoa.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 192, 192);
        btnXoa.TabIndex = 12;
        btnXoa.TabStop = true;
        btnXoa.AccessibleName = null;
        btnXoa.AutoEllipsis = false;
        btnXoa.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnXoa.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnXoa.Visible = true;
        btnXoa.Enabled = true;
        btnXoa.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnLamMoi
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.Location = new global::System.Drawing.Point(169, 48);
        btnLamMoi.Size = new global::System.Drawing.Size(161, 40);
        btnLamMoi.Margin = new global::System.Windows.Forms.Padding(4, 4, 0, 4);
        btnLamMoi.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnLamMoi.Dock = global::System.Windows.Forms.DockStyle.Fill;

        btnLamMoi.AutoSize = false;
        btnLamMoi.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnLamMoi.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnLamMoi.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnLamMoi.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnLamMoi.TabIndex = 13;
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
        tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
        tableLayoutPanel4.Controls.Add(label5, 0, 0);
        tableLayoutPanel4.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel4.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel3.Controls.Add(dgvNhaCungCap, 0, 1);
        tableLayoutPanel1.Controls.Add(groupBox6, 1, 0);
        groupBox6.Controls.Add(tableLayoutPanel7);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 0);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel11, -1, -1);
        tableLayoutPanel11.Controls.Add(label12, 0, 0);
        tableLayoutPanel11.Controls.Add(txtTenNCC, 0, 1);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel13, -1, -1);
        tableLayoutPanel13.Controls.Add(label14, 0, 0);
        tableLayoutPanel13.Controls.Add(txtDienThoai, 0, 1);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel15, -1, -1);
        tableLayoutPanel15.Controls.Add(label16, 0, 0);
        tableLayoutPanel15.Controls.Add(txtDiaChi, 0, 1);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel17, 0, 2);
        tableLayoutPanel17.Controls.Add(btnThem, 0, 0);
        tableLayoutPanel17.Controls.Add(btnSua, 1, 0);
        tableLayoutPanel17.Controls.Add(btnXoa, 0, 1);
        tableLayoutPanel17.Controls.Add(btnLamMoi, 1, 1);
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
        Text = "Nhà cung cấp | Hiệu thuốc ABC";
        KeyPreview = false;
        ShowIcon = true;
        ShowInTaskbar = true;
        Name = "frmNhaCungCap";
        dgvNhaCungCap.CellClick += dgvNhaCungCap_CellClick;
        btnThem.Click += btnThem_Click;
        btnSua.Click += btnSua_Click;
        btnXoa.Click += btnXoa_Click;
        btnLamMoi.Click += btnLamMoi_Click;
                btnTimKiem.Click += btnTimKiem_Click;
        Load += frmNhaCungCap_Load;
        tableLayoutPanel17.ResumeLayout(false);
        tableLayoutPanel17.PerformLayout();
        tableLayoutPanel15.ResumeLayout(false);
        tableLayoutPanel15.PerformLayout();
        tableLayoutPanel13.ResumeLayout(false);
        tableLayoutPanel13.PerformLayout();
        tableLayoutPanel11.ResumeLayout(false);
        tableLayoutPanel11.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel8.PerformLayout();
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
                //
        // dgvNhaCungCapColumn1
        //
        this.dgvNhaCungCapColumn1.Name = "MaNCC";
        this.dgvNhaCungCapColumn1.HeaderText = "Mã nhà cung cấp";
        this.dgvNhaCungCapColumn1.Visible = true;
        this.dgvNhaCungCapColumn1.ReadOnly = true;
        this.dgvNhaCungCapColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvNhaCungCapColumn1.Resizable = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn1.DataPropertyName = "MaNCC";
        this.dgvNhaCungCapColumn1.DefaultCellStyle = dgvNhaCungCapColumn1Style;
        this.dgvNhaCungCapColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvNhaCungCapColumn1.MinimumWidth = 5;
        this.dgvNhaCungCapColumn1.Width = 183;
        this.dgvNhaCungCapColumn1.FillWeight = 72F;
        //
        // dgvNhaCungCapColumn2
        //
        this.dgvNhaCungCapColumn2.Name = "TenNCC";
        this.dgvNhaCungCapColumn2.HeaderText = "Nhà cung cấp";
        this.dgvNhaCungCapColumn2.Visible = true;
        this.dgvNhaCungCapColumn2.ReadOnly = true;
        this.dgvNhaCungCapColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvNhaCungCapColumn2.Resizable = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn2.DataPropertyName = "TenNCC";
        this.dgvNhaCungCapColumn2.DefaultCellStyle = dgvNhaCungCapColumn2Style;
        this.dgvNhaCungCapColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvNhaCungCapColumn2.MinimumWidth = 5;
        this.dgvNhaCungCapColumn2.Width = 220;
        this.dgvNhaCungCapColumn2.FillWeight = 220F;
        //
        // dgvNhaCungCapColumn3
        //
        this.dgvNhaCungCapColumn3.Name = "DienThoai";
        this.dgvNhaCungCapColumn3.HeaderText = "Số điện thoại";
        this.dgvNhaCungCapColumn3.Visible = true;
        this.dgvNhaCungCapColumn3.ReadOnly = true;
        this.dgvNhaCungCapColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvNhaCungCapColumn3.Resizable = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn3.DataPropertyName = "DienThoai";
        this.dgvNhaCungCapColumn3.DefaultCellStyle = dgvNhaCungCapColumn3Style;
        this.dgvNhaCungCapColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvNhaCungCapColumn3.MinimumWidth = 5;
        this.dgvNhaCungCapColumn3.Width = 151;
        this.dgvNhaCungCapColumn3.FillWeight = 104F;
        //
        // dgvNhaCungCapColumn4
        //
        this.dgvNhaCungCapColumn4.Name = "DiaChi";
        this.dgvNhaCungCapColumn4.HeaderText = "Địa chỉ";
        this.dgvNhaCungCapColumn4.Visible = true;
        this.dgvNhaCungCapColumn4.ReadOnly = true;
        this.dgvNhaCungCapColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvNhaCungCapColumn4.Resizable = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn4.DataPropertyName = "DiaChi";
        this.dgvNhaCungCapColumn4.DefaultCellStyle = dgvNhaCungCapColumn4Style;
        this.dgvNhaCungCapColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvNhaCungCapColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvNhaCungCapColumn4.MinimumWidth = 5;
        this.dgvNhaCungCapColumn4.Width = 220;
        this.dgvNhaCungCapColumn4.FillWeight = 180F;

        this.dgvNhaCungCap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvNhaCungCap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvNhaCungCapColumn1, this.dgvNhaCungCapColumn2, this.dgvNhaCungCapColumn3, this.dgvNhaCungCapColumn4 });
        ((global::System.ComponentModel.ISupportInitialize)dgvNhaCungCap).EndInit();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
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
        btnThem.BackColor=Color.FromArgb(22, 101, 52);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
        btnThem.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
        btnThem.Cursor = Cursors.Hand;
        btnThem.UseVisualStyleBackColor = false;btnThem.ForeColor=Color.White;
        btnSua.BackColor=Color.FromArgb(15, 118, 110);
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnSua.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnSua.Cursor = Cursors.Hand;
        btnSua.UseVisualStyleBackColor = false;btnSua.ForeColor=Color.White;
        btnXoa.BackColor=Color.FromArgb(225, 29, 72);
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.FlatAppearance.BorderSize = 0;
        btnXoa.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 63, 94);
        btnXoa.FlatAppearance.MouseDownBackColor = Color.FromArgb(190, 18, 60);
        btnXoa.Cursor = Cursors.Hand;
        btnXoa.UseVisualStyleBackColor = false;btnXoa.ForeColor=Color.White;
        btnLamMoi.BackColor=Color.FromArgb(15, 118, 110);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.FlatAppearance.BorderSize = 0;
        btnLamMoi.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnLamMoi.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnLamMoi.Cursor = Cursors.Hand;
        btnLamMoi.UseVisualStyleBackColor = false;btnLamMoi.ForeColor=Color.White;

        dgvNhaCungCap.BackgroundColor=Color.White;dgvNhaCungCap.EnableHeadersVisualStyles=false;
        dgvNhaCungCap.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvNhaCungCap.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvNhaCungCap.DefaultCellStyle.BackColor=Color.White;dgvNhaCungCap.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvNhaCungCap.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvNhaCungCap.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvNhaCungCap.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvNhaCungCap.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvNhaCungCap.MultiSelect=false;
        dgvNhaCungCap.AllowUserToResizeColumns=false;
        dgvNhaCungCap.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhaCungCap.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhaCungCap.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvNhaCungCap.ColumnHeadersDefaultCellStyle.BackColor;
        dgvNhaCungCap.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvNhaCungCap.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvNhaCungCap.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvNhaCungCap.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
                dgvNhaCungCap.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvNhaCungCap.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvNhaCungCap.ColumnHeadersHeight=43;
        
        
        
        
        ResumeLayout(false);
        PerformLayout();
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.Label label5;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.DataGridView dgvNhaCungCap;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvNhaCungCapColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvNhaCungCapColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvNhaCungCapColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvNhaCungCapColumn4;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
    private global::System.Windows.Forms.Label label12;
    private global::System.Windows.Forms.TextBox txtTenNCC;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
    private global::System.Windows.Forms.Label label14;
    private global::System.Windows.Forms.TextBox txtDienThoai;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
    private global::System.Windows.Forms.Label label16;
    private global::System.Windows.Forms.TextBox txtDiaChi;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLamMoi;
}

}