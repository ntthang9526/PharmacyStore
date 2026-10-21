using System.Data;
using Microsoft.Data.SqlClient;

namespace Store.DAL
{
    public class VoucherDAL
    {
        // Lấy voucher theo mã code (chưa kiểm tra hết lượt/khóa - việc đó do BLL xử lý)
        public DataTable LayVoucher(string code)
        {
            const string sql = @"
                SELECT Id, Code, DiscountType, DiscountValue, Quantity, IsActive
                FROM Vouchers
                WHERE Code = @code";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = code;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
