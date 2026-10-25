using Store.BLL;
using Store.Shared.DTOs.Auth;
namespace Store.POS;
public partial class frmDangNhap : Form
{
    private readonly AuthBLL authBLL = new();
    public frmDangNhap() { InitializeComponent(); }
    private async void btnDangNhap_Click(object sender, EventArgs e)
    {
        btnDangNhap.Enabled = false;
        try
        {
            var request = new LoginRequest { Username = txtBoxTaiKhoan.Text.Trim(), Password = txtBoxMatKhau.Text };
            var result = await Task.Run(() => authBLL.Login(request));
            if (!result.Success || !result.EmployeeID.HasValue)
            {
                MessageBox.Show(result.Message, "Đăng nhập");
                txtBoxMatKhau.SelectAll();
                txtBoxMatKhau.Focus();
                return;
            }
            PhienDangNhap.NhanVienId = result.EmployeeID.Value;
            PhienDangNhap.HoTen = result.FullName ?? "";
            PhienDangNhap.IsAdmin = result.IsAdmin;
            Hide();
            using var main = new frmMain();
            main.ShowDialog();
            Close();
        }
        catch (Exception ex)
        {
            Show();
            MessageBox.Show("Không đăng nhập được: " + ex.Message, "Lỗi");
        }
        finally { PhienDangNhap.Xoa(); btnDangNhap.Enabled = true; }
    }
}
