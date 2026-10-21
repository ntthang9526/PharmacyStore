
using Store.BLL;
using System.Data;

namespace Store.POS
{
    public partial class frmNhaCungCap : Form
    {
        private NhaCungCapBLL bll = new NhaCungCapBLL();
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboMaNCC();
            SetControlState(false);
        }
        private void LoadData()
        {
            dgvNhaCungCap.DataSource = bll.GetAllNhaCungCap();

            if (dgvNhaCungCap.Columns["MaNCC"] != null)
            {
                dgvNhaCungCap.Columns["MaNCC"].HeaderText = "Mã NCC";
                dgvNhaCungCap.Columns["TenNCC"].HeaderText = "Tên nhà cung cấp";
                dgvNhaCungCap.Columns["DienThoai"].HeaderText = "Điện thoại";
                dgvNhaCungCap.Columns["DiaChi"].HeaderText = "Địa chỉ";
            }

            dgvNhaCungCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhaCungCap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhaCungCap.RowHeadersVisible = false;
        }

        private void LoadComboMaNCC()
        {
            cboMaNCC.DataSource = bll.GetAllNhaCungCap();
            cboMaNCC.DisplayMember = "MaNCC"; // Hiển thị mã nhà cung cấp
            cboMaNCC.ValueMember = "MaNCC";   // Giá trị thực tế khi chọn
            cboMaNCC.SelectedIndex = -1;     // Để trống ban đầu
        }

        private void SetControlState(bool status)
        {
            txtMaNCC.Enabled = status;
            txtTenNCC.Enabled = status;
            txtDienThoai.Enabled = status;
            txtDiaChi.Enabled = status;
        }
        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                txtMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
                txtTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtMaNCC.Enabled = false;
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!txtTenNCC.Enabled)
            {
                SetControlState(true); // Mở khóa tất cả các ô
                txtMaNCC.Enabled = true; // Cho phép tự nhập mã NCC
                txtMaNCC.Clear();
                txtTenNCC.Clear();
                txtDienThoai.Clear();
                txtDiaChi.Clear();
                txtMaNCC.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu xuống CSDL
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCC.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            string dienThoai = txtDienThoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(dienThoai))
            {
                MessageBox.Show("Số điện thoại không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (!dienThoai.All(char.IsDigit) || dienThoai.Length < 9 || dienThoai.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải là dãy số từ 9 đến 11 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            string diaChi = txtDiaChi.Text.Trim();
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                MessageBox.Show("Địa chỉ không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            // Gọi BLL để thêm mới
            if (bll.ThemMoiNCC(txtMaNCC.Text.Trim(), txtTenNCC.Text.Trim(), dienThoai, diaChi, out string error))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadComboMaNCC();
                SetControlState(false); // Thêm xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Lỗi (Có thể do trùng mã): " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtTenNCC.Enabled)
            {
                if (string.IsNullOrEmpty(txtMaNCC.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetControlState(true);  // Mở khóa các ô để sửa thông tin
                txtMaNCC.Enabled = false; // Khóa mã NCC lại vì là khóa chính không được sửa
                txtTenNCC.Focus();
                return;
            }

            // Trường hợp 2: Khi đã sửa xong thông tin trên ô, bấm nút Sửa lần nữa để LƯU xuống CSDL
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            string dienThoai = txtDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(dienThoai) && (!dienThoai.All(char.IsDigit) || dienThoai.Length < 9 || dienThoai.Length > 11))
            {
                MessageBox.Show("Số điện thoại phải là dãy số từ 9 đến 11 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            // Gọi hàm sửa trong BLL
            if (bll.SuaNCC(txtMaNCC.Text.Trim(), txtTenNCC.Text.Trim(), dienThoai, txtDiaChi.Text.Trim(), out string error))
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LoadComboMaNCC();
                SetControlState(false); // Lưu xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Lỗi: " + error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (bll.XoaNCC(txtMaNCC.Text, out string error))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Lỗi: " + error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtMaNCC.Enabled = true;
            cboMaNCC.SelectedIndex = -1; // Bỏ chọn ComboBox tìm kiếm
            SetControlState(false);
            LoadData();
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
            // 1. Kiểm tra xem đã chọn mã trên ComboBox chưa
            if (cboMaNCC.SelectedValue == null || string.IsNullOrWhiteSpace(cboMaNCC.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn mã nhà cung cấp cần tìm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }

            string maChon = cboMaNCC.SelectedValue.ToString();

            // 2. Lọc dữ liệu bằng DataView
            DataTable dt = bll.GetAllNhaCungCap();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"MaNCC = '{maChon}'";

            dgvNhaCungCap.DataSource = dv;

            // 3. Đổ dữ liệu lên các ô TextBox phía trên nếu tìm thấy
            if (dv.Count > 0)
            {
                txtMaNCC.Text = dv[0]["MaNCC"].ToString();
                txtTenNCC.Text = dv[0]["TenNCC"].ToString();
                txtDienThoai.Text = dv[0]["DienThoai"].ToString();
                txtDiaChi.Text = dv[0]["DiaChi"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhà cung cấp này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
