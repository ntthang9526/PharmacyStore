using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.DAL;

public class DBConnection
{
    public static string connectionString =>
        Environment.GetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING")
        ?? @"Server=.\SQLEXPRESS;Database=PharmacyStoreTrialDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;";

    public static SqlConnection GetDbConnection()
    {
        var connection = new SqlConnection(connectionString);
        try { connection.Open(); return connection; }
        catch { connection.Dispose(); throw; }
    }

    public DataTable DocBang(string sql, params SqlParameter[] parameters)
    {
        using var connection = GetDbConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        using var adapter = new SqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public bool CapNhatDuLieu(string sql, params SqlParameter[] parameters)
    {
        using var connection = GetDbConnection();
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddRange(parameters);
        return command.ExecuteNonQuery() > 0;
    }

    public static string LikePattern(string? keyword) => "%" + (keyword ?? "").Trim()
        .Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") + "%";

}
