using Store.BLL;
using Store.Shared.DTOs.Auth;
namespace Store.POS;
public partial class frmDangNhap : Form
{
    private readonly AuthBLL authBLL = new();
    public frmDangNhap() { InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this); }
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
            PhienDangNhap.VaiTro = result.Role;
            PhienDangNhap.HoTen = result.FullName ?? "";
            Hide();
            using var main = new frmMain();
            main.ShowDialog();
            if (main.DaDangXuat)
            {
                PhienDangNhap.Xoa();
                txtBoxMatKhau.Clear();
                chkHienMatKhau.Checked = false;
                txtBoxTaiKhoan.Focus();
                Show();
                return;
            }
            Close();
        }
        catch (Exception ex)
        {
            Show();
            MessageBox.Show("Không đăng nhập được: " + ex.Message, "Lỗi");
        }
        finally { PhienDangNhap.Xoa(); btnDangNhap.Enabled = true; }
    }

    private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
    {
        txtBoxMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
    }
}
