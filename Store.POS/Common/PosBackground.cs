namespace Store.POS;

internal static class PosBackground
{
    internal static async Task<bool> LoadAsync<T>(Form form,Func<T> read,Action<T> apply,Action<Exception> fail)
    {
        if(form.IsDisposed || form.Disposing || !form.Enabled) return false;
        using var loading=PosLoading.Show(form);
        form.Enabled=false;form.UseWaitCursor=true;
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
            if(!form.IsDisposed && !form.Disposing) {form.UseWaitCursor=false;form.Enabled=true;}
        }
    }
}
