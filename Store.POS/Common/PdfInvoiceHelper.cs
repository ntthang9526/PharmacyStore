using System.Data;
using System.Globalization;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Store.POS.Common;

internal static class PdfInvoiceHelper
{
    private static readonly CultureInfo vn=CultureInfo.GetCultureInfo("vi-VN");
    static PdfInvoiceHelper() => QuestPDF.Settings.License=LicenseType.Community;
    public static async Task HoiInSauThanhToanAsync(Form owner,int orderId,decimal amount)
    {
        if(owner.IsDisposed || owner.Disposing) return;
        if(MessageBox.Show(owner,$"Đã thanh toán HD{orderId:D5} ({amount:N0} đ).\nBạn có muốn in hóa đơn không?",
            "Thanh toán",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)!=DialogResult.Yes) return;
        DataSet? data=null;
        bool loaded=await PosBackground.LoadAsync(owner,()=>new Store.BLL.HoaDonBLL().LayBanIn(orderId),
            result=>data=result,error=>MessageBox.Show(owner,"Hóa đơn đã thanh toán nhưng chưa in được: "+error.Message,"In hóa đơn"));
        if(!loaded || owner.IsDisposed || owner.Disposing) return;
        try {XuatHoaDon(data!,owner);}
        catch(Exception ex) {MessageBox.Show(owner,"Hóa đơn đã thanh toán nhưng chưa in được: "+ex.Message,"In hóa đơn");}
    }

    public static void XuatHoaDon(DataSet data,IWin32Window owner)
    {
        byte[] pdf=TaoBanIn(data);int id=(int)data.Tables[0].Rows[0]["MaHD"];
        using var save=new SaveFileDialog {Filter="PDF (*.pdf)|*.pdf",DefaultExt="pdf",AddExtension=true,FileName=$"HoaDon_HD{id:D5}.pdf",Title="Lưu / in lại hóa đơn",OverwritePrompt=true};
        if(save.ShowDialog(owner)!=DialogResult.OK) return;
        File.WriteAllBytes(save.FileName,pdf);
        if(MessageBox.Show(owner,"Đã lưu file PDF. Mở file để xem hoặc in?","Xuất hóa đơn",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
        {
            try {Process.Start(new ProcessStartInfo(save.FileName){UseShellExecute=true});}
            catch(Exception ex) {MessageBox.Show(owner,"Đã lưu tại "+save.FileName+"\nKhông mở được file PDF: "+ex.Message,"Mở hóa đơn");}
        }
    }
    public static byte[] TaoBanIn(DataSet data)
    {
        if(data.Tables.Count<2 || data.Tables[0].Rows.Count!=1 || data.Tables[1].Rows.Count==0) throw new ArgumentException("Thiếu dữ liệu hóa đơn.");
        DataRow bill=data.Tables[0].Rows[0];
        if(Convert.ToString(bill["PaymentStatus"])!="PAID") throw new InvalidOperationException("Chỉ in hóa đơn đã thanh toán.");
        decimal total=Convert.ToDecimal(bill["TongTien"]),discount=Convert.ToDecimal(bill["GiamGia"]),received=Convert.ToDecimal(bill["ThucThu"]);
        if(data.Tables[1].Rows.Cast<DataRow>().Sum(row=>Convert.ToDecimal(row["ThanhTien"]))!=total || total-discount!=received)
            throw new InvalidOperationException("Tổng tiền không khớp chi tiết hóa đơn. Chưa xuất PDF; hãy kiểm tra dữ liệu chứng từ.");
        string Text(string column)=>Convert.ToString(bill[column])??"";
        string Money(string column)=>Convert.ToDecimal(bill[column]).ToString("N0",vn)+" đ";
        return Document.Create(container=>container.Page(page=>
        {
            page.Size(PageSizes.A5);page.Margin(22);page.DefaultTextStyle(x=>x.FontFamily("Arial").FontSize(9));
            page.Header().PaddingBottom(10).Column(col=>
            {
                col.Item().Text("PHARMACY").AlignCenter().Bold().FontSize(14).FontColor("#0F766E");
                col.Item().Text("HÓA ĐƠN BÁN THUỐC").AlignCenter().Bold().FontSize(12);
                col.Item().Text("ĐÃ THANH TOÁN · HD"+((int)bill["MaHD"]).ToString("D5")).AlignCenter();
            });
            page.Content().Column(col=>
            {
                col.Spacing(5);
                col.Item().Text($"Ngày lập: {(DateTime)bill["NgayLap"]:dd/MM/yyyy HH:mm}");
                if(!bill.IsNull("NgayThanhToan")) col.Item().Text($"Ngày thu: {(DateTime)bill["NgayThanhToan"]:dd/MM/yyyy HH:mm}");
                col.Item().Text("Khách hàng: "+Text("KhachHang"));
                col.Item().Text("NV lập: "+Text("NhanVien")+" · NV thu: "+Text("NhanVienThuTien"));
                col.Item().Text("Hình thức: "+Text("HinhThucThanhToan"));
                if(!bill.IsNull("PrescriptionNumber")) col.Item().Background("#F0FDFA").Padding(6).Column(rx=>
                {
                    rx.Item().Text("Đơn thuốc: "+Text("PrescriptionNumber")).Bold();
                    rx.Item().Text("Người bệnh: "+Text("PatientName"));
                    rx.Item().Text("Người kê: "+Text("Prescriber")+$" · Ngày: {bill["PrescriptionDate"]:dd/MM/yyyy}");
                });
                col.Item().Table(table=>
                {
                    table.ColumnsDefinition(c=>{c.RelativeColumn(3);c.RelativeColumn(1);c.RelativeColumn(1);c.RelativeColumn(1.6f);c.RelativeColumn(1.8f);});
                    table.Header(h=>{foreach(string title in new[]{"Thuốc / Số lô","ĐVT","SL","Đơn giá","Thành tiền"}) h.Cell().Background("#0F766E").Padding(4).Text(title).FontColor(Colors.White).Bold();});
                    foreach(DataRow row in data.Tables[1].Rows)
                    {
                        string name=Convert.ToString(row["TenThuoc"])??"";
                        if(Convert.ToBoolean(row["KeDon"])) name+=" [Kê đơn]";
                        table.Cell().BorderBottom(0.5f).BorderColor("#CBD5E1").Padding(4).Text(name+"\nLô: "+row["SoLo"]);
                        table.Cell().Padding(4).Text(Convert.ToString(row["DonViTinh"]));
                        table.Cell().Padding(4).Text(Convert.ToString(row["SoLuong"])).AlignRight();
                        table.Cell().Padding(4).Text(Convert.ToDecimal(row["DonGia"]).ToString("N0",vn)).AlignRight();
                        table.Cell().Padding(4).Text(Convert.ToDecimal(row["ThanhTien"]).ToString("N0",vn)).AlignRight();
                        string dosage=Convert.ToString(row["HuongDan"])??"";
                        if(dosage.Length>0) table.Cell().ColumnSpan(5).Padding(4).ShowEntire().Text("Hướng dẫn ("+row["TenThuoc"]+"): "+dosage).Italic();
                    }
                });
                col.Item().Text("Tiền hàng: "+Money("TongTien")).AlignRight();
                col.Item().Text("Giảm giá: "+Money("GiamGia")+(Text("Voucher").Length>0?" · "+Text("Voucher"):"")).AlignRight();
                col.Item().Text("ĐÃ THU: "+Money("ThucThu")).AlignRight().Bold().FontSize(12);
                if(bill.Table.Columns.Contains("NguonDauHoaDon") && Convert.ToString(bill["NguonDauHoaDon"])!.StartsWith("Chưa"))
                    col.Item().Text("Đầu hóa đơn cũ chưa đủ bản lưu tên lúc giao dịch; phần thiếu hiển thị theo danh mục hiện tại.").FontSize(8).FontColor(Colors.Grey.Darken1);
                if(data.Tables[1].Rows.Cast<DataRow>().Any(r=>Convert.ToString(r["NguonThongTin"])!.StartsWith("Chưa có lịch sử")))
                    col.Item().Text("Chứng từ cũ: một số thông tin hiển thị theo danh mục hiện tại do chưa có bản lưu lịch sử.").FontSize(8).FontColor(Colors.Grey.Darken1);
            });
            page.Footer().PaddingTop(10).Text(t=>{t.AlignCenter();t.Span("Cảm ơn quý khách! · Trang ");t.CurrentPageNumber();t.Span(" / ");t.TotalPages();});
        })).GeneratePdf();
    }
}
