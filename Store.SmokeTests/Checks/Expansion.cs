using System.Data;
using System.Reflection;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.Shared;

internal static partial class Program
{
    static void TestExpansion()
    {
        int supplier=0,medicine=0,voucher=0;
        var vouchers=new VoucherBLL();
        string code="UI"+Guid.NewGuid().ToString("N")[..12].ToUpperInvariant();
        string medName=prefix+" Thuốc thử";
        int category=Count("SELECT MIN(Id) FROM Categories");
        int employee=Count("SELECT MIN(Id) FROM Employees");
        void Preview(Form form,string filename)
        {
            Directory.CreateDirectory("artifacts/previews");
            using var bitmap=new Bitmap(form.Width,form.Height);
            form.DrawToBitmap(bitmap,new Rectangle(0,0,bitmap.Width,bitmap.Height));
            bitmap.Save("artifacts/previews/"+filename+".png");
        }
        void Dialog<T>(Action trigger,Action<T> fill) where T:Form
        {
            bool opened=false;Exception? failure=null;
            using var timer=new System.Windows.Forms.Timer {Interval=60};
            timer.Tick+=(_,_)=>
            {
                var dialog=Application.OpenForms.OfType<T>().FirstOrDefault();
                if(dialog==null) return;
                timer.Stop();opened=true;dialog.Location=new Point(-20000,-20000);
                try {fill(dialog);}
                catch(Exception ex) {failure=ex;dialog.Close();}
            };
            timer.Start();trigger();timer.Stop();
            if(failure!=null) throw failure;
            Check(opened,"opens actual "+typeof(T).Name+" modal");
        }
        try
        {
            foreach(var type in new[]{typeof(frmQuanLyThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang)})
            {
                using var form=(Form)Activator.CreateInstance(type)!;Offscreen(form);
                Check(UiField<TextBox>(form,"txtTimKiem").Visible && !form.Controls.OfType<ComboBox>().Any(),type.Name+": search is a text field");
            }
            using(var import=new frmNhapKho())
            {
                Offscreen(import);
                var suppliers=UiField<ComboBox>(import,"cboNhaCungCap");var medicines=UiField<ComboBox>(import,"cboThuoc");
                var batch=UiField<TextBox>(import,"txtSoLo");var note=UiField<TextBox>(import,"txtGhiChu");
                Check(suppliers.SelectedIndex==-1 && medicines.SelectedIndex==-1,"stock-in starts without supplier or medicine");
                Check(!batch.Enabled && !UiField<Button>(import,"btnThemThuoc").Enabled && !UiField<Button>(import,"btnTaoThuoc").Enabled,"batch entry/add-line/create-medicine gated before supplier selection");
                note.Text="Giữ ghi chú khi thêm nhanh";UiField<NumericUpDown>(import,"nudSoLuong").Value=5;UiField<NumericUpDown>(import,"nudGiaNhap").Value=1234;
                Dialog<frmThemDanhMuc>(()=>UiField<Button>(import,"btnThemNCC").PerformClick(),dialog=>
                {
                    UiField<TextBox>(dialog,"txtTen").Text=prefix+" NCC nhập mới";
                    UiField<TextBox>(dialog,"txtDienThoai").Text="09"+Random.Shared.Next(10000000,99999999);
                    UiField<TextBox>(dialog,"txtDiaChi").Text="Địa chỉ kiểm thử";
                    UiField<Button>(dialog,"btnLuu").PerformClick();
                    supplier=dialog.CreatedId;
                    Check(supplier>0,"stock-in quick supplier saves to database");
                });
                Check(Convert.ToInt32(suppliers.SelectedValue)==supplier && medicines.SelectedIndex==-1 && !batch.Enabled,"new supplier selected, medicine remains empty and batch locked");
                Dialog<frmThemThuoc>(()=>UiField<Button>(import,"btnTaoThuoc").PerformClick(),dialog=>
                {
                    UiField<Button>(dialog,"btnLuu").PerformClick();
                    Check(dialog.CreatedId==0 && UiField<Label>(dialog,"lblLoi").Text.Length>0,"quick medicine rejects empty fields");
                    UiField<TextBox>(dialog,"txtTen").Text=medName;UiField<TextBox>(dialog,"txtDonVi").Text="Hộp";
                    UiField<NumericUpDown>(dialog,"nudGiaBan").Value=2500.50m;
                    Check(UiField<ComboBox>(dialog,"cboNhom").SelectedIndex==-1,"quick medicine requires explicit category selection");
                    UiField<Button>(dialog,"btnLuu").PerformClick();
                    Check(dialog.CreatedId==0,"missing category rejected");
                    UiField<ComboBox>(dialog,"cboNhom").SelectedValue=category;
                    CheckLayout(dialog,"quick medicine");Preview(dialog,"quick-medicine");
                    UiField<Button>(dialog,"btnLuu").PerformClick();medicine=dialog.CreatedId;
                    Check(medicine>0,"quick medicine saves with returned identity");
                });
                Check(Convert.ToInt32(medicines.SelectedValue)==medicine && batch.Enabled,"new medicine selected and batch entry unlocked");
                Check(Count("SELECT COUNT(*) FROM Batches WHERE MedicineId=@m",("@m",medicine))==0,"new medicine has no invented stock or batch");
                Check(note.Text=="Giữ ghi chú khi thêm nhanh" && UiField<NumericUpDown>(import,"nudSoLuong").Value==5 && UiField<NumericUpDown>(import,"nudGiaNhap").Value==1234,"quick creation preserves import draft inputs");
                Check(!new ThuocBLL().ThemNhanhThuoc(" "+medName.ToLowerInvariant()+" ",category,supplier,"hộp",2500,out _,out _),"duplicate active medicine for supplier/unit rejected");
                Check(!new ThuocBLL().ThemNhanhThuoc("x",category,supplier,"Hộp",-1,out _,out _),"negative selling price rejected");
                batch.Text="OLD";medicines.SelectedIndex=-1;
                Check(!batch.Enabled && batch.Text=="","clearing medicine selection clears and locks batch number");
                medicines.SelectedValue=medicine;batch.Text="LOT-UI";
                Dialog<frmThemThuoc>(()=>UiField<Button>(import,"btnTaoThuoc").PerformClick(),dialog=>UiField<Button>(dialog,"btnHuy").PerformClick());
                Check(batch.Text=="LOT-UI" && Convert.ToInt32(medicines.SelectedValue)==medicine,"cancel quick medicine preserves batch draft and selection");
                UiField<Button>(import,"btnThemThuoc").PerformClick();
                Check(UiField<DataTable>(import,"dtPhieu").Rows.Count==1 && !suppliers.Enabled && !UiField<Button>(import,"btnThemNCC").Enabled,"adding import line locks supplier and supplier creation");
                InvokePrivate(import,"LamMoiPhieu");
                Check(suppliers.SelectedIndex==-1 && medicines.SelectedIndex==-1 && !batch.Enabled && suppliers.Enabled,"reset import restores initial empty gated state: supplier="+suppliers.SelectedIndex+", medicine="+medicines.SelectedIndex+", batch="+batch.Enabled+", supplierEnabled="+suppliers.Enabled);
            }
            foreach(var invalid in new[]{"","A B","GIẢM","A' OR 1=1","ABCDEFGHIJKLMNOPQRSTU"})
                Check(!vouchers.Luu(null,invalid,"FIXED",100,1,true,null,out _),"invalid voucher code rejected: "+invalid);
            Check(!vouchers.Luu(null,code,"PERCENT",101,1,true,null,out _),"percentage above 100 rejected");
            Check(!vouchers.Luu(null,code,"FIXED",0,1,true,null,out _),"zero discount rejected");
            Check(!vouchers.Luu(null,code,"FIXED",1,-1,true,null,out _),"negative voucher quantity rejected");
            Check(!vouchers.Luu(null,code,"OTHER",1,1,true,null,out _),"invalid voucher type rejected");
            Check(!vouchers.Luu(null,code,"FIXED",1.001m,1,true,null,out _),"overprecision discount rejected");
            using(var form=new frmVoucher())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvVoucher");
                Check(grid.CurrentCell==null && !UiField<TextBox>(form,"txtCode").Enabled,"voucher opens with no selected editor");
                UiField<Button>(form,"btnThem").PerformClick();UiField<Button>(form,"btnLuu").PerformClick();
                Check(UiField<Label>(form,"lblTrangThai").Text.Length>0,"voucher screen shows validation errors");
                UiField<TextBox>(form,"txtCode").Text=code;UiField<ComboBox>(form,"cboLoai").SelectedValue="FIXED";
                UiField<NumericUpDown>(form,"nudGiaTri").Value=100.50m;UiField<NumericUpDown>(form,"nudSoLuot").Value=4;
                UiField<Button>(form,"btnLuu").PerformClick();
                voucher=Count("SELECT Id FROM Vouchers WHERE Code=@code",("@code",code));
                Check(voucher>0 && vouchers.KiemTra(code.ToLowerInvariant(),out var info,out _) && info!.DiscountValue==101m,"voucher created through UI usable by sales");
                Check(!vouchers.Luu(null,code.ToLowerInvariant(),"FIXED",1,1,true,null,out _),"duplicate voucher code rejected");
                UiField<TextBox>(form,"txtTimKiem").Text=code;UiField<Button>(form,"btnTimKiem").PerformClick();
                Check(grid.Rows.Count==1,"voucher text search returns matching voucher");
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(grid.Columns["Code"].Index,0)});
                UiField<Button>(form,"btnSua").PerformClick();
                Scalar("UPDATE Vouchers SET Quantity=Quantity-1 WHERE Id=@v",("@v",voucher));
                UiField<Button>(form,"btnLuu").PerformClick();
                Check(Count("SELECT Quantity FROM Vouchers WHERE Id=@v",("@v",voucher))==3 && UiField<Label>(form,"lblTrangThai").Text.Contains("vừa được sử dụng"),"stale edit cannot restore a concurrently consumed voucher use");
                ClickBackground(form);
                Check(!UiField<Button>(form,"btnLuu").Enabled && UiField<TextBox>(form,"txtCode").Text=="","voucher background clears edit state");
            }
            Check(vouchers.Luu(voucher,code,"PERCENT",25,3,false,3,out _) && !vouchers.KiemTra(code,out _,out _),"voucher can be locked from sales");
            Check(vouchers.Luu(voucher,code,"PERCENT",25,3,true,3,out _),"voucher can be reactivated");
            Check(vouchers.TimKiem("' OR 1=1 --").Rows.Count==0 && vouchers.TimKiem("[%]_").Rows.Count==0,"voucher search is parameterized and treats wildcards literally");
            var imports=new NhapKhoBLL();
            Check(imports.LuuPhieuNhap(employee,supplier,prefix,new List<NhapKhoItem>{new(){MaThuoc=medicine,TenThuoc=medName,SoLo=prefix,HanDung=DateTime.Today.AddDays(20),SoLuong=4,GiaNhap=1000}},out _,out _,out var error),"new medicine can be imported: "+error);
            Check(new BanHangBLL().ThanhToan(employee,new List<CartItem>{new(){MaThuoc=medicine,SoLuong=1}},code,null,1876m,out int order,out _,out _,out error),"new voucher applies to real trial sale: "+error);
            Check(!vouchers.Xoa(voucher,out error) && error.Contains("hóa đơn"),"referenced voucher cannot be deleted");
            var bills=new HoaDonBLL();var results=bills.TimKiem(code,DateTime.Today,DateTime.Today);
            Check(results.Rows.Count==1 && (int)results.Rows[0]["MaHD"]==order && (decimal)results.Rows[0]["ThucThu"]==1876m,"invoice search/filter returns exact stored net revenue");
            var detail=bills.ChiTiet(order);
            Check(detail.Rows.Count==1 && (string)detail.Rows[0]["TenThuoc"]==medName && (decimal)detail.Rows[0]["ThanhTien"]==2501m,"invoice details preserve sale price and batch");
            Check(bills.TimKiem("HD"+order.ToString("D5")).Rows.Cast<DataRow>().Any(row=>(int)row["MaHD"]==order),"formatted invoice ID is searchable");
            Check(bills.TimKiem("Khách lẻ").Rows.Cast<DataRow>().Any(row=>(int)row["MaHD"]==order),"guest invoices searchable by displayed customer name");
            Check(bills.TimKiem(code,DateTime.Today.AddDays(1),DateTime.Today.AddDays(1)).Rows.Count==0,"invoice date range excludes other days");
            Check(bills.TimKiem("' OR 1=1 --").Rows.Count==0,"invoice keyword safely parameterized");
            bool invalidRange=false;try {bills.TimKiem("",DateTime.Today.AddDays(1),DateTime.Today);} catch(ArgumentException) {invalidRange=true;}
            Check(invalidRange,"reversed invoice date range rejected");
            using(var form=new frmHoaDon())
            {
                Offscreen(form);
                Check(UiField<DataGridView>(form,"dgvHoaDon").CurrentCell==null && UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==0,"invoice form starts without an auto-selected record");
                UiField<TextBox>(form,"txtTimKiem").Text=code;UiField<Button>(form,"btnTimKiem").PerformClick();
                var grid=UiField<DataGridView>(form,"dgvHoaDon");
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,0)});
                Check(UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==1 && UiField<Label>(form,"lblChiTiet").Text.Contains("HD"+order.ToString("D5")),"selecting invoice loads line details and totals");
                CheckLayout(form,"invoice details");Preview(form,"frmHoaDon-detail");
                UiField<TextBox>(form,"txtTimKiem").Text="__ABSENT__";UiField<Button>(form,"btnTimKiem").PerformClick();
                Check(grid.Rows.Count==0 && UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==0,"empty invoice search clears previous details");
            }
            using(var form=new frmHoaDon(order)) {Offscreen(form);Check(UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==1,"dashboard invoice navigation opens requested invoice details");}
            using(var main=new frmMain())
            {
                Offscreen(main);var summary=bills.ThongKeHomNay().Rows[0];
                Check(UiField<Label>(main,"lblSoHoaDon").Text==summary["SoHoaDon"].ToString(),"dashboard today invoice count matches database");
                Check(UiField<Label>(main,"lblDoanhThu").Text==$"{summary["DoanhThu"]:N0} đ","dashboard revenue deducts discounts");
                Check(UiField<Label>(main,"lblSapHet").Text==new CanhBaoBLL().LayThuocSapHet().Rows.Count.ToString() && UiField<Label>(main,"lblSapHetHan").Text==new CanhBaoBLL().LayLoSapHetHan().Rows.Count.ToString(),"dashboard warning counts match stock and expiry views");
                Check(UiField<DataGridView>(main,"dgvGanNhat").Rows.Count<=5 && UiField<DataGridView>(main,"dgvGanNhat").CurrentCell==null,"dashboard recent list capped at five and unselected");
                Check(!UiField<Label>(main,"lblTongQuan").Text.Contains("Chưa tải"),"dashboard loads without hidden data error");
                CheckLayout(main.Controls.Find("dashboard",true).Single(),"dashboard populated");
                Preview(main,"frmMain-populated");
            }
            TestManagementMenu();
        }
        finally
        {
            Scalar(@"DELETE FROM Orders WHERE Id IN (SELECT d.OrderId FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                     DELETE FROM StockIns WHERE Id IN (SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                     DELETE FROM Batches WHERE MedicineId=@m; DELETE FROM Medicines WHERE Id=@m;
                     DELETE FROM Vouchers WHERE Id=@v; DELETE FROM Suppliers WHERE Id=@s;",("@m",medicine),("@v",voucher),("@s",supplier));
            Console.WriteLine("Expansion test fixtures removed; existing trial records retained.");
        }
    }
}
