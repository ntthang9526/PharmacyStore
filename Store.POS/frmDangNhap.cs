using System.Data.Common;
using Store.Shared.DTOs.Auth;
using Store.BLL;
namespace Store.POS
{
    public partial class frmDangNhap : Form
    {
        private readonly AuthBLL authBLL = new();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private async void btnDangNhap_Click(object sender, EventArgs e)
        {
            //string username = txtBoxTaiKhoan.Text.Trim();
            //string password = txtBoxMatKhau.Text;
            //if (string.IsNullOrEmpty(username))
            //{
            //    MessageBox.Show("Tài khoản không được để trống!", "Lỗi đăng nhập");
            //    txtBoxTaiKhoan.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi đăng nhập");
            //    txtBoxMatKhau.Focus();
            //    return;
            //}

            btnDangNhap.Enabled = false;
            try
            {
                //var request = new LoginRequest
                //{
                //    Username = username,
                //    Password = password
                //};
                //LoginResponse result = await Task.Run(() => authBLL.Login(request));
                //if (!result.Success)
                //{
                //    MessageBox.Show(result.Message);
                //    txtBoxMatKhau.SelectAll();
                //    txtBoxMatKhau.Focus();
                //    return;
                //}
                //MessageBox.Show(result.Message);
                this.Hide();
                ShowFormMain();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                MessageBox.Show("Lỗi ứng dụng","Lỗi");
            }
            finally
            {
                btnDangNhap.Enabled = true;
            }
        }
        private void ShowFormMain()
        {
            using var frmMain = new frmMain();
            frmMain.ShowDialog();
        }
    }
}
