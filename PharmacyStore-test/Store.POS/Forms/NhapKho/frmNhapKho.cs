using System.Data;
using System.Globalization;
using Store.BLL;
using Store.Shared;

namespace Store.POS
{
    public partial class frmNhapKho : Form, ILamMoiDuLieu
    {
        private readonly NhapKhoBLL bll = new NhapKhoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");
        private bool dangNapNCC;

        private DataRow? dongPhieuDangChon;
        private DataTable dtPhieu = new DataTable();   // Các dòng của phiếu nhập
        private readonly Button btnExcelNhapKho;

        public frmNhapKho()
        {
            InitializeComponent();
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
            txtSoLo.MaxLength = 50;

            // Thêm nút Nhập Excel vào tableLayoutPanel34 cạnh nút Lưu phiếu
            tableLayoutPanel34.SetColumnSpan(btnLuu, 1);
            btnExcelNhapKho = new Button
            {
                Name = "btnExcelNhapKho",
                Text = "Nhập Excel ▾",
                Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold),
                BackColor = Color.FromArgb(15, 118, 110), // Teal
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 4, 0, 0)
            };
            btnExcelNhapKho.FlatAppearance.BorderSize = 0;

            var menuExcel = new ContextMenuStrip();
            menuExcel.Items.Add("Nhập danh sách mặt hàng từ Excel (.xlsx)", null, (_, _) => NhapExcelPhieuNhap());
            menuExcel.Items.Add("Tải file mẫu nhập kho theo NCC hiện tại (.xlsx)", null, (_, _) => TaiFileMauNhapKho());
            btnExcelNhapKho.Click += (_, _) => menuExcel.Show(btnExcelNhapKho, new Point(0, btnExcelNhapKho.Height));

            tableLayoutPanel34.Controls.Add(btnExcelNhapKho, 1, 0);

            cboNhaCungCap.SelectedIndexChanged += (_, _) => { if (!dangNapNCC) NapThuocTheoNCC(); };
            cboThuoc.SelectedIndexChanged += (_, _) => { txtSoLo.Clear(); CapNhatNhapLo(); };
            btnThemNCC.Click += (_, _) => ThemNCC();
            btnTaoThuoc.Click += (_, _) => TaoThuoc();
            CapNhatNhapLo();
            dgvPhieu.CellClick += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvPhieu.Rows[e.RowIndex].DataBoundItem is DataRowView row)
                    dongPhieuDangChon = row.Row;
            };
            dgvPhieu.KeyUp += (_, e) =>
            {
                if ((e.KeyCode is Keys.Up or Keys.Down or Keys.Home or Keys.End or Keys.PageUp or Keys.PageDown) &&
                    dgvPhieu.CurrentRow?.DataBoundItem is DataRowView row)
                    dongPhieuDangChon = row.Row;
            };
        }

        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
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

            try
            {
                NapNhaCungCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách nhà cung cấp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            CapNhatTongVon();
        }

        private void NapNhaCungCap(int? selected=null)
        {
            dangNapNCC=true;
            try
            {
                cboNhaCungCap.DisplayMember="TenNCC";cboNhaCungCap.ValueMember="MaNCC";
                cboNhaCungCap.DataSource=bll.LayNhaCungCap();
                cboNhaCungCap.SelectedIndex=-1;
                if(selected.HasValue)
                {
                    cboNhaCungCap.SelectedValue=selected.Value;
                    if(cboNhaCungCap.SelectedValue is not int id || id!=selected.Value) cboNhaCungCap.SelectedIndex=-1;
                }
            }
            finally {dangNapNCC=false;}
            NapThuocTheoNCC();
        }

        private void NapThuocTheoNCC()
        {
            txtSoLo.Clear();
            cboThuoc.DataSource=null;
            if(cboNhaCungCap.SelectedValue is int id && id>0)
            {
                try
                {
                    cboThuoc.DisplayMember="TenThuoc";cboThuoc.ValueMember="MaThuoc";
                    cboThuoc.DataSource=bll.LayThuocTheoNCC(id);
                    cboThuoc.SelectedIndex=-1;
                }
                catch(Exception ex) {MessageBox.Show("Không tải được thuốc: "+ex.Message,"Lỗi");}
            }
            CapNhatNhapLo();
        }

        private void CapNhatNhapLo()
        {
            bool coNCC=cboNhaCungCap.SelectedValue is int id && id>0;
            cboThuoc.Enabled=coNCC;
            btnTaoThuoc.Enabled=coNCC;
            txtSoLo.Enabled=coNCC && cboThuoc.SelectedItem is DataRowView;
            txtSoLo.BackColor=txtSoLo.Enabled?Color.White:SystemColors.ControlLight;
            btnThemThuoc.Enabled=txtSoLo.Enabled;
        }

        private void ThemNCC()
        {
            if(dtPhieu.Rows.Count>0) return;
            using var dialog=new frmThemDanhMuc();
            if(dialog.ShowDialog(this)!=DialogResult.OK) return;
            try {NapNhaCungCap(dialog.CreatedId);}
            catch(Exception ex) {MessageBox.Show("Không tải được NCC mới: "+ex.Message,"Lỗi");}
        }

        private void TaoThuoc()
        {
            if(cboNhaCungCap.SelectedValue is not int id) return;
            using var dialog=new frmThemThuoc(id,cboNhaCungCap.Text);
            if(dialog.ShowDialog(this)!=DialogResult.OK) return;
            NapThuocTheoNCC();
            cboThuoc.SelectedValue=dialog.CreatedId;
            CapNhatNhapLo();
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
                Store.Shared.TienTe.LamTron(nudGiaNhap.Value));

            txtSoLo.Clear();
            nudSoLuong.Value = 1;
            txtSoLo.Focus();
            CapNhatTongVon();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dongPhieuDangChon == null || dongPhieuDangChon.RowState == DataRowState.Detached)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dtPhieu.Rows.Remove(dongPhieuDangChon);
            dongPhieuDangChon = null;
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
            btnThemNCC.Enabled = dtPhieu.Rows.Count == 0;
            cboNhaCungCap.Enabled = dtPhieu.Rows.Count == 0;   // Phiếu đã có thuốc thì không đổi nhà cung cấp nữa
        }

        private void LamMoiPhieu()
        {
            dongPhieuDangChon = null;
            dtPhieu.Rows.Clear();
            txtGhiChu.Clear();
            txtSoLo.Clear();
            nudSoLuong.Value = 1;
            nudGiaNhap.Value = 0;
            dtpHanDung.Value=DateTime.Today.AddYears(1);
            CapNhatTongVon();
            try {NapNhaCungCap();}
            catch(Exception ex)
            {
                cboNhaCungCap.DataSource=null;NapThuocTheoNCC();
                MessageBox.Show("Phiếu đã được làm mới nhưng chưa tải được NCC: "+ex.Message,"Lỗi");
            }
            CapNhatNhapLo();
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

        private void TaiFileMauNhapKho()
        {
            if (cboNhaCungCap.SelectedValue is not int maNCC || maNCC <= 0)
            {
                MessageBox.Show("Vui lòng chọn một Nhà cung cấp trước để hệ thống xuất mẫu kèm danh sách thuốc của nhà cung cấp đó.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var dtThuocNCC = bll.LayThuocTheoNCC(maNCC);
                string tenNCC = cboNhaCungCap.Text;
                Common.ExcelHelper.XuatFileMauNhapKho(tenNCC, dtThuocNCC);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file mẫu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NhapExcelPhieuNhap()
        {
            if (cboNhaCungCap.SelectedValue is not int maNCC || maNCC <= 0)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp của phiếu trước khi nhập từ Excel.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhaCungCap.Focus();
                return;
            }

            string? duongDan = Common.ExcelHelper.ChonFileExcelDeDoc();
            if (string.IsNullOrEmpty(duongDan)) return;

            try
            {
                var dtThuocNCC = bll.LayThuocTheoNCC(maNCC);
                if (dtThuocNCC.Rows.Count == 0)
                {
                    MessageBox.Show("Nhà cung cấp này hiện chưa có thuốc nào trong hệ thống. Hãy tạo thuốc trước.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var danhSach = Common.ExcelHelper.DocDanhSachNhapKhoTuExcel(duongDan, dtThuocNCC, out var errors);
                if (danhSach.Count == 0)
                {
                    string msg = "Không đọc được dòng mặt hàng nhập kho hợp lệ nào từ file Excel.";
                    if (errors.Count > 0)
                        msg += "\n\nChi tiết lỗi:\n" + string.Join("\n", errors.Take(8));
                    MessageBox.Show(msg, "Lỗi nhập file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Map để lấy Đơn vị tính của từng thuốc
                var dicDonVi = new Dictionary<int, string>();
                foreach (DataRow r in dtThuocNCC.Rows)
                    dicDonVi[Convert.ToInt32(r["MaThuoc"])] = r["DonViTinh"]?.ToString() ?? "";

                int themThanhCong = 0;
                foreach (var item in danhSach)
                {
                    string dvt = dicDonVi.TryGetValue(item.MaThuoc, out var dv) ? dv : "";
                    dtPhieu.Rows.Add(
                        item.MaThuoc,
                        item.TenThuoc,
                        dvt,
                        item.SoLo,
                        item.HanDung,
                        item.SoLuong,
                        Store.Shared.TienTe.LamTron(item.GiaNhap));
                    themThanhCong++;
                }

                CapNhatTongVon();

                string ketQua = $"Đã thêm {themThanhCong} mặt hàng từ file Excel vào phiếu nhập hiện tại.";
                if (errors.Count > 0)
                {
                    ketQua += $"\n\nBỏ qua {errors.Count} dòng có lỗi:\n" + string.Join("\n", errors.Take(6));
                    if (errors.Count > 6) ketQua += $"\n... và {errors.Count - 6} lỗi khác.";
                    MessageBox.Show(ketQua, "Kết quả nhập Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ketQua + "\nHãy kiểm tra lại danh sách trên lưới và bấm 'LƯU PHIẾU' để hoàn tất.",
                        "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đọc file Excel nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
