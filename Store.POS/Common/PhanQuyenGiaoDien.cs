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
            return c.Name is "btnThem" or "btnSua" or "btnXoa" or "btnLuu" or "btnThemNhom" or "btnThemNCC" or "btnBoQua" or "btnHuy" or "btnNhapExcel";
        return false;
    }
    public static void ApDung(Form form)
    {
        if(PosFormStyle.IsInDesigner(form)) return;
        bool allowed=PhienDangNhap.LaAdmin;
        foreach(var c in Controls(form))
        {
            if(!Cam(form,c)) continue;
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
        SapXepNutNhanVien(form, allowed);
    }
    private static void SapXepNutNhanVien(Form form, bool allowed)
    {
        if (form is frmKhachHang)
        {
            var btnThem = form.Controls.Find("btnThem", true).FirstOrDefault() as Button;
            var btnSua = form.Controls.Find("btnSua", true).FirstOrDefault() as Button;
            var btnXoa = form.Controls.Find("btnXoa", true).FirstOrDefault() as Button;
            var btnLamMoi = (form.Controls.Find("btnLamMoi", true).FirstOrDefault() ?? form.Controls.Find("btnLamLai", true).FirstOrDefault()) as Button;
            if (btnThem?.Parent is TableLayoutPanel tbl)
            {
                tbl.SuspendLayout();
                if (!allowed)
                {
                    tbl.SetCellPosition(btnThem, new TableLayoutPanelCellPosition(0, 0));
                    btnThem.Margin = new Padding(0, 4, 4, 4);
                    if (btnSua != null)
                    {
                        tbl.SetCellPosition(btnSua, new TableLayoutPanelCellPosition(1, 0));
                        btnSua.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnLamMoi != null)
                    {
                        tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(0, 1));
                        tbl.SetColumnSpan(btnLamMoi, 2);
                        btnLamMoi.Margin = new Padding(0, 4, 0, 4);
                    }
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 44F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                }
                else
                {
                    tbl.SetCellPosition(btnThem, new TableLayoutPanelCellPosition(0, 0));
                    btnThem.Margin = new Padding(0, 4, 4, 4);
                    if (btnSua != null)
                    {
                        tbl.SetCellPosition(btnSua, new TableLayoutPanelCellPosition(1, 0));
                        btnSua.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnXoa != null)
                    {
                        tbl.SetCellPosition(btnXoa, new TableLayoutPanelCellPosition(0, 1));
                        btnXoa.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnLamMoi != null)
                    {
                        tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(1, 1));
                        tbl.SetColumnSpan(btnLamMoi, 1);
                        btnLamMoi.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 44F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                }
                tbl.ResumeLayout(true);
            }
            if (form.Controls.Find("tableLayoutPanel8", true).FirstOrDefault() is TableLayoutPanel pnlInputs)
            {
                if (pnlInputs.RowStyles.Count > 2)
                    pnlInputs.RowStyles[2].Height = allowed ? 56F : 0F;
            }
        }
        else if (form is frmNhaCungCap or frmNhomThuoc)
        {
            var btnThem = form.Controls.Find("btnThem", true).FirstOrDefault() as Button;
            var btnSua = form.Controls.Find("btnSua", true).FirstOrDefault() as Button;
            var btnXoa = form.Controls.Find("btnXoa", true).FirstOrDefault() as Button;
            var btnLamMoi = form.Controls.Find("btnLamMoi", true).FirstOrDefault() as Button;
            if (btnLamMoi?.Parent is TableLayoutPanel tbl)
            {
                tbl.SuspendLayout();
                if (!allowed)
                {
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(0, 0));
                    tbl.SetColumnSpan(btnLamMoi, 2);
                    btnLamMoi.Margin = new Padding(0, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 0F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                }
                else
                {
                    if (btnThem != null)
                    {
                        tbl.SetCellPosition(btnThem, new TableLayoutPanelCellPosition(0, 0));
                        btnThem.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnSua != null)
                    {
                        tbl.SetCellPosition(btnSua, new TableLayoutPanelCellPosition(1, 0));
                        btnSua.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnXoa != null)
                    {
                        tbl.SetCellPosition(btnXoa, new TableLayoutPanelCellPosition(0, 1));
                        btnXoa.Margin = new Padding(0, 4, 4, 4);
                    }
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(1, 1));
                    tbl.SetColumnSpan(btnLamMoi, 1);
                    btnLamMoi.Margin = new Padding(4, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 44F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                }
                tbl.ResumeLayout(true);
            }
        }
        else if (form is frmVoucher)
        {
            var btnThem = form.Controls.Find("btnThem", true).FirstOrDefault() as Button;
            var btnSua = form.Controls.Find("btnSua", true).FirstOrDefault() as Button;
            var btnLuu = form.Controls.Find("btnLuu", true).FirstOrDefault() as Button;
            var btnHuy = form.Controls.Find("btnHuy", true).FirstOrDefault() as Button;
            var btnXoa = form.Controls.Find("btnXoa", true).FirstOrDefault() as Button;
            var btnLamMoi = form.Controls.Find("btnLamMoi", true).FirstOrDefault() as Button;
            if (btnLamMoi?.Parent is TableLayoutPanel tbl)
            {
                tbl.SuspendLayout();
                if (!allowed)
                {
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(0, 0));
                    tbl.SetColumnSpan(btnLamMoi, 2);
                    btnLamMoi.Margin = new Padding(0, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 0F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                    if (tbl.RowStyles.Count > 3) tbl.RowStyles[3].Height = 0F;
                }
                else
                {
                    if (btnThem != null)
                    {
                        tbl.SetCellPosition(btnThem, new TableLayoutPanelCellPosition(0, 0));
                        btnThem.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnSua != null)
                    {
                        tbl.SetCellPosition(btnSua, new TableLayoutPanelCellPosition(1, 0));
                        btnSua.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnLuu != null)
                    {
                        tbl.SetCellPosition(btnLuu, new TableLayoutPanelCellPosition(0, 1));
                        btnLuu.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnHuy != null)
                    {
                        tbl.SetCellPosition(btnHuy, new TableLayoutPanelCellPosition(1, 1));
                        btnHuy.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnXoa != null)
                    {
                        tbl.SetCellPosition(btnXoa, new TableLayoutPanelCellPosition(0, 2));
                        btnXoa.Margin = new Padding(0, 4, 4, 4);
                    }
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(1, 2));
                    tbl.SetColumnSpan(btnLamMoi, 1);
                    btnLamMoi.Margin = new Padding(4, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 44F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 44F;
                    if (tbl.RowStyles.Count > 3) tbl.RowStyles[3].Height = 0F;
                }
                tbl.ResumeLayout(true);
            }
        }
        else if (form is frmQuanLyThuoc)
        {
            var btnThem = form.Controls.Find("btnThem", true).FirstOrDefault() as Button;
            var btnSua = form.Controls.Find("btnSua", true).FirstOrDefault() as Button;
            var btnXoa = form.Controls.Find("btnXoa", true).FirstOrDefault() as Button;
            var btnLuu = form.Controls.Find("btnLuu", true).FirstOrDefault() as Button;
            var btnBoQua = form.Controls.Find("btnBoQua", true).FirstOrDefault() as Button;
            var btnLamMoi = form.Controls.Find("btnLamMoi", true).FirstOrDefault() as Button;
            if (btnLamMoi?.Parent is TableLayoutPanel tbl)
            {
                tbl.SuspendLayout();
                if (!allowed)
                {
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(0, 0));
                    tbl.SetColumnSpan(btnLamMoi, 2);
                    btnLamMoi.Margin = new Padding(0, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 0F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 0F;
                }
                else
                {
                    if (btnThem != null)
                    {
                        tbl.SetCellPosition(btnThem, new TableLayoutPanelCellPosition(0, 0));
                        btnThem.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnSua != null)
                    {
                        tbl.SetCellPosition(btnSua, new TableLayoutPanelCellPosition(1, 0));
                        btnSua.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnXoa != null)
                    {
                        tbl.SetCellPosition(btnXoa, new TableLayoutPanelCellPosition(0, 1));
                        btnXoa.Margin = new Padding(0, 4, 4, 4);
                    }
                    if (btnLuu != null)
                    {
                        tbl.SetCellPosition(btnLuu, new TableLayoutPanelCellPosition(1, 1));
                        tbl.SetColumnSpan(btnLuu, 1);
                        btnLuu.Margin = new Padding(4, 4, 0, 4);
                    }
                    if (btnBoQua != null)
                    {
                        tbl.SetCellPosition(btnBoQua, new TableLayoutPanelCellPosition(0, 2));
                        btnBoQua.Margin = new Padding(0, 4, 4, 4);
                    }
                    tbl.SetCellPosition(btnLamMoi, new TableLayoutPanelCellPosition(1, 2));
                    tbl.SetColumnSpan(btnLamMoi, 1);
                    btnLamMoi.Margin = new Padding(4, 4, 0, 4);
                    if (tbl.RowStyles.Count > 0) tbl.RowStyles[0].Height = 44F;
                    if (tbl.RowStyles.Count > 1) tbl.RowStyles[1].Height = 44F;
                    if (tbl.RowStyles.Count > 2) tbl.RowStyles[2].Height = 44F;
                }
                tbl.ResumeLayout(true);
            }
        }
    }
}
