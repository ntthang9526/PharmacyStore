using System.Data;
using System.Reflection;
using Store.BLL;
using Store.DAL;
using Store.POS;

internal static partial class Program
{
    static readonly BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
    static T UiField<T>(object target, string name) => (T)target.GetType().GetField(name, PrivateInstance)!.GetValue(target)!;
    static IEnumerable<DataGridView> UiGrids(Control control)
    {
        foreach (Control child in control.Controls)
            if (child is DataGridView grid) yield return grid;
            else foreach (var nested in UiGrids(child)) yield return nested;
    }
    static void InvokePrivate(object target, string name, params object[] args)
        => target.GetType().GetMethod(name, PrivateInstance)!.Invoke(target, args);
    static void Offscreen(Form form)
    {
        if(form is frmMain) form.WindowState=FormWindowState.Normal;
        form.ShowInTaskbar = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-20000, -20000);
        form.Show();
        Application.DoEvents();
    }
    static void ClickBackground(Form form)
    {
        var heading = form.Controls.Find("posHeading", true).Single();
        typeof(Control).GetMethod("OnMouseDown", PrivateInstance)!.Invoke(heading,
            new object[] { new MouseEventArgs(MouseButtons.Left, 1, 5, 5, 0) });
        Application.DoEvents();
    }
    static Form CreateCatalogDialog(bool supplier) => supplier ? new frmThemDanhMuc() : new frmThemNhomThuoc();
    static int QuickCreatedId(Form form) => (int)form.GetType().GetProperty("CreatedId")!.GetValue(form)!;
    static void TestUiRequests()
    {
        var groups = new NhomThuocBLL();
        var suppliers = new NhaCungCapBLL();
        Check(!groups.ThemNhanhNhom("   ", out _, out _), "empty category rejected");
        Check(!groups.ThemNhanhNhom(new string('a',101), out _, out _), "overlong category rejected");
        Check(!groups.ThemNhanhNhom("abc\ndef", out _, out _), "control characters rejected");
        Check(!suppliers.ThemNhanhNCC("", "0901234567", "Test", out _, out _), "empty supplier rejected");
        Check(!suppliers.ThemNhanhNCC(new string('a',151), "0901234567", "Test", out _, out _), "overlong supplier rejected");
        Check(!suppliers.ThemNhanhNCC("Test", "09abc", "Test", out _, out _), "invalid supplier phone rejected");
        Check(!suppliers.ThemNhanhNCC("Test", "1234567890", "Test", out _, out _), "phone without initial zero rejected");
        Check(!suppliers.ThemNhanhNCC("Test", "0901234567", "", out _, out _), "empty address rejected");
        Check(!suppliers.ThemNhanhNCC("Test", "0901234567", new string('a',251), out _, out _), "overlong address rejected");
        Check(DanhMucValidation.NameError("Nhóm O'Brien – số 1",100)==null, "Vietnamese names/apostrophes allowed");

        foreach (bool supplier in new[] {false,true})
        {
            using var dialog = CreateCatalogDialog(supplier);
            Offscreen(dialog);
            UiField<Button>(dialog,"btnLuu").PerformClick();
            Check(QuickCreatedId(dialog)==0 && dialog.DialogResult!=DialogResult.OK && UiField<Label>(dialog,"lblLoi").Text.Length>0,
                "quick dialog blocks blank save: supplier="+supplier);
            UiField<TextBox>(dialog,"txtTen").Text="Keep entered text";
            UiField<Button>(dialog,"btnHuy").PerformClick();
            Check(QuickCreatedId(dialog)==0, "cancel creates no record: supplier="+supplier);
        }

        int groupId=0, supplierId=0;
        string groupName=prefix+" Nhóm O'Brien";
        string supplierName=prefix+" Nhà cung cấp";
        string phone="09"+Random.Shared.Next(10000000,99999999);
        try
        {
            using var form=new frmQuanLyThuoc();
            Offscreen(form);
            var grid=UiField<DataGridView>(form,"dgvThuoc");
            Check(grid.Rows.Count>0 && grid.SelectedRows.Count==0 && grid.CurrentCell==null,"medicine grid opens with no selection");
            Check(!grid.Columns["MaNCC"].Visible && !grid.Columns["MaNhom"].Visible && grid.Columns["TenNCC"].Visible && grid.Columns["TenNhom"].Visible,
                "medicine grid shows related names and hides keys");
            Check(grid.Rows[0].Cells["TenNCC"].Value is string && grid.Rows[0].Cells["TenNhom"].Value is string,"joined supplier/category names loaded");
            grid.Focus();
            grid.CurrentCell=grid.Rows[0].Cells["TenThuoc"];
            grid.Rows[0].Selected=true;
            InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(grid.Columns["TenThuoc"].Index,0));
            Check(UiField<ComboBox>(form,"cboNhaCungCap").SelectedValue?.Equals(grid.Rows[0].Cells["MaNCC"].Value)==true,
                "editing still uses hidden supplier ID");
            ClickBackground(form);
            Check(grid.SelectedRows.Count==0 && grid.CurrentCell==null,"background click clears selected row and current cell");
            grid.Focus();grid.CurrentCell=grid.Rows[0].Cells["TenThuoc"];grid.Rows[0].Selected=true;
            UiField<Button>(form,"btnThem").Focus();Application.DoEvents();
            Check(grid.SelectedRows.Count==0 && grid.CurrentCell==null,"focus outside grid clears selection");
            UiField<Button>(form,"btnThem").PerformClick();
            UiField<TextBox>(form,"txtTenThuoc").Text="Thuốc đang nhập";
            UiField<TextBox>(form,"txtDonGia").Text="45000";
            UiField<ComboBox>(form,"cboDonViTinh").Text="Hộp";

            foreach (bool supplier in new[] {false,true})
            {
                bool saved=false;
                Exception? failure=null;
                using var timer=new System.Windows.Forms.Timer { Interval=50 };
                timer.Tick+=(_,_)=>
                {
                    var dialog=Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType() == (supplier ? typeof(frmThemDanhMuc) : typeof(frmThemNhomThuoc)));
                    if(dialog==null) return;
                    timer.Stop();
                    try
                    {
                        dialog.Location=new Point(-20000,-20000);
                        UiField<TextBox>(dialog,"txtTen").Text=supplier?supplierName:groupName;
                        if(supplier) { UiField<TextBox>(dialog,"txtDienThoai").Text=phone; UiField<TextBox>(dialog,"txtDiaChi").Text="Địa chỉ thử nghiệm"; }
                        UiField<Button>(dialog,"btnLuu").PerformClick();
                        saved=dialog.DialogResult==DialogResult.OK;
                        if(!saved) throw new Exception(UiField<Label>(dialog,"lblLoi").Text);
                        if(supplier) supplierId=QuickCreatedId(dialog); else groupId=QuickCreatedId(dialog);
                    }
                    catch(Exception ex) { failure=ex; dialog.Close(); }
                };
                timer.Start();
                UiField<Button>(form,supplier?"btnThemNCC":"btnThemNhom").PerformClick();
                timer.Stop();
                if(failure!=null) throw failure;
                Check(saved,"quick-create from medicine form succeeds: supplier="+supplier);
                var combo=UiField<ComboBox>(form,supplier?"cboNhaCungCap":"cboNhomThuoc");
                Check(Convert.ToInt32(combo.SelectedValue)==(supplier?supplierId:groupId) && combo.Text==(supplier?supplierName:groupName),
                    "new item auto-selected by name with correct hidden ID");
                Check(UiField<TextBox>(form,"txtTenThuoc").Text=="Thuốc đang nhập" && UiField<TextBox>(form,"txtDonGia").Text=="45000" && UiField<ComboBox>(form,"cboDonViTinh").Text=="Hộp",
                    "quick-create preserves medicine draft");
            }
            Check(!groups.ThemNhanhNhom("  "+groupName.ToLowerInvariant()+"  ",out _,out _),"duplicate category rejected despite case/outer spaces");
            Check(!suppliers.ThemNhanhNCC(supplierName.ToLowerInvariant(),"098"+Random.Shared.Next(1000000,9999999),"Test",out _,out _),"duplicate supplier name rejected");
            Check(!suppliers.ThemNhanhNCC(prefix+" Other supplier",phone,"Test",out _,out _),"duplicate supplier phone rejected");
            using var suppliersForm=new frmNhaCungCap();
            Offscreen(suppliersForm);
            Check(UiField<TextBox>(suppliersForm,"txtTimKiem").PlaceholderText.Length>0,"supplier search is a labelled text input");
            Directory.CreateDirectory("artifacts/previews");
            using var snapshot=new Bitmap(form.Width,form.Height);
            form.DrawToBitmap(snapshot,new Rectangle(0,0,snapshot.Width,snapshot.Height));
            snapshot.Save("artifacts/previews/frmQuanLyThuoc-quick-add.png");
        }
        finally
        {
            if(groupId>0) new NhomThuocDAL().DeleteNhomThuoc(groupId.ToString());
            if(supplierId>0) new NhaCungCapDAL().DeleteNhaCungCap(supplierId.ToString());
        }

        using var importForm=new frmNhapKho();
        Offscreen(importForm);
        var lines=UiField<DataTable>(importForm,"dtPhieu");
        lines.Rows.Add(1,"Panadol","Hộp","TEST",DateTime.Today.AddYears(1),1,100m);
        Application.DoEvents();
        var importGrid=UiField<DataGridView>(importForm,"dgvPhieu");
        Check(importGrid.SelectedRows.Count==0 && importGrid.CurrentCell==null,"newly bound stock-in lines stay unselected");
        importGrid.Focus();importGrid.CurrentCell=importGrid.Rows[0].Cells["TenThuoc"];importGrid.Rows[0].Selected=true;
        typeof(DataGridView).GetMethod("OnCellClick",PrivateInstance)!.Invoke(importGrid,new object[]{new DataGridViewCellEventArgs(importGrid.Columns["TenThuoc"].Index,0)});
        ClickBackground(importForm);
        Check(importGrid.CurrentCell==null && importGrid.SelectedRows.Count==0,"stock-in line highlight clears outside");
        UiField<Button>(importForm,"btnXoaDong").PerformClick();
        Check(lines.Rows.Count==0,"stock-in remove still deletes the explicitly chosen line after deselection");
    }
}
