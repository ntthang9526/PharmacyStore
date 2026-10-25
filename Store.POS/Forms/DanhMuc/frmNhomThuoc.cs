using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmNhomThuoc : Form, ILamMoiDuLieu
    {
        private string maDangChon="";
        private NhomThuocBLL bll = new NhomThuocBLL();

        public frmNhomThuoc()
        {
            InitializeComponent();
            PhanQuyenGiaoDien.Gan(this);
            btnTimNhom.Click += (_, _) => TimNhom();
            txtTimNhom.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TimNhom(); }
            };
            PosFormStyle.ConfigureGridSelection(this, posLayout, ResetDetails);
        }
        // Sự kiện khi Form load lên
        private void TimNhom()
        {
            try { ResetDetails(); dgvNhomThuoc.DataSource = bll.TimKiemNhom(txtTimNhom.Text.Trim()); }
            catch (Exception ex) { MessageBox.Show("Không tìm được nhóm thuốc: " + ex.Message, "Lỗi"); }
        }

        private void ResetDetails()
        {
            SetControlState(false);
            maDangChon=""; txtTenNhom.Clear();
        }

        private void frmNhomThuoc_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            LoadData();
            SetControlState(false);
            ResetDetails();
        }
        // Hàm tải dữ liệu lên DataGridView
        private void LoadData()
        {
            dgvNhomThuoc.DataSource = bll.GetAllNhomThuoc();
        }
        private void SetControlState(bool status)
        {
            txtTenNhom.Enabled = status;
        }
        private void dgvNhomThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhomThuoc.Rows[e.RowIndex];
                maDangChon = row.Cells["MaNhom"].Value.ToString()??"";
                txtTenNhom.Text = row.Cells["TenNhom"].Value.ToString();
                SetControlState(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!txtTenNhom.Enabled)
            {
                SetControlState(true); // Mở khóa các ô
                maDangChon="";
                txtTenNhom.Clear();
                txtTenNhom.Focus();
                return;
            }

            // Trường hợp 2: Khi người dùng đã nhập xong và bấm Thêm lần nữa để lưu xuống CSDL

            if (string.IsNullOrWhiteSpace(txtTenNhom.Text))
            {
                MessageBox.Show("Tên nhóm thuốc không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhom.Focus();
                return;
            }

            // Gọi BLL để thêm mới
            if (bll.ThemMoiNhom(maDangChon.Trim(), txtTenNhom.Text.Trim(), out string error))
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
            string maNhom = maDangChon;
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
            txtTimNhom.Clear();
            maDangChon="";
            txtTenNhom.Clear();
            SetControlState(false);
            LoadData();
            txtTenNhom.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!txtTenNhom.Enabled)
            {
                if (string.IsNullOrEmpty(maDangChon))
                {
                    MessageBox.Show("Vui lòng chọn nhóm thuốc cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetControlState(true);  // Mở khóa các ô để sửa thông tin
                txtTenNhom.Focus();
                return;
            }

            // Trường hợp 2: Khi đã sửa xong thông tin trên ô, bấm nút Sửa lần nữa để LƯU xuống CSDL
            string maNhom = maDangChon.Trim();
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
