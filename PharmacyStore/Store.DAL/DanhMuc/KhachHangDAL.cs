using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class KhachHangDAL
{
    private readonly DBConnection db = new();
    public DataTable GetAllKhachHang() => db.DocBang("SELECT Id AS MaKH, FullName AS TenKH, Phone AS DienThoai, Points AS Diem FROM Customers ORDER BY Id");
    public bool InsertKhachHang(string id, string tenKH, string dienThoai, string diem) => db.CapNhatDuLieu(
        "INSERT INTO Customers(FullName, Phone, Points) VALUES(@FullName, @Phone, @Points)",
        new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = tenKH.Trim() },
            new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = dienThoai.Trim() },
            new SqlParameter("@Points", SqlDbType.Int) { Value = int.Parse(diem) });
    public bool UpdateKhachHang(string id, string tenKH, string dienThoai, string diem) => db.CapNhatDuLieu(
        "UPDATE Customers SET FullName = @FullName, Phone = @Phone, Points = @Points WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) },
        new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = tenKH.Trim() },
            new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = dienThoai.Trim() },
            new SqlParameter("@Points", SqlDbType.Int) { Value = int.Parse(diem) });
    public bool DeleteKhachHang(string id) => db.CapNhatDuLieu(
        "DELETE FROM Customers WHERE Id = @id",
        new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) });
    public DataTable TimKiemKhachHang(string keyword) => db.DocBang(
        "SELECT Id AS MaKH, FullName AS TenKH, Phone AS DienThoai, Points AS Diem FROM Customers WHERE FullName LIKE @kw OR Phone LIKE @kw OR CAST(Id AS NVARCHAR(20)) LIKE @kw ORDER BY FullName",
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

}
