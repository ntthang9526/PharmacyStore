using System.Data;
using System.Reflection;
using Store.POS;
using Store.DAL;

internal static partial class Program
{
    static void TestFillAlert()
    {
        using(var form=new frmNhomThuoc())
        {
            Offscreen(form);
            var grid=UiField<DataGridView>(form,"dgvNhomThuoc");
            var data=new DataTable();data.Columns.Add("MaNhom");data.Columns.Add("TenNhom");data.Rows.Add("1","Thuốc A");grid.DataSource=data;
            Application.DoEvents();
            int Width()=>grid.Columns.Cast<DataGridViewColumn>().Sum(c=>c.Width);
            Check(grid.AutoSizeColumnsMode==DataGridViewAutoSizeColumnsMode.Fill && Math.Abs(Width()-(grid.ClientSize.Width-2))<=3,"Designer columns fill the available frame");
            int idWidth=grid.Columns[0].Width;int nameWidth=grid.Columns[1].Width;
            Check(nameWidth>idWidth*2,"name column receives more space than the ID column");
            data.Rows[0][1]=new string('W',160);Application.DoEvents();
            Check(grid.Columns[0].Width==idWidth && grid.Columns[1].Width==nameWidth,"long names cannot override Designer column widths");
            form.ClientSize=new Size(1100,700);Application.DoEvents();
            Check(grid.Columns[0].Width==idWidth && Math.Abs(Width()-(grid.ClientSize.Width-2))<=3,"resizing preserves compact IDs and fills the name column");
        }
        var settings=typeof(frmCanhBao).Assembly.GetType("Store.POS.CanhBaoSettings")!;
        var flags=BindingFlags.Static|BindingFlags.NonPublic;
        string path=(string)settings.GetProperty("FilePath",flags)!.GetValue(null)!;
        byte[]? previous=File.Exists(path)?File.ReadAllBytes(path):null;
        int original=(int)settings.GetProperty("NguongTon",flags)!.GetValue(null)!;
        int value=original==23?24:23;
        try
        {
            using(var alerts=new frmCanhBao())
            {
                Offscreen(alerts);var number=UiField<NumericUpDown>(alerts,"nudNguongTon");
                number.Text=value.ToString();
                var key=new KeyEventArgs(Keys.Enter);InvokePrivate(alerts,"nudNguongTon_KeyDown",number,key);
                Check(key.SuppressKeyPress && File.ReadAllText(path)==value.ToString(),"Enter saves the typed stock threshold without a new button");
                Check(UiField<GroupBox>(alerts,"grpSapHet").Text.Contains("< "+value),"saved threshold refreshes the low-stock list");
            }
            using(var reopened=new frmCanhBao())
            {
                Offscreen(reopened);
                Check(UiField<NumericUpDown>(reopened,"nudNguongTon").Value==value,"threshold survives closing and reopening the form");
            }
            using(var main=new frmMain())
            {
                Offscreen(main);
                Check(UiField<Label>(main,"label13").Text=="Tồn còn hạn dưới "+value,"home uses the saved stock threshold");main.Dispose();
            }
            try {settings.GetMethod("Luu",flags)!.Invoke(null,new object[]{0});Check(false,"zero must be rejected");}
            catch(TargetInvocationException ex) when(ex.InnerException is ArgumentOutOfRangeException)
            {Check(File.ReadAllText(path)==value.ToString(),"invalid threshold cannot replace the saved value");}
        }
        finally
        {
            if(previous==null) File.Delete(path);else File.WriteAllBytes(path,previous);
            DBConnection.NotifyDataChanged();
        }
    }
}
