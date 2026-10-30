using System.Data;
using Microsoft.Data.SqlClient;

namespace Store.DAL
{
    public class BaoCaoDAL
    {
        // Doanh thu gom theo từng ngày trong khoảng [tuNgay, denNgay]
        public DataTable LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT CAST(COALESCE(o.PaidAt,o.OrderDate) AS DATE) AS Ngay,
                       COUNT(*) AS SoHoaDon,
                       SUM(o.TotalAmount) AS TongTien,
                       SUM(o.DiscountAmount) AS GiamGia,
                       SUM(o.TotalAmount - o.DiscountAmount) AS DoanhThu
                FROM Orders o
                WHERE o.PaymentStatus='PAID' AND COALESCE(o.PaidAt,o.OrderDate) >= @tu AND COALESCE(o.PaidAt,o.OrderDate) < DATEADD(DAY, 1, @den)
                GROUP BY CAST(COALESCE(o.PaidAt,o.OrderDate) AS DATE)
                ORDER BY Ngay";
            return Lay(sql, tuNgay, denNgay);
        }

        // Danh sách hóa đơn chi tiết trong khoảng
        public DataTable LayDanhSachHoaDon(DateTime tuNgay, DateTime denNgay)
        {
            const string sql = @"
                SELECT o.PaymentMethod AS HinhThucThanhToan, o.Id AS MaHD, COALESCE(o.PaidAt,o.OrderDate) AS NgayLap, COALESCE(o.EmployeeNameSnapshot,e.FullName) AS NhanVien,
                       COALESCE(o.CustomerNameSnapshot,c.FullName, N'Khách lẻ') AS KhachHang,
                       COALESCE(o.VoucherCodeSnapshot,v.Code, '') AS Voucher,
                       o.TotalAmount AS TongTien, o.DiscountAmount AS GiamGia,
                       o.TotalAmount - o.DiscountAmount AS ThucThu
                FROM Orders o
                JOIN Employees e ON e.Id = o.EmployeeId
                LEFT JOIN Vouchers v ON v.Id = o.VoucherId
                LEFT JOIN Customers c ON c.Id = o.CustomerId
                WHERE o.PaymentStatus='PAID' AND COALESCE(o.PaidAt,o.OrderDate) >= @tu AND COALESCE(o.PaidAt,o.OrderDate) < DATEADD(DAY, 1, @den)
                ORDER BY COALESCE(o.PaidAt,o.OrderDate) DESC";
            return Lay(sql, tuNgay, denNgay);
        }

        private DataTable Lay(string sql, DateTime tuNgay, DateTime denNgay)
        {
            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@tu", SqlDbType.Date).Value = tuNgay.Date;
            cmd.Parameters.Add("@den", SqlDbType.Date).Value = denNgay.Date;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
