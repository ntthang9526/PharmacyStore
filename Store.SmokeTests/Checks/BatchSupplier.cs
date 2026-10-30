using System.Data;
using Microsoft.Data.SqlClient;
using Store.DAL;
using Store.BLL;
using Store.Shared;
using Store.POS;
internal static partial class Program
{
    static void TestBatchSupplier()
    {
        string tag="SOURCE_"+Guid.NewGuid().ToString("N")[..10].ToUpperInvariant(),name=tag+" Medicine",aName=tag+" Supplier A",bName=tag+" Supplier B";
        int admin=PhanQuyenDAL.NhanVienId,category=0,a=0,b=0,med=0,legacy=0;
        var catalog=new ThuocBLL();var imports=new NhapKhoBLL();var lots=new LoHangBLL();var receipts=new PhieuNhapDAL();
        List<NhapKhoItem> Lines()=>new(){new(){MaThuoc=med,TenThuoc=name,SoLo=tag,HanDung=DateTime.Today.AddDays(30),SoLuong=2,GiaNhap=500}};
        DataRow Lot()=>lots.TimKiem(tag).Rows.Cast<DataRow>().Single(r=>(string)r["SoLo"]==tag);
        void UiState(bool expected)
        {
            using var form=new frmQuanLyThuoc();Offscreen(form);var grid=UiField<DataGridView>(form,"dgvThuoc");var row=grid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med);
            InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,row.Index));UiField<Button>(form,"btnSua").PerformClick();
            Check(UiField<ComboBox>(form,"cboNhaCungCap").Enabled==expected && UiField<Button>(form,"btnThemNCC").Enabled==expected,"supplier editor and quick-add availability="+expected);
            Check(UiField<TextBox>(form,"txtTenThuoc").Enabled && !UiField<ComboBox>(form,"cboDonViTinh").Enabled,"other edits work while unit remains locked");
        }
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out category,out var error),"source category created");
            Check(new NhaCungCapBLL().ThemNhanhNCC(aName,"0901234567","Test",out a,out error),"source supplier A created");
            Check(new NhaCungCapBLL().ThemNhanhNCC(bName,"0901234568","Test",out b,out error),"source supplier B created");
            Check(catalog.ThemNhanhThuoc(name,category,a,"Hộp",1000,out med,out error),"source medicine created");UiState(true);
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",b,out error),"supplier can change before first lot");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",a,out error),"supplier restored before first lot");
            int first;
            using(var editor=new frmQuanLyThuoc())
            {
                Offscreen(editor);var grid=UiField<DataGridView>(editor,"dgvThuoc");var row=grid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med);
                InvokePrivate(editor,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,row.Index));UiField<Button>(editor,"btnSua").PerformClick();
                var supplierBox=UiField<ComboBox>(editor,"cboNhaCungCap");supplierBox.SelectedValue=b;
                Check(imports.LuuPhieuNhap(admin,a,tag,Lines(),out first,out _,out error),"first supplier A receipt saved while editor is open");
                editor.LamMoiDuLieu();
                Check(!supplierBox.Enabled && !UiField<Button>(editor,"btnThemNCC").Enabled && (int)supplierBox.SelectedValue! == a,"refresh locks supplier and discards stale unsaved supplier reassignment after first receipt");
            }
            Check((bool)catalog.TimKiemThuoc(name).Rows.Cast<DataRow>().Single(r=>(int)r["MaThuoc"]==med)["KhoaNCC"],"catalog marks supplier locked after stock receipt");UiState(false);
            Check(!catalog.SuaThuoc(med.ToString(),name+" Rejected",category.ToString(),"Hộp","2000","0",b,out error) && error.Contains("nhà cung cấp"),"supplier change after first lot is rejected");
            Check(Count("SELECT SupplierId FROM Medicines WHERE Id=@m",("@m",med))==a && (string)Scalar("SELECT Name FROM Medicines WHERE Id=@m",("@m",med))==name,"rejected edit changes neither supplier nor other medicine fields");
            Check(catalog.SuaThuoc(med.ToString(),name,category.ToString(),"Hộp","1000","0",a,out error),"ordinary edits with original supplier remain allowed");
            Check((string)Lot()["TenNCC"]==aName,"batch source comes from supplier A receipt");
            // Reproduce a catalog reassignment made by an older version, using only this fixture.
            Scalar("UPDATE Medicines SET SupplierId=@b WHERE Id=@m;SELECT 1",("@b",b),("@m",med));
            Check((string)Lot()["TenNCC"]==aName,"batch source stays A after catalog supplier changes externally");
            Check((string)receipts.TimKiem(first.ToString(),null,null).Rows.Cast<DataRow>().Single(r=>(int)r["MaPhieu"]==first)["TenNCC"]==aName,"receipt and batch source remain consistent");
            Check(lots.TimKiem(aName).Rows.Cast<DataRow>().Any(r=>(string)r["SoLo"]==tag) && !lots.TimKiem(bName).Rows.Cast<DataRow>().Any(r=>(string)r["SoLo"]==tag),"source search does not infer the current catalog supplier");
            Check(imports.LuuPhieuNhap(admin,b,tag,Lines(),out _,out _,out error),"legacy reassigned medicine can produce a second recorded source");
            string sources=(string)Lot()["TenNCC"];
            Check(sources.Contains(aName) && sources.Contains(bName) && sources.Split(';').Length==2,"batch with two recorded sources displays both without selecting an arbitrary supplier");
            Check(lots.TimKiem(aName).Rows.Cast<DataRow>().Any(r=>(string)r["SoLo"]==tag) && lots.TimKiem(bName).Rows.Cast<DataRow>().Any(r=>(string)r["SoLo"]==tag),"multi-source batch is searchable by either recorded supplier");
            Check(catalog.ThemNhanhThuoc(tag+" Legacy",category,a,"Hộp",1000,out legacy,out error),"legacy medicine created");
            Scalar("INSERT INTO Batches(MedicineId,BatchNumber,ExpiryDate,Stock) VALUES(@m,@lot,DATEADD(day,30,CAST(GETDATE() AS date)),0);SELECT 1",("@m",legacy),("@lot",tag+"-legacy"));
            Check(!catalog.SuaThuoc(legacy.ToString(),tag+" Legacy",category.ToString(),"Hộp","1000","0",b,out error),"even an empty legacy lot prevents reassignment");
            var legacyLot=lots.TimKiem(tag+"-legacy").Rows.Cast<DataRow>().Single();
            Check((string)legacyLot["TenNCC"]=="Chưa xác định","lot without a receipt clearly has unknown source");
        }
        finally
        {
            SetTestSession(admin);using var connection=DBConnection.GetDbConnection();
            using var command=new SqlCommand(@"
                DELETE d FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId IN (@m,@l);
                DELETE FROM StockIns WHERE Note=@tag AND SupplierId IN (@a,@b);
                DELETE FROM Batches WHERE MedicineId IN (@m,@l);DELETE FROM Medicines WHERE Id IN (@m,@l);
                DELETE FROM Suppliers WHERE Id IN (@a,@b);DELETE FROM Categories WHERE Id=@g;",connection);
            command.Parameters.AddWithValue("@m",med);command.Parameters.AddWithValue("@l",legacy);command.Parameters.AddWithValue("@a",a);command.Parameters.AddWithValue("@b",b);command.Parameters.AddWithValue("@g",category);command.Parameters.AddWithValue("@tag",tag);command.ExecuteNonQuery();DBConnection.NotifyDataChanged();
        }
    }
}
