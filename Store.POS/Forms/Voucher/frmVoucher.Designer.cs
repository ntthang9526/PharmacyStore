using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmVoucher
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dgvVoucherColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvVoucherColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        components = new global::System.ComponentModel.Container();
        posLayout = new global::System.Windows.Forms.TableLayoutPanel();
        posHeading = new global::System.Windows.Forms.Label();
        tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox3 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel4 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel5 = new global::System.Windows.Forms.TableLayoutPanel();
        label6 = new global::System.Windows.Forms.Label();
        txtTimKiem = new global::System.Windows.Forms.TextBox();
        btnTimKiem = new global::System.Windows.Forms.Button();
        dgvVoucher = new global::System.Windows.Forms.DataGridView();
        
        
        
        groupBox7 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel8 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel9 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel10 = new global::System.Windows.Forms.TableLayoutPanel();
        label11 = new global::System.Windows.Forms.Label();
        txtCode = new global::System.Windows.Forms.TextBox();
        tableLayoutPanel12 = new global::System.Windows.Forms.TableLayoutPanel();
        label13 = new global::System.Windows.Forms.Label();
        cboLoai = new global::System.Windows.Forms.ComboBox();
        tableLayoutPanel14 = new global::System.Windows.Forms.TableLayoutPanel();
        label15 = new global::System.Windows.Forms.Label();
        nudGiaTri = new global::System.Windows.Forms.NumericUpDown();
        tableLayoutPanel16 = new global::System.Windows.Forms.TableLayoutPanel();
        label17 = new global::System.Windows.Forms.Label();
        nudSoLuot = new global::System.Windows.Forms.NumericUpDown();
        tableLayoutPanel18 = new global::System.Windows.Forms.TableLayoutPanel();
        label19 = new global::System.Windows.Forms.Label();
        chkHoatDong = new global::System.Windows.Forms.CheckBox();
        tableLayoutPanel20 = new global::System.Windows.Forms.FlowLayoutPanel();
        btnThem = new global::System.Windows.Forms.Button();
        btnSua = new global::System.Windows.Forms.Button();
        btnLuu = new global::System.Windows.Forms.Button();
        btnHuy = new global::System.Windows.Forms.Button();
        btnXoa = new global::System.Windows.Forms.Button();
        btnLamMoi = new global::System.Windows.Forms.Button();
        lblTrangThai = new global::System.Windows.Forms.Label();
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        groupBox3.SuspendLayout();
        tableLayoutPanel4.SuspendLayout();
        tableLayoutPanel5.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvVoucher).BeginInit();
        groupBox7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel9.SuspendLayout();
        tableLayoutPanel10.SuspendLayout();
        tableLayoutPanel12.SuspendLayout();
        tableLayoutPanel14.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudGiaTri).BeginInit();
        tableLayoutPanel16.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudSoLuot).BeginInit();
        tableLayoutPanel18.SuspendLayout();
        tableLayoutPanel20.SuspendLayout();
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
        posHeading.Text = "Quản lý voucher";
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
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel1.AutoScroll = false;
        tableLayoutPanel1.Visible = true;
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.Enabled = true;
        tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 40F));

        // tableLayoutPanel2
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.Text = "";
        tableLayoutPanel2.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel2.Size = new global::System.Drawing.Size(1158, 541);
        tableLayoutPanel2.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel2.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel2.AutoSize = false;
        tableLayoutPanel2.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel2.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel2.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel2.BackColor = Color.FromArgb(242, 246, 248);
        tableLayoutPanel2.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel2.TabIndex = 0;
        tableLayoutPanel2.TabStop = false;
        tableLayoutPanel2.AccessibleName = null;
        tableLayoutPanel2.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel2.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel2.AutoScroll = false;
        tableLayoutPanel2.Visible = true;
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.Enabled = true;
        tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 354F));
        tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // groupBox3
        groupBox3.Name = "groupBox3";
        groupBox3.Text = "Danh sách voucher";
        groupBox3.Location = new global::System.Drawing.Point(0, 0);
        groupBox3.Size = new global::System.Drawing.Size(792, 541);
        groupBox3.Margin = new global::System.Windows.Forms.Padding(0, 0, 12, 0);
        groupBox3.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox3.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox3.AutoSize = false;
        groupBox3.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox3.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox3.ForeColor = Color.FromArgb(15, 23, 42);
        groupBox3.BackColor = Color.White;
        groupBox3.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox3.TabIndex = 0;
        groupBox3.TabStop = false;
        groupBox3.AccessibleName = null;
        groupBox3.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox3.Visible = true;
        groupBox3.Enabled = true;

        // tableLayoutPanel4
        tableLayoutPanel4.Name = "tableLayoutPanel4";
        tableLayoutPanel4.Text = "";
        tableLayoutPanel4.Location = new global::System.Drawing.Point(12, 25);
        tableLayoutPanel4.Size = new global::System.Drawing.Size(768, 504);
        tableLayoutPanel4.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel4.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel4.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel4.AutoSize = false;
        tableLayoutPanel4.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel4.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel4.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel4.BackColor = Color.White;
        tableLayoutPanel4.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel4.TabIndex = 0;
        tableLayoutPanel4.TabStop = false;
        tableLayoutPanel4.AccessibleName = null;
        tableLayoutPanel4.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel4.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel4.RowCount = 2;
        tableLayoutPanel4.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel4.AutoScroll = false;
        tableLayoutPanel4.Visible = true;
        tableLayoutPanel4.ColumnCount = 1;
        tableLayoutPanel4.Enabled = true;
        tableLayoutPanel4.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel4.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel4.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // tableLayoutPanel5
        tableLayoutPanel5.Name = "tableLayoutPanel5";
        tableLayoutPanel5.Text = "";
        tableLayoutPanel5.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel5.Size = new global::System.Drawing.Size(768, 44);
        tableLayoutPanel5.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 12);
        tableLayoutPanel5.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel5.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel5.AutoSize = false;
        tableLayoutPanel5.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel5.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel5.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel5.BackColor = Color.White;
        tableLayoutPanel5.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel5.TabIndex = 0;
        tableLayoutPanel5.TabStop = false;
        tableLayoutPanel5.AccessibleName = null;
        tableLayoutPanel5.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel5.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel5.RowCount = 1;
        tableLayoutPanel5.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel5.AutoScroll = false;
        tableLayoutPanel5.Visible = true;
        tableLayoutPanel5.ColumnCount = 3;
        tableLayoutPanel5.Enabled = true;
        tableLayoutPanel5.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel5.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel5.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 120F));
        tableLayoutPanel5.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label6
        label6.Name = "label6";
        label6.Text = "Mã voucher";
        label6.Location = new global::System.Drawing.Point(3, 0);
        label6.Size = new global::System.Drawing.Size(114, 44);
        label6.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label6.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label6.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label6.AutoSize = false;
        label6.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label6.ForeColor = Color.FromArgb(15, 23, 42);
        label6.BackColor = Color.White;
        label6.Cursor = global::System.Windows.Forms.Cursors.Default;
        label6.TabIndex = 0;
        label6.TabStop = false;
        label6.AccessibleName = null;
        label6.AutoEllipsis = false;
        label6.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label6.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label6.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label6.Visible = true;
        label6.Enabled = true;

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
        txtTimKiem.PlaceholderText = "Nhập mã voucher";
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
        btnTimKiem.ForeColor = Color.FromArgb(15, 23, 42);
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

        // dgvVoucher
        dgvVoucher.Name = "dgvVoucher";
        dgvVoucher.Text = "";
        dgvVoucher.Location = new global::System.Drawing.Point(0, 56);
        dgvVoucher.Size = new global::System.Drawing.Size(768, 448);
        dgvVoucher.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucher.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucher.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvVoucher.AutoSize = false;
        dgvVoucher.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvVoucher.ForeColor = Color.FromArgb(15, 23, 42);
        dgvVoucher.BackColor = Color.White;
        dgvVoucher.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvVoucher.TabIndex = 1;
        dgvVoucher.TabStop = true;
        dgvVoucher.AccessibleName = null;
        dgvVoucher.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvVoucher.ReadOnly = true;
        dgvVoucher.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvVoucher.AllowUserToAddRows = false;
        dgvVoucher.AllowUserToDeleteRows = false;
        dgvVoucher.AllowUserToResizeRows = false;
        dgvVoucher.AllowUserToResizeColumns = true;
        dgvVoucher.RowHeadersVisible = false;
        dgvVoucher.MultiSelect = false;
        dgvVoucher.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvVoucher.ShowCellToolTips = true;
        dgvVoucher.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvVoucher.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
        dgvVoucher.BackgroundColor = Color.White;
        dgvVoucher.GridColor = Color.FromArgb(226, 232, 240);
        dgvVoucher.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvVoucher.EnableHeadersVisualStyles = false;
        dgvVoucher.ColumnHeadersHeight = 36;
        dgvVoucher.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvVoucher.ColumnHeadersVisible = true;
        dgvVoucher.Visible = true;
        dgvVoucher.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvVoucherDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvVoucherDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvVoucherDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvVoucherDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvVoucherDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvVoucherDefaultCellStyle.Format = "";
        dgvVoucherDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvVoucherDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvVoucherDefaultCellStyle.NullValue = "";
        dgvVoucher.DefaultCellStyle = dgvVoucherDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvVoucherColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
        dgvVoucherColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvVoucherColumnHeadersDefaultCellStyle.Format = "";
        dgvVoucherColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvVoucherColumnHeadersDefaultCellStyle.NullValue = "";
        dgvVoucher.ColumnHeadersDefaultCellStyle = dgvVoucherColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvVoucherAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherAlternatingRowsDefaultCellStyle.Font = null;
        dgvVoucherAlternatingRowsDefaultCellStyle.Format = "";
        dgvVoucherAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvVoucher.AlternatingRowsDefaultCellStyle = dgvVoucherAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvVoucherRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 246, 248);
        dgvVoucherRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvVoucherRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvVoucherRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvVoucherRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvVoucherRowHeadersDefaultCellStyle.Format = "";
        dgvVoucherRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvVoucherRowHeadersDefaultCellStyle.NullValue = "";
        dgvVoucher.RowHeadersDefaultCellStyle = dgvVoucherRowHeadersDefaultCellStyle;
        dgvVoucher.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn1Style.Font = null;
        dgvVoucherColumn1Style.Format = "";
        dgvVoucherColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn2Style.Font = null;
        dgvVoucherColumn2Style.Format = "";
        dgvVoucherColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn3Style.Font = null;
        dgvVoucherColumn3Style.Format = "";
        dgvVoucherColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn4Style.Font = null;
        dgvVoucherColumn4Style.Format = "";
        dgvVoucherColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn4Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn5Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvVoucherColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn5Style.Font = null;
        dgvVoucherColumn5Style.Format = "#,##0.##";
        dgvVoucherColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn5Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn6Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn6Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvVoucherColumn6Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn6Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn6Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn6Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn6Style.Font = null;
        dgvVoucherColumn6Style.Format = "";
        dgvVoucherColumn6Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn6Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn6Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvVoucherColumn7Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvVoucherColumn7Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dgvVoucherColumn7Style.BackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn7Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn7Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn7Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvVoucherColumn7Style.Font = null;
        dgvVoucherColumn7Style.Format = "";
        dgvVoucherColumn7Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvVoucherColumn7Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvVoucherColumn7Style.NullValue = false;
        

        // groupBox7
        groupBox7.Name = "groupBox7";
        groupBox7.Text = "Thông tin chi tiết";
        groupBox7.Location = new global::System.Drawing.Point(804, 0);
        groupBox7.Size = new global::System.Drawing.Size(354, 541);
        groupBox7.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox7.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox7.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox7.AutoSize = false;
        groupBox7.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox7.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox7.ForeColor = Color.FromArgb(15, 23, 42);
        groupBox7.BackColor = Color.White;
        groupBox7.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox7.TabIndex = 1;
        groupBox7.TabStop = false;
        groupBox7.AccessibleName = null;
        groupBox7.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox7.Visible = true;
        groupBox7.Enabled = true;

        // tableLayoutPanel8
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.Text = "";
        tableLayoutPanel8.Location = new global::System.Drawing.Point(12, 25);
        tableLayoutPanel8.Size = new global::System.Drawing.Size(330, 504);
        tableLayoutPanel8.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel8.AutoSize = false;
        tableLayoutPanel8.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel8.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel8.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel8.BackColor = Color.White;
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
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 280F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.AutoSize));

        // tableLayoutPanel9
        tableLayoutPanel9.Name = "tableLayoutPanel9";
        tableLayoutPanel9.Text = "";
        tableLayoutPanel9.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel9.Size = new global::System.Drawing.Size(330, 280);
        tableLayoutPanel9.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel9.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel9.Dock = global::System.Windows.Forms.DockStyle.Top;

        tableLayoutPanel9.AutoSize = true;
        tableLayoutPanel9.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanel9.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel9.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel9.BackColor = Color.White;
        tableLayoutPanel9.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel9.TabIndex = 0;
        tableLayoutPanel9.TabStop = false;
        tableLayoutPanel9.AccessibleName = null;
        tableLayoutPanel9.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel9.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel9.RowCount = 5;
        tableLayoutPanel9.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel9.AutoScroll = false;
        tableLayoutPanel9.Visible = true;
        tableLayoutPanel9.ColumnCount = 1;
        tableLayoutPanel9.Enabled = true;
        tableLayoutPanel9.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));
        tableLayoutPanel9.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 56F));

        // tableLayoutPanel10
        tableLayoutPanel10.Name = "tableLayoutPanel10";
        tableLayoutPanel10.Text = "";
        tableLayoutPanel10.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel10.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel10.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel10.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel10.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel10.AutoSize = false;
        tableLayoutPanel10.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel10.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel10.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel10.BackColor = Color.White;
        tableLayoutPanel10.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel10.TabIndex = 0;
        tableLayoutPanel10.TabStop = false;
        tableLayoutPanel10.AccessibleName = null;
        tableLayoutPanel10.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel10.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel10.RowCount = 2;
        tableLayoutPanel10.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel10.AutoScroll = false;
        tableLayoutPanel10.Visible = true;
        tableLayoutPanel10.ColumnCount = 1;
        tableLayoutPanel10.Enabled = true;
        tableLayoutPanel10.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label11
        label11.Name = "label11";
        label11.Text = "Mã voucher";
        label11.Location = new global::System.Drawing.Point(3, 0);
        label11.Size = new global::System.Drawing.Size(324, 22);
        label11.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label11.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label11.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label11.AutoSize = false;
        label11.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label11.ForeColor = Color.FromArgb(15, 23, 42);
        label11.BackColor = Color.White;
        label11.Cursor = global::System.Windows.Forms.Cursors.Default;
        label11.TabIndex = 0;
        label11.TabStop = false;
        label11.AccessibleName = null;
        label11.AutoEllipsis = false;
        label11.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label11.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label11.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label11.Visible = true;
        label11.Enabled = true;

        // txtCode
        txtCode.Name = "txtCode";
        txtCode.Text = "";
        txtCode.Location = new global::System.Drawing.Point(0, 23);
        txtCode.Size = new global::System.Drawing.Size(330, 30);
        txtCode.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        txtCode.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        txtCode.Dock = global::System.Windows.Forms.DockStyle.Top;

        txtCode.AutoSize=false;
        txtCode.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtCode.ForeColor=Color.FromArgb(15,23,42);
        txtCode.BackColor=Color.White;
        txtCode.Cursor = global::System.Windows.Forms.Cursors.IBeam;
        txtCode.TabIndex = 1;
        txtCode.TabStop = true;
        txtCode.AccessibleName = null;
        txtCode.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        txtCode.BorderStyle=BorderStyle.FixedSingle;
        txtCode.Multiline = false;
        txtCode.ReadOnly = false;
        txtCode.MaxLength = 20;
        txtCode.PlaceholderText = "";
        txtCode.CharacterCasing = global::System.Windows.Forms.CharacterCasing.Upper;
        txtCode.ScrollBars = global::System.Windows.Forms.ScrollBars.None;
        txtCode.Visible = true;
        txtCode.Enabled = false;

        // tableLayoutPanel12
        tableLayoutPanel12.Name = "tableLayoutPanel12";
        tableLayoutPanel12.Text = "";
        tableLayoutPanel12.Location = new global::System.Drawing.Point(0, 56);
        tableLayoutPanel12.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel12.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel12.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel12.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel12.AutoSize = false;
        tableLayoutPanel12.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel12.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel12.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel12.BackColor = Color.White;
        tableLayoutPanel12.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel12.TabIndex = 1;
        tableLayoutPanel12.TabStop = false;
        tableLayoutPanel12.AccessibleName = null;
        tableLayoutPanel12.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel12.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel12.RowCount = 2;
        tableLayoutPanel12.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel12.AutoScroll = false;
        tableLayoutPanel12.Visible = true;
        tableLayoutPanel12.ColumnCount = 1;
        tableLayoutPanel12.Enabled = true;
        tableLayoutPanel12.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel12.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel12.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label13
        label13.Name = "label13";
        label13.Text = "Loại giảm giá";
        label13.Location = new global::System.Drawing.Point(3, 0);
        label13.Size = new global::System.Drawing.Size(324, 22);
        label13.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label13.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label13.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label13.AutoSize = false;
        label13.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label13.ForeColor = Color.FromArgb(15, 23, 42);
        label13.BackColor = Color.White;
        label13.Cursor = global::System.Windows.Forms.Cursors.Default;
        label13.TabIndex = 0;
        label13.TabStop = false;
        label13.AccessibleName = null;
        label13.AutoEllipsis = false;
        label13.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label13.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label13.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label13.Visible = true;
        label13.Enabled = true;

        // cboLoai
        cboLoai.Name = "cboLoai";
        cboLoai.Text = "";
        cboLoai.Location = new global::System.Drawing.Point(0, 23);
        cboLoai.Size = new global::System.Drawing.Size(330, 31);
        cboLoai.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        cboLoai.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        cboLoai.Dock = global::System.Windows.Forms.DockStyle.Top;

        cboLoai.AutoSize = false;
        cboLoai.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        cboLoai.ForeColor=Color.FromArgb(15,23,42);
        cboLoai.BackColor=Color.White;
        cboLoai.Cursor = global::System.Windows.Forms.Cursors.Default;
        cboLoai.TabIndex = 1;
        cboLoai.TabStop = true;
        cboLoai.AccessibleName = null;
        cboLoai.FlatStyle=FlatStyle.Standard;
        cboLoai.MaxLength = 0;
        cboLoai.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
        cboLoai.DropDownWidth = 330;
        cboLoai.DropDownHeight = 106;
        cboLoai.IntegralHeight = true;
        cboLoai.Visible = true;
        cboLoai.Enabled = false;

        // tableLayoutPanel14
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.Text = "";
        tableLayoutPanel14.Location = new global::System.Drawing.Point(0, 112);
        tableLayoutPanel14.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel14.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel14.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel14.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel14.AutoSize = false;
        tableLayoutPanel14.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel14.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel14.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel14.BackColor = Color.White;
        tableLayoutPanel14.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel14.TabIndex = 2;
        tableLayoutPanel14.TabStop = false;
        tableLayoutPanel14.AccessibleName = null;
        tableLayoutPanel14.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel14.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel14.RowCount = 2;
        tableLayoutPanel14.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel14.AutoScroll = false;
        tableLayoutPanel14.Visible = true;
        tableLayoutPanel14.ColumnCount = 1;
        tableLayoutPanel14.Enabled = true;
        tableLayoutPanel14.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel14.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel14.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label15
        label15.Name = "label15";
        label15.Text = "Giá trị giảm (đ hoặc %)";
        label15.Location = new global::System.Drawing.Point(3, 0);
        label15.Size = new global::System.Drawing.Size(324, 22);
        label15.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label15.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label15.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label15.AutoSize = false;
        label15.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label15.ForeColor = Color.FromArgb(15, 23, 42);
        label15.BackColor = Color.White;
        label15.Cursor = global::System.Windows.Forms.Cursors.Default;
        label15.TabIndex = 0;
        label15.TabStop = false;
        label15.AccessibleName = null;
        label15.AutoEllipsis = false;
        label15.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label15.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label15.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label15.Visible = true;
        label15.Enabled = true;

        // nudGiaTri
        nudGiaTri.Minimum = 0M;
        nudGiaTri.Maximum = 9999999999999999.99M;
        nudGiaTri.DecimalPlaces = 0;
        nudGiaTri.Name = "nudGiaTri";
        nudGiaTri.Text = "0.00";
        nudGiaTri.Location = new global::System.Drawing.Point(0, 23);
        nudGiaTri.Size = new global::System.Drawing.Size(330, 30);
        nudGiaTri.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        nudGiaTri.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        nudGiaTri.Dock = global::System.Windows.Forms.DockStyle.Top;

        nudGiaTri.AutoSize = false;
        nudGiaTri.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        nudGiaTri.ForeColor=Color.FromArgb(15,23,42);
        nudGiaTri.BackColor=Color.White;
        nudGiaTri.Cursor = global::System.Windows.Forms.Cursors.Default;
        nudGiaTri.TabIndex = 1;
        nudGiaTri.TabStop = true;
        nudGiaTri.AccessibleName = null;
        nudGiaTri.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        nudGiaTri.BorderStyle=BorderStyle.FixedSingle;
        nudGiaTri.ReadOnly = false;
        nudGiaTri.ThousandsSeparator = true;
        nudGiaTri.Increment = 1M;
        nudGiaTri.AutoScroll = false;
        nudGiaTri.Visible = true;
        nudGiaTri.Enabled = false;
        nudGiaTri.Value = 0M;

        // tableLayoutPanel16
        tableLayoutPanel16.Name = "tableLayoutPanel16";
        tableLayoutPanel16.Text = "";
        tableLayoutPanel16.Location = new global::System.Drawing.Point(0, 168);
        tableLayoutPanel16.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel16.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel16.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel16.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel16.AutoSize = false;
        tableLayoutPanel16.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel16.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel16.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel16.BackColor = Color.White;
        tableLayoutPanel16.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel16.TabIndex = 3;
        tableLayoutPanel16.TabStop = false;
        tableLayoutPanel16.AccessibleName = null;
        tableLayoutPanel16.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel16.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel16.RowCount = 2;
        tableLayoutPanel16.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel16.AutoScroll = false;
        tableLayoutPanel16.Visible = true;
        tableLayoutPanel16.ColumnCount = 1;
        tableLayoutPanel16.Enabled = true;
        tableLayoutPanel16.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel16.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel16.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label17
        label17.Name = "label17";
        label17.Text = "Số lượt còn lại";
        label17.Location = new global::System.Drawing.Point(3, 0);
        label17.Size = new global::System.Drawing.Size(324, 22);
        label17.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label17.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label17.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label17.AutoSize = false;
        label17.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label17.ForeColor = Color.FromArgb(15, 23, 42);
        label17.BackColor = Color.White;
        label17.Cursor = global::System.Windows.Forms.Cursors.Default;
        label17.TabIndex = 0;
        label17.TabStop = false;
        label17.AccessibleName = null;
        label17.AutoEllipsis = false;
        label17.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label17.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label17.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label17.Visible = true;
        label17.Enabled = true;

        // nudSoLuot
        nudSoLuot.Minimum = 0M;
        nudSoLuot.Maximum = 2147483647M;
        nudSoLuot.DecimalPlaces = 0;
        nudSoLuot.Name = "nudSoLuot";
        nudSoLuot.Text = "0";
        nudSoLuot.Location = new global::System.Drawing.Point(0, 23);
        nudSoLuot.Size = new global::System.Drawing.Size(330, 30);
        nudSoLuot.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        nudSoLuot.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        nudSoLuot.Dock = global::System.Windows.Forms.DockStyle.Top;

        nudSoLuot.AutoSize = false;
        nudSoLuot.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        nudSoLuot.ForeColor=Color.FromArgb(15,23,42);
        nudSoLuot.BackColor=Color.White;
        nudSoLuot.Cursor = global::System.Windows.Forms.Cursors.Default;
        nudSoLuot.TabIndex = 1;
        nudSoLuot.TabStop = true;
        nudSoLuot.AccessibleName = null;
        nudSoLuot.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Left;
        nudSoLuot.BorderStyle=BorderStyle.FixedSingle;
        nudSoLuot.ReadOnly = false;
        nudSoLuot.ThousandsSeparator = true;
        nudSoLuot.Increment = 1M;
        nudSoLuot.AutoScroll = false;
        nudSoLuot.Visible = true;
        nudSoLuot.Enabled = false;
        nudSoLuot.Value = 0M;

        // tableLayoutPanel18
        tableLayoutPanel18.Name = "tableLayoutPanel18";
        tableLayoutPanel18.Text = "";
        tableLayoutPanel18.Location = new global::System.Drawing.Point(0, 224);
        tableLayoutPanel18.Size = new global::System.Drawing.Size(330, 56);
        tableLayoutPanel18.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel18.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel18.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel18.AutoSize = false;
        tableLayoutPanel18.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel18.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel18.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel18.BackColor = Color.White;
        tableLayoutPanel18.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel18.TabIndex = 4;
        tableLayoutPanel18.TabStop = false;
        tableLayoutPanel18.AccessibleName = null;
        tableLayoutPanel18.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel18.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel18.RowCount = 2;
        tableLayoutPanel18.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel18.AutoScroll = false;
        tableLayoutPanel18.Visible = true;
        tableLayoutPanel18.ColumnCount = 1;
        tableLayoutPanel18.Enabled = true;
        tableLayoutPanel18.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel18.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel18.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 34F));

        // label19
        label19.Name = "label19";
        label19.Text = "Trạng thái";
        label19.Location = new global::System.Drawing.Point(3, 0);
        label19.Size = new global::System.Drawing.Size(324, 22);
        label19.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label19.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label19.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label19.AutoSize = false;
        label19.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label19.ForeColor = Color.FromArgb(15, 23, 42);
        label19.BackColor = Color.White;
        label19.Cursor = global::System.Windows.Forms.Cursors.Default;
        label19.TabIndex = 0;
        label19.TabStop = false;
        label19.AccessibleName = null;
        label19.AutoEllipsis = false;
        label19.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        label19.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label19.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label19.Visible = true;
        label19.Enabled = true;

        // chkHoatDong
        chkHoatDong.Name = "chkHoatDong";
        chkHoatDong.Text = "Đang hoạt động";
        chkHoatDong.Location = new global::System.Drawing.Point(0, 23);
        chkHoatDong.Size = new global::System.Drawing.Size(330, 24);
        chkHoatDong.Margin = new global::System.Windows.Forms.Padding(0, 1, 0, 5);
        chkHoatDong.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        chkHoatDong.Dock = global::System.Windows.Forms.DockStyle.Top;

        chkHoatDong.AutoSize = false;
        chkHoatDong.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        chkHoatDong.ForeColor = Color.FromArgb(15, 23, 42);
        chkHoatDong.BackColor = Color.White;
        chkHoatDong.Cursor = global::System.Windows.Forms.Cursors.Default;
        chkHoatDong.TabIndex = 1;
        chkHoatDong.TabStop = true;
        chkHoatDong.AccessibleName = null;
        chkHoatDong.AutoEllipsis = false;
        chkHoatDong.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        chkHoatDong.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        chkHoatDong.UseVisualStyleBackColor = true;
        chkHoatDong.Checked = true;
        chkHoatDong.CheckAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        chkHoatDong.CheckState = global::System.Windows.Forms.CheckState.Checked;
        chkHoatDong.Visible = true;
        chkHoatDong.Enabled = false;

        // tableLayoutPanel20
        tableLayoutPanel20.Name = "tableLayoutPanel20";
        tableLayoutPanel20.Text = "";
        tableLayoutPanel20.Location = new global::System.Drawing.Point(0, 328);
        tableLayoutPanel20.Size = new global::System.Drawing.Size(330, 132);
        tableLayoutPanel20.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel20.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel20.Dock = global::System.Windows.Forms.DockStyle.Bottom;

        tableLayoutPanel20.AutoSize = true;
        tableLayoutPanel20.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanel20.FlowDirection = global::System.Windows.Forms.FlowDirection.LeftToRight;
        tableLayoutPanel20.WrapContents = true;
        tableLayoutPanel20.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel20.ForeColor = Color.FromArgb(15, 23, 42);
        tableLayoutPanel20.BackColor = Color.White;
        tableLayoutPanel20.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel20.TabIndex = 1;
        tableLayoutPanel20.TabStop = false;
        tableLayoutPanel20.AccessibleName = null;
        tableLayoutPanel20.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel20.AutoScroll = false;
        tableLayoutPanel20.Visible = true;
        tableLayoutPanel20.Enabled = true;

        // btnThem
        btnThem.Name = "btnThem";
        btnThem.Text = "Thêm";
        btnThem.Location = new global::System.Drawing.Point(0, 4);
        btnThem.Size = new global::System.Drawing.Size(156, 38);
        btnThem.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnThem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnThem.Dock = global::System.Windows.Forms.DockStyle.None;

        btnThem.AutoSize = false;
        btnThem.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnThem.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnThem.ForeColor = Color.FromArgb(15, 23, 42);
        btnThem.BackColor = global::System.Drawing.Color.FromArgb(255, 192, 255, 192);
        btnThem.TabIndex = 0;
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
        btnSua.Size = new global::System.Drawing.Size(156, 38);
        btnSua.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnSua.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnSua.Dock = global::System.Windows.Forms.DockStyle.None;

        btnSua.AutoSize = false;
        btnSua.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnSua.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnSua.ForeColor = Color.FromArgb(15, 23, 42);
        btnSua.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnSua.TabIndex = 1;
        btnSua.TabStop = true;
        btnSua.AccessibleName = null;
        btnSua.AutoEllipsis = false;
        btnSua.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnSua.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnSua.Visible = true;
        btnSua.Enabled = false;
        btnSua.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnLuu
        btnLuu.Name = "btnLuu";
        btnLuu.Text = "Lưu";
        btnLuu.Location = new global::System.Drawing.Point(0, 48);
        btnLuu.Size = new global::System.Drawing.Size(156, 38);
        btnLuu.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnLuu.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnLuu.Dock = global::System.Windows.Forms.DockStyle.None;

        btnLuu.AutoSize = false;
        btnLuu.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnLuu.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnLuu.ForeColor = Color.FromArgb(15, 23, 42);
        btnLuu.BackColor = global::System.Drawing.Color.FromArgb(255, 192, 255, 192);
        btnLuu.TabIndex = 2;
        btnLuu.TabStop = true;
        btnLuu.AccessibleName = null;
        btnLuu.AutoEllipsis = false;
        btnLuu.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnLuu.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnLuu.Visible = true;
        btnLuu.Enabled = false;
        btnLuu.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnHuy
        btnHuy.Name = "btnHuy";
        btnHuy.Text = "Hủy";
        btnHuy.Location = new global::System.Drawing.Point(169, 48);
        btnHuy.Size = new global::System.Drawing.Size(156, 38);
        btnHuy.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnHuy.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnHuy.Dock = global::System.Windows.Forms.DockStyle.None;

        btnHuy.AutoSize = false;
        btnHuy.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnHuy.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnHuy.ForeColor = Color.FromArgb(15, 23, 42);
        btnHuy.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 192, 192);
        btnHuy.TabIndex = 3;
        btnHuy.TabStop = true;
        btnHuy.AccessibleName = null;
        btnHuy.AutoEllipsis = false;
        btnHuy.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnHuy.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnHuy.Visible = true;
        btnHuy.Enabled = false;
        btnHuy.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnXoa
        btnXoa.Name = "btnXoa";
        btnXoa.Text = "Xóa";
        btnXoa.Location = new global::System.Drawing.Point(0, 92);
        btnXoa.Size = new global::System.Drawing.Size(156, 38);
        btnXoa.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnXoa.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnXoa.Dock = global::System.Windows.Forms.DockStyle.None;

        btnXoa.AutoSize = false;
        btnXoa.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnXoa.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnXoa.ForeColor = Color.FromArgb(15, 23, 42);
        btnXoa.BackColor = global::System.Drawing.Color.FromArgb(255, 255, 192, 192);
        btnXoa.TabIndex = 4;
        btnXoa.TabStop = true;
        btnXoa.AccessibleName = null;
        btnXoa.AutoEllipsis = false;
        btnXoa.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnXoa.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnXoa.Visible = true;
        btnXoa.Enabled = false;
        btnXoa.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // btnLamMoi
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.Location = new global::System.Drawing.Point(169, 92);
        btnLamMoi.Size = new global::System.Drawing.Size(156, 38);
        btnLamMoi.Margin = new global::System.Windows.Forms.Padding(0, 4, 8, 0);
        btnLamMoi.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnLamMoi.Dock = global::System.Windows.Forms.DockStyle.None;

        btnLamMoi.AutoSize = false;
        btnLamMoi.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnLamMoi.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        btnLamMoi.ForeColor = Color.FromArgb(15, 23, 42);
        btnLamMoi.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnLamMoi.TabIndex = 5;
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
        tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
        tableLayoutPanel2.Controls.Add(groupBox3, 0, 0);
        groupBox3.Controls.Add(tableLayoutPanel4);
        tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
        tableLayoutPanel5.Controls.Add(label6, 0, 0);
        tableLayoutPanel5.Controls.Add(txtTimKiem, 1, 0);
        tableLayoutPanel5.Controls.Add(btnTimKiem, 2, 0);
        tableLayoutPanel4.Controls.Add(dgvVoucher, 0, 1);
        tableLayoutPanel2.Controls.Add(groupBox7, 1, 0);
        groupBox7.Controls.Add(tableLayoutPanel8);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel9, 0, 0);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel10, -1, -1);
        tableLayoutPanel10.Controls.Add(label11, 0, 0);
        tableLayoutPanel10.Controls.Add(txtCode, 0, 1);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel12, -1, -1);
        tableLayoutPanel12.Controls.Add(label13, 0, 0);
        tableLayoutPanel12.Controls.Add(cboLoai, 0, 1);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel14, -1, -1);
        tableLayoutPanel14.Controls.Add(label15, 0, 0);
        tableLayoutPanel14.Controls.Add(nudGiaTri, 0, 1);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel16, -1, -1);
        tableLayoutPanel16.Controls.Add(label17, 0, 0);
        tableLayoutPanel16.Controls.Add(nudSoLuot, 0, 1);
        tableLayoutPanel9.Controls.Add(tableLayoutPanel18, -1, -1);
        tableLayoutPanel18.Controls.Add(label19, 0, 0);
        tableLayoutPanel18.Controls.Add(chkHoatDong, 0, 1);
        tableLayoutPanel8.Controls.Add(tableLayoutPanel20, 0, 2);
        tableLayoutPanel20.Controls.Add(btnThem);
        tableLayoutPanel20.Controls.Add(btnSua);
        tableLayoutPanel20.Controls.Add(btnLuu);
        tableLayoutPanel20.Controls.Add(btnHuy);
        tableLayoutPanel20.Controls.Add(btnXoa);
        tableLayoutPanel20.Controls.Add(btnLamMoi);
        tableLayoutPanel1.Controls.Add(lblTrangThai, 0, 1);
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
        Text = "Quản lý voucher | Pharmacy";
        KeyPreview = false;
        ShowIcon = true;
        ShowInTaskbar = true;
        Name = "frmVoucher";
        tableLayoutPanel20.ResumeLayout(false);
        tableLayoutPanel20.PerformLayout();
        tableLayoutPanel18.ResumeLayout(false);
        tableLayoutPanel18.PerformLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudSoLuot).EndInit();
        tableLayoutPanel16.ResumeLayout(false);
        tableLayoutPanel16.PerformLayout();
        ((global::System.ComponentModel.ISupportInitialize)nudGiaTri).EndInit();
        tableLayoutPanel14.ResumeLayout(false);
        tableLayoutPanel14.PerformLayout();
        tableLayoutPanel12.ResumeLayout(false);
        tableLayoutPanel12.PerformLayout();
        tableLayoutPanel10.ResumeLayout(false);
        tableLayoutPanel10.PerformLayout();
        tableLayoutPanel9.ResumeLayout(false);
        tableLayoutPanel9.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel8.PerformLayout();
        groupBox7.ResumeLayout(false);
        groupBox7.PerformLayout();
                //
        // dgvVoucherColumn1
        //
        this.dgvVoucherColumn1.Name = "Id";
        this.dgvVoucherColumn1.HeaderText = "ID voucher";
        this.dgvVoucherColumn1.Visible = false;
        this.dgvVoucherColumn1.ReadOnly = true;
        this.dgvVoucherColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn1.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn1.DataPropertyName = "Id";
        this.dgvVoucherColumn1.DefaultCellStyle = dgvVoucherColumn1Style;
        this.dgvVoucherColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn1.MinimumWidth = 5;
        this.dgvVoucherColumn1.Width = 133;
        this.dgvVoucherColumn1.FillWeight = 48F;
        //
        // dgvVoucherColumn2
        //
        this.dgvVoucherColumn2.Name = "Code";
        this.dgvVoucherColumn2.HeaderText = "Mã voucher";
        this.dgvVoucherColumn2.Visible = true;
        this.dgvVoucherColumn2.ReadOnly = true;
        this.dgvVoucherColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn2.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn2.DataPropertyName = "Code";
        this.dgvVoucherColumn2.DefaultCellStyle = dgvVoucherColumn2Style;
        this.dgvVoucherColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn2.MinimumWidth = 5;
        this.dgvVoucherColumn2.Width = 160;
        this.dgvVoucherColumn2.FillWeight = 104F;
        //
        // dgvVoucherColumn3
        //
        this.dgvVoucherColumn3.Name = "DiscountType";
        this.dgvVoucherColumn3.HeaderText = "Mã loại giảm giá";
        this.dgvVoucherColumn3.Visible = false;
        this.dgvVoucherColumn3.ReadOnly = true;
        this.dgvVoucherColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn3.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn3.DataPropertyName = "DiscountType";
        this.dgvVoucherColumn3.DefaultCellStyle = dgvVoucherColumn3Style;
        this.dgvVoucherColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn3.MinimumWidth = 5;
        this.dgvVoucherColumn3.Width = 177;
        this.dgvVoucherColumn3.FillWeight = 120F;
        //
        // dgvVoucherColumn4
        //
        this.dgvVoucherColumn4.Name = "LoaiGiam";
        this.dgvVoucherColumn4.HeaderText = "Loại giảm giá";
        this.dgvVoucherColumn4.Visible = true;
        this.dgvVoucherColumn4.ReadOnly = true;
        this.dgvVoucherColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn4.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn4.DataPropertyName = "LoaiGiam";
        this.dgvVoucherColumn4.DefaultCellStyle = dgvVoucherColumn4Style;
        this.dgvVoucherColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn4.MinimumWidth = 5;
        this.dgvVoucherColumn4.Width = 175;
        this.dgvVoucherColumn4.FillWeight = 96F;
        //
        // dgvVoucherColumn5
        //
        this.dgvVoucherColumn5.Name = "DiscountValue";
        this.dgvVoucherColumn5.HeaderText = "Giá trị giảm";
        this.dgvVoucherColumn5.Visible = true;
        this.dgvVoucherColumn5.ReadOnly = true;
        this.dgvVoucherColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn5.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn5.DataPropertyName = "DiscountValue";
        this.dgvVoucherColumn5.DefaultCellStyle = dgvVoucherColumn5Style;
        this.dgvVoucherColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn5.MinimumWidth = 5;
        this.dgvVoucherColumn5.Width = 139;
        this.dgvVoucherColumn5.FillWeight = 120F;
        //
        // dgvVoucherColumn6
        //
        this.dgvVoucherColumn6.Name = "Quantity";
        this.dgvVoucherColumn6.HeaderText = "Lượt còn lại";
        this.dgvVoucherColumn6.Visible = true;
        this.dgvVoucherColumn6.ReadOnly = true;
        this.dgvVoucherColumn6.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvVoucherColumn6.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn6.DataPropertyName = "Quantity";
        this.dgvVoucherColumn6.DefaultCellStyle = dgvVoucherColumn6Style;
        this.dgvVoucherColumn6.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn6.MinimumWidth = 5;
        this.dgvVoucherColumn6.Width = 139;
        this.dgvVoucherColumn6.FillWeight = 120F;
        //
        // dgvVoucherColumn7
        //
        this.dgvVoucherColumn7.Name = "IsActive";
        this.dgvVoucherColumn7.HeaderText = "Hoạt động";
        this.dgvVoucherColumn7.Visible = true;
        this.dgvVoucherColumn7.ReadOnly = true;
        this.dgvVoucherColumn7.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.dgvVoucherColumn7.Resizable = DataGridViewTriState.False;
        this.dgvVoucherColumn7.DataPropertyName = "IsActive";
        this.dgvVoucherColumn7.DefaultCellStyle = dgvVoucherColumn7Style;
        this.dgvVoucherColumn7.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvVoucherColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvVoucherColumn7.MinimumWidth = 5;
        this.dgvVoucherColumn7.Width = 132;
        this.dgvVoucherColumn7.FillWeight = 96F;

        this.dgvVoucher.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvVoucher.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvVoucherColumn1, this.dgvVoucherColumn2, this.dgvVoucherColumn3, this.dgvVoucherColumn4, this.dgvVoucherColumn5, this.dgvVoucherColumn6, this.dgvVoucherColumn7 });
        ((global::System.ComponentModel.ISupportInitialize)dgvVoucher).EndInit();
        tableLayoutPanel5.ResumeLayout(false);
        tableLayoutPanel5.PerformLayout();
        tableLayoutPanel4.ResumeLayout(false);
        tableLayoutPanel4.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanel2.PerformLayout();
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
        btnLuu.BackColor=Color.FromArgb(22, 101, 52);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
        btnLuu.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 66, 34);
        btnLuu.Cursor = Cursors.Hand;
        btnLuu.UseVisualStyleBackColor = false;btnLuu.ForeColor=Color.White;
        btnHuy.BackColor=Color.FromArgb(100, 116, 139);
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
        btnHuy.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.UseVisualStyleBackColor = false;btnHuy.ForeColor=Color.White;
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

        dgvVoucher.BackgroundColor=Color.White;dgvVoucher.EnableHeadersVisualStyles=false;
        dgvVoucher.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvVoucher.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvVoucher.DefaultCellStyle.BackColor=Color.White;dgvVoucher.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvVoucher.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvVoucher.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvVoucher.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(242, 246, 248);
        dgvVoucher.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvVoucher.MultiSelect=false;
        dgvVoucher.AllowUserToResizeColumns=false;
        dgvVoucher.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvVoucher.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvVoucher.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvVoucher.ColumnHeadersDefaultCellStyle.BackColor;
        dgvVoucher.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvVoucher.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvVoucher.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvVoucher.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                dgvVoucher.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvVoucher.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvVoucher.ColumnHeadersHeight=43;
        
        
        
        
        
        
        
        ResumeLayout(false);
        PerformLayout();
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private global::System.Windows.Forms.GroupBox groupBox3;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private global::System.Windows.Forms.Label label6;
    private global::System.Windows.Forms.TextBox txtTimKiem;
    private global::System.Windows.Forms.Button btnTimKiem;
    private global::System.Windows.Forms.DataGridView dgvVoucher;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvVoucherColumn6;
    private global::System.Windows.Forms.DataGridViewCheckBoxColumn dgvVoucherColumn7;
    private global::System.Windows.Forms.GroupBox groupBox7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.Label label11;
    private global::System.Windows.Forms.TextBox txtCode;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
    private global::System.Windows.Forms.Label label13;
    private global::System.Windows.Forms.ComboBox cboLoai;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.Label label15;
    private global::System.Windows.Forms.NumericUpDown nudGiaTri;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
    private global::System.Windows.Forms.Label label17;
    private global::System.Windows.Forms.NumericUpDown nudSoLuot;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
    private global::System.Windows.Forms.Label label19;
    private global::System.Windows.Forms.CheckBox chkHoatDong;
    private global::System.Windows.Forms.FlowLayoutPanel tableLayoutPanel20;
    private global::System.Windows.Forms.Button btnThem;
    private global::System.Windows.Forms.Button btnSua;
    private global::System.Windows.Forms.Button btnLuu;
    private global::System.Windows.Forms.Button btnHuy;
    private global::System.Windows.Forms.Button btnXoa;
    private global::System.Windows.Forms.Button btnLamMoi;
    private global::System.Windows.Forms.Label lblTrangThai;
}

}