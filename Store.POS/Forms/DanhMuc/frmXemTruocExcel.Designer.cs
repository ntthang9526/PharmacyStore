#nullable disable
namespace Store.POS;
partial class frmXemTruocExcel
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        layout=new TableLayoutPanel();dgvDuLieu=new DataGridView();txtLoi=new TextBox();lblKetQua=new Label();actions=new FlowLayoutPanel();btnNhap=new Button();btnHuy=new Button();
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).BeginInit();SuspendLayout();
        layout.Name="layout";layout.Dock=DockStyle.Fill;layout.Padding=new Padding(12);layout.ColumnCount=1;layout.RowCount=4;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,40));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,100));layout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));
        lblKetQua.Name="lblKetQua";lblKetQua.Dock=DockStyle.Fill;
        dgvDuLieu.Name="dgvDuLieu";dgvDuLieu.Dock=DockStyle.Fill;dgvDuLieu.ReadOnly=true;dgvDuLieu.AllowUserToAddRows=false;dgvDuLieu.AllowUserToDeleteRows=false;
        dgvDuLieu.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;dgvDuLieu.ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        txtLoi.Name="txtLoi";txtLoi.Dock=DockStyle.Fill;txtLoi.Multiline=true;txtLoi.ReadOnly=true;txtLoi.ScrollBars=ScrollBars.Vertical;
        actions.Dock=DockStyle.Fill;actions.FlowDirection=FlowDirection.RightToLeft;
        btnNhap.Name="btnNhap";btnNhap.Size=new Size(230,42);btnNhap.DialogResult=DialogResult.OK;
        btnHuy.Name="btnHuy";btnHuy.Text="Bỏ qua";btnHuy.Size=new Size(120,42);btnHuy.DialogResult=DialogResult.Cancel;
        actions.Controls.Add(btnHuy);actions.Controls.Add(btnNhap);layout.Controls.Add(lblKetQua,0,0);layout.Controls.Add(dgvDuLieu,0,1);layout.Controls.Add(txtLoi,0,2);layout.Controls.Add(actions,0,3);
        AutoScaleDimensions=new SizeF(8,20);AutoScaleMode=AutoScaleMode.Font;Font=new Font("Segoe UI",10.2F);ClientSize=new Size(1000,650);MinimumSize=new Size(800,550);
        Name="frmXemTruocExcel";Text="Kiểm tra dữ liệu Excel";StartPosition=FormStartPosition.CenterParent;AcceptButton=btnNhap;CancelButton=btnHuy;Controls.Add(layout);
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).EndInit();
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
ResumeLayout(false);
    }
    private TableLayoutPanel layout;
    private DataGridView dgvDuLieu;
    private TextBox txtLoi;
    private Label lblKetQua;
    private FlowLayoutPanel actions;
    private Button btnNhap,btnHuy;
}
