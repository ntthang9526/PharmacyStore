using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.DAL
{
    public class DBConnection
    {
        public static string connectionString =
             "Data Source=LINHEHE;" +
             "Initial Catalog=PharmacyStoreDB;" +
             "Integrated Security=True;" +
             "TrustServerCertificate=True";


        SqlConnection sqlConnect = null;

        // Hàm mở kết nối
        private void KetNoiCSDL()
        {
            sqlConnect = new SqlConnection(connectionString);

            if (sqlConnect.State != ConnectionState.Open)
                sqlConnect.Open();
        }

        // Hàm đóng kết nối
        private void DongKetNoiCSDL()
        {
            if (sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        // Hàm thực thi câu lệnh SELECT
        // trả về một DataTable
        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();

            KetNoiCSDL();

            SqlDataAdapter sqldataAdapte =
                new SqlDataAdapter(sql, sqlConnect);

            sqldataAdapte.Fill(dtBang);

            DongKetNoiCSDL();

            return dtBang;
        }

        // Hàm thực hiện INSERT, UPDATE, DELETE
        public void CapNhatDuLieu(string sql)
        {
            KetNoiCSDL();

            SqlCommand sqlcommand = new SqlCommand();

            sqlcommand.Connection = sqlConnect;
            sqlcommand.CommandText = sql;

            sqlcommand.ExecuteNonQuery();

            DongKetNoiCSDL();

            sqlcommand.Dispose();
        }

    }
}
