using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmCanhBao : Form, ILamMoiDuLieu
    {
        private bool dangNapCanhBao;
        private readonly CanhBaoBLL bll = new CanhBaoBLL();

        public frmCanhBao()
        {
            InitializeComponent();
            global::Store.POS.PosFormStyle.ConfigureDisabledButtons(this);
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
            nudNguongTon.KeyDown += nudNguongTon_KeyDown;
            dgvSapHetHan.CellFormatting += (_, e) =>
            {
                if (e.RowIndex >= 0 && dgvSapHetHan.Columns[e.ColumnIndex].Name == "SoNgayConLai"
                    && e.Value is int days && days < 0)
                {
                    e.Value = "Hết hạn";
                    e.FormattingApplied = true;
                }
            };

            dgvSapHetHan.CellValueChanged += dgvSapHetHan_CellValueChanged;
            dgvSapHetHan.CurrentCellDirtyStateChanged += (_, e) =>
            {
                if (dgvSapHetHan.IsCurrentCellDirty) dgvSapHetHan.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void frmCanhBao_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            nudNguongTon.Value = CanhBaoSettings.NguongTon;
            LocCanhBao();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocCanhBao();
        }

        private void nudNguongTon_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            if (!int.TryParse(nudNguongTon.Text, out int value) || value < nudNguongTon.Minimum || value > nudNguongTon.Maximum)
            {
                MessageBox.Show(this, "Ngưỡng tồn phải là số nguyên từ 1 đến 100.000.", "Ngưỡng tồn");
                return;
            }
            try
            {
                CanhBaoSettings.Luu(value);
                nudNguongTon.Value = value;
                LocCanhBao();
            }
            catch (Exception ex) { MessageBox.Show(this, "Không lưu được ngưỡng tồn: " + ex.Message, "Lỗi"); }
        }

        private void LocCanhBao()
        {
            try
            {
                int nguongTon = (int)nudNguongTon.Value;
                int soNgay = (int)nudSoNgay.Value;

                DataTable dtHet = bll.LayThuocSapHet(nguongTon);
                dgvSapHet.DataSource = dtHet;
                grpSapHet.Text = $"Thuốc sắp hết hàng (tồn < {nguongTon}): {dtHet.Rows.Count} thuốc";

                DataTable dtHan = bll.LayLoSapHetHan(soNgay, true);
                dangNapCanhBao = true;
                try { dgvSapHetHan.DataSource = dtHan; KhoiPhucCheckbox(); }
                finally { dangNapCanhBao = false; }
                CapNhatTieuDeSapHetHan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu cảnh báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiPhucCheckbox()
        {
            bool previous = dangNapCanhBao;
            dangNapCanhBao = true;
            try
            {
            foreach (DataGridViewRow r in dgvSapHetHan.Rows)
            {
                if (r.IsNewRow) continue;
                string key = LayKeyLo(r);
                r.Cells[colDaXuLy.Index].Value = LoXuLyDaXuLy.Contains(key);
            }
            }
            finally { dangNapCanhBao = previous; }
            TauMauSapHetHan();
        }

        private void CapNhatTieuDeSapHetHan()
        {
            int chuaXuLy = 0;
            int total = 0;
            foreach (DataGridViewRow r in dgvSapHetHan.Rows)
            {
                if (r.IsNewRow) continue;
                total++;
                if (r.Cells[colDaXuLy.Index].Value is not true) chuaXuLy++;
            }
            int soNgay = (int)nudSoNgay.Value;
            grpSapHetHan.Text = $"Lô sắp / đã hết hạn (dưới {soNgay} ngày): {chuaXuLy} lô" +
                (total - chuaXuLy > 0 ? $" (đã xử lý: {total - chuaXuLy})" : "");
        }

        private void TauMauSapHetHan()
        {
            foreach (DataGridViewRow r in dgvSapHetHan.Rows)
            {
                if (r.IsNewRow) continue;
                bool daXuLy = r.Cells[colDaXuLy.Index].Value is true;
                if (daXuLy)
                {
                    r.DefaultCellStyle.BackColor = Color.LightGray;
                    r.DefaultCellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    bool hetHan = Convert.ToInt32(r.Cells["SoNgayConLai"].Value) < 0;
                    r.DefaultCellStyle.BackColor = hetHan ? Color.LightCoral : Color.LemonChiffon;
                    r.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                }
            }
        }

        // Tô màu: đỏ nhạt = hết hàng; vàng nhạt = cảnh báo sắp hết hàng/hạn.
        private void dgvSapHet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dgvSapHet.Rows)
            {
                bool hetHang = Convert.ToInt32(r.Cells["TonKho"].Value) == 0;
                r.DefaultCellStyle.BackColor = hetHang ? Color.LightCoral : Color.LemonChiffon;
            }
        }

        private void dgvSapHetHan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            KhoiPhucCheckbox();
            CapNhatTieuDeSapHetHan();
        }

        private void dgvSapHetHan_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dangNapCanhBao || e.RowIndex < 0 || e.ColumnIndex < 0 || dgvSapHetHan.Columns[e.ColumnIndex] != colDaXuLy) return;
            var row = dgvSapHetHan.Rows[e.RowIndex];
            string key = LayKeyLo(row);
            bool daXuLy = row.Cells[colDaXuLy.Index].Value is true;
            bool changed = daXuLy ? LoXuLyDaXuLy.Add(key) : LoXuLyDaXuLy.Remove(key);
            if (!changed) return;
            TauMauSapHetHan();
            CapNhatTieuDeSapHetHan();
            // Thông báo thay đổi để trang chủ cập nhật
            Store.DAL.DBConnection.NotifyDataChanged();
        }

        private void dgvSapHetHan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private static string LayKeyLo(DataGridViewRow row)
        {
            int maThuoc = row.Cells["MaThuoc"].Value == null ? 0 : Convert.ToInt32(row.Cells["MaThuoc"].Value);
            string soLo = row.Cells["SoLo"].Value?.ToString() ?? "";
            return maThuoc + "|" + soLo;
        }

        // Tập các lô đã được đánh dấu "đã xử lý" (key = "MaThuoc|SoLo"), dùng static để chia sẻ với frmMain
        internal static readonly HashSet<string> LoXuLyDaXuLy = new HashSet<string>();
    }
}
