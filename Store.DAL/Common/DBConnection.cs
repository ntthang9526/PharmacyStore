using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.DAL;

public class DBConnection
{
    private static long dataVersion;
    public static long DataVersion => Interlocked.Read(ref dataVersion);

    public static void NotifyDataChanged()
    {
        Interlocked.Increment(ref dataVersion);
        CapNhatPhienBanDatabase();
    }

    public static long LayPhienBanDatabase() => ThuLayPhienBanDatabase() ?? DataVersion;

    public static long? ThuLayPhienBanDatabase()
    {
        try
        {
            using var connection = GetDbConnection();
            using var command = new SqlCommand("SELECT [Value] FROM dbo.SystemConfig WHERE [Key] = 'DataVersion'", connection);
            var result = command.ExecuteScalar();
            if (result != null && long.TryParse(result.ToString(), out long v))
                return v;
        }
        catch
        {
            // Bỏ qua lỗi kết nối khi chạy offline hoặc môi trường chưa có bảng
        }
        return null;
    }

    private static void CapNhatPhienBanDatabase()
    {
        try
        {
            using var connection = GetDbConnection();
            using var command = new SqlCommand(@"
                IF OBJECT_ID('dbo.SystemConfig', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.SystemConfig (
                        [Key] nvarchar(50) PRIMARY KEY,
                        [Value] nvarchar(255) NOT NULL,
                        UpdatedAt datetime2 NOT NULL DEFAULT GETDATE()
                    );
                    INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('DataVersion', '1', GETDATE());
                    INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('LastModified', CONVERT(nvarchar(30), GETDATE(), 126), GETDATE());
                END
                ELSE
                BEGIN
                    UPDATE dbo.SystemConfig 
                    SET [Value] = CAST(ISNULL(TRY_CAST([Value] AS bigint), 0) + 1 AS nvarchar(50)), 
                        UpdatedAt = GETDATE() 
                    WHERE [Key] = 'DataVersion';

                    UPDATE dbo.SystemConfig 
                    SET [Value] = CONVERT(nvarchar(30), GETDATE(), 126), 
                        UpdatedAt = GETDATE() 
                    WHERE [Key] = 'LastModified';

                    IF @@ROWCOUNT = 0
                        INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('DataVersion', '1', GETDATE());
                END", connection);
            command.ExecuteNonQuery();
        }
        catch
        {
            // Không làm gián đoạn nghiệp vụ khi mất kết nối mạng tạm thời hoặc chạy offline
        }
    }

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
