using Microsoft.Data.SqlClient;
using Store.BLL;
using Store.DAL;
using Store.Shared;
using Store.Shared.DTOs.Auth;
using Store.POS;
using Store.POS.FormBanHang;
using System.Reflection;

internal static partial class Program
{
    static bool renderUi;
    static int checks, categoryId, supplierId, customerId, medicineId, voucherId;
    static readonly string prefix = "MERGETEST_" + Guid.NewGuid().ToString("N")[..8];
    static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception("FAIL: " + message);
        checks++;
        Console.WriteLine("PASS: " + message);
    }
    static object Scalar(string sql, params (string Key, object Value)[] parameters)
    {
        using var conn = DBConnection.GetDbConnection();
        using var cmd = new SqlCommand(sql, conn);
        foreach (var p in parameters) cmd.Parameters.AddWithValue(p.Key, p.Value);
        return cmd.ExecuteScalar() ?? DBNull.Value;
    }
    static int Count(string sql, params (string Key, object Value)[] parameters) => Convert.ToInt32(Scalar(sql, parameters));
    static int Stock() => Count("SELECT COALESCE(SUM(Stock),0) FROM Batches WHERE MedicineId=@id AND ExpiryDate>=CAST(GETDATE() AS date)",("@id",medicineId));
    static List<CartItem> Cart(int qty) => [new CartItem { MaThuoc = medicineId, SoLuong = qty }];

    [STAThread]
    static int Main(string[] args)
    {
        var settings = new SqlConnectionStringBuilder(DBConnection.connectionString);
        if (settings.InitialCatalog != "PharmacyStoreTrialDB") throw new Exception("Tests require the isolated PharmacyStoreTrialDB.");
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
        renderUi = args.Contains("--ui-preview");
        if(!args.Contains("--designer-offline") && !args.Contains("--dashboard-headers") && !args.Contains("--measure-headers") && !args.Contains("--edit-columns") && !args.Contains("--designed-columns") && !args.Contains("--conditional-fill") && !args.Contains("--preserve-design")) SetTestSession(Count("SELECT Id FROM Employees WHERE Username='admin'"));
        if(args.Contains("--cart-actions-live"))
        {
            try
            {
                var medicines=new MedicineDAL().GetMedicines().Values;
                medicineId=medicines.First(m=>new MedicineDAL().SoLuongConHan(m.Id)>=4).Id;
                TestCartActions();Console.WriteLine($"ALL {checks} LIVE CART ACTION CHECKS PASSED");return 0;
            }
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--preserve-design"))
        {
            try {TestPreserveDesign();Console.WriteLine($"ALL {checks} PRESERVE DESIGN CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--conditional-fill"))
        {
            try {TestConditionalFill();Console.WriteLine($"ALL {checks} CONDITIONAL FILL CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--designed-columns"))
        {
            try {TestDesignedColumns();Console.WriteLine($"ALL {checks} DESIGNER COLUMN CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--medicine-list-live"))
        {
            try {TestMedicineListLive();Console.WriteLine($"ALL {checks} LIVE MEDICINE LIST CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--edit-columns"))
        {
            try {TestEditColumns();Console.WriteLine($"ALL {checks} EDIT-COLUMN CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--measure-headers")) { MeasureHeaders();return 0; }
        if(args.Contains("--dashboard-headers"))
        {
            try {TestDashboardHeaders();Console.WriteLine($"ALL {checks} DASHBOARD/HEADER CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--fill-alert"))
        {
            try {TestFillAlert();Console.WriteLine($"ALL {checks} FILL/ALERT CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--grid-receipt-report"))
        {
            try {TestGridReceiptReport();Console.WriteLine($"ALL {checks} GRID/RECEIPT/REPORT CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--excel-payments"))
        {
            try {TestExcelPayments();Console.WriteLine($"ALL {checks} EXCEL/PAYMENT CHECKS PASSED");return 0;}
            catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--layout-audit"))
        {
            try {AuditEmbeddedLayouts();return 0;} catch(Exception ex){Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--roles"))
        {
            try {TestRoles();Console.WriteLine($"ALL {checks} ROLE/IMPORT CHECKS PASSED");return 0;}
            catch(Exception ex) {Console.WriteLine(ex);return 1;}
        }
        if(args.Contains("--embedded-ui"))
        {
            try {TestEmbeddedUi();Console.WriteLine($"ALL {checks} EMBEDDED UI CHECKS PASSED");return 0;}
            catch(Exception ex) {Console.WriteLine(ex);return 1;}
        }
        if (args.Contains("--employees-windows"))
        {
            try { TestEmployeesWindows(); Console.WriteLine($"ALL {checks} EMPLOYEE/WINDOW CHECKS PASSED"); return 0; }
            catch(Exception ex) {Console.WriteLine(ex);return 1;}
        }
        if (args.Contains("--designer-offline"))
        {
            try { TestDesignerOffline(); Console.WriteLine($"ALL {checks} DESIGNER OFFLINE CHECKS PASSED"); return 0; }
            catch(Exception ex) {Console.WriteLine(ex);return 1;}
        }
        if (args.Contains("--expansion"))
        {
            try { TestExpansion(); Console.WriteLine($"ALL {checks} EXPANSION CHECKS PASSED"); return 0; }
            catch(Exception ex) { Console.WriteLine(ex); return 1; }
        }
        if (args.Contains("--followup-ui"))
        {
            try { TestFollowupUi(); Console.WriteLine($"ALL {checks} FOLLOWUP CHECKS PASSED"); return 0; }
            catch (Exception ex) { Console.WriteLine(ex); return 1; }
        }
        if (args.Contains("--ui-requests"))
        {
            try { TestUiRequests(); Console.WriteLine($"ALL {checks} REQUEST CHECKS PASSED"); return 0; }
            catch (Exception ex) { Console.WriteLine(ex); return 1; }
        }
        if (args.Contains("--render-only"))
        {
            try { RenderForms(); Console.WriteLine($"ALL {checks} LAYOUT CHECKS PASSED"); return 0; }
            catch (Exception ex) { Console.WriteLine(ex); return 1; }
        }
        if (args.Contains("--menu-only"))
        {
            try { TestManagementMenu(); Console.WriteLine($"ALL {checks} MENU CHECKS PASSED"); return 0; }
            catch (Exception ex) { Console.WriteLine(ex); return 1; }
        }
        try { TestManagementMenu(); Run(); Console.WriteLine($"ALL {checks} CHECKS PASSED"); return 0; }
        catch (Exception ex) { Console.WriteLine(ex); return 1; }
        finally { Cleanup(); }
    }

    static void TestManagementMenu()
    {
        using var main = new frmMain { WindowState=FormWindowState.Normal, ShowInTaskbar = false, StartPosition = FormStartPosition.Manual, Location = new System.Drawing.Point(-20000, -20000) };
        main.Show();
        var show = typeof(frmMain).GetMethod("ShowFormQuanLyKhac", BindingFlags.NonPublic | BindingFlags.Instance)!;
        var menu = (ContextMenuStrip)typeof(frmMain).GetField("menuQuanLyKhac", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(main)!;
        for (int cycle = 0; cycle < 3; cycle++)
        {
            show.Invoke(main, null);
            Application.DoEvents();
            Check(menu.Visible, "management menu opens, cycle " + cycle);
            menu.Close(ToolStripDropDownCloseReason.ItemClicked);
            Application.DoEvents();
            Check(!menu.IsDisposed && !main.IsDisposed, "closing menu keeps it alive, cycle " + cycle);
        }
        var types = new[] { typeof(frmNhomThuoc), typeof(frmNhaCungCap), typeof(frmKhachHang), typeof(frmLoHang), typeof(frmVoucher), typeof(frmHoaDon), typeof(frmNhanVien), typeof(frmPhieuNhap), typeof(frmDoiMatKhau) };
        for (int index = 0; index < types.Length; index++)
        {
            var expected=types[index];
            show.Invoke(main,null);menu.Close(ToolStripDropDownCloseReason.ItemClicked);
            menu.Items[index].PerformClick();Application.DoEvents();
            var child=Application.OpenForms.Cast<Form>().FirstOrDefault(f=>f.GetType()==expected && f.Parent?.FindForm()==main);
            Check(child!=null && !child.TopLevel && !child.Modal && main.Enabled && !menu.IsDisposed,"management command opens embedded "+expected.Name);
            child?.Close();Application.DoEvents();
        }
        main.Dispose();
        Check(menu.IsDisposed, "main form disposes its menu");
    }

    static void Run()
    {
        var auth = new AuthBLL();
        Check(!auth.Login(new LoginRequest { Username="", Password="" }).Success, "empty login rejected");
        Check(!auth.Login(new LoginRequest { Username="admin", Password="wrong" }).Success, "wrong password rejected");
        var login=auth.Login(new LoginRequest { Username="admin", Password="123456" });
        Check(login.Success && login.EmployeeID.HasValue, "real seeded login accepted");
        int employeeId=login.EmployeeID!.Value;

        string name=prefix+" O'Brien";
        var categories=new NhomThuocBLL();
        Check(categories.ThemMoiNhom("",name,out var error),"category auto-ID and apostrophe: "+error);
        categoryId=Count("SELECT Id FROM Categories WHERE Name=@n",("@n",name));
        Check(categoryId>0,"category generated identity");
        Check(categories.SuaNhom(categoryId.ToString(),name+" updated",out error),"category update");
        var suppliers=new NhaCungCapBLL();
        Check(suppliers.ThemMoiNCC("",name,"0901234567","Trial",out error),"supplier auto-ID: "+error);
        supplierId=Count("SELECT Id FROM Suppliers WHERE Name=@n",("@n",name));
        Check(suppliers.SuaNCC(supplierId.ToString(),name,"0901234567","Updated",out error),"supplier update");
        string phone="09"+Random.Shared.Next(10000000,99999999).ToString();
        var customers=new KhachHangBLL();
        Check(customers.ThemMoiKhachHang("",name,phone,"0",out error),"customer auto-ID: "+error);
        customerId=Count("SELECT Id FROM Customers WHERE Phone=@p",("@p",phone));
        Check(customers.SuaKhachHang(customerId.ToString(),name,phone,"2",out error),"customer update");
        Check(new CustomerDAL().GetCustomerByPhone(phone)?.Id==customerId,"customer lookup");

        var catalog=new ThuocBLL();
        Check(catalog.ThemMoiThuoc("",name,categoryId.ToString(),"Hộp","1000.25","0",supplierId,out error),"medicine auto-ID and supplier selection: "+error);
        medicineId=Count("SELECT Id FROM Medicines WHERE Name=@n",("@n",name));
        Check(Convert.ToDecimal(Scalar("SELECT Price FROM Medicines WHERE Id=@id",("@id",medicineId)))==1000,"new medicine stores whole-dong price");
        Check(Stock()==0 && new BatchDAL().TimToanBoSoLuongConHan(medicineId)==0,"new medicine has zero stock without null errors");
        Check(catalog.TimKiemThuoc("O'Brien").Rows.Count>0,"search with apostrophe uses SQL parameters");
        Check(!catalog.ThemMoiThuoc("",name,categoryId.ToString(),"Hộp","invalid","0",supplierId,out error),"invalid price rejected");

        var imports=new NhapKhoBLL();
        var lines=new List<NhapKhoItem> {
            new() { MaThuoc=medicineId,TenThuoc=name,SoLo=prefix+"EARLY",HanDung=DateTime.Today.AddDays(2),SoLuong=5,GiaNhap=500.25m },
            new() { MaThuoc=medicineId,TenThuoc=name,SoLo=prefix+"LATE",HanDung=DateTime.Today.AddDays(40),SoLuong=8,GiaNhap=599.75m }};
        Check(!imports.LuuPhieuNhap(employeeId,1,"Wrong supplier",lines,out _,out _,out error) && Stock()==0,"wrong supplier rejected and import rolled back");
        Check(imports.LuuPhieuNhap(employeeId,supplierId,"Test",lines,out int stockInId,out decimal cost,out error),"stock-in saved: "+error);
        Check(stockInId>0 && cost==7300 && Stock()==13,"stock-in cost/details and stock increment");
        Check(Count("SELECT COUNT(*) FROM StockInDetails WHERE StockInId=@id AND UnitCost<>ROUND(UnitCost,0)",("@id",stockInId))==0,"stock-in detail costs are whole dong");
        var importedBatches = new LoHangBLL().TimKiem(prefix);
        Check(importedBatches.Rows.Count == 2 && importedBatches.Rows.Cast<System.Data.DataRow>().Sum(row => (int)row["SoLuongDaNhap"]) == 13,
            "batch inventory aggregates quantities from saved import lines");
        Check(importedBatches.Rows.Cast<System.Data.DataRow>().All(row => row["NgayNhapGanNhat"] is DateTime && row["TenNCC"] is string),
            "batch inventory includes import dates and supplier names");
        Scalar("INSERT INTO Batches(MedicineId,BatchNumber,ExpiryDate,Stock) VALUES(@m,@n,DATEADD(day,-1,CAST(GETDATE() AS date)),10)",("@m",medicineId),("@n",prefix+"EXPIRED"));
        Check(catalog.SuaThuoc(medicineId.ToString(),name,categoryId.ToString(),"Hộp","1000.25","999",supplierId,out error) && Stock()==13,"catalog update does not overwrite batch stocks");

        TestSelectedFixes();

        voucherId=Count("INSERT INTO Vouchers(Code,DiscountType,DiscountValue,Quantity,IsActive) VALUES(@code,'FIXED',100.25,1,1);SELECT CAST(SCOPE_IDENTITY() AS int)",("@code",prefix));
        var voucherBLL=new VoucherBLL();
        Check(voucherBLL.KiemTra(prefix,out var voucher,out error),"voucher lookup: "+error);
        Check(voucherBLL.TinhGiamGia(new VoucherInfo { DiscountType="PERCENT",DiscountValue=10 },1005)==101,"percentage rounding");
        Check(voucherBLL.TinhGiamGia(new VoucherInfo { DiscountType="FIXED",DiscountValue=9999 },10)==10,"voucher capped at subtotal");

        var sales=new BanHangBLL();
        Check(!sales.ThanhToan(0,Cart(1),null,null,1000.25m,out _,out _,out _,out error),"sale requires login");
        Check(!sales.ThanhToan(employeeId,Cart(-1),null,null,0,out _,out _,out _,out error),"negative sale quantity rejected");
        Check(!sales.ThanhToan(employeeId,Cart(7),prefix,customerId,1,out _,out _,out _,out error) && Stock()==13,"changed payable rejected without inventory mutation");
        Check(!sales.ThanhToan(employeeId,Cart(14),prefix,customerId,14*1000m-100,out _,out _,out _,out error),"insufficient stock rejected");
        Check(Stock()==13 && Count("SELECT Quantity FROM Vouchers WHERE Id=@id",("@id",voucherId))==1,"failed sale rolls back stock and voucher");
        Check(Count("SELECT COUNT(*) FROM Orders WHERE CustomerId=@id",("@id",customerId))==0,"failed sale leaves no orphan order");
        Check(sales.ThanhToan(employeeId,Cart(7),prefix,customerId,7*1000m-100,out int orderId,out decimal subtotal,out decimal discount,out error),"sale commits: "+error);
        Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@id AND UnitPrice<>1000",("@id",orderId))==0,"legacy fractional price saved to new invoice as whole dong");
        Check(orderId>0 && subtotal==7000m && discount==100,"whole-dong prices and payable persisted consistently");
        Check(Count("SELECT COUNT(*) FROM Orders WHERE Id=@o AND CustomerId=@c",("@o",orderId),("@c",customerId))==1,"customer linked to persisted order");
        Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@o",("@o",orderId))==2,"FEFO sale split over two batches");
        Check(Count("SELECT Stock FROM Batches WHERE MedicineId=@m AND BatchNumber=@n",("@m",medicineId),("@n",prefix+"EARLY"))==0,"earliest batch consumed first");
        Check(Stock()==6 && Count("SELECT Stock FROM Batches WHERE MedicineId=@m AND BatchNumber=@n",("@m",medicineId),("@n",prefix+"EXPIRED"))==10,"expired batch excluded from sales");
        Check(Count("SELECT Quantity FROM Vouchers WHERE Id=@id",("@id",voucherId))==0,"voucher decremented exactly once");
        Check(!sales.ThanhToan(employeeId,Cart(1),prefix,null,900.25m,out _,out _,out _,out error) && Stock()==6,"exhausted voucher rejected without stock loss");

        var concurrent=Enumerable.Range(0,2).Select(index=>Task.Run(()=>new BanHangBLL().ThanhToan(employeeId,Cart(4),null,null,4000m,out _,out _,out _,out _))).ToArray();
        Task.WaitAll(concurrent);
        Check(concurrent.Count(t=>t.Result)==1 && Stock()==2,"concurrent sales cannot oversell");
        Check(new CanhBaoDAL().LayThuocSapHet(10).Rows.Cast<System.Data.DataRow>().Any(r=>(int)r["MaThuoc"]==medicineId),"low stock warning includes test medicine");
        TestExpirySoonOnly();
        Check(new BaoCaoBLL().LayBaoCao(DateTime.Today,DateTime.Today,out _,out var orders,out int orderCount,out _,out _,out _,out error) && orders.Rows.Cast<System.Data.DataRow>().Any(r=>(int)r["MaHD"]==orderId),"revenue report includes saved order: "+error);
        Check(!new BaoCaoBLL().LayBaoCao(DateTime.Today.AddDays(1),DateTime.Today,out _,out _,out _,out _,out _,out _,out error),"invalid report date range rejected");



        foreach(var type in new[]{typeof(frmDangNhap),typeof(frmMain),typeof(frmBanHang),typeof(frmThanhToanQR),typeof(frmQuanLyThuoc),typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmNhapKho),typeof(frmCanhBao),typeof(frmBaoCao),typeof(frmLoHang),typeof(frmVoucher),typeof(frmHoaDon),typeof(frmNhanVien)})
        {
            using var form=(Form)(type==typeof(frmThanhToanQR)?Activator.CreateInstance(type,1000m)!:Activator.CreateInstance(type)!);
            if(form is frmMain) form.WindowState=FormWindowState.Normal;
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(-20000, -20000);
            if (type == typeof(frmThanhToanQR)) form.CreateControl();
            else form.Show();
            Application.DoEvents();
            Check(form.Controls.Count>0,type.Name+" constructor/designer/data-load smoke check");
            if (renderUi && type != typeof(frmThanhToanQR))
            {
                Directory.CreateDirectory("artifacts/previews");
                using var bitmap = new System.Drawing.Bitmap(form.Width, form.Height);
                form.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(Path.Combine("artifacts/previews", type.Name + ".png"));
                if (type != typeof(frmBanHang) && type != typeof(frmMain) && type != typeof(frmDangNhap)) CheckLayout(form, type.Name);
            }
        }

        Check(catalog.XoaThuoc(medicineId.ToString(),out error),"medicine soft delete: "+error);
        Check(Count("SELECT COUNT(*) FROM OrderDetails WHERE OrderId=@o",("@o",orderId))==2,"soft delete preserves sales history");
    }

    static void RenderForms()
    {
        Directory.CreateDirectory("artifacts/previews");
        foreach (var type in new[] { typeof(frmDangNhap), typeof(frmMain), typeof(frmBanHang), typeof(frmQuanLyThuoc), typeof(frmNhomThuoc), typeof(frmNhaCungCap), typeof(frmKhachHang), typeof(frmNhapKho), typeof(frmCanhBao), typeof(frmBaoCao), typeof(frmLoHang), typeof(frmVoucher), typeof(frmHoaDon), typeof(frmNhanVien), typeof(frmPhieuNhap), typeof(frmDoiMatKhau) })
        {
            using var form = (Form)Activator.CreateInstance(type)!;
            if(form is frmMain) form.WindowState=FormWindowState.Normal;
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(-20000, -20000);
            if (form is frmBaoCao)
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var onLoad = type.GetMethod("frmBaoCao_Load", flags)!;
                form.Load -= (EventHandler)onLoad.CreateDelegate(typeof(EventHandler), form);
                var daily = (DataGridView)type.GetField("dgvTheoNgay", flags)!.GetValue(form)!;
                var orders = (DataGridView)type.GetField("dgvHoaDon", flags)!.GetValue(form)!;
                var dal = new BaoCaoDAL();
                daily.DataSource = dal.LayDoanhThuTheoNgay(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1),DateTime.Today);
                orders.DataSource = dal.LayDanhSachHoaDon(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1),DateTime.Today);
                form.Show();
            }
            else form.Show();
            Application.DoEvents();
            if (type != typeof(frmBanHang) && type != typeof(frmMain) && type != typeof(frmDangNhap))
            {
                foreach (var grid in UiGrids(form))
                    Check(grid.SelectedRows.Count == 0 && grid.CurrentCell == null, type.Name + ": no row selected on opening");
                CheckLayout(form,type.Name);
                var originalSize = form.Size;
                form.Size = form.MinimumSize;
                Application.DoEvents();
                CheckLayout(form,type.Name + " minimum size");
                form.Size = originalSize;
                Application.DoEvents();
            }
            using var image = new System.Drawing.Bitmap(form.Width,form.Height);
            form.DrawToBitmap(image,new System.Drawing.Rectangle(0,0,image.Width,image.Height));
            image.Save(Path.Combine("artifacts/previews",type.Name+".png"));
        }

        foreach (bool supplier in new[] { false, true })
        {
            using var dialog = CreateCatalogDialog(supplier);
            Offscreen(dialog);
            CheckLayout(dialog, supplier ? "quick supplier" : "quick category");
            using var preview = new System.Drawing.Bitmap(dialog.Width, dialog.Height);
            dialog.DrawToBitmap(preview, new System.Drawing.Rectangle(0, 0, preview.Width, preview.Height));
            preview.Save(Path.Combine("artifacts/previews", supplier ? "quick-supplier.png" : "quick-category.png"));
        }
    }

    static void CheckLayout(Control parent, string name)
    {
        foreach (Control child in parent.Controls)
        {
            if (child.Visible && child is Button or TextBox or ComboBox or DateTimePicker or NumericUpDown or DataGridView or TableLayoutPanel or GroupBox)
            {
                Check(child.Width > 0 && child.Height > 0 && child.Left >= 0 && child.Top >= 0 &&
                    child.Right <= parent.ClientSize.Width + 1 && child.Bottom <= parent.ClientSize.Height + 1,
                    name + ": " + child.Name + " stays within its layout cell [" + child.Bounds + " parent=" + parent.ClientSize + "]");
            }
            if (child is not DataGridView && child is not ComboBox && child is not NumericUpDown) CheckLayout(child, name);
        }
    }

    static void Cleanup()
    {
        Scalar(@"DELETE FROM Orders WHERE Id IN (SELECT d.OrderId FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                 DELETE FROM StockIns WHERE Id IN (SELECT d.StockInId FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId WHERE b.MedicineId=@m);
                 DELETE FROM Batches WHERE MedicineId=@m;
                 DELETE FROM Medicines WHERE Id=@m;
                 DELETE FROM Vouchers WHERE Id=@v;",("@m",medicineId),("@v",voucherId));
        if(customerId>0) new KhachHangDAL().DeleteKhachHang(customerId.ToString());
        if(categoryId>0) new NhomThuocDAL().DeleteNhomThuoc(categoryId.ToString());
        if(supplierId>0) new NhaCungCapDAL().DeleteNhaCungCap(supplierId.ToString());
        Console.WriteLine("Temporary test records removed from trial database.");
    }
}
