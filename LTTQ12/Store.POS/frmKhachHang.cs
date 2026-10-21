
using Store.BLL;
using System.Data;

namespace Store.POS
{
    public partial class frmKhachHang : Form
    {
        private KhachHangBLL bll = new KhachHangBLL();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboMaKH();
            SetControlState(false);
        }
        private void LoadData()
        {
            dgvKhachHang.DataSource = bll.GetAllKhachHang();

            // Đổi tiêu đề cột hiển thị tiếng Việt có dấu cho đẹp
            if (dgvKhachHang.Columns["MaKH"] != null)
            {
                dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
                dgvKhachHang.Columns["TenKH"].HeaderText = "Tên khách hàng";
                dgvKhachHang.Columns["DienThoai"].HeaderText = "Số điện thoại";
                dgvKhachHang.Columns["Diem"].HeaderText = "Điểm";
            }

            // Cấu hình giao diện DataGridView chuyên nghiệp
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.RowHeadersVisible = false;
        }

        private void LoadComboMaKH()
        {
            cboMaKH.DataSource = bll.GetAllKhachHang();
            cboMaKH.DisplayMember = "MaKH"; // Hiển thị mã khách hàng lên ô chọn
            cboMaKH.ValueMember = "MaKH";   // Giá trị thực tế khi chọn
            cboMaKH.SelectedIndex = -1;     // Để trống ban đầu cho đẹp, không chọn sẵn dòng nào
        }

        // Hàm quản lý trạng thái khóa/mở các ô nhập liệu
        private void SetControlState(bool status)
        {
            txtMaKH.Enabled = status;
            txtTenKH.Enabled = status;
            txtDienThoai.Enabled = status;
            txtDiem.Enabled = status;
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
                txtDiem.Text = row.Cells["Diem"].Value.ToString();
                txtMaKH.Enabled = false; // Khóa mã không cho sửa khi đang chọn dòng cập nhật
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Trường hợp 1: Nếu các ô đang bị khóa (Bấm lần đầu để chuẩn bị thêm mới)
            if (!txtTenKH.Enabled)
            {
                SetControlState(true); // Mở khóa tất cả các ô
                txtMaKH.Enabled = true; // Cho phép tự nhập mã KH
                txtMaKH.Clear();
                txtTenKH.Clear();
                txtDienThoai.Clear();
                txtDiem.Clear();
                txtMaKH.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu vào CSDL
            // 1. Kiểm tra mã khách hàng không để trống
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Mã khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            // 2. Kiểm tra tên khách hàng không để trống
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }

            // 3. Kiểm tra số điện thoại (Bắt buộc nhập và đúng định dạng)
            string sdt = txtDienThoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (!sdt.All(char.IsDigit) || sdt.Length < 9 || sdt.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải là dãy số từ 9 đến 11 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            // 4. Kiểm tra định dạng điểm (phải là số nguyên >= 0)
            if (!int.TryParse(txtDiem.Text.Trim(), out int diem) || diem < 0)
            {
                MessageBox.Show("Điểm phải là số nguyên lớn hơn hoặc bằng 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiem.Focus();
                return;
            }

            // 5. Gọi BLL để thêm mới
            if (bll.ThemMoiKhachHang(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(), sdt, txtDiem.Text.Trim(), out string error))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadComboMaKH();
                SetControlState(false); // Thêm thành công thì khóa các ô lại
            }
            else
            {
                MessageBox.Show("Lỗi (Có thể do trùng mã): " + error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            // Trường hợp 1: Nếu chưa mở khóa (Bấm lần đầu để mở khóa sửa)
            if (!txtTenKH.Enabled)
            {
                if (string.IsNullOrEmpty(txtMaKH.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetControlState(true);  // Mở khóa các ô để sửa thông tin
                txtMaKH.Enabled = false; // Khóa mã KH lại vì là khóa chính không được sửa
                txtTenKH.Focus();
                return;
            }

            // Trường hợp 2: Khi đã sửa xong thông tin trên ô, bấm nút Sửa lần nữa để LƯU xuống CSDL
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return;
            }

            string sdt = txtDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(sdt) && (!sdt.All(char.IsDigit) || sdt.Length < 9 || sdt.Length > 11))
            {
                MessageBox.Show("Số điện thoại phải là dãy số từ 9 đến 11 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (!int.TryParse(txtDiem.Text.Trim(), out int diem) || diem < 0)
            {
                MessageBox.Show("Điểm phải là số nguyên lớn hơn hoặc bằng 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiem.Focus();
                return;
            }

            // Gọi hàm sửa trong BLL
            if (bll.SuaKhachHang(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(), sdt, txtDiem.Text.Trim(), out string error))
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadComboMaKH();
                SetControlState(false); // Lưu xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Lỗi: " + error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (bll.XoaKhachHang(txtMaKH.Text, out string error))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamLai_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Lỗi: " + error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDienThoai.Clear();
            txtDiem.Clear();
            txtMaKH.Enabled = true;
            cboMaKH.SelectedIndex = -1; // Bỏ chọn ComboBox tìm kiếm
            SetControlState(false);
            LoadData();
            txtTenKH.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát khỏi form này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn mã chưa
            if (cboMaKH.SelectedValue == null || string.IsNullOrWhiteSpace(cboMaKH.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn mã khách hàng cần tìm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKH.Focus();
                return;
            }

            string maChon = cboMaKH.SelectedValue.ToString();

            // 2. Lấy toàn bộ dữ liệu và dùng DataView để lọc theo Mã KH
            DataTable dt = bll.GetAllKhachHang();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"MaKH = '{maChon}'"; // Lọc dòng có MaKH khớp với lựa chọn

            dgvKhachHang.DataSource = dv;

            // 3. Nếu tìm thấy, đổ dữ liệu lên các ô TextBox ở form để dễ nhìn/sửa
            if (dv.Count > 0)
            {
                txtMaKH.Text = dv[0]["MaKH"].ToString();
                txtTenKH.Text = dv[0]["TenKH"].ToString();
                txtDienThoai.Text = dv[0]["DienThoai"].ToString();
                txtDiem.Text = dv[0]["Diem"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
