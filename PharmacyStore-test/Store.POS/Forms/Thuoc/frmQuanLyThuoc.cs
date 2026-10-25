using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmQuanLyThuoc : Form, ILamMoiDuLieu
    {
        private ThuocBLL bll = new ThuocBLL(); // Gọi tầng BLL để xử lý nghiệp vụ và validate

        private bool isThem = false;          // Biến cờ kiểm tra trạng thái: true là đang thêm mới, false là bình thường/sửa
        private readonly Button btnNhapExcel;
        private readonly Button btnXuatExcel;

        public frmQuanLyThuoc()
        {
            InitializeComponent();
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
            PosFormStyle.ConfigureGridSelection(this, posLayout, ResetDetails);

            // Cấu hình nút Nhập Excel & Xuất Excel trên thanh công cụ
            btnNhapExcel = new Button
            {
                Name = "btnNhapExcel",
                Text = "Nhập Excel",
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(22, 101, 52), // Xanh lá đậm
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 0, 0, 0)
            };
            btnNhapExcel.FlatAppearance.BorderSize = 0;
            btnNhapExcel.Click += (_, _) => NhapThuocTuExcel();

            btnXuatExcel = new Button
            {
                Name = "btnXuatExcel",
                Text = "Xuất Excel ▾",
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(15, 118, 110), // Teal
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 0, 0, 0)
            };
            btnXuatExcel.FlatAppearance.BorderSize = 0;

            var menuXuat = new ContextMenuStrip();
            menuXuat.Items.Add("Xuất toàn bộ danh sách thuốc (.xlsx)", null, (_, _) => XuatDanhSachThuocExcel());
            menuXuat.Items.Add("Tải file Excel mẫu để nhập thuốc (.xlsx)", null, (_, _) => Common.ExcelHelper.XuatFileMauNhapThuoc());
            btnXuatExcel.Click += (_, _) => menuXuat.Show(btnXuatExcel, new Point(0, btnXuatExcel.Height));

            tableLayoutPanel4.ColumnCount = 5;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel4.Controls.Add(btnNhapExcel, 3, 0);
            tableLayoutPanel4.Controls.Add(btnXuatExcel, 4, 0);
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

        private void ResetDetails()
        {
            isThem = false;
            SetControlState(false);
            txtMaThuoc.Clear(); txtTenThuoc.Clear(); txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
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
            ResetDetails();
        }
        private void LoadDataToGrid()
        {
            dgvThuoc.DataSource = bll.GetAllThuoc();
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
            txtMaThuoc.Enabled = false;
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
            txtMaThuoc.Clear();
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
            txtTenThuoc.Focus();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvThuoc.Rows[e.RowIndex];
                txtMaThuoc.Text = row.Cells["MaThuoc"].Value.ToString();
                txtTenThuoc.Text = row.Cells["TenThuoc"].Value.ToString();
                cboNhomThuoc.SelectedValue = row.Cells["MaNhom"].Value;
                cboNhaCungCap.SelectedValue = row.Cells["MaNCC"].Value;
                cboDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
                txtDonGia.Text = Store.Shared.TienTe.LamTron(Convert.ToDecimal(row.Cells["DonGia"].Value)).ToString("0");
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
            DialogResult dr = MessageBox.Show("Ngừng kinh doanh thuốc này? Lịch sử và tồn kho vẫn được giữ.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string error;
                bool kq = bll.XoaThuoc(txtMaThuoc.Text.Trim(), out error);

                if (kq)
                {
                    MessageBox.Show("Đã ngừng kinh doanh thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGrid();

                    // Xóa trắng các ô nhập
                    txtMaThuoc.Clear();
                    txtTenThuoc.Clear();
                    cboNhomThuoc.SelectedIndex = -1;
                    cboDonViTinh.SelectedIndex = -1;
                    txtDonGia.Clear();
                    txtSoLuongTon.Text = "0";
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
                kq = bll.ThemMoiThuoc(txtMaThuoc.Text.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error);
            }
            else
            {
                // Gọi hàm Sửa
                kq = bll.SuaThuoc(txtMaThuoc.Text.Trim(), txtTenThuoc.Text.Trim(), maNhom, dvt, txtDonGia.Text.Trim(), txtSoLuongTon.Text.Trim(), Convert.ToInt32(cboNhaCungCap.SelectedValue), out error);
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

            txtMaThuoc.Clear();
            txtTenThuoc.Clear();
            cboNhomThuoc.SelectedIndex = -1;
            cboDonViTinh.SelectedIndex = -1;
            txtDonGia.Clear();
            txtSoLuongTon.Text = "0";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDetails();
                dgvThuoc.DataSource = bll.TimKiemThuoc(txtTimKiem.Text.Trim());
            }
            catch (Exception ex) { MessageBox.Show("Không tìm được dữ liệu: " + ex.Message, "Lỗi"); }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close(); 
            }
        }

        private void XuatDanhSachThuocExcel()
        {
            try
            {
                var dt = bll.GetAllThuoc();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Hiện không có thuốc nào để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Common.ExcelHelper.XuatDanhSachThuoc(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhapThuocTuExcel()
        {
            string? duongDan = Common.ExcelHelper.ChonFileExcelDeDoc();
            if (string.IsNullOrEmpty(duongDan)) return;

            try
            {
                var danhSach = Common.ExcelHelper.DocDanhSachThuocTuExcel(duongDan, out var errors);
                if (danhSach.Count == 0)
                {
                    string msg = "Không đọc được dữ liệu thuốc hợp lệ từ file.";
                    if (errors.Count > 0)
                        msg += "\n\nChi tiết lỗi:\n" + string.Join("\n", errors.Take(10));
                    MessageBox.Show(msg, "Lỗi nhập file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tải danh mục hiện có để map
                var nhomBLL = new NhomThuocBLL();
                var nccBLL = new NhaCungCapBLL();

                var dtNhom = nhomBLL.GetAllNhomThuoc();
                var dtNCC = nccBLL.GetAllNhaCungCap();

                var dicNhom = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow r in dtNhom.Rows)
                    dicNhom[r["TenNhom"].ToString()!.Trim()] = Convert.ToInt32(r["MaNhom"]);

                var dicNCC = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (DataRow r in dtNCC.Rows)
                    dicNCC[r["TenNCC"].ToString()!.Trim()] = Convert.ToInt32(r["MaNCC"]);

                int thanhCong = 0;
                int nhomMoi = 0;
                int nccMoi = 0;

                foreach (var item in danhSach)
                {
                    // 1. Kiểm tra / Tự động tạo nhóm thuốc nếu chưa có
                    if (!dicNhom.TryGetValue(item.TenNhom, out int maNhom))
                    {
                        if (nhomBLL.ThemNhanhNhom(item.TenNhom, out maNhom, out _))
                        {
                            dicNhom[item.TenNhom] = maNhom;
                            nhomMoi++;
                        }
                        else
                        {
                            errors.Add($"Dòng {item.RowIndex}: Không tạo được nhóm thuốc '{item.TenNhom}'.");
                            continue;
                        }
                    }

                    // 2. Kiểm tra / Tự động tạo nhà cung cấp nếu chưa có
                    if (!dicNCC.TryGetValue(item.TenNCC, out int maNCC))
                    {
                        if (nccBLL.ThemNhanhNCC(item.TenNCC, "0000000000", "Chưa cập nhật", out maNCC, out _))
                        {
                            dicNCC[item.TenNCC] = maNCC;
                            nccMoi++;
                        }
                        else
                        {
                            errors.Add($"Dòng {item.RowIndex}: Không tạo được NCC '{item.TenNCC}'.");
                            continue;
                        }
                    }

                    // 3. Thêm mới thuốc
                    if (bll.ThemMoiThuocFull(item.TenThuoc, maNhom, maNCC, item.BarCode, item.DonViTinh, item.GiaBan, out string errThuoc))
                    {
                        thanhCong++;
                    }
                    else
                    {
                        errors.Add($"Dòng {item.RowIndex}: Không thêm được thuốc '{item.TenThuoc}' ({errThuoc}).");
                    }
                }

                // Cập nhật lại dữ liệu giao diện
                LoadDataToGrid();
                LoadComboNhomThuoc();
                cboNhaCungCap.DataSource = nccBLL.GetAllNhaCungCap();

                string ketQua = $"Đã nhập thành công {thanhCong}/{danhSach.Count} thuốc.";
                if (nhomMoi > 0 || nccMoi > 0)
                    ketQua += $"\n(Tự động tạo mới: {nhomMoi} nhóm thuốc, {nccMoi} nhà cung cấp).";

                if (errors.Count > 0)
                {
                    ketQua += $"\n\nCó {errors.Count} lỗi xảy ra:\n" + string.Join("\n", errors.Take(8));
                    if (errors.Count > 8) ketQua += $"\n... và {errors.Count - 8} lỗi khác.";
                    MessageBox.Show(ketQua, "Kết quả nhập Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ketQua, "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đọc và nhập file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
