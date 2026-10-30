using Store.BLL;
using System.Data;

namespace Store.POS
{
    public partial class frmNhaCungCap : Form, ILamMoiDuLieu
    {
        private string maDangChon="";
        private NhaCungCapBLL bll = new NhaCungCapBLL();
        private bool isThem = false;
        private bool isSua = false;
        public frmNhaCungCap()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PhanQuyenGiaoDien.Gan(this);
            txtTimKiem.MaxLength = 200;
            txtTimKiem.PlaceholderText = "Tên, điện thoại hoặc địa chỉ";
            txtTimKiem.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTimKiem_Click(this, EventArgs.Empty); }
            };
            PosFormStyle.ConfigureRowNavigation(dgvNhaCungCap,row=>dgvNhaCungCap_CellClick(dgvNhaCungCap,new DataGridViewCellEventArgs(0,row.Index)));
            PosFormStyle.ConfigureGridSelection(this, posLayout, () => ResetDetails(false));
        }

        private void ResetDetails(bool force = false)
        {
            if (!force && txtTenNCC.Enabled) return;
            SetControlState(false);
            maDangChon=""; txtTenNCC.Clear(); txtDienThoai.Clear(); txtDiaChi.Clear();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            LoadData();

            SetControlState(false);
            ResetDetails();
        }
        private void LoadData()
        {
            dgvNhaCungCap.DataSource = bll.GetAllNhaCungCap();
        }

        private void SetControlState(bool status)
        {
            dgvNhaCungCap.Enabled=!status;
            txtTenNCC.Enabled = status;
            txtDienThoai.Enabled = status;
            txtDiaChi.Enabled = status;

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
        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!dgvNhaCungCap.Enabled) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
                maDangChon = row.Cells["MaNCC"].Value.ToString()??"";
                txtTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!txtTenNCC.Enabled)
            {
                isThem = true;
                isSua = false;
                SetControlState(true); // Mở khóa tất cả các ô, tắt sửa và xóa
                maDangChon="";
                txtTenNCC.Clear();
                txtDienThoai.Clear();
                txtDiaChi.Clear();
                txtTenNCC.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu xuống CSDL

            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            string dienThoai = txtDienThoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(dienThoai))
            {
                MessageBox.Show("Nhập số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            if (!dienThoai.All(char.IsDigit) || dienThoai.Length < 9 || dienThoai.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải có 9–11 chữ số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            string diaChi = txtDiaChi.Text.Trim();
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                MessageBox.Show("Nhập địa chỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            // Gọi BLL để thêm mới
            if (bll.ThemMoiNCC(maDangChon.Trim(), txtTenNCC.Text.Trim(), dienThoai, diaChi, out string error))
            {
                MessageBox.Show("Đã thêm nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();

                SetControlState(false); // Thêm xong thì khóa lại
            }
            else
            {
                MessageBox.Show("Không lưu được: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtTenNCC.Enabled)
            {
                if (string.IsNullOrEmpty(maDangChon))
                {
                    MessageBox.Show("Chọn nhà cung cấp cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                isThem = false;
                isSua = true;
                SetControlState(true);  // Mở khóa các ô để sửa thông tin, tắt thêm và xóa
                txtTenNCC.Focus();
                return;
            }

            // Trường hợp 2: Khi đã sửa xong thông tin trên ô, bấm nút Sửa lần nữa để LƯU xuống CSDL
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            string dienThoai = txtDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(dienThoai) && (!dienThoai.All(char.IsDigit) || dienThoai.Length < 9 || dienThoai.Length > 11))
            {
                MessageBox.Show("Số điện thoại phải có 9–11 chữ số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            // Gọi hàm sửa trong BLL
            if (bll.SuaNCC(maDangChon.Trim(), txtTenNCC.Text.Trim(), dienThoai, txtDiaChi.Text.Trim(), out string error))
            {
                MessageBox.Show("Đã lưu thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DialogResult dr = MessageBox.Show("Xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (bll.XoaNCC(maDangChon, out string error))
                {
                    MessageBox.Show("Đã xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            maDangChon="";
            txtTenNCC.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtTimKiem.Clear();
            SetControlState(false);
            LoadData();
        }



        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDetails();
                dgvNhaCungCap.DataSource = bll.TimKiemNCC(txtTimKiem.Text.Trim());
            }
            catch (Exception ex) { MessageBox.Show("Không tìm được dữ liệu: " + ex.Message, "Lỗi"); }
        }
    }
}
