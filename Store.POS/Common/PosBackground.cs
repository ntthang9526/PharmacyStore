namespace Store.POS;

internal static class PosBackground
{
    private static readonly HashSet<Form> loadingForms=new();

    internal static async Task<bool> LoadAsync<T>(Form form,Func<T> read,Action<T> apply,Action<Exception> fail)
    {
        if(form.IsDisposed || form.Disposing || !form.Enabled || !loadingForms.Add(form)) return false;
        using var loading=PosLoading.Show(form);
        bool lockForm=form is not frmMain;
        if(lockForm) form.Enabled=false;
        form.UseWaitCursor=true;
        try
        {
            var result=await Task.Run(read);
            if(form.IsDisposed || form.Disposing) return false;
            apply(result);return true;
        }
        catch(Exception error)
        {
            if(!form.IsDisposed && !form.Disposing) fail(error);
            return false;
        }
        finally
        {
            loadingForms.Remove(form);
            if(!form.IsDisposed && !form.Disposing)
            {
                form.UseWaitCursor=false;
                if(lockForm) form.Enabled=true;
            }
        }
    }
}
