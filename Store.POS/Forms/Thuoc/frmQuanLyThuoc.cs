using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmQuanLyThuoc : Form, ILamMoiDuLieu
    {
        private string maDangChon="";
        private ThuocBLL bll = new ThuocBLL(); // Gọi tầng BLL để xử lý nghiệp vụ và validate

        private bool khoaNhaCungCap;
        private bool isThem = false;          // Biến cờ kiểm tra trạng thái: true là đang thêm mới, false là bình thường/sửa
        public frmQuanLyThuoc()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            btnNhapExcel.Click+=(_,_)=>NhapExcel();btnXuatExcel.Click+=(_,_)=>XuatExcel();
            btnMauExcel.Click+=(_,_)=> {try {Store.POS.Common.ExcelHelper.XuatFileMauNhapThuoc();}catch(Exception ex){MessageBox.Show(ex.Message);}};
            PhanQuyenGiaoDien.Gan(this);
            var quickAddHints = new ToolTip(components ??= new System.ComponentModel.Container());
            quickAddHints.SetToolTip(txtHoatChat, "Có thể để trống. Tối đa 150 ký tự.");
            quickAddHints.SetToolTip(txtMaVach, "Có thể để trống. Nhập hoặc quét mã; tối đa 50 ký tự, không trùng thuốc khác.");
            quickAddHints.SetToolTip(cboDonViTinh, "Đơn vị chỉ được chọn khi thêm thuốc, không đổi sau khi tạo.");
            quickAddHints.SetToolTip(cboNhaCungCap, "Không đổi nhà cung cấp sau khi thuốc đã có lô hàng/phiếu nhập.");
            quickAddHints.SetToolTip(btnThemNhom, "Thêm nhóm thuốc mới");
            quickAddHints.SetToolTip(btnThemNCC, "Thêm nhà cung cấp mới");
            btnThemNhom.Click += (_, _) => ThemDanhMuc(false);
            btnThemNCC.Click += (_, _) => ThemDanhMuc(true);
            txtMaVach.KeyDown+=(_,e)=> {if(e.KeyCode==Keys.Enter) {e.SuppressKeyPress=true;SelectNextControl(txtMaVach,true,true,true,true);}};
            txtTimKiem.MaxLength = 200;
            txtTimKiem.PlaceholderText = "Tên thuốc, hoạt chất, nhóm, NCC hoặc mã vạch";
            txtTimKiem.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTimKiem_Click(this, EventArgs.Empty); }
            };
            PosFormStyle.ConfigureRowNavigation(dgvThuoc,row=>dgvThuoc_CellClick(dgvThuoc,new DataGridViewCellEventArgs(0,row.Index)));
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
            khoaNhaCungCap = false;
            SetControlState(false);
            maDangChon=""; txtTenThuoc.Clear();txtMaVach.Clear();txtHoatChat.Clear(); txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;chkKeDon.Checked=false;
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
            using var loading=PosLoading.Show(this);
            GanDuLieuThuoc(bll.TimKiemThuoc(txtTimKiem.Text,ChiThuocDaAn));
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
            dgvThuoc.Enabled=!status;
            cboTrangThaiBan.Enabled = status;chkKeDon.Enabled=status;
            txtMaVach.Enabled = status;
            txtHoatChat.Enabled = status;
            txtTenThuoc.Enabled = status;
            cboNhomThuoc.Enabled = status;
            cboDonViTinh.Enabled = status && isThem;
            txtDonGia.Enabled = status;
            txtSoLuongTon.Enabled = false;
            cboNhaCungCap.Enabled = status && (isThem || !khoaNhaCungCap);
            btnThemNhom.Enabled = status;
            btnThemNCC.Enabled = cboNhaCungCap.Enabled;

            btnLuu.Enabled = status;
            btnBoQua.Enabled = status;

            btnThem.Enabled = !status && !ChiThuocDaAn;
            btnSua.Enabled = !status && !ChiThuocDaAn;
            btnXoa.Enabled = !status;
            btnXoa.Text = ChiThuocDaAn?"Hiện lại":"Ẩn thuốc";
            btnXoa.BackColor = ChiThuocDaAn?Color.FromArgb(15,118,110):Color.FromArgb(225,29,72);
            chkDaAn.Enabled = !status;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            SetControlState(true); // Mở khóa các ô nhập liệu

            // Xóa trắng các ô để người dùng nhập mới
            maDangChon="";
            txtTenThuoc.Clear();txtMaVach.Clear();txtHoatChat.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;chkKeDon.Checked=false;
            txtTenThuoc.Focus();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!dgvThuoc.Enabled) return;
            if (e.RowIndex < 0 || e.RowIndex >= dgvThuoc.Rows.Count ||
                dgvThuoc.Rows[e.RowIndex].DataBoundItem is not DataRowView data) return;
            khoaNhaCungCap = Convert.ToBoolean(data["KhoaNCC"]);chkKeDon.Checked=Convert.ToBoolean(data["KeDon"]);
            maDangChon = Convert.ToString(data["MaThuoc"]) ?? "";
            txtTenThuoc.Text = Convert.ToString(data["TenThuoc"]);
            txtMaVach.Text = Convert.ToString(data["MaVach"]);
            txtHoatChat.Text = Convert.ToString(data["HoatChat"]);
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
                MessageBox.Show("Chọn thuốc cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;       // Đánh dấu đây là thao tác SỬA
            SetControlState(true); // Mở khóa các ô nhập liệu
            txtTenThuoc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(ChiThuocDaAn) {HienLaiThuoc();return;}
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show("Chọn thuốc cần ẩn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận trước khi xóa tránh bấm nhầm
            DialogResult dr = MessageBox.Show("Ẩn thuốc này? Tồn kho và lịch sử vẫn được giữ.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string error;
                bool kq = bll.XoaThuoc(maDangChon.Trim(), out error);

                if (kq)
                {
                    MessageBox.Show("Đã ẩn thuốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();

                    // Xóa trắng các ô nhập
                    maDangChon="";
                    txtTenThuoc.Clear();txtMaVach.Clear();txtHoatChat.Clear();
                    cboNhomThuoc.SelectedIndex = -1;
                    cboDonViTinh.SelectedIndex = -1;
                    txtDonGia.Clear();
                    txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;chkKeDon.Checked=false;
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
                MessageBox.Show("Nhập tên thuốc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThuoc.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số từ 0 trở lên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuongTon.Text.Trim(), out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Tồn kho phải là số nguyên từ 0 trở lên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuongTon.Focus();
                return;
            }

            if (cboNhomThuoc.SelectedValue == null || cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhóm thuốc và nhà cung cấp.");
                return;
            }
            txtDonGia.Text = Store.Shared.TienTe.LamTron(donGia).ToString("0");
            string error = "";
            bool kq = false;
            string maNhom = cboNhomThuoc.SelectedValue?.ToString() ?? "";
            string dvt = cboDonViTinh.Text.Trim();

            if (isThem)
            {
                kq = bll.ThemMoiThuoc(maDangChon.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error, cboTrangThaiBan.SelectedIndex==0,chkKeDon.Checked,txtMaVach.Text,txtHoatChat.Text);
            }
            else
            {
                // Gọi hàm Sửa
                kq = bll.SuaThuoc(maDangChon.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error, cboTrangThaiBan.SelectedIndex==0,chkKeDon.Checked,txtMaVach.Text,txtHoatChat.Text);
            }

            if (kq)
            {
                MessageBox.Show(isThem ? "Đã thêm thuốc." : "Đã lưu thuốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGrid();

                SetControlState(false);
            }
            else
            {
                MessageBox.Show("Không lưu được thuốc: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            SetControlState(false);

            maDangChon="";
            txtTenThuoc.Clear();txtMaVach.Clear();txtHoatChat.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;chkKeDon.Checked=false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using var loading=PosLoading.Show(this);
            try
            {
                ResetDetails(true);
                    GanDuLieuThuoc(bll.TimKiemThuoc(txtTimKiem.Text.Trim(),ChiThuocDaAn));
            }
            catch (Exception ex) { MessageBox.Show("Không tìm được dữ liệu: " + ex.Message, "Lỗi"); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            maDangChon = "";
            txtTenThuoc.Clear();txtMaVach.Clear();txtHoatChat.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            cboNhaCungCap.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            cboTrangThaiBan.SelectedIndex = 0;chkKeDon.Checked=false;
            SetControlState(false);
            LamMoiDuLieu();
            dgvThuoc.ClearSelection();
        }
    }
}
