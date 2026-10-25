using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmCanhBao : Form, ILamMoiDuLieu
    {
        private readonly CanhBaoBLL bll = new CanhBaoBLL();

        public frmCanhBao()
        {
            InitializeComponent();
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
                dgvSapHetHan.DataSource = dtHan;
                grpSapHetHan.Text = $"Lô sắp / đã hết hạn (dưới {soNgay} ngày): {dtHan.Rows.Count} lô";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu cảnh báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            foreach (DataGridViewRow r in dgvSapHetHan.Rows)
            {
                r.DefaultCellStyle.BackColor = Convert.ToInt32(r.Cells["SoNgayConLai"].Value) < 0 ? Color.LightCoral : Color.LemonChiffon;
            }
        }
    }
}
