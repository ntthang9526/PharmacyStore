using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class LoHangDAL
{
    public DataTable TimKiem(string keyword)
    {
        const string sql = @"
            SELECT b.Id AS MaLo, b.BatchNumber AS SoLo, m.Name AS TenThuoc,
                   s.Name AS TenNCC, c.Name AS TenNhom, b.ExpiryDate AS HanDung,
                   b.Stock AS TonKho, nhap.SoLuongDaNhap, nhap.NgayNhapGanNhat
            FROM Batches b
            JOIN Medicines m ON m.Id = b.MedicineId
            JOIN Suppliers s ON s.Id = m.SupplierId
            JOIN Categories c ON c.Id = m.CategoryId
            OUTER APPLY (
                SELECT SUM(d.Quantity) AS SoLuongDaNhap, MAX(i.ImportDate) AS NgayNhapGanNhat
                FROM StockInDetails d JOIN StockIns i ON i.Id = d.StockInId
                WHERE d.BatchId = b.Id
            ) nhap
            WHERE b.BatchNumber LIKE @kw OR m.Name LIKE @kw OR m.BarCode LIKE @kw
               OR s.Name LIKE @kw OR c.Name LIKE @kw OR CAST(b.Id AS NVARCHAR(20)) LIKE @kw
            ORDER BY b.ExpiryDate, b.Id";
        return new DBConnection().DocBang(sql,
            new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });
    }
}
