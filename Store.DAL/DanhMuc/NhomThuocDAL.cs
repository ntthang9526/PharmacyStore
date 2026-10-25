using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class NhomThuocDAL
{
    private readonly DBConnection db = new();
    public DataTable GetAllNhomThuoc() => db.DocBang("SELECT Id AS MaNhom, Name AS TenNhom FROM Categories ORDER BY Id");
    public bool InsertNhomThuoc(string id, string tenNhom) => db.CapNhatDuLieu(
        "INSERT INTO Categories(Name) VALUES(@Name)",
        new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = tenNhom.Trim() });
    public bool UpdateNhomThuoc(string id, string tenNhom) => db.CapNhatDuLieu(
        "UPDATE Categories SET Name = @Name WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) },
        new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = tenNhom.Trim() });
    public bool DeleteNhomThuoc(string id) => db.CapNhatDuLieu(
        "DELETE FROM Categories WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) });

    public int ThemNhanh(string name)
    {
        using var connection = DBConnection.GetDbConnection();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        PhanQuyenDAL.YeuCau(Quyen.QuanTri,connection,transaction);
        using var command = new SqlCommand(@"
            IF EXISTS (SELECT 1 FROM Categories WITH (UPDLOCK, HOLDLOCK)
                       WHERE UPPER(LTRIM(RTRIM(Name))) = UPPER(@name))
                THROW 50010, N'Nhóm thuốc này đã tồn tại.', 1;
            INSERT INTO Categories(Name) VALUES(@name);
            SELECT CAST(SCOPE_IDENTITY() AS INT);", connection, transaction);
        command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name;
        int id = Convert.ToInt32(command.ExecuteScalar());
        transaction.Commit();
        DBConnection.NotifyDataChanged();
        return id;
    }

    public DataTable TimKiemNhomThuoc(string keyword) => db.DocBang(
        "SELECT Id AS MaNhom, Name AS TenNhom FROM Categories WHERE Name LIKE @kw OR CAST(Id AS NVARCHAR(20)) LIKE @kw ORDER BY Name",
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

}
