using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.POS.FormBanHang
{
    public partial class frmThanhToanQR : Form
    {
        public frmThanhToanQR(decimal tongTien)
        {
            InitializeComponent();
            lblTongTien.Text = $"Thanh toán: {tongTien:N0} đ";
            picQR.SizeMode = PictureBoxSizeMode.Zoom;

            // Thay bằng thông tin nhận tiền thực tế
            string nganHang = "970415"; // BIN VietinBank
            string soTaiKhoan = "109669936206";
            string chuTaiKhoan = "NGUYEN TRUNG THANG";

            string soTien = tongTien.ToString(
                "0", CultureInfo.InvariantCulture);

            string noiDung = Uri.EscapeDataString(
                $"THANH TOAN HOA DON");

            string url =
                $"https://img.vietqr.io/image/" +
                $"{nganHang}-{soTaiKhoan}-compact2.png" +
                $"?amount={soTien}" +
                $"&addInfo={noiDung}" +
                $"&accountName={Uri.EscapeDataString(chuTaiKhoan)}";

            Shown += (s, e) => picQR.LoadAsync(url);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
