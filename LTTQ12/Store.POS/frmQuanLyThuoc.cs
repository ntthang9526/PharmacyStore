
using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmQuanLyThuoc : Form
    {
        private ThuocBLL bll = new ThuocBLL(); // Gọi tầng BLL để xử lý nghiệp vụ và validate
        private bool isThem = false;          // Biến cờ kiểm tra trạng thái: true là đang thêm mới, false là bình thường/sửa
        public frmQuanLyThuoc()
        {
            InitializeComponent();
        }


        private void frmQuanLyThuoc_Load(object sender, EventArgs e)
        {
            LoadDataToGrid();
            LoadComboMaThuoc();
            LoadComboNhomThuoc();
            LoadComboDonViTinh();
            SetControlState(false); // Ban đầu khóa các ô nhập liệu và nút Lưu/Bỏ qua
        }
        private void LoadDataToGrid()
        {
            dgvThuoc.DataSource = bll.GetAllThuoc();
        }
        // Hàm đổ dữ liệu mã thuốc vào cboMaThuoc trong khung Tìm kiếm
        private void LoadComboMaThuoc()
        {
            cboMaThuoc.DataSource = bll.GetAllThuoc();
            cboMaThuoc.DisplayMember = "MaThuoc"; // Sửa lại thành "MaThuoc" vì DAL đã định nghĩa AS MaThuoc
            cboMaThuoc.ValueMember = "MaThuoc";   // Sửa lại thành "MaThuoc"
            cboMaThuoc.SelectedIndex = -1;        // Để trống ban đầu cho đẹp
        }

        // Hàm nạp danh sách Nhóm thuốc vào cboNhomThuoc
        private void LoadComboNhomThuoc()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNhom");
            dt.Columns.Add("TenNhom");

            // Thêm tạm vài dòng dữ liệu nhóm thuốc khớp với CategoryId trong CSDL của bạn (ví dụ: 1, 2, 3...)
            dt.Rows.Add("1", "1");
            dt.Rows.Add("2", "2");
            dt.Rows.Add("3", "3");

            cboNhomThuoc.DataSource = dt;
            cboNhomThuoc.DisplayMember = "TenNhom";
            cboNhomThuoc.ValueMember = "MaNhom";
            cboNhomThuoc.SelectedIndex = -1;
        }

        // Hàm nạp danh sách Đơn vị tính vào cboDonViTinh (Dùng danh sách tĩnh đơn giản)
        private void LoadComboDonViTinh()
        {
            cboDonViTinh.Items.Clear();
            cboDonViTinh.Items.Add("Hộp");
            cboDonViTinh.Items.Add("Gói");
            cboDonViTinh.Items.Add("Tuýp");
            cboDonViTinh.Items.Add("Chai");
            cboDonViTinh.Items.Add("Viên");
            cboDonViTinh.SelectedIndex = -1;
        }
        // Hàm quản lý trạng thái bật/tắt của các control
        private void SetControlState(bool status)
        {
            txtMaThuoc.Enabled = status;
            txtTenThuoc.Enabled = status;
            cboNhomThuoc.Enabled = status;
            cboDonViTinh.Enabled = status;
            txtDonGia.Enabled = status;
            txtSoLuongTon.Enabled = status;

            btnLuu.Enabled = status;
            btnBoQua.Enabled = status;

            btnThem.Enabled = !status;
            btnSua.Enabled = !status;
            btnXoa.Enabled = !status;
        }




        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            SetControlState(true); // Mở khóa các ô nhập liệu

            // Xóa trắng các ô để người dùng nhập mới
            txtMaThuoc.Clear();
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
            txtMaThuoc.Focus();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuoc.Rows[e.RowIndex];
                txtMaThuoc.Text = row.Cells["MaThuoc"].Value.ToString();
                txtTenThuoc.Text = row.Cells["TenThuoc"].Value.ToString();
                cboNhomThuoc.Text = row.Cells["MaNhom"].Value.ToString();
                cboDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtSoLuongTon.Text = row.Cells["SoLuongTon"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaThuoc.Text))
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;       // Đánh dấu đây là thao tác SỬA
            SetControlState(true); // Mở khóa các ô nhập liệu
            txtMaThuoc.Enabled = false; // KHÔNG cho phép sửa mã thuốc (vì là khóa chính)
            txtTenThuoc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaThuoc.Text))
            {
                MessageBox.Show("Vui lòng chọn thuốc cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận trước khi xóa tránh bấm nhầm
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa thuốc này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string error;
                bool kq = bll.XoaThuoc(txtMaThuoc.Text.Trim(), out error);

                if (kq)
                {
                    MessageBox.Show("Xóa thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();
                    LoadComboMaThuoc();
                    // Xóa trắng các ô nhập
                    txtMaThuoc.Clear();
                    txtTenThuoc.Clear();
                    cboNhomThuoc.SelectedIndex = -1;
                    cboDonViTinh.SelectedIndex = -1;
                    txtDonGia.Clear();
                    txtSoLuongTon.Clear();
                }
                else
                {
                    MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra validate chung
            if (isThem && string.IsNullOrWhiteSpace(txtMaThuoc.Text))
            {
                MessageBox.Show("Mã thuốc không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaThuoc.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenThuoc.Text))
            {
                MessageBox.Show("Tên thuốc không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThuoc.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ và lớn hơn hoặc bằng 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng tồn phải là số nguyên lớn hơn hoặc bằng 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTon.Focus();
                return;
            }

            string error = "";
            bool kq = false;
            string maNhom = cboNhomThuoc.SelectedValue != null ? cboNhomThuoc.SelectedValue.ToString() : "1";
            string dvt = cboDonViTinh.Text.Trim();

            if (isThem)
            {
                kq = bll.ThemMoiThuoc(txtMaThuoc.Text.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), out error);
            }
            else
            {
                // Gọi hàm Sửa
                kq = bll.SuaThuoc(txtMaThuoc.Text.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), out error);
            }

            if (kq)
            {
                MessageBox.Show(isThem ? "Thêm thuốc thành công!" : "Cập nhật thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGrid();
                LoadComboMaThuoc();
                SetControlState(false);
            }
            else
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu (Có thể do trùng mã): " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            SetControlState(false);

            txtMaThuoc.Clear();
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Clear();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = cboMaThuoc.Text.Trim();

            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã thuốc cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi hàm tìm kiếm từ tầng BLL
            DataTable dtKQ = bll.TimKiemThuoc(tuKhoa);
            dgvThuoc.DataSource = dtKQ;

            // Nếu tìm thấy kết quả, đổ dữ liệu dòng đầu tiên lên các ô nhập phía trên
            if (dtKQ.Rows.Count > 0)
            {
                DataRow row = dtKQ.Rows[0];

                txtMaThuoc.Text = row["MaThuoc"].ToString();
                txtTenThuoc.Text = row["TenThuoc"].ToString();
                if (row["MaNhom"] != DBNull.Value)
                    cboNhomThuoc.SelectedValue = row["MaNhom"].ToString();
                cboDonViTinh.Text = row["DonViTinh"].ToString();
                txtDonGia.Text = row["DonGia"].ToString();
                txtSoLuongTon.Text = row["SoLuongTon"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã thuốc phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close(); 
            }
        }
    }
}
