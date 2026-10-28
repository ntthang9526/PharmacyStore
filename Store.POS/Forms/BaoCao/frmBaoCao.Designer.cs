using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{

partial class frmBaoCao
{
    private global::System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.dgvTheoNgayColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvTheoNgayColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvTheoNgayColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvTheoNgayColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvTheoNgayColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.dgvHoaDonColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        components = new global::System.ComponentModel.Container();
        posLayout = new global::System.Windows.Forms.TableLayoutPanel();
        posHeading = new global::System.Windows.Forms.Label();
        tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox2 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
        label4 = new global::System.Windows.Forms.Label();
        dtpTuNgay = new global::System.Windows.Forms.DateTimePicker();
        label5 = new global::System.Windows.Forms.Label();
        dtpDenNgay = new global::System.Windows.Forms.DateTimePicker();
        btnXem = new global::System.Windows.Forms.Button();
        groupBox6 = new global::System.Windows.Forms.GroupBox();
        tableLayoutPanel7 = new global::System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel8 = new global::System.Windows.Forms.TableLayoutPanel();
        label9 = new global::System.Windows.Forms.Label();
        lblSoHD = new global::System.Windows.Forms.Label();
        tableLayoutPanel10 = new global::System.Windows.Forms.TableLayoutPanel();
        label11 = new global::System.Windows.Forms.Label();
        lblTongTien = new global::System.Windows.Forms.Label();
        tableLayoutPanel12 = new global::System.Windows.Forms.TableLayoutPanel();
        label13 = new global::System.Windows.Forms.Label();
        lblGiamGia = new global::System.Windows.Forms.Label();
        tableLayoutPanel14 = new global::System.Windows.Forms.TableLayoutPanel();
        label15 = new global::System.Windows.Forms.Label();
        lblDoanhThu = new global::System.Windows.Forms.Label();
        tableLayoutPanel16 = new global::System.Windows.Forms.TableLayoutPanel();
        groupBox17 = new global::System.Windows.Forms.GroupBox();
        dgvTheoNgay = new global::System.Windows.Forms.DataGridView();
        
        
        groupBox18 = new global::System.Windows.Forms.GroupBox();
        dgvHoaDon = new global::System.Windows.Forms.DataGridView();
        
        
        
        posLayout.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        tableLayoutPanel3.SuspendLayout();
        groupBox6.SuspendLayout();
        tableLayoutPanel7.SuspendLayout();
        tableLayoutPanel8.SuspendLayout();
        tableLayoutPanel10.SuspendLayout();
        tableLayoutPanel12.SuspendLayout();
        tableLayoutPanel14.SuspendLayout();
        tableLayoutPanel16.SuspendLayout();
        groupBox17.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvTheoNgay).BeginInit();
        groupBox18.SuspendLayout();
        ((global::System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
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
        posLayout.RowCount = 3;
        posLayout.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        posLayout.AutoScroll = false;
        posLayout.Visible = true;
        posLayout.ColumnCount = 1;
        posLayout.Enabled = true;
        posLayout.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        posLayout.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 48F));
        posLayout.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 44F));
        posLayout.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // posHeading
        posHeading.Name = "posHeading";
        posHeading.Text = "Báo cáo doanh thu";
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
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 86F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 96F));
        tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // groupBox2
        groupBox2.Name = "groupBox2";
        groupBox2.Text = "Lọc theo thời gian";
        groupBox2.Location = new global::System.Drawing.Point(0, 0);
        groupBox2.Size = new global::System.Drawing.Size(1158, 86);
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
        tableLayoutPanel3.Size = new global::System.Drawing.Size(1134, 49);
        tableLayoutPanel3.Margin = new global::System.Windows.Forms.Padding(3, 3, 3, 3);
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
        tableLayoutPanel3.ColumnCount = 6;
        tableLayoutPanel3.Enabled = true;
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 90F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 180F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 110F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 180F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 160F));
        tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label4
        label4.Name = "label4";
        label4.Text = "Từ ngày";
        label4.Location = new global::System.Drawing.Point(0, 2);
        label4.Size = new global::System.Drawing.Size(78, 45);
        label4.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
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

        // dtpTuNgay
        dtpTuNgay.Name = "dtpTuNgay";
        dtpTuNgay.Text = "";
        dtpTuNgay.Location = new global::System.Drawing.Point(90, 2);
        dtpTuNgay.Size = new global::System.Drawing.Size(168, 30);
        dtpTuNgay.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        dtpTuNgay.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dtpTuNgay.Dock = global::System.Windows.Forms.DockStyle.Top;

        dtpTuNgay.AutoSize = false;
        dtpTuNgay.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        dtpTuNgay.ForeColor=Color.FromArgb(15,23,42);
        dtpTuNgay.BackColor = global::System.Drawing.SystemColors.Window;
        dtpTuNgay.Cursor = global::System.Windows.Forms.Cursors.Default;
        dtpTuNgay.TabIndex = 1;
        dtpTuNgay.TabStop = true;
        dtpTuNgay.AccessibleName = null;
        dtpTuNgay.Checked = true;
        dtpTuNgay.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        dtpTuNgay.ShowCheckBox = false;
        dtpTuNgay.Visible = true;
        dtpTuNgay.Enabled = true;
        dtpTuNgay.Value = new global::System.DateTime(2026, 10, 22);

        // label5
        label5.Name = "label5";
        label5.Text = "Đến ngày";
        label5.Location = new global::System.Drawing.Point(270, 2);
        label5.Size = new global::System.Drawing.Size(98, 45);
        label5.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        label5.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label5.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label5.AutoSize = false;
        label5.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label5.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label5.BackColor = Color.FromArgb(248,250,252);
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

        // dtpDenNgay
        dtpDenNgay.Name = "dtpDenNgay";
        dtpDenNgay.Text = "";
        dtpDenNgay.Location = new global::System.Drawing.Point(380, 2);
        dtpDenNgay.Size = new global::System.Drawing.Size(168, 30);
        dtpDenNgay.Margin = new global::System.Windows.Forms.Padding(0, 2, 12, 2);
        dtpDenNgay.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dtpDenNgay.Dock = global::System.Windows.Forms.DockStyle.Top;

        dtpDenNgay.AutoSize = false;
        dtpDenNgay.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        dtpDenNgay.ForeColor=Color.FromArgb(15,23,42);
        dtpDenNgay.BackColor = global::System.Drawing.SystemColors.Window;
        dtpDenNgay.Cursor = global::System.Windows.Forms.Cursors.Default;
        dtpDenNgay.TabIndex = 3;
        dtpDenNgay.TabStop = true;
        dtpDenNgay.AccessibleName = null;
        dtpDenNgay.Checked = true;
        dtpDenNgay.Format = global::System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        dtpDenNgay.ShowCheckBox = false;
        dtpDenNgay.Visible = true;
        dtpDenNgay.Enabled = true;
        dtpDenNgay.Value = new global::System.DateTime(2026, 10, 22);

        // btnXem
        btnXem.Name = "btnXem";
        btnXem.Text = "Xem báo cáo";
        btnXem.Location = new global::System.Drawing.Point(560, 2);
        btnXem.Size = new global::System.Drawing.Size(148, 32);
        btnXem.Margin = new global::System.Windows.Forms.Padding(0, 1, 12, 2);
        btnXem.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        btnXem.Dock = global::System.Windows.Forms.DockStyle.Top;

        btnXem.AutoSize = false;
        btnXem.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        btnXem.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        btnXem.ForeColor = global::System.Drawing.SystemColors.ControlText;
        btnXem.BackColor = global::System.Drawing.SystemColors.ControlLight;
        btnXem.TabIndex = 4;
        btnXem.TabStop = true;
        btnXem.AccessibleName = null;
        btnXem.AutoEllipsis = false;
        btnXem.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;

        btnXem.DialogResult = global::System.Windows.Forms.DialogResult.None;
        btnXem.Visible = true;
        btnXem.Enabled = true;
        btnXem.FlatAppearance.BorderColor = global::System.Drawing.SystemColors.ActiveBorder;

        // groupBox6
        groupBox6.Name = "groupBox6";
        groupBox6.Text = "Tổng hợp";
        groupBox6.Location = new global::System.Drawing.Point(0, 86);
        groupBox6.Size = new global::System.Drawing.Size(1158, 96);
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
        tableLayoutPanel7.Size = new global::System.Drawing.Size(1134, 59);
        tableLayoutPanel7.Margin = new global::System.Windows.Forms.Padding(3, 3, 3, 3);
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
        tableLayoutPanel7.RowCount = 1;
        tableLayoutPanel7.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel7.AutoScroll = false;
        tableLayoutPanel7.Visible = true;
        tableLayoutPanel7.ColumnCount = 4;
        tableLayoutPanel7.Enabled = true;
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanel7.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanel7.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // tableLayoutPanel8
        tableLayoutPanel8.Name = "tableLayoutPanel8";
        tableLayoutPanel8.Text = "";
        tableLayoutPanel8.Location = new global::System.Drawing.Point(0, 0);
        tableLayoutPanel8.Size = new global::System.Drawing.Size(283, 59);
        tableLayoutPanel8.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel8.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel8.AutoSize = false;
        tableLayoutPanel8.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel8.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel8.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel8.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel8.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel8.TabIndex = 0;
        tableLayoutPanel8.TabStop = false;
        tableLayoutPanel8.AccessibleName = null;
        tableLayoutPanel8.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel8.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel8.RowCount = 2;
        tableLayoutPanel8.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel8.AutoScroll = false;
        tableLayoutPanel8.Visible = true;
        tableLayoutPanel8.ColumnCount = 1;
        tableLayoutPanel8.Enabled = true;
        tableLayoutPanel8.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22F));
        tableLayoutPanel8.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label9
        label9.Name = "label9";
        label9.Text = "Số hóa đơn";
        label9.Location = new global::System.Drawing.Point(3, 0);
        label9.Size = new global::System.Drawing.Size(277, 22);
        label9.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label9.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label9.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label9.AutoSize = false;
        label9.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label9.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label9.BackColor = Color.FromArgb(248,250,252);
        label9.Cursor = global::System.Windows.Forms.Cursors.Default;
        label9.TabIndex = 0;
        label9.TabStop = false;
        label9.AccessibleName = null;
        label9.AutoEllipsis = false;
        label9.TextAlign = global::System.Drawing.ContentAlignment.TopLeft;
        label9.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label9.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label9.Visible = true;
        label9.Enabled = true;

        // lblSoHD
        lblSoHD.Name = "lblSoHD";
        lblSoHD.Text = "0";
        lblSoHD.Location = new global::System.Drawing.Point(3, 22);
        lblSoHD.Size = new global::System.Drawing.Size(277, 37);
        lblSoHD.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        lblSoHD.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        lblSoHD.Dock = global::System.Windows.Forms.DockStyle.Fill;

        lblSoHD.AutoSize = false;
        lblSoHD.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        lblSoHD.ForeColor = global::System.Drawing.SystemColors.ControlText;
        lblSoHD.BackColor = Color.FromArgb(248,250,252);
        lblSoHD.Cursor = global::System.Windows.Forms.Cursors.Default;
        lblSoHD.TabIndex = 1;
        lblSoHD.TabStop = false;
        lblSoHD.AccessibleName = null;
        lblSoHD.AutoEllipsis = false;
        lblSoHD.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        lblSoHD.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        lblSoHD.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        lblSoHD.Visible = true;
        lblSoHD.Enabled = true;

        // tableLayoutPanel10
        tableLayoutPanel10.Name = "tableLayoutPanel10";
        tableLayoutPanel10.Text = "";
        tableLayoutPanel10.Location = new global::System.Drawing.Point(283, 0);
        tableLayoutPanel10.Size = new global::System.Drawing.Size(283, 59);
        tableLayoutPanel10.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel10.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel10.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel10.AutoSize = false;
        tableLayoutPanel10.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel10.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel10.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel10.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel10.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel10.TabIndex = 1;
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
        tableLayoutPanel10.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label11
        label11.Name = "label11";
        label11.Text = "Tổng tiền hàng";
        label11.Location = new global::System.Drawing.Point(3, 0);
        label11.Size = new global::System.Drawing.Size(277, 22);
        label11.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label11.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label11.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label11.AutoSize = false;
        label11.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label11.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label11.BackColor = Color.FromArgb(248,250,252);
        label11.Cursor = global::System.Windows.Forms.Cursors.Default;
        label11.TabIndex = 0;
        label11.TabStop = false;
        label11.AccessibleName = null;
        label11.AutoEllipsis = false;
        label11.TextAlign = global::System.Drawing.ContentAlignment.TopLeft;
        label11.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label11.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label11.Visible = true;
        label11.Enabled = true;

        // lblTongTien
        lblTongTien.Name = "lblTongTien";
        lblTongTien.Text = "0 đ";
        lblTongTien.Location = new global::System.Drawing.Point(3, 22);
        lblTongTien.Size = new global::System.Drawing.Size(277, 37);
        lblTongTien.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        lblTongTien.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        lblTongTien.Dock = global::System.Windows.Forms.DockStyle.Fill;

        lblTongTien.AutoSize = false;
        lblTongTien.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        lblTongTien.ForeColor = global::System.Drawing.SystemColors.ControlText;
        lblTongTien.BackColor = Color.FromArgb(248,250,252);
        lblTongTien.Cursor = global::System.Windows.Forms.Cursors.Default;
        lblTongTien.TabIndex = 3;
        lblTongTien.TabStop = false;
        lblTongTien.AccessibleName = null;
        lblTongTien.AutoEllipsis = false;
        lblTongTien.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        lblTongTien.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        lblTongTien.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        lblTongTien.Visible = true;
        lblTongTien.Enabled = true;

        // tableLayoutPanel12
        tableLayoutPanel12.Name = "tableLayoutPanel12";
        tableLayoutPanel12.Text = "";
        tableLayoutPanel12.Location = new global::System.Drawing.Point(566, 0);
        tableLayoutPanel12.Size = new global::System.Drawing.Size(283, 59);
        tableLayoutPanel12.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel12.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel12.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel12.AutoSize = false;
        tableLayoutPanel12.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel12.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel12.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel12.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel12.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel12.TabIndex = 2;
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
        tableLayoutPanel12.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label13
        label13.Name = "label13";
        label13.Text = "Giảm giá";
        label13.Location = new global::System.Drawing.Point(3, 0);
        label13.Size = new global::System.Drawing.Size(277, 22);
        label13.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label13.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label13.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label13.AutoSize = false;
        label13.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label13.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label13.BackColor = Color.FromArgb(248,250,252);
        label13.Cursor = global::System.Windows.Forms.Cursors.Default;
        label13.TabIndex = 0;
        label13.TabStop = false;
        label13.AccessibleName = null;
        label13.AutoEllipsis = false;
        label13.TextAlign = global::System.Drawing.ContentAlignment.TopLeft;
        label13.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label13.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label13.Visible = true;
        label13.Enabled = true;

        // lblGiamGia
        lblGiamGia.Name = "lblGiamGia";
        lblGiamGia.Text = "0 đ";
        lblGiamGia.Location = new global::System.Drawing.Point(3, 22);
        lblGiamGia.Size = new global::System.Drawing.Size(277, 37);
        lblGiamGia.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        lblGiamGia.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        lblGiamGia.Dock = global::System.Windows.Forms.DockStyle.Fill;

        lblGiamGia.AutoSize = false;
        lblGiamGia.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        lblGiamGia.ForeColor = global::System.Drawing.SystemColors.ControlText;
        lblGiamGia.BackColor = Color.FromArgb(248,250,252);
        lblGiamGia.Cursor = global::System.Windows.Forms.Cursors.Default;
        lblGiamGia.TabIndex = 5;
        lblGiamGia.TabStop = false;
        lblGiamGia.AccessibleName = null;
        lblGiamGia.AutoEllipsis = false;
        lblGiamGia.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        lblGiamGia.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        lblGiamGia.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        lblGiamGia.Visible = true;
        lblGiamGia.Enabled = true;

        // tableLayoutPanel14
        tableLayoutPanel14.Name = "tableLayoutPanel14";
        tableLayoutPanel14.Text = "";
        tableLayoutPanel14.Location = new global::System.Drawing.Point(849, 0);
        tableLayoutPanel14.Size = new global::System.Drawing.Size(285, 59);
        tableLayoutPanel14.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel14.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel14.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel14.AutoSize = false;
        tableLayoutPanel14.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel14.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel14.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel14.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel14.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel14.TabIndex = 3;
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
        tableLayoutPanel14.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // label15
        label15.Name = "label15";
        label15.Text = "Doanh thu thực";
        label15.Location = new global::System.Drawing.Point(3, 0);
        label15.Size = new global::System.Drawing.Size(279, 22);
        label15.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        label15.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        label15.Dock = global::System.Windows.Forms.DockStyle.Fill;

        label15.AutoSize = false;
        label15.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        label15.ForeColor = global::System.Drawing.SystemColors.ControlText;
        label15.BackColor = Color.FromArgb(248,250,252);
        label15.Cursor = global::System.Windows.Forms.Cursors.Default;
        label15.TabIndex = 0;
        label15.TabStop = false;
        label15.AccessibleName = null;
        label15.AutoEllipsis = false;
        label15.TextAlign = global::System.Drawing.ContentAlignment.TopLeft;
        label15.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        label15.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        label15.Visible = true;
        label15.Enabled = true;

        // lblDoanhThu
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Text = "0 đ";
        lblDoanhThu.Location = new global::System.Drawing.Point(3, 22);
        lblDoanhThu.Size = new global::System.Drawing.Size(279, 37);
        lblDoanhThu.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
        lblDoanhThu.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        lblDoanhThu.Dock = global::System.Windows.Forms.DockStyle.Fill;

        lblDoanhThu.AutoSize = false;
        lblDoanhThu.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        lblDoanhThu.ForeColor = global::System.Drawing.SystemColors.ControlText;
        lblDoanhThu.BackColor = Color.FromArgb(248,250,252);
        lblDoanhThu.Cursor = global::System.Windows.Forms.Cursors.Default;
        lblDoanhThu.TabIndex = 7;
        lblDoanhThu.TabStop = false;
        lblDoanhThu.AccessibleName = null;
        lblDoanhThu.AutoEllipsis = false;
        lblDoanhThu.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
        lblDoanhThu.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        lblDoanhThu.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        lblDoanhThu.Visible = true;
        lblDoanhThu.Enabled = true;

        // tableLayoutPanel16
        tableLayoutPanel16.Name = "tableLayoutPanel16";
        tableLayoutPanel16.Text = "";
        tableLayoutPanel16.Location = new global::System.Drawing.Point(3, 185);
        tableLayoutPanel16.Size = new global::System.Drawing.Size(1152, 393);
        tableLayoutPanel16.Margin = new global::System.Windows.Forms.Padding(3, 3, 3, 3);
        tableLayoutPanel16.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        tableLayoutPanel16.Dock = global::System.Windows.Forms.DockStyle.Fill;

        tableLayoutPanel16.AutoSize = false;
        tableLayoutPanel16.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        tableLayoutPanel16.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        tableLayoutPanel16.ForeColor = global::System.Drawing.SystemColors.ControlText;
        tableLayoutPanel16.BackColor = Color.FromArgb(248,250,252);
        tableLayoutPanel16.Cursor = global::System.Windows.Forms.Cursors.Default;
        tableLayoutPanel16.TabIndex = 2;
        tableLayoutPanel16.TabStop = false;
        tableLayoutPanel16.AccessibleName = null;
        tableLayoutPanel16.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
        tableLayoutPanel16.CellBorderStyle = global::System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel16.RowCount = 1;
        tableLayoutPanel16.GrowStyle = global::System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
        tableLayoutPanel16.AutoScroll = false;
        tableLayoutPanel16.Visible = true;
        tableLayoutPanel16.ColumnCount = 2;
        tableLayoutPanel16.Enabled = true;
        tableLayoutPanel16.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 38F));
        tableLayoutPanel16.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 62F));
        tableLayoutPanel16.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100F));

        // groupBox17
        groupBox17.Name = "groupBox17";
        groupBox17.Text = "Doanh thu theo ngày";
        groupBox17.Location = new global::System.Drawing.Point(0, 0);
        groupBox17.Size = new global::System.Drawing.Size(460, 393);
        groupBox17.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox17.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox17.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox17.AutoSize = false;
        groupBox17.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox17.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox17.ForeColor = global::System.Drawing.SystemColors.ControlText;
        groupBox17.BackColor = Color.FromArgb(248,250,252);
        groupBox17.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox17.TabIndex = 0;
        groupBox17.TabStop = false;
        groupBox17.AccessibleName = null;
        groupBox17.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox17.Visible = true;
        groupBox17.Enabled = true;

        // dgvTheoNgay
        dgvTheoNgay.Name = "dgvTheoNgay";
        dgvTheoNgay.Text = "";
        dgvTheoNgay.Location = new global::System.Drawing.Point(12, 25);
        dgvTheoNgay.Size = new global::System.Drawing.Size(436, 356);
        dgvTheoNgay.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        dgvTheoNgay.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgay.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvTheoNgay.AutoSize = false;
        dgvTheoNgay.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvTheoNgay.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvTheoNgay.BackColor = Color.FromArgb(248,250,252);
        dgvTheoNgay.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvTheoNgay.TabIndex = 0;
        dgvTheoNgay.TabStop = true;
        dgvTheoNgay.AccessibleName = null;
        dgvTheoNgay.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvTheoNgay.ReadOnly = true;
        dgvTheoNgay.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvTheoNgay.AllowUserToAddRows = false;
        dgvTheoNgay.AllowUserToDeleteRows = false;
        dgvTheoNgay.AllowUserToResizeRows = false;
        dgvTheoNgay.AllowUserToResizeColumns = true;
        dgvTheoNgay.RowHeadersVisible = false;
        dgvTheoNgay.MultiSelect = false;
        dgvTheoNgay.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvTheoNgay.ShowCellToolTips = true;
        dgvTheoNgay.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvTheoNgay.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
        dgvTheoNgay.BackgroundColor = global::System.Drawing.SystemColors.ScrollBar;
        dgvTheoNgay.GridColor = global::System.Drawing.SystemColors.ControlLight;
        dgvTheoNgay.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvTheoNgay.EnableHeadersVisualStyles = false;
        dgvTheoNgay.ColumnHeadersHeight = 59;
        dgvTheoNgay.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvTheoNgay.ColumnHeadersVisible = true;
        dgvTheoNgay.Visible = true;
        dgvTheoNgay.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvTheoNgayDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvTheoNgayDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvTheoNgayDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvTheoNgayDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvTheoNgayDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvTheoNgayDefaultCellStyle.Format = "";
        dgvTheoNgayDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvTheoNgayDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvTheoNgayDefaultCellStyle.NullValue = "";
        dgvTheoNgay.DefaultCellStyle = dgvTheoNgayDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvTheoNgayColumnHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvTheoNgayColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvTheoNgayColumnHeadersDefaultCellStyle.Format = "";
        dgvTheoNgayColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvTheoNgayColumnHeadersDefaultCellStyle.NullValue = "";
        dgvTheoNgay.ColumnHeadersDefaultCellStyle = dgvTheoNgayColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.Font = null;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.Format = "";
        dgvTheoNgayAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvTheoNgay.AlternatingRowsDefaultCellStyle = dgvTheoNgayAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvTheoNgayRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvTheoNgayRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvTheoNgayRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvTheoNgayRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvTheoNgayRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvTheoNgayRowHeadersDefaultCellStyle.Format = "";
        dgvTheoNgayRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvTheoNgayRowHeadersDefaultCellStyle.NullValue = "";
        dgvTheoNgay.RowHeadersDefaultCellStyle = dgvTheoNgayRowHeadersDefaultCellStyle;
        dgvTheoNgay.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn1Style.Font = null;
        dgvTheoNgayColumn1Style.Format = "dd/MM/yyyy";
        dgvTheoNgayColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn2Style.Font = null;
        dgvTheoNgayColumn2Style.Format = "";
        dgvTheoNgayColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn3Style.Font = null;
        dgvTheoNgayColumn3Style.Format = "N0";
        dgvTheoNgayColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn4Style.Font = null;
        dgvTheoNgayColumn4Style.Format = "N0";
        dgvTheoNgayColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayColumn4Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvTheoNgayColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvTheoNgayColumn5Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvTheoNgayColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvTheoNgayColumn5Style.Font = null;
        dgvTheoNgayColumn5Style.Format = "N0";
        dgvTheoNgayColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvTheoNgayColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvTheoNgayColumn5Style.NullValue = "";
        

        // groupBox18
        groupBox18.Name = "groupBox18";
        groupBox18.Text = "Danh sách hóa đơn";
        groupBox18.Location = new global::System.Drawing.Point(460, 0);
        groupBox18.Size = new global::System.Drawing.Size(692, 393);
        groupBox18.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        groupBox18.Padding = new global::System.Windows.Forms.Padding(12, 2, 12, 12);
        groupBox18.Dock = global::System.Windows.Forms.DockStyle.Fill;

        groupBox18.AutoSize = false;
        groupBox18.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowOnly;
        groupBox18.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2F, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point);
        groupBox18.ForeColor = global::System.Drawing.SystemColors.ControlText;
        groupBox18.BackColor = Color.FromArgb(248,250,252);
        groupBox18.Cursor = global::System.Windows.Forms.Cursors.Default;
        groupBox18.TabIndex = 1;
        groupBox18.TabStop = false;
        groupBox18.AccessibleName = null;
        groupBox18.FlatStyle = global::System.Windows.Forms.FlatStyle.Standard;
        groupBox18.Visible = true;
        groupBox18.Enabled = true;

        // dgvHoaDon
        dgvHoaDon.Name = "dgvHoaDon";
        dgvHoaDon.Text = "";
        dgvHoaDon.Location = new global::System.Drawing.Point(12, 25);
        dgvHoaDon.Size = new global::System.Drawing.Size(668, 356);
        dgvHoaDon.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
        dgvHoaDon.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDon.Dock = global::System.Windows.Forms.DockStyle.Fill;

        dgvHoaDon.AutoSize = false;
        dgvHoaDon.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvHoaDon.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvHoaDon.BackColor = Color.FromArgb(248,250,252);
        dgvHoaDon.Cursor = global::System.Windows.Forms.Cursors.Default;
        dgvHoaDon.TabIndex = 0;
        dgvHoaDon.TabStop = true;
        dgvHoaDon.AccessibleName = null;
        dgvHoaDon.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
        dgvHoaDon.ReadOnly = true;
        dgvHoaDon.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
        dgvHoaDon.AllowUserToAddRows = false;
        dgvHoaDon.AllowUserToDeleteRows = false;
        dgvHoaDon.AllowUserToResizeRows = false;
        dgvHoaDon.AllowUserToResizeColumns = true;
        dgvHoaDon.RowHeadersVisible = false;
        dgvHoaDon.MultiSelect = false;
        dgvHoaDon.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dgvHoaDon.ShowCellToolTips = true;
        dgvHoaDon.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        dgvHoaDon.AutoSizeRowsMode = global::System.Windows.Forms.DataGridViewAutoSizeRowsMode.None;
        dgvHoaDon.BackgroundColor = global::System.Drawing.SystemColors.ScrollBar;
        dgvHoaDon.GridColor = global::System.Drawing.SystemColors.ControlLight;
        dgvHoaDon.CellBorderStyle = global::System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        dgvHoaDon.EnableHeadersVisualStyles = false;
        dgvHoaDon.ColumnHeadersHeight = 59;
        dgvHoaDon.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHoaDon.ColumnHeadersVisible = true;
        dgvHoaDon.Visible = true;
        dgvHoaDon.Enabled = true;
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvHoaDonDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvHoaDonDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvHoaDonDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.GradientActiveCaption;
        dgvHoaDonDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvHoaDonDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvHoaDonDefaultCellStyle.Format = "";
        dgvHoaDonDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(4, 4, 4, 4);
        dgvHoaDonDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
        dgvHoaDonDefaultCellStyle.NullValue = "";
        dgvHoaDon.DefaultCellStyle = dgvHoaDonDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumnHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumnHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvHoaDonColumnHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
        dgvHoaDonColumnHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumnHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumnHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvHoaDonColumnHeadersDefaultCellStyle.Format = "";
        dgvHoaDonColumnHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumnHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvHoaDonColumnHeadersDefaultCellStyle.NullValue = "";
        dgvHoaDon.ColumnHeadersDefaultCellStyle = dgvHoaDonColumnHeadersDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonAlternatingRowsDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonAlternatingRowsDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonAlternatingRowsDefaultCellStyle.BackColor = global::System.Drawing.Color.White;
        dgvHoaDonAlternatingRowsDefaultCellStyle.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonAlternatingRowsDefaultCellStyle.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonAlternatingRowsDefaultCellStyle.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonAlternatingRowsDefaultCellStyle.Font = null;
        dgvHoaDonAlternatingRowsDefaultCellStyle.Format = "";
        dgvHoaDonAlternatingRowsDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonAlternatingRowsDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonAlternatingRowsDefaultCellStyle.NullValue = "";
        dgvHoaDon.AlternatingRowsDefaultCellStyle = dgvHoaDonAlternatingRowsDefaultCellStyle;
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonRowHeadersDefaultCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonRowHeadersDefaultCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dgvHoaDonRowHeadersDefaultCellStyle.BackColor = Color.FromArgb(248,250,252);
        dgvHoaDonRowHeadersDefaultCellStyle.ForeColor = global::System.Drawing.SystemColors.WindowText;
        dgvHoaDonRowHeadersDefaultCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
        dgvHoaDonRowHeadersDefaultCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
        dgvHoaDonRowHeadersDefaultCellStyle.Font = new global::System.Drawing.Font("Segoe UI", 10.2F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point);
        dgvHoaDonRowHeadersDefaultCellStyle.Format = "";
        dgvHoaDonRowHeadersDefaultCellStyle.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonRowHeadersDefaultCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.True;
        dgvHoaDonRowHeadersDefaultCellStyle.NullValue = "";
        dgvHoaDon.RowHeadersDefaultCellStyle = dgvHoaDonRowHeadersDefaultCellStyle;
        dgvHoaDon.RowTemplate.Height = 34;
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn1Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn1Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn1Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn1Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn1Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn1Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn1Style.Font = null;
        dgvHoaDonColumn1Style.Format = "";
        dgvHoaDonColumn1Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn1Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn1Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn2Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn2Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn2Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn2Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn2Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn2Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn2Style.Font = null;
        dgvHoaDonColumn2Style.Format = "dd/MM/yyyy HH:mm";
        dgvHoaDonColumn2Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn2Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn2Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn3Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn3Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn3Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn3Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn3Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn3Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn3Style.Font = null;
        dgvHoaDonColumn3Style.Format = "";
        dgvHoaDonColumn3Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn3Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn3Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn4Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn4Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn4Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn4Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn4Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn4Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn4Style.Font = null;
        dgvHoaDonColumn4Style.Format = "";
        dgvHoaDonColumn4Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn4Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn4Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn5Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn5Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn5Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn5Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn5Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn5Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn5Style.Font = null;
        dgvHoaDonColumn5Style.Format = "N0";
        dgvHoaDonColumn5Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn5Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn5Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn6Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn6Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn6Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn6Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn6Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn6Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn6Style.Font = null;
        dgvHoaDonColumn6Style.Format = "N0";
        dgvHoaDonColumn6Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn6Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn6Style.NullValue = "";
        
        

        
        global::System.Windows.Forms.DataGridViewCellStyle dgvHoaDonColumn7Style = new global::System.Windows.Forms.DataGridViewCellStyle();
        dgvHoaDonColumn7Style.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.NotSet;
        dgvHoaDonColumn7Style.BackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn7Style.ForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn7Style.SelectionBackColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn7Style.SelectionForeColor = global::System.Drawing.Color.Empty;
        dgvHoaDonColumn7Style.Font = null;
        dgvHoaDonColumn7Style.Format = "N0";
        dgvHoaDonColumn7Style.Padding = new global::System.Windows.Forms.Padding(0, 0, 0, 0);
        dgvHoaDonColumn7Style.WrapMode = global::System.Windows.Forms.DataGridViewTriState.NotSet;
        dgvHoaDonColumn7Style.NullValue = "";
        
        this.Controls.Add(posLayout);
        excelActions=new FlowLayoutPanel();excelActions.Name="excelActions";excelActions.Dock=DockStyle.Fill;excelActions.Margin=Padding.Empty;excelActions.WrapContents=false;

        posLayout.Controls.Add(posHeading, 0, 0);
        btnXuatExcel=new Button();btnXuatExcel.Name="btnXuatExcel";btnXuatExcel.Text="Xuất Excel";btnXuatExcel.Size=new Size(120,38);btnXuatExcel.Font=new Font("Segoe UI Semibold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);btnXuatExcel.Margin=new Padding(3);excelActions.Controls.Add(btnXuatExcel);

        posLayout.Controls.Add(excelActions,0,1);

        posLayout.Controls.Add(tableLayoutPanel1, 0, 2);
        tableLayoutPanel1.Controls.Add(groupBox2, 0, 0);
        groupBox2.Controls.Add(tableLayoutPanel3);
        tableLayoutPanel3.Controls.Add(label4, 0, 0);
        tableLayoutPanel3.Controls.Add(dtpTuNgay, 1, 0);
        tableLayoutPanel3.Controls.Add(label5, 2, 0);
        tableLayoutPanel3.Controls.Add(dtpDenNgay, 3, 0);
        tableLayoutPanel3.Controls.Add(btnXem, 4, 0);
        tableLayoutPanel1.Controls.Add(groupBox6, 0, 1);
        groupBox6.Controls.Add(tableLayoutPanel7);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel8, -1, -1);
        tableLayoutPanel8.Controls.Add(label9, 0, 0);
        tableLayoutPanel8.Controls.Add(lblSoHD, 0, 1);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel10, -1, -1);
        tableLayoutPanel10.Controls.Add(label11, 0, 0);
        tableLayoutPanel10.Controls.Add(lblTongTien, 0, 1);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel12, -1, -1);
        tableLayoutPanel12.Controls.Add(label13, 0, 0);
        tableLayoutPanel12.Controls.Add(lblGiamGia, 0, 1);
        tableLayoutPanel7.Controls.Add(tableLayoutPanel14, -1, -1);
        tableLayoutPanel14.Controls.Add(label15, 0, 0);
        tableLayoutPanel14.Controls.Add(lblDoanhThu, 0, 1);
        tableLayoutPanel1.Controls.Add(tableLayoutPanel16, 0, 2);
        tableLayoutPanel16.Controls.Add(groupBox17, 0, 0);
        groupBox17.Controls.Add(dgvTheoNgay);
        tableLayoutPanel16.Controls.Add(groupBox18, 1, 0);
        groupBox18.Controls.Add(dgvHoaDon);
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
        Text = "Báo cáo doanh thu | Hiệu thuốc ABC";
        KeyPreview = false;
        ShowIcon = true;
        ShowInTaskbar = true;
        Name = "frmBaoCao";
        btnXem.Click += btnXem_Click;
        Load += frmBaoCao_Load;
        ((global::System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
        groupBox18.ResumeLayout(false);
        groupBox18.PerformLayout();
                //
        // dgvTheoNgayColumn1
        //
        this.dgvTheoNgayColumn1.Name = "Ngay";
        this.dgvTheoNgayColumn1.HeaderText = "Ngày";
        this.dgvTheoNgayColumn1.Visible = true;
        this.dgvTheoNgayColumn1.ReadOnly = true;
        this.dgvTheoNgayColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvTheoNgayColumn1.Resizable = DataGridViewTriState.False;
        this.dgvTheoNgayColumn1.DataPropertyName = "Ngay";
        this.dgvTheoNgayColumn1.DefaultCellStyle = dgvTheoNgayColumn1Style;
        this.dgvTheoNgayColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvTheoNgayColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvTheoNgayColumn1.MinimumWidth = 5;
        this.dgvTheoNgayColumn1.Width = 112;
        this.dgvTheoNgayColumn1.FillWeight = 100F;
        //
        // dgvTheoNgayColumn2
        //
        this.dgvTheoNgayColumn2.Name = "SoHoaDon";
        this.dgvTheoNgayColumn2.HeaderText = "Số hóa đơn";
        this.dgvTheoNgayColumn2.Visible = true;
        this.dgvTheoNgayColumn2.ReadOnly = true;
        this.dgvTheoNgayColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvTheoNgayColumn2.Resizable = DataGridViewTriState.False;
        this.dgvTheoNgayColumn2.DataPropertyName = "SoHoaDon";
        this.dgvTheoNgayColumn2.DefaultCellStyle = dgvTheoNgayColumn2Style;
        this.dgvTheoNgayColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvTheoNgayColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvTheoNgayColumn2.MinimumWidth = 5;
        this.dgvTheoNgayColumn2.Width = 98;
        this.dgvTheoNgayColumn2.FillWeight = 50F;
        //
        // dgvTheoNgayColumn3
        //
        this.dgvTheoNgayColumn3.Name = "TongTien";
        this.dgvTheoNgayColumn3.HeaderText = "Tổng tiền";
        this.dgvTheoNgayColumn3.Visible = true;
        this.dgvTheoNgayColumn3.ReadOnly = true;
        this.dgvTheoNgayColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvTheoNgayColumn3.Resizable = DataGridViewTriState.False;
        this.dgvTheoNgayColumn3.DataPropertyName = "TongTien";
        this.dgvTheoNgayColumn3.DefaultCellStyle = dgvTheoNgayColumn3Style;
        this.dgvTheoNgayColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvTheoNgayColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvTheoNgayColumn3.MinimumWidth = 5;
        this.dgvTheoNgayColumn3.Width = 126;
        this.dgvTheoNgayColumn3.FillWeight = 85F;
        //
        // dgvTheoNgayColumn4
        //
        this.dgvTheoNgayColumn4.Name = "GiamGia";
        this.dgvTheoNgayColumn4.HeaderText = "Giảm giá";
        this.dgvTheoNgayColumn4.Visible = true;
        this.dgvTheoNgayColumn4.ReadOnly = true;
        this.dgvTheoNgayColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvTheoNgayColumn4.Resizable = DataGridViewTriState.False;
        this.dgvTheoNgayColumn4.DataPropertyName = "GiamGia";
        this.dgvTheoNgayColumn4.DefaultCellStyle = dgvTheoNgayColumn4Style;
        this.dgvTheoNgayColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvTheoNgayColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvTheoNgayColumn4.MinimumWidth = 5;
        this.dgvTheoNgayColumn4.Width = 118;
        this.dgvTheoNgayColumn4.FillWeight = 70F;
        //
        // dgvTheoNgayColumn5
        //
        this.dgvTheoNgayColumn5.Name = "DoanhThu";
        this.dgvTheoNgayColumn5.HeaderText = "Doanh thu";
        this.dgvTheoNgayColumn5.Visible = true;
        this.dgvTheoNgayColumn5.ReadOnly = true;
        this.dgvTheoNgayColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvTheoNgayColumn5.Resizable = DataGridViewTriState.False;
        this.dgvTheoNgayColumn5.DataPropertyName = "DoanhThu";
        this.dgvTheoNgayColumn5.DefaultCellStyle = dgvTheoNgayColumn5Style;
        this.dgvTheoNgayColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvTheoNgayColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvTheoNgayColumn5.MinimumWidth = 5;
        this.dgvTheoNgayColumn5.Width = 132;
        this.dgvTheoNgayColumn5.FillWeight = 85F;
        //
        // dgvHoaDonColumn1
        //
        this.dgvHoaDonColumn1.Name = "MaHD";
        this.dgvHoaDonColumn1.HeaderText = "Mã hóa đơn";
        this.dgvHoaDonColumn1.Visible = true;
        this.dgvHoaDonColumn1.ReadOnly = true;
        this.dgvHoaDonColumn1.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn1.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn1.DataPropertyName = "MaHD";
        this.dgvHoaDonColumn1.DefaultCellStyle = dgvHoaDonColumn1Style;
        this.dgvHoaDonColumn1.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn1.MinimumWidth = 5;
        this.dgvHoaDonColumn1.Width = 103;
        this.dgvHoaDonColumn1.FillWeight = 48F;
        //
        // dgvHoaDonColumn2
        //
        this.dgvHoaDonColumn2.Name = "NgayLap";
        this.dgvHoaDonColumn2.HeaderText = "Ngày thanh toán";
        this.dgvHoaDonColumn2.Visible = true;
        this.dgvHoaDonColumn2.ReadOnly = true;
        this.dgvHoaDonColumn2.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn2.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn2.DataPropertyName = "NgayLap";
        this.dgvHoaDonColumn2.DefaultCellStyle = dgvHoaDonColumn2Style;
        this.dgvHoaDonColumn2.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn2.MinimumWidth = 5;
        this.dgvHoaDonColumn2.Width = 180;
        this.dgvHoaDonColumn2.FillWeight = 110F;
        //
        // dgvHoaDonColumn3
        //
        this.dgvHoaDonColumn3.Name = "NhanVien";
        this.dgvHoaDonColumn3.HeaderText = "Nhân viên";
        this.dgvHoaDonColumn3.Visible = true;
        this.dgvHoaDonColumn3.ReadOnly = true;
        this.dgvHoaDonColumn3.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn3.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn3.DataPropertyName = "NhanVien";
        this.dgvHoaDonColumn3.DefaultCellStyle = dgvHoaDonColumn3Style;
        this.dgvHoaDonColumn3.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn3.MinimumWidth = 5;
        this.dgvHoaDonColumn3.Width = 160;
        this.dgvHoaDonColumn3.FillWeight = 90F;
        //
        // dgvHoaDonColumn4
        //
        this.dgvHoaDonColumn4.Name = "Voucher";
        this.dgvHoaDonColumn4.HeaderText = "Mã voucher";
        this.dgvHoaDonColumn4.Visible = true;
        this.dgvHoaDonColumn4.ReadOnly = true;
        this.dgvHoaDonColumn4.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn4.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn4.DataPropertyName = "Voucher";
        this.dgvHoaDonColumn4.DefaultCellStyle = dgvHoaDonColumn4Style;
        this.dgvHoaDonColumn4.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn4.MinimumWidth = 5;
        this.dgvHoaDonColumn4.Width = 160;
        this.dgvHoaDonColumn4.FillWeight = 50F;
        //
        // dgvHoaDonColumn5
        //
        this.dgvHoaDonColumn5.Name = "TongTien";
        this.dgvHoaDonColumn5.HeaderText = "Tổng tiền";
        this.dgvHoaDonColumn5.Visible = true;
        this.dgvHoaDonColumn5.ReadOnly = true;
        this.dgvHoaDonColumn5.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn5.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn5.DataPropertyName = "TongTien";
        this.dgvHoaDonColumn5.DefaultCellStyle = dgvHoaDonColumn5Style;
        this.dgvHoaDonColumn5.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn5.MinimumWidth = 5;
        this.dgvHoaDonColumn5.Width = 126;
        this.dgvHoaDonColumn5.FillWeight = 80F;
        //
        // dgvHoaDonColumn6
        //
        this.dgvHoaDonColumn6.Name = "GiamGia";
        this.dgvHoaDonColumn6.HeaderText = "Giảm giá";
        this.dgvHoaDonColumn6.Visible = true;
        this.dgvHoaDonColumn6.ReadOnly = true;
        this.dgvHoaDonColumn6.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn6.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn6.DataPropertyName = "GiamGia";
        this.dgvHoaDonColumn6.DefaultCellStyle = dgvHoaDonColumn6Style;
        this.dgvHoaDonColumn6.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn6.MinimumWidth = 5;
        this.dgvHoaDonColumn6.Width = 118;
        this.dgvHoaDonColumn6.FillWeight = 65F;
        //
        // dgvHoaDonColumn7
        //
        this.dgvHoaDonColumn7.Name = "ThucThu";
        this.dgvHoaDonColumn7.HeaderText = "Thực thu";
        this.dgvHoaDonColumn7.Visible = true;
        this.dgvHoaDonColumn7.ReadOnly = true;
        this.dgvHoaDonColumn7.SortMode = global::System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        this.dgvHoaDonColumn7.Resizable = DataGridViewTriState.False;
        this.dgvHoaDonColumn7.DataPropertyName = "ThucThu";
        this.dgvHoaDonColumn7.DefaultCellStyle = dgvHoaDonColumn7Style;
        this.dgvHoaDonColumn7.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        this.dgvHoaDonColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.dgvHoaDonColumn7.MinimumWidth = 5;
        this.dgvHoaDonColumn7.Width = 118;
        this.dgvHoaDonColumn7.FillWeight = 80F;

        this.dgvTheoNgay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvTheoNgay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvTheoNgayColumn1, this.dgvTheoNgayColumn2, this.dgvTheoNgayColumn3, this.dgvTheoNgayColumn4, this.dgvTheoNgayColumn5 });

        this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.dgvHoaDonColumn1, this.dgvHoaDonColumn2, this.dgvHoaDonColumn3, this.dgvHoaDonColumn4, this.dgvHoaDonColumn5, this.dgvHoaDonColumn6, this.dgvHoaDonColumn7 });
        ((global::System.ComponentModel.ISupportInitialize)dgvTheoNgay).EndInit();
        groupBox17.ResumeLayout(false);
        groupBox17.PerformLayout();
        tableLayoutPanel16.ResumeLayout(false);
        tableLayoutPanel16.PerformLayout();
        tableLayoutPanel14.ResumeLayout(false);
        tableLayoutPanel14.PerformLayout();
        tableLayoutPanel12.ResumeLayout(false);
        tableLayoutPanel12.PerformLayout();
        tableLayoutPanel10.ResumeLayout(false);
        tableLayoutPanel10.PerformLayout();
        tableLayoutPanel8.ResumeLayout(false);
        tableLayoutPanel8.PerformLayout();
        tableLayoutPanel7.ResumeLayout(false);
        tableLayoutPanel7.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        tableLayoutPanel3.ResumeLayout(false);
        tableLayoutPanel3.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        posLayout.ResumeLayout(false);
        posLayout.PerformLayout();
        posLayout.RowStyles.Insert(1,new RowStyle(SizeType.Absolute,46F));posLayout.RowCount=3;posLayout.SetRow(tableLayoutPanel1,2);
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        btnXem.BackColor=Color.FromArgb(15, 118, 110);
        btnXem.FlatStyle = FlatStyle.Flat;
        btnXem.FlatAppearance.BorderSize = 0;
        btnXem.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnXem.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnXem.Cursor = Cursors.Hand;
        btnXem.UseVisualStyleBackColor = false;btnXem.ForeColor=Color.White;
        btnXuatExcel.BackColor=Color.FromArgb(15, 118, 110);
        btnXuatExcel.FlatStyle = FlatStyle.Flat;
        btnXuatExcel.FlatAppearance.BorderSize = 0;
        btnXuatExcel.FlatAppearance.MouseOverBackColor = Color.FromArgb(13, 148, 136);
        btnXuatExcel.FlatAppearance.MouseDownBackColor = Color.FromArgb(17, 94, 89);
        btnXuatExcel.Cursor = Cursors.Hand;
        btnXuatExcel.UseVisualStyleBackColor = false;btnXuatExcel.ForeColor=Color.White;
        dgvTheoNgay.BackgroundColor=Color.White;dgvTheoNgay.EnableHeadersVisualStyles=false;
        dgvTheoNgay.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvTheoNgay.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvTheoNgay.DefaultCellStyle.BackColor=Color.White;dgvTheoNgay.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvTheoNgay.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvTheoNgay.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvTheoNgay.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvHoaDon.BackgroundColor=Color.White;dgvHoaDon.EnableHeadersVisualStyles=false;
        dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(15,118,110);dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
        dgvHoaDon.DefaultCellStyle.BackColor=Color.White;dgvHoaDon.DefaultCellStyle.ForeColor=Color.FromArgb(15,23,42);
        dgvHoaDon.DefaultCellStyle.SelectionBackColor=Color.FromArgb(204,251,241);dgvHoaDon.DefaultCellStyle.SelectionForeColor=Color.FromArgb(15,23,42);
        dgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(248,250,252);
        dgvTheoNgay.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvTheoNgay.MultiSelect=false;
        dgvTheoNgay.AllowUserToResizeColumns=false;
        dgvTheoNgay.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvTheoNgay.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvTheoNgay.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvTheoNgay.ColumnHeadersDefaultCellStyle.BackColor;
        dgvTheoNgay.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvTheoNgay.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvTheoNgay.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvTheoNgay.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        dgvHoaDon.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvHoaDon.MultiSelect=false;
        dgvHoaDon.AllowUserToResizeColumns=false;
        dgvHoaDon.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvHoaDon.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvHoaDon.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor;
        dgvHoaDon.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvHoaDon.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvHoaDon.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                dgvHoaDon.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvHoaDon.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvHoaDon.ColumnHeadersHeight=43;
        dgvTheoNgay.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvTheoNgay.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvTheoNgay.ColumnHeadersHeight=43;
        
        
        
        
        
        
        
        
        
        
        
        
        ResumeLayout(false);
        PerformLayout();
    }

    private global::System.Windows.Forms.TableLayoutPanel posLayout;
    private global::System.Windows.Forms.Label posHeading;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private global::System.Windows.Forms.GroupBox groupBox2;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private global::System.Windows.Forms.Label label4;
    private global::System.Windows.Forms.DateTimePicker dtpTuNgay;
    private global::System.Windows.Forms.Label label5;
    private global::System.Windows.Forms.DateTimePicker dtpDenNgay;
    private global::System.Windows.Forms.Button btnXem;
    private global::System.Windows.Forms.GroupBox groupBox6;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    private global::System.Windows.Forms.Label label9;
    private global::System.Windows.Forms.Label lblSoHD;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
    private global::System.Windows.Forms.Label label11;
    private global::System.Windows.Forms.Label lblTongTien;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
    private global::System.Windows.Forms.Label label13;
    private global::System.Windows.Forms.Label lblGiamGia;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    private global::System.Windows.Forms.Label label15;
    private global::System.Windows.Forms.Label lblDoanhThu;
    private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
    private global::System.Windows.Forms.GroupBox groupBox17;
    private global::System.Windows.Forms.DataGridView dgvTheoNgay;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvTheoNgayColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvTheoNgayColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvTheoNgayColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvTheoNgayColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvTheoNgayColumn5;
    private global::System.Windows.Forms.GroupBox groupBox18;
    private global::System.Windows.Forms.DataGridView dgvHoaDon;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn1;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn2;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn3;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn4;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn5;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn6;
    private global::System.Windows.Forms.DataGridViewTextBoxColumn dgvHoaDonColumn7;
    private FlowLayoutPanel excelActions;
    private Button btnXuatExcel;
}

}