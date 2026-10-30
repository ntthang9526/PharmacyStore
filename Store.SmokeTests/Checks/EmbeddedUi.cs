using System.Reflection;
using Store.POS;

internal static partial class Program
{
    static void TestEmbeddedUi()
    {
        using var main=new frmMain();
        Check(main.WindowState==FormWindowState.Normal && main.ClientSize==new Size(1680,900) && main.StartPosition==FormStartPosition.CenterScreen && main.MaximizeBox,"home opens centered at the Designer size 1680x900 and remains resizable");
        Offscreen(main);
        var host=UiField<Panel>(main,"contentHost");var dashboard=UiField<TableLayoutPanel>(main,"dashboard");
        var sidebar=UiField<Panel>(main,"panelChucNang");
        Check(main.Controls.Find("panelMenuBar",true).Length==0,"top navigation bar removed");
        Check(sidebar.Contains(UiField<Button>(main,"btnTrangChu")) && main.Controls.Find("btnCuaSo",true).Length==0,"sidebar keeps Home without redundant open-forms button");
        Form Open(Type type)
        {
            typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();WaitForLoads();
            return UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
        }
        void Preview(string name)
        {
            Directory.CreateDirectory("artifacts/previews");
            using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));
            image.Save("artifacts/previews/"+name+".png");
        }
        main.ClientSize=new Size(1600,900);Application.DoEvents();WaitForLoads();
        Check(sidebar.Top==0 && sidebar.Height==main.ClientSize.Height,"vertical navigation fills full window height");
        Check(dashboard.Dock==DockStyle.Fill && dashboard.Bounds==host.ClientRectangle,"dashboard fills content viewport");
        CheckLayout(dashboard,"responsive dashboard");
        var title=UiField<Label>(main,"lblTitle");
        Check(title.Parent==UiField<TableLayoutPanel>(main,"shellLayout") && title.Height>40 && title.Width>900,"branding fills header row");
        Preview("embedded-home");
        var sales=(frmBanHang)Open(typeof(frmBanHang));
        Check(!sales.TopLevel && sales.Parent==host && sales.Dock==DockStyle.Fill && sales.FormBorderStyle==FormBorderStyle.None,"sales embedded borderless in content panel");
        Check(!dashboard.Visible && sales.Visible,"content switches from dashboard to sale");
        UiField<TextBox>(sales,"boxMaVoucher").Text="DRAFT-NOT-APPLIED";
        var shortcut=new KeyEventArgs(Keys.F4);
        typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(sales,new object[]{shortcut});Application.DoEvents();WaitForLoads();
        Check(shortcut.SuppressKeyPress && !sales.Visible,"F4 works from embedded sales without closing draft");
        var medicine=(frmQuanLyThuoc)Open(typeof(frmQuanLyThuoc));
        UiField<Button>(medicine,"btnThem").PerformClick();UiField<TextBox>(medicine,"txtTenThuoc").Text="Thuốc chưa lưu";
        Check(!sales.Visible && !sales.IsDisposed && medicine.Visible,"switching hides old form without disposing it");
        UiField<Button>(main,"btnTrangChu").PerformClick();Application.DoEvents();WaitForLoads();
        Check(dashboard.Visible && !medicine.Visible && !medicine.IsDisposed,"home shows dashboard and retains form draft");
        Check(ReferenceEquals(medicine,Open(typeof(frmQuanLyThuoc))) && UiField<TextBox>(medicine,"txtTenThuoc").Text=="Thuốc chưa lưu","opening same function restores draft instance");
        UiField<Button>(main,"btnTrangChu").PerformClick();
        Check(dashboard.Visible && !medicine.Visible,"Home hides embedded form and returns to dashboard");
        Open(typeof(frmBanHang));
        Check(UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-NOT-APPLIED","switching retains sale inputs");
        void SendKey(Control control, KeyEventArgs key)
        {
            typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(control,new object[]{key});Application.DoEvents();WaitForLoads();
        }
        var escape=new KeyEventArgs(Keys.Escape);SendKey(sales,escape);
        Check(!sales.IsDisposed && sales.Visible && UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-NOT-APPLIED","Esc keeps the sales form and draft open");
        (Keys Key,string Button,Type? Form)[] navigation={
            (Keys.F1,"btnTrangChu",null),(Keys.F2,"btnBanHang",typeof(frmBanHang)),
            (Keys.F3,"btnHoaDon",typeof(frmHoaDon)),(Keys.F4,"btnQuanLyThuoc",typeof(frmQuanLyThuoc)),
            (Keys.F5,"btnQuanLyKhac",null),(Keys.F6,"btnNhapKho",typeof(frmNhapKho)),
            (Keys.F7,"btnCanhBao",typeof(frmCanhBao)),(Keys.F8,"btnBaoCao",typeof(frmBaoCao))
        };
        foreach(var item in navigation)
        {
            Open(typeof(frmBanHang));
            Check(UiField<Button>(main,item.Button).Text.EndsWith("("+item.Key+")"),item.Key+" label matches its sidebar button");
            var key=new KeyEventArgs(item.Key);SendKey(sales,key);
            Check(key.SuppressKeyPress,item.Key+" is handled from embedded sales");
            if(item.Key==Keys.F5)
            {
                var menu=UiField<ContextMenuStrip>(main,"menuQuanLyKhac");
                Check(menu.Visible && sales.Visible,"F5 opens the management menu without leaving the cart");menu.Close();
            }
            else if(item.Form==null) Check(dashboard.Visible && !sales.Visible,"F1 returns to home");
            else Check(UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[item.Form].Visible,item.Key+" opens "+item.Form.Name);
            Check(!sales.IsDisposed && UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-NOT-APPLIED",item.Key+" preserves the sale draft");
        }
        Open(typeof(frmBanHang));
        foreach(var modifier in new[]{Keys.Control,Keys.Alt,Keys.Shift})
            foreach(var item in navigation)
            {
                var key=new KeyEventArgs(modifier|item.Key);SendKey(sales,key);
                Check(!key.Handled && sales.Visible,modifier+"+"+item.Key+" does not trigger navigation");
            }
        var handled=new KeyEventArgs(Keys.F1){Handled=true};SendKey(main,handled);
        Check(sales.Visible,"already-handled keys do not trigger navigation twice");
        var invoiceShortcut=new KeyEventArgs(Keys.F3);
        typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(sales,new object[]{invoiceShortcut});Application.DoEvents();WaitForLoads();
        var invoices=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmHoaDon)];
        Check(invoiceShortcut.SuppressKeyPress && invoices.Visible && !sales.Visible,"F3 opens invoices from embedded sales");
        Check(UiField<Button>(main,"btnHoaDon").BackColor==Color.FromArgb(15,118,110) && title.Text.Contains("hóa đơn"),"active invoice navigation and breadcrumb agree");
        UiField<Button>(main,"btnBanHang").PerformClick();Application.DoEvents();WaitForLoads();
        Check(sales.Visible && UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-NOT-APPLIED","invoice navigation retains the sale draft");
        foreach(var size in new[]{new Size(1600,900),new Size(1366,728),new Size(1100,700)})
        {
            main.ClientSize=size;Application.DoEvents();WaitForLoads();
            var grid=UiField<DataGridView>(sales,"tableGioHang");var entry=UiField<TextBox>(sales,"boxNhapSanPham");
            Check(host.Right<=main.ClientSize.Width+1 && host.Bottom<=main.ClientSize.Height+1,"content fits resized home "+size);
            Check(grid.Width>200 && grid.Height>150 && entry.Width>80,"cart and barcode entry remain usable "+size);
            var total=UiField<Panel>(sales,"panel8");var payment=UiField<Panel>(sales,"panel9");
            Check(total.Parent==UiField<TableLayoutPanel>(sales,"paymentLayout") && payment.Parent==total.Parent,"total and payment controls share payment flow "+size);
            Check(UiField<Button>(sales,"btnThanhToan").Visible && total.Visible,"payment controls remain present "+size);
            if(size.Width==1366) Preview("embedded-sales-laptop");
        }
        main.ClientSize=new Size(1600,900);Application.DoEvents();WaitForLoads();
        UiField<Panel>(sales,"panelTienMat").Visible=true;Application.DoEvents();WaitForLoads();
        var scroll=UiField<Panel>(sales,"paymentScroll");var pay=UiField<Button>(sales,"btnThanhToan");
        scroll.ScrollControlIntoView(pay);Application.DoEvents();WaitForLoads();
        Check(pay.Visible && UiField<TextBox>(sales,"boxTienKhach").Visible,"cash input and payment reachable through scroll");
        Preview("embedded-sales-cash");
        foreach(var type in new[]{typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmNhapKho),typeof(frmLoHang),typeof(frmVoucher),typeof(frmHoaDon),typeof(frmCanhBao),typeof(frmBaoCao),typeof(frmNhanVien)})
        {
            var form=Open(type);
            Check(!form.TopLevel && form.Parent==host && form.Visible,"function embeds correctly: "+type.Name);
            Check(form.Controls.Find("btnTamAn",true).Length==0,"redundant Hide button removed: "+type.Name);
            Check(UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").Values.Count(f=>f.Visible)==1,"one active content view: "+type.Name);
        }
        Preview("embedded-employees");
        var staff=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmNhanVien)];staff.Close();Application.DoEvents();WaitForLoads();
        Check(dashboard.Visible,"closing active embedded form restores dashboard");
        foreach(var form in UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").Values.ToArray()) form.Close();
    }
}
