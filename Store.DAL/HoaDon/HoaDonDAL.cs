using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public class HoaDonDAL
{
    private const string SelectSql = @"
        SELECT {0} o.Id AS MaHD, o.OrderDate AS NgayLap, e.FullName AS NhanVien,
               COALESCE(c.FullName,N'Khách lẻ') AS KhachHang,
               COALESCE(v.Code,'') AS Voucher, o.PaymentMethod AS HinhThucThanhToan, o.PaymentStatus,
               CASE o.PaymentStatus WHEN 'PENDING' THEN N'Chờ thanh toán' ELSE N'Đã thanh toán' END AS TrangThai,
               o.PaidAt AS NgayThanhToan, pe.FullName AS NhanVienThuTien, o.TotalAmount AS TongTien,
               o.DiscountAmount AS GiamGia, CASE WHEN o.PaymentStatus='PAID' THEN o.TotalAmount-o.DiscountAmount ELSE 0 END AS ThucThu, o.TotalAmount-o.DiscountAmount AS CanThanhToan
        FROM Orders o JOIN Employees e ON e.Id=o.EmployeeId
        LEFT JOIN Customers c ON c.Id=o.CustomerId LEFT JOIN Vouchers v ON v.Id=o.VoucherId LEFT JOIN Employees pe ON pe.Id=o.PaidEmployeeId ";

    public DataTable TimKiem(string keyword, DateTime? from, DateTime? to,bool chiCho=false) => new DBConnection().DocBang(
        string.Format(SelectSql, "") + @"
        WHERE (@pending=0 OR o.PaymentStatus='PENDING') AND (@from IS NULL OR COALESCE(o.PaidAt,o.OrderDate)>=@from) AND (@to IS NULL OR COALESCE(o.PaidAt,o.OrderDate)<DATEADD(day,1,@to))
          AND (CAST(o.Id AS NVARCHAR(20)) LIKE @kw OR
               ('HD'+CASE WHEN o.Id<100000 THEN RIGHT('00000'+CAST(o.Id AS VARCHAR(20)),5) ELSE CAST(o.Id AS VARCHAR(20)) END) LIKE @kw
               OR e.FullName LIKE @kw OR COALESCE(c.FullName,N'Khách lẻ') LIKE @kw OR c.Phone LIKE @kw OR v.Code LIKE @kw)
        ORDER BY COALESCE(o.PaidAt,o.OrderDate) DESC,o.Id DESC",
        new SqlParameter("@pending",SqlDbType.Bit){Value=chiCho},
        new SqlParameter("@kw",SqlDbType.NVarChar,-1) {Value=DBConnection.LikePattern(keyword)},
        new SqlParameter("@from",SqlDbType.Date) {Value=(object?)from?.Date??DBNull.Value},
        new SqlParameter("@to",SqlDbType.Date) {Value=(object?)to?.Date??DBNull.Value});

    public DataTable LayGanNhat() => new DBConnection().DocBang(
        string.Format(SelectSql,"TOP (5)")+" ORDER BY COALESCE(o.PaidAt,o.OrderDate) DESC,o.Id DESC");

    public DataTable ThongKeHomNay() => new DBConnection().DocBang(@"
        SELECT COUNT(*) AS SoHoaDon, COALESCE(SUM(TotalAmount-DiscountAmount),0) AS DoanhThu
        FROM Orders WHERE PaymentStatus='PAID' AND COALESCE(PaidAt,OrderDate)>=CAST(GETDATE() AS DATE) AND COALESCE(PaidAt,OrderDate)<DATEADD(day,1,CAST(GETDATE() AS DATE))");

    public DataTable ChiTiet(int id) => new DBConnection().DocBang(@"
        SELECT m.Name AS TenThuoc,m.Unit AS DonViTinh,b.BatchNumber AS SoLo,
               d.Quantity AS SoLuong,d.UnitPrice AS DonGia,d.Quantity*d.UnitPrice AS ThanhTien,
               COALESCE(d.DosageInstruction,'') AS HuongDan
        FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId JOIN Medicines m ON m.Id=b.MedicineId
        WHERE d.OrderId=@id
        UNION ALL
        SELECT m.Name,m.Unit,N'Chưa phân lô',d.Quantity,d.UnitPrice,d.Quantity*d.UnitPrice,N''
        FROM OrderPendingItems d JOIN Medicines m ON m.Id=d.MedicineId JOIN Orders o ON o.Id=d.OrderId
        WHERE d.OrderId=@id AND o.PaymentStatus='PENDING'",new SqlParameter("@id",SqlDbType.Int){Value=id});
}
