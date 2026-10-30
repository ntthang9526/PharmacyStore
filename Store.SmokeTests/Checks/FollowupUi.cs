using System.Data;
using System.Reflection;
using Store.BLL;
using Store.DAL;
using Store.POS;

internal static partial class Program
{
    static void TestFollowupUi()
    {
        var supplierRows = new NhaCungCapBLL().GetAllNhaCungCap();
        var categoryRows = new NhomThuocBLL().GetAllNhomThuoc();
        var customerRows = new KhachHangBLL().GetAllKhachHang();
        string supplier = (string)supplierRows.Rows[0]["TenNCC"];
        string category = (string)categoryRows.Rows[0]["TenNhom"];
        string customer = (string)customerRows.Rows[0]["TenKH"];
        Check(new NhaCungCapBLL().TimKiemNCC(supplier[..Math.Min(4,supplier.Length)]).Rows.Count>0,"supplier search accepts partial names");
        Check(new NhomThuocBLL().TimKiemNhom(category[..Math.Min(4,category.Length)]).Rows.Count>0,"category search accepts partial names");
        Check(new KhachHangBLL().TimKiemKhachHang(customer[..Math.Min(4,customer.Length)]).Rows.Count>0,"customer search accepts names");
        Check(new NhaCungCapBLL().TimKiemNCC("").Rows.Count==supplierRows.Rows.Count,"blank supplier search returns all");
        Check(new NhomThuocBLL().TimKiemNhom("").Rows.Count==categoryRows.Rows.Count,"blank category search returns all");
        Check(new KhachHangBLL().TimKiemKhachHang("").Rows.Count==customerRows.Rows.Count,"blank customer search returns all");
        foreach(var keyword in new[]{"' OR 1=1 --", "__ABSENT__[%]'"})
            Check(new NhaCungCapBLL().TimKiemNCC(keyword).Rows.Count==0 && new NhomThuocBLL().TimKiemNhom(keyword).Rows.Count==0,"special search characters stay literal: "+keyword);
        Check(new ThuocBLL().TimKiemThuoc(supplier).Rows.Count>0,"medicine search accepts supplier names");
        Check(new ThuocBLL().TimKiemThuoc(category).Rows.Count>0,"medicine search accepts category names");
        var batches = new LoHangBLL().TimKiem("");
        Check(batches.Rows.Count==Count("SELECT COUNT(*) FROM Batches"),"batch view includes every saved batch");
        if(batches.Rows.Count>0)
        {
            string number=(string)batches.Rows[0]["SoLo"];
            Check(new LoHangBLL().TimKiem(number).Rows.Count>0,"batch search accepts batch numbers");
            Check(new LoHangBLL().TimKiem((string)batches.Rows[0]["TenThuoc"]).Rows.Count>0,"batch search accepts medicine names");
            Check(new LoHangBLL().TimKiem((string)batches.Rows[0]["TenNCC"]).Rows.Count>0,"batch search accepts supplier names");
        }
        Check(new LoHangBLL().TimKiem("__ABSENT__[%]'").Rows.Count==0,"batch search safely returns no matches");

        var cases=new[]{
            (typeof(frmQuanLyThuoc),"dgvThuoc","MaThuoc","txtMaThuoc","txtTenThuoc"),
            (typeof(frmNhaCungCap),"dgvNhaCungCap","MaNCC","txtMaNCC","txtTenNCC"),
            (typeof(frmNhomThuoc),"dgvNhomThuoc","MaNhom","txtMaNhom","txtTenNhom"),
            (typeof(frmKhachHang),"dgvKhachHang","MaKH","txtMaKH","txtTenKH")};
        foreach(var (type,gridName,idColumn,idField,nameField) in cases)
        {
            using var form=(Form)Activator.CreateInstance(type)!;
            Offscreen(form);
            Check(form.Controls.Find(idField,true).Length==0,type.Name+": redundant ID input removed");
            var grid=UiField<DataGridView>(form,gridName);
            Check(grid.SelectedRows.Count==0 && grid.CurrentCell==null && UiField<TextBox>(form,nameField).Text=="",type.Name+": initial record details empty");
            grid.Focus();
            var visibleColumn=grid.Columns.Cast<DataGridViewColumn>().First(c=>c.Visible);
            grid.CurrentCell=grid.Rows[0].Cells[visibleColumn.Index];grid.Rows[0].Selected=true;
            InvokePrivate(form,gridName+"_CellClick",grid,new DataGridViewCellEventArgs(visibleColumn.Index,0));
            Check(UiField<string>(form,"maDangChon")!="" && UiField<TextBox>(form,nameField).Text!="",type.Name+": selecting row fills details");
            InvokePrivate(form,"btnSua_Click",UiField<Button>(form,"btnSua"),EventArgs.Empty);
            string previous=UiField<TextBox>(form,nameField).Text;
            typeof(Control).GetMethod("OnMouseDown",PrivateInstance)!.Invoke(UiField<TextBox>(form,nameField),new object[]{new MouseEventArgs(MouseButtons.Left,1,1,1,0)});
            Check(UiField<TextBox>(form,nameField).Text==previous && UiField<TextBox>(form,nameField).Enabled,type.Name+": clicking editable input preserves data");
            ClickBackground(form);
            Check(UiField<TextBox>(form,nameField).Text==previous && UiField<TextBox>(form,nameField).Enabled && !grid.Enabled,type.Name+": blank click preserves unsaved editor input");
            Check(grid.SelectedRows.Count==0 && grid.CurrentCell==null,type.Name+": blank click also clears grid selection");
            InvokePrivate(form,"ResetDetails",true);
            Check(UiField<string>(form,"maDangChon")=="" && UiField<TextBox>(form,nameField).Text=="" && !UiField<TextBox>(form,nameField).Enabled && grid.Enabled,type.Name+": explicit discard resets fields and unlocks list");
            if(type==typeof(frmQuanLyThuoc))
            {
                Check(UiField<ComboBox>(form,"cboNhomThuoc").SelectedIndex==-1 && UiField<ComboBox>(form,"cboNhaCungCap").SelectedIndex==-1 && UiField<TextBox>(form,"txtSoLuongTon").Text=="0","medicine reset restores all dropdown/default states");
            }
            if(type==typeof(frmNhaCungCap))
                Check(grid.Columns["MaNCC"].Visible && grid.Columns["TenNCC"].Visible && grid.Columns["DienThoai"].Visible && grid.Columns["DiaChi"].Visible,"supplier table shows full four-column record including code");
            if(type==typeof(frmNhomThuoc))
                Check(grid.Columns["MaNhom"].Visible && grid.Columns["TenNhom"].Visible,"category table shows both code and name");
        }
        foreach(var (type,fieldName,keyword) in new[]{
            (typeof(frmQuanLyThuoc),"txtTimKiem","Panadol"),
            (typeof(frmNhaCungCap),"txtTimKiem",supplier[..Math.Min(4,supplier.Length)]),
            (typeof(frmKhachHang),"txtTimKiem",customer[..Math.Min(4,customer.Length)])})
        {
            using var form=(Form)Activator.CreateInstance(type)!;Offscreen(form);
            var combo=UiField<TextBox>(form,fieldName);
            Check(combo.MaxLength==200,type.Name+": text search has no dropdown");
            combo.Text=keyword;
            var key=new KeyEventArgs(Keys.Enter);
            typeof(Control).GetMethod("OnKeyDown",PrivateInstance)!.Invoke(combo,new object[]{key});Application.DoEvents();
            var grid=UiGrids(form).Single();
            Check(key.SuppressKeyPress && grid.Rows.Count>0 && combo.Text==keyword,type.Name+": Enter performs text search without erasing keyword");
        }
        using(var form=new frmNhomThuoc())
        {
            Offscreen(form);UiField<TextBox>(form,"txtTimNhom").Text=category;
            UiField<Button>(form,"btnTimNhom").PerformClick();Application.DoEvents();
            Check(UiField<DataGridView>(form,"dgvNhomThuoc").Rows.Count>0,"new category search row is connected");
            UiField<TextBox>(form,"txtTimNhom").Text="__no_category__";
            UiField<Button>(form,"btnTimNhom").PerformClick();Application.DoEvents();
            Check(UiField<DataGridView>(form,"dgvNhomThuoc").Rows.Count==0,"category search handles empty result without selecting stale details");
        }
        using(var form=new frmLoHang())
        {
            Offscreen(form);var grid=UiField<DataGridView>(form,"dgvLoHang");
            Check(grid.ReadOnly && grid.Rows.Count==batches.Rows.Count && grid.CurrentCell==null,"batch form opens read-only with no selected row");
            UiField<TextBox>(form,"txtTimKiem").Text="__no_batch__";
            UiField<Button>(form,"btnTimKiem").PerformClick();Application.DoEvents();
            Check(grid.Rows.Count==0 && UiField<Label>(form,"lblKetQua").Text.StartsWith("0 "),"batch form updates results/status for text search");
            UiField<Button>(form,"btnLamMoi").PerformClick();Application.DoEvents();
            Check(grid.Rows.Count==batches.Rows.Count,"batch refresh restores all rows");
        }
        TestManagementMenu();
    }
}
