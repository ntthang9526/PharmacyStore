using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;
using Svg;

namespace Store.POS.FormBanHang
{
    public partial class frmThanhToanQR : Form
    {
        private readonly decimal tongTien;

        public frmThanhToanQR() : this(0m) {}

        public frmThanhToanQR(decimal tongTien)
        {
            InitializeComponent();

            this.tongTien = tongTien;

            lblTongTien.Text =
                $"Thanh toán: {tongTien:N0} đ";


            this.Shown += frmThanhToanQR_Shown;
        }

        private async void frmThanhToanQR_Shown(
            object? sender, EventArgs e)
        {
            if (PosFormStyle.IsInDesigner(this)) return;
            btnHoanTat.Enabled = false;

            try
            {
                string nganHang = "970415";
                string soTaiKhoan = "109669936206";
                string chuTaiKhoan = "NGUYEN TRUNG THANG";

                string soTien = tongTien.ToString(
                    "0", CultureInfo.InvariantCulture);

                string noiDung = Uri.EscapeDataString(
                    "THANH TOAN HOA DON");

                string tenTK = Uri.EscapeDataString(
                    chuTaiKhoan);

                // Sử dụng định dạng SVG mới
                string url =
                    "https://img.vietqr.io/image/" +
                    $"{nganHang}-{soTaiKhoan}-compact.svg" +
                    $"?amount={soTien}" +
                    $"&addInfo={noiDung}" +
                    $"&accountName={tenTK}";

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout =
                        TimeSpan.FromSeconds(20);

                    using (HttpResponseMessage response =
                        await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();

                        byte[] data =
                            await response.Content
                                .ReadAsByteArrayAsync();

                        string contentType =
                            response.Content.Headers
                                .ContentType?.MediaType ?? "";

                        Bitmap qrBitmap;

                        if (contentType == "image/svg+xml")
                        {
                            // Render toàn bộ SVG, bao gồm QR
                            using (MemoryStream ms =
                                new MemoryStream(data))
                            {
                                SvgDocument svg =
                                    SvgDocument.Open<SvgDocument>(ms);

                                qrBitmap = svg.Draw();
                            }
                        }
                        else if (contentType == "image/png" ||
                                 contentType == "image/jpeg")
                        {
                            using (MemoryStream ms =
                                new MemoryStream(data))
                            using (Image img =
                                Image.FromStream(ms))
                            {
                                qrBitmap = new Bitmap(img);
                            }
                        }
                        else
                        {
                            throw new Exception(
                                "Định dạng không hỗ trợ: " +
                                contentType);
                        }

                        if (qrBitmap == null)
                        {
                            throw new Exception(
                                "Không tạo được ảnh QR.");
                        }

                        if (this.IsDisposed || picQR.IsDisposed)
                        {
                            qrBitmap.Dispose();
                            return;
                        }

                        Image oldImage = picQR.Image;

                        picQR.Image = qrBitmap;

                        oldImage?.Dispose();

                        btnHoanTat.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.IsDisposed) return;

                MessageBox.Show(
                    "Không tải được mã QR!\n\n" +
                    ex.Message,
                    "Lỗi QR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(
            object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHoanTat_Click(
            object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
