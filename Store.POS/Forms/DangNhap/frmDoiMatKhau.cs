using Store.BLL;
namespace Store.POS;
public partial class frmDoiMatKhau : Form
{
    public frmDoiMatKhau()
    {
        InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
        btnLuu.Click+=(_,_)=>Luu();btnHuy.Click+=(_,_)=>Xoa();
        Load+=(_,_)=> {if(!PosFormStyle.IsInDesigner(this)) lblTaiKhoan.Text="Tài khoản: "+PhienDangNhap.HoTen;};
        VisibleChanged+=(_,_)=> {if(!Visible) Xoa();};
    }
    private void Xoa() {txtCu.Clear();txtMoi.Clear();txtXacNhan.Clear();lblTrangThai.Text="";}
    private void Luu()
    {
        if(!new MatKhauBLL().Doi(txtCu.Text,txtMoi.Text,txtXacNhan.Text,out var error))
        {lblTrangThai.ForeColor=Color.Firebrick;lblTrangThai.Text=error;return;}
        Xoa();lblTrangThai.ForeColor=Color.DarkGreen;lblTrangThai.Text="Đã đổi mật khẩu. Dùng mật khẩu mới ở lần đăng nhập tiếp theo.";
    }
}
