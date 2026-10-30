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
    static void TestSellingStatus()
    {
        string tag="STATUS_"+Guid.NewGuid().ToString("N")[..10];
        int admin=PhanQuyenDAL.NhanVienId,group=0,supplier=0,med=0;
        var orders=new List<int>();var catalog=new ThuocBLL();var lots=new LoHangBLL();var sales=new BanHangBLL();var invoices=new HoaDonBLL();var imports=new NhapKhoBLL();
        List<CartItem> Cart(int q)=>new(){new(){MaThuoc=med,SoLuong=q}};
        List<NhapKhoItem> Line(string lot,int q,int days)=>new(){new(){MaThuoc=med,TenThuoc=tag,SoLo=tag+lot,HanDung=DateTime.Today.AddDays(days),SoLuong=q,GiaNhap=500}};
        int Stock()=>Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@m",("@m",med));
        int BatchStock(int batch)=>Count("SELECT Stock FROM Batches WHERE Id=@b",("@b",batch));
        void SetSelling(bool value)
        {
            Check(catalog.SuaThuoc(med.ToString(),tag,group.ToString(),"Viên","1000","0",supplier,out var error,value),"set medicine selling="+value+": "+error);
        }
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out group,out var error),"status fixture category: "+error);
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"status fixture supplier");
            Check(catalog.ThemNhanhThuoc(tag,group,supplier,"Viên",1000,out med,out error),"new medicine defaults to selling");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("E",2,2),out _,out _,out error),"earliest lot imported");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("L",3,30),out _,out _,out error),"later lot imported");
            Scalar("INSERT INTO Batches(MedicineId,BatchNumber,ExpiryDate,Stock) OUTPUT INSERTED.Id VALUES(@m,@n,DATEADD(day,-1,CAST(GETDATE() AS date)),4)",("@m",med),("@n",tag+"X"));
            int early=Count("SELECT Id FROM Batches WHERE MedicineId=@m AND BatchNumber=@n",("@m",med),("@n",tag+"E"));
            int late=Count("SELECT Id FROM Batches WHERE MedicineId=@m AND BatchNumber=@n",("@m",med),("@n",tag+"L"));
            Check(lots.TimKiem(tag).Rows.Cast<DataRow>().Count(r=>(string)r["TrangThaiLo"]=="Hết hạn")==1,"expired lot is visibly marked expired rather than allowed for sale");
            Check(Stock()==9 && new MedicineDAL().SoLuongConHan(med)==5,"physical stock includes expired lots; salable stock excludes them");
            Check(lots.DoiTrangThai(early,true,out error),"Admin locks earliest lot: "+error);
            Check(Stock()==9 && new MedicineDAL().SoLuongConHan(med)==3 && new BatchDAL().TimToanBoSoLuongConHan(med)==3,"locking a lot preserves physical stock and excludes it from both stock checks");
            var found=new BanHangDAL().TimThuocBan(tag);Check(found.Rows.Count==1 && (int)found.Rows[0]["TonKho"]==3,"sales search shows salable stock only");
            Check(!sales.ThanhToan(admin,Cart(4),null,null,4000,out _,out _,out _,out error) && Stock()==9,"insufficient unlocked stock rejects sale without partial deduction");
            Check(sales.ThanhToan(admin,Cart(2),null,null,2000,out int paid,out _,out _,out error),"sale uses an available later lot: "+error);orders.Add(paid);
            Check(BatchStock(early)==2 && BatchStock(late)==1 && Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@o AND BatchId=@b",("@o",paid),("@b",late))==1,"FEFO skips locked earliest lot");
            Check(sales.ThanhToan(admin,Cart(1),null,null,1000,out int pending,out _,out _,out error,"Chưa xác định",true),"create pending invoice before stock status changes");orders.Add(pending);
            Check(lots.DoiTrangThai(late,true,out error),"lock remaining salable lot");
            int before=Stock();
            Check(!invoices.ThanhToanCho(pending,"Tiền mặt",1000,out error) && Stock()==before && (string)Scalar("SELECT PaymentStatus FROM Orders WHERE Id=@o",("@o",pending))=="PENDING","pending invoice rechecks locked lots and stays pending without stock changes");
            Check(imports.LuuPhieuNhap(admin,supplier,tag,Line("L",1,30),out _,out _,out error),"can receive more stock into a locked lot");
            Check(Count("SELECT CAST(IsBlocked AS int) FROM Batches WHERE Id=@b",("@b",late))==1 && new MedicineDAL().SoLuongConHan(med)==0,"receiving stock never silently unlocks a lot");
            Check(lots.DoiTrangThai(early,false,out error),"Admin unlocks early lot");
            SetSelling(false);before=Stock();
            var record=catalog.TimKiemThuoc(tag).Rows.Cast<DataRow>().Single(r=>(int)r["MaThuoc"]==med);
            Check(!(bool)record["DangBan"] && (string)record["TrangThaiBan"]=="Ngừng bán" && (int)record["SoLuongTon"]==before && (int)record["TonCoTheBan"]==0,"stopped medicine remains in management with physical and salable stock separated");
            Check(lots.TimKiem(tag).Rows.Cast<DataRow>().Any(r=>(string)r["TrangThaiLo"]=="Thuốc ngừng bán"),"unlocked lot clearly shows when its medicine is stopped");
            Check(!new MedicineDAL().GetMedicines().ContainsKey(med) && new BanHangDAL().TimThuocBan(tag).Rows.Count==0 && new BanHangDAL().LayThongTinThuoc(med).Rows.Count==0,"stopped medicine is excluded from all sales lookups");
            Check(new MedicineDAL().SoLuongConHan(med)==0 && new BatchDAL().TimToanBoSoLuongConHan(med)==0,"stopped medicine has zero salable stock");
            Check(!sales.ThanhToan(admin,Cart(1),null,null,1000,out _,out _,out _,out error) && Stock()==before,"stale cart cannot sell a stopped medicine");
            Check(!invoices.ThanhToanCho(pending,"Tiền mặt",1000,out error) && Stock()==before,"pending invoice cannot sell a stopped medicine");
            var warnings=new CanhBaoDAL().LayThuocSapHet(100);
            Check(!warnings.Rows.Cast<DataRow>().Any(r=>(int)r["MaThuoc"]==med),"stopped medicine is excluded from replenishment alerts");
            Directory.CreateDirectory("artifacts/selling-status-20261029");
            ExcelHelper.XuatDanhSachThuoc(catalog.TimKiemThuoc(tag),"artifacts/selling-status-20261029/status.xlsx",false);
            using(var book=new XLWorkbook("artifacts/selling-status-20261029/status.xlsx"))Check(book.Worksheet(1).Cell(1,7).GetString()=="Trạng thái bán" && book.Worksheet(1).Cell(2,7).GetString()=="Ngừng bán","Excel export includes medicine status");
            using(var form=new frmQuanLyThuoc())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvThuoc");int index=grid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med).Index;
                InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,index));
                Check(UiField<ComboBox>(form,"cboTrangThaiBan").SelectedIndex==1,"medicine editor displays the stopped state");
                form.LamMoiDuLieu();Check(UiField<ComboBox>(form,"cboTrangThaiBan").SelectedIndex==1,"refresh preserves displayed medicine status");
            }
            SetSelling(true);
            Check(invoices.ThanhToanCho(pending,"Tiền mặt",1000,out error),"pending invoice can be paid after medicine resumes and a lot is unlocked: "+error);
            Check(BatchStock(early)==1 && BatchStock(late)==2,"resumed sale uses earliest allowed lot, leaving locked lot untouched");
            int staff=Count("SELECT TOP(1) Id FROM Employees WHERE Role='STAFF' AND IsActive=1 ORDER BY Id");SetTestSession(staff);
            Check(!lots.DoiTrangThai(late,false,out error),"staff cannot unlock a lot through business layer");
            Check(!catalog.SuaThuoc(med.ToString(),tag,group.ToString(),"Viên","1000","0",supplier,out error,false),"staff cannot stop a medicine through business layer");
            SetTestSession(admin);
            Check(lots.DoiTrangThai(late,false,out error) && new MedicineDAL().SoLuongConHan(med)==3,"unlocking restores only unexpired stock");
            Check(!lots.DoiTrangThai(late,false,out error),"stale repeated status change is reported instead of silently saving");
            foreach(bool selling in new[]{false,true})
            {
                SetSelling(selling);
                foreach(var type in new[]{typeof(frmQuanLyThuoc),typeof(frmLoHang)})
                {
                    using var main=new frmMain();Offscreen(main);main.ClientSize=new Size(1500,820);
                    typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
                    var opened=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
                    foreach(var size in new[]{new Size(1500,820),new Size(1382,753),new Size(1100,700)})
                    {
                        main.ClientSize=size;Application.DoEvents();
                        IEnumerable<Control> Walk(Control parent){foreach(Control child in parent.Controls){yield return child;foreach(var item in Walk(child))yield return item;}}
                        foreach(var control in Walk(opened).Where(c=>c.Visible && c is Button or ComboBox or TextBoxBase))
                        {
                            var rect=control.RectangleToScreen(control.ClientRectangle);var visible=rect;
                            for(Control? parent=control.Parent;parent!=null;parent=parent.Parent)visible=Rectangle.Intersect(visible,parent.RectangleToScreen(parent.ClientRectangle));
                            Check(visible.Width>=rect.Width-2 && visible.Height>=rect.Height-2,type.Name+" Admin "+size.Width+" control fits: "+control.Name);
                        }
                    }
                    main.ClientSize=new Size(1500,820);Application.DoEvents();
                    using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/selling-status-20261029/"+type.Name+"-"+selling+".png");
                    main.Dispose();
                }
            }
        }
        finally
        {
            SetTestSession(admin);
            using var connection=DBConnection.GetDbConnection();
            using var command=new SqlCommand(@"
                DELETE FROM OrderPendingItems WHERE MedicineId=@m;
                DELETE d FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m;
                DELETE FROM Orders WHERE Id IN (SELECT value FROM STRING_SPLIT(@orders,','));
                DELETE d FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m;
                DELETE FROM StockIns WHERE Note=@tag AND SupplierId=@s;
                DELETE FROM Batches WHERE MedicineId=@m;DELETE FROM Medicines WHERE Id=@m;
                DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;",connection);
            command.Parameters.AddWithValue("@m",med);command.Parameters.AddWithValue("@s",supplier);command.Parameters.AddWithValue("@g",group);command.Parameters.AddWithValue("@tag",tag);
            command.Parameters.AddWithValue("@orders",string.Join(",",orders));command.ExecuteNonQuery();DBConnection.NotifyDataChanged();
        }
    }
}
