using Store.POS;
using Store.DAL;
using System.Reflection;
internal static partial class Program
{
    static void ReviewStaffUi()
    {
        int staff=Count("SELECT TOP (1) Id FROM Employees WHERE Role='STAFF' AND IsActive=1 ORDER BY Id");
        Check(staff>0,"an existing active staff account is available; no test employee is created");
        SetTestSession(staff);
        Directory.CreateDirectory("artifacts/staff-ui-review-20261029");
        using var main=new frmMain();Offscreen(main);
        Check(PhanQuyenDAL.LayPhien().VaiTro=="STAFF","screens use the actual STAFF session");
        var menu=UiField<ContextMenuStrip>(main,"menuQuanLyKhac");
        Check(!menu.Items[6].Available,"employee management is hidden from the staff menu");
        var problems=new List<string>();
        var types=new[]{typeof(frmQuanLyThuoc),typeof(frmNhapKho),typeof(frmLoHang),typeof(frmPhieuNhap),typeof(frmHoaDon),typeof(frmNhomThuoc),typeof(frmNhaCungCap),typeof(frmKhachHang),typeof(frmVoucher),typeof(frmBaoCao),typeof(frmCanhBao),typeof(frmDoiMatKhau),typeof(frmBanHang)};
        foreach(var size in new[]{new Size(1500,820),new Size(1382,753),new Size(1100,700)})
        {
            main.ClientSize=size;Application.DoEvents();
            foreach(var type in types)
            {
                typeof(frmMain).GetMethod("MoForm",PrivateInstance)!.MakeGenericMethod(type).Invoke(main,null);Application.DoEvents();
                var form=UiField<Dictionary<Type,Form>>(main,"cuaSoDangMo")[type];
                Check(form.Visible,type.Name+" is accessible to staff at "+size.Width);
                IEnumerable<Control> Walk(Control parent)
                {
                    foreach(Control child in parent.Controls){yield return child;foreach(var nested in Walk(child))yield return nested;}
                }
                bool Forbidden(Control c)
                {
                    if(form is frmLoHang)return c.Name=="btnDoiTrangThai";
                    if(form is frmNhapKho)return c.Name is "btnThemNCC" or "btnTaoThuoc";
                    if(form is frmKhachHang)return c.Name is "btnXoa" or "txtDiem";
                    if(form is frmQuanLyThuoc or frmNhomThuoc or frmNhaCungCap or frmVoucher)
                        return c.Name is "btnThem" or "btnSua" or "btnXoa" or "btnLuu" or "btnThemNhom" or "btnThemNCC" or "btnBoQua" or "btnHuy" or "btnNhapExcel" or "btnMauExcel";
                    return false;
                }
                foreach(var c in Walk(form))
                {
                    if(Forbidden(c))Check(!c.Visible && !c.Enabled,type.Name+" hides and disables "+c.Name);
                    if(!c.Visible || c is not (Button or TextBoxBase or Label or ComboBox or NumericUpDown or DateTimePicker or DataGridView))continue;
                    var rect=c.RectangleToScreen(c.ClientRectangle);var visible=rect;
                    for(Control? parent=c.Parent;parent!=null;parent=parent.Parent)visible=Rectangle.Intersect(visible,parent.RectangleToScreen(parent.ClientRectangle));
                    if(visible.Width<rect.Width-2 || visible.Height<rect.Height-2)problems.Add(type.Name+" "+size.Width+" CLIP "+c.Name+" "+rect.Size+" -> "+visible.Size);
                    if(c is Button button && button.Text.Length>0)
                    {
                        int needed=TextRenderer.MeasureText(button.Text,button.Font).Height+button.Padding.Vertical;
                        if(button.Height<needed)problems.Add(type.Name+" "+size.Width+" BUTTON TEXT "+button.Name+" height="+button.Height+" needed="+needed);
                    }
                }
                foreach(var actions in Walk(form).OfType<FlowLayoutPanel>().Where(p=>p.Controls.Cast<Control>().Any(c=>c.Name=="btnLamMoi")))
                {
                    var visibleButtons=actions.Controls.OfType<Button>().Where(b=>b.Visible).ToArray();
                    Check(visibleButtons.Length>0 && visibleButtons.All(b=>b.Height>=38 && b.Width>=140),type.Name+" uses compact readable action buttons");
                    Check(actions.Bottom>=actions.Parent!.ClientSize.Height-actions.Parent.Padding.Bottom-2,type.Name+" actions stay at the bottom of the detail panel");
                    Check(visibleButtons.Min(b=>b.Top)<=actions.Padding.Top+4,type.Name+" has no blank action row above the visible buttons");
                }
                foreach(var grid in Walk(form).OfType<DataGridView>()) foreach(DataGridViewColumn col in grid.Columns)
                    if(col.Visible && !string.IsNullOrWhiteSpace(col.HeaderText))
                        Check(col.Width>=TextRenderer.MeasureText(col.HeaderText,col.HeaderCell.InheritedStyle.Font??grid.Font).Width+col.HeaderCell.InheritedStyle.Padding.Horizontal+24,type.Name+" header fits: "+col.HeaderText);
                if(form is frmNhapKho)Check(UiField<Button>(form,"btnThemThuoc").Visible && UiField<Button>(form,"btnLuu").Visible,"staff keeps stock-entry controls");
                if(form is frmKhachHang)Check(UiField<Button>(form,"btnThem").Visible && UiField<Button>(form,"btnSua").Visible,"staff keeps customer add/edit controls");
                using var image=new Bitmap(main.Width,main.Height);main.DrawToBitmap(image,new Rectangle(Point.Empty,image.Size));
                image.Save("artifacts/staff-ui-review-20261029/"+type.Name+"-"+size.Width+".png");
            }
        }
        File.WriteAllLines("artifacts/staff-ui-review-20261029/layout-findings.txt",problems);
        foreach(var problem in problems)Console.WriteLine(problem);
        Check(problems.Count==0,"staff controls and button captions fit at all three window sizes");
        main.Dispose();
    }
}
