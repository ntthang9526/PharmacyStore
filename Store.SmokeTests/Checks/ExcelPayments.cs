using System.Data;
using ClosedXML.Excel;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.POS.Common;
using Store.Shared;
using Store.Shared.DTOs.Auth;

internal static partial class Program
{
    static void TestExcelPayments()
    {
        string tag="XP"+Guid.NewGuid().ToString("N")[..10];int admin=PhanQuyenDAL.NhanVienId;
        int group=0,supplier=0,med=0,excelMed=0,staff=0,voucher=0,otherVoucher=0;
        var orders=new List<int>();string folder=Path.Combine("artifacts","excel-check-"+tag);Directory.CreateDirectory(folder);
        var catalog=new ThuocBLL();var sales=new BanHangBLL();var invoices=new HoaDonBLL();var imports=new NhapKhoBLL();
        List<CartItem> CartFor(int quantity)=>new(){new(){MaThuoc=med,SoLuong=quantity}};
        List<NhapKhoItem> StockLine(string lot,int quantity,int days)=>new(){new(){MaThuoc=med,TenThuoc=tag,SoLo=lot,HanDung=DateTime.Today.AddDays(days),SoLuong=quantity,GiaNhap=500}};
        int StockFor()=>Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@id AND ExpiryDate>=CAST(GETDATE() AS date)",("@id",med));
        decimal Revenue()=>Convert.ToDecimal(invoices.ThongKeHomNay().Rows[0]["DoanhThu"]);
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out group,out var error),"payment fixture category: "+error);
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"payment fixture supplier");
            Check(catalog.ThemNhanhThuoc(tag,group,supplier,"Viên",1000,out med,out error),"payment fixture medicine");
            Check(new NhanVienBLL().Luu(null,tag,tag.ToLowerInvariant(),"Pass123!","",true,admin,out staff,out error),"payment fixture cashier");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,StockLine(tag+"E",2,2),out _,out _,out error),"early batch imported");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,StockLine(tag+"L",3,30),out _,out _,out error),"late batch imported");
            Check(new VoucherBLL().Luu(null,tag,"FIXED",100,1,true,null,out error),"payment fixture voucher");
            voucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",tag));decimal before=Revenue();
            Check(sales.ThanhToan(admin,CartFor(4),tag,null,3900,out int pending,out _,out _,out error,"Chưa xác định",true),"create pending invoice: "+error);orders.Add(pending);
            Check(StockFor()==5 && Count("SELECT Quantity FROM Vouchers WHERE Id=@id",("@id",voucher))==1 && Revenue()==before,"pending invoice reserves neither stock nor voucher and adds no revenue");
            Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@id",("@id",pending))==0 && Count("SELECT COUNT(*) FROM OrderPendingItems WHERE OrderId=@id",("@id",pending))==1,"pending lines are separate from completed FEFO details");
            using(var main=new frmMain())
            {
                Offscreen(main);
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmHoaDon)).Invoke(main,null);Application.DoEvents();
                var form=(frmHoaDon)UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmHoaDon)];
                UiField<TextBox>(form,"txtTimKiem").Text="HD"+pending.ToString("D5");UiField<Button>(form,"btnTimKiem").PerformClick();
                var grid=UiField<DataGridView>(form,"dgvHoaDon");
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,0)});Application.DoEvents();
                Check(UiField<Button>(form,"btnThanhToanCho").Visible && UiField<ComboBox>(form,"cboHinhThuc").Visible,"pending selection reveals method choice and confirmation");
                foreach(var size in new[]{new Size(1382,753),new Size(1100,700)})
                {
                    main.ClientSize=size;Application.DoEvents();CheckLayout(form,"pending invoice "+size);
                    var label=UiField<Label>(form,"lblChiTiet");
                    int height=TextRenderer.MeasureText(label.Text,label.Font,new Size(label.Width,int.MaxValue),TextFormatFlags.WordBreak).Height;
                    Check(label.Height>=height-2,"selected invoice summary is not vertically clipped "+size);
                    var detailGrid=UiField<DataGridView>(form,"dgvChiTiet");
                    Check(detailGrid.GetRowDisplayRectangle(0,true).Height>=detailGrid.Rows[0].Height-2,"pending invoice has room to read one complete detail row "+size);
                }
                using var picture=new Bitmap(main.Width,main.Height);main.DrawToBitmap(picture,new Rectangle(0,0,picture.Width,picture.Height));picture.Save("artifacts/previews/invoice-pending-theme.png");
                main.Dispose();
            }
            var pendingRow=invoices.TimKiem("HD"+pending.ToString("D5")).Rows[0];
            Check((string)pendingRow["TrangThai"]=="Chờ thanh toán" && (decimal)pendingRow["ThucThu"]==0 && (decimal)pendingRow["CanThanhToan"]==3900,"invoice view distinguishes amount due and collected");
            Check(invoices.ChiTiet(pending).Rows.Count==1 && (string)invoices.ChiTiet(pending).Rows[0]["SoLo"]=="Chưa phân lô","pending invoice detail is readable before assigning batches");
            Check(!invoices.ThanhToanCho(pending,"Tiền mặt",3901,out _) && StockFor()==5,"changed payable amount cannot be confirmed");
            Scalar("UPDATE Orders SET OrderDate=DATEADD(day,-1,GETDATE()) WHERE Id=@id",("@id",pending));
            Check(catalog.SuaThuoc(med.ToString(),tag,group.ToString(),"Viên","2000","0",supplier,out error),"catalog price changed after draft");
            SetTestSession(staff);
            Check(invoices.ThanhToanCho(pending,"Chuyển khoản QR",3900,out error),"staff confirms another employee's pending invoice: "+error);
            Check(StockFor()==1 && Count("SELECT Quantity FROM Vouchers WHERE Id=@id",("@id",voucher))==0,"confirmation deducts stock and voucher exactly once");
            Check(Count("SELECT Stock FROM Batches WHERE MedicineId=@m AND BatchNumber=@b",("@m",med),("@b",tag+"E"))==0 && Count("SELECT Stock FROM Batches WHERE MedicineId=@m AND BatchNumber=@b",("@m",med),("@b",tag+"L"))==1,"confirmation uses FEFO across early and late batches");
            Check(Count("SELECT COUNT(*) FROM OrderPendingItems WHERE OrderId=@id",("@id",pending))==0 && Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@id AND UnitPrice=1000",("@id",pending))==2,"confirmation replaces draft with batch details and preserves quoted unit price");
            Check((string)Scalar("SELECT PaymentMethod FROM Orders WHERE Id=@id",("@id",pending))=="Chuyển khoản QR" && Count("SELECT PaidEmployeeId FROM Orders WHERE Id=@id",("@id",pending))==staff,"actual payment method and collecting cashier are recorded");
            Check(Revenue()==before+3900,"revenue uses payment date rather than yesterday's draft date");
            Check(!invoices.ThanhToanCho(pending,"Tiền mặt",3900,out _) && StockFor()==1,"paid invoice cannot be paid again");
            Check(sales.ThanhToan(staff,CartFor(1),null,null,2000,out int concurrent,out _,out _,out error,"Chưa xác định",true),"second pending invoice");orders.Add(concurrent);
            var results=Task.WhenAll(Task.Run(()=>invoices.ThanhToanCho(concurrent,"Tiền mặt",2000,out _)),Task.Run(()=>invoices.ThanhToanCho(concurrent,"Tiền mặt",2000,out _))).GetAwaiter().GetResult();
            Check(results.Count(x=>x)==1 && StockFor()==0,"two concurrent confirmations commit exactly one sale");
            SetTestSession(admin);
            Check(new VoucherBLL().Luu(null,tag+"B","FIXED",50,1,true,null,out error),"rollback voucher fixture");otherVoucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",tag+"B"));
            Check(sales.ThanhToan(admin,CartFor(2),tag+"B",null,3950,out int blocked,out _,out _,out error,"Chưa xác định",true),"pending invoice does not need reserved stock");orders.Add(blocked);
            long version=DBConnection.DataVersion;
            Check(!invoices.ThanhToanCho(blocked,"Tiền mặt",3950,out error) && error.Contains("tồn kho"),"insufficient stock leaves invoice pending");
            Check((string)Scalar("SELECT PaymentStatus FROM Orders WHERE Id=@id",("@id",blocked))=="PENDING" && Count("SELECT Quantity FROM Vouchers WHERE Id=@id",("@id",otherVoucher))==1 && DBConnection.DataVersion==version,"failed confirmation rolls back voucher and status without refresh");
            Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@id",("@id",blocked))==0,"failed confirmation leaves no partial sale details");
            Scalar("INSERT INTO Batches(MedicineId,BatchNumber,ExpiryDate,Stock) VALUES(@m,@b,DATEADD(day,-1,CAST(GETDATE() AS date)),10)",("@m",med),("@b",tag+"EX"));
            Check(!invoices.ThanhToanCho(blocked,"Tiền mặt",3950,out _) && StockFor()==0,"expired stock cannot settle a pending invoice");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,StockLine(tag+"N",2,60),out _,out _,out error),"replenish missing stock");
            Scalar("UPDATE Vouchers SET Quantity=0 WHERE Id=@id",("@id",otherVoucher));
            Check(!invoices.ThanhToanCho(blocked,"Tiền mặt",3950,out _) && StockFor()==2,"voucher availability rechecked at payment");
            Scalar("UPDATE Vouchers SET Quantity=1 WHERE Id=@id",("@id",otherVoucher));
            Check(invoices.ThanhToanCho(blocked,"Tiền mặt",3950,out error),"pending invoice can be settled after stock and voucher become available");
            Check(!sales.ThanhToan(admin,CartFor(1),null,null,2000,out _,out _,out _,out _,"BAD",true),"invalid payment method rejected");
            Check(invoices.TimKiem(tag,null,null,true).Rows.Cast<DataRow>().All(row=>(string)row["PaymentStatus"]=="PENDING"),"pending-only filter returns no paid orders");
            var report=new BaoCaoDAL().LayDanhSachHoaDon(DateTime.Today,DateTime.Today);
            Check(report.Rows.Cast<DataRow>().Any(row=>(int)row["MaHD"]==pending),"paid report includes old draft settled today");

            // Excel parsing uses numeric values even when the cell's display format contains separators.
            string medicinesPath=Path.Combine(folder,"medicines.xlsx");
            using(var book=new XLWorkbook())
            {
                var sheet=book.AddWorksheet("Thuốc");string[] headers={"Tên thuốc","Mã vạch","Nhóm","NCC","ĐVT","Giá"};
                for(int c=0;c<6;c++) sheet.Cell(1,c+1).Value=headers[c];
                sheet.Cell(2,1).Value=tag+" Excel";sheet.Cell(2,2).Value="00"+tag;sheet.Cell(2,3).Value=tag;sheet.Cell(2,4).Value=tag;sheet.Cell(2,5).Value="Viên";
                sheet.Cell(2,6).Value=1234.25m;sheet.Cell(2,6).Style.NumberFormat.Format="#,##0.00";
                sheet.Cell(3,1).Value="Bad medicine";sheet.Cell(3,6).Value=-1;book.SaveAs(medicinesPath);
            }
            var rows=ExcelHelper.DocDanhSachThuocTuExcel(medicinesPath,out var errors);
            Check(rows.Count==1 && errors.Count==1 && rows[0].GiaBan==1234.25m && rows[0].BarCode=="00"+tag,"Excel reads formatted numeric prices, keeps barcode leading zeros, and reports bad rows");
            Check(catalog.ThemNhanhThuoc(rows[0].TenThuoc,group,supplier,rows[0].DonViTinh,rows[0].GiaBan,out excelMed,out error,rows[0].BarCode),"Excel medicine uses normal validated insert");
            Check((string)Scalar("SELECT BarCode FROM Medicines WHERE Id=@id",("@id",excelMed))=="00"+tag && Convert.ToDecimal(Scalar("SELECT Price FROM Medicines WHERE Id=@id",("@id",excelMed)))==1234,"Excel barcode saved and money rounded to whole dong");
            Check(!catalog.ThemNhanhThuoc(tag+" Duplicate",group,supplier,"Viên",1000,out _,out _,rows[0].BarCode),"duplicate barcode rejected without duplicate record");
            SetTestSession(staff);Check(!catalog.ThemNhanhThuoc(tag+" Denied",group,supplier,"Viên",1000,out _,out _,tag+"D"),"staff cannot bypass Excel catalog import permissions");SetTestSession(admin);
            string stockPath=Path.Combine(folder,"stock.xlsx");var supplierMedicines=new NhapKhoDAL().LayThuocTheoNCC(supplier);
            using(var book=new XLWorkbook())
            {
                var sheet=book.AddWorksheet("Nhập kho");sheet.Cell(1,1).Value="Mã thuốc";
                sheet.Cell(2,1).Value=med;sheet.Cell(2,2).Value=tag;sheet.Cell(2,3).Value="EXCELLOT";sheet.Cell(2,4).Value=DateTime.Today.AddDays(30);sheet.Cell(2,5).Value=3;sheet.Cell(2,6).Value=500.25;
                sheet.Cell(3,1).Value=med;sheet.Cell(3,2).Value="Wrong Name";sheet.Cell(3,3).Value="WRONG";sheet.Cell(3,4).Value=DateTime.Today.AddDays(30);sheet.Cell(3,5).Value=3;sheet.Cell(3,6).Value=500;
                sheet.Cell(4,1).Value=999999;sheet.Cell(4,2).Value=tag;sheet.Cell(4,3).Value="WRONG";sheet.Cell(4,4).Value=DateTime.Today.AddDays(30);sheet.Cell(4,5).Value=3;sheet.Cell(4,6).Value=500;
                book.SaveAs(stockPath);
            }
            var stockRows=ExcelHelper.DocDanhSachNhapKhoTuExcel(stockPath,supplierMedicines,out errors);
            Check(stockRows.Count==1 && errors.Count==2 && stockRows[0].GiaNhap==500.25m,"stock Excel validates medicine ID/name and does not silently fall back from bad ID");
            int beforePreview=StockFor();
            using(var preview=new frmXemTruocExcel(new DataTable(),errors)) {Offscreen(preview);Check(!UiField<Button>(preview,"btnNhap").Enabled,"preview with no valid rows cannot commit");}
            Check(StockFor()==beforePreview,"reading and previewing stock Excel does not change inventory");
            string exportPath=Path.Combine(folder,"invoices.xlsx");var data=invoices.TimKiem("HD"+pending.ToString("D5"));
            ExcelTable.Luu(exportPath,"Invoices",data,("MaHD","Mã"),("TrangThai","Trạng thái"),("HinhThucThanhToan","Hình thức"),("ThucThu","Đã thu"));
            using(var book=new XLWorkbook(exportPath))
                Check(book.Worksheet(1).Cell(3,2).GetString()=="Đã thanh toán" && book.Worksheet(1).Cell(3,3).GetString()=="Chuyển khoản QR" && book.Worksheet(1).Cell(3,4).GetValue<decimal>()==3900,"export workbook retains payment status, method and numeric amount");
            using(var main=new frmMain())
            {
                Offscreen(main);Check(UiField<Label>(main,"lblTitle").BackColor==Color.FromArgb(15,118,110) && UiField<Panel>(main,"panelChucNang").BackColor==Color.FromArgb(15,23,42),"main uses TEST_Update colors");
                Check(main.Controls.Find("btnCuaSo",true).Length==0 && main.Controls.Find("btnTamAn",true).Length==0,"deleted navigation buttons remain absent");main.Dispose();
            }
            using(var form=new frmHoaDon())
            {
                Offscreen(form);Check(UiField<DataGridView>(form,"dgvHoaDon").Columns.Contains("TrangThai") && UiField<DataGridView>(form,"dgvHoaDon").Columns.Contains("HinhThucThanhToan"),"invoice columns exist in Designer");
                Check(!UiField<Button>(form,"btnThanhToanCho").Visible,"no confirmation action before selecting pending invoice");
            }
            string medicineExport=Path.Combine(folder,"export-medicines.xlsx");
            ExcelHelper.XuatDanhSachThuoc(catalog.TimKiemThuoc(tag+" Excel"),medicineExport,false);
            var roundtrip=ExcelHelper.DocDanhSachThuocTuExcel(medicineExport,out var roundtripErrors);
            Check(roundtrip.Count==1 && roundtripErrors.Count==0 && roundtrip[0].BarCode=="00"+tag,"actual medicine export can be read back using the import format");
            string medicineTemplate=Path.Combine(folder,"medicine-template.xlsx");ExcelHelper.XuatFileMauNhapThuoc(medicineTemplate,false);
            Check(ExcelHelper.DocDanhSachThuocTuExcel(medicineTemplate,out _).Count==2,"medicine import template matches its parser");
            string stockTemplate=Path.Combine(folder,"stock-template.xlsx");ExcelHelper.XuatFileMauNhapKho(tag,supplierMedicines,stockTemplate,false);
            Check(ExcelHelper.DocDanhSachNhapKhoTuExcel(stockTemplate,supplierMedicines,out _).Count==0,"stock template never invents quantities or costs for real products");
            string detailPath=Path.Combine(folder,"detail.xlsx");
            ExcelHelper.XuatChiTietMotHoaDon(pending,"Khách lẻ",tag,DateTime.Today,4000,100,3900,invoices.ChiTiet(pending),"Đã thanh toán","Chuyển khoản QR",detailPath,false);
            using(var book=new XLWorkbook(detailPath))
            {
                Check(book.Worksheet(1).Cell("A2").GetString().Contains("Chuyển khoản QR"),"actual invoice detail export includes status and payment method");
                Check(book.Worksheet(1).Cell(8,4).GetString()=="Viên","actual invoice detail export retains medicine unit");
            }
            var reports=new BaoCaoBLL();Check(reports.LayBaoCao(DateTime.Today,DateTime.Today,out var daily,out var paidOrders,out int billCount,out decimal total,out decimal reductions,out decimal revenue,out _),"paid report data prepared for export");
            string reportPath=Path.Combine(folder,"report.xlsx");ExcelHelper.XuatBaoCaoDoanhThu(DateTime.Today,DateTime.Today,billCount,total,reductions,revenue,daily,paidOrders,reportPath,false);
            using(var book=new XLWorkbook(reportPath)) Check(book.Worksheets.Count==2 && book.Worksheet(2).Cell(3,8).GetString()=="Hình thức thanh toán","actual report export creates both sheets including payment method");
            SetTestSession(staff);
            using(var medForm=new frmQuanLyThuoc())
            {Offscreen(medForm);Check(!UiField<Button>(medForm,"btnNhapExcel").Visible && UiField<Button>(medForm,"btnXuatExcel").Visible,"staff may export but cannot import medicine catalog");}
            using(var stockForm=new frmNhapKho())
            {Offscreen(stockForm);Check(UiField<Button>(stockForm,"btnNhapExcel").Visible,"staff may import stock Excel");}
        }
        finally
        {
            SetTestSession(admin);
            foreach(int id in orders) Scalar("DELETE FROM Orders WHERE Id=@id",("@id",id));
            Scalar(@"DELETE FROM StockIns WHERE Id IN(SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                DELETE FROM Batches WHERE MedicineId=@m;DELETE FROM Medicines WHERE Id IN(@m,@x);DELETE FROM Vouchers WHERE Id IN(@v,@v2);
                DELETE FROM Employees WHERE Id=@staff;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;",("@m",med),("@x",excelMed),("@v",voucher),("@v2",otherVoucher),("@staff",staff),("@s",supplier),("@g",group));
            // This folder is created only for this test run.
            string target=Path.GetFullPath(folder),workspace=Path.GetFullPath("artifacts")+Path.DirectorySeparatorChar;
            if(!target.StartsWith(workspace,StringComparison.OrdinalIgnoreCase) || !Path.GetFileName(target).StartsWith("excel-check-XP")) throw new Exception("Unsafe test cleanup path");
            Directory.Delete(target,true);
        }
    }
}
