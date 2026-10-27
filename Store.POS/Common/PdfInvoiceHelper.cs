using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Store.POS.FormBanHang;
using System.Diagnostics;
using QColor = QuestPDF.Infrastructure.Color;

namespace Store.POS.Common
{
    /// <summary>
    /// Tạo hóa đơn PDF cho phiên bán hàng sử dụng QuestPDF.
    /// </summary>
    internal static class PdfInvoiceHelper
    {
        static PdfInvoiceHelper()
        {
            // Đặt license cộng đồng (miễn phí cho doanh thu < $1M/năm)
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // Màu chủ đạo
        private static readonly QColor PrimaryTeal = QColor.FromHex("#0F766E");
        private static readonly QColor DarkText = QColor.FromHex("#0F172A");

        /// <summary>
        /// Xuất hóa đơn PDF và mở hộp thoại lưu file.
        /// </summary>
        public static void XuatHoaDon(
            int maHoaDon,
            string nhanVien,
            string khachHang,
            DateTime ngayLap,
            IEnumerable<RowData> gioHang,
            decimal tamTinh,
            decimal giamGia,
            decimal tongTien,
            string hinhThucThanhToan,
            string? maVoucher = null)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"HoaDon_{maHoaDon:D5}_{ngayLap:yyyyMMdd}.pdf",
                Title = "Lưu hóa đơn PDF"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var itemList = gioHang.ToList();
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(1.5f, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(c => ComposeContent(c, maHoaDon, nhanVien, khachHang, ngayLap,
                        itemList, tamTinh, giamGia, tongTien, hinhThucThanhToan, maVoucher));
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Cảm ơn quý khách! · Hiệu thuốc ABC · ").FontSize(8).FontColor(Colors.Grey.Medium);
                        x.CurrentPageNumber().FontSize(8).FontColor(Colors.Grey.Medium);
                        x.Span("/").FontSize(8).FontColor(Colors.Grey.Medium);
                        x.TotalPages().FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            });

            document.GeneratePdf(sfd.FileName);

            var dr = MessageBox.Show("Xuất hóa đơn PDF thành công!\nBạn có muốn mở ngay không?",
                "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                try { System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = sfd.FileName, UseShellExecute = true }); }
                catch (Exception ex) { MessageBox.Show("Không mở được file: " + ex.Message, "Lỗi"); }
            }
        }

        private static void ComposeHeader(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().AlignCenter().Text("HIỆU THUỐC ABC")
                    .FontSize(16).Bold().FontColor(PrimaryTeal);
                col.Item().AlignCenter().Text("Địa chỉ: 123 Đường Dược, Q.1, TP.HCM · ĐT: 028.1234.5678")
                    .FontSize(8).FontColor(Colors.Grey.Darken2);
                col.Item().PaddingVertical(4).LineHorizontal(1).LineColor(PrimaryTeal);
                col.Item().AlignCenter().Text("HÓA ĐƠN BÁN HÀNG")
                    .FontSize(13).Bold().FontColor(Colors.Grey.Darken3);
                col.Item().PaddingBottom(4);
            });
        }

        private static void ComposeContent(IContainer container,
            int maHoaDon, string nhanVien, string khachHang, DateTime ngayLap,
            List<RowData> gioHang,
            decimal tamTinh, decimal giamGia, decimal tongTien,
            string hinhThucThanhToan, string? maVoucher)
        {
            container.Column(col =>
            {
                // Thông tin hóa đơn
                col.Item().Table(t =>
                {
                    t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                    t.Cell().Text($"Số HD: HD{maHoaDon:D5}").Bold();
                    t.Cell().AlignRight().Text($"Ngày: {ngayLap:dd/MM/yyyy HH:mm}");
                    t.Cell().Text($"Nhân viên: {nhanVien}");
                    t.Cell().AlignRight().Text($"Khách hàng: {(string.IsNullOrWhiteSpace(khachHang) ? "Khách lẻ" : khachHang)}");
                    if (!string.IsNullOrWhiteSpace(maVoucher))
                    {
                        t.Cell().Text($"Voucher: {maVoucher}");
                        t.Cell().Text("");
                    }
                });

                col.Item().PaddingVertical(6).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);

                // Bảng sản phẩm
                col.Item().Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(24);   // STT
                        c.RelativeColumn(5);    // Tên thuốc
                        c.RelativeColumn(1.5f); // ĐVT
                        c.RelativeColumn(1);    // SL
                        c.RelativeColumn(2.5f); // Đơn giá
                        c.RelativeColumn(2.5f); // Thành tiền
                    });

                    // Header
                    t.Header(h =>
                    {
                        static IContainer HeaderCell(IContainer c) =>
                            c.DefaultTextStyle(x => x.Bold().FontColor(Colors.White))
                             .Background(QColor.FromHex("#0F766E"))
                             .PaddingVertical(4).PaddingHorizontal(4);
                        h.Cell().Element(HeaderCell).Text("STT");
                        h.Cell().Element(HeaderCell).Text("Tên thuốc");
                        h.Cell().Element(HeaderCell).Text("ĐVT");
                        h.Cell().Element(HeaderCell).AlignCenter().Text("SL");
                        h.Cell().Element(HeaderCell).AlignRight().Text("Đơn giá");
                        h.Cell().Element(HeaderCell).AlignRight().Text("Thành tiền");
                    });

                    int stt = 1;
                    foreach (var item in gioHang)
                    {
                        bool alt = stt % 2 == 0;
                        var bg = alt ? QColor.FromHex("#F0FDFA") : Colors.White;
                        IContainer DataCell(IContainer c) => c.Background(bg).PaddingVertical(3).PaddingHorizontal(4);
                        t.Cell().Element(DataCell).Text(stt.ToString());
                        t.Cell().Element(DataCell).Text(item.Name);
                        t.Cell().Element(DataCell).Text(item.Unit);
                        t.Cell().Element(DataCell).AlignCenter().Text(item.Quantity.ToString());
                        t.Cell().Element(DataCell).AlignRight().Text($"{item.Price:N0}");
                        t.Cell().Element(DataCell).AlignRight().Text($"{item.Total:N0}");
                        stt++;
                    }
                });

                col.Item().PaddingVertical(6).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten1);

                // Tổng tiền
                col.Item().Table(t =>
                {
                    t.ColumnsDefinition(c => { c.RelativeColumn(3); c.RelativeColumn(2); });
                    t.Cell().Text("Tạm tính:").AlignRight();
                    t.Cell().AlignRight().Text($"{tamTinh:N0} đ");
                    if (giamGia > 0)
                    {
                        t.Cell().Text("Giảm giá:").AlignRight();
                        t.Cell().AlignRight().Text($"- {giamGia:N0} đ").FontColor(Colors.Red.Medium);
                    }
                    t.Cell().DefaultTextStyle(x => x.Bold().FontSize(11)).Text("TỔNG CỘNG:").AlignRight();
                    t.Cell().DefaultTextStyle(x => x.Bold().FontSize(11)).AlignRight()
                        .Text($"{tongTien:N0} đ").FontColor(PrimaryTeal);
                });

                col.Item().PaddingVertical(4);
                col.Item().Text($"Hình thức thanh toán: {hinhThucThanhToan}").FontSize(8).FontColor(Colors.Grey.Darken2);
                col.Item().PaddingTop(12).AlignCenter().Text("--- Hết ---").FontSize(8).FontColor(Colors.Grey.Medium);
            });
        }
    }
}
