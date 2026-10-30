using Store.POS;
using Store.DAL;
using System.Reflection;

internal static partial class Program
{
    static void TestAsyncLoading()
    {
        bool uiOnly=Environment.GetCommandLineArgs().Contains("--background-ui");
        var catalog=uiOnly?new Dictionary<int,Store.Shared.Models.Medicine>():new MedicineDAL().GetMedicines();
        foreach(var medicine in catalog.Values.Take(12))
            Check(medicine.AvailableStock==new MedicineDAL().SoLuongConHan(medicine.Id),"batched availability matches live stock for "+medicine.Id);
        var helper=typeof(frmMain).Assembly.GetType("Store.POS.PosBackground")!;
        var load=helper.GetMethod("LoadAsync",BindingFlags.Static|BindingFlags.NonPublic)!.MakeGenericMethod(typeof(int));
        using var owner=new Form();Offscreen(owner);
        int uiThread=Environment.CurrentManagedThreadId,readThread=uiThread,applyThread=0,applied=0;bool failed=false;
        using var gate=new ManualResetEventSlim();
        var task=(Task<bool>)load.Invoke(null,[owner,(Func<int>)(()=>{readThread=Environment.CurrentManagedThreadId;gate.Wait();return 7;}),(Action<int>)(x=>{applyThread=Environment.CurrentManagedThreadId;applied=x;}),(Action<Exception>)(_=>failed=true)])!;
        Check(!task.IsCompleted && !owner.Enabled,"slow data read returns control and disables duplicate actions");
        bool heartbeat=false;using var timer=new System.Windows.Forms.Timer{Interval=30};timer.Tick+=(_,_)=>{heartbeat=true;gate.Set();timer.Stop();};timer.Start();
        WaitForLoads();
        Check(task.GetAwaiter().GetResult() && heartbeat,"UI timer still runs during the slow read");
        Console.WriteLine($"Threads UI={uiThread}, read={readThread}, apply={applyThread}, applied={applied}, context={SynchronizationContext.Current?.GetType().Name}");
        Check(readThread!=uiThread && applyThread==uiThread && applied==7,"SQL work is on worker thread and binding is on UI thread");
        Check(owner.Enabled && !owner.UseWaitCursor && !failed,"successful load restores controls");
        task=(Task<bool>)load.Invoke(null,[owner,(Func<int>)(()=>throw new InvalidOperationException("test")),(Action<int>)(_=>applied=99),(Action<Exception>)(_=>failed=true)])!;
        WaitForLoads();Check(!task.GetAwaiter().GetResult() && failed && applied==7 && owner.Enabled,"failed read preserves displayed result and restores controls");
        using var closing=new Form();Offscreen(closing);gate.Reset();applied=0;
        task=(Task<bool>)load.Invoke(null,[closing,(Func<int>)(()=>{gate.Wait();return 1;}),(Action<int>)(_=>applied++),(Action<Exception>)(_=>failed=true)])!;
        closing.Dispose();gate.Set();
        var deadline=DateTime.UtcNow.AddSeconds(5);while(!task.IsCompleted && DateTime.UtcNow<deadline){Application.DoEvents();Thread.Sleep(5);}
        Check(task.IsCompleted && !task.GetAwaiter().GetResult() && applied==0,"closed form is not updated by late results");
        if(uiOnly) return;
        using var sale=new frmBanHang();Offscreen(sale);
        Check(sale.Enabled,"sales initial load finishes asynchronously");
        using var invoices=new frmHoaDon();Offscreen(invoices);
        Check(UiField<DataGridView>(invoices,"dgvHoaDon").Rows.Count>0,"invoice background load displays saved invoices");
        using var report=new frmBaoCao();Offscreen(report);
        Check(UiField<DataGridView>(report,"dgvHoaDon").Rows.Count>0,"report background load displays paid invoices");
    }
}
