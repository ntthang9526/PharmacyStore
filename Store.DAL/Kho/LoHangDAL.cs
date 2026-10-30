using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class LoHangDAL
{
    public DataTable TimKiem(string keyword)
    {
        const string sql = @"
            SELECT b.Id AS MaLo, b.BatchNumber AS SoLo, m.Name AS TenThuoc,
                   COALESCE(nguon.TenNCC,N'Chưa xác định') AS TenNCC, c.Name AS TenNhom, b.ExpiryDate AS HanDung,
                   b.Stock AS TonKho, nhap.SoLuongDaNhap, nhap.NgayNhapGanNhat, gia.UnitCost AS GiaNhapGanNhat, b.IsBlocked AS KhoaLo,
                   CASE WHEN b.IsBlocked=1 THEN N'Tạm khóa'
                        WHEN b.ExpiryDate<CAST(GETDATE() AS date) THEN N'Hết hạn'
                        WHEN m.IsActive=0 THEN N'Thuốc đã ẩn'
                        WHEN m.IsSelling=0 THEN N'Thuốc ngừng bán'
                        ELSE N'Được xuất bán' END AS TrangThaiLo,
                   CASE WHEN m.IsActive=1 AND m.IsSelling=1 AND b.IsBlocked=0 AND b.ExpiryDate>=CAST(GETDATE() AS date) THEN b.Stock ELSE 0 END AS TonCoTheBan
            FROM Batches b
            JOIN Medicines m ON m.Id = b.MedicineId
            JOIN Categories c ON c.Id = m.CategoryId
            OUTER APPLY (
                SELECT STRING_AGG(CAST(n.Name AS nvarchar(max)),N'; ') WITHIN GROUP (ORDER BY n.SupplierId) AS TenNCC
                FROM (SELECT DISTINCT i.SupplierId,s.Name
                      FROM StockInDetails d JOIN StockIns i ON i.Id=d.StockInId JOIN Suppliers s ON s.Id=i.SupplierId
                      WHERE d.BatchId=b.Id) n
            ) nguon
            OUTER APPLY (
                SELECT SUM(d.Quantity) AS SoLuongDaNhap, MAX(i.ImportDate) AS NgayNhapGanNhat
                FROM StockInDetails d JOIN StockIns i ON i.Id = d.StockInId
                WHERE d.BatchId = b.Id
            ) nhap
            OUTER APPLY (SELECT TOP(1) d.UnitCost FROM StockInDetails d JOIN StockIns i ON i.Id=d.StockInId
                         WHERE d.BatchId=b.Id ORDER BY i.ImportDate DESC,i.Id DESC,d.Id DESC) gia
            WHERE b.BatchNumber LIKE @kw OR m.Name LIKE @kw OR m.BarCode LIKE @kw
               OR COALESCE(nguon.TenNCC,N'Chưa xác định') LIKE @kw OR c.Name LIKE @kw OR CAST(b.Id AS NVARCHAR(20)) LIKE @kw
            ORDER BY b.ExpiryDate, b.Id";
        return new DBConnection().DocBang(sql,
            new SqlParameter("@kw", SqlDbType.NVarChar, -1) { Value = DBConnection.LikePattern(keyword) });
    }
    public bool DoiTrangThai(int id, bool khoa, out string error)
    {
        error="";
        if(id<=0) {error="Vui lòng chọn lô hàng.";return false;}
        try
        {
            bool changed=new DBConnection().CapNhatDuLieu("UPDATE Batches SET IsBlocked=@blocked WHERE Id=@id AND IsBlocked<>@blocked",
                new SqlParameter("@id",SqlDbType.Int){Value=id},new SqlParameter("@blocked",SqlDbType.Bit){Value=khoa});
            if(!changed) error="Trạng thái lô đã thay đổi hoặc lô không còn tồn tại. Hãy làm mới danh sách.";
            return changed;
        }
        catch(Exception ex) {error=ex.Message;return false;}
    }
}
