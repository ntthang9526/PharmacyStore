using System.Data;
using System.Reflection;
using Store.DAL;
using Store.BLL;
using Store.POS;
using Store.Shared;
using Store.Shared.DTOs.Auth;

internal static partial class Program
{
    static void SetTestSession(int id)
    {
        var session=typeof(frmMain).Assembly.GetType("Store.POS.PhienDangNhap")!;
        session.GetProperty("NhanVienId")!.SetValue(null,id);
        if(id<=0) {session.GetProperty("VaiTro")!.SetValue(null,"STAFF");return;}
        var account=(HoTen:(string)Scalar("SELECT FullName FROM Employees WHERE Id=@id",("@id",id)),VaiTro:(string)Scalar("SELECT Role FROM Employees WHERE Id=@id",("@id",id)));
        session.GetProperty("HoTen")!.SetValue(null,account.HoTen);
        session.GetProperty("VaiTro")!.SetValue(null,account.VaiTro);
    }
    static void TestRoles()
    {
        int admin=PhanQuyenDAL.NhanVienId,staff=0,otherAdmin=0,group=0,supplier=0,med=0,customer=0;
        string tag="ROLE"+Guid.NewGuid().ToString("N")[..10],user=tag.ToLowerInvariant();
        var employees=new NhanVienBLL();var categories=new NhomThuocBLL();var suppliers=new NhaCungCapBLL();
        var medicines=new ThuocBLL();var imports=new NhapKhoBLL();var customers=new KhachHangBLL();
        bool Login(string password)=>new AuthBLL().Login(new LoginRequest{Username=user,Password=password}).Success;
        void Denied(Action action,string title)
        {
            bool denied=false;try {action();}catch(UnauthorizedAccessException) {denied=true;}
            Check(denied,title);
        }
        void Preview(Form form,string name)
        {
            Directory.CreateDirectory("artifacts/previews");
            using var bitmap=new Bitmap(form.Width,form.Height);form.DrawToBitmap(bitmap,new Rectangle(0,0,bitmap.Width,bitmap.Height));
            bitmap.Save("artifacts/previews/"+name+".png");
        }
        try
        {
            Check(PhanQuyenDAL.ChoPhep("ADMIN",Quyen.QuanTri) && !PhanQuyenDAL.ChoPhep("STAFF",Quyen.QuanTri) &&
                PhanQuyenDAL.ChoPhep("STAFF",Quyen.BanHang) && !PhanQuyenDAL.ChoPhep("UNKNOWN",Quyen.Xem),"role policy grants explicit roles and denies unknown roles");
            if(Count("SELECT COUNT(*) FROM Employees WHERE Role='ADMIN' AND IsActive=1")==1)
            {
                var row=employees.TimKiem("").Rows.Cast<DataRow>().Single(r=>(int)r["MaNV"]==admin);
                long version=DBConnection.DataVersion;
                Check(!employees.Luu(admin,(string)row["HoTen"],(string)row["TaiKhoan"],"",row["DienThoai"].ToString()!,true,admin,out _,out var lastError,"STAFF") && lastError.Contains("Admin"),"last active admin cannot be demoted");
                Check(DBConnection.DataVersion==version,"denied last-admin edit does not commit or emit refresh");
            }
            Check(employees.Luu(null,tag+" Staff",user,"Old123!","",true,admin,out staff,out var error),"create staff account: "+error);
            Check(employees.Luu(null,tag+" Admin",user+"a","Old123!","",true,admin,out otherAdmin,out error,"ADMIN"),"create explicit admin account: "+error);
            Check(new AuthBLL().Login(new LoginRequest{Username=user,Password="Old123!"}).Role=="STAFF","login response carries staff role");
            Check(new AuthBLL().Login(new LoginRequest{Username=user+"a",Password="Old123!"}).Role=="ADMIN","login response carries admin role");
            Check(!employees.Luu(null,tag,user+"bad","Old123!","",true,admin,out _,out _,"owner"),"unknown role rejected before saving");
            Check(categories.ThemNhanhNhom(tag,out group,out error),"fixture category");
            Check(suppliers.ThemNhanhNCC(tag,"0901234567","Test",out supplier,out error),"fixture supplier");
            Check(medicines.ThemNhanhThuoc(tag,group,supplier,"Viên",2000,out med,out error),"fixture medicine");
            Check(medicines.TimKiemThuoc(tag).Rows[0].IsNull("GiaNhapGanNhat"),"medicine with no imports has unknown purchase price");
            var lines=new List<NhapKhoItem>{new(){MaThuoc=med,TenThuoc=tag,SoLo=tag,HanDung=DateTime.Today.AddDays(60),SoLuong=10,GiaNhap=600}};
            Check(imports.LuuPhieuNhap(admin,supplier,tag,lines,out int first,out _,out error),"admin imports stock");
            lines[0].SoLuong=5;lines[0].GiaNhap=750;
            Check(imports.LuuPhieuNhap(admin,supplier,tag+" later",lines,out int second,out _,out error),"second import adds to same batch at new cost");
            Check(new LoHangDAL().TimKiem(tag).Rows.Cast<DataRow>().Single()["GiaNhapGanNhat"] is decimal batchCost && batchCost==750,"batch shows most recent import cost");
            Check((decimal)medicines.TimKiemThuoc(tag).Rows[0]["GiaNhapGanNhat"]==750,"medicine shows most recent import cost");
            var history=new PhieuNhapBLL();
            Check(history.TimKiem(tag,null,null).Rows.Count==2,"import history searches medicines, supplier, or note");
            Check((decimal)history.ChiTiet(first).Rows[0]["GiaNhap"]==600 && (decimal)history.ChiTiet(second).Rows[0]["GiaNhap"]==750,"import details retain original costs after later imports");
            Check((decimal)history.ChiTiet(first).Rows[0]["ThanhTien"]==6000,"import detail calculates exact line total");
            Check(history.TimKiem("' OR 1=1 --",null,null).Rows.Count==0 && history.TimKiem("[%]_",null,null).Rows.Count==0,"history search uses literal parameterized keywords");
            bool dateError=false;try{history.TimKiem(tag,DateTime.Today,DateTime.Today.AddDays(-1));}catch(ArgumentException){dateError=true;}
            Check(dateError,"inverted import date range rejected");
            Check(history.TimKiem(tag,DateTime.Today,DateTime.Today).Rows.Count==2,"import history includes full end date");

            SetTestSession(staff);
            long before=DBConnection.DataVersion;
            Check(!categories.ThemMoiNhom("",tag+" Denied",out error),"staff cannot create category through BLL");
            Denied(()=>new NhomThuocDAL().ThemNhanh(tag+" quick"),"staff cannot bypass quick category dialog");
            Denied(()=>new NhaCungCapDAL().ThemNhanh(tag+" quick","0901234567","Test"),"staff cannot bypass quick supplier dialog");
            Denied(()=>new ThuocDAL().ThemNhanh(tag+" quick",group,supplier,"Viên",1000),"staff cannot bypass quick medicine dialog");
            Check(!medicines.SuaThuoc(med.ToString(),tag,group.ToString(),"Viên","9000","0",supplier,out error),"staff cannot change medicine prices");
            Check(!medicines.XoaThuoc(med.ToString(),out error),"staff cannot deactivate medicine");
            Check(!new VoucherBLL().Luu(null,tag,"FIXED",100,1,true,null,out error),"staff cannot manage vouchers");
            Denied(()=>employees.TimKiem(""),"staff cannot query account management");
            Check(!employees.Luu(staff,tag,user,"","",true,staff,out _,out error,"ADMIN"),"staff cannot promote self");
            Check(!employees.Luu(staff,tag,user,"","",true,admin,out _,out error,"ADMIN"),"spoofed admin operator ID rejected");
            int stockBefore=Count("SELECT Stock FROM Batches WHERE MedicineId=@m",("@m",med));
            Check(DBConnection.DataVersion==before,"unauthorized catalog edits emit no refresh");
            Check(imports.LuuPhieuNhap(staff,supplier,tag+" staff import",lines,out int staffReceipt,out _,out error),"staff may import stock: "+error);
            stockBefore+=lines[0].SoLuong;
            Check(Count("SELECT EmployeeId FROM StockIns WHERE Id=@id",("@id",staffReceipt))==staff,"staff receipt records actual importing employee");
            Check(!imports.LuuPhieuNhap(admin,supplier,tag,lines,out _,out _,out error),"staff cannot import under another employee ID");
            Check(Count("SELECT Stock FROM Batches WHERE MedicineId=@m",("@m",med))==stockBefore && DBConnection.DataVersion>before,"staff import commits stock and refresh without allowing spoofed writes");
            Check(history.TimKiem(tag,null,null).Rows.Count==3 && history.ChiTiet(first).Rows.Count==1,"staff can view import history and details");
            var bills=new HoaDonBLL();
            Check(bills.TimKiem("",null,null).Rows.Count==Count("SELECT COUNT(*) FROM Orders"),"staff can view every employee's invoices");
            Check(new BaoCaoBLL().LayBaoCao(DateTime.Today.AddYears(-10),DateTime.Today,out _,out var billRows,out _,out _,out _,out _,out error) && billRows.Rows.Count==Count("SELECT COUNT(*) FROM Orders WHERE OrderDate>=DATEADD(year,-10,CAST(GETDATE() AS date)) AND OrderDate<DATEADD(day,1,CAST(GETDATE() AS date))"),"staff can view complete store reports");
            string phone="09"+Random.Shared.Next(10000000,99999999);
            Check(customers.ThemMoiKhachHang("",tag,phone,"0",out error),"staff may create customer with zero points");
            customer=Count("SELECT Id FROM Customers WHERE Phone=@phone",("@phone",phone));
            Check(customers.SuaKhachHang(customer.ToString(),tag+" edited",phone,"0",out error),"staff may edit customer basics");
            Check(!customers.SuaKhachHang(customer.ToString(),tag,phone,"100",out error) && !customers.XoaKhachHang(customer.ToString(),out error),"staff cannot change points or delete customer");
            Check(new BanHangBLL().ThanhToan(staff,new List<CartItem>{new(){MaThuoc=med,SoLuong=1}},"",customer,2000,out int order,out _,out _,out error),"staff can sell and pay using own identity: "+error);
            Check(Count("SELECT EmployeeId FROM Orders WHERE Id=@id",("@id",order))==staff && Count("SELECT Stock FROM Batches WHERE MedicineId=@m",("@m",med))==stockBefore-1,"sale records actual cashier and deducts stock");
            Check(!new BanHangBLL().ThanhToan(admin,new List<CartItem>{new(){MaThuoc=med,SoLuong=1}},"",customer,2000,out _,out _,out _,out error),"cashier cannot impersonate another employee");

            var passwords=new MatKhauBLL();string oldHash=(string)Scalar("SELECT PasswordHash FROM Employees WHERE Id=@id",("@id",staff));
            Check(!passwords.Doi("Wrong123!","New123!","New123!",out _) && Login("Old123!"),"wrong current password does not change account");
            foreach(var bad in new[]{"","12345","      ",new string('a',129),"abcde\n"})
                Check(!passwords.Doi("Old123!",bad,bad,out _),"invalid new password rejected");
            Check(!passwords.Doi("Old123!","New123!","Mismatch",out _) && !passwords.Doi("Old123!","Old123!","Old123!",out _),"confirmation mismatch and reused password rejected");
            Check(passwords.Doi("Old123!","New123!","New123!",out error) && !Login("Old123!") && Login("New123!"),"self password change updates login");
            Check(((string)Scalar("SELECT PasswordHash FROM Employees WHERE Id=@id",("@id",staff))).StartsWith("PBKDF2-SHA256$") &&
                !new MatKhauDAL().Doi(oldHash,oldHash),"password is hashed and stale update cannot overwrite a newer password");

            using(var main=new frmMain())
            {
                Offscreen(main);Check(UiField<Button>(main,"btnNhapKho").Enabled && UiField<Button>(main,"btnNhapKho").Visible,"staff import navigation available");
                var menu=UiField<ContextMenuStrip>(main,"menuQuanLyKhac");
                Check(!menu.Items[6].Available && menu.Items[5].Enabled && menu.Items[7].Enabled && menu.Items[8].Enabled && UiField<Button>(main,"btnBaoCao").Enabled,"staff keeps invoices, reports, import history and own password entry points");
                Preview(main,"roles-staff-home");
                UiField<Button>(main,"btnNhapKho").PerformClick();Application.DoEvents();
                Check(UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").ContainsKey(typeof(frmNhapKho)),"staff opens stock-in from main navigation");
                menu.Items[7].PerformClick();Application.DoEvents();
                var open=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo");
                Check(open.ContainsKey(typeof(frmPhieuNhap)) && open[typeof(frmPhieuNhap)].Visible,"staff opens embedded import history from menu");
                menu.Items[8].PerformClick();Application.DoEvents();
                Check(open.ContainsKey(typeof(frmDoiMatKhau)) && open[typeof(frmDoiMatKhau)].Visible && !open[typeof(frmPhieuNhap)].Visible,"password form opens embedded and hides previous form");
                foreach(var size in new[]{new Size(1382,753),new Size(1100,700)})
                {
                    main.ClientSize=size;Application.DoEvents();
                    CheckLayout(open[typeof(frmDoiMatKhau)],"embedded password "+size);
                    menu.Items[7].PerformClick();Application.DoEvents();CheckLayout(open[typeof(frmPhieuNhap)],"embedded imports "+size);
                    menu.Items[8].PerformClick();Application.DoEvents();
                }
                main.ClientSize=new Size(1382,753);Application.DoEvents();Preview(main,"roles-password-embedded");
                menu.Items[7].PerformClick();Application.DoEvents();Preview(main,"roles-imports-embedded");
                main.Dispose();
            }
            using(var form=new frmNhapKho())
            {
                Offscreen(form);
                Check(!UiField<Button>(form,"btnThemNCC").Visible && !UiField<Button>(form,"btnTaoThuoc").Visible,"staff import hides admin-only quick additions");
                UiField<ComboBox>(form,"cboNhaCungCap").SelectedValue=supplier;
                UiField<ComboBox>(form,"cboThuoc").SelectedValue=med;
                UiField<TextBox>(form,"txtSoLo").Text=tag+"UI";UiField<NumericUpDown>(form,"nudSoLuong").Value=2;
                UiField<NumericUpDown>(form,"nudGiaNhap").Value=750;UiField<Button>(form,"btnThemThuoc").PerformClick();
                Check(UiField<DataTable>(form,"dtPhieu").Rows.Count==1 && UiField<Button>(form,"btnLuu").Visible && UiField<Button>(form,"btnLuu").Enabled,"staff can select supplier and medicine, add receipt lines and reach Save");
                CheckLayout(form,"staff stock-in");Preview(form,"staff-stock-in");
            }
            foreach(Form form in new Form[]{new frmQuanLyThuoc(),new frmNhomThuoc(),new frmNhaCungCap(),new frmVoucher()})
            using(form)
            {
                Offscreen(form);
                foreach(string name in new[]{"btnThem","btnSua","btnXoa","btnLuu","btnBoQua","btnHuy"})
                foreach(var control in form.Controls.Find(name,true)) Check(!control.Visible && !control.Enabled,"staff hides forbidden button: "+form.Name+" "+name);
                foreach(var control in form.Controls.Find("btnThem",true)) {control.Enabled=true;control.Visible=true;Check(!control.Enabled && !control.Visible,"state changes cannot show or re-enable restricted button");}
                CheckLayout(form,"staff "+form.Name);
            }
            using(var form=new frmKhachHang())
            {Offscreen(form);Check(!UiField<Button>(form,"btnXoa").Visible,"staff customer delete hidden");UiField<Button>(form,"btnThem").PerformClick();Check(!UiField<TextBox>(form,"txtDiem").Enabled && UiField<TextBox>(form,"txtTenKH").Enabled,"staff can edit customer basics but not points");}
            using(var form=new frmPhieuNhap())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvPhieuNhap");
                Check(grid.CurrentCell==null && UiField<DataGridView>(form,"dgvChiTiet").CurrentCell==null,"import history starts with no selection or details");
                UiField<TextBox>(form,"txtTimKiem").Text=tag;UiField<Button>(form,"btnTimKiem").PerformClick();
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,0)});
                Check(UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==1 && UiField<Label>(form,"lblChiTiet").Text.Contains("NV:"),"import selection shows line detail and employee");
                form.LamMoiDuLieu();Check(UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==1,"import refresh retains selected receipt");
                CheckLayout(form,"import history");Preview(form,"frmPhieuNhap");ClickBackground(form);
                Check(grid.CurrentCell==null && UiField<DataGridView>(form,"dgvChiTiet").Rows.Count==0,"click outside import grid clears selection and details");
            }
            using(var form=new frmDoiMatKhau())
            {
                Offscreen(form);Check(UiField<TextBox>(form,"txtCu").UseSystemPasswordChar && UiField<TextBox>(form,"txtMoi").UseSystemPasswordChar,"password fields masked");
                UiField<TextBox>(form,"txtCu").Text="New123!";form.Hide();Application.DoEvents();
                Check(UiField<TextBox>(form,"txtCu").Text=="","switching away clears password draft");
                form.Show();Application.DoEvents();CheckLayout(form,"change password");Preview(form,"frmDoiMatKhau");
            }
            SetTestSession(admin);
            Check(employees.Luu(staff,tag,user,"","",true,admin,out _,out error,"ADMIN"),"admin promotes staff");
            SetTestSession(staff);
            using(var main=new frmMain())
            {
                Offscreen(main);Check(UiField<Button>(main,"btnNhapKho").Enabled,"promoted account has admin navigation");
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmBanHang)).Invoke(main,null);Application.DoEvents();
                var open=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo");var sale=open[typeof(frmBanHang)];
                UiField<TextBox>(sale,"boxNhapSanPham").Text=med.ToString();UiField<Button>(sale,"btnNhapSanPham").PerformClick();
                UiField<Button>(main,"btnNhapKho").PerformClick();Application.DoEvents();
                var stock=(frmNhapKho)open[typeof(frmNhapKho)];
                UiField<ComboBox>(stock,"cboNhaCungCap").SelectedValue=supplier;UiField<ComboBox>(stock,"cboThuoc").SelectedValue=med;
                UiField<TextBox>(stock,"txtSoLo").Text=tag+"Draft";UiField<NumericUpDown>(stock,"nudGiaNhap").Value=750;
                UiField<Button>(stock,"btnThemThuoc").PerformClick();
                Scalar("UPDATE Employees SET Role='STAFF' WHERE Id=@id",("@id",staff));DBConnection.NotifyDataChanged();
                InvokePrivate(main,"CapNhatQuyen");
                Check(UiField<Button>(main,"btnNhapKho").Enabled && !UiField<ContextMenuStrip>(main,"menuQuanLyKhac").Items[6].Available,"role downgrade hides account management while keeping stock-in available");
                Check(!sale.IsDisposed && open.ContainsKey(typeof(frmBanHang)) && UiField<System.ComponentModel.BindingList<Store.POS.FormBanHang.RowData>>(sale,"gioHang").Count==1,"role change preserves cashier cart draft");
                UiField<Button>(main,"btnNhapKho").PerformClick();Application.DoEvents();
                Check(ReferenceEquals(stock,open[typeof(frmNhapKho)]) && UiField<DataTable>(stock,"dtPhieu").Rows.Count==1 && !UiField<Button>(stock,"btnTaoThuoc").Visible,"downgrade preserves import draft and hides quick additions");
                Scalar("UPDATE Employees SET Role='ADMIN' WHERE Id=@id",("@id",staff));InvokePrivate(main,"CapNhatQuyen");
                UiField<Button>(main,"btnNhapKho").PerformClick();Application.DoEvents();
                Check(ReferenceEquals(stock,open[typeof(frmNhapKho)]) && UiField<DataTable>(stock,"dtPhieu").Rows.Count==1 && UiField<Button>(stock,"btnTaoThuoc").Visible && UiField<Button>(stock,"btnTaoThuoc").Enabled,"promotion restores allowed quick addition without clearing receipt draft");
                Scalar("UPDATE Employees SET Role='STAFF' WHERE Id=@id",("@id",staff));InvokePrivate(main,"CapNhatQuyen");
                Check(!medicines.XoaThuoc(med.ToString(),out _),"writes recheck current database role after downgrade");main.Dispose();
            }
            SetTestSession(admin);Check(employees.Khoa(staff,admin,out error),"admin locks employee");
            SetTestSession(staff);
            Check(!passwords.Doi("New123!","Again123!","Again123!",out _) && !Login("New123!"),"locked account cannot change password or login");
            Check(!new BanHangBLL().ThanhToan(staff,new List<CartItem>{new(){MaThuoc=med,SoLuong=1}},"",customer,2000,out _,out _,out _,out _),"locked account cannot sell through existing session");
            Check(!imports.LuuPhieuNhap(staff,supplier,tag,lines,out _,out _,out _),"locked account cannot import through existing session");
            SetTestSession(0);Denied(()=>new PhieuNhapDAL().TimKiem("",null,null),"anonymous import history rejected");
        }
        finally
        {
            SetTestSession(admin);
            Scalar(@"DELETE FROM Orders WHERE EmployeeId=@staff;
                DELETE FROM StockIns WHERE Id IN (SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                DELETE FROM Batches WHERE MedicineId=@m;DELETE FROM Medicines WHERE Id=@m;
                DELETE FROM Customers WHERE Id=@c;DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;
                DELETE FROM Employees WHERE Id IN (@staff,@other);",("@staff",staff),("@other",otherAdmin),("@m",med),("@c",customer),("@s",supplier),("@g",group));
        }
    }
}
