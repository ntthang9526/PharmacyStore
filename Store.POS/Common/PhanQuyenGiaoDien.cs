using Store.DAL;
namespace Store.POS;
internal static class PhanQuyenGiaoDien
{
    public static bool ChiAdmin(Type type) => type==typeof(frmNhanVien) ||
        type==typeof(frmThemThuoc) || type==typeof(frmThemDanhMuc) || type==typeof(frmThemNhomThuoc);
    public static void Gan(Form form)
    {
        void Update() {if(!PosFormStyle.IsInDesigner(form)) ApDung(form);}
        form.Load+=(_,_)=>Update();form.VisibleChanged+=(_,_)=> {if(form.Visible) Update();};
        foreach(var c in Controls(form))
        {
            bool updating=false;
            c.VisibleChanged+=(_,_)=>
            {
                if(!PosFormStyle.IsInDesigner(form) && !PhienDangNhap.LaAdmin && c.Visible && Cam(form,c)) c.Visible=false;
            };
            c.EnabledChanged+=(_,_)=>
            {
                if(updating || PosFormStyle.IsInDesigner(form) || !c.Enabled || PhienDangNhap.LaAdmin) return;
                if(Cam(form,c)) {updating=true;c.Enabled=false;updating=false;}
            };
        }
    }
    private static IEnumerable<Control> Controls(Control parent)
    {
        foreach(Control child in parent.Controls) {yield return child;foreach(var nested in Controls(child)) yield return nested;}
    }
    private static bool Cam(Form form,Control c)
    {
        if(ChiAdmin(form.GetType())) return c is Button or TextBox or ComboBox or NumericUpDown or CheckBox;
        if(form is frmNhapKho) return c.Name is "btnThemNCC" or "btnTaoThuoc";
        if(form is frmKhachHang) return c.Name is "btnXoa" or "txtDiem";
        if(form is frmQuanLyThuoc or frmNhomThuoc or frmNhaCungCap or frmVoucher)
            return c.Name is "btnThem" or "btnSua" or "btnXoa" or "btnLuu" or "btnThemNhom" or "btnThemNCC" or "btnBoQua" or "btnHuy";
        return false;
    }
    public static void ApDung(Form form)
    {
        if(PosFormStyle.IsInDesigner(form)) return;
        foreach(var c in Controls(form))
        {
            if(!Cam(form,c)) continue;
            bool allowed=PhienDangNhap.LaAdmin;
            c.Visible=allowed;
            if(!allowed) c.Enabled=false;
            if(c.Name=="txtDiem" && c.Parent!=null) c.Parent.Visible=allowed;
            if(c.Name is "btnThemNCC" or "btnThemNhom" or "btnTaoThuoc" && c.Parent is TableLayoutPanel table)
            {
                int column=table.GetColumn(c);
                if(column>=0 && column<table.ColumnStyles.Count && table.ColumnStyles[column].SizeType==SizeType.Absolute)
                    table.ColumnStyles[column].Width=allowed?38F:0F;
            }
        }
    }
}
