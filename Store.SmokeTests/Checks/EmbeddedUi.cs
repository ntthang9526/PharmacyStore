using System.Reflection;
using Store.POS;

internal static partial class Program
{
    static void TestEmbeddedUi()
    {
        using var main=new frmMain();
        Check(main.WindowState==FormWindowState.Normal && main.ClientSize==new Size(1500,820) && main.StartPosition==FormStartPosition.CenterScreen && main.MaximizeBox,"home opens centered at 1500x820 and remains resizable");
        Offscreen(main);
        var host=UiField<Panel>(main,"contentHost");var dashboard=UiField<TableLayoutPanel>(main,"dashboard");
        var sidebar=UiField<Panel>(main,"panelChucNang");
        Check(main.Controls.Find("panelMenuBar",true).Length==0,"top navigation bar removed");
        Check(sidebar.Contains(UiField<Button>(main,"btnTrangChu")) && main.Controls.Find("btnCuaSo",true).Length==0,"sidebar keeps Home without redundant open-forms button");
        Form Open(Type type)
        {
            typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
            return UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
        }
        void Preview(string name)
        {
            Directory.CreateDirectory("artifacts/previews");
            using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(0,0,image.Width,image.Height));
            image.Save("artifacts/previews/"+name+".png");
        }
        main.ClientSize=new Size(1600,900);Application.DoEvents();
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
        var shortcut=new KeyEventArgs(Keys.F2);
        typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(sales,new object[]{shortcut});Application.DoEvents();
        Check(shortcut.SuppressKeyPress && !sales.Visible,"F2 works from embedded sales without closing draft");
        var medicine=(frmQuanLyThuoc)Open(typeof(frmQuanLyThuoc));
        UiField<Button>(medicine,"btnThem").PerformClick();UiField<TextBox>(medicine,"txtTenThuoc").Text="Thuốc chưa lưu";
        Check(!sales.Visible && !sales.IsDisposed && medicine.Visible,"switching hides old form without disposing it");
        UiField<Button>(main,"btnTrangChu").PerformClick();Application.DoEvents();
        Check(dashboard.Visible && !medicine.Visible && !medicine.IsDisposed,"home shows dashboard and retains form draft");
        Check(ReferenceEquals(medicine,Open(typeof(frmQuanLyThuoc))) && UiField<TextBox>(medicine,"txtTenThuoc").Text=="Thuốc chưa lưu","opening same function restores draft instance");
        UiField<Button>(main,"btnTrangChu").PerformClick();
        Check(dashboard.Visible && !medicine.Visible,"Home hides embedded form and returns to dashboard");
        Open(typeof(frmBanHang));
        Check(UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-NOT-APPLIED","switching retains sale inputs");
        foreach(var size in new[]{new Size(1600,900),new Size(1366,728),new Size(1100,700)})
        {
            main.ClientSize=size;Application.DoEvents();
            var grid=UiField<DataGridView>(sales,"tableGioHang");var entry=UiField<TextBox>(sales,"boxNhapSanPham");
            Check(host.Right<=main.ClientSize.Width+1 && host.Bottom<=main.ClientSize.Height+1,"content fits resized home "+size);
            Check(grid.Width>200 && grid.Height>150 && entry.Width>80,"cart and barcode entry remain usable "+size);
            var total=UiField<Panel>(sales,"panel8");var payment=UiField<Panel>(sales,"panel9");
            Check(total.Parent==UiField<TableLayoutPanel>(sales,"paymentLayout") && payment.Parent==total.Parent,"total and payment controls share payment flow "+size);
            Check(UiField<Button>(sales,"btnThanhToan").Visible && total.Visible,"payment controls remain present "+size);
            if(size.Width==1366) Preview("embedded-sales-laptop");
        }
        main.ClientSize=new Size(1600,900);Application.DoEvents();
        UiField<Panel>(sales,"panelTienMat").Visible=true;Application.DoEvents();
        var scroll=UiField<Panel>(sales,"paymentScroll");var pay=UiField<Button>(sales,"btnThanhToan");
        scroll.ScrollControlIntoView(pay);Application.DoEvents();
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
        var staff=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmNhanVien)];staff.Close();Application.DoEvents();
        Check(dashboard.Visible,"closing active embedded form restores dashboard");
        foreach(var form in UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").Values.ToArray()) form.Close();
    }
}
