using System.Data;
using Microsoft.Data.SqlClient;
namespace Store.DAL;
public partial class HoaDonDAL
{
    private const string SelectSql = @"
        SELECT {0} o.Id AS MaHD, o.OrderDate AS NgayLap, COALESCE(o.EmployeeNameSnapshot,e.FullName) AS NhanVien,
               COALESCE(o.CustomerNameSnapshot,c.FullName,N'Khách lẻ') AS KhachHang,
               COALESCE(o.VoucherCodeSnapshot,v.Code,'') AS Voucher, o.PaymentMethod AS HinhThucThanhToan, o.PaymentStatus,
               CASE o.PaymentStatus WHEN 'PENDING' THEN N'Chờ thanh toán' WHEN 'CANCELLED' THEN N'Đã hủy' ELSE N'Đã thanh toán' END AS TrangThai,
               o.PaidAt AS NgayThanhToan, COALESCE(o.PaidEmployeeNameSnapshot,pe.FullName) AS NhanVienThuTien, o.TotalAmount AS TongTien,
               o.DiscountAmount AS GiamGia, CASE WHEN o.PaymentStatus='PAID' THEN o.TotalAmount-o.DiscountAmount ELSE 0 END AS ThucThu,
               o.TotalAmount-o.DiscountAmount AS GiaTriHoaDon,
               CASE WHEN o.PaymentStatus='PENDING' THEN o.TotalAmount-o.DiscountAmount ELSE 0 END AS CanThanhToan,
               o.OrderVersion,o.ModifiedAt,COALESCE(o.ModifiedEmployeeNameSnapshot,me.FullName) AS NhanVienSua,o.CancelledAt,COALESCE(o.CancelledEmployeeNameSnapshot,ce.FullName) AS NhanVienHuy,o.CancelReason,o.PrescriptionNumber,o.PatientName,o.Prescriber,o.PrescriptionDate,o.PrescriptionCheckedBy,o.PrescriptionCheckedAt,
               CASE WHEN o.EmployeeNameSnapshot IS NOT NULL AND o.CustomerNameSnapshot IS NOT NULL AND o.CustomerPhoneSnapshot IS NOT NULL AND o.VoucherCodeSnapshot IS NOT NULL
                    AND (o.PaymentStatus<>'PAID' OR o.PaidEmployeeNameSnapshot IS NOT NULL)
                    AND (o.ModifiedAt IS NULL OR o.ModifiedEmployeeNameSnapshot IS NOT NULL)
                    AND (o.CancelledAt IS NULL OR o.CancelledEmployeeNameSnapshot IS NOT NULL)
                    THEN N'Lưu lúc giao dịch' ELSE N'Chưa đủ lịch sử; phần thiếu theo danh mục hiện tại' END AS NguonDauHoaDon
        FROM Orders o JOIN Employees e ON e.Id=o.EmployeeId
        LEFT JOIN Customers c ON c.Id=o.CustomerId LEFT JOIN Vouchers v ON v.Id=o.VoucherId LEFT JOIN Employees pe ON pe.Id=o.PaidEmployeeId
        LEFT JOIN Employees me ON me.Id=o.ModifiedEmployeeId LEFT JOIN Employees ce ON ce.Id=o.CancelledEmployeeId ";

    public DataTable TimKiem(string keyword, DateTime? from, DateTime? to,bool chiCho=false) => new DBConnection().DocBang(
        string.Format(SelectSql, "") + @"
        WHERE (@pending=0 OR o.PaymentStatus='PENDING') AND (@from IS NULL OR COALESCE(o.PaidAt,o.OrderDate)>=@from) AND (@to IS NULL OR COALESCE(o.PaidAt,o.OrderDate)<DATEADD(day,1,@to))
          AND (CAST(o.Id AS NVARCHAR(20)) LIKE @kw OR
               ('HD'+CASE WHEN o.Id<100000 THEN RIGHT('00000'+CAST(o.Id AS VARCHAR(20)),5) ELSE CAST(o.Id AS VARCHAR(20)) END) LIKE @kw
               OR COALESCE(o.EmployeeNameSnapshot,e.FullName) LIKE @kw OR COALESCE(o.PaidEmployeeNameSnapshot,pe.FullName) LIKE @kw
               OR COALESCE(o.CustomerNameSnapshot,c.FullName,N'Khách lẻ') LIKE @kw OR COALESCE(o.CustomerPhoneSnapshot,c.Phone,'') LIKE @kw OR COALESCE(o.VoucherCodeSnapshot,v.Code,'') LIKE @kw)
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

    private const string DetailSql = @"
        SELECT COALESCE(d.MedicineNameSnapshot,m.Name) AS TenThuoc,COALESCE(d.MedicineUnitSnapshot,m.Unit) AS DonViTinh,b.BatchNumber AS SoLo,
               d.Quantity AS SoLuong,d.UnitPrice AS DonGia,d.Quantity*d.UnitPrice AS ThanhTien,
               COALESCE(d.DosageInstruction,'') AS HuongDan,COALESCE(d.IsPrescriptionSnapshot,m.IsPrescription) AS KeDon, CASE WHEN d.MedicineNameSnapshot IS NOT NULL AND d.MedicineUnitSnapshot IS NOT NULL AND d.IsPrescriptionSnapshot IS NOT NULL THEN N'Lưu lúc lập chứng từ' ELSE N'Chưa có lịch sử; danh mục hiện tại' END AS NguonThongTin
        FROM OrderDetails d JOIN Batches b ON b.Id=d.BatchId JOIN Medicines m ON m.Id=b.MedicineId
        WHERE d.OrderId=@id
        UNION ALL
        SELECT COALESCE(d.MedicineNameSnapshot,m.Name),COALESCE(d.MedicineUnitSnapshot,m.Unit),N'Chưa phân lô',d.Quantity,d.UnitPrice,d.Quantity*d.UnitPrice,COALESCE(d.DosageInstruction,''),COALESCE(d.IsPrescriptionSnapshot,m.IsPrescription), CASE WHEN d.MedicineNameSnapshot IS NOT NULL AND d.MedicineUnitSnapshot IS NOT NULL AND d.IsPrescriptionSnapshot IS NOT NULL THEN N'Lưu lúc lập chứng từ' ELSE N'Chưa có lịch sử; danh mục hiện tại' END
        FROM OrderPendingItems d JOIN Medicines m ON m.Id=d.MedicineId JOIN Orders o ON o.Id=d.OrderId
        WHERE d.OrderId=@id AND o.PaymentStatus IN ('PENDING','CANCELLED')";
    public DataTable ChiTiet(int id) => new DBConnection().DocBang(DetailSql,new SqlParameter("@id",SqlDbType.Int){Value=id});

    public DataSet LayDonCho(int id)
    {
        using var conn=DBConnection.GetDbConnection();
        using var tran=conn.BeginTransaction(IsolationLevel.Serializable);
        PhanQuyenDAL.YeuCau(Quyen.BanHang,conn,tran);
        using var cmd=new SqlCommand(@"SELECT o.Id,o.CustomerId,o.OrderVersion,COALESCE(v.Code,'') AS Voucher,o.PrescriptionNumber,o.PatientName,o.Prescriber,o.PrescriptionDate,o.PrescriptionCheckedBy
            FROM Orders o LEFT JOIN Vouchers v ON v.Id=o.VoucherId WHERE o.Id=@id AND o.PaymentStatus='PENDING';
            SELECT d.MedicineId,COALESCE(d.MedicineNameSnapshot,m.Name) AS TenThuoc,
                   COALESCE(d.MedicineUnitSnapshot,m.Unit) AS DonViTinh,d.Quantity,d.UnitPrice,COALESCE(d.DosageInstruction,'') AS HuongDan,COALESCE(d.IsPrescriptionSnapshot,0) | m.IsPrescription AS KeDon
            FROM OrderPendingItems d JOIN Medicines m ON m.Id=d.MedicineId WHERE d.OrderId=@id ORDER BY d.Id",conn,tran);
        cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
        using var adapter=new SqlDataAdapter(cmd);var data=new DataSet();adapter.Fill(data);
        if(data.Tables[0].Rows.Count!=1) throw new InvalidOperationException("Hóa đơn đã thanh toán, đã hủy hoặc không còn tồn tại.");
        tran.Commit();return data;
    }
}
