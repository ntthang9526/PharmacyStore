using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.DAL
{
    public class DBConnection
    {
        private static readonly string Server = ".\\SQLEXPRESS";// Thay Instance cua mn vao day nhe
        private static readonly string Database = "PharmacyStoreDB";

        private static readonly string ConnectionString =
            $"Server={Server};Database={Database};Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetDbConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

    }
}
