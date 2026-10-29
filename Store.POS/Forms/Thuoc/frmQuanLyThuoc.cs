using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmQuanLyThuoc : Form, ILamMoiDuLieu
    {
        private string maDangChon="";
        private ThuocBLL bll = new ThuocBLL(); // Gọi tầng BLL để xử lý nghiệp vụ và validate

        private bool isThem = false;          // Biến cờ kiểm tra trạng thái: true là đang thêm mới, false là bình thường/sửa
        public frmQuanLyThuoc()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            btnNhapExcel.Click+=(_,_)=>NhapExcel();btnXuatExcel.Click+=(_,_)=>XuatExcel();
            btnMauExcel.Click+=(_,_)=> {try {Store.POS.Common.ExcelHelper.XuatFileMauNhapThuoc();}catch(Exception ex){MessageBox.Show(ex.Message);}};
            PhanQuyenGiaoDien.Gan(this);
            var quickAddHints = new ToolTip(components ??= new System.ComponentModel.Container());
            quickAddHints.SetToolTip(btnThemNhom, "Thêm nhóm thuốc mới");
            quickAddHints.SetToolTip(btnThemNCC, "Thêm nhà cung cấp mới");
            btnThemNhom.Click += (_, _) => ThemDanhMuc(false);
            btnThemNCC.Click += (_, _) => ThemDanhMuc(true);
            txtTimKiem.MaxLength = 200;
            txtTimKiem.PlaceholderText = "Tên thuốc, nhóm, NCC hoặc mã vạch";
            txtTimKiem.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTimKiem_Click(this, EventArgs.Empty); }
            };
            PosFormStyle.ConfigureGridSelection(this, posLayout, () => ResetDetails(false));
        }

        private void ThemDanhMuc(bool supplier)
        {
            if (supplier)
            {
                using var dialog = new frmThemDanhMuc();
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                cboNhaCungCap.DataSource = new NhaCungCapBLL().GetAllNhaCungCap();
                cboNhaCungCap.SelectedValue = dialog.CreatedId;
            }
            else
            {
                using var dialog = new frmThemNhomThuoc();
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                cboNhomThuoc.DataSource = new NhomThuocBLL().GetAllNhomThuoc();
                cboNhomThuoc.SelectedValue = dialog.CreatedId;
            }
        }

        private void ResetDetails(bool force = false)
        {
            if (!force && btnLuu.Enabled) return;
            isThem = false;
            SetControlState(false);
            maDangChon=""; txtTenThuoc.Clear(); txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;
            cboNhomThuoc.SelectedIndex = -1;
            cboNhaCungCap.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            cboDonViTinh.Text = "";
        }

        private void frmQuanLyThuoc_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            LoadDataToGrid();

            LoadComboNhomThuoc();
            cboNhaCungCap.DataSource = new NhaCungCapBLL().GetAllNhaCungCap();
            cboNhaCungCap.DisplayMember = "TenNCC";
            cboNhaCungCap.ValueMember = "MaNCC";
            LoadComboDonViTinh();
            SetControlState(false); // Ban đầu khóa các ô nhập liệu và nút Lưu/Bỏ qua
            ResetDetails(true);
        }
        private void LoadDataToGrid()
        {
            GanDuLieuThuoc(bll.GetAllThuoc());
        }
        private void GanDuLieuThuoc(DataTable data)
        {
            dgvThuoc.DataSource = data;
        }

        // Hàm nạp danh sách Nhóm thuốc vào cboNhomThuoc
        private void LoadComboNhomThuoc()
        {
            cboNhomThuoc.DataSource = new NhomThuocBLL().GetAllNhomThuoc();
            cboNhomThuoc.DisplayMember = "TenNhom";
            cboNhomThuoc.ValueMember = "MaNhom";
            cboNhomThuoc.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cboTrangThaiBan.Enabled = status;
            txtTenThuoc.Enabled = status;
            cboNhomThuoc.Enabled = status;
            cboDonViTinh.Enabled = status;
            txtDonGia.Enabled = status;
            txtSoLuongTon.Enabled = false;
            cboNhaCungCap.Enabled = status;
            btnThemNhom.Enabled = status;
            btnThemNCC.Enabled = status;

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
            maDangChon="";
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;
            txtTenThuoc.Focus();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvThuoc.Rows.Count ||
                dgvThuoc.Rows[e.RowIndex].DataBoundItem is not DataRowView data) return;
            maDangChon = Convert.ToString(data["MaThuoc"]) ?? "";
            txtTenThuoc.Text = Convert.ToString(data["TenThuoc"]);
            cboNhomThuoc.SelectedValue = data["MaNhom"];
            cboNhaCungCap.SelectedValue = data["MaNCC"];
            cboDonViTinh.Text = Convert.ToString(data["DonViTinh"]);
            txtDonGia.Text = Store.Shared.TienTe.LamTron(Convert.ToDecimal(data["DonGia"])).ToString("0");
            txtSoLuongTon.Text = Convert.ToString(data["SoLuongTon"]);
            cboTrangThaiBan.SelectedIndex = Convert.ToBoolean(data["DangBan"])?0:1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;       // Đánh dấu đây là thao tác SỬA
            SetControlState(true); // Mở khóa các ô nhập liệu
            txtTenThuoc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show("Vui lòng chọn thuốc cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận trước khi xóa tránh bấm nhầm
            DialogResult dr = MessageBox.Show("Ẩn thuốc này khỏi danh mục? Nếu chỉ tạm ngừng bán, hãy chọn Sửa → Trạng thái bán → Ngừng bán. Lịch sử và tồn kho vẫn được giữ.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string error;
                bool kq = bll.XoaThuoc(maDangChon.Trim(), out error);

                if (kq)
                {
                    MessageBox.Show("Đã ẩn thuốc khỏi danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();

                    // Xóa trắng các ô nhập
                    maDangChon="";
                    txtTenThuoc.Clear();
                    cboNhomThuoc.SelectedIndex = -1;
                    cboDonViTinh.SelectedIndex = -1;
                    txtDonGia.Clear();
                    txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;
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

            if (cboNhomThuoc.SelectedValue == null || cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhóm thuốc và nhà cung cấp!");
                return;
            }
            txtDonGia.Text = Store.Shared.TienTe.LamTron(donGia).ToString("0");
            string error = "";
            bool kq = false;
            string maNhom = cboNhomThuoc.SelectedValue?.ToString() ?? "";
            string dvt = cboDonViTinh.Text.Trim();

            if (isThem)
            {
                kq = bll.ThemMoiThuoc(maDangChon.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error, cboTrangThaiBan.SelectedIndex==0);
            }
            else
            {
                // Gọi hàm Sửa
                kq = bll.SuaThuoc(maDangChon.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error, cboTrangThaiBan.SelectedIndex==0);
            }

            if (kq)
            {
                MessageBox.Show(isThem ? "Thêm thuốc thành công!" : "Cập nhật thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGrid();

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

            maDangChon="";
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDetails(true);
                    GanDuLieuThuoc(bll.TimKiemThuoc(txtTimKiem.Text.Trim()));
            }
            catch (Exception ex) { MessageBox.Show("Không tìm được dữ liệu: " + ex.Message, "Lỗi"); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            maDangChon = "";
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            cboNhaCungCap.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;
            SetControlState(false);
            LamMoiDuLieu();
            dgvThuoc.ClearSelection();
        }
    }
}
