using System.Data;
using System.Globalization;
using Store.BLL;
using Store.POS.Common;

namespace Store.POS
{
    public partial class frmBaoCao : Form, ILamMoiDuLieu
    {
        private readonly BaoCaoBLL bll = new BaoCaoBLL();
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");
        private readonly Button btnXuatExcel;

        private DataTable? dtTheoNgayHienTai;
        private DataTable? dtHoaDonHienTai;
        private int soHDHienTai;
        private decimal tongTienHienTai, giamGiaHienTai, doanhThuHienTai;

        public frmBaoCao()
        {
            InitializeComponent();
            PosFormStyle.ConfigureGridSelection(this, posLayout, null);

            btnXuatExcel = new Button
            {
                Name = "btnXuatExcel",
                Text = "Xuất Excel",
                Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold),
                BackColor = Color.FromArgb(22, 101, 52), // Xanh lá đậm chuẩn POS
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 2, 12, 2)
            };
            btnXuatExcel.FlatAppearance.BorderSize = 0;
            btnXuatExcel.Click += (s, e) => XuatExcel();

            tableLayoutPanel3.ColumnCount = 7;
            tableLayoutPanel3.ColumnStyles.Insert(5, new ColumnStyle(SizeType.Absolute, 160F));
            tableLayoutPanel3.Controls.Add(btnXuatExcel, 5, 0);
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

        private void XemBaoCao(bool notifyEmpty = true)
        {
            if (bll.LayBaoCao(dtpTuNgay.Value, dtpDenNgay.Value,
                    out DataTable theoNgay, out DataTable hoaDon,
                    out int soHD, out decimal tongTien, out decimal giamGia, out decimal doanhThu,
                    out string error))
            {
                dtTheoNgayHienTai = theoNgay;
                dtHoaDonHienTai = hoaDon;
                soHDHienTai = soHD;
                tongTienHienTai = tongTien;
                giamGiaHienTai = giamGia;
                doanhThuHienTai = doanhThu;

                dgvTheoNgay.DataSource = theoNgay;
                dgvHoaDon.DataSource = hoaDon;

                lblSoHD.Text = soHD.ToString("N0", vn);
                lblTongTien.Text = tongTien.ToString("N0", vn) + " đ";
                lblGiamGia.Text = giamGia.ToString("N0", vn) + " đ";
                lblDoanhThu.Text = doanhThu.ToString("N0", vn) + " đ";

                if (notifyEmpty && soHD == 0)
                    MessageBox.Show("Không có hóa đơn nào trong khoảng ngày đã chọn.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void XuatExcel()
        {
            if (dtTheoNgayHienTai == null || dtHoaDonHienTai == null || soHDHienTai == 0)
            {
                MessageBox.Show("Hiện không có dữ liệu báo cáo để xuất Excel. Hãy chọn khoảng thời gian và bấm 'Xem báo cáo' trước.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                ExcelHelper.XuatBaoCaoDoanhThu(dtpTuNgay.Value, dtpDenNgay.Value,
                    soHDHienTai, tongTienHienTai, giamGiaHienTai, doanhThuHienTai,
                    dtTheoNgayHienTai, dtHoaDonHienTai);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
