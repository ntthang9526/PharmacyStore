using System.Data;
using System.Globalization;
using Store.BLL;
using Store.Shared;

namespace Store.POS
{
    public partial class frmBanHang : Form
    {
        private readonly BanHangBLL bll = new BanHangBLL();
        private readonly VoucherBLL voucherBll = new VoucherBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");

        private DataTable dtGio = new DataTable();   // Giỏ hàng
        private VoucherInfo? voucherApDung = null;   // Voucher đang áp dụng (null = chưa áp)

        public frmBanHang()
        {
            InitializeComponent();
        }

        // =====================================================================
        // KHỞI TẠO
        // =====================================================================
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            TaoBangGioHang();
            dgvGioHang.DataSource = dtGio;
            CauHinhCotGioHang();

            TimThuoc("");
            CapNhatTongTien();
        }

        private void TaoBangGioHang()
        {
            dtGio.Columns.Add("MaThuoc", typeof(int));
            dtGio.Columns.Add("TenThuoc", typeof(string));
            dtGio.Columns.Add("DonViTinh", typeof(string));
            dtGio.Columns.Add("DonGia", typeof(decimal));
            dtGio.Columns.Add("SoLuong", typeof(int));
            dtGio.Columns.Add("TonKho", typeof(int));                                  // Để kiểm tra khi sửa số lượng
            dtGio.Columns.Add("ThanhTien", typeof(decimal), "DonGia * SoLuong");       // Cột tự tính
        }

        private void CauHinhCotGioHang()
        {
            dgvGioHang.Columns["MaThuoc"]!.HeaderText = "Mã";
            dgvGioHang.Columns["TenThuoc"]!.HeaderText = "Tên thuốc";
            dgvGioHang.Columns["DonViTinh"]!.HeaderText = "ĐVT";
            dgvGioHang.Columns["DonGia"]!.HeaderText = "Đơn giá";
            dgvGioHang.Columns["SoLuong"]!.HeaderText = "Số lượng";
            dgvGioHang.Columns["ThanhTien"]!.HeaderText = "Thành tiền";
            dgvGioHang.Columns["TonKho"]!.Visible = false;

            dgvGioHang.Columns["DonGia"]!.DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["ThanhTien"]!.DefaultCellStyle.Format = "N0";

            // Chỉ cho sửa cột Số lượng
            foreach (DataGridViewColumn col in dgvGioHang.Columns)
                col.ReadOnly = col.Name != "SoLuong";

            dgvGioHang.Columns["MaThuoc"]!.FillWeight = 30;
            dgvGioHang.Columns["TenThuoc"]!.FillWeight = 120;
            dgvGioHang.Columns["DonViTinh"]!.FillWeight = 35;
            dgvGioHang.Columns["SoLuong"]!.FillWeight = 45;
        }

        private void CauHinhCotThuoc()
        {
            if (dgvThuoc.Columns.Count == 0) return;

            dgvThuoc.Columns["MaThuoc"]!.HeaderText = "Mã";
            dgvThuoc.Columns["TenThuoc"]!.HeaderText = "Tên thuốc";
            dgvThuoc.Columns["DonViTinh"]!.HeaderText = "ĐVT";
            dgvThuoc.Columns["DonGia"]!.HeaderText = "Đơn giá";
            dgvThuoc.Columns["KeDon"]!.HeaderText = "Kê đơn";
            dgvThuoc.Columns["TonKho"]!.HeaderText = "Tồn kho";
            dgvThuoc.Columns["DonGia"]!.DefaultCellStyle.Format = "N0";

            dgvThuoc.Columns["MaThuoc"]!.FillWeight = 30;
            dgvThuoc.Columns["TenThuoc"]!.FillWeight = 130;
            dgvThuoc.Columns["DonViTinh"]!.FillWeight = 40;
            dgvThuoc.Columns["KeDon"]!.FillWeight = 40;
            dgvThuoc.Columns["TonKho"]!.FillWeight = 40;
        }

        // =====================================================================
        // TÌM KIẾM THUỐC
        // =====================================================================
        private void TimThuoc(string tuKhoa)
        {
            try
            {
                dgvThuoc.DataSource = bll.TimThuoc(tuKhoa);
                CauHinhCotThuoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách thuốc: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimThuoc(txtTimKiem.Text);
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   // Tránh tiếng "bíp"
                TimThuoc(txtTimKiem.Text);
            }
        }

        // =====================================================================
        // GIỎ HÀNG
        // =====================================================================
        private void btnThemGio_Click(object sender, EventArgs e)
        {
            ThemVaoGio();
        }

        private void dgvThuoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ThemVaoGio();
        }

        private void ThemVaoGio()
        {
            DataGridViewRow? row = dgvThuoc.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Vui lòng chọn thuốc cần thêm vào giỏ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maThuoc = Convert.ToInt32(row.Cells["MaThuoc"].Value);
            string tenThuoc = row.Cells["TenThuoc"].Value?.ToString() ?? "";
            string dvt = row.Cells["DonViTinh"].Value?.ToString() ?? "";
            decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
            int tonKho = Convert.ToInt32(row.Cells["TonKho"].Value);
            bool keDon = Convert.ToBoolean(row.Cells["KeDon"].Value);
            int soLuong = (int)nudSoLuong.Value;

            if (tonKho <= 0)
            {
                MessageBox.Show($"Thuốc \"{tenThuoc}\" đã hết hàng (hoặc các lô còn lại đã hết hạn)!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow? dong = dtGio.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["MaThuoc"] == maThuoc);
            int daCo = dong == null ? 0 : (int)dong["SoLuong"];

            if (daCo + soLuong > tonKho)
            {
                MessageBox.Show($"Chỉ còn {tonKho} {dvt} trong kho (trong giỏ đã có {daCo}).", "Không đủ hàng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thuốc kê đơn: nhắc nhân viên xác nhận có đơn của bác sĩ
            if (keDon && dong == null)
            {
                DialogResult dr = MessageBox.Show(
                    $"\"{tenThuoc}\" là thuốc KÊ ĐƠN.\nKhách hàng đã có đơn thuốc của bác sĩ chưa?",
                    "Thuốc kê đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes) return;
            }

            if (dong != null)
                dong["SoLuong"] = daCo + soLuong;
            else
                dtGio.Rows.Add(maThuoc, tenThuoc, dvt, donGia, soLuong, tonKho);

            nudSoLuong.Value = 1;
            CapNhatTongTien();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null || dgvGioHang.CurrentRow.DataBoundItem is not DataRowView drv)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa trong giỏ hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvGioHang.CancelEdit();
            dtGio.Rows.Remove(drv.Row);
            CapNhatTongTien();
        }

        private void btnXoaGio_Click(object sender, EventArgs e)
        {
            if (dtGio.Rows.Count == 0) return;

            DialogResult dr = MessageBox.Show("Xóa toàn bộ giỏ hàng?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                LamMoiDonHang();
        }

        // --- Sửa số lượng trực tiếp trên lưới giỏ hàng ---
        private void dgvGioHang_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvGioHang.Columns[e.ColumnIndex].Name != "SoLuong") return;

            if (!int.TryParse(e.FormattedValue?.ToString(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên lớn hơn 0 (muốn bỏ thuốc thì dùng nút Xóa dòng).",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            int ton = Convert.ToInt32(dgvGioHang.Rows[e.RowIndex].Cells["TonKho"].Value);
            if (sl > ton)
            {
                MessageBox.Show($"Trong kho chỉ còn {ton}!", "Không đủ hàng",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void dgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BindingContext?[dtGio]?.EndCurrentEdit(); // Ghi giá trị vào DataTable để cột Thành tiền tự cập nhật
            CapNhatTongTien();
        }

        private void dgvGioHang_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;   // Nhập chữ vào ô số: bỏ qua, CellValidating đã báo lỗi
        }

        // =====================================================================
        // VOUCHER + TỔNG TIỀN
        // =====================================================================
        private void btnApDung_Click(object sender, EventArgs e)
        {
            string code = txtVoucher.Text.Trim();
            if (code == "")
            {
                MessageBox.Show("Vui lòng nhập mã voucher!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVoucher.Focus();
                return;
            }

            if (voucherBll.KiemTra(code, out VoucherInfo? v, out string error))
            {
                voucherApDung = v;
                lblVoucherThongBao.ForeColor = Color.Green;
                lblVoucherThongBao.Text = v!.DiscountType == "PERCENT"
                    ? $"Đã áp dụng: giảm {v.DiscountValue:0.##}%"
                    : $"Đã áp dụng: giảm {v.DiscountValue.ToString("N0", vn)} đ";
            }
            else
            {
                voucherApDung = null;
                lblVoucherThongBao.ForeColor = Color.Red;
                lblVoucherThongBao.Text = error;
            }
            CapNhatTongTien();
        }

        // Sửa mã voucher sau khi đã áp dụng -> hủy áp dụng, bắt bấm "Áp dụng" lại
        private void txtVoucher_TextChanged(object sender, EventArgs e)
        {
            if (voucherApDung != null)
            {
                voucherApDung = null;
                lblVoucherThongBao.Text = "";
                CapNhatTongTien();
            }
        }

        private decimal TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow r in dtGio.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                tong += Convert.ToDecimal(r["DonGia"]) * Convert.ToInt32(r["SoLuong"]);
            }
            return tong;
        }

        private void CapNhatTongTien()
        {
            decimal tong = TinhTongTien();
            decimal giam = voucherApDung == null ? 0 : voucherBll.TinhGiamGia(voucherApDung, tong);
            decimal phaiTra = tong - giam;

            lblTongTien.Text = tong.ToString("N0", vn) + " đ";
            lblGiamGia.Text = "- " + giam.ToString("N0", vn) + " đ";
            lblPhaiTra.Text = phaiTra.ToString("N0", vn) + " đ";

            btnThanhToan.Enabled = dtGio.Rows.Count > 0;
        }

        private void LamMoiDonHang()
        {
            dgvGioHang.CancelEdit();
            dtGio.Rows.Clear();
            voucherApDung = null;
            txtVoucher.Clear();
            lblVoucherThongBao.Text = "";
            nudSoLuong.Value = 1;
            CapNhatTongTien();
        }

        // =====================================================================
        // THANH TOÁN (lưu hóa đơn + trừ tồn kho)
        // =====================================================================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            dgvGioHang.EndEdit();

            if (dtGio.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<CartItem> items = dtGio.Rows.Cast<DataRow>().Select(r => new CartItem
            {
                MaThuoc = (int)r["MaThuoc"],
                TenThuoc = r["TenThuoc"].ToString() ?? "",
                SoLuong = (int)r["SoLuong"],
                DonGia = (decimal)r["DonGia"]
            }).ToList();

            decimal tamTinh = TinhTongTien();
            decimal tamGiam = voucherApDung == null ? 0 : voucherBll.TinhGiamGia(voucherApDung, tamTinh);

            DialogResult dr = MessageBox.Show(
                $"Xác nhận thanh toán {(tamTinh - tamGiam).ToString("N0", vn)} đ ?",
                "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            bool ok = bll.ThanhToan(PhienDangNhap.NhanVienId, items, voucherApDung?.Code,
                out int maHoaDon, out decimal tongTien, out decimal giamGia, out string error);

            if (ok)
            {
                MessageBox.Show(
                    $"Thanh toán thành công!\n\n" +
                    $"Mã hóa đơn: HD{maHoaDon:D5}\n" +
                    $"Tổng tiền hàng: {tongTien.ToString("N0", vn)} đ\n" +
                    $"Giảm giá: {giamGia.ToString("N0", vn)} đ\n" +
                    $"Khách phải trả: {(tongTien - giamGia).ToString("N0", vn)} đ",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoiDonHang();
                TimThuoc(txtTimKiem.Text);   // Tải lại để thấy tồn kho đã bị trừ
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại: " + error, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                TimThuoc(txtTimKiem.Text);   // Tồn kho có thể đã thay đổi
            }
        }
    }
}
