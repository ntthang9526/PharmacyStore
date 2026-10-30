using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.POS.Common;
using Store.Shared;

internal static partial class Program
{
    static void TestReviewFixes()
    {
        string tag="F4"+Guid.NewGuid().ToString("N")[..9].ToUpperInvariant(), folder="artifacts/review-fixes";
        Directory.CreateDirectory(folder);
        int admin=Count("SELECT Id FROM Employees WHERE Username='admin'"),category=0,supplier=0,med=0,customer=0,otherCustomer=0,creator=0,collector=0,voucher=0,otherVoucher=0;
        var orders=new List<int>();var sales=new BanHangBLL();var invoices=new HoaDonBLL();
        string creatorName="Người lập "+tag,collectorName="Người thu "+tag,customerName="Khách lúc mua "+tag,otherName="Khách sửa đơn "+tag;
        string phone="09"+Random.Shared.Next(10000000,99999999),otherPhone="08"+Random.Shared.Next(10000000,99999999);
        string firstCode=tag+"A",secondCode=tag+"C";
        List<CartItem> Cart()=>[new(){MaThuoc=med,SoLuong=1,HuongDan="Hướng dẫn được nhập theo đơn thử nghiệm."}];
        DonThuocInfo Prescription()=>new(){SoDon="DT-"+tag,BenhNhan="Người bệnh thử nghiệm",NguoiKeDon="Người kê thử nghiệm",NgayKeDon=DateTime.Today,DaKiemTra=true};
        DataRow Bill(int id)=>invoices.TimKiem("HD"+id.ToString("D5")).Rows.Cast<DataRow>().Single(r=>(int)r["MaHD"]==id);
        int Sale(int actor,int? person,string? code,decimal due,bool pending=false)
        {
            SetTestSession(actor);
            Check(sales.ThanhToan(actor,Cart(),code,person,due,out int id,out _,out _,out string error,choThanhToan:pending,donThuoc:Prescription()),"fixture invoice saves: "+error);
            orders.Add(id);return id;
        }
        void Photo(Form form,string name) {using var image=new Bitmap(form.Width,form.Height);form.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));image.Save(Path.Combine(folder,name+".png"));}
        try
        {
            Check(new NhomThuocBLL().ThemNhanhNhom(tag,out category,out string error),"review category: "+error);
            Check(new NhaCungCapBLL().ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"review supplier: "+error);
            Check(new ThuocBLL().ThemNhanhThuoc(tag,category,supplier,"Hộp",1000,out med,out error,keDon:true),"review medicine: "+error);
            Check(new NhapKhoBLL().LuuPhieuNhap(admin,supplier,tag,new List<NhapKhoItem>{new(){MaThuoc=med,SoLo=tag,HanDung=DateTime.Today.AddDays(2),SoLuong=40,GiaNhap=500}},out _,out _,out error),"review stock: "+error);
            Check(new KhachHangBLL().ThemMoiKhachHang("",customerName,phone,"0",out error),"review customer: "+error);
            customer=Count("SELECT Id FROM Customers WHERE Phone=@p",("@p",phone));
            Check(new KhachHangBLL().ThemMoiKhachHang("",otherName,otherPhone,"0",out error),"review second customer: "+error);
            otherCustomer=Count("SELECT Id FROM Customers WHERE Phone=@p",("@p",otherPhone));
            Check(new NhanVienBLL().Luu(null,creatorName,(tag+"a").ToLowerInvariant(),"Test123!","",true,admin,out creator,out error),"review creator: "+error);
            Check(new NhanVienBLL().Luu(null,collectorName,(tag+"b").ToLowerInvariant(),"Test123!","",true,admin,out collector,out error),"review collector: "+error);
            Check(new VoucherBLL().Luu(null,firstCode,"FIXED",100,5,true,null,out error),"review voucher: "+error);
            voucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",firstCode));
            Check(new VoucherBLL().Luu(null,secondCode,"FIXED",200,5,true,null,out error),"review second voucher: "+error);
            otherVoucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",secondCode));
            int paid=Sale(creator,customer,firstCode,900);
            Check((string)Bill(paid)["NguonDauHoaDon"]=="Lưu lúc giao dịch","new paid invoice marks captured header provenance");
            Scalar("UPDATE Customers SET FullName=@n,Phone=@p WHERE Id=@id",("@n",customerName+" mới"),("@p","07"+Random.Shared.Next(10000000,99999999)),("@id",customer));
            Scalar("UPDATE Employees SET FullName=@n WHERE Id=@id",("@n",creatorName+" mới"),("@id",creator));
            Scalar("UPDATE Vouchers SET Code=@c WHERE Id=@id",("@c",tag+"B"),("@id",voucher));
            var row=Bill(paid);
            Check((string)row["KhachHang"]==customerName && (string)row["NhanVien"]==creatorName && (string)row["NhanVienThuTien"]==creatorName && (string)row["Voucher"]==firstCode,"paid header names and voucher remain stable after master edits");
            Check(invoices.TimKiem(phone).Rows.Cast<DataRow>().Any(r=>(int)r["MaHD"]==paid),"historical customer phone remains searchable");
            Check(invoices.TimKiem(firstCode).Rows.Cast<DataRow>().Any(r=>(int)r["MaHD"]==paid),"historical voucher code remains searchable");
            var reports=new BaoCaoBLL();
            Check(reports.LayBaoCao(DateTime.Today,DateTime.Today,out _,out var report,out _,out _,out _,out _,out error),"report query: "+error);
            var reportRow=report.Rows.Cast<DataRow>().Single(r=>(int)r["MaHD"]==paid);
            Check((string)reportRow["KhachHang"]==customerName && (string)reportRow["NhanVien"]==creatorName && (string)reportRow["Voucher"]==firstCode,"report uses the same historical header values");
            int pending=Sale(creator,customer,tag+"B",900,true);
            SetTestSession(collector);
            Check(sales.ThanhToan(collector,Cart(),secondCode,otherCustomer,800,out _,out _,out _,out error,"Chưa xác định",true,pending,(byte[])Bill(pending)["OrderVersion"],Prescription()),"editing pending captures new party references: "+error);
            Check((string)Bill(pending)["NhanVien"]==creatorName+" mới" && (string)Bill(pending)["NhanVienSua"]==collectorName,"editing retains original creator and captures editor name");
            Scalar("UPDATE Customers SET FullName=@n WHERE Id=@id",("@n",otherName+" mới"),("@id",otherCustomer));
            Scalar("UPDATE Vouchers SET Code=@c WHERE Id=@id",("@c",tag+"D"),("@id",otherVoucher));
            Scalar("UPDATE Employees SET FullName=@n WHERE Id=@id",("@n",collectorName+" lúc thu"),("@id",collector));
            Check(invoices.ThanhToanCho(pending,"Tiền mặt",800,out error,(byte[])Bill(pending)["OrderVersion"]),"pending payment: "+error);
            Scalar("UPDATE Employees SET FullName=@n WHERE Id=@id",("@n",collectorName+" đổi sau thu"),("@id",collector));
            row=Bill(pending);
            Check((string)row["KhachHang"]==otherName && (string)row["Voucher"]==secondCode,"payment retains customer and voucher from saved draft");
            Check((string)row["NhanVienThuTien"]==collectorName+" lúc thu" && (string)row["NhanVienSua"]==collectorName,"collector and editor keep their names at each event");
            Check((string)row["NguonDauHoaDon"]=="Lưu lúc giao dịch","edited and paid new invoice has complete header history");
            int cancelled=Sale(creator,null,null,1000,true);SetTestSession(collector);
            string cancelName=collectorName+" đổi sau thu";
            Check(invoices.HuyHoaDonCho(cancelled,(byte[])Bill(cancelled)["OrderVersion"],"Kiểm thử",out error),"cancel captures actor: "+error);
            Scalar("UPDATE Employees SET FullName=@n WHERE Id=@id",("@n",collectorName+" cuối"),("@id",collector));
            Check((string)Bill(cancelled)["NhanVienHuy"]==cancelName,"cancelled invoice keeps cancellation actor name");
            int walkIn=Sale(creator,null,null,1000);
            Check((string)Bill(walkIn)["KhachHang"]=="Khách lẻ" && (string)Bill(walkIn)["Voucher"]=="" && (string)Bill(walkIn)["NguonDauHoaDon"]=="Lưu lúc giao dịch","walk-in and absent voucher are valid captured values");
            SetTestSession(admin);
            var print=invoices.LayBanIn(pending);
            byte[] Pdf(DataSet data)=>(byte[])typeof(frmMain).Assembly.GetType("Store.POS.Common.PdfInvoiceHelper")!.GetMethod("TaoBanIn",BindingFlags.Public|BindingFlags.Static)!.Invoke(null,new object[]{data})!;
            File.WriteAllBytes(Path.Combine(folder,"hoa-don-giu-thong-tin-cu.pdf"),Pdf(print));
            var h=print.Tables[0].Rows[0];
            ExcelHelper.XuatChiTietMotHoaDon(pending,(string)h["KhachHang"],(string)h["NhanVien"],(DateTime)h["NgayLap"],(decimal)h["TongTien"],(decimal)h["GiamGia"],(decimal)h["ThucThu"],print.Tables[1],outputPath:Path.Combine(folder,"hoa-don.xlsx"),notify:false,headerSource:(string)h["NguonDauHoaDon"]);
            using(var book=new XLWorkbook(Path.Combine(folder,"hoa-don.xlsx")))
                Check(book.Worksheet(1).Cell("B5").GetString()==otherName && book.Worksheet(1).Cell("E5").GetString()==creatorName+" mới","Excel reprint retains captured customer and creator names");
            Scalar("UPDATE Orders SET CustomerNameSnapshot=NULL,CustomerPhoneSnapshot=NULL,EmployeeNameSnapshot=NULL,PaidEmployeeNameSnapshot=NULL,VoucherCodeSnapshot=NULL WHERE Id=@id",("@id",paid));
            Check(((string)Bill(paid)["NguonDauHoaDon"]).StartsWith("Chưa") && (string)Bill(paid)["KhachHang"]==customerName+" mới","legacy header fallback is explicitly labelled instead of pretending to be original history");
            File.WriteAllBytes(Path.Combine(folder,"hoa-don-cu-chua-du-lich-su.pdf"),Pdf(invoices.LayBanIn(paid)));
            var alerts=new CanhBaoDAL();
            Check(alerts.LayLoSapHetHan(30,true).Rows.Cast<DataRow>().Any(r=>(int)r["MaThuoc"]==med),"stock fixture initially has expiry warning");
            Check(new ThuocBLL().XoaThuoc(med.ToString(),out error),"hide stocked medicine: "+error);
            var warning=alerts.LayLoSapHetHan(30,true).Rows.Cast<DataRow>().Single(r=>(int)r["MaThuoc"]==med);
            Check(((string)warning["TenThuoc"]).Contains("Đã ẩn") && ((string)warning["TrangThai"]).Contains("Đã ẩn") && (int)warning["TonLo"]>0,"hidden medicine remains warned with visible hidden status and stock");
            Check(!new MedicineDAL().GetMedicines().ContainsKey(med),"hidden medicine stays excluded from cashier catalog");
            using var main=new frmMain();Offscreen(main);main.ClientSize=new Size(1100,700);Application.DoEvents();
            Form Open(Type type) {typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();return UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];}
            void ClickRow(DataGridView grid,int index)
            {
                grid.CurrentCell=grid.Rows[index].Cells.Cast<DataGridViewCell>().First(c=>c.Visible);
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(grid.CurrentCell.ColumnIndex,index)});Application.DoEvents();
            }
            foreach(var spec in new[]{
                (typeof(frmQuanLyThuoc),"dgvThuoc","MaThuoc","maDangChon","txtTenThuoc"),
                (typeof(frmNhomThuoc),"dgvNhomThuoc","MaNhom","maDangChon","txtTenNhom"),
                (typeof(frmNhaCungCap),"dgvNhaCungCap","MaNCC","maDangChon","txtTenNCC"),
                (typeof(frmKhachHang),"dgvKhachHang","MaKH","maDangChon","txtTenKH"),
                (typeof(frmVoucher),"dgvVoucher","Id","selectedId","txtCode"),
                (typeof(frmNhanVien),"dgvNhanVien","MaNV","selectedId","txtHoTen"),
                (typeof(frmHoaDon),"dgvHoaDon","MaHD","hoaDonDangXem",""),
                (typeof(frmPhieuNhap),"dgvPhieuNhap","MaPhieu","selected","")})
            {
                var form=Open(spec.Item1);var grid=UiField<DataGridView>(form,spec.Item2);
                Check(grid.Rows.Count>=2,spec.Item1.Name+" has keyboard test rows");
                foreach(var key in new[]{Keys.Down,Keys.PageDown,Keys.Home,Keys.End,Keys.Up,Keys.PageUp,Keys.Enter})
                {
                    ClickRow(grid,0);
                    typeof(DataGridView).GetMethod("ProcessDataGridViewKey",PrivateInstance)!.Invoke(grid,new object[]{new KeyEventArgs(key)});
                    typeof(Control).GetMethod("OnKeyUp",PrivateInstance)!.Invoke(grid,new object[]{new KeyEventArgs(key)});Application.DoEvents();
                    Check(Convert.ToString(UiField<object?>(form,spec.Item4))==Convert.ToString(((DataRowView)grid.CurrentRow!.DataBoundItem)[spec.Item3]),spec.Item1.Name+" action target matches highlighted row after "+key);
                }
                if(form is frmQuanLyThuoc) Photo(main,"chon-dong-thuoc");
                if(spec.Item5.Length>0)
                {
                    UiField<Button>(form,"btnThem").PerformClick();var name=UiField<TextBox>(form,spec.Item5);name.Text="DRAFT_KEEP";
                    typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,1)});
                    Check(!grid.Enabled && name.Text=="DRAFT_KEEP",spec.Item1.Name+" prevents switching records over unsaved editor input");
                }
                if(form is frmQuanLyThuoc) Photo(main,"bao-ve-ban-nhap");
            }
            var invoice=(frmHoaDon)Open(typeof(frmHoaDon));var master=UiField<DataGridView>(invoice,"dgvHoaDon");
            ClickRow(master,0);
            typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(master,new object[]{new KeyEventArgs(Keys.PageDown)});
            typeof(DataGridView).GetMethod("ProcessDataGridViewKey",PrivateInstance)!.Invoke(master,new object[]{new KeyEventArgs(Keys.PageDown)});
            typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(master,new object[]{new KeyEventArgs(Keys.ShiftKey)});
            int targetBeforeLeave=(int)((DataRowView)master.CurrentRow!.DataBoundItem)["MaHD"];
            typeof(Control).GetMethod("OnLeave",PrivateInstance)!.Invoke(master,new object[]{EventArgs.Empty});
            Check(UiField<int?>(invoice,"hoaDonDangXem")==targetBeforeLeave,"leaving grid with navigation key held synchronizes target before clearing highlight");
            int index=master.Rows.Cast<DataGridViewRow>().Single(r=>Convert.ToInt32(((DataRowView)r.DataBoundItem)["MaHD"])==pending).Index;
            ClickRow(master,index);
            var link=UiField<LinkLabel>(invoice,"lblChiTiet");Check(link.LinkArea.Length>0 && link.TabStop,"prescription summary exposes an actual keyboard-accessible link");
            typeof(Control).GetMethod("OnMouseDown",PrivateInstance)!.Invoke(link,new object[]{new MouseEventArgs(MouseButtons.Left,1,8,8,0)});Application.DoEvents();
            Check(UiField<int?>(invoice,"hoaDonDangXem")==pending && UiField<DataGridView>(invoice,"dgvChiTiet").Visible,"link mouse-down keeps selected invoice and details");
            bool opened=false,readOnly=false;using(var timer=new System.Windows.Forms.Timer {Interval=50})
            {
                timer.Tick+=(_,_)=> {var dialog=Application.OpenForms.OfType<frmDonThuoc>().FirstOrDefault();if(dialog==null)return;opened=true;readOnly=UiField<TextBox>(dialog,"txtSoDon").ReadOnly && UiField<TextBox>(dialog,"txtSoDon").Text==Prescription().SoDon;dialog.Close();};
                timer.Start();typeof(LinkLabel).GetMethod("OnLinkClicked",PrivateInstance)!.Invoke(link,new object[]{new LinkLabelLinkClickedEventArgs(link.Links[0])});timer.Stop();
            }
            Check(opened && readOnly,"clicking prescription link opens the matching read-only prescription");
            Check(UiField<int?>(invoice,"hoaDonDangXem")==pending,"closing prescription keeps invoice action target");Photo(main,"lien-ket-don-thuoc");
            ClickBackground(invoice);Check(!UiField<int?>(invoice,"hoaDonDangXem").HasValue,"background click still clears invoice selection");
            var alertForm=(frmCanhBao)Open(typeof(frmCanhBao));Photo(main,"canh-bao-thuoc-da-an");
        }
        finally
        {
            SetTestSession(admin);
            foreach(int id in orders) Scalar("DELETE FROM OrderDetails WHERE OrderId=@id;DELETE FROM OrderPendingItems WHERE OrderId=@id;DELETE FROM Orders WHERE Id=@id",("@id",id));
            Scalar("DELETE d FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@med;DELETE FROM Batches WHERE MedicineId=@med;DELETE FROM StockIns WHERE Note=@tag;DELETE FROM Medicines WHERE Id=@med;DELETE FROM Vouchers WHERE Id IN(@v,@ov);DELETE FROM Customers WHERE Id IN(@c,@oc);DELETE FROM Employees WHERE Id IN(@e,@pe);DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g",("@med",med),("@tag",tag),("@v",voucher),("@ov",otherVoucher),("@c",customer),("@oc",otherCustomer),("@e",creator),("@pe",collector),("@s",supplier),("@g",category));
        }
    }
}
