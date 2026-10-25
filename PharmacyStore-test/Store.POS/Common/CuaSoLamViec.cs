namespace Store.POS;

internal interface ILamMoiDuLieu
{
    void LamMoiDuLieu();
}

internal static class CuaSoLamViec
{
    internal static void TamAn(Form form)
    {
        if ((form.Parent?.FindForm() ?? form.Owner) is frmMain main) main.VeTrangChu();
        else form.WindowState = FormWindowState.Minimized;
    }
}
