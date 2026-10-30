using System.Data;
using Microsoft.Data.SqlClient;
using Store.DAL;
using Store.BLL;
using Store.Shared;
using Store.POS;
using Store.POS.Common;
using ClosedXML.Excel;
internal static partial class Program
{
    static void TestDocumentHistory()
    {
        string tag="HISTORY_"+Guid.NewGuid().ToString("N")[..10],original=tag+" Original",renamed=tag+" Renamed";
        int admin=PhanQuyenDAL.NhanVienId,group=0,supplier=0,med=0;
        var orders=new List<int>();var receipts=new List<int>();var catalog=new ThuocBLL();var sales=new BanHangBLL();var invoices=new HoaDonDAL();var imports=new NhapKhoBLL();var stockDocs=new PhieuNhapDAL();
        List<CartItem> Cart(int q)=>new(){new(){MaThuoc=med,SoLuong=q}};
        List<NhapKhoItem> Line(string lot,int q,int days)=>new(){new(){MaThuoc=med,TenThuoc="stale UI name",SoLo=tag+lot,HanDung=DateTime.Today.AddDays(days),SoLuong=q,GiaNhap=500}};
        void Verify(DataTable rows,string name,string unit,bool captured,string message)
        {
            string key=rows.Columns.Contains("DonViTinh")?"DonViTinh":"DonVi";
            Check(rows.Rows.Count>0 && rows.Rows.Cast<DataRow>().All(r=>(string)r["TenThuoc"]==name && (string)r[key]==unit && ((string)r["NguonThongTin"]).StartsWith(captured?"Lưu lúc":"Chưa có")),message);
        }
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out group,out var error),"history category created");
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"history supplier created");
            Check(catalog.ThemNhanhThuoc(original,group,supplier,"Hộp",1000,out med,out error),"new medicine records its initial unit");
            Check(!catalog.SuaThuoc(med.ToString(),renamed,group.ToString(),"Viên","1000","0",supplier,out error) && error.Contains("đơn vị"),"cannot change unit even before any transactions");
            Check((string)Scalar("SELECT Name FROM Medicines WHERE Id=@m",("@m",med))==original && (string)Scalar("SELECT Unit FROM Medicines WHERE Id=@m",("@m",med))=="Hộp","rejected edit changes neither name nor unit");
            using(var form=new frmQuanLyThuoc())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvThuoc");var row=grid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med);
                InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,row.Index));UiField<Button>(form,"btnSua").PerformClick();
                Check(!UiField<ComboBox>(form,"cboDonViTinh").Enabled && UiField<TextBox>(form,"txtTenThuoc").Enabled,"edit mode locks unit while other medicine fields remain editable");
                UiField<Button>(form,"btnBoQua").PerformClick();UiField<Button>(form,"btnThem").PerformClick();
                Check(UiField<ComboBox>(form,"cboDonViTinh").Enabled,"create mode still permits selecting a unit");
            }
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("E",2,2),out int first,out _,out error),"first receipt saved");receipts.Add(first);
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("L",5,30),out int second,out _,out error),"second receipt saved");receipts.Add(second);
            Verify(stockDocs.ChiTiet(first),original,"Hộp",true,"receipt captures authoritative name/unit instead of stale input");
            Check(sales.ThanhToan(admin,Cart(1),null,null,1000,out int paid,out _,out _,out error),"direct paid invoice saved");orders.Add(paid);
            Check(sales.ThanhToan(admin,Cart(3),null,null,3000,out int pending,out _,out _,out error,"Chưa xác định",true),"pending invoice captures name/unit before allocating lots");orders.Add(pending);
            Check(catalog.SuaThuoc(med.ToString(),renamed,group.ToString(),"Hộp","1000","0",supplier,out error),"medicine name can still change with unit unchanged");
            Verify(invoices.ChiTiet(paid),original,"Hộp",true,"paid invoice retains name/unit after rename");
            Verify(invoices.ChiTiet(pending),original,"Hộp",true,"pending invoice retains name/unit after rename");
            Verify(stockDocs.ChiTiet(first),original,"Hộp",true,"old receipt retains name/unit after rename");
            Check(stockDocs.TimKiem(original,null,null).Rows.Cast<DataRow>().Any(r=>(int)r["MaPhieu"]==first),"receipt search finds the historical medicine name");
            Check(!new ThuocDAL().UpdateThuoc(med.ToString(),renamed,group.ToString(),"Viên",1000,0,supplier,out error),"direct DAL call cannot bypass unit protection after transactions");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("NEW",1,60),out int third,out _,out error),"receipt after rename saved");receipts.Add(third);
            Verify(stockDocs.ChiTiet(third),renamed,"Hộp",true,"new receipt captures the new name without changing old receipts");
            Check(new HoaDonBLL().ThanhToanCho(pending,"Tiền mặt",3000,out error),"pending invoice paid after catalog rename: "+error);
            var split=invoices.ChiTiet(pending);Check(split.Rows.Count==2,"pending invoice is split over FEFO lots");
            Verify(split,original,"Hộp",true,"all allocated lots preserve the pending invoice's original name/unit");
            // Simulate an external/older catalog edit on this test-only medicine.
            Scalar("UPDATE Medicines SET Name=@n,Unit=N'Viên' WHERE Id=@m;SELECT 1",("@m",med),("@n",tag+" External"));
            Verify(invoices.ChiTiet(paid),original,"Hộp",true,"snapshotted invoice unit survives external catalog changes");
            Verify(stockDocs.ChiTiet(first),original,"Hộp",true,"snapshotted receipt unit survives external catalog changes");
            int legacy=Count("INSERT INTO Orders(EmployeeId,TotalAmount,DiscountAmount,Channel,PaymentMethod,PaymentStatus) OUTPUT INSERTED.Id VALUES(@e,1000,0,'POS',N'Chưa xác định','PENDING')",("@e",admin));orders.Add(legacy);
            Scalar("INSERT INTO OrderPendingItems(OrderId,MedicineId,Quantity,UnitPrice) VALUES(@o,@m,1,1000);SELECT 1",("@o",legacy),("@m",med));
            Verify(invoices.ChiTiet(legacy),tag+" External","Viên",false,"legacy pending item explicitly labels current catalog fallback");
            Check(new HoaDonBLL().ThanhToanCho(legacy,"Tiền mặt",1000,out error),"legacy pending invoice remains payable");
            Verify(invoices.ChiTiet(legacy),tag+" External","Viên",false,"paying legacy invoice does not fabricate a captured original name/unit");
            Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@o AND MedicineNameSnapshot IS NULL AND MedicineUnitSnapshot IS NULL",("@o",legacy))>0,"legacy paid detail retains null snapshots for honest provenance");
            Scalar("UPDATE StockInDetails SET MedicineNameSnapshot=NULL,MedicineUnitSnapshot=NULL WHERE StockInId=@i;SELECT 1",("@i",second));
            Verify(stockDocs.ChiTiet(second),tag+" External","Viên",false,"legacy receipt explicitly labels current catalog fallback");
            Directory.CreateDirectory("artifacts/document-history-20261029");
            foreach(int id in new[]{paid,legacy})
            {
                string file="artifacts/document-history-20261029/invoice-"+id+".xlsx";
                ExcelHelper.XuatChiTietMotHoaDon(id,"Khách lẻ","Test",DateTime.Now,1000,0,1000,invoices.ChiTiet(id),outputPath:file,notify:false);
                using var book=new XLWorkbook(file);var sheet=book.Worksheet(1);
                Check(sheet.Cell(7,8).GetString()=="Nguồn tên/đơn vị" && sheet.Cell(8,8).GetString().StartsWith(id==paid?"Lưu lúc":"Chưa có"),"invoice export retains provenance for "+id);
                if(id==paid)Check(sheet.Cell(8,2).GetString()==original && sheet.Cell(8,4).GetString()=="Hộp","invoice export preserves captured name and unit");
            }
        }
        finally
        {
            SetTestSession(admin);using var connection=DBConnection.GetDbConnection();
            using var command=new SqlCommand(@"
                DELETE FROM OrderPendingItems WHERE MedicineId=@m;
                DELETE d FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m;
                DELETE FROM Orders WHERE Id IN (SELECT value FROM STRING_SPLIT(@orders,','));
                DELETE d FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m;
                DELETE FROM StockIns WHERE Note=@tag AND SupplierId=@s;
                DELETE FROM Batches WHERE MedicineId=@m;DELETE FROM Medicines WHERE Id=@m;
                DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;",connection);
            command.Parameters.AddWithValue("@m",med);command.Parameters.AddWithValue("@s",supplier);command.Parameters.AddWithValue("@g",group);command.Parameters.AddWithValue("@tag",tag);command.Parameters.AddWithValue("@orders",string.Join(",",orders));command.ExecuteNonQuery();DBConnection.NotifyDataChanged();
        }
    }
}
