#nullable enable
namespace Store.POS;
partial class frmHuyHoaDonCho
{
    private System.ComponentModel.IContainer? components;
    private TextBox txtLyDo=null!;
    private Label lblThongTin=null!,lblLoi=null!;
    private Button btnXacNhan=null!,btnBoQua=null!;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        components=new System.ComponentModel.Container();
        var layout=new TableLayoutPanel();
        var buttons=new FlowLayoutPanel();
        lblThongTin=new Label();txtLyDo=new TextBox();lblLoi=new Label();
        btnXacNhan=new Button();btnBoQua=new Button();
        SuspendLayout();
        layout.Dock=DockStyle.Fill;layout.Padding=new Padding(16);layout.ColumnCount=1;layout.RowCount=4;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent,100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,30));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute,46));
        lblThongTin.Name="lblThongTin";lblThongTin.Dock=DockStyle.Fill;lblThongTin.TextAlign=ContentAlignment.MiddleLeft;
        txtLyDo.Name="txtLyDo";txtLyDo.Dock=DockStyle.Fill;txtLyDo.Multiline=true;txtLyDo.MaxLength=250;
        txtLyDo.PlaceholderText="Lý do hủy (bắt buộc, tối đa 250 ký tự)";
        lblLoi.Name="lblLoi";lblLoi.Dock=DockStyle.Fill;lblLoi.ForeColor=Color.Firebrick;
        buttons.Dock=DockStyle.Fill;buttons.FlowDirection=FlowDirection.RightToLeft;buttons.WrapContents=false;
        btnXacNhan.Name="btnXacNhan";btnXacNhan.Text="Xác nhận hủy";btnXacNhan.Size=new Size(150,38);
        btnXacNhan.BackColor=Color.FromArgb(225,29,72);btnXacNhan.ForeColor=Color.White;btnXacNhan.FlatStyle=FlatStyle.Flat;
        btnBoQua.Name="btnBoQua";btnBoQua.Text="Quay lại";btnBoQua.Size=new Size(120,38);btnBoQua.DialogResult=DialogResult.Cancel;
        buttons.Controls.Add(btnXacNhan);buttons.Controls.Add(btnBoQua);
        layout.Controls.Add(lblThongTin,0,0);layout.Controls.Add(txtLyDo,0,1);layout.Controls.Add(lblLoi,0,2);layout.Controls.Add(buttons,0,3);
        Controls.Add(layout);Font=new Font("Segoe UI",10);BackColor=Color.FromArgb(248,250,252);
        ClientSize=new Size(520,280);MinimumSize=new Size(480,300);StartPosition=FormStartPosition.CenterParent;
        Text="Hủy hóa đơn chờ";MinimizeBox=false;MaximizeBox=false;ShowInTaskbar=false;
        AcceptButton=btnXacNhan;CancelButton=btnBoQua;ResumeLayout(false);
    }
}

