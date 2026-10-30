using System.Data;
using System.ComponentModel;
using Store.BLL;
using Store.DAL;
using Store.Shared;
using Store.POS;
using Store.POS.FormBanHang;

internal static partial class Program
{
    static void TestPendingActions()
    {
        string tag="PENDING_"+Guid.NewGuid().ToString("N")[..8];
        int admin=PhanQuyenDAL.NhanVienId,group=0,supplier=0,med=0,other=0,staff=0,voucher=0;
        var ids=new List<int>();var sales=new BanHangBLL();var invoices=new HoaDonBLL();
        List<CartItem> Cart(int q)=>new(){new(){MaThuoc=med,SoLuong=q}};
        DataRow Row(int id)=>new HoaDonDAL().TimKiem("HD"+id.ToString("D5"),null,null).Rows.Cast<DataRow>().Single(x=>(int)x["MaHD"]==id);
        byte[] Version(int id)=>(byte[])Row(id)["OrderVersion"];
        int Stock()=>Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId IN(@m,@o)",("@m",med),("@o",other));
        decimal Revenue()=>Convert.ToDecimal(invoices.ThongKeHomNay().Rows[0]["DoanhThu"]);
        int Draft(int q)
        {
            Check(sales.ThanhToan(PhanQuyenDAL.NhanVienId,Cart(q),null,null,q*1000,out int id,out _,out _,out var error,"Chưa xác định",true),"create editable pending order: "+error);
            ids.Add(id);return id;
        }
        bool Edit(int id,byte[] version,int q,out string error)=>sales.ThanhToan(PhanQuyenDAL.NhanVienId,Cart(q),null,null,q*1000,
            out _,out _,out _,out error,"Chưa xác định",true,id,version);
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out group,out var error),"pending category");
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"pending supplier");
            Check(new ThuocBLL().ThemNhanhThuoc(tag,group,supplier,"Viên",1000,out med,out error),"pending medicine");
            Check(new ThuocBLL().ThemNhanhThuoc(tag+" Other",group,supplier,"Viên",2000,out other,out error),"second medicine");
            Check(new NhanVienBLL().Luu(null,tag,tag.ToLowerInvariant(),"Test123!","",true,admin,out staff,out error),"pending staff");
            Check(new NhapKhoBLL().LuuPhieuNhap(admin,supplier,tag,new List<NhapKhoItem>{
                new(){MaThuoc=med,SoLo=tag+"M",HanDung=DateTime.Today.AddDays(40),SoLuong=20,GiaNhap=500},
                new(){MaThuoc=other,SoLo=tag+"O",HanDung=DateTime.Today.AddDays(40),SoLuong=20,GiaNhap=500}},out _,out _,out error),"pending stock fixtures");
            Check(new VoucherBLL().Luu(null,tag,"FIXED",100,4,true,null,out error),"pending voucher");
            voucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",tag));
            int stock=Stock();decimal revenue=Revenue();SetTestSession(staff);
            int id=Draft(2);var initialVersion=Version(id);
            int beforeCount=Count("SELECT COUNT(*) FROM Orders");
            Check(Edit(id,initialVersion,3,out error),"staff edits existing pending order: "+error);
            Check(Count("SELECT COUNT(*) FROM Orders")==beforeCount && (decimal)Row(id)["TongTien"]==3000,"edit retains invoice ID without creating another invoice");
            Check((string)Row(id)["PaymentStatus"]=="PENDING" && Row(id)["ModifiedAt"] is DateTime && (string)Row(id)["NhanVienSua"]==tag,"edit records employee/time and stays pending");
            Check(Stock()==stock && Revenue()==revenue,"edit changes neither stock nor revenue");
            Check(!Edit(id,initialVersion,4,out error) && error.Contains("thay đổi"),"stale editor cannot overwrite newer edit");
            Check(!invoices.HuyHoaDonCho(id,initialVersion,"Stale cancellation",out error),"stale cancel is rejected");
            Check(!invoices.ThanhToanCho(id,"Tiền mặt",3000,out error,initialVersion),"stale payment version is rejected even with matching amount");
            var version=Version(id);
            Check(!sales.ThanhToan(staff,Cart(0),null,null,0,out _,out _,out _,out error,"Chưa xác định",true,id,version),"empty/zero quantity edit rejected");
            Check(!sales.ThanhToan(staff,Cart(4),null,null,1,out _,out _,out _,out error,"Chưa xác định",true,id,version),"incorrect edited total rejected");
            Check(!sales.ThanhToan(staff,Cart(4),tag+"INVALID",null,4000,out _,out _,out _,out error,"Chưa xác định",true,id,version),"invalid voucher edit rejected");
            Check(Version(id).SequenceEqual(version) && (int)invoices.LayDonCho(id).Tables[1].Rows[0]["Quantity"]==3,"failed edits leave header and lines unchanged");

            using(var editor=new frmBanHang(id))
            {
                Offscreen(editor);
                Check(UiField<BindingList<RowData>>(editor,"gioHang").Single().Quantity==3,"pending editor restores existing items");
                Check(UiField<Button>(editor,"btnLuuCho").Text=="Lưu thay đổi" && !UiField<Button>(editor,"btnThanhToan").Visible,"edit mode exposes save rather than a new checkout");
                UiField<TextBox>(editor,"boxNhapSanPham").Text=other.ToString();UiField<Button>(editor,"btnNhapSanPham").PerformClick();
                Check(UiField<BindingList<RowData>>(editor,"gioHang").Count==2,"pending editor can add another medicine");
                editor.Close();
            }
            Check(Version(id).SequenceEqual(version) && invoices.LayDonCho(id).Tables[1].Rows.Count==1,"closing editor without saving keeps stored invoice intact");

            var replaced=new List<CartItem>{new(){MaThuoc=other,SoLuong=2}};
            Check(sales.ThanhToan(staff,replaced,tag,null,3900,out int sameId,out _,out _,out error,"Chưa xác định",true,id,version) && sameId==id,"edit replaces removed medicine, adds another, and changes voucher");
            Check(invoices.LayDonCho(id).Tables[1].Rows.Count==1 && (int)invoices.LayDonCho(id).Tables[1].Rows[0]["MedicineId"]==other,"old pending lines are replaced atomically");
            Check(Count("SELECT Quantity FROM Vouchers WHERE Id=@v",("@v",voucher))==4 && Stock()==stock,"editing voucher consumes no usage and no stock");

            version=Version(id);
            Check(!invoices.HuyHoaDonCho(id,version," ",out error) && !invoices.HuyHoaDonCho(id,version,new string('x',251),out error),"cancel reason is required and length limited");
            Check(invoices.HuyHoaDonCho(id,version,"Khách đổi ý",out error),"staff cancels pending order: "+error);
            var cancelled=Row(id);
            Check((string)cancelled["TrangThai"]=="Đã hủy" && (decimal)cancelled["ThucThu"]==0 && (decimal)cancelled["CanThanhToan"]==0,"cancelled order has its own status, zero collected, zero due");
            Check((string)cancelled["CancelReason"]=="Khách đổi ý" && cancelled["CancelledAt"] is DateTime && (string)cancelled["NhanVienHuy"]==tag,"cancellation reason, time and employee retained");
            Check(invoices.ChiTiet(id).Rows.Count==1 && (string)invoices.ChiTiet(id).Rows[0]["TenThuoc"]==tag+" Other","cancelled order retains readable item history");
            Check(!invoices.TimKiem("",null,null,true).Rows.Cast<DataRow>().Any(x=>(int)x["MaHD"]==id),"pending-only list excludes cancelled orders");
            Check(!Edit(id,Version(id),1,out error) && !invoices.HuyHoaDonCho(id,Version(id),"Again",out error) && !invoices.ThanhToanCho(id,"Tiền mặt",3900,out error),"cancelled order cannot be edited, cancelled twice, or paid");
            Check(Stock()==stock && Revenue()==revenue && Count("SELECT Quantity FROM Vouchers WHERE Id=@v",("@v",voucher))==4,"cancellation changes no inventory, voucher usage or revenue");

            int paid=Draft(1);Check(invoices.ThanhToanCho(paid,"Tiền mặt",1000,out error,Version(paid)),"normal payment remains available");
            int paidStock=Stock();decimal paidRevenue=Revenue();
            Check(!Edit(paid,Version(paid),2,out error) && !invoices.HuyHoaDonCho(paid,Version(paid),"Cannot cancel paid",out error),"paid order rejects editing and cancellation");
            Check(Stock()==paidStock && Revenue()==paidRevenue,"rejected paid-order changes preserve settled stock/revenue");
            int concurrent=Draft(1);version=Version(concurrent);
            var results=Task.WhenAll(Task.Run(()=>Edit(concurrent,version,2,out _)),Task.Run(()=>Edit(concurrent,version,3,out _))).GetAwaiter().GetResult();
            Check(results.Count(x=>x)==1,"two editors using one version commit only one update");
            int race=Draft(1);version=Version(race);
            results=Task.WhenAll(Task.Run(()=>invoices.HuyHoaDonCho(race,version,"Race",out _)),
                Task.Run(()=>invoices.ThanhToanCho(race,"Tiền mặt",1000,out _,version))).GetAwaiter().GetResult();
            Check(results.Count(x=>x)==1,"cancel versus payment commits exactly one outcome");
            int shortage=Draft(50);Check(Edit(shortage,Version(shortage),40,out error),"unreserved draft may be edited even when stock is insufficient");
            int longCancelled=Draft(1);string longReason=new string('L',250);
            Check(invoices.HuyHoaDonCho(longCancelled,Version(longCancelled),longReason,out error),"maximum-length cancellation reason is accepted");
            SetTestSession(0);
            Check(!Edit(shortage,Version(shortage),1,out error) && !invoices.HuyHoaDonCho(shortage,Version(shortage),"Anonymous",out error),"anonymous edits/cancels are denied");
            SetTestSession(staff);
            Directory.CreateDirectory("artifacts/pending-actions");
            using(var main=new frmMain())
            {
                Offscreen(main);
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmHoaDon)).Invoke(main,null);
                var form=(frmHoaDon)UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmHoaDon)];
                form.MoHoaDon(shortage);
                var method=UiField<ComboBox>(form,"cboHinhThuc");
                var payButton=UiField<Button>(form,"btnThanhToanCho");
                Check((string?)method.SelectedItem=="Tiền mặt" && payButton.Enabled,"pending payment defaults to cash");
                int beforeInvalidStock=Stock();decimal beforeInvalidRevenue=Revenue();
                method.SelectedIndex=-1;
                Check(!payButton.Enabled,"clearing payment method disables checkout");
                payButton.PerformClick();
                Check((string)Row(shortage)["PaymentStatus"]=="PENDING" && Stock()==beforeInvalidStock && Revenue()==beforeInvalidRevenue,
                    "checkout with no method leaves invoice, stock and revenue unchanged");
                method.SelectedIndex=1;form.LamMoiDuLieu();
                Check((string?)method.SelectedItem=="Chuyển khoản QR" && payButton.Enabled,"explicit QR choice survives invoice refresh");
                method.SelectedIndex=0;
                Check(payButton.Enabled,"choosing cash enables checkout again");
                Check(UiField<Button>(form,"btnSuaCho").Visible && UiField<Button>(form,"btnHuyCho").Visible,"selected pending order shows edit/cancel buttons");
                foreach(var size in new[]{new Size(1500,820),new Size(1382,753),new Size(1100,700)})
                {
                    main.ClientSize=size;Application.DoEvents();CheckLayout(form,"pending actions "+size.Width);
                    foreach(string name in new[]{"btnSuaCho","btnHuyCho","btnThanhToanCho","cboHinhThuc"})
                    {
                        var control=UiField<Control>(form,name);
                        Check(control.Right<=control.Parent!.ClientSize.Width,"pending action fits toolbar "+name+" "+size.Width);
                    }
                }
                using(var image=new Bitmap(main.Width,main.Height)) {main.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/pending-actions/invoice-pending.png");}
                form.MoHoaDon(id);
                Check(!UiField<Button>(form,"btnSuaCho").Visible && !UiField<Button>(form,"btnHuyCho").Visible && UiField<Label>(form,"lblChiTiet").Text.Contains("Khách đổi ý"),"cancelled selection hides actions and shows reason");
                form.MoHoaDon(longCancelled);Application.DoEvents();
                var details=UiField<DataGridView>(form,"dgvChiTiet");
                Check(details.GetRowDisplayRectangle(0,true).Height>=details.Rows[0].Height-2 &&
                    UiField<ToolTip>(form,"chiTietHint").GetToolTip(UiField<Label>(form,"lblChiTiet"))?.Contains(longReason)==true,
                    "long cancellation reason remains available without hiding invoice lines");
                main.Dispose();
            }
            using(var editor=new frmBanHang(shortage))
            {
                Offscreen(editor);Check(UiField<Button>(editor,"btnLuuCho").Enabled,"editor permits saving unreserved stock-short draft");
                using var image=new Bitmap(editor.Width,editor.Height);editor.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/pending-actions/edit-pending.png");
            }
            using(var dialog=new frmHuyHoaDonCho(shortage))
            {
                Offscreen(dialog);UiField<TextBox>(dialog,"txtLyDo").Text="Khách không mua nữa";Check(dialog.LyDo=="Khách không mua nữa","cancel dialog captures reason");
                using var image=new Bitmap(dialog.Width,dialog.Height);dialog.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save("artifacts/pending-actions/cancel-pending.png");
            }
            using(var alerts=new frmCanhBao())
                Check(UiField<DataGridViewColumn>(alerts,"colDaXuLy").HeaderText=="Đã xem" && UiField<DataGridViewColumn>(alerts,"colDaXuLy").ToolTipText.Contains("không khóa"),"alert label explicitly means viewed, not stock processing");
        }
        finally
        {
            SetTestSession(admin);
            foreach(int id in ids) Scalar("DELETE FROM Orders WHERE Id=@id",("@id",id));
            Scalar(@"DELETE FROM StockIns WHERE Id IN(SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId IN(@m,@o));
                DELETE FROM Batches WHERE MedicineId IN(@m,@o);DELETE FROM Medicines WHERE Id IN(@m,@o);
                DELETE FROM Vouchers WHERE Id=@v;DELETE FROM Employees WHERE Id=@e;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;",
                ("@m",med),("@o",other),("@v",voucher),("@e",staff),("@s",supplier),("@g",group));
        }
    }
}

