using System.Data;
using System.Globalization;
using Store.BLL;

namespace Store.POS
{
    public partial class frmBaoCao : Form
    {
        private readonly BaoCaoBLL bll = new BaoCaoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            DateTime homNay = DateTime.Today;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);   // Mặc định: từ đầu tháng
            dtpDenNgay.Value = homNay;
            XemBaoCao();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            XemBaoCao();
        }

        private void XemBaoCao()
        {
            if (bll.LayBaoCao(dtpTuNgay.Value, dtpDenNgay.Value,
                    out DataTable theoNgay, out DataTable hoaDon,
                    out int soHD, out decimal tongTien, out decimal giamGia, out decimal doanhThu,
                    out string error))
            {
                dgvTheoNgay.DataSource = theoNgay;
                dgvHoaDon.DataSource = hoaDon;
                CauHinhCot();

                lblSoHD.Text = soHD.ToString("N0", vn);
                lblTongTien.Text = tongTien.ToString("N0", vn) + " đ";
                lblGiamGia.Text = giamGia.ToString("N0", vn) + " đ";
                lblDoanhThu.Text = doanhThu.ToString("N0", vn) + " đ";

                if (soHD == 0)
                    MessageBox.Show("Không có hóa đơn nào trong khoảng ngày đã chọn.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CauHinhCot()
        {
            if (dgvTheoNgay.Columns.Count > 0)
            {
                dgvTheoNgay.Columns["Ngay"]!.HeaderText = "Ngày";
                dgvTheoNgay.Columns["SoHoaDon"]!.HeaderText = "Số HĐ";
                dgvTheoNgay.Columns["TongTien"]!.HeaderText = "Tổng tiền hàng";
                dgvTheoNgay.Columns["GiamGia"]!.HeaderText = "Giảm giá";
                dgvTheoNgay.Columns["DoanhThu"]!.HeaderText = "Doanh thu";
                dgvTheoNgay.Columns["Ngay"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvTheoNgay.Columns["TongTien"]!.DefaultCellStyle.Format = "N0";
                dgvTheoNgay.Columns["GiamGia"]!.DefaultCellStyle.Format = "N0";
                dgvTheoNgay.Columns["DoanhThu"]!.DefaultCellStyle.Format = "N0";
                dgvTheoNgay.Columns["SoHoaDon"]!.FillWeight = 40;
            }

            if (dgvHoaDon.Columns.Count > 0)
            {
                dgvHoaDon.Columns["MaHD"]!.HeaderText = "Mã HĐ";
                dgvHoaDon.Columns["NgayLap"]!.HeaderText = "Ngày lập";
                dgvHoaDon.Columns["NhanVien"]!.HeaderText = "Nhân viên";
                dgvHoaDon.Columns["Voucher"]!.HeaderText = "Voucher";
                dgvHoaDon.Columns["TongTien"]!.HeaderText = "Tổng tiền hàng";
                dgvHoaDon.Columns["GiamGia"]!.HeaderText = "Giảm giá";
                dgvHoaDon.Columns["ThucThu"]!.HeaderText = "Thực thu";
                dgvHoaDon.Columns["NgayLap"]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHoaDon.Columns["TongTien"]!.DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["GiamGia"]!.DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["ThucThu"]!.DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["MaHD"]!.FillWeight = 35;
                dgvHoaDon.Columns["NgayLap"]!.FillWeight = 80;
            }
        }
    }
}
