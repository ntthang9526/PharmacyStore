using System.ComponentModel;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using Store.BLL;
using Store.DAL;
using Store.Shared;
using Store.POS;
using Store.POS.FormBanHang;
using Store.POS.Common;

internal static partial class Program
{
    static void TestPrescriptionsPdf()
    {
        string tag="RX"+Guid.NewGuid().ToString("N")[..8];
        int category=0,supplier=0,rx=0,otc=0,paid=0,draft=0;
        int admin=Count("SELECT Id FROM Employees WHERE Username='admin'");
        var sales=new BanHangBLL();var bills=new HoaDonBLL();var catalog=new ThuocBLL();
        var ids=new List<int>();
        DonThuocInfo Info()=>new(){SoDon="DT-"+tag,BenhNhan="Nguyễn Văn Kiểm Thử",NguoiKeDon="Người kê đơn thử nghiệm",NgayKeDon=DateTime.Today,DaKiemTra=true};
        List<CartItem> Items(int quantity=3,string dosage="Hướng dẫn được ghi theo đơn thử nghiệm.")=>[new(){MaThuoc=rx,SoLuong=quantity,HuongDan=dosage}];
        bool Save(List<CartItem> items,DonThuocInfo? info,bool pending,out string error)
        {
            bool ok=sales.ThanhToan(admin,items,null,null,items.Sum(i=>i.SoLuong)*1000m,out int id,out _,out _,out error,choThanhToan:pending,donThuoc:info);
            if(ok) ids.Add(id);return ok;
        }
        int StockRx()=>Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@m",("@m",rx));
        string output="artifacts/prescriptions-pdf";Directory.CreateDirectory(output);
        try
        {
            category=Count("INSERT INTO Categories(Name) VALUES(@n);SELECT CAST(SCOPE_IDENTITY() AS int)",("@n",tag));
            supplier=Count("INSERT INTO Suppliers(Name,Phone) VALUES(@n,'0901234567');SELECT CAST(SCOPE_IDENTITY() AS int)",("@n",tag));
            Check(catalog.ThemNhanhThuoc(tag+" Thuốc kê đơn",category,supplier,"Hộp",1000,out rx,out string error,keDon:true),"create prescription medicine: "+error);
            Check(catalog.ThemNhanhThuoc(tag+" Thuốc thường",category,supplier,"Hộp",1000,out otc,out error),"create non-prescription medicine: "+error);
            Check(Count("SELECT CAST(IsPrescription AS int) FROM Medicines WHERE Id=@id",("@id",rx))==1,"quick-add persists prescription classification");
            Check(catalog.SuaThuoc(rx.ToString(),tag+" Thuốc kê đơn",category.ToString(),"Hộp","1000","0",supplier,out error),"old catalog callers preserve existing classification");
            Check(Count("SELECT CAST(IsPrescription AS int) FROM Medicines WHERE Id=@id",("@id",rx))==1,"omitted optional flag does not silently unset prescription medicine");
            Check(new NhapKhoBLL().LuuPhieuNhap(admin,supplier,tag,new List<NhapKhoItem>{
                new(){MaThuoc=rx,TenThuoc=tag,SoLo=tag+"-A",HanDung=DateTime.Today.AddDays(30),SoLuong=2,GiaNhap=500},
                new(){MaThuoc=rx,TenThuoc=tag,SoLo=tag+"-B",HanDung=DateTime.Today.AddDays(60),SoLuong=10,GiaNhap=500},
                new(){MaThuoc=otc,TenThuoc=tag,SoLo=tag+"-C",HanDung=DateTime.Today.AddDays(60),SoLuong=5,GiaNhap=500}},out _,out _,out error),"stock fixtures: "+error);
            int before=StockRx();int orderCount=Count("SELECT COUNT(*) FROM Orders");
            Check(!Save(Items(),null,false,out error),"prescription sale rejects missing prescription metadata");
            Check(!new BanHangDAL().TaoHoaDon(admin,null,null,3000,0,Items(),out _,out error),"DAL independently rejects bypass without prescription");
            var bad=Info();bad.DaKiemTra=false;
            Check(!Save(Items(),bad,false,out error),"unverified prescription rejected");
            bad=Info();bad.NgayKeDon=DateTime.Today.AddDays(1);
            Check(!Save(Items(),bad,false,out error),"future prescription date rejected");
            Check(!Save(Items(dosage:""),Info(),false,out error),"missing usage instruction rejected");
            Check(!Save(Items(dosage:new string('x',501)),Info(),false,out error),"oversized instruction rejected before SQL truncation");
            Check(StockRx()==before && Count("SELECT COUNT(*) FROM Orders")==orderCount,"failed validations leave stock and invoice count unchanged");
            Check(Save(Items(),null,true,out error),"incomplete prescription may be saved as unpaid draft: "+error);draft=ids.Last();
            Check(!bills.KiemTraDonThuocCho(draft,out error) && error.Contains("Sửa đơn"),"pending payment preflight directs user to complete prescription");
            Check(!bills.ThanhToanCho(draft,"Tiền mặt",3000,out error),"pending payment also rejects missing prescription in DAL");
            Check(StockRx()==before,"incomplete pending prescription does not reserve or deduct stock");
            var partial=new DonThuocInfo{BenhNhan="Người bệnh đang nhập dở"};
            Check(Save(Items(dosage:""),partial,true,out error),"partial prescription with missing number/doctor/dosage can be saved as draft: "+error);
            int partialId=ids.Last();
            var partialData=bills.LayDonCho(partialId);var partialInfo=HoaDonBLL.DocDonThuoc(partialData.Tables[0].Rows[0]);
            Check(partialInfo?.BenhNhan==partial.BenhNhan && partialInfo.SoDon=="" && !partialInfo.DaKiemTra,"loading draft preserves partial fields without a prescription number");
            Check(Count("SELECT COUNT(*) FROM Orders WHERE Id=@id AND PrescriptionCheckedBy IS NULL AND PrescriptionCheckedAt IS NULL",("@id",partialId))==1,"saving draft never records a reviewer or review time");
            Check(!bills.ThanhToanCho(partialId,"Tiền mặt",3000,out error) && StockRx()==before,"partial prescription remains blocked at payment with no stock deduction");
            var tooLong=new DonThuocInfo{BenhNhan=new string('X',151)};
            Check(!Save(Items(dosage:""),tooLong,true,out error),"draft still validates field lengths before SQL truncation");
            using(var editor=new frmBanHang(partialId))
            {Offscreen(editor);Check(UiField<DonThuocInfo?>(editor,"donThuoc")?.BenhNhan==partial.BenhNhan,"pending editor restores unfinished prescription fields");}
            using(var partialForm=new frmDonThuoc(partial,new[]{new RowData{Id=rx,Name=tag,IsPrescription=true}}))
            {
                Offscreen(partialForm);UiField<Button>(partialForm,"btnLuuNhap").PerformClick();
                Check(partialForm.DialogResult==DialogResult.OK && partialForm.DonThuoc?.BenhNhan==partial.BenhNhan && !partialForm.DonThuoc.DaKiemTra && partialForm.HuongDan[rx]=="","Save draft returns partial prescription and blank dosage without requiring confirmation");
            }
            var pendingData=bills.LayDonCho(draft);byte[] version=(byte[])pendingData.Tables[0].Rows[0]["OrderVersion"];
            Scalar("UPDATE Medicines SET IsPrescription=0 WHERE Id=@id",("@id",rx));
            Check(sales.ThanhToan(admin,Items(),null,null,3000,out _,out _,out _,out error,"Chưa xác định",true,draft,version),"editing draft survives later classification change");
            Check(!bills.KiemTraDonThuocCho(draft,out error) && Count("SELECT COUNT(*) FROM OrderPendingItems WHERE OrderId=@id AND IsPrescriptionSnapshot=1",("@id",draft))==1,"edit cannot erase captured prescription requirement");
            Scalar("UPDATE Medicines SET IsPrescription=1 WHERE Id=@id",("@id",rx));
            version=(byte[])bills.LayDonCho(draft).Tables[0].Rows[0]["OrderVersion"];
            Check(sales.ThanhToan(admin,Items(),null,null,3000,out _,out _,out _,out error,"Chưa xác định",true,draft,version,Info()),"edit pending saves verified prescription: "+error);
            Check(bills.KiemTraDonThuocCho(draft,out error),"completed pending prescription passes preflight: "+error);
            Check(Count("SELECT COUNT(*) FROM Orders WHERE Id=@id AND PrescriptionCheckedBy=@e AND PrescriptionCheckedAt IS NOT NULL",("@id",draft),("@e",admin))==1,"reviewer and review time saved");
            var loaded=bills.LayDonCho(draft);
            Check(HoaDonBLL.DocDonThuoc(loaded.Tables[0].Rows[0])?.SoDon==Info().SoDon && loaded.Tables[1].Rows.Cast<DataRow>().All(r=>(string)r["HuongDan"]==Items()[0].HuongDan),"reloading pending preserves metadata and instructions");
            using(var editor=new frmBanHang(draft))
            {
                Offscreen(editor);var cart=UiField<BindingList<RowData>>(editor,"gioHang");var review=UiField<DonThuocInfo>(editor,"donThuoc");
                Check(review.DaKiemTra,"loading unchanged verified draft preserves its confirmation");
                cart.Add(new RowData{Id=otc,Name="OTC",Quantity=1,Price=1000});
                Check(review.DaKiemTra,"adding ordinary medicine leaves prescription confirmation unchanged");
                cart[0].Quantity++;cart.ResetItem(0);
                Check(!review.DaKiemTra && UiField<Button>(editor,"btnDonThuoc").Text.Contains("*"),"changing prescription quantity removes confirmation and marks recheck required");
                review.DaKiemTra=true;cart.ResetBindings();Check(review.DaKiemTra,"redrawing an unchanged cart does not remove confirmation");
                cart[0].HuongDan="Changed instructions";cart.ResetItem(0);Check(!review.DaKiemTra,"changing dosage instructions also requires recheck");
                review.DaKiemTra=true;cart.RemoveAt(0);Check(!review.DaKiemTra,"removing a prescription medicine invalidates prior confirmation");
                review.DaKiemTra=true;cart.Add(new RowData{Id=rx,Name=tag,IsPrescription=true,Quantity=1,Price=1000});Check(!review.DaKiemTra,"adding prescription medicine invalidates prior confirmation");
            }
            var unverified=Info();unverified.DaKiemTra=false;
            Check(sales.ThanhToan(admin,Items(4),null,null,4000,out _,out _,out _,out error,"Chưa xác định",true,draft,(byte[])loaded.Tables[0].Rows[0]["OrderVersion"],unverified),"modified prescription saves as unverified pending draft");
            Check(Count("SELECT COUNT(*) FROM Orders WHERE Id=@id AND PrescriptionCheckedBy IS NULL AND PrescriptionCheckedAt IS NULL",("@id",draft))==1 && !bills.ThanhToanCho(draft,"Tiền mặt",4000,out error),"modified unverified draft clears reviewer and cannot be paid");
            loaded=bills.LayDonCho(draft);
            Check(sales.ThanhToan(admin,Items(),null,null,3000,out _,out _,out _,out error,"Chưa xác định",true,draft,(byte[])loaded.Tables[0].Rows[0]["OrderVersion"],Info()),"freshly reviewed pending draft can be saved again");
            loaded=bills.LayDonCho(draft);
            Check(bills.ThanhToanCho(draft,"Tiền mặt",3000,out error,(byte[])loaded.Tables[0].Rows[0]["OrderVersion"]),"pay completed pending prescription: "+error);paid=draft;
            Check(StockRx()==before-3,"prescription sale deducts stock exactly once");
            Check(!bills.ThanhToanCho(paid,"Tiền mặt",3000,out error),"paid prescription cannot be charged twice");
            var detail=bills.ChiTiet(paid);
            Check(detail.Rows.Count==2 && detail.Rows.Cast<DataRow>().All(r=>Convert.ToBoolean(r["KeDon"]) && (string)r["HuongDan"]==Items()[0].HuongDan),"FEFO split persists instruction and classification on every batch line");
            Scalar("UPDATE Medicines SET Name=@n,IsPrescription=0 WHERE Id=@id",("@n",tag+" Đã đổi tên"),("@id",rx));
            var print=bills.LayBanIn(paid);
            Check(print.Tables[1].Rows.Cast<DataRow>().All(r=>((string)r["TenThuoc"]).EndsWith("Thuốc kê đơn") && Convert.ToBoolean(r["KeDon"])),"reprint uses medicine name and prescription flag captured at sale");
            var helper=typeof(frmMain).Assembly.GetType("Store.POS.Common.PdfInvoiceHelper")!;
            byte[] Pdf(DataSet data)=>(byte[])helper.GetMethod("TaoBanIn",BindingFlags.Static|BindingFlags.Public)!.Invoke(null,new object[]{data})!;
            byte[] pdf=Pdf(print);File.WriteAllBytes(Path.Combine(output,"hoa-don-ke-don.pdf"),pdf);
            Check(System.Text.Encoding.ASCII.GetString(pdf,0,5)=="%PDF-" && pdf.Length>1000,"actual PDF generated from persisted paid invoice");
            var wrong=print.Copy();wrong.Tables[0].Rows[0]["TongTien"]=1m;
            bool invalidTotals=false;try {Pdf(wrong);} catch(TargetInvocationException ex) when(ex.InnerException is InvalidOperationException) {invalidTotals=true;}
            Check(invalidTotals,"PDF refuses inconsistent invoice totals");
            var many=print.Copy();var template=many.Tables[1].Rows[0].ItemArray;
            for(int i=0;i<55;i++) {var row=many.Tables[1].NewRow();row.ItemArray=(object[])template.Clone();row["HuongDan"]=new string('x',480);many.Tables[1].Rows.Add(row);}
            many.Tables[0].Rows[0]["TongTien"]=many.Tables[1].Rows.Cast<DataRow>().Sum(r=>Convert.ToDecimal(r["ThanhTien"]));
            many.Tables[0].Rows[0]["ThucThu"]=many.Tables[0].Rows[0]["TongTien"];
            File.WriteAllBytes(Path.Combine(output,"hoa-don-nhieu-trang.pdf"),Pdf(many));
            Check(new FileInfo(Path.Combine(output,"hoa-don-nhieu-trang.pdf")).Length>pdf.Length,"long multi-page PDF renders without layout exception");
            Check(Save(new List<CartItem>{new(){MaThuoc=otc,SoLuong=1}},null,false,out error),"non-prescription sale remains unchanged: "+error);
            File.WriteAllBytes(Path.Combine(output,"hoa-don-thuong.pdf"),Pdf(bills.LayBanIn(ids.Last())));
            Scalar("UPDATE Medicines SET IsPrescription=1 WHERE Id=@id",("@id",rx));
            Check(Save(Items(1),null,true,out error),"second draft created for print restrictions");draft=ids.Last();
            bool blocked=false;try {bills.LayBanIn(draft);} catch(InvalidOperationException){blocked=true;}
            Check(blocked,"cannot issue paid PDF for unpaid invoice");
            loaded=bills.LayDonCho(draft);
            Check(bills.HuyHoaDonCho(draft,(byte[])loaded.Tables[0].Rows[0]["OrderVersion"],"Kiểm thử",out error),"cancel test draft");
            blocked=false;try {bills.LayBanIn(draft);} catch(InvalidOperationException){blocked=true;}
            Check(blocked,"cancelled invoice cannot be printed as paid");
            using(var form=new frmDonThuoc(null,new[]{new RowData{Id=rx,Name="Thuốc kê đơn thử nghiệm",IsPrescription=true}}))
            {
                Offscreen(form);UiField<Button>(form,"btnLuu").PerformClick();
                Check(UiField<Label>(form,"lblLoi").Text.Length>0 && form.DialogResult==DialogResult.None,"prescription form validates before closing");
                UiField<TextBox>(form,"txtSoDon").Text=Info().SoDon;UiField<TextBox>(form,"txtBenhNhan").Text=Info().BenhNhan;UiField<TextBox>(form,"txtNguoiKeDon").Text=Info().NguoiKeDon;
                UiField<CheckBox>(form,"chkDaKiemTra").Checked=true;
                UiField<Label>(form,"lblLoi").Text="";
                ((BindingList<RowData>)UiField<DataGridView>(form,"dgvHuongDan").DataSource!)[0].HuongDan=Items()[0].HuongDan;
                using var image=new Bitmap(form.Width,form.Height);form.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save(Path.Combine(output,"don-thuoc.png"));
                UiField<Button>(form,"btnLuu").PerformClick();Check(form.DonThuoc?.DaKiemTra==true && form.HuongDan[rx]==Items()[0].HuongDan,"prescription form returns verified fields and per-medicine instruction");
            }
            using(var reviewed=new frmDonThuoc(Info(),new[]{new RowData{Id=rx,Name=tag,IsPrescription=true,HuongDan="Original instructions"}}))
            {
                Offscreen(reviewed);UiField<TextBox>(reviewed,"txtBenhNhan").Text="Changed patient";
                Check(!UiField<CheckBox>(reviewed,"chkDaKiemTra").Checked,"editing prescription metadata clears the checkbox");
                UiField<CheckBox>(reviewed,"chkDaKiemTra").Checked=true;
                UiField<DataGridView>(reviewed,"dgvHuongDan").Rows[0].Cells["colHuongDan"].Value="Changed dosage";
                Check(!UiField<CheckBox>(reviewed,"chkDaKiemTra").Checked,"editing prescription grid clears the checkbox");
                UiField<CheckBox>(reviewed,"chkDaKiemTra").Checked=true;UiField<Button>(reviewed,"btnLuuNhap").PerformClick();
                Check(reviewed.DonThuoc?.DaKiemTra==false,"Save draft never claims verified even when the checkbox was checked");
            }
            using(var invoiceForm=new frmHoaDon())
            {
                Offscreen(invoiceForm);var grid=UiField<DataGridView>(invoiceForm,"dgvHoaDon");
                var rows=grid.Rows.Cast<DataGridViewRow>();
                typeof(frmHoaDon).GetMethod("HienChiTiet",PrivateInstance)!.Invoke(invoiceForm,new[]{rows.First(r=>Convert.ToInt32(r.Cells["MaHD"].Value)==paid)});
                Check(UiField<Button>(invoiceForm,"btnInPdf").Enabled,"paid invoice enables In / PDF");
                typeof(frmHoaDon).GetMethod("HienChiTiet",PrivateInstance)!.Invoke(invoiceForm,new[]{rows.First(r=>Convert.ToInt32(r.Cells["MaHD"].Value)==draft)});
                Check(!UiField<Button>(invoiceForm,"btnInPdf").Enabled,"cancelled invoice disables In / PDF");
            }
            using(var form=new frmBanHang())
            {
                Offscreen(form);
                var cart=UiField<BindingList<RowData>>(form,"gioHang");
                cart.Add(new RowData{Id=rx,Name=tag,Price=1000,Quantity=1,IsPrescription=true});
                typeof(frmBanHang).GetField("donThuoc",PrivateInstance)!.SetValue(form,Info());
                cart.Clear();Application.DoEvents();
                Check(typeof(frmBanHang).GetField("donThuoc",PrivateInstance)!.GetValue(form)==null,"emptying cart clears previous patient prescription");
            }
            using(var readOnly=new frmDonThuoc(Info(),new[]{new RowData{Id=rx,Name=tag,IsPrescription=true,HuongDan=Items()[0].HuongDan}},true))
            {
                Offscreen(readOnly);
                Check(UiField<TextBox>(readOnly,"txtSoDon").ReadOnly && UiField<DataGridView>(readOnly,"dgvHuongDan").ReadOnly && !UiField<CheckBox>(readOnly,"chkDaKiemTra").Enabled,"history prescription opens without editing paid records");
            }
            var excel=Path.Combine(output,"mau-thuoc.xlsx");ExcelHelper.XuatFileMauNhapThuoc(excel,false);
            var import=ExcelHelper.DocDanhSachThuocTuExcel(excel,out var errors);
            Check(errors.Count==0 && import.Count==2 && !import[0].KeDon && import[1].KeDon,"Excel template preserves prescription flag");
            using(var workbook=new XLWorkbook(excel)) {workbook.Worksheet(1).Cell(2,7).Value="invalid";workbook.Save();}
            import=ExcelHelper.DocDanhSachThuocTuExcel(excel,out errors);
            Check(errors.Count==1 && import.Count==1,"invalid prescription Excel flag rejected instead of silently changed");
        }
        finally
        {
            foreach(int id in ids) {Scalar("DELETE FROM OrderDetails WHERE OrderId=@id;DELETE FROM OrderPendingItems WHERE OrderId=@id;DELETE FROM Orders WHERE Id=@id",("@id",id));}
            Scalar("DELETE d FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId IN (@rx,@otc);DELETE FROM Batches WHERE MedicineId IN (@rx,@otc);DELETE FROM StockIns WHERE Note=@tag;DELETE FROM Medicines WHERE Id IN (@rx,@otc);DELETE FROM Suppliers WHERE Id=@sup;DELETE FROM Categories WHERE Id=@cat",("@rx",rx),("@otc",otc),("@tag",tag),("@sup",supplier),("@cat",category));
        }
    }
}
