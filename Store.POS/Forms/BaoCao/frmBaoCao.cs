using System.Data;
using System.Globalization;
using Store.BLL;

namespace Store.POS
{
    public partial class frmBaoCao : Form, ILamMoiDuLieu
    {
        private readonly BaoCaoBLL bll = new BaoCaoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");

        public frmBaoCao()
        {
            InitializeComponent();
            btnXuatExcel.Click+=(_,_)=>XuatExcel();
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            DateTime homNay = DateTime.Today;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);   // Mặc định: từ đầu tháng
            dtpDenNgay.Value = homNay;
            XemBaoCao(false);
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            XemBaoCao();
        }

        private bool XemBaoCao(bool notifyEmpty=true)
        {
            if (bll.LayBaoCao(dtpTuNgay.Value, dtpDenNgay.Value,
                    out DataTable theoNgay, out DataTable hoaDon,
                    out int soHD, out decimal tongTien, out decimal giamGia, out decimal doanhThu,
                    out string error))
            {
                dgvTheoNgay.DataSource = theoNgay;
                dgvHoaDon.DataSource = hoaDon;

                lblSoHD.Text = soHD.ToString("N0", vn);
                lblTongTien.Text = tongTien.ToString("N0", vn) + " đ";
                lblGiamGia.Text = giamGia.ToString("N0", vn) + " đ";
                lblDoanhThu.Text = doanhThu.ToString("N0", vn) + " đ";

                if (notifyEmpty && soHD == 0)
                    MessageBox.Show("Không có hóa đơn nào trong khoảng ngày đã chọn.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                dgvTheoNgay.DataSource = null;
                dgvHoaDon.DataSource = null;
                lblSoHD.Text = lblTongTien.Text = lblGiamGia.Text = lblDoanhThu.Text = "—";
                MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

    }
}
