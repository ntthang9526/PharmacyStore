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
        if(form is frmLoHang) return c.Name is "btnDoiTrangThai";
        if(form is frmNhapKho) return c.Name is "btnThemNCC" or "btnTaoThuoc";
        if(form is frmKhachHang) return c.Name is "btnXoa" or "txtDiem";
        if(form is frmQuanLyThuoc or frmNhomThuoc or frmNhaCungCap or frmVoucher)
            return c.Name is "btnThem" or "btnSua" or "btnXoa" or "btnLuu" or "btnThemNhom" or "btnThemNCC" or "btnBoQua" or "btnHuy" or "btnNhapExcel" or "btnMauExcel";
        return false;
    }
    private sealed class HiddenLayout
    {
        internal readonly Dictionary<ColumnStyle, float> Widths = new();
        internal readonly Dictionary<Control, (bool Visible, bool Enabled)> Controls = new();
    }
    private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Form, HiddenLayout> hiddenLayouts = new();
    public static void ApDung(Form form)
    {
        if (PosFormStyle.IsInDesigner(form)) return;
        bool allowed = PhienDangNhap.LaAdmin;
        // Editors are recreated when roles change; admins keep their Designer layout.
        if (allowed && form is not frmNhapKho) return;
        var layout = hiddenLayouts.GetOrCreateValue(form);
        if (allowed && layout.Controls.Count == 0) return;
        foreach (var c in Controls(form))
        {
            if (!Cam(form, c)) continue;
            if (!allowed)
            {
                var properties = System.ComponentModel.TypeDescriptor.GetProperties(c);
                // Visible/Enabled getters inherit the parent's state; preserve the control's own setting.
                layout.Controls.TryAdd(c, (!properties["Visible"]!.ShouldSerializeValue(c), !properties["Enabled"]!.ShouldSerializeValue(c)));
                c.Visible = false;
                c.Enabled = false;
            }
            else if (layout.Controls.Remove(c, out var original))
            {
                c.Visible = original.Visible;
                c.Enabled = original.Enabled;
            }
            if (c.Name == "txtDiem" && c.Parent != null) c.Parent.Visible = allowed;
            if (c.Name is "btnThemNCC" or "btnThemNhom" or "btnTaoThuoc" or "btnDoiTrangThai" && c.Parent is TableLayoutPanel table)
            {
                int index = table.GetColumn(c);
                if (index < 0 || index >= table.ColumnStyles.Count) continue;
                var style = table.ColumnStyles[index];
                if (style.SizeType != SizeType.Absolute) continue;
                if (!allowed) { layout.Widths.TryAdd(style, style.Width); style.Width = 0; }
                else if (layout.Widths.Remove(style, out float width)) style.Width = width;
            }
        }
    }
}
