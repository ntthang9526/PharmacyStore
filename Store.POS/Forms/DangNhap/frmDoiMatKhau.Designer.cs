#nullable disable
namespace Store.POS;
partial class frmDoiMatKhau
{
    private System.ComponentModel.IContainer components=null;
    protected override void Dispose(bool disposing) {if(disposing) components?.Dispose();base.Dispose(disposing);}
    private void InitializeComponent()
    {
        posLayout=new TableLayoutPanel();posHeading=new Label();editor=new TableLayoutPanel();
        lblTaiKhoan=new Label();lblCu=new Label();lblMoi=new Label();lblXacNhan=new Label();lblTrangThai=new Label();
        txtCu=new TextBox();txtMoi=new TextBox();txtXacNhan=new TextBox();actions=new FlowLayoutPanel();btnLuu=new Button();btnHuy=new Button();
        SuspendLayout();
        posLayout.Name="posLayout";posLayout.Dock=DockStyle.Fill;posLayout.Padding=new Padding(18);posLayout.ColumnCount=1;posLayout.RowCount=3;
        posLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,52));posLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100));posLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,60));
        posHeading.Name="posHeading";posHeading.Text="Đổi mật khẩu";posHeading.Dock=DockStyle.Fill;posHeading.Font=new Font("Segoe UI",18,FontStyle.Bold);
        editor.Name="editor";editor.Dock=DockStyle.Fill;editor.MinimumSize=new Size(450,300);editor.MaximumSize=new Size(600,380);editor.ColumnCount=1;editor.RowCount=8;
        editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,42));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,26));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,42));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,26));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,42));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,26));editor.RowStyles.Add(new RowStyle(SizeType.Absolute,42));
        editor.RowStyles.Add(new RowStyle(SizeType.Absolute,54));
        lblTaiKhoan.Name="lblTaiKhoan";lblTaiKhoan.Text="Tài khoản đang đăng nhập";lblTaiKhoan.Dock=DockStyle.Fill;
        lblCu.Text="Mật khẩu hiện tại";lblCu.Dock=DockStyle.Fill;
        lblMoi.Text="Mật khẩu mới (6–128 ký tự)";lblMoi.Dock=DockStyle.Fill;
        lblXacNhan.Text="Nhập lại mật khẩu mới";lblXacNhan.Dock=DockStyle.Fill;
        txtCu.Name="txtCu";txtCu.Dock=DockStyle.Top;txtCu.MaxLength=128;txtCu.UseSystemPasswordChar=true;
        txtMoi.Name="txtMoi";txtMoi.Dock=DockStyle.Top;txtMoi.MaxLength=128;txtMoi.UseSystemPasswordChar=true;
        txtXacNhan.Name="txtXacNhan";txtXacNhan.Dock=DockStyle.Top;txtXacNhan.MaxLength=128;txtXacNhan.UseSystemPasswordChar=true;
        actions.Dock=DockStyle.Fill;btnLuu.Name="btnLuu";btnLuu.Text="Đổi mật khẩu";btnLuu.Size=new Size(150,40);
        btnHuy.Name="btnHuy";btnHuy.Text="Xóa nội dung";btnHuy.Size=new Size(150,40);actions.Controls.Add(btnLuu);actions.Controls.Add(btnHuy);
        editor.Controls.Add(lblTaiKhoan,0,0);editor.Controls.Add(lblCu,0,1);editor.Controls.Add(txtCu,0,2);
        editor.Controls.Add(lblMoi,0,3);editor.Controls.Add(txtMoi,0,4);editor.Controls.Add(lblXacNhan,0,5);editor.Controls.Add(txtXacNhan,0,6);editor.Controls.Add(actions,0,7);
        lblTrangThai.Name="lblTrangThai";lblTrangThai.Dock=DockStyle.Fill;
        posLayout.Controls.Add(posHeading,0,0);posLayout.Controls.Add(editor,0,1);posLayout.Controls.Add(lblTrangThai,0,2);
        AutoScaleDimensions=new SizeF(8,20);AutoScaleMode=AutoScaleMode.Font;Font=new Font("Segoe UI",10.2F);
        ClientSize=new Size(1182,653);MinimumSize=new Size(900,570);Name="frmDoiMatKhau";Text="Đổi mật khẩu";Controls.Add(posLayout);
        BackColor=Color.FromArgb(248,250,252);ForeColor=Color.FromArgb(15,23,42);
ResumeLayout(false);
    }
    private TableLayoutPanel posLayout,editor;
    private Label posHeading,lblTaiKhoan,lblCu,lblMoi,lblXacNhan,lblTrangThai;
    private TextBox txtCu,txtMoi,txtXacNhan;
    private FlowLayoutPanel actions;
    private Button btnLuu,btnHuy;
}
