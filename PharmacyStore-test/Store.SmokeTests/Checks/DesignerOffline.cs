using System.ComponentModel;
using Store.POS;
using Store.POS.FormBanHang;

internal static partial class Program
{
    private sealed class FormDesignSite(IComponent component) : ISite
    {
        public IComponent Component => component;
        public IContainer? Container => null;
        public bool DesignMode => true;
        public string? Name {get;set;}
        public object? GetService(Type serviceType) => null;
    }
    static void TestDesignerOffline()
    {
        var previous=Environment.GetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING");
        Environment.SetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING",
            "Server=invalid-designer-host;Database=PharmacyStoreTrialDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=1;");
        try
        {
            foreach(var type in new[]{typeof(frmDangNhap),typeof(frmMain),typeof(frmBanHang),typeof(frmThanhToanQR),typeof(frmQuanLyThuoc),typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmNhapKho),typeof(frmCanhBao),typeof(frmBaoCao),typeof(frmLoHang),typeof(frmVoucher),typeof(frmHoaDon),typeof(frmNhanVien),typeof(frmThemThuoc),typeof(frmThemDanhMuc),typeof(frmThemNhomThuoc)})
            {
                using var form=(Form)Activator.CreateInstance(type)!;
                form.Site=new FormDesignSite(form);
                Offscreen(form);
                Check(form.Controls.Count>0,type.Name+": opens Design without SQL/network");
            }
        }
        finally {Environment.SetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING",previous);}
    }
}
