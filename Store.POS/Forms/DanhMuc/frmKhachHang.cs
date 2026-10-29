using Store.BLL;
using System.Data;

namespace Store.POS
{
    public partial class frmKhachHang : Form, ILamMoiDuLieu
    {
        private string maDangChon="";
        private KhachHangBLL bll = new KhachHangBLL();
        private bool isThem = false;
        private bool isSua = false;
        public frmKhachHang() : this(null) {}
        public frmKhachHang(string? sdt)
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PhanQuyenGiaoDien.Gan(this);
            txtTimKiem.MaxLength = 200;
            txtTimKiem.PlaceholderText = "Tên hoặc số điện thoại";
            txtTimKiem.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTimKiem_Click(this, EventArgs.Empty); }
            };
            PosFormStyle.ConfigureGridSelection(this, posLayout, () => ResetDetails(false));
            if (!string.IsNullOrEmpty(sdt)) txtDienThoai.Text = sdt;
        }

        private void ResetDetails(bool force = false)
        {
            if (!force && txtTenKH.Enabled) return;
            SetControlState(false);
            maDangChon=""; txtTenKH.Clear(); txtDienThoai.Clear(); txtDiem.Text = "0";
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            LoadData();

            SetControlState(false);
            ResetDetails();
        }
        private void LoadData()
        {
            dgvKhachHang.DataSource = bll.GetAllKhachHang();
        }

        // Hàm quản lý trạng thái khóa/mở các ô nhập liệu
        private void SetControlState(bool status)
        {
            txtTenKH.Enabled = status;
            txtDienThoai.Enabled = status;
            txtDiem.Enabled = status;

            if (!status)
            {
                isThem = false;
                isSua = false;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnThem.Enabled = isThem;
                btnSua.Enabled = isSua;
                btnXoa.Enabled = false;
            }
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                maDangChon = row.Cells["MaKH"].Value.ToString()??"";
                txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
                txtDiem.Text = row.Cells["Diem"].Value.ToString();
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Trường hợp 1: Nếu các ô đang bị khóa (Bấm lần đầu để chuẩn bị thêm mới)
            if (!txtTenKH.Enabled)
            {
                isThem = true;
                isSua = false;
                SetControlState(true); // Mở khóa tất cả các ô, tắt sửa và xóa
                maDangChon="";
                txtTenKH.Clear();
                txtDienThoai.Clear();
                txtDiem.Text = "0";
                txtTenKH.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu vào CSDL
            // 1. Kiểm tra mã khách hàng không để trống

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

            if (DanhMucValidation.CustomerPhoneError(sdt) is string phoneError)
            {
                MessageBox.Show(phoneError, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (bll.ThemMoiKhachHang(maDangChon.Trim(), txtTenKH.Text.Trim(), sdt, txtDiem.Text.Trim(), out string error))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();

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
                if (string.IsNullOrEmpty(maDangChon))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                isThem = false;
                isSua = true;
                SetControlState(true);  // Mở khóa các ô để sửa thông tin, tắt thêm và xóa
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
            if (DanhMucValidation.CustomerPhoneError(sdt) is string phoneError)
            {
                MessageBox.Show(phoneError, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (bll.SuaKhachHang(maDangChon.Trim(), txtTenKH.Text.Trim(), sdt, txtDiem.Text.Trim(), out string error))
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();

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
                if (bll.XoaKhachHang(maDangChon, out string error))
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            maDangChon="";
            txtTenKH.Clear();
            txtDienThoai.Clear();
            txtDiem.Text = "0";
            txtTimKiem.Clear();
            SetControlState(false);
            LoadData();
            txtTenKH.Focus();
        }

        private void btnLamLai_Click(object sender, EventArgs e) => btnLamMoi_Click(sender, e);

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDetails();
                dgvKhachHang.DataSource = bll.TimKiemKhachHang(txtTimKiem.Text.Trim());
            }
            catch (Exception ex) { MessageBox.Show("Không tìm được dữ liệu: " + ex.Message, "Lỗi"); }
        }
    }
}
