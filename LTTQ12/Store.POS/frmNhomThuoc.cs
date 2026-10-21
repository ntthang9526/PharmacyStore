
using System.Data;
using Store.BLL;



namespace Store.POS
{
    public partial class frmNhomThuoc : Form
    {
        private NhomThuocBLL bll = new NhomThuocBLL();
        public frmNhomThuoc()
        {
            InitializeComponent();
        }
        // Sự kiện khi Form load lên
        private void frmNhomThuoc_Load(object sender, EventArgs e)
        {
            LoadData();
            SetControlState(false);
        }
        // Hàm tải dữ liệu lên DataGridView
        private void LoadData()
        {
            DataTable dt = bll.GetAllNhomThuoc();

            dgvNhomThuoc.DataSource = dt;
            if (dgvNhomThuoc.Columns["MaNhom"] != null)
            {
                dgvNhomThuoc.Columns["MaNhom"].HeaderText = "Mã nhóm";
                dgvNhomThuoc.Columns["TenNhom"].HeaderText = "Tên nhóm thuốc";
            }
            // 2. Tự động giãn các cột để phủ kín chiều rộng DataGridView (không bị khoảng xám thừa)
            dgvNhomThuoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 3. Khi click vào dòng nào thì sáng nguyên cả dòng
            dgvNhomThuoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 4. Ẩn cột chọn thừa ở ngoài cùng bên trái (nếu không muốn thấy ô vuông trống)
            dgvNhomThuoc.RowHeadersVisible = false;
        }
        private void SetControlState(bool status)
        {
            txtMaNhom.Enabled = status;
            txtTenNhom.Enabled = status;
        }
        private void dgvNhomThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhomThuoc.Rows[e.RowIndex];
                txtMaNhom.Text = row.Cells["MaNhom"].Value.ToString();
                txtTenNhom.Text = row.Cells["TenNhom"].Value.ToString();
                txtMaNhom.Enabled = false;
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!txtTenNhom.Enabled)
            {
                SetControlState(true); // Mở khóa các ô
                txtMaNhom.Enabled = true; // Cho phép tự nhập mã nhóm
                txtMaNhom.Clear();
                txtTenNhom.Clear();
                txtMaNhom.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu xuống CSDL
            if (string.IsNullOrWhiteSpace(txtMaNhom.Text))
            {
                MessageBox.Show("Mã nhóm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhom.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenNhom.Text))
            {
                MessageBox.Show("Tên nhóm thuốc không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            // Gọi BLL để thêm mới
            if (bll.ThemMoiNhom(txtMaNhom.Text.Trim(), txtTenNhom.Text.Trim(), out string error))
            {
                MessageBox.Show("Thêm nhóm thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetControlState(false); // Thêm xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Lỗi (Có thể do trùng mã): " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhom = txtMaNhom.Text;
            if (string.IsNullOrEmpty(maNhom))
            {
                MessageBox.Show("Vui lòng chọn nhóm thuốc cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm thuốc này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (bll.XoaNhom(maNhom, out string error))
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
            txtMaNhom.Clear();
            txtTenNhom.Clear();
            txtMaNhom.Enabled = true;
            SetControlState(false);
            LoadData();
            txtMaNhom.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtTenNhom.Enabled)
            {
                if (string.IsNullOrEmpty(txtMaNhom.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhóm thuốc cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetControlState(true);  // Mở khóa các ô để sửa thông tin
                txtMaNhom.Enabled = false; // Khóa mã nhóm lại vì là khóa chính không được sửa
                txtTenNhom.Focus();
                return;
            }

            // Trường hợp 2: Khi đã sửa xong thông tin trên ô, bấm nút Sửa lần nữa để LƯU xuống CSDL
            string maNhom = txtMaNhom.Text.Trim();
            string tenNhom = txtTenNhom.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenNhom))
            {
                MessageBox.Show("Tên nhóm thuốc không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            if (bll.SuaNhom(maNhom, tenNhom, out string error))
            {
                MessageBox.Show("Cập nhật nhóm thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetControlState(false); // Lưu xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close(); // Đóng form quản lý nhóm thuốc
            }
        }
    }
}
