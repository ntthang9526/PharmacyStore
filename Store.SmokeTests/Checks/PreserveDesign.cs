using System.Reflection;
using System.Data;
using Store.POS;
internal static partial class Program
{
    static void TestPreserveDesign()
    {
        var style=typeof(frmMain).Assembly.GetType("Store.POS.PosFormStyle")!.GetMethod("ApplyTableStyle",PrivateStatic)!;
        using var grid=new DataGridView {Width=150,Height=180,RowHeadersVisible=false,BorderStyle=BorderStyle.None,AllowUserToAddRows=false};
        grid.Columns.Add(new DataGridViewTextBoxColumn {Name="Id",DataPropertyName="Id",Width=70});
        grid.Columns.Add(new DataGridViewTextBoxColumn {Name="Name",DataPropertyName="Name",Width=130});
        style.Invoke(null,new object[]{grid});grid.Columns[1].Width=185;
        var data=new DataTable();data.Columns.Add("Id");data.Columns.Add("Name");data.Rows.Add("1","Thuốc A");grid.DataSource=data;
        typeof(DataGridView).GetMethod("OnDataBindingComplete",PrivateInstance,null,new[]{typeof(DataGridViewBindingCompleteEventArgs)},null)!.Invoke(grid,new object[]{new DataGridViewBindingCompleteEventArgs(System.ComponentModel.ListChangedType.Reset)});
        Check(grid.Columns[1].Width==185,"refresh keeps a newly edited width instead of the initialization width");
        grid.Width=450;style.Invoke(null,new object[]{grid});grid.Width=150;
        Check(grid.Columns[1].Width==185,"repeated styling and resize keep the latest base width");
        grid.Columns.RemoveAt(0);grid.Width=300;
        Check(grid.Columns.Count==1 && grid.Columns[0].Width==300,"a removed column is not resurrected during resizing");
        using(var sales=new frmBanHang())
        {
            var search=UiField<DataGridView>(sales,"tableTimKiem");var cart=UiField<DataGridView>(sales,"tableGioHang");
            Check(search.DataSource==null && cart.DataSource==null,"Designer grids are detached from model sample binding sources");
            Check(search.Columns.Count==3 && cart.Columns.Count==7,"sales retains only the intended columns before runtime data loads");
        }
        var session=typeof(frmMain).Assembly.GetType("Store.POS.PhienDangNhap")!;
        var role=session.GetProperty("VaiTro",BindingFlags.Public|BindingFlags.Static)!;
        var permissions=typeof(frmMain).Assembly.GetType("Store.POS.PhanQuyenGiaoDien")!.GetMethod("ApDung",BindingFlags.Public|BindingFlags.Static)!;
        object? previous=role.GetValue(null);
        try
        {
            role.SetValue(null,"ADMIN");
            using var form=new frmQuanLyThuoc();form.Site=new FormDesignSite(form);Offscreen(form);form.Site=null;
            var button=UiField<Button>(form,"btnSua");var table=(TableLayoutPanel)button.Parent!;
            table.SetCellPosition(button,new TableLayoutPanelCellPosition(0,2));button.Margin=new Padding(9);table.RowStyles[0].Height=73;
            var position=table.GetCellPosition(button);button.Visible=false;
            permissions.Invoke(null,new object[]{form});permissions.Invoke(null,new object[]{form});
            Check(table.GetCellPosition(button).Equals(position) && button.Margin==new Padding(9) && table.RowStyles[0].Height==73,"repeated permission refresh preserves designed button positions, margins and row heights");
            Check(!button.Visible,"permission refresh does not show a button hidden by the designer");
            role.SetValue(null,"STAFF");permissions.Invoke(null,new object[]{form});
            Check(!UiField<Button>(form,"btnThem").Visible && !button.Visible,"staff permissions still hide restricted actions");
            Check(table.GetCellPosition(button).Equals(position),"staff permissions leave Designer button placement intact");
            role.SetValue(null,"ADMIN");
            using var stock=new frmNhapKho();stock.Site=new FormDesignSite(stock);Offscreen(stock);stock.Site=null;
            var addSupplier=UiField<Button>(stock,"btnThemNCC");var addMedicine=UiField<Button>(stock,"btnTaoThuoc");
            var quickPanel=(TableLayoutPanel)addSupplier.Parent!;var quickStyle=quickPanel.ColumnStyles[quickPanel.GetColumn(addSupplier)];
            quickStyle.Width=75;addMedicine.Visible=false;
            permissions.Invoke(null,new object[]{stock});
            Check(quickStyle.Width==75 && !addMedicine.Visible,"admin refresh preserves custom quick-add width and designer-hidden buttons");
            role.SetValue(null,"STAFF");permissions.Invoke(null,new object[]{stock});
            Check(!addSupplier.Visible && quickStyle.Width==0,"staff cannot use restricted quick-add actions");
            role.SetValue(null,"ADMIN");permissions.Invoke(null,new object[]{stock});
            Check(addSupplier.Visible && addSupplier.Enabled && quickStyle.Width==75 && !addMedicine.Visible,"role changes restore actual design visibility and width instead of fixed defaults");
        }
        finally {role.SetValue(null,previous);}
    }
    const BindingFlags PrivateStatic=BindingFlags.NonPublic|BindingFlags.Static;
}
