using System.Reflection;
using Store.POS;

internal static partial class Program
{
    static void TestSyncPoll()
    {
        using var main=new frmMain();Offscreen(main);
        var components=UiField<System.ComponentModel.IContainer>(main,"components");
        Check(components.Components.OfType<System.Windows.Forms.Timer>().Count(t=>t.Interval==15000)==1,"database polling timer runs every 15 seconds");
        Check(!components.Components.OfType<System.Windows.Forms.Timer>().Any(t=>t.Interval==600000),"old ten-minute polling timer removed");
        var poll=typeof(frmMain).GetMethod("KiemTraDongBoDatabaseAsync",PrivateInstance)!;
        Task Run(Func<long?> read)=>(Task)poll.Invoke(main,[read])!;
        void Finish(Task task)
        {
            var deadline=DateTime.UtcNow.AddSeconds(5);
            while(!task.IsCompleted && DateTime.UtcNow<deadline){Application.DoEvents();Thread.Sleep(5);}
            Check(task.IsCompleted,"poll finishes without blocking the message loop");task.GetAwaiter().GetResult();
            SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
        }
        long before=UiField<long>(main,"phienBanDatabase");int reads=0,ui=Environment.CurrentManagedThreadId,worker=ui;
        using var gate=new ManualResetEventSlim();
        var first=Run(()=>{Interlocked.Increment(ref reads);worker=Environment.CurrentManagedThreadId;gate.Wait();return before;});
        Check(!first.IsCompleted && main.Enabled,"slow version query does not lock the main form");
        var duplicate=Run(()=>{Interlocked.Increment(ref reads);return before;});
        Check(duplicate.IsCompleted,"a second poll is skipped while one is running");
        bool heartbeat=false;using var timer=new System.Windows.Forms.Timer{Interval=30};timer.Tick+=(_,_)=>{heartbeat=true;gate.Set();timer.Stop();};timer.Start();Finish(first);
        Check(heartbeat && worker!=ui && reads==1,"UI responds and only one background query runs");
        Check(UiField<long>(main,"phienBanDatabase")==before,"unchanged version does not reload forms");
        Finish(Run(()=>null));Check(UiField<long>(main,"phienBanDatabase")==before,"failed connection does not advance the last observed database version");
        Finish(Run(()=>throw new InvalidOperationException("test")));Check(!UiField<bool>(main,"dangKiemTraDatabase"),"failed query releases the polling guard");
        Finish(Run(()=>before));Check(!UiField<bool>(main,"dangKiemTraDatabase"),"polling can continue after an error");
        gate.Reset();var closing=Run(()=>{gate.Wait();return before+1;});main.Dispose();gate.Set();Finish(closing);
        Check(UiField<long>(main,"phienBanDatabase")==before,"late response does not synchronize a disposed main form");
    }
}
