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
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            btnNhapExcel.Click+=(_,_)=>NhapExcelPhieuNhap();btnMauExcel.Click+=(_,_)=>TaiFileMauNhapKho();
            PhanQuyenGiaoDien.Gan(this);
            PosFormStyle.ConfigureRowNavigation(dgvPhieu,row=>NapDongPhieu(((DataRowView)row.DataBoundItem).Row));
            PosFormStyle.ConfigureGridSelection(this, posLayout, null,clearSelectionOnLeave:false);
            txtSoLo.MaxLength=50;
            cboNhaCungCap.SelectedIndexChanged += (_,_) => { if(!dangNapNCC) NapThuocTheoNCC(); };
            cboThuoc.SelectedIndexChanged += (_,_) => { txtSoLo.Clear(); CapNhatNhapLo(); };
            btnThemNCC.Click += (_,_) => ThemNCC();
            btnTaoThuoc.Click += (_,_) => TaoThuoc();
            CapNhatNhapLo();
            dgvPhieu.CellClick += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvPhieu.Rows[e.RowIndex].DataBoundItem is DataRowView row)
                    NapDongPhieu(row.Row);
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

        private void NapNhaCungCap(int? selected=null,bool throwOnError=false)
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
            NapThuocTheoNCC(throwOnError);
        }

        private void NapThuocTheoNCC(bool throwOnError=false)
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
                catch(Exception ex) {if(throwOnError) throw;MessageBox.Show("Không tải được thuốc: "+ex.Message,"Lỗi");}
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
            btnCapNhatDong.Enabled=txtSoLo.Enabled && dongPhieuDangChon is {RowState:not DataRowState.Detached};
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

        private void NapDongPhieu(DataRow row)
        {
            dongPhieuDangChon=row;
            cboThuoc.SelectedValue=row["MaThuoc"];
            txtSoLo.Text=Convert.ToString(row["SoLo"]);
            dtpHanDung.Value=(DateTime)row["HanDung"];
            nudSoLuong.Value=(int)row["SoLuong"];
            nudGiaNhap.Value=(decimal)row["GiaNhap"];
            CapNhatNhapLo();
        }

        private void BoChonDongPhieu()
        {
            dongPhieuDangChon=null;
            btnCapNhatDong.Enabled=false;
        }

        private void btnThemThuoc_Click(object sender, EventArgs e) => LuuDongPhieu(false);
        private void btnCapNhatDong_Click(object sender, EventArgs e) => LuuDongPhieu(true);
        private void LuuDongPhieu(bool capNhat)
        {
            if(capNhat && (dongPhieuDangChon==null || dongPhieuDangChon.RowState==DataRowState.Detached))
            {
                MessageBox.Show("Chọn dòng phiếu nhập cần cập nhật.","Thông báo");
                return;
            }
            if (cboNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboThuoc.SelectedItem is not DataRowView thuoc)
            {
                MessageBox.Show("Nhà cung cấp này chưa có thuốc để nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string soLo = txtSoLo.Text.Trim();
            if (soLo.Length==0 || soLo.Length>50 || soLo.Any(char.IsControl))
            {
                MessageBox.Show("Số lô không được trống, tối đa 50 ký tự và không chứa ký tự điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLo.Focus();
                return;
            }
            if (dtpHanDung.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Hạn dùng phải sau hôm nay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpHanDung.Focus();
                return;
            }
            if (nudSoLuong.Value<1 || Store.Shared.TienTe.LamTron(nudGiaNhap.Value)<=0)
            {
                MessageBox.Show("Số lượng và giá nhập sau làm tròn phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudGiaNhap.Focus();
                return;
            }

            object[] values={
                Convert.ToInt32(thuoc["MaThuoc"]),
                thuoc["TenThuoc"].ToString()??"",
                thuoc["DonViTinh"].ToString()??"",
                soLo.ToUpper(),
                dtpHanDung.Value.Date,
                (int)nudSoLuong.Value,
                Store.Shared.TienTe.LamTron(nudGiaNhap.Value)};
            if(capNhat) dongPhieuDangChon!.ItemArray=values;
            else dtPhieu.Rows.Add(values);
            BoChonDongPhieu();
            dgvPhieu.ClearSelection();dgvPhieu.CurrentCell=null;

            txtSoLo.Clear();
            nudSoLuong.Value = 1;
            txtSoLo.Focus();
            CapNhatTongVon();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dongPhieuDangChon == null || dongPhieuDangChon.RowState == DataRowState.Detached)
            {
                MessageBox.Show("Chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dtPhieu.Rows.Remove(dongPhieuDangChon);
            BoChonDongPhieu();
            txtSoLo.Clear();nudSoLuong.Value=1;
            CapNhatTongVon();
        }

        internal string? CanhBaoChuaLuu => dtPhieu.Rows.Count>0
            ? $"Phiếu nhập kho: {dtPhieu.Rows.Count} dòng thuốc chưa lưu."
            : (cboThuoc.SelectedItem!=null || !string.IsNullOrWhiteSpace(txtSoLo.Text) || !string.IsNullOrWhiteSpace(txtGhiChu.Text) || nudGiaNhap.Value>0)
                ? "Thông tin phiếu nhập kho đang nhập chưa lưu." : null;

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            if (dtPhieu.Rows.Count == 0) return;
            if (MessageBox.Show("Xóa hết thuốc trong phiếu nhập?", "Xác nhận",
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
            BoChonDongPhieu();
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
                MessageBox.Show("Phiếu nhập chưa có thuốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (MessageBox.Show($"Lưu phiếu nhập {TinhTongVon().ToString("N0", vn)} đ?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int maNCC = Convert.ToInt32(cboNhaCungCap.SelectedValue);
            if (bll.LuuPhieuNhap(PhienDangNhap.NhanVienId, maNCC, txtGhiChu.Text, ds,
                    out int maPhieu, out decimal tongVon, out string error))
            {
                MessageBox.Show(
                    $"Đã lưu PN{maPhieu:D5}. Tổng tiền: {tongVon.ToString("N0", vn)} đ.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiPhieu();
            }
            else
            {
                MessageBox.Show("Không lưu được phiếu nhập: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
