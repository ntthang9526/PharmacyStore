using System.Data;
using Store.POS;
using Store.BLL;

internal static partial class Program
{
    static void TestMedicineListLive()
    {
        var source=new ThuocBLL().GetAllThuoc();Check(source.Rows.Count>0,"database query returns active medicines: "+source.Rows.Count);
        using var form=new frmQuanLyThuoc();Offscreen(form);
        var grid=UiField<DataGridView>(form,"dgvThuoc");
        void HasData(string stage)
        {
            Application.DoEvents();
            Check(grid.Columns.Count>0 && grid.Rows.Count==source.Rows.Count,stage+": row count matches database");
            Check(grid.Rows.Cast<DataGridViewRow>().All(r=>r.Cells["MaThuoc"].Value!=null && !string.IsNullOrWhiteSpace(Convert.ToString(r.Cells["TenThuoc"].Value))),stage+": IDs and medicine names are displayed");
        }
        HasData("opening form");InvokePrivate(form,"dgvThuoc_CellClick",grid,new DataGridViewCellEventArgs(0,0));
        Check(!string.IsNullOrWhiteSpace(UiField<TextBox>(form,"txtTenThuoc").Text),"selecting a real medicine fills its details");
        form.LamMoiDuLieu();HasData("refreshing form");
        var nameColumn=grid.Columns["TenThuoc"]!;int editedWidth=nameColumn.Width+33;nameColumn.Width=editedWidth;
        form.LamMoiDuLieu();HasData("refreshing after manual width edit");
        Check(!grid.AutoGenerateColumns && nameColumn.Width==editedWidth,"refresh keeps explicit Designer columns and the edited Width");
        Check(!grid.Columns["MaNhom"]!.Visible && !grid.Columns["MaNCC"]!.Visible,"technical category/supplier IDs are hidden");
        UiField<TextBox>(form,"txtTimKiem").Text=Convert.ToString(source.Rows[0]["TenThuoc"])!;
        UiField<Button>(form,"btnTimKiem").PerformClick();Application.DoEvents();
        Check(grid.Rows.Count>0 && Convert.ToString(grid.Rows[0].Cells["TenThuoc"].Value)==Convert.ToString(source.Rows[0]["TenThuoc"]),"search also binds the medicine name correctly");
        UiField<Button>(form,"btnLamMoi").PerformClick();HasData("clearing search");
        Directory.CreateDirectory("artifacts/design-all-columns");using var screenshot=new Bitmap(form.Width,form.Height);
        form.DrawToBitmap(screenshot,new Rectangle(0,0,screenshot.Width,screenshot.Height));screenshot.Save("artifacts/design-all-columns/live-medicine-list.png");
    }
}
