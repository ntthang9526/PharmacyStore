using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.DAL;

public class DBConnection
{
    private static long dataVersion;
    public static long DataVersion => Interlocked.Read(ref dataVersion);
    public static void NotifyDataChanged() => Interlocked.Increment(ref dataVersion);

    public static string connectionString =>
        Environment.GetEnvironmentVariable("PHARMACY_TRIAL_CONNECTION_STRING")
        ?? @"Server=LINHEHE;Database=PharmacyStoreTrialDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;";

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
        using var transaction = connection.BeginTransaction();
        PhanQuyenDAL.YeuCau(Quyen.QuanTri,connection,transaction);
        using var command = new SqlCommand(sql, connection,transaction);
        command.Parameters.AddRange(parameters);
        bool changed = command.ExecuteNonQuery() > 0;
        transaction.Commit();
        if (changed) NotifyDataChanged();
        return changed;
    }

    public static string LikePattern(string? keyword) => "%" + (keyword ?? "").Trim()
        .Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") + "%";

}
