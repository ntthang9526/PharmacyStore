using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class ThuocDAL
{
    public int ThemNhanh(string name, int categoryId, int supplierId, string unit, decimal price)
    {
        using var conn = DBConnection.GetDbConnection();
        using var transaction = conn.BeginTransaction(IsolationLevel.Serializable);
        PhanQuyenDAL.YeuCau(Quyen.QuanTri,conn,transaction);
        using var cmd = new SqlCommand(@"
            IF EXISTS (SELECT 1 FROM Medicines WITH (UPDLOCK,HOLDLOCK)
                       WHERE IsActive=1 AND SupplierId=@sup AND UPPER(LTRIM(RTRIM(Name)))=UPPER(@name)
                         AND UPPER(LTRIM(RTRIM(Unit)))=UPPER(@unit))
                THROW 50012, N'Thuốc này đã có trong danh sách của nhà cung cấp.', 1;
            INSERT INTO Medicines(Name,CategoryId,SupplierId,Unit,Price)
            VALUES(@name,@cat,@sup,@unit,@price);
            SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, transaction);
        cmd.Parameters.Add("@name",SqlDbType.NVarChar,150).Value=name;
        cmd.Parameters.Add("@cat",SqlDbType.Int).Value=categoryId;
        cmd.Parameters.Add("@sup",SqlDbType.Int).Value=supplierId;
        cmd.Parameters.Add("@unit",SqlDbType.NVarChar,30).Value=unit;
        cmd.Parameters.Add("@price",SqlDbType.Decimal).Value=Store.Shared.TienTe.LamTron(price);
        cmd.Parameters["@price"].Precision=18;cmd.Parameters["@price"].Scale=2;
        int id=Convert.ToInt32(cmd.ExecuteScalar());
        transaction.Commit();
        DBConnection.NotifyDataChanged();
        return id;
    }
}
