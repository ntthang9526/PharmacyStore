namespace Store.POS;

public partial class frmMain
{
    private Panel? vungDangTai;
    private int soLuotDangTai;
    private IDisposable? taiLanDau;

    private void ConfigureLoading()
    {
        if (PosFormStyle.IsInDesigner(this)) return;
        vungDangTai = new Panel
        {
            Name = "vungDangTai", Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(242, 246, 248),
            Visible = false
        };
        vungDangTai.Controls.Add(new Label
        {
            Dock=DockStyle.Fill,Text="Đang tải...",TextAlign=ContentAlignment.MiddleCenter,
            ForeColor=Color.FromArgb(15,118,110),Font=new Font("Segoe UI",15F)
        });
        contentHost.Controls.Add(vungDangTai);
        taiLanDau = HienDangTai();
    }

    internal IDisposable HienDangTai()
    {
        soLuotDangTai++;
        UseWaitCursor=true;
        GiuVungDangTai();
        // Paint the shell before synchronous form Load handlers query the database.
        if (Visible) { lblTitle.Refresh(); panelChucNang.Refresh(); vungDangTai?.Refresh(); }
        return new LuotDangTai(this);
    }

    private void GiuVungDangTai()
    {
        if (soLuotDangTai <= 0 || vungDangTai == null || IsDisposed) return;
        vungDangTai.Show(); vungDangTai.BringToFront();
    }

    private sealed class LuotDangTai(frmMain main) : IDisposable
    {
        private frmMain? owner = main;
        public void Dispose()
        {
            var current = owner;
            if (current == null) return;
            owner = null;
            current.soLuotDangTai--;
            if (!current.IsDisposed && current.soLuotDangTai == 0)
            {
                current.vungDangTai?.Hide();current.UseWaitCursor=false;
            }
        }
    }
}
