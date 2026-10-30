using System.Runtime.InteropServices;
using System.Text;
using Store.POS;

internal static partial class Program
{
    private delegate bool CloseDialogCallback(IntPtr window, IntPtr parameter);
    [DllImport("kernel32.dll")] private static extern uint GetCurrentThreadId();
    [DllImport("user32.dll")] private static extern bool EnumThreadWindows(uint thread, CloseDialogCallback callback, IntPtr parameter);
    [DllImport("user32.dll", CharSet=CharSet.Unicode)] private static extern int GetClassName(IntPtr window, StringBuilder text, int length);
    [DllImport("user32.dll", CharSet=CharSet.Unicode)] private static extern int GetWindowText(IntPtr window, StringBuilder text, int length);
    [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr window, uint message, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool PostMessage(IntPtr window, uint message, IntPtr wParam, IntPtr lParam);

    static void TestMainCloseConfirmation()
    {
        // Only answer native dialogs on this test's UI thread.
        List<(string Title,int DefaultButton)> AnswerDialogs(Action action, int answer)
        {
            var dialogs=new List<(string,int)>();
            uint thread=GetCurrentThreadId();
            using var timer=new System.Windows.Forms.Timer {Interval=50};
            timer.Tick+=(_,_)=>EnumThreadWindows(thread,(window,_)=>
            {
                var name=new StringBuilder(64);GetClassName(window,name,name.Capacity);
                if(name.ToString()!="#32770") return true;
                var title=new StringBuilder(256);GetWindowText(window,title,title.Capacity);
                int defaultButton=(int)(SendMessage(window,0x400,IntPtr.Zero,IntPtr.Zero).ToInt64()&0xffff);
                dialogs.Add((title.ToString(),defaultButton));
                PostMessage(window,0x111,(IntPtr)answer,IntPtr.Zero);
                return true;
            },IntPtr.Zero);
            timer.Start();
            try {action();Application.DoEvents();}
            finally {timer.Stop();}
            return dialogs;
        }

        using var main=new frmMain();Offscreen(main);
        Check(UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo").Count==0,"home starts without cached child forms");
        var dialogs=AnswerDialogs(main.Close,7);
        Check(dialogs.Count==1 && dialogs[0].Title=="Đóng ứng dụng","closing fresh home asks for confirmation");
        Check(dialogs[0].DefaultButton==7,"No is the default exit choice");
        Check(!main.IsDisposed && main.Visible,"No keeps fresh home open");

        typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmBanHang)).Invoke(main,null);
        Application.DoEvents();
        var sales=(frmBanHang)UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmBanHang)];
        UiField<TextBox>(sales,"boxMaVoucher").Text="DRAFT-KEEP";
        dialogs=AnswerDialogs(main.Close,7);
        Check(dialogs.Count==1 && !main.IsDisposed && sales.Visible,"No also keeps the active child form open");
        Check(UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-KEEP","canceling exit preserves sale input");
        string Warning(frmMain form,bool logout=false)=>(string)typeof(frmMain).GetMethod("NoiDungXacNhanThoat",PrivateInstance)!.Invoke(form,new object[]{logout})!;
        Check(Warning(main).Contains("chưa lưu"),"close warning identifies unsaved sale input");
        var cart=UiField<System.ComponentModel.BindingList<Store.POS.FormBanHang.RowData>>(sales,"gioHang");
        cart.Add(new Store.POS.FormBanHang.RowData{Id=1,Name="Draft",Unit="Hộp",Quantity=2,Price=1000});
        typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmNhapKho)).Invoke(main,null);Application.DoEvents();
        var stock=(frmNhapKho)UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[typeof(frmNhapKho)];
        var draft=UiField<System.Data.DataTable>(stock,"dtPhieu");
        draft.Rows.Add(1,"Draft medicine","Hộp","DRAFT",DateTime.Today.AddYears(1),2,1000m);
        Check(Warning(main).Contains("Giỏ hàng bán hàng: 1") && Warning(main).Contains("Phiếu nhập kho: 1"),"close warning includes hidden cart and current stock draft");
        Check(Warning(main,true).Contains("Giỏ hàng") && Warning(main,true).Contains("Phiếu nhập") && Warning(main,true).Contains("đăng xuất"),"logout checks both unsaved documents");
        dialogs=AnswerDialogs(main.Close,7);
        Check(!main.IsDisposed && cart.Count==1 && draft.Rows.Count==1,"No preserves both unsaved cart and receipt rows");
        cart.Clear();draft.Rows.Clear();UiField<TextBox>(sales,"boxMaVoucher").Clear();
        UiField<TextBox>(stock,"txtSoLo").Text="UNADDED-LOT";
        Check(Warning(main).Contains("đang nhập chưa lưu"),"unadded stock input is also detected");
        UiField<TextBox>(stock,"txtSoLo").Clear();UiField<TextBox>(sales,"boxMaVoucher").Text="DRAFT-KEEP";
        UiField<Button>(main,"btnTrangChu").PerformClick();
        dialogs=AnswerDialogs(main.Close,7);
        Check(dialogs.Count==1 && !sales.IsDisposed && !sales.Visible,"home still confirms exit with a hidden sale draft");
        Check(UiField<TextBox>(sales,"boxMaVoucher").Text=="DRAFT-KEEP","canceling exit preserves the hidden draft");

        var shutdown=new FormClosingEventArgs(CloseReason.WindowsShutDown,false);
        dialogs=AnswerDialogs(()=>typeof(Form).GetMethod("OnFormClosing",PrivateInstance)!.Invoke(main,[shutdown]),7);
        Check(dialogs.Count==0 && !shutdown.Cancel,"Windows shutdown does not show the user exit prompt");
        dialogs=AnswerDialogs(main.Close,6);
        Check(dialogs.Count==1 && main.IsDisposed,"Yes closes the main form");
        Check(sales.IsDisposed,"confirmed exit disposes the embedded sale form");

        using var logout=new frmMain();Offscreen(logout);
        dialogs=AnswerDialogs(()=>UiField<Button>(logout,"btnDangXuat").PerformClick(),6);
        Check(dialogs.Count==1 && dialogs[0].Title=="Xác nhận đăng xuất","logout asks once without a second exit prompt");
        Check(logout.DaDangXuat && logout.IsDisposed,"confirmed logout keeps its existing result");
        using var cancelLogout=new frmMain();Offscreen(cancelLogout);
        typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(typeof(frmBanHang)).Invoke(cancelLogout,null);
        var draftSale=(frmBanHang)UiField<Dictionary<Type,Form>>(cancelLogout,"cuaSoDangMo")[typeof(frmBanHang)];
        UiField<TextBox>(draftSale,"boxMaVoucher").Text="KEEP-ON-LOGOUT";
        dialogs=AnswerDialogs(()=>UiField<Button>(cancelLogout,"btnDangXuat").PerformClick(),7);
        Check(dialogs.Count==1 && dialogs[0].DefaultButton==7 && !cancelLogout.DaDangXuat && !cancelLogout.IsDisposed,"No is the logout default and cancels logout");
        Check(UiField<TextBox>(draftSale,"boxMaVoucher").Text=="KEEP-ON-LOGOUT","cancelled logout preserves draft input");
        cancelLogout.Dispose();
    }
}
