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
        }

        private void frmCanhBao_Load(object sender, EventArgs e)
        {
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

                CauHinhCot();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu cảnh báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CauHinhCot()
        {
            if (dgvSapHet.Columns.Count > 0)
            {
                dgvSapHet.Columns["MaThuoc"]!.HeaderText = "Mã";
                dgvSapHet.Columns["TenThuoc"]!.HeaderText = "Tên thuốc";
                dgvSapHet.Columns["DonViTinh"]!.HeaderText = "ĐVT";
                dgvSapHet.Columns["NhaCungCap"]!.HeaderText = "Nhà cung cấp";
                dgvSapHet.Columns["TonKho"]!.HeaderText = "Tồn kho (còn hạn)";
                dgvSapHet.Columns["MaThuoc"]!.FillWeight = 30;
                dgvSapHet.Columns["TenThuoc"]!.FillWeight = 140;
                dgvSapHet.Columns["DonViTinh"]!.FillWeight = 40;
            }

            if (dgvSapHetHan.Columns.Count > 0)
            {
                dgvSapHetHan.Columns["MaThuoc"]!.HeaderText = "Mã";
                dgvSapHetHan.Columns["TenThuoc"]!.HeaderText = "Tên thuốc";
                dgvSapHetHan.Columns["SoLo"]!.HeaderText = "Số lô";
                dgvSapHetHan.Columns["HanDung"]!.HeaderText = "Hạn dùng";
                dgvSapHetHan.Columns["SoNgayConLai"]!.HeaderText = "Số ngày còn lại";
                dgvSapHetHan.Columns["TonLo"]!.HeaderText = "Tồn của lô";
                dgvSapHetHan.Columns["TrangThai"]!.HeaderText = "Trạng thái";
                dgvSapHetHan.Columns["HanDung"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvSapHetHan.Columns["MaThuoc"]!.FillWeight = 30;
                dgvSapHetHan.Columns["TenThuoc"]!.FillWeight = 140;
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
