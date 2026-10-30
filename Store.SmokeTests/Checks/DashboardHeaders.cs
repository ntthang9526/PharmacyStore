using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text.Json;
using Store.POS;
using Store.DAL;

internal static partial class Program
{
    static IEnumerable<Type> HeaderForms()=>typeof(frmMain).Assembly.GetTypes().Where(t=>t.IsPublic && !t.IsAbstract && t.IsSubclassOf(typeof(Form)) && t.GetConstructor(Type.EmptyTypes)!=null);
    static void MeasureHeaders()
    {
        var result=new List<object>();
        foreach(var type in HeaderForms())
        {
            using var form=(Form)Activator.CreateInstance(type)!;form.Site=new FormDesignSite(form);Offscreen(form);
            var fields=type.GetFields(PrivateInstance);
            foreach(var grid in UiGrids(form)) foreach(DataGridViewColumn col in grid.Columns)
            {
                var font=col.HeaderCell.InheritedStyle.Font??grid.Font;
                var size=TextRenderer.MeasureText(col.HeaderText,font);
                result.Add(new {Form=type.Name,Grid=fields.First(f=>ReferenceEquals(f.GetValue(form),grid)).Name,Field=fields.FirstOrDefault(f=>ReferenceEquals(f.GetValue(form),col))?.Name,Column=col.Name,Width=size.Width+col.HeaderCell.InheritedStyle.Padding.Horizontal+32,Height=size.Height+col.HeaderCell.InheritedStyle.Padding.Vertical+12});
            }
            form.Dispose();
        }
        File.WriteAllText("artifacts/fix-dashboard-headers-20261028/header-metrics.json",JsonSerializer.Serialize(result));
    }
    static void TestDashboardHeaders()
    {
        // No database writes: use an unreachable endpoint for the notification regression.
        string? connection=Environment.GetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING");
        Environment.SetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING","Server=tcp:127.0.0.1,1;Database=AuditNeverConnect;User ID=probe;Password=probe;Connect Timeout=1;ConnectRetryCount=0;Encrypt=False;");
        try
        {
            using(var alerts=new frmCanhBao())
            {
                alerts.Site=new FormDesignSite(alerts);Offscreen(alerts);var grid=UiField<DataGridView>(alerts,"dgvSapHetHan");
                var handled=(HashSet<string>)typeof(frmCanhBao).GetField("LoXuLyDaXuLy",BindingFlags.Static|BindingFlags.NonPublic)!.GetValue(null)!;
                string key="987654|HEADERTEST";handled.Remove(key);
                var data=new DataTable();data.Columns.Add("MaThuoc",typeof(int));data.Columns.Add("SoLo");data.Columns.Add("SoNgayConLai",typeof(int));data.Rows.Add(987654,"HEADERTEST",3);
                long version=DBConnection.DataVersion;grid.DataSource=data;Application.DoEvents();InvokePrivate(alerts,"KhoiPhucCheckbox");
                Check(DBConnection.DataVersion==version,"binding and restoring unchecked rows never signals a write");
                try
                {
                    grid.Rows[0].Cells[UiField<DataGridViewColumn>(alerts,"colDaXuLy").Index].Value=true;
                    Check(DBConnection.DataVersion==version+1 && handled.Contains(key),"a real checkbox change signals exactly one write");
                    version=DBConnection.DataVersion;
                    for(int i=0;i<3;i++) {grid.DataSource=data.Copy();Application.DoEvents();InvokePrivate(alerts,"KhoiPhucCheckbox");}
                    Check(DBConnection.DataVersion==version && grid.Rows[0].Cells[UiField<DataGridViewColumn>(alerts,"colDaXuLy").Index].Value is true,"repeated rebinds restore checked rows without a refresh loop");
                    InvokePrivate(alerts,"dgvSapHetHan_CellValueChanged",grid,new DataGridViewCellEventArgs(UiField<DataGridViewColumn>(alerts,"colDaXuLy").Index,0));
                    Check(DBConnection.DataVersion==version,"duplicate checkbox events never signal another write");
                }
                finally {handled.Remove(key);}
            }
            using(var main=new frmMain())
            {
                main.Site=new FormDesignSite(main);Offscreen(main);
                foreach(string name in new[]{"dgvSapHet","dgvSapHetHan","dgvGanNhat"})
                {
                    var grid=UiField<DataGridView>(main,name);Check(grid.Columns.Count>=5 && !grid.AutoGenerateColumns,name+" has explicit Designer columns");
                    var table=new DataTable();foreach(DataGridViewColumn col in grid.Columns) table.Columns.Add(col.DataPropertyName, col.DataPropertyName is "NgayLap" or "HanDung"?typeof(DateTime):col.DataPropertyName is "MaThuoc" or "MaHD" or "TonKho" or "TonLo" or "SoNgayConLai"?typeof(int):col.DataPropertyName is "TongTien" or "GiamGia" or "CanThanhToan" or "ThucThu"?typeof(decimal):typeof(string));
                    var row=table.NewRow();foreach(DataColumn col in table.Columns)row[col]=col.DataType==typeof(DateTime)?DateTime.Today:col.DataType==typeof(int)?3:col.DataType==typeof(decimal)?12000m:"Dữ liệu mẫu";table.Rows.Add(row);
                    grid.DataSource=table;Application.DoEvents();Check(grid.Rows.Count==1 && grid.Rows[0].Cells.Cast<DataGridViewCell>().All(c=>c.Value!=null),name+" shows bound values in its columns");
                }
                Check(Convert.ToInt32(UiField<DataGridView>(main,"dgvGanNhat").Rows[0].Cells["MaHD"].Value)==3,"recent invoices retain the ID used to open details");
                main.Dispose();
            }
        }
        finally {Environment.SetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING",connection);}
    }
}
