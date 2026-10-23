using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class NhaCungCapDAL
{
    private readonly DBConnection db = new();
    public DataTable GetAllNhaCungCap() => db.DocBang("SELECT Id AS MaNCC, Name AS TenNCC, Phone AS DienThoai, Address AS DiaChi FROM Suppliers ORDER BY Id");
    public bool InsertNhaCungCap(string id, string tenNCC, string dienThoai, string diaChi) => db.CapNhatDuLieu(
        "INSERT INTO Suppliers(Name, Phone, Address) VALUES(@Name, @Phone, @Address)",
        new SqlParameter("@Name", SqlDbType.NVarChar, 150) { Value = tenNCC.Trim() },
            new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = dienThoai.Trim() },
            new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = diaChi.Trim() });
    public bool UpdateNhaCungCap(string id, string tenNCC, string dienThoai, string diaChi) => db.CapNhatDuLieu(
        "UPDATE Suppliers SET Name = @Name, Phone = @Phone, Address = @Address WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) },
        new SqlParameter("@Name", SqlDbType.NVarChar, 150) { Value = tenNCC.Trim() },
            new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = dienThoai.Trim() },
            new SqlParameter("@Address", SqlDbType.NVarChar, 250) { Value = diaChi.Trim() });
    public bool DeleteNhaCungCap(string id) => db.CapNhatDuLieu(
        "DELETE FROM Suppliers WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) });

    public int ThemNhanh(string name, string phone, string address)
    {
        using var connection = DBConnection.GetDbConnection();
        using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        using var command = new SqlCommand(@"
            IF EXISTS (SELECT 1 FROM Suppliers WITH (UPDLOCK, HOLDLOCK)
                       WHERE UPPER(LTRIM(RTRIM(Name))) = UPPER(@name) OR Phone = @phone)
                THROW 50011, N'Tên hoặc số điện thoại nhà cung cấp đã tồn tại.', 1;
            INSERT INTO Suppliers(Name, Phone, Address) VALUES(@name, @phone, @address);
            SELECT CAST(SCOPE_IDENTITY() AS INT);", connection, transaction);
        command.Parameters.Add("@name", SqlDbType.NVarChar, 150).Value = name;
        command.Parameters.Add("@phone", SqlDbType.VarChar, 15).Value = phone;
        command.Parameters.Add("@address", SqlDbType.NVarChar, 250).Value = address;
        int id = Convert.ToInt32(command.ExecuteScalar());
        transaction.Commit();
        DBConnection.NotifyDataChanged();
        return id;
    }

    public DataTable TimKiemNhaCungCap(string keyword) => db.DocBang(
        "SELECT Id AS MaNCC, Name AS TenNCC, Phone AS DienThoai, Address AS DiaChi FROM Suppliers WHERE Name LIKE @kw OR Phone LIKE @kw OR Address LIKE @kw OR CAST(Id AS NVARCHAR(20)) LIKE @kw ORDER BY Name",
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

}
