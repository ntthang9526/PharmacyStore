using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class HoaDonDAL
{
    private const string SelectSql = @"
        SELECT {0} o.Id AS MaHD, o.OrderDate AS NgayLap, e.FullName AS NhanVien,
               COALESCE(c.FullName,N'Khách lẻ') AS KhachHang,
               COALESCE(v.Code,'') AS Voucher, o.TotalAmount AS TongTien,
               o.DiscountAmount AS GiamGia, o.TotalAmount-o.DiscountAmount AS ThucThu
        FROM Orders o JOIN Employees e ON e.Id=o.EmployeeId
        LEFT JOIN Customers c ON c.Id=o.CustomerId LEFT JOIN Vouchers v ON v.Id=o.VoucherId ";

    public DataTable TimKiem(string keyword, DateTime? from, DateTime? to) => new DBConnection().DocBang(
        string.Format(SelectSql, "") + @"
        WHERE (@from IS NULL OR o.OrderDate>=@from) AND (@to IS NULL OR o.OrderDate<DATEADD(day,1,@to))
          AND (CAST(o.Id AS NVARCHAR(20)) LIKE @kw OR
               ('HD'+CASE WHEN o.Id<100000 THEN RIGHT('00000'+CAST(o.Id AS VARCHAR(20)),5) ELSE CAST(o.Id AS VARCHAR(20)) END) LIKE @kw
               OR e.FullName LIKE @kw OR COALESCE(c.FullName,N'Khách lẻ') LIKE @kw OR c.Phone LIKE @kw OR v.Code LIKE @kw)
        ORDER BY o.OrderDate DESC,o.Id DESC",
        new SqlParameter("@kw",SqlDbType.NVarChar,-1) {Value=DBConnection.LikePattern(keyword)},
        new SqlParameter("@from",SqlDbType.Date) {Value=(object?)from?.Date??DBNull.Value},
        new SqlParameter("@to",SqlDbType.Date) {Value=(object?)to?.Date??DBNull.Value});

    public DataTable LayGanNhat() => new DBConnection().DocBang(
        string.Format(SelectSql,"TOP (5)")+" ORDER BY o.OrderDate DESC,o.Id DESC");

    public DataTable ThongKeHomNay() => new DBConnection().DocBang(@"
        SELECT COUNT(*) AS SoHoaDon, COALESCE(SUM(TotalAmount-DiscountAmount),0) AS DoanhThu
        FROM Orders WHERE OrderDate>=CAST(GETDATE() AS DATE) AND OrderDate<DATEADD(day,1,CAST(GETDATE() AS DATE))");

    public DataTable ChiTiet(int id) => new DBConnection().DocBang(@"
        SELECT m.Name AS TenThuoc,m.Unit AS DonViTinh,b.BatchNumber AS SoLo,
               d.Quantity AS SoLuong,d.UnitPrice AS DonGia,d.Quantity*d.UnitPrice AS ThanhTien,
               COALESCE(d.DosageInstruction,'') AS HuongDan
        FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId JOIN Medicines m ON m.Id=b.MedicineId
        WHERE d.OrderId=@id ORDER BY d.Id",new SqlParameter("@id",SqlDbType.Int) {Value=id});
}
