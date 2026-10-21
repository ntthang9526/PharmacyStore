using Store.BLL;

namespace Store.POS
{
    public partial class frmLogin : Form
    {
        private NhanVienBLL bll = new NhanVienBLL();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text.Trim();
            string password = txtMatKhau.Text;

            if (bll.DangNhap(username, password, out int maNV, out string hoTen, out string error))
            {
                PhienDangNhap.NhanVienId = maNV;
                PhienDangNhap.HoTen = hoTen;
                DialogResult = DialogResult.OK; // Tự đóng form đăng nhập
            }
            else
            {
                MessageBox.Show(error, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(username))
                {
                    txtTaiKhoan.Focus();
                }
                else
                {
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }
    }
}
