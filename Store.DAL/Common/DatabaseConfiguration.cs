using Microsoft.Data.SqlClient;

namespace Store.DAL;

internal static class DatabaseConfiguration
{
    internal static string Load(string baseDirectory)
    {
        string? path = null;
        for (var directory = new DirectoryInfo(baseDirectory); directory != null; directory = directory.Parent)
        {
            var candidate = Path.Combine(directory.FullName, ".env");
            if (File.Exists(candidate)) { path = candidate; break; }
            if (File.Exists(Path.Combine(directory.FullName, "PharmacyStore.slnx"))) break;
        }
        if (path == null)
            return @"Server=.\SQLEXPRESS;Database=PharmacyStoreDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=5;";

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        int lineNumber = 0;
        foreach (string line in File.ReadLines(path))
        {
            lineNumber++;
            string text = line.Trim();
            if (text.Length == 0 || text.StartsWith('#')) continue;
            int separator = text.IndexOf('=');
            if (separator <= 0) throw new InvalidOperationException($"File .env sai định dạng ở dòng {lineNumber}.");
            string key = text[..separator].Trim(), value = text[(separator + 1)..].Trim();
            if (value.StartsWith('\'') || value.StartsWith('"'))
            {
                if (value.Length < 2 || value[^1] != value[0])
                    throw new InvalidOperationException($"File .env thiếu dấu nháy ở dòng {lineNumber}.");
                value = value[1..^1];
            }
            values[key] = value;
        }
        string Required(string key) => values.TryGetValue(key, out string? value) && !string.IsNullOrWhiteSpace(value)
            ? value : throw new InvalidOperationException($"Bạn chưa điền {key} trong file .env.");
        bool Flag(string key, bool fallback) => !values.TryGetValue(key, out string? value) ? fallback
            : bool.TryParse(value, out bool result) ? result
            : throw new InvalidOperationException($"{key} trong file .env phải là true hoặc false.");
        int timeout = 30;
        if (values.TryGetValue("DB_CONNECT_TIMEOUT", out string? configuredTimeout) &&
            (!int.TryParse(configuredTimeout, out timeout) || timeout < 1 || timeout > 300))
            throw new InvalidOperationException("DB_CONNECT_TIMEOUT trong file .env phải từ 1 đến 300 giây.");

        var settings = new SqlConnectionStringBuilder
        {
            DataSource = Required("DB_SERVER"),
            InitialCatalog = Required("DB_NAME"),
            UserID = Required("DB_USER"),
            Password = Required("DB_PASSWORD"),
            IntegratedSecurity = false,
            Encrypt = Flag("DB_ENCRYPT", true),
            TrustServerCertificate = Flag("DB_TRUST_SERVER_CERTIFICATE", true),
            ConnectTimeout = timeout
        };
        return settings.ConnectionString;
    }
}
