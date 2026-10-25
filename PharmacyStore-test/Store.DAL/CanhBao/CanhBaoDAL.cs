using System.Data;
using Microsoft.Data.SqlClient;

namespace Store.DAL
{
    public class CanhBaoDAL
    {
        // Thuốc có tổng tồn (các lô CÒN HẠN) nhỏ hơn ngưỡng - kể cả thuốc chưa có lô nào (tồn = 0)
        public DataTable LayThuocSapHet(int nguongTon)
        {
            const string sql = @"
                SELECT m.Id AS MaThuoc, m.Name AS TenThuoc, m.Unit AS DonViTinh, s.Name AS NhaCungCap,
                       ISNULL(SUM(CASE WHEN b.ExpiryDate >= CAST(GETDATE() AS DATE) THEN b.Stock END), 0) AS TonKho
                FROM Medicines m
                JOIN Suppliers s ON s.Id = m.SupplierId
                LEFT JOIN Batches b ON b.MedicineId = m.Id
                WHERE m.IsActive = 1
                GROUP BY m.Id, m.Name, m.Unit, s.Name
                HAVING ISNULL(SUM(CASE WHEN b.ExpiryDate >= CAST(GETDATE() AS DATE) THEN b.Stock END), 0) < @n
                ORDER BY TonKho, m.Name";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@n", SqlDbType.Int).Value = nguongTon;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        // Lô còn hàng mà hạn dùng còn dưới N ngày (lô đã quá hạn vẫn hiện để xử lý)
        public DataTable LayLoSapHetHan(int soNgay)
        {
            const string sql = @"
                SELECT m.Id AS MaThuoc, m.Name AS TenThuoc, b.BatchNumber AS SoLo, b.ExpiryDate AS HanDung,
                       DATEDIFF(DAY, CAST(GETDATE() AS DATE), b.ExpiryDate) AS SoNgayConLai,
                       b.Stock AS TonLo,
                       CASE WHEN b.ExpiryDate < CAST(GETDATE() AS DATE)
                            THEN N'Đã hết hạn' ELSE N'Sắp hết hạn' END AS TrangThai
                FROM Batches b
                JOIN Medicines m ON m.Id = b.MedicineId
                WHERE b.Stock > 0 AND m.IsActive = 1
                  AND DATEDIFF(DAY, CAST(GETDATE() AS DATE), b.ExpiryDate) < @d
                ORDER BY b.ExpiryDate, m.Name";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@d", SqlDbType.Int).Value = soNgay;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
