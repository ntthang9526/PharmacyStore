using System.Data;
using Store.BLL;
using Store.DAL;
using Store.POS;
internal static partial class Program
{
    static void TestExpirySoonOnly()
    {
        string tag="EXP"+Guid.NewGuid().ToString("N")[..8];var ids=new List<int>();
        try
        {
            foreach(var item in new[]{("OLD",-1,2),("TODAY",0,2),("SOON",1,2),("LAST",29,2),("LIMIT",30,2),("FAR",31,2),("EMPTY",2,0)})
                ids.Add(Count("INSERT INTO Batches(MedicineId,BatchNumber,ExpiryDate,Stock) OUTPUT INSERTED.Id VALUES(@m,@n,DATEADD(day,@d,CAST(GETDATE() AS date)),@s)",
                    ("@m",medicineId),("@n",tag+item.Item1),("@d",item.Item2),("@s",item.Item3)));
            var alerts=new CanhBaoBLL().LayLoSapHetHan();
            var fixture=alerts.Rows.Cast<DataRow>().Where(r=>((string)r["SoLo"]).StartsWith(tag)).ToList();
            Check(fixture.Count==3 && new[]{"TODAY","SOON","LAST"}.All(s=>fixture.Any(r=>(string)r["SoLo"]==tag+s)),"expiry alerts include today through day 29 only");
            Check(alerts.Rows.Cast<DataRow>().All(r=>(int)r["SoNgayConLai"]>=0 && (int)r["SoNgayConLai"]<30 && (int)r["TonLo"]>0),"expired, empty and outside-threshold batches are excluded");
            Check(new CanhBaoBLL().LayLoSapHetHan(1).Rows.Cast<DataRow>().Count(r=>((string)r["SoLo"]).StartsWith(tag))==1,"expiry filter respects custom day threshold");
            Check(Count("SELECT Stock FROM Batches WHERE Id=@id",("@id",ids[0]))==2,"expired batch data and inventory remain untouched");
            using(var form=new frmCanhBao())
            {
                Offscreen(form);var grid=UiField<DataGridView>(form,"dgvSapHetHan");
                var full=new CanhBaoBLL().LayLoSapHetHan(30,true);
                Check(grid.Rows.Count==full.Rows.Count && grid.Rows.Cast<DataGridViewRow>().Any(row=>Convert.ToInt32(row.Cells["SoNgayConLai"].Value)<0),"F5 includes expired batches");
                var remaining=full.AsEnumerable().Select(row=>row.Field<int>("SoNgayConLai")).ToList();
                int firstExpired=remaining.FindIndex(days=>days<0);
                Check(firstExpired>=0 && remaining.Skip(firstExpired).All(days=>days<0) && remaining.Take(firstExpired).SequenceEqual(remaining.Take(firstExpired).OrderBy(days=>days)),"F5 lists nearest upcoming expiry first, expired batches last");
                var expiredRow=grid.Rows.Cast<DataGridViewRow>().First(row=>Convert.ToInt32(row.Cells["SoNgayConLai"].Value)<0);
                Check(expiredRow.Cells["SoNgayConLai"].FormattedValue.ToString()=="Hết hạn" && expiredRow.Cells["TrangThai"].Value.ToString()=="Hết hạn","F5 displays expired label instead of a negative number");
                Check(UiField<GroupBox>(form,"grpSapHetHan").Text.Contains("đã hết hạn"),"F5 caption includes expired batches");
            }
            using(var main=new frmMain())
            {
                Offscreen(main);
                Check(UiField<Label>(main,"lblSapHetHan").Text==alerts.Rows.Count.ToString(),"home expiry count uses the same upcoming-only data");
                Check(UiField<DataGridView>(main,"dgvSapHetHan").Rows.Cast<DataGridViewRow>().All(row=>Convert.ToInt32(row.Cells["SoNgayConLai"].Value)>=0),"home list excludes expired batches");
                Check(UiField<GroupBox>(main,"groupBox20").Text=="Lô sắp hết hạn","home caption no longer includes expired lots");main.Dispose();
            }
        }
        finally {foreach(int id in ids) Scalar("DELETE FROM Batches WHERE Id=@id",("@id",id));}
    }
}
