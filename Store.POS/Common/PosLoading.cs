namespace Store.POS;

internal static class PosLoading
{
    internal static IDisposable? Show(Form form)
    {
        if (PosFormStyle.IsInDesigner(form) || form.IsDisposed) return null;
        var main = form as frmMain ?? form.Parent?.FindForm() as frmMain;
        return main?.HienDangTai();
    }
}
