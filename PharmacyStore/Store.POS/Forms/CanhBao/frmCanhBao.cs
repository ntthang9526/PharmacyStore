using System.Data;
using Store.BLL;

namespace Store.POS
{
    public partial class frmCanhBao : Form
    {
        private readonly CanhBaoBLL bll = new CanhBaoBLL();

        public frmCanhBao()
        {
            InitializeComponent();
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
        }

        private void frmCanhBao_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            LocCanhBao();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocCanhBao();
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

                DataTable dtHan = bll.LayLoSapHetHan(soNgay);
                dgvSapHetHan.DataSource = dtHan;
                grpSapHetHan.Text = $"Lô sắp hết hạn (còn dưới {soNgay} ngày) hoặc đã hết hạn: {dtHan.Rows.Count} lô";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu cảnh báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tô màu: đỏ nhạt = hết hàng / đã hết hạn; vàng nhạt = cảnh báo
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
                bool daHetHan = Convert.ToInt32(r.Cells["SoNgayConLai"].Value) < 0;
                r.DefaultCellStyle.BackColor = daHetHan ? Color.LightCoral : Color.LemonChiffon;
            }
        }
    }
}
