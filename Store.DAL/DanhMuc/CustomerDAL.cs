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
    public class CustomerDAL
    {
        public Customer? GetCustomerByPhone(string phone)
        {
            string sql = """
                select * from Customers
                where Phone = @Phone
                """;
            using var conn = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, conn);
            command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phone;

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            return new Customer
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Point = reader.GetInt32(reader.GetOrdinal("Points"))
            };
        }
        public Customer? GetCustomerById(int id)
        {
            string sql = """
                select * from Customers
                where Id = @Id
                """;
            using var conn = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, conn);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            return new Customer
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                Point = reader.GetInt32(reader.GetOrdinal("Points"))
            };
        }
        //public bool AddCustomer(string name, string phone)
        //{
        //    string sql = """
        //        insert
        //        """
        //}
    }
}
