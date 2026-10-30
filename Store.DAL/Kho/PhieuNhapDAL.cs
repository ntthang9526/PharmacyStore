using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class PhieuNhapDAL
{
    public DataTable TimKiem(string keyword,DateTime? from,DateTime? to)
    {
        using(var connection=DBConnection.GetDbConnection()) PhanQuyenDAL.YeuCau(Quyen.Xem,connection);
        return new DBConnection().DocBang(@"
            SELECT i.Id AS MaPhieu,i.ImportDate AS NgayNhap,s.Name AS TenNCC,e.FullName AS NhanVien,
                   i.TotalCost AS TongTien,i.Note AS GhiChu
            FROM StockIns i JOIN Suppliers s ON s.Id=i.SupplierId JOIN Employees e ON e.Id=i.EmployeeId
            WHERE (@from IS NULL OR i.ImportDate>=@from) AND (@to IS NULL OR i.ImportDate<DATEADD(day,1,CONVERT(datetime2,@to)))
              AND (CAST(i.Id AS varchar(20)) LIKE @kw OR s.Name LIKE @kw OR e.FullName LIKE @kw OR i.Note LIKE @kw
                   OR EXISTS(SELECT 1 FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId JOIN Medicines m ON m.Id=b.MedicineId
                             WHERE d.StockInId=i.Id AND (b.BatchNumber LIKE @kw OR COALESCE(d.MedicineNameSnapshot,m.Name) LIKE @kw OR m.BarCode LIKE @kw)))
            ORDER BY i.ImportDate DESC,i.Id DESC",
            new SqlParameter("@kw",SqlDbType.NVarChar,-1){Value=DBConnection.LikePattern(keyword)},
            new SqlParameter("@from",SqlDbType.Date){Value=(object?)from?.Date??DBNull.Value},
            new SqlParameter("@to",SqlDbType.Date){Value=(object?)to?.Date??DBNull.Value});
    }
    public DataTable ChiTiet(int id)
    {
        using(var connection=DBConnection.GetDbConnection()) PhanQuyenDAL.YeuCau(Quyen.Xem,connection);
        return new DBConnection().DocBang(@"
            SELECT COALESCE(d.MedicineNameSnapshot,m.Name) AS TenThuoc,COALESCE(d.MedicineUnitSnapshot,m.Unit) AS DonVi,b.BatchNumber AS SoLo,b.ExpiryDate AS HanDung,
                   d.Quantity AS SoLuong,d.UnitCost AS GiaNhap,d.Quantity*d.UnitCost AS ThanhTien, CASE WHEN d.MedicineNameSnapshot IS NOT NULL AND d.MedicineUnitSnapshot IS NOT NULL THEN N'Lưu lúc lập chứng từ' ELSE N'Chưa có lịch sử; danh mục hiện tại' END AS NguonThongTin
            FROM StockInDetails d JOIN Batches b ON b.Id=d.BatchId JOIN Medicines m ON m.Id=b.MedicineId
            WHERE d.StockInId=@id ORDER BY d.Id",new SqlParameter("@id",SqlDbType.Int){Value=id});
    }
}
