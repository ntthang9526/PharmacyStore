using System.Data;
using System.Diagnostics;
using ClosedXML.Excel;

namespace Store.POS.Common
{
    public static class ExcelHelper
    {
        private static bool DocSoTien(IXLCell cell,out decimal value)
        {
            value=0;
            if(cell.DataType==XLDataType.Number) return cell.TryGetValue(out value);
            return decimal.TryParse(cell.GetString().Trim(),System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.CurrentCulture,out value);
        }

        private static readonly XLColor PrimaryColor = XLColor.FromHtml("#166534"); // Xanh lá dược phẩm đậm
        private static readonly XLColor HeaderBgColor = XLColor.FromHtml("#0F766E"); // Teal thanh lịch
        private static readonly XLColor AlternateRowColor = XLColor.FromHtml("#F8FAFC");

        /// <summary>
        /// Mở SaveFileDialog để lấy đường dẫn lưu file Excel
        /// </summary>
        public static string? ChonNoiLuuFile(string tenFileMacDinh)
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = tenFileMacDinh,
                Title = "Chọn nơi lưu file Excel"
            };

            return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
        }

        /// <summary>
        /// Hỏi mở file sau khi xuất thành công
        /// </summary>
        public static void ThongBaoVaMoFile(string duongDan, string thongDiep = "Đã xuất file Excel.")
        {
            var dr = MessageBox.Show($"{thongDiep}\nMở file ngay?", "Thành công",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = duongDan,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không mở được file: " + ex.Message, "Lỗi mở file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Xuất Báo cáo tổng hợp doanh thu ra Excel
        /// </summary>
        public static void XuatBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay,
            int soHD, decimal tongTien, decimal giamGia, decimal doanhThu,
            DataTable dtTheoNgay, DataTable dtHoaDon,string? outputPath=null,bool notify=true)
        {
            string tenMacDinh = $"BaoCaoDoanhThu_{tuNgay:yyyyMMdd}_{denNgay:yyyyMMdd}.xlsx";
            string? duongDan = outputPath ?? ChonNoiLuuFile(tenMacDinh);
            if (string.IsNullOrEmpty(duongDan)) return;

            using var workbook = new XLWorkbook();

            // Sheet 1: Tổng quan & Doanh thu theo ngày
            var ws1 = workbook.Worksheets.Add("Doanh thu theo ngày");
            ws1.ShowGridLines = true;

            // Tiêu đề
            ws1.Cell("A1").Value = "BÁO CÁO DOANH THU NHÀ THUỐC";
            ws1.Range("A1:E1").Merge().Style.Font.SetBold().Font.SetFontSize(16).Font.SetFontColor(PrimaryColor);
            ws1.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            ws1.Cell("A2").Value = $"Từ ngày: {tuNgay:dd/MM/yyyy}  -  Đến ngày: {denNgay:dd/MM/yyyy} (Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm})";
            ws1.Range("A2:E2").Merge().Style.Font.SetItalic().Font.SetFontSize(11).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Bảng tóm tắt chỉ số
            int rowIdx = 4;
            ws1.Cell(rowIdx, 1).Value = "TỔNG QUAN CHỈ SỐ";
            ws1.Range(rowIdx, 1, rowIdx, 4).Merge().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.FromHtml("#E2E8F0"));

            rowIdx++;
            ws1.Cell(rowIdx, 1).Value = "Tổng số hóa đơn:";
            ws1.Cell(rowIdx, 2).Value = soHD;
            ws1.Cell(rowIdx, 2).Style.Font.SetBold().NumberFormat.Format = "#,##0";

            ws1.Cell(rowIdx, 3).Value = "Tổng tiền hàng:";
            ws1.Cell(rowIdx, 4).Value = tongTien;
            ws1.Cell(rowIdx, 4).Style.Font.SetBold().NumberFormat.Format = "#,##0\" đ\"";

            rowIdx++;
            ws1.Cell(rowIdx, 1).Value = "Tổng giảm giá:";
            ws1.Cell(rowIdx, 2).Value = giamGia;
            ws1.Cell(rowIdx, 2).Style.Font.SetBold().NumberFormat.Format = "#,##0\" đ\"";

            ws1.Cell(rowIdx, 3).Value = "THỰC THU (DOANH THU):";
            ws1.Cell(rowIdx, 4).Value = doanhThu;
            ws1.Cell(rowIdx, 4).Style.Font.SetBold().Font.SetFontColor(XLColor.Red).NumberFormat.Format = "#,##0\" đ\"";

            // Bảng dữ liệu theo ngày
            rowIdx += 2;
            ws1.Cell(rowIdx, 1).Value = "CHI TIẾT THEO NGÀY";
            ws1.Range(rowIdx, 1, rowIdx, 5).Merge().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.FromHtml("#E2E8F0"));

            rowIdx++;
            string[] headersTheoNgay = { "Ngày bán", "Số hóa đơn", "Tiền hàng", "Giảm giá", "Doanh thu" };
            for (int col = 0; col < headersTheoNgay.Length; col++)
            {
                var cell = ws1.Cell(rowIdx, col + 1);
                cell.Value = headersTheoNgay[col];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(HeaderBgColor);
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            int startDataRow = rowIdx + 1;
            foreach (DataRow r in dtTheoNgay.Rows)
            {
                rowIdx++;
                ws1.Cell(rowIdx, 1).Value = r.Table.Columns.Contains("Ngay") && r["Ngay"] != DBNull.Value 
                    ? Convert.ToDateTime(r["Ngay"]).ToString("dd/MM/yyyy") : "";
                ws1.Cell(rowIdx, 2).Value = r.Table.Columns.Contains("SoHoaDon") ? Convert.ToInt32(r["SoHoaDon"]) : (r.Table.Columns.Contains("SoHD") ? Convert.ToInt32(r["SoHD"]) : 0);
                ws1.Cell(rowIdx, 3).Value = Convert.ToDecimal(r["TongTien"]);
                ws1.Cell(rowIdx, 4).Value = Convert.ToDecimal(r["GiamGia"]);
                ws1.Cell(rowIdx, 5).Value = Convert.ToDecimal(r["DoanhThu"]);

                ws1.Cell(rowIdx, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws1.Cell(rowIdx, 2).Style.NumberFormat.Format = "#,##0";
                ws1.Cell(rowIdx, 3).Style.NumberFormat.Format = "#,##0\" đ\"";
                ws1.Cell(rowIdx, 4).Style.NumberFormat.Format = "#,##0\" đ\"";
                ws1.Cell(rowIdx, 5).Style.NumberFormat.Format = "#,##0\" đ\"";
            }

            if (rowIdx >= startDataRow)
            {
                var rangeData = ws1.Range(startDataRow - 1, 1, rowIdx, 5);
                rangeData.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                rangeData.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }

            ws1.Columns().AdjustToContents();

            // Sheet 2: Danh sách hóa đơn chi tiết
            var ws2 = workbook.Worksheets.Add("Danh sách hóa đơn");
            ws2.ShowGridLines = true;

            ws2.Cell("A1").Value = "DANH SÁCH HÓA ĐƠN BÁN HÀNG";
            ws2.Range("A1:I1").Merge().Style.Font.SetBold().Font.SetFontSize(15).Font.SetFontColor(PrimaryColor);
            ws2.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            int rIdx2 = 3;
            string[] headersHD = { "Mã HĐ", "Ngày thanh toán", "Khách hàng", "NV lập", "Tiền hàng", "Giảm giá", "Thực thu", "Hình thức thanh toán", "NV thu" };
            for (int col = 0; col < headersHD.Length; col++)
            {
                var cell = ws2.Cell(rIdx2, col + 1);
                cell.Value = headersHD[col];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(HeaderBgColor);
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            int startRow2 = rIdx2 + 1;
            foreach (DataRow r in dtHoaDon.Rows)
            {
                rIdx2++;
                ws2.Cell(rIdx2, 1).Value = "HD" + Convert.ToInt32(r["MaHD"]).ToString("D5");
                var ngayObj = r.Table.Columns.Contains("NgayLap") ? r["NgayLap"] : (r.Table.Columns.Contains("NgayTao") ? r["NgayTao"] : null);
                ws2.Cell(rIdx2, 2).Value = (ngayObj != null && ngayObj != DBNull.Value) ? Convert.ToDateTime(ngayObj).ToString("dd/MM/yyyy HH:mm") : "";
                ws2.Cell(rIdx2, 3).Value = r.Table.Columns.Contains("KhachHang") ? (r["KhachHang"]?.ToString() ?? "") : "";
                ws2.Cell(rIdx2, 4).Value = r["NhanVien"]?.ToString() ?? "";
                ws2.Cell(rIdx2, 5).Value = Convert.ToDecimal(r["TongTien"]);
                ws2.Cell(rIdx2, 6).Value = Convert.ToDecimal(r["GiamGia"]);
                ws2.Cell(rIdx2, 7).Value = Convert.ToDecimal(r["ThucThu"]);
                ws2.Cell(rIdx2,8).Value=r["HinhThucThanhToan"]?.ToString()??"";
                ws2.Cell(rIdx2,9).Value=Convert.ToString(r["NhanVienThuTien"]);

                ws2.Cell(rIdx2, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws2.Cell(rIdx2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws2.Cell(rIdx2, 5).Style.NumberFormat.Format = "#,##0\" đ\"";
                ws2.Cell(rIdx2, 6).Style.NumberFormat.Format = "#,##0\" đ\"";
                ws2.Cell(rIdx2, 7).Style.NumberFormat.Format = "#,##0\" đ\"";
            }

            if (rIdx2 >= startRow2)
            {
                var rangeData2 = ws2.Range(startRow2 - 1, 1, rIdx2, 9);
                rangeData2.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                rangeData2.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }

            ws2.Columns().AdjustToContents();

            workbook.SaveAs(duongDan);
            if(notify) ThongBaoVaMoFile(duongDan, "Đã xuất báo cáo Excel.");
        }

        /// <summary>
        /// Xuất danh sách hóa đơn theo kết quả tìm kiếm ra Excel
        /// </summary>
        public static void XuatDanhSachHoaDon(DataTable data,DateTime? from,DateTime? to)
        {
            string? path=ChonNoiLuuFile("DanhSachHoaDon.xlsx");if(path==null) return;
            ExcelTable.Luu(path,"DANH SÁCH HÓA ĐƠN",data,("MaHD","Mã HĐ"),("NgayLap","Ngày lập"),("KhachHang","Khách hàng"),
                ("NhanVien","NV lập"),("NhanVienThuTien","NV thu tiền"),("TrangThai","Trạng thái"),("HinhThucThanhToan","Hình thức thanh toán"),
                ("TongTien","Tiền hàng"),("GiamGia","Giảm giá"),("GiaTriHoaDon","Giá trị hóa đơn"),("ThucThu","Đã thu"),("CanThanhToan","Còn phải thu"),
                ("ModifiedAt","Sửa lúc"),("NhanVienSua","NV sửa"),("CancelledAt","Hủy lúc"),("NhanVienHuy","NV hủy"),("CancelReason","Lý do hủy"),("NguonDauHoaDon","Nguồn thông tin đầu hóa đơn"));ThongBaoVaMoFile(path);
        }

        public static void XuatChiTietMotHoaDon(int maHD, string thongTinKhach, string nhanVien, DateTime ngayLap,
            decimal tongTien, decimal giamGia, decimal thucThu, DataTable dtChiTiet,string status="Đã thanh toán",string method="Chưa xác định",string? outputPath=null,bool notify=true,string? headerSource=null)
        {
            string tenMacDinh = $"HoaDon_HD{maHD:D5}.xlsx";
            string? duongDan = outputPath ?? ChonNoiLuuFile(tenMacDinh);
            if (string.IsNullOrEmpty(duongDan)) return;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add($"HD{maHD:D5}");
            ws.ShowGridLines = true;

            // Thông tin nhà thuốc
            ws.Cell("A1").Value = "PHARMACY";
            ws.Range("A1:F1").Merge().Style.Font.SetBold().Font.SetFontSize(16).Font.SetFontColor(PrimaryColor);
            ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            ws.Cell("A2").Value = "HÓA ĐƠN BÁN THUỐC · "+status+" · "+method;
            ws.Range("A2:F2").Merge().Style.Font.SetBold().Font.SetFontSize(13);
            ws.Cell("A2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Thông tin hóa đơn
            int rIdx = 4;
            ws.Cell(rIdx, 1).Value = "Mã hóa đơn:";
            ws.Cell(rIdx, 2).Value = $"HD{maHD:D5}";
            ws.Cell(rIdx, 2).Style.Font.SetBold();

            ws.Cell(rIdx, 4).Value = "Ngày lập:";
            ws.Cell(rIdx, 5).Value = ngayLap.ToString("dd/MM/yyyy HH:mm");

            rIdx++;
            ws.Cell(rIdx, 1).Value = "Khách hàng:";
            ws.Cell(rIdx, 2).Value = thongTinKhach;

            ws.Cell(rIdx, 4).Value = "Nhân viên:";
            ws.Cell(rIdx, 5).Value = nhanVien;

            // Bảng danh sách mặt hàng
            rIdx += 2;
            string[] headers = { "STT", "Tên thuốc", "Số lô", "ĐVT", "SL", "Đơn giá", "Thành tiền", "Nguồn tên/đơn vị", "Hướng dẫn sử dụng" };
            for (int col = 0; col < headers.Length; col++)
            {
                var cell = ws.Cell(rIdx, col + 1);
                cell.Value = headers[col];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(HeaderBgColor);
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            int startRow = rIdx + 1;
            int stt = 1;

            foreach (DataRow r in dtChiTiet.Rows)
            {
                rIdx++;
                ws.Cell(rIdx, 1).Value = stt++;
                ws.Cell(rIdx, 2).Value = r.Table.Columns.Contains("TenThuoc") ? r["TenThuoc"]?.ToString() : (r.Table.Columns.Contains("Thuoc") ? r["Thuoc"]?.ToString() : "");
                ws.Cell(rIdx, 3).Value = r.Table.Columns.Contains("SoLo") ? r["SoLo"]?.ToString() : "";
                ws.Cell(rIdx, 4).Value = r.Table.Columns.Contains("DonViTinh") ? r["DonViTinh"]?.ToString() : r.Table.Columns.Contains("DonVi") ? r["DonVi"]?.ToString() : (r.Table.Columns.Contains("DVT") ? r["DVT"]?.ToString() : "");
                ws.Cell(rIdx, 5).Value = Convert.ToInt32(r["SoLuong"]);
                ws.Cell(rIdx, 6).Value = Convert.ToDecimal(r["DonGia"]);
                ws.Cell(rIdx, 7).Value = Convert.ToDecimal(r["ThanhTien"]);
                ws.Cell(rIdx, 8).Value = r.Table.Columns.Contains("NguonThongTin") ? Convert.ToString(r["NguonThongTin"]) : "Chưa xác định nguồn";
                ws.Cell(rIdx,9).Value=r.Table.Columns.Contains("HuongDan")?Convert.ToString(r["HuongDan"]):"";
                ws.Cell(rIdx,9).Style.Alignment.WrapText=true;

                ws.Cell(rIdx, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell(rIdx, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell(rIdx, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                ws.Cell(rIdx, 5).Style.NumberFormat.Format = "#,##0";
                ws.Cell(rIdx, 6).Style.NumberFormat.Format = "#,##0\" đ\"";
                ws.Cell(rIdx, 7).Style.NumberFormat.Format = "#,##0\" đ\"";
            }

            // Dòng tổng tiền
            rIdx++;
            ws.Cell(rIdx, 1).Value = "Tổng tiền hàng:";
            ws.Range(rIdx, 1, rIdx, 6).Merge().Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            ws.Cell(rIdx, 7).Value = tongTien;
            ws.Cell(rIdx, 7).Style.Font.SetBold().NumberFormat.Format = "#,##0\" đ\"";

            rIdx++;
            ws.Cell(rIdx, 1).Value = "Giảm giá (Voucher):";
            ws.Range(rIdx, 1, rIdx, 6).Merge().Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            ws.Cell(rIdx, 7).Value = giamGia;
            ws.Cell(rIdx, 7).Style.Font.SetBold().NumberFormat.Format = "#,##0\" đ\"";

            rIdx++;
            ws.Cell(rIdx, 1).Value = "THỰC THU:";
            ws.Range(rIdx, 1, rIdx, 6).Merge().Style.Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            ws.Cell(rIdx, 7).Value = thucThu;
            ws.Cell(rIdx, 7).Style.Font.SetBold().Font.SetFontColor(XLColor.Red).NumberFormat.Format = "#,##0\" đ\"";

            if (rIdx >= startRow)
            {
                var rangeData = ws.Range(startRow - 1, 1, rIdx, 9);
                rangeData.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                rangeData.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }

            // Lời cảm ơn
            rIdx += 2;
            ws.Cell(rIdx, 1).Value = "Cảm ơn quý khách và hẹn gặp lại!";
            ws.Range(rIdx, 1, rIdx, 7).Merge().Style.Font.SetItalic().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            ws.Columns().AdjustToContents();
            if(!string.IsNullOrWhiteSpace(headerSource)) {rIdx+=2;ws.Cell(rIdx,1).Value="Thông tin đầu hóa đơn: "+headerSource;ws.Range(rIdx,1,rIdx,9).Merge().Style.Alignment.WrapText=true;ws.Row(rIdx).Height=32;}
            workbook.SaveAs(duongDan);
            if(notify) ThongBaoVaMoFile(duongDan, $"Đã xuất HD{maHD:D5} ra Excel.");
        }

        /// <summary>
        /// Mở OpenFileDialog để chọn file Excel cần đọc
        /// </summary>
        public static string? ChonFileExcelDeDoc()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
                Title = "Chọn file Excel cần nhập dữ liệu"
            };
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
        }

        /// <summary>
        /// Xuất toàn bộ danh mục thuốc ra Excel
        /// </summary>
        public static void XuatDanhSachThuoc(DataTable data,string? outputPath=null,bool notify=true)
        {
            string? path=outputPath ?? ChonNoiLuuFile("DanhSachThuoc.xlsx");if(path==null) return;
            using var book=new XLWorkbook();var sheet=book.AddWorksheet("Thuốc");
            sheet.Column(2).Style.NumberFormat.Format="@";
            string[] keys={"TenThuoc","MaVach","TenNhom","TenNCC","DonViTinh","DonGia","TrangThaiBan","SoLuongTon","TonCoTheBan","KeDon","HoatChat"};
            string[] headings={"Tên thuốc","Mã vạch","Nhóm thuốc","Nhà cung cấp","Đơn vị tính","Giá bán","Trạng thái bán","Tồn thực tế","Tồn có thể bán","Kê đơn","Hoạt chất"};
            for(int c=0;c<keys.Length;c++) sheet.Cell(1,c+1).Value=headings[c];
            for(int row=0;row<data.Rows.Count;row++) for(int col=0;col<keys.Length;col++)
            {
                object v=data.Rows[row][keys[col]];
                if(v is decimal price) {sheet.Cell(row+2,col+1).Value=price;sheet.Cell(row+2,col+1).Style.NumberFormat.Format="#,##0";}
                else if(v is int count) sheet.Cell(row+2,col+1).Value=count;
                else sheet.Cell(row+2,col+1).Value=v==DBNull.Value?"":v.ToString()??"";
            }
            sheet.Row(1).Style.Font.Bold=true;sheet.SheetView.FreezeRows(1);sheet.Columns().AdjustToContents();
            book.SaveAs(path);if(notify) ThongBaoVaMoFile(path);
        }

        public static void XuatFileMauNhapThuoc(string? outputPath=null,bool notify=true)
        {
            string? duongDan = outputPath ?? ChonNoiLuuFile("MauNhapThuoc.xlsx");
            if (string.IsNullOrEmpty(duongDan)) return;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("NhapThuoc");
            ws.ShowGridLines = true;

            string[] headers = { "Tên thuốc (*)", "Mã vạch (Barcode)", "Nhóm thuốc (*)", "Nhà cung cấp (*)", "Đơn vị tính (*)", "Giá bán (*)", "Kê đơn (0/1)", "Hoạt chất (không bắt buộc)" };
            for (int col = 0; col < headers.Length; col++)
            {
                var cell = ws.Cell(1, col + 1);
                cell.Value = headers[col];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(HeaderBgColor);
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            // Dòng dữ liệu mẫu
            ws.Cell(2, 1).Value = "Panadol Cảm Cúm";
            ws.Column(2).Style.NumberFormat.Format="@";
            ws.Cell(2, 2).Value = "8935001800999";
            ws.Cell(2, 3).Value = "Thuốc giảm đau - hạ sốt";
            ws.Cell(2, 4).Value = "Dược Hậu Giang (DHG)";
            ws.Cell(2, 5).Value = "Hộp";
            ws.Cell(2, 6).Value = 55000;

            ws.Cell(3, 1).Value = "Klamentin 500/125";
            ws.Cell(3, 2).Value = "";
            ws.Cell(3, 3).Value = "Kháng sinh - Kháng viêm";
            ws.Cell(3, 4).Value = "Sanofi Việt Nam";
            ws.Cell(3, 5).Value = "Hộp";
            ws.Cell(3, 6).Value = 180000;
            ws.Cell(2,7).Value=0;ws.Cell(3,7).Value=1;
            ws.Cell(1,2).CreateComment().AddText("Không bắt buộc. Giữ định dạng Text để không mất số 0 ở đầu; tối đa 50 ký tự, không có khoảng trắng.");
            ws.Cell(1,8).CreateComment().AddText("Có thể để trống; nhập đúng hoạt chất theo nhãn thuốc, tối đa 150 ký tự.");
            ws.SheetView.FreezeRows(1);

            ws.Cell(2, 6).Style.NumberFormat.Format = "#,##0";
            ws.Cell(3, 6).Style.NumberFormat.Format = "#,##0";

            var range = ws.Range(1, 1, 3, 8);
            range.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            range.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);

            ws.Columns().AdjustToContents();
            workbook.SaveAs(duongDan);
            if(notify) ThongBaoVaMoFile(duongDan, "Đã tạo file mẫu nhập thuốc.");
        }

        /// <summary>
        /// Đọc file Excel danh mục thuốc để chuẩn bị Import
        /// </summary>
        public static List<MedicineImportRow> DocDanhSachThuocTuExcel(string duongDan, out List<string> errors)
        {
            var result = new List<MedicineImportRow>();
            errors = new List<string>();

            if(new FileInfo(duongDan).Length>20*1024*1024) throw new ArgumentException("File Excel tối đa 20 MB.");
            using var workbook = new XLWorkbook(duongDan);
            var ws = workbook.Worksheets.FirstOrDefault();
            if (ws == null)
            {
                errors.Add("File Excel không có bất kỳ trang tính (worksheet) nào.");
                return result;
            }

            if(!ws.Cell(1,1).GetString().Contains("Tên thuốc",StringComparison.OrdinalIgnoreCase) || !ws.Cell(1,6).GetString().Contains("Giá",StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("File không đúng mẫu. Tải file mẫu ở màn hình Quản lý thuốc.");
            int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;
            if(lastRow>10001) throw new ArgumentException("Mỗi file tối đa 10.000 dòng dữ liệu.");
            if (lastRow < 2)
            {
                errors.Add("File Excel không chứa dòng dữ liệu nào.");
                return result;
            }

            int rxColumn=ws.Row(1).CellsUsed().FirstOrDefault(c=>c.GetString().StartsWith("Kê đơn",StringComparison.OrdinalIgnoreCase))?.Address.ColumnNumber??0;
            int ingredientColumn=ws.Row(1).CellsUsed().FirstOrDefault(c=>c.GetString().StartsWith("Hoạt chất",StringComparison.OrdinalIgnoreCase))?.Address.ColumnNumber??0;
            for (int r = 2; r <= lastRow; r++)
            {
                string tenThuoc = ws.Cell(r, 1).GetString().Trim();
                if (string.IsNullOrWhiteSpace(tenThuoc)) continue; // Bỏ qua dòng trống

                string barcode = ws.Cell(r, 2).GetString().Trim();
                string tenNhom = ws.Cell(r, 3).GetString().Trim();
                string tenNCC = ws.Cell(r, 4).GetString().Trim();
                string dvt = ws.Cell(r, 5).GetString().Trim();
                string giaStr = ws.Cell(r, 6).GetString().Trim();
                string hoatChat=ingredientColumn>0?ws.Cell(r,ingredientColumn).GetString().Trim():"";
                if(hoatChat.Length>150 || hoatChat.Any(char.IsControl)) {errors.Add($"Dòng {r}: Hoạt chất tối đa 150 ký tự, không chứa ký tự điều khiển.");continue;}
                if(barcode.Length>50 || barcode.Any(c=>c<'!' || c>'~')) {errors.Add($"Dòng {r}: Mã vạch tối đa 50 ký tự không dấu, không chứa khoảng trắng hoặc ký tự điều khiển.");continue;}
                string rx=rxColumn>0?ws.Cell(r,rxColumn).GetString().Trim():"0";
                if(rx.Length==0) rx="0";
                if(rx is not ("0" or "1") && !bool.TryParse(rx,out _)) {errors.Add($"Dòng {r}: Kê đơn phải là 0/1 hoặc True/False.");continue;}
                bool keDon=rx=="1" || rx.Equals("True",StringComparison.OrdinalIgnoreCase);

                if (string.IsNullOrWhiteSpace(tenNhom))
                {
                    errors.Add($"Dòng {r}: Tên nhóm thuốc không được để trống.");
                    continue;
                }
                if (string.IsNullOrWhiteSpace(tenNCC))
                {
                    errors.Add($"Dòng {r}: Nhà cung cấp không được để trống.");
                    continue;
                }
                if (string.IsNullOrWhiteSpace(dvt))
                {
                    errors.Add($"Dòng {r}: Đơn vị tính không được để trống.");
                    continue;
                }

                if (!DocSoTien(ws.Cell(r,6),out decimal giaBan) || giaBan < 0)
                {
                    errors.Add($"Dòng {r}: Giá bán '{giaStr}' không hợp lệ.");
                    continue;
                }

                result.Add(new MedicineImportRow
                {
                    RowIndex = r,
                    TenThuoc = tenThuoc,
                    BarCode = string.IsNullOrWhiteSpace(barcode) ? null : barcode,
                    TenNhom = tenNhom,
                    TenNCC = tenNCC,
                    DonViTinh = dvt,
                    GiaBan = giaBan,KeDon=keDon,HoatChat=string.IsNullOrEmpty(hoatChat)?null:hoatChat
                });
            }

            return result;
        }

        /// <summary>
        /// Tạo và xuất file Excel mẫu nhập kho theo Nhà cung cấp
        /// </summary>
        public static void XuatFileMauNhapKho(string tenNCC, DataTable dsThuocNCC,string? outputPath=null,bool notify=true)
        {
            string tenFile = $"MauNhapKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string? duongDan = outputPath ?? ChonNoiLuuFile(tenFile);
            if (string.IsNullOrEmpty(duongDan)) return;

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("NhapKho");
            ws.ShowGridLines = true;

            ws.Cell("A1").Value = $"MẪU PHIẾU NHẬP KHO - NHÀ CUNG CẤP: {tenNCC.ToUpper()}";
            ws.Range("A1:F1").Merge().Style.Font.SetBold().Font.SetFontSize(14).Font.SetFontColor(PrimaryColor);
            ws.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            int rIdx = 3;
            string[] headers = { "Mã thuốc (*)", "Tên thuốc", "Số lô (*)", "Hạn dùng (dd/MM/yyyy) (*)", "Số lượng (*)", "Giá nhập (*)" };
            for (int col = 0; col < headers.Length; col++)
            {
                var cell = ws.Cell(rIdx, col + 1);
                cell.Value = headers[col];
                cell.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                cell.Style.Fill.SetBackgroundColor(HeaderBgColor);
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            int startRow = rIdx + 1;
            DateTime hanDungMacDinh = DateTime.Today.AddYears(1);

            if (dsThuocNCC != null && dsThuocNCC.Rows.Count > 0)
            {
                foreach (DataRow r in dsThuocNCC.Rows)
                {
                    rIdx++;
                    ws.Cell(rIdx, 1).Value = Convert.ToInt32(r["MaThuoc"]);
                    ws.Cell(rIdx, 2).Value = r["TenThuoc"]?.ToString() ?? "";
                    ws.Cell(rIdx, 3).Value = $"LOT-{DateTime.Today.Year}-A";
                    ws.Cell(rIdx, 4).Value = hanDungMacDinh.ToString("dd/MM/yyyy");
                    ws.Cell(rIdx, 5).Value = "";
                    ws.Cell(rIdx, 6).Value = "";

                    ws.Cell(rIdx, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(rIdx, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    ws.Cell(rIdx, 5).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(rIdx, 6).Style.NumberFormat.Format = "#,##0";
                }
            }
            else
            {
                rIdx++;
                ws.Cell(rIdx, 1).Value = 1;
                ws.Cell(rIdx, 2).Value = "Tên thuốc mẫu";
                ws.Cell(rIdx, 3).Value = "LOT01";
                ws.Cell(rIdx, 4).Value = hanDungMacDinh.ToString("dd/MM/yyyy");
                ws.Cell(rIdx, 5).Value = 100;
                ws.Cell(rIdx, 6).Value = 30000;
            }

            if (rIdx >= startRow)
            {
                var range = ws.Range(startRow - 1, 1, rIdx, 6);
                range.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                range.Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(duongDan);
            if(notify) ThongBaoVaMoFile(duongDan, "Đã tạo file mẫu nhập kho.");
        }

        /// <summary>
        /// Đọc dữ liệu từ file Excel phiếu nhập kho
        /// </summary>
        public static List<NhapKhoImportRow> DocDanhSachNhapKhoTuExcel(string duongDan, DataTable dsThuocNCC, out List<string> errors)
        {
            var result = new List<NhapKhoImportRow>();
            errors = new List<string>();

            if(new FileInfo(duongDan).Length>20*1024*1024) throw new ArgumentException("File Excel tối đa 20 MB.");
            using var workbook = new XLWorkbook(duongDan);
            var ws = workbook.Worksheets.FirstOrDefault();
            if (ws == null)
            {
                errors.Add("File Excel không có bất kỳ sheet nào.");
                return result;
            }

            // Tạo từ điển tra cứu thuốc của NCC: MaThuoc -> DataRow và TenThuoc -> DataRow
            var dicMa = new Dictionary<int, DataRow>();
            var dicTen = new Dictionary<string, DataRow>(StringComparer.OrdinalIgnoreCase);
            var duplicateNames=new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataRow r in dsThuocNCC.Rows)
            {
                int ma = Convert.ToInt32(r["MaThuoc"]);
                string ten = r["TenThuoc"]?.ToString()?.Trim() ?? "";
                dicMa[ma] = r;
                if(dicTen.ContainsKey(ten)) duplicateNames.Add(ten);
                if (!string.IsNullOrEmpty(ten) && !dicTen.ContainsKey(ten))
                    dicTen[ten] = r;
            }

            int lastRow = ws.LastRowUsed()?.RowNumber() ?? 0;
            if(lastRow>10001) throw new ArgumentException("Mỗi file tối đa 10.000 dòng dữ liệu.");
            // Tìm dòng header (dòng chứa "Mã thuốc" hoặc bắt đầu từ dòng 1/3)
            int headerRow = 0;
            for (int r = 1; r <= Math.Min(10, lastRow); r++)
            {
                string text = ws.Cell(r, 1).GetString();
                if (text.Contains("Mã thuốc", StringComparison.OrdinalIgnoreCase))
                {
                    headerRow = r;
                    break;
                }
            }

            if(headerRow==0) throw new ArgumentException("Không tìm thấy tiêu đề Mã thuốc. Hãy dùng File mẫu nhập kho.");
            for (int r = headerRow + 1; r <= lastRow; r++)
            {
                string maStr = ws.Cell(r, 1).GetString().Trim();
                string tenStr = ws.Cell(r, 2).GetString().Trim();
                string soLo = ws.Cell(r, 3).GetString().Trim();
                string hanDungStr = ws.Cell(r, 4).GetString().Trim();
                string soLuongStr = ws.Cell(r, 5).GetString().Trim();
                string giaNhapStr = ws.Cell(r, 6).GetString().Trim();

                if (string.IsNullOrWhiteSpace(maStr) && string.IsNullOrWhiteSpace(tenStr)) continue;

                DataRow? thuocRow = null;
                if (int.TryParse(maStr, out int maThuoc) && dicMa.TryGetValue(maThuoc, out var foundMa))
                {
                    thuocRow = foundMa;
                }
                else if (string.IsNullOrWhiteSpace(maStr) && !duplicateNames.Contains(tenStr) && !string.IsNullOrWhiteSpace(tenStr) && dicTen.TryGetValue(tenStr, out var foundTen))
                {
                    thuocRow = foundTen;
                }

                if(thuocRow!=null && !string.IsNullOrWhiteSpace(tenStr) && !string.Equals(tenStr,thuocRow["TenThuoc"].ToString()?.Trim(),StringComparison.OrdinalIgnoreCase))
                {
                    errors.Add($"Dòng {r}: Mã thuốc và tên thuốc không khớp.");continue;
                }
                if (thuocRow == null)
                {
                    errors.Add($"Dòng {r}: Không tìm thấy thuốc (Mã: '{maStr}', Tên: '{tenStr}') thuộc nhà cung cấp hiện tại.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(soLo))
                {
                    errors.Add($"Dòng {r}: Số lô không được để trống.");
                    continue;
                }

                DateTime hanDung;
                if (ws.Cell(r, 4).DataType == XLDataType.DateTime)
                {
                    hanDung = ws.Cell(r, 4).GetDateTime();
                }
                else if (!DateTime.TryParseExact(hanDungStr, new[] { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" },
                         System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out hanDung))
                {
                    if (!DateTime.TryParse(hanDungStr, out hanDung))
                    {
                        errors.Add($"Dòng {r}: Hạn dùng '{hanDungStr}' không đúng định dạng ngày (dd/MM/yyyy).");
                        continue;
                    }
                }

                if (hanDung.Date <= DateTime.Today)
                {
                    errors.Add($"Dòng {r}: Hạn dùng ({hanDung:dd/MM/yyyy}) phải sau ngày hôm nay.");
                    continue;
                }

                if (!int.TryParse(soLuongStr, out int soLuong) || soLuong <= 0)
                {
                    errors.Add($"Dòng {r}: Số lượng '{soLuongStr}' phải là số nguyên > 0.");
                    continue;
                }

                if (!DocSoTien(ws.Cell(r,6),out decimal giaNhap) || Store.Shared.TienTe.LamTron(giaNhap) <= 0)
                {
                    errors.Add($"Dòng {r}: Giá nhập '{giaNhapStr}' phải > 0.");
                    continue;
                }

                result.Add(new NhapKhoImportRow
                {
                    RowIndex = r,
                    MaThuoc = Convert.ToInt32(thuocRow["MaThuoc"]),
                    TenThuoc = thuocRow["TenThuoc"].ToString() ?? "",
                    SoLo = soLo.ToUpper(),
                    HanDung = hanDung.Date,
                    SoLuong = soLuong,
                    GiaNhap = giaNhap
                });
            }

            return result;
        }
    }

    public class MedicineImportRow
    {
        public int RowIndex { get; set; }
        public string TenThuoc { get; set; } = "";
        public string? BarCode { get; set; }
        public bool KeDon {get;set;}
        public string? HoatChat {get;set;}
        public string TenNhom { get; set; } = "";
        public string TenNCC { get; set; } = "";
        public string DonViTinh { get; set; } = "";
        public decimal GiaBan { get; set; }
    }

    public class NhapKhoImportRow
    {
        public int RowIndex { get; set; }
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; } = "";
        public string SoLo { get; set; } = "";
        public DateTime HanDung { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
    }
}
