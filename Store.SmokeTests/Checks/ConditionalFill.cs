using System.Reflection;
using System.Data;
using Store.POS;
internal static partial class Program
{
    static void TestConditionalFill()
    {
        using var host = new Form { ClientSize = new Size(500,300) };
        var grid = new DataGridView { Width=300, Height=200, RowHeadersVisible=false, BorderStyle=BorderStyle.None, AllowUserToAddRows=false, AutoGenerateColumns=false };
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name="Id",DataPropertyName="Id",Width=70 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name="Name",DataPropertyName="Name",Width=130 });
        host.Controls.Add(grid);host.Site=new FormDesignSite(host);Offscreen(host);
        typeof(frmMain).Assembly.GetType("Store.POS.PosFormStyle")!.GetMethod("ApplyTableStyle",BindingFlags.Static|BindingFlags.NonPublic)!.Invoke(null,new object[]{grid});
        int Total()=>grid.Columns.Cast<DataGridViewColumn>().Sum(c=>c.Width);
        Check(Total()==300 && grid.Columns[0].Width==70,"spare space fills the name column while compact IDs retain their width");
        grid.Width=500;Application.DoEvents();
        Check(Total()==500 && grid.Columns[0].Width==70,"a wider frame fills without changing compact columns");
        grid.Width=150;Application.DoEvents();
        Check(grid.Columns[0].Width==70 && grid.Columns[1].Width==130,"a narrow frame restores Designer widths instead of squeezing columns");
        grid.Width=300;Application.DoEvents();
        Check(Total()==300,"expanding again does not accumulate extra width");
        var data=new DataTable();data.Columns.Add("Id");data.Columns.Add("Name");data.Rows.Add("1",new string('W',200));grid.DataSource=data;Application.DoEvents();
        Check(Total()==300 && grid.Columns[0].Width==70,"long cell content does not change the designed widths");
        using var sales=new frmBanHang();sales.Site=new FormDesignSite(sales);Offscreen(sales);
        var cart=UiField<DataGridView>(sales,"tableGioHang");var search=UiField<DataGridView>(sales,"tableTimKiem");
        Check(cart.Columns.Count==7 && search.Columns.Count==3,"restored sales form has the original seven cart columns and three search columns");
        Check(cart.Columns.Contains("btnAction") && search.Columns.Contains("btnThemGioHang"),"sales action buttons remain attached to their grids");
        Check(typeof(frmBanHang).GetField("cardKhachHang",BindingFlags.NonPublic|BindingFlags.Instance)==null,"sales layout uses the pre-push customer/payment area");
        using var preview=new Bitmap(sales.Width,sales.Height);sales.DrawToBitmap(preview,sales.ClientRectangle);
        Directory.CreateDirectory("artifacts/restore-sales-fill");preview.Save("artifacts/restore-sales-fill/sales.png");
    }
}
