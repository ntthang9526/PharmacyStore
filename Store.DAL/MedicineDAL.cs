using Microsoft.Data.SqlClient;
using Store.Shared.Models;
using System;
using System.Collections.Generic;
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

            string sql = "select * from Medicines";
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
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    IsPrescription = reader.GetBoolean(reader.GetOrdinal("IsPrescription")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    
                };
                list.Add(medicine.Id, medicine);
            }
            return list;
        }
    }
}
