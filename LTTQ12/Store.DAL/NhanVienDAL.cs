using System.Data;
using Microsoft.Data.SqlClient;

namespace Store.DAL
{
    public class NhanVienDAL
    {
        // Kiểm tra tài khoản + mật khẩu (phân biệt hoa/thường ở mật khẩu), chỉ cho nhân viên đang làm việc.
        public DataTable DangNhap(string username, string password)
        {
            const string sql = @"
                SELECT Id, FullName
                FROM Employees
                WHERE Username = @u
                  AND PasswordHash COLLATE Latin1_General_CS_AS = @p
                  AND IsActive = 1";

            using var conn = new SqlConnection(DBConnection.connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@u", SqlDbType.VarChar, 50).Value = username;
            cmd.Parameters.Add("@p", SqlDbType.VarChar, 255).Value = password;

            DataTable dt = new DataTable();
            using var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
    }
}
