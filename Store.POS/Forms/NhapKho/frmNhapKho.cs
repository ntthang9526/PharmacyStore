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

        public frmNhapKho()
        {
            InitializeComponent();
            btnNhapExcel.Click+=(_,_)=>NhapExcelPhieuNhap();btnMauExcel.Click+=(_,_)=>TaiFileMauNhapKho();
            PhanQuyenGiaoDien.Gan(this);
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
            txtSoLo.MaxLength=50;
            cboNhaCungCap.SelectedIndexChanged += (_,_) => { if(!dangNapNCC) NapThuocTheoNCC(); };
            cboThuoc.SelectedIndexChanged += (_,_) => { txtSoLo.Clear(); CapNhatNhapLo(); };
            btnThemNCC.Click += (_,_) => ThemNCC();
            btnTaoThuoc.Click += (_,_) => TaoThuoc();
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
    }
}
