using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;
#nullable disable
namespace Store.POS
{
partial class frmXemTruocExcel
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        this.colKeDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colHoatChat = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.previewCol9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        layout=new TableLayoutPanel();dgvDuLieu=new DataGridView();txtLoi=new TextBox();lblKetQua=new Label();actions=new FlowLayoutPanel();btnNhap=new Button();btnHuy=new Button();
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).BeginInit();SuspendLayout();
        layout.Name="layout";layout.Dock=DockStyle.Fill;layout.Padding=new Padding(12);layout.ColumnCount=1;layout.RowCount=4;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,40));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));
        lblKetQua.Name="lblKetQua";lblKetQua.Dock=DockStyle.Fill;
        dgvDuLieu.Name="dgvDuLieu";
        dgvDuLieu.GridColor = Color.FromArgb(226, 232, 240);
        dgvDuLieu.BackgroundColor = Color.White;dgvDuLieu.Dock=DockStyle.Fill;dgvDuLieu.ReadOnly=true;dgvDuLieu.AllowUserToAddRows=false;dgvDuLieu.AllowUserToDeleteRows=false;
        dgvDuLieu.ShowCellToolTips = true;
        dgvDuLieu.DefaultCellStyle.WrapMode = DataGridViewTriState.False;dgvDuLieu.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        txtLoi.Name="txtLoi";txtLoi.Dock=DockStyle.Fill;txtLoi.Multiline=true;txtLoi.ReadOnly=true;txtLoi.ScrollBars=ScrollBars.Vertical;
        actions.Dock=DockStyle.Fill;actions.FlowDirection=FlowDirection.RightToLeft;
        btnNhap.Name="btnNhap";
        btnNhap.BackColor = Color.FromArgb(22, 101, 52);
        btnNhap.ForeColor = Color.White;
        btnNhap.FlatStyle = FlatStyle.Flat;
        btnNhap.FlatAppearance.BorderSize = 0;
        btnNhap.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 128, 61);
        btnNhap.FlatAppearance.MouseDownBackColor = Color.FromArgb(18, 83, 43);
        btnNhap.Cursor = Cursors.Hand;
        btnNhap.UseVisualStyleBackColor = false;btnNhap.Size=new Size(230,42);btnNhap.DialogResult=DialogResult.OK;
        btnHuy.Name="btnHuy";
        btnHuy.BackColor = Color.FromArgb(100, 116, 139);
        btnHuy.ForeColor = Color.White;
        btnHuy.FlatStyle = FlatStyle.Flat;
        btnHuy.FlatAppearance.BorderSize = 0;
        btnHuy.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 163, 184);
        btnHuy.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 85, 105);
        btnHuy.Cursor = Cursors.Hand;
        btnHuy.UseVisualStyleBackColor = false;btnHuy.Text="Bỏ qua";btnHuy.Size=new Size(120,42);btnHuy.DialogResult=DialogResult.Cancel;
        actions.Controls.Add(btnHuy);actions.Controls.Add(btnNhap);layout.Controls.Add(lblKetQua,0,0);layout.Controls.Add(dgvDuLieu,0,1);layout.Controls.Add(txtLoi,0,2);layout.Controls.Add(actions,0,3);
        AutoScaleDimensions=new SizeF(8,20);AutoScaleMode=AutoScaleMode.Font;Font=new Font("Segoe UI",10.2F);ClientSize=new Size(1000,650);MinimumSize=new Size(800,550);
        Name="frmXemTruocExcel";Text="Kiểm tra dữ liệu Excel";StartPosition=FormStartPosition.CenterParent;AcceptButton=btnNhap;CancelButton=btnHuy;Controls.Add(layout);
        
          
        
        
        
        dgvDuLieu.RowHeadersVisible=false;
        
        
                //
        // previewCol0
        //
        this.previewCol0.Name = "previewCol0";
        this.previewCol0.HeaderText = "Tên thuốc";
        this.previewCol0.DataPropertyName = "Thuốc";
        this.previewCol0.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol0.MinimumWidth = 5;
        this.previewCol0.Width = 125;
        this.previewCol0.FillWeight = 220F;
        //
        // previewCol1
        //
        this.previewCol1.Name = "previewCol1";
        this.previewCol1.HeaderText = "Mã vạch";
        this.previewCol1.DataPropertyName = "Mã vạch";
        this.previewCol1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol1.MinimumWidth = 5;
        this.previewCol1.Width = 110;
        this.previewCol1.FillWeight = 80F;
        //
        // previewCol2
        //
        this.previewCol2.Name = "previewCol2";
        this.previewCol2.HeaderText = "Nhóm thuốc";
        this.previewCol2.DataPropertyName = "Nhóm";
        this.previewCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol2.MinimumWidth = 5;
        this.previewCol2.Width = 139;
        this.previewCol2.FillWeight = 220F;
        //
        // previewCol3
        //
        this.previewCol3.Name = "previewCol3";
        this.previewCol3.HeaderText = "Nhà cung cấp";
        this.previewCol3.Width = 149;
        this.previewCol3.MinimumWidth = 5;
        this.previewCol3.DataPropertyName = "NCC";
        this.previewCol3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol3.FillWeight = 48F;
        //
        // previewCol4
        //
        this.previewCol4.Name = "previewCol4";
        this.previewCol4.HeaderText = "Đơn vị tính";
        this.previewCol4.DataPropertyName = "Đơn vị";
        this.previewCol4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol4.MinimumWidth = 5;
        this.previewCol4.Width = 126;
        this.previewCol4.FillWeight = 72F;
        //
        // previewCol5
        //
        this.previewCol5.Name = "previewCol5";
        this.previewCol5.HeaderText = "Giá bán";
        this.previewCol5.DataPropertyName = "Giá bán";
        this.previewCol5.DefaultCellStyle.Format = "N0";
        this.previewCol5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.previewCol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol5.MinimumWidth = 5;
        this.previewCol5.Width = 110;
        this.previewCol5.FillWeight = 80F;
        //
        // previewCol6
        //
        this.previewCol6.Name = "previewCol6";
        this.previewCol6.HeaderText = "Số lô";
        this.previewCol6.DataPropertyName = "Số lô";
        this.previewCol6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol6.MinimumWidth = 5;
        this.previewCol6.Width = 110;
        this.previewCol6.FillWeight = 64F;
        //
        // previewCol7
        //
        this.previewCol7.Name = "previewCol7";
        this.previewCol7.HeaderText = "Hạn dùng";
        this.previewCol7.DataPropertyName = "Hạn dùng";
        this.previewCol7.DefaultCellStyle.Format = "dd/MM/yyyy";
        this.previewCol7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol7.MinimumWidth = 5;
        this.previewCol7.Width = 118;
        this.previewCol7.FillWeight = 88F;
        //
        // previewCol8
        //
        this.previewCol8.Name = "previewCol8";
        this.previewCol8.HeaderText = "Số lượng";
        this.previewCol8.DataPropertyName = "Số lượng";
        this.previewCol8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol8.MinimumWidth = 5;
        this.previewCol8.Width = 110;
        this.previewCol8.FillWeight = 88F;
        //
        // previewCol9
        //
        this.previewCol9.Name = "previewCol9";
        this.previewCol9.HeaderText = "Giá nhập";
        this.previewCol9.DataPropertyName = "Giá nhập";
        this.previewCol9.DefaultCellStyle.Format = "N0";
        this.previewCol9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        this.previewCol9.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.previewCol9.MinimumWidth = 5;
        this.previewCol9.Width = 111;
        this.previewCol9.FillWeight = 88F;

        this.colKeDon.Name = "colKeDon";
        this.colKeDon.HeaderText = "Kê đơn";
        this.colKeDon.DataPropertyName = "Kê đơn";
        this.colKeDon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colKeDon.Width = 90;
        this.colKeDon.ReadOnly = true;
        this.colHoatChat.Name = "colHoatChat";
        this.colHoatChat.HeaderText = "Hoạt chất";
        this.colHoatChat.DataPropertyName = "Hoạt chất";
        this.colHoatChat.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        this.colHoatChat.Width = 200;
        this.colHoatChat.ReadOnly = true;

        this.dgvDuLieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dgvDuLieu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.previewCol0, this.previewCol1, this.previewCol2, this.previewCol3, this.previewCol4, this.previewCol5, this.previewCol6, this.previewCol7, this.previewCol8, this.previewCol9, this.colKeDon, this.colHoatChat });
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).EndInit();
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
        dgvDuLieu.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
        dgvDuLieu.MultiSelect=false;
        dgvDuLieu.AllowUserToResizeColumns=false;
        dgvDuLieu.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvDuLieu.DefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvDuLieu.ColumnHeadersDefaultCellStyle.SelectionBackColor=dgvDuLieu.ColumnHeadersDefaultCellStyle.BackColor;
        dgvDuLieu.ColumnHeadersDefaultCellStyle.SelectionForeColor=dgvDuLieu.ColumnHeadersDefaultCellStyle.ForeColor;
        dgvDuLieu.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvDuLieu.ColumnHeadersHeight=38;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
                txtLoi.Font=new Font("Segoe UI",10.5F,FontStyle.Regular);
        txtLoi.ForeColor=Color.FromArgb(15,23,42);
        txtLoi.BackColor=Color.FromArgb(241,245,249);
        txtLoi.BorderStyle=BorderStyle.FixedSingle;
                dgvDuLieu.ColumnHeadersDefaultCellStyle.WrapMode=DataGridViewTriState.False;
        dgvDuLieu.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvDuLieu.ColumnHeadersHeight=40;
        
        
        
        
        
        
        
        
        
        
        ResumeLayout(false);
    }
    private System.Windows.Forms.DataGridViewTextBoxColumn previewCol0,previewCol1,previewCol2,previewCol3,previewCol4,previewCol5,previewCol6,previewCol7,previewCol8,previewCol9;
    private System.Windows.Forms.DataGridViewTextBoxColumn colKeDon,colHoatChat;
    private TableLayoutPanel layout;
    private DataGridView dgvDuLieu;
    private TextBox txtLoi;
    private Label lblKetQua;
    private FlowLayoutPanel actions;
    private Button btnNhap,btnHuy;
}

}