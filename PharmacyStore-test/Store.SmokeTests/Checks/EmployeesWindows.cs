using System.ComponentModel;
using System.Data;
using Store.BLL;
using Store.DAL;
using Store.POS;
using Store.POS.FormBanHang;
using Store.Shared;
using Store.Shared.DTOs.Auth;

internal static partial class Program
{
    static void TestEmployeesWindows()
    {
        string tag="EW"+Guid.NewGuid().ToString("N")[..10], user=tag.ToLowerInvariant();
        int operatorId=Count("SELECT Id FROM Employees WHERE Username='admin'"), employee=0,uiEmployee=0,group=0,supplier=0,med=0,customer=0,voucher=0,order=0;
        var staff=new NhanVienBLL();var catalog=new ThuocBLL();var groups=new NhomThuocBLL();var suppliers=new NhaCungCapBLL();
        var customers=new KhachHangBLL();var vouchers=new VoucherBLL();var imports=new NhapKhoBLL();
        var session=typeof(frmMain).Assembly.GetType("Store.POS.PhienDangNhap")!;
        int oldOperator=(int)session.GetProperty("NhanVienId")!.GetValue(null)!;
        string oldName=(string)session.GetProperty("HoTen")!.GetValue(null)!;
        session.GetProperty("NhanVienId")!.SetValue(null,operatorId);
        session.GetProperty("HoTen")!.SetValue(null,"Kiểm thử");
        bool Login(string account,string password)=>new AuthBLL().Login(new LoginRequest {Username=account,Password=password}).Success;
        void Synced(Func<bool> done,string message)
        {
            var watch=System.Diagnostics.Stopwatch.StartNew();
            while(!done() && watch.ElapsedMilliseconds<6000) {Application.DoEvents();System.Threading.Thread.Sleep(20);}
            Check(done(),message);
        }
        void Preview(Form form,string file)
        {
            using var image=new Bitmap(form.Width,form.Height);form.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));
            image.Save("artifacts/previews/"+file+".png");
        }
        try
        {
            foreach(var bad in new[]{"","a","has space","tài_khoản",new string('a',51),"line\nname"})
                Check(!staff.Luu(null,tag,bad,"Abc123!","0901234567",true,operatorId,out _,out _),"invalid staff username rejected: "+bad.Replace("\n","\\n"));
            foreach(var bad in new[]{"","12345",new string('a',129),"      ","12345\n"})
                Check(!staff.Luu(null,tag,user,bad,"0901234567",true,operatorId,out _,out _),"invalid staff password rejected");
            Check(!staff.Luu(null,"",user,"Abc123!","",true,operatorId,out _,out _),"staff name required");
            Check(!staff.Luu(null,tag,user,"Abc123!","090123456",true,operatorId,out _,out _),"staff phone validates ten digits when provided");
            Check(!staff.Luu(null,tag,user,"Abc123!","",true,0,out _,out _),"staff writes require login");
            long before=DBConnection.DataVersion;
            Check(staff.Luu(null,tag,user,"Abc123!","0901234567",true,operatorId,out employee,out var error),"create employee: "+error);
            Check(DBConnection.DataVersion>before,"successful employee commit advances data version");
            Check(Login(user,"Abc123!"),"new employee can log in");
            string hash=(string)Scalar("SELECT PasswordHash FROM Employees WHERE Id=@id",("@id",employee));
            Check(hash.StartsWith("PBKDF2-SHA256$") && hash!="Abc123!","new password is salted and hashed");
            var data=staff.TimKiem(user);
            Check(data.Rows.Count==1 && !data.Columns.Contains("PasswordHash"),"employee search returns safe display fields only");
            Check(!staff.Luu(null,tag,user.ToUpperInvariant(),"Abc123!","",true,operatorId,out _,out error) && error.Contains("đã tồn tại"),"duplicate username rejected");
            Check(staff.Luu(employee,tag+" Updated",user,"","0901234567",true,operatorId,out _,out error) && Login(user,"Abc123!"),"blank edit password keeps existing password");
            Check(staff.Luu(employee,tag,user,"New123!","",true,operatorId,out _,out error) && !Login(user,"Abc123!") && Login(user,"New123!"),"changing password updates login without exposing password");
            before=DBConnection.DataVersion;
            Check(!staff.Khoa(operatorId,operatorId,out error) && DBConnection.DataVersion==before,"cannot lock current account; failed write emits no change");
            Check(staff.Khoa(employee,operatorId,out error) && !Login(user,"New123!"),"locked employee cannot log in");
            Check(staff.Luu(employee,tag,user,"","",true,operatorId,out _,out error) && Login(user,"New123!"),"employee can be reactivated");
            Check(staff.TimKiem("' OR 1=1 --").Rows.Count==0 && staff.TimKiem("[%]_").Rows.Count==0,"employee search safely handles SQL and wildcard characters");
            using(var form=new frmNhanVien())
            {
                Offscreen(form);
                var grid=UiField<DataGridView>(form,"dgvNhanVien");
                Check(!grid.AutoGenerateColumns && grid.Columns.Count==5 && grid.CurrentCell==null,"employee form explicit columns and no initial selection");
                UiField<Button>(form,"btnThem").PerformClick();UiField<Button>(form,"btnLuu").PerformClick();
                Check(UiField<Label>(form,"lblTrangThai").Text.Length>0,"employee form displays validation errors");
                UiField<TextBox>(form,"txtHoTen").Text=tag+" UI";UiField<TextBox>(form,"txtTaiKhoan").Text=user+"ui";
                UiField<TextBox>(form,"txtMatKhau").Text="Ui12345!";UiField<Button>(form,"btnLuu").PerformClick();
                uiEmployee=Count("SELECT Id FROM Employees WHERE Username=@u",("@u",user+"ui"));
                Check(uiEmployee>0 && Login(user+"ui","Ui12345!"),"employee form creates working account");
                UiField<TextBox>(form,"txtTimKiem").Text=user+"ui";UiField<Button>(form,"btnTimKiem").PerformClick();
                typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(grid,new object[]{new DataGridViewCellEventArgs(0,0)});
                Check(UiField<TextBox>(form,"txtMatKhau").UseSystemPasswordChar && UiField<TextBox>(form,"txtMatKhau").Text=="","selection never loads stored password into editor");
                UiField<Button>(form,"btnSua").PerformClick();UiField<TextBox>(form,"txtHoTen").Text=tag+" UI Updated";
                UiField<Button>(form,"btnLuu").PerformClick();
                Check(Login(user+"ui","Ui12345!"),"UI edit preserves blank password");
                ClickBackground(form);
                Check(UiField<TextBox>(form,"txtHoTen").Text=="" && !UiField<Button>(form,"btnLuu").Enabled,"employee background resets editor");
                CheckLayout(form,"employee form");Preview(form,"frmNhanVien");
            }
            Check(groups.ThemNhanhNhom(tag+" Group",out group,out error),"window fixture group created");
            Check(suppliers.ThemNhanhNCC(tag+" Supplier","0901234567","Test",out supplier,out error),"window fixture supplier created");
            Check(catalog.ThemNhanhThuoc(tag+" Medicine",group,supplier,"Hộp",1000,out med,out error),"window fixture medicine created");
            string phone="09"+Random.Shared.Next(10000000,99999999);
            Check(customers.ThemMoiKhachHang("",tag+" Customer",phone,"0",out error),"window fixture customer created");
            customer=Count("SELECT Id FROM Customers WHERE Phone=@p",("@p",phone));
            Check(vouchers.Luu(null,tag,"FIXED",100,5,true,null,out error),"window fixture voucher created");
            voucher=Count("SELECT Id FROM Vouchers WHERE Code=@c",("@c",tag));
            Check(imports.LuuPhieuNhap(employee,supplier,tag,new List<NhapKhoItem>{new(){MaThuoc=med,TenThuoc=tag,SoLo=tag,HanDung=DateTime.Today.AddDays(30),SoLuong=6,GiaNhap=500}},out _,out _,out error),"window fixture stock imported");

            using(var main=new frmMain())
            {
                Offscreen(main);
                Form Open(Type type)
                {
                    typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
                    var form=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];return form;
                }
                var sales=(frmBanHang)Open(typeof(frmBanHang));
                Check(!sales.TopLevel && !sales.Modal && main.Enabled,"sales is embedded and home navigation remains enabled");
                Check(ReferenceEquals(sales,Open(typeof(frmBanHang))),"opening a function twice reuses its current form");
                UiField<TextBox>(sales,"boxNhapSanPham").Text=med.ToString();UiField<Button>(sales,"btnNhapSanPham").PerformClick();
                UiField<TextBox>(sales,"boxMaVoucher").Text=tag;InvokePrivate(sales,"ApDungVoucher");
                UiField<TextBox>(sales,"boxMaKhachHang").Text=phone;InvokePrivate(sales,"ThemKhachHang");
                UiField<TextBox>(sales,"boxTienKhach").Text="5000";
                var cart=UiField<BindingList<RowData>>(sales,"gioHang");
                UiField<Button>(sales,"btnTamAn").PerformClick();
                Check(!sales.Visible && !sales.IsDisposed && cart.Count==1 && UiField<TextBox>(sales,"boxTienKhach").Text=="5000","hide sales preserves cart and cash draft");
                var stock=(frmNhapKho)Open(typeof(frmNhapKho));
                UiField<ComboBox>(stock,"cboNhaCungCap").SelectedValue=supplier;UiField<ComboBox>(stock,"cboThuoc").SelectedValue=med;
                UiField<TextBox>(stock,"txtSoLo").Text=tag+"Draft";UiField<NumericUpDown>(stock,"nudSoLuong").Value=2;UiField<NumericUpDown>(stock,"nudGiaNhap").Value=500;
                UiField<TextBox>(stock,"txtGhiChu").Text="Phiếu nhập đang soạn";UiField<Button>(stock,"btnThemThuoc").PerformClick();
                UiField<TextBox>(stock,"txtSoLo").Text="NEXT-LOT";UiField<Button>(stock,"btnTamAn").PerformClick();
                var medicine=(frmQuanLyThuoc)Open(typeof(frmQuanLyThuoc));var medGrid=UiField<DataGridView>(medicine,"dgvThuoc");
                var medRow=medGrid.Rows.Cast<DataGridViewRow>().Single(r=>(int)r.Cells["MaThuoc"].Value==med);
                InvokePrivate(medicine,"dgvThuoc_CellClick",medGrid,new DataGridViewCellEventArgs(0,medRow.Index));
                UiField<Button>(medicine,"btnSua").PerformClick();UiField<TextBox>(medicine,"txtTenThuoc").Text="Tên chưa lưu";
                UiField<Button>(medicine,"btnTamAn").PerformClick();
                var batch=(frmLoHang)Open(typeof(frmLoHang));UiField<TextBox>(batch,"txtTimKiem").Text=tag;UiField<Button>(batch,"btnTimKiem").PerformClick();
                UiField<Button>(batch,"btnTamAn").PerformClick();
                var invoices=(frmHoaDon)Open(typeof(frmHoaDon));UiField<TextBox>(invoices,"txtTimKiem").Text=tag;UiField<Button>(invoices,"btnTimKiem").PerformClick();
                UiField<Button>(invoices,"btnTamAn").PerformClick();
                var staffForm=(frmNhanVien)Open(typeof(frmNhanVien));UiField<Button>(staffForm,"btnTamAn").PerformClick();
                var groupForm=Open(typeof(frmNhomThuoc));UiField<Button>(groupForm,"btnTamAn").PerformClick();
                var supplierForm=Open(typeof(frmNhaCungCap));UiField<Button>(supplierForm,"btnTamAn").PerformClick();
                var customerForm=Open(typeof(frmKhachHang));UiField<Button>(customerForm,"btnTamAn").PerformClick();
                var voucherForm=Open(typeof(frmVoucher));UiField<Button>(voucherForm,"btnTamAn").PerformClick();
                var alerts=Open(typeof(frmCanhBao));UiField<Button>(alerts,"btnTamAn").PerformClick();
                var report=Open(typeof(frmBaoCao));UiField<Button>(report,"btnTamAn").PerformClick();
                var windows=UiField<ContextMenuStrip>(main,"menuCuaSo");UiField<Button>(main,"btnCuaSo").PerformClick();
                Check(windows.Items.Count==12 && windows.Items.Cast<ToolStripItem>().All(i=>i.Text?.Contains("đang ẩn")==true),"home lists all hidden functional forms");
                windows.Close();windows.Items.Cast<ToolStripItem>().First(i=>i.Text?.StartsWith("Bán hàng")==true).PerformClick();
                Check(sales.Visible && ReferenceEquals(sales,Application.OpenForms.OfType<frmBanHang>().Single()),"window menu restores same sales instance");
                UiField<Button>(sales,"btnTamAn").PerformClick();
                Check(catalog.SuaThuoc(med.ToString(),tag+" Renamed",group.ToString(),"Hộp","1200","0",supplier,out error),"medicine price/name changed through business layer");
                Synced(()=>cart[0].Price==1200,"timer refreshes hidden sales automatically after commit");
                Check(cart.Count==1 && cart[0].Quantity==1 && cart[0].Name==tag+" Renamed" && UiField<decimal>(sales,"tongTien")==1100,"cart prices/names and voucher total recomputed without dropping cart");
                Check(UiField<TextBox>(sales,"boxTienKhach").Text=="5000" && UiField<TextBox>(sales,"boxTraLai").Text=="3,900","cash draft retained and change recomputed");
                Check(UiField<TextBox>(medicine,"txtTenThuoc").Text=="Tên chưa lưu" && UiField<TextBox>(medicine,"txtTenThuoc").Enabled,"catalog edits survive automatic refresh while grid is reloaded");
                Check(UiField<DataTable>(stock,"dtPhieu").Rows.Count==1 && UiField<TextBox>(stock,"txtGhiChu").Text=="Phiếu nhập đang soạn" && UiField<TextBox>(stock,"txtSoLo").Text=="NEXT-LOT","stock-in lines, note and unfinished batch draft survive refresh");
                Check(groups.SuaNhom(group.ToString(),tag+" Group New",out error) && suppliers.SuaNCC(supplier.ToString(),tag+" Supplier New","0901234567","Test",out error),"category and supplier updated");
                Synced(()=>UiField<ComboBox>(medicine,"cboNhomThuoc").Text==tag+" Group New","combobox display names refresh while selected IDs remain stable");
                Check(UiField<ComboBox>(stock,"cboNhaCungCap").Text==tag+" Supplier New","pending stock-in supplier name refreshed");
                string newPhone="09"+Random.Shared.Next(10000000,99999999);
                Check(customers.SuaKhachHang(customer.ToString(),tag+" Customer New",newPhone,"3",out error),"customer changed through business layer");
                Synced(()=>UiField<TextBox>(sales,"boxMaKhachHang").Text==newPhone,"applied sales customer refreshes by stable ID after phone change");
                Check(vouchers.Luu(voucher,tag,"PERCENT",25,5,true,5,out error),"voucher changed through business layer");
                Synced(()=>UiField<decimal>(sales,"tongTien")==900,"applied voucher and totals update after voucher edit");
                Check(imports.LuuPhieuNhap(employee,supplier,tag,new List<NhapKhoItem>{new(){MaThuoc=med,TenThuoc=tag,SoLo=tag,HanDung=DateTime.Today.AddDays(30),SoLuong=3,GiaNhap=500}},out _,out _,out error),"additional stock committed");
                Synced(()=>UiField<DataGridView>(batch,"dgvLoHang").Rows.Cast<DataGridViewRow>().Any(r=>(int)r.Cells["TonKho"].Value==9),"hidden batch view refreshes stock and retains search");
                Check(new BanHangBLL().ThanhToan(employee,new List<CartItem>{new(){MaThuoc=med,SoLuong=1}},tag,customer,900,out order,out _,out _,out error),"sale committed for refresh/history checks: "+error);
                Synced(()=>UiField<DataGridView>(invoices,"dgvHoaDon").Rows.Cast<DataGridViewRow>().Any(r=>(int)r.Cells["MaHD"].Value==order),"hidden invoice list refreshes after saved sale");
                Check(UiField<TextBox>(invoices,"txtTimKiem").Text==tag && UiField<Label>(main,"lblSoHoaDon").Text==Count("SELECT COUNT(*) FROM Orders WHERE CAST(OrderDate AS date)=CAST(GETDATE() AS date)").ToString(),"invoice filter preserved and home count refreshed");
                invoices.MoHoaDon(order);Check(UiField<DataGridView>(invoices,"dgvChiTiet").Rows.Count==1,"existing invoice window can navigate to requested order");
                cart[0].Quantity=99;sales.CapNhatGiaTien();
                Check(!UiField<Button>(sales,"btnThanhToan").Enabled && UiField<Label>(sales,"lblTinhTrangGioHang").Text.Contains("chỉ còn"),"insufficient refreshed stock blocks checkout without deleting cart");
                cart[0].Quantity=1;sales.CapNhatGiaTien();
                Check(staff.Khoa(employee,operatorId,out error),"employee with invoice can be locked");
                Synced(()=>UiField<DataGridView>(staffForm,"dgvNhanVien").Rows.Cast<DataGridViewRow>().Any(r=>(int)r.Cells["MaNV"].Value==employee && !(bool)r.Cells["DangLam"].Value),"employee list refreshes after account lock");
                Check(Count("SELECT COUNT(*) FROM Orders WHERE Id=@id AND EmployeeId=@e",("@id",order),("@e",employee))==1 && !Login(user,"New123!"),"soft lock keeps invoice history and blocks login");
                Check(catalog.XoaThuoc(med.ToString(),out error),"medicine deactivated");
                Synced(()=>!UiField<Button>(sales,"btnThanhToan").Enabled && UiField<Label>(sales,"lblTinhTrangGioHang").Text.Contains("ngừng bán"),"inactive product disables payment and preserves draft for correction");
                Check(UiField<DataTable>(stock,"dtPhieu").Rows.Count==1,"inactive catalog item does not silently discard stock-in draft");
                CheckLayout(sales,"sales hide control");Preview(sales,"sales-refresh-warning");
                foreach(var form in UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").Values.ToArray()) form.Close();
                Check(UiField<Button>(main,"btnCuaSo").Text.Contains("(0)"),"closing forms removes them from open-window list");
            }
            using(var modalMain=new frmMain {WindowState=FormWindowState.Normal,ShowInTaskbar=false,StartPosition=FormStartPosition.Manual,Location=new Point(-20000,-20000)})
            {
                Exception? failure=null;
                using var timer=new System.Windows.Forms.Timer {Interval=60};
                timer.Tick+=(_,_)=>
                {
                    timer.Stop();
                    try
                    {
                        typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmBanHang)).Invoke(modalMain,null);
                        var child=(frmBanHang)UiField<Dictionary<Type,Form>>(modalMain,"cuaSoDangMo")[typeof(frmBanHang)];
                        Check(modalMain.Modal && modalMain.Enabled && child.Visible && !child.TopLevel && !child.Modal,"real login-style home supports embedded functional forms");
                        UiField<Button>(child,"btnTamAn").PerformClick();
                        Check(!child.Visible && modalMain.Enabled,"hide returns to enabled home under modal login loop");
                    }
                    catch(Exception ex) {failure=ex;}
                    finally {foreach(var child in UiField<Dictionary<Type,Form>>(modalMain,"cuaSoDangMo").Values.ToArray()) child.Close();modalMain.DialogResult=DialogResult.OK;modalMain.Close();}
                };
                timer.Start();modalMain.ShowDialog();
                if(failure!=null) throw failure;
            }
        }
        finally
        {
            Scalar(@"DELETE FROM Orders WHERE Id=@o;
                DELETE FROM StockIns WHERE Id IN (SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                DELETE FROM Batches WHERE MedicineId=@m;DELETE FROM Medicines WHERE Id=@m;
                DELETE FROM Vouchers WHERE Id=@v;DELETE FROM Customers WHERE Id=@c;
                DELETE FROM Suppliers WHERE Id=@s;DELETE FROM Categories WHERE Id=@g;
                DELETE FROM Employees WHERE Id IN (@e,@ui);",("@o",order),("@m",med),("@v",voucher),("@c",customer),("@s",supplier),("@g",group),("@e",employee),("@ui",uiEmployee));
            session.GetProperty("NhanVienId")!.SetValue(null,oldOperator);session.GetProperty("HoTen")!.SetValue(null,oldName);
            Console.WriteLine("Employee/window test fixtures removed; existing records retained.");
        }
    }
}
