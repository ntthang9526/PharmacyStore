using Store.Shared.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Store.DAL
{
    public class EmployeeDAL
    {
        public Employee? GetEmployeeByUsername(string username)
        {
            string sql = """
                select Id, FullName, Username, PasswordHash, Phone, IsActive, Role
                from Employees
                where Username = @Username
                """;
            using var conn = DBConnection.GetDbConnection();
            using var command = new SqlCommand(sql, conn);

            command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }
            return new Employee
            {
                ID = reader.GetInt32(reader.GetOrdinal("Id")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                Role = reader.GetString(reader.GetOrdinal("Role")),
            };
        }
    }
}
