namespace Store.POS
{
    public partial class frmMain : Form
    {
        /// <summary>true nếu người dùng chọn Đăng xuất (Program sẽ quay lại màn hình đăng nhập).</summary>
        public bool DangXuat { get; private set; } = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblNguoiDung.Text = "Nhân viên: " + PhienDangNhap.HoTen;
            lblNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MoForm<frmBanHang>(); // Vào là mở sẵn màn hình bán hàng
        }

        // Mở form con trong vùng MDI; nếu đã mở rồi thì chỉ đưa lên trên
        private void MoForm<T>() where T : Form, new()
        {
            foreach (Form f in MdiChildren)
            {
                if (f is T)
                {
                    f.Activate();
                    return;
                }
            }

            T frm = new T();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void mnuBanHang_Click(object sender, EventArgs e) => MoForm<frmBanHang>();
        private void mnuQuanLyThuoc_Click(object sender, EventArgs e) => MoForm<frmQuanLyThuoc>();
        private void mnuNhomThuoc_Click(object sender, EventArgs e) => MoForm<frmNhomThuoc>();
        private void mnuNhaCungCap_Click(object sender, EventArgs e) => MoForm<frmNhaCungCap>();
        private void mnuKhachHang_Click(object sender, EventArgs e) => MoForm<frmKhachHang>();

        private void mnuNhapKho_Click(object sender, EventArgs e) => MoForm<frmNhapKho>();
        private void mnuCanhBao_Click(object sender, EventArgs e) => MoForm<frmCanhBao>();
        private void mnuBaoCaoDoanhThu_Click(object sender, EventArgs e) => MoForm<frmBaoCao>();

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DangXuat = true;
                Close();
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đăng xuất đã xác nhận rồi nên không hỏi lại
            if (!DangXuat && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
