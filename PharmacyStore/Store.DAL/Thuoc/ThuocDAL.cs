using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class ThuocDAL
{
    private readonly DBConnection db = new();
    private const string SelectSql = @"
        SELECT m.Id AS MaThuoc, m.Name AS TenThuoc, m.CategoryId AS MaNhom,
               m.SupplierId AS MaNCC, c.Name AS TenNhom, s.Name AS TenNCC, m.Unit AS DonViTinh, m.Price AS DonGia,
               ISNULL(SUM(b.Stock), 0) AS SoLuongTon
        FROM Medicines m
        JOIN Categories c ON c.Id = m.CategoryId
        JOIN Suppliers s ON s.Id = m.SupplierId
        LEFT JOIN Batches b ON b.MedicineId = m.Id
        WHERE m.IsActive = 1 {0}
        GROUP BY m.Id, m.Name, m.CategoryId, m.SupplierId, c.Name, s.Name, m.Unit, m.Price ORDER BY m.Id";
    public DataTable GetAllThuoc() => db.DocBang(string.Format(SelectSql, ""));
    public DataTable TimKiemThuoc(string keyword) => db.DocBang(
        string.Format(SelectSql, "AND (CAST(m.Id AS NVARCHAR(20)) LIKE @kw OR m.Name LIKE @kw OR c.Name LIKE @kw OR s.Name LIKE @kw OR m.BarCode LIKE @kw)"),
        new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });

    public bool InsertThuoc(string id, string name, string category, string unit, decimal price,
                            int stock, int supplierId, out string error)
        => Save(true, id, name, category, unit, price, supplierId, out error);
    public bool UpdateThuoc(string id, string name, string category, string unit, decimal price,
                            int stock, int supplierId, out string error)
        => Save(false, id, name, category, unit, price, supplierId, out error);

    private bool Save(bool insert, string id, string name, string category, string unit,
                      decimal price, int supplierId, out string error)
    {
        error = "";
        if (string.IsNullOrWhiteSpace(name) || name.Length > 150 ||
            string.IsNullOrWhiteSpace(unit) || unit.Length > 30 || price < 0 ||
            !int.TryParse(category, out int categoryId) || categoryId <= 0 || supplierId <= 0)
        { error = "Vui lòng nhập tên, nhóm thuốc, nhà cung cấp, đơn vị và đơn giá hợp lệ."; return false; }
        try
        {
            return db.CapNhatDuLieu(insert
                ? "INSERT INTO Medicines(CategoryId,SupplierId,Name,Unit,Price) VALUES(@cat,@sup,@name,@unit,@price)"
                : "UPDATE Medicines SET CategoryId=@cat,SupplierId=@sup,Name=@name,Unit=@unit,Price=@price WHERE Id=@id",
                new SqlParameter("@id", SqlDbType.Int) { Value = insert ? 0 : int.Parse(id) },
                new SqlParameter("@cat", SqlDbType.Int) { Value = categoryId },
                new SqlParameter("@sup", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@name", SqlDbType.NVarChar, 150) { Value = name.Trim() },
                new SqlParameter("@unit", SqlDbType.NVarChar, 30) { Value = unit.Trim() },
                new SqlParameter("@price", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = price });
        }
        catch (Exception ex) { error = ex.Message; return false; }
    }
    public bool DeleteThuoc(string id, out string error)
    {
        error = "";
        try { return db.CapNhatDuLieu("UPDATE Medicines SET IsActive=0 WHERE Id=@id",
            new SqlParameter("@id", SqlDbType.Int) { Value = int.Parse(id) }); }
        catch (Exception ex) { error = ex.Message; return false; }
    }
}
