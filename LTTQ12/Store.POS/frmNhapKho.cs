using System.Data;
using System.Globalization;
using Store.BLL;
using Store.Shared;

namespace Store.POS
{
    public partial class frmNhapKho : Form
    {
        private readonly NhapKhoBLL bll = new NhapKhoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");
        private DataTable dtPhieu = new DataTable();   // Các dòng của phiếu nhập

        public frmNhapKho()
        {
            InitializeComponent();
        }

        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            lblNgayNhapGiaTri.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dtpHanDung.Value = DateTime.Today.AddYears(1);

            dtPhieu.Columns.Add("MaThuoc", typeof(int));
            dtPhieu.Columns.Add("TenThuoc", typeof(string));
            dtPhieu.Columns.Add("DonViTinh", typeof(string));
            dtPhieu.Columns.Add("SoLo", typeof(string));
            dtPhieu.Columns.Add("HanDung", typeof(DateTime));
            dtPhieu.Columns.Add("SoLuong", typeof(int));
            dtPhieu.Columns.Add("GiaNhap", typeof(decimal));
            dtPhieu.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * GiaNhap");
            dgvPhieu.DataSource = dtPhieu;
            CauHinhLuoi();

            try
            {
                cboNhaCungCap.DataSource = bll.LayNhaCungCap();
                cboNhaCungCap.DisplayMember = "TenNCC";
                cboNhaCungCap.ValueMember = "MaNCC";
                NapThuocTheoNCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CapNhatTongVon();
        }

        private void CauHinhLuoi()
        {
            dgvPhieu.Columns["MaThuoc"]!.HeaderText = "Mã";
            dgvPhieu.Columns["TenThuoc"]!.HeaderText = "Tên thuốc";
            dgvPhieu.Columns["DonViTinh"]!.HeaderText = "ĐVT";
            dgvPhieu.Columns["SoLo"]!.HeaderText = "Số lô";
            dgvPhieu.Columns["HanDung"]!.HeaderText = "Hạn dùng";
            dgvPhieu.Columns["SoLuong"]!.HeaderText = "Số lượng";
            dgvPhieu.Columns["GiaNhap"]!.HeaderText = "Giá nhập";
            dgvPhieu.Columns["ThanhTien"]!.HeaderText = "Thành tiền";
            dgvPhieu.Columns["HanDung"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieu.Columns["GiaNhap"]!.DefaultCellStyle.Format = "N0";
            dgvPhieu.Columns["ThanhTien"]!.DefaultCellStyle.Format = "N0";
            dgvPhieu.Columns["MaThuoc"]!.FillWeight = 30;
            dgvPhieu.Columns["TenThuoc"]!.FillWeight = 140;
            dgvPhieu.Columns["DonViTinh"]!.FillWeight = 40;
        }

        // Chỉ hiện thuốc của nhà cung cấp đang chọn
        private void NapThuocTheoNCC()
        {
            if (cboNhaCungCap.SelectedValue == null)
            {
                cboThuoc.DataSource = null;
                return;
            }

            int maNCC = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            cboThuoc.DataSource = bll.LayThuocTheoNCC(maNCC);
            cboThuoc.DisplayMember = "TenThuoc";
            cboThuoc.ValueMember = "MaThuoc";
        }

        // Chỉ chạy khi NGƯỜI DÙNG đổi nhà cung cấp (không chạy khi nạp dữ liệu bằng code)
        private void cboNhaCungCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NapThuocTheoNCC();
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboThuoc.SelectedItem is not DataRowView thuoc)
            {
                MessageBox.Show("Nhà cung cấp này chưa có thuốc nào để nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soLo = txtSoLo.Text.Trim();
            if (soLo == "")
            {
                MessageBox.Show("Vui lòng nhập số lô!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLo.Focus();
                return;
            }
            if (dtpHanDung.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Hạn dùng phải sau ngày hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpHanDung.Focus();
                return;
            }
            if (nudGiaNhap.Value <= 0)
            {
                MessageBox.Show("Giá nhập phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudGiaNhap.Focus();
                return;
            }

            dtPhieu.Rows.Add(
                Convert.ToInt32(thuoc["MaThuoc"]),
                thuoc["TenThuoc"].ToString(),
                thuoc["DonViTinh"].ToString(),
                soLo.ToUpper(),
                dtpHanDung.Value.Date,
                (int)nudSoLuong.Value,
                nudGiaNhap.Value);

            txtSoLo.Clear();
            nudSoLuong.Value = 1;
            txtSoLo.Focus();
            CapNhatTongVon();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow?.DataBoundItem is not DataRowView drv)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dtPhieu.Rows.Remove(drv.Row);
            CapNhatTongVon();
        }

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            if (dtPhieu.Rows.Count == 0) return;
            if (MessageBox.Show("Xóa toàn bộ các dòng trong phiếu nhập?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LamMoiPhieu();
            }
        }

        // Tiền vốn = tổng (số lượng x giá nhập)
        private decimal TinhTongVon()
        {
            decimal tong = 0;
            foreach (DataRow r in dtPhieu.Rows)
                tong += Convert.ToInt32(r["SoLuong"]) * Convert.ToDecimal(r["GiaNhap"]);
            return tong;
        }

        private void CapNhatTongVon()
        {
            lblTongVon.Text = TinhTongVon().ToString("N0", vn) + " đ";
            btnLuu.Enabled = dtPhieu.Rows.Count > 0;
            cboNhaCungCap.Enabled = dtPhieu.Rows.Count == 0;   // Phiếu đã có thuốc thì không đổi nhà cung cấp nữa
        }

        private void LamMoiPhieu()
        {
            dtPhieu.Rows.Clear();
            txtGhiChu.Clear();
            txtSoLo.Clear();
            nudSoLuong.Value = 1;
            CapNhatTongVon();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboNhaCungCap.SelectedValue == null || dtPhieu.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<NhapKhoItem> ds = dtPhieu.Rows.Cast<DataRow>().Select(r => new NhapKhoItem
            {
                MaThuoc = (int)r["MaThuoc"],
                TenThuoc = r["TenThuoc"].ToString() ?? "",
                SoLo = r["SoLo"].ToString() ?? "",
                HanDung = (DateTime)r["HanDung"],
                SoLuong = (int)r["SoLuong"],
                GiaNhap = (decimal)r["GiaNhap"]
            }).ToList();

            if (MessageBox.Show($"Lưu phiếu nhập với tổng tiền vốn {TinhTongVon().ToString("N0", vn)} đ ?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int maNCC = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            if (bll.LuuPhieuNhap(PhienDangNhap.NhanVienId, maNCC, txtGhiChu.Text, ds,
                    out int maPhieu, out decimal tongVon, out string error))
            {
                MessageBox.Show(
                    $"Đã lưu phiếu nhập PN{maPhieu:D5}.\nTổng tiền vốn: {tongVon.ToString("N0", vn)} đ\nSố lượng thuốc đã được cộng dồn vào kho.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiPhieu();
            }
            else
            {
                MessageBox.Show("Lưu phiếu nhập thất bại: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
