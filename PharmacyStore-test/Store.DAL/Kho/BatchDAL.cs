using Microsoft.Data.SqlClient;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL
{
    public class BatchDAL
    {
        public int TimToanBoSoLuongConHan(int medicineId)
        {
            string sql = """
                select COALESCE(SUM(Stock), 0) as TotalStock
                from Batches
                where MedicineId = @MedicineId and CAST(ExpiryDate AS date) >= CAST(GETDATE() AS date)
                """;
            using var connection = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@MedicineId", SqlDbType.Int).Value = medicineId;
            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return 0;
            }
            return reader.GetInt32(reader.GetOrdinal("TotalStock"));
        }
        //public Medicine TimThuocTheoId(int ma)
        //{
        //    string sql = "select * from Medicines med join Batches ba on med.Id = ba.MedicineId";
        //    using var connection = DBConnection.GetDbConnection();
        //    using var command = new SqlCommand(sql, connection);

        //    using var reader = command.ExecuteReader();
        //}
    }
}
