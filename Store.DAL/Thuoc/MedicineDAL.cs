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
    public class MedicineDAL
    {
        public Dictionary<int,Medicine> GetMedicines()
        {
            Dictionary<int, Medicine> list = new();

            string sql = "select * from Medicines where IsActive = 1 AND IsSelling = 1";
            using var connection = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Medicine medicine = new Medicine
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                    SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                    BarCode = reader.IsDBNull(reader.GetOrdinal("BarCode")) ? "" : reader.GetString(reader.GetOrdinal("BarCode")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    ActiveIngredient = reader.IsDBNull(reader.GetOrdinal("ActiveIngredient")) ? "" : reader.GetString(reader.GetOrdinal("ActiveIngredient")),
                    Unit = reader.GetString(reader.GetOrdinal("Unit")),
                    Price = Store.Shared.TienTe.LamTron(reader.GetDecimal(reader.GetOrdinal("Price"))),
                    IsPrescription = reader.GetBoolean(reader.GetOrdinal("IsPrescription")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    
                };
                list.Add(medicine.Id, medicine);
            }
            return list;
        }


        public int SoLuongConHan(int medId)
        {
            string sql = """
                select b.MedicineId, SUM(b.Stock) as TotalStock
                from Batches b JOIN Medicines m ON m.Id=b.MedicineId
                where m.IsActive=1 AND m.IsSelling=1 AND b.IsBlocked=0 AND CAST(b.ExpiryDate AS DATE) >= CAST(GETDATE() AS DATE) and b.MedicineId = @MedicineId
                group by b.MedicineId
                """;

            using var connection = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@MedicineId", SqlDbType.Int).Value = medId;

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return 0;
            }
            return reader.GetInt32(reader.GetOrdinal("TotalStock"));
        }
    }
}
