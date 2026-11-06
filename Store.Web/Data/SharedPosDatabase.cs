using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacyWeb.Data.Entities;

namespace PharmacyWeb.Data;

public static class SharedPosDatabase
{
    // Chỉ bổ sung metadata web; không đổi khóa, ý nghĩa tiền hoặc bảng hóa đơn của POS.
    private static readonly (string Table, string Column, string Definition)[] Columns =
    [
        ("Categories", "Slug", "AS (N'nhom-' + CONVERT(nvarchar(20), [Id])) PERSISTED"),
        ("Categories", "Description", "nvarchar(255) NULL"),
        ("Categories", "IsActive", "bit NOT NULL CONSTRAINT DF_WebCategoryActive DEFAULT(1) WITH VALUES"),
        ("Categories", "DisplayOrder", "int NOT NULL CONSTRAINT DF_WebCategoryOrder DEFAULT(0) WITH VALUES"),
        ("Suppliers", "IsActive", "bit NOT NULL CONSTRAINT DF_WebSupplierActive DEFAULT(1) WITH VALUES"),
        ("Medicines", "Slug", "AS (N'thuoc-' + CONVERT(nvarchar(20), [Id])) PERSISTED"),
        ("Medicines", "PackagingSpec", "nvarchar(150) NOT NULL CONSTRAINT DF_WebPackaging DEFAULT(N'') WITH VALUES"),
        ("Medicines", "Featured", "bit NOT NULL CONSTRAINT DF_WebFeatured DEFAULT(0) WITH VALUES"),
        ("Medicines", "DescriptionHtml", "nvarchar(max) NULL"),
        ("Medicines", "MainImageUrl", "nvarchar(max) NULL"),
        ("Medicines", "CreatedAt", "datetime2 NOT NULL CONSTRAINT DF_WebMedicineCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES"),
        ("Medicines", "RowVersion", "rowversion NOT NULL"),
        ("Batches", "ReservedQuantity", "int NOT NULL CONSTRAINT DF_WebReserved DEFAULT(0) WITH VALUES"),
        ("Batches", "RowVersion", "rowversion NOT NULL"),
        ("Batches", "CreatedAt", "datetime2 NOT NULL CONSTRAINT DF_WebBatchCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES"),
        ("Customers", "Email", "nvarchar(100) NOT NULL CONSTRAINT DF_WebCustomerEmail DEFAULT(N'') WITH VALUES"),
        ("Customers", "WebAccountActive", "bit NOT NULL CONSTRAINT DF_WebCustomerAccount DEFAULT(0) WITH VALUES"),
        ("Customers", "IsLocked", "bit NOT NULL CONSTRAINT DF_WebCustomerLocked DEFAULT(0) WITH VALUES"),
        ("Customers", "CreatedAt", "datetime2 NOT NULL CONSTRAINT DF_WebCustomerCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES"),
        ("Customers", "UserId", "nvarchar(450) NULL")
    ];

    public static string GenerateIntegrationScript(PharmacyWebDbContext context)
    {
        if (!context.Database.IsSqlServer()) throw new InvalidOperationException("Chỉ tạo script cho SQL Server.");
        var sql = new StringBuilder("""
            -- Chạy sau khi đã sao lưu PharmacyStoreDB. Script có thể chạy lại.
            SET XACT_ABORT ON;
            SET ANSI_NULLS ON;
            SET QUOTED_IDENTIFIER ON;
            SET ANSI_PADDING ON;
            SET ANSI_WARNINGS ON;
            SET CONCAT_NULL_YIELDS_NULL ON;
            SET ARITHABORT ON;
            SET NUMERIC_ROUNDABORT OFF;
            IF DB_NAME() <> N'PharmacyStoreDB' AND DB_NAME() NOT LIKE N'PharmacyWebIntegration[_]%'
                THROW 50001,N'Sai database đích. Chỉ dùng database POS hoặc database kiểm tra đã đặt tên.',1;
            IF OBJECT_ID(N'dbo.Medicines',N'U') IS NULL OR OBJECT_ID(N'dbo.Customers',N'U') IS NULL
                THROW 50002,N'Database chưa có cấu trúc POS. Không tạo dữ liệu mẫu thay thế.',1;
            BEGIN TRANSACTION;
            GO

            """);
        foreach (var (table, column, definition) in Columns)
        {
            string alter = $"ALTER TABLE dbo.[{table}] ADD [{column}] {definition};";
            sql.AppendLine($"IF COL_LENGTH(N'dbo.{table}',N'{column}') IS NULL EXEC(N'{alter.Replace("'", "''")}');\nGO");
        }
        // EF sắp bảng theo khóa ngoại; bỏ toàn bộ CREATE của các bảng POS đang có.
        foreach (string batch in Regex.Split(context.Database.GenerateCreateScript(), @"^\s*GO\s*$", RegexOptions.Multiline))
        {
            var table = Regex.Match(batch, @"^\s*CREATE TABLE (?:\[dbo\]\.)?\[(Web[^\]]+)\]", RegexOptions.IgnoreCase);
            var index = Regex.Match(batch, @"^\s*CREATE (?:UNIQUE )?INDEX \[([^\]]+)\] ON (?:\[dbo\]\.)?\[(Web[^\]]+)\]", RegexOptions.IgnoreCase);
            if (table.Success)
                sql.AppendLine($"IF OBJECT_ID(N'dbo.{table.Groups[1].Value}',N'U') IS NULL BEGIN\n{batch.Trim()}\nEND;\nGO");
            else if (index.Success)
                sql.AppendLine($"IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'{index.Groups[1].Value}' AND object_id=OBJECT_ID(N'dbo.{index.Groups[2].Value}')) BEGIN\n{batch.Trim()}\nEND;\nGO");
        }
        sql.AppendLine("""
            IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE name=N'CK_WebBatchReservation')
                EXEC(N'ALTER TABLE dbo.Batches WITH CHECK ADD CONSTRAINT CK_WebBatchReservation CHECK (ReservedQuantity>=0 AND Stock>=ReservedQuantity)');
            IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name=N'UX_WebCustomerUser' AND object_id=OBJECT_ID(N'dbo.Customers'))
                EXEC(N'CREATE UNIQUE INDEX UX_WebCustomerUser ON dbo.Customers(UserId) WHERE UserId IS NOT NULL');
            IF NOT EXISTS(SELECT 1 FROM sys.foreign_key_columns f
                JOIN sys.columns c ON c.object_id=f.parent_object_id AND c.column_id=f.parent_column_id
                WHERE f.parent_object_id=OBJECT_ID(N'dbo.WebUsers') AND f.referenced_object_id=OBJECT_ID(N'dbo.Customers')
                    AND c.name=N'CustomerProfileId')
                ALTER TABLE dbo.WebUsers WITH CHECK ADD CONSTRAINT FK_WebUsers_PosCustomer
                    FOREIGN KEY(CustomerProfileId) REFERENCES dbo.Customers(Id);
            COMMIT TRANSACTION;
            GO
            """);
        return sql.ToString();
    }

    public static async Task InitializeAsync(PharmacyWebDbContext context, UserManager<ApplicationUser> users,
        RoleManager<ApplicationRole> roles, string contentRoot)
    {
        await context.Database.OpenConnectionAsync();
        try
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT CASE WHEN COL_LENGTH(N'dbo.Batches',N'ReservedQuantity') IS NOT NULL AND OBJECT_ID(N'dbo.WebUsers',N'U') IS NOT NULL THEN 1 ELSE 0 END";
            if (Convert.ToInt32(await command.ExecuteScalarAsync()) != 1)
                throw new InvalidOperationException("Cần áp dụng Database/IntegratePosDatabase.sql lên database POS trước khi chạy web.");
        }
        finally { await context.Database.CloseConnectionAsync(); }

        foreach (string name in new[] { "Admin", "Customer" })
            if (!await roles.RoleExistsAsync(name))
            {
                var result = await roles.CreateAsync(new ApplicationRole(name));
                if (!result.Succeeded) throw new InvalidOperationException("Chưa tạo được vai trò web.");
            }
        if ((await users.GetUsersInRoleAsync("Admin")).Count > 0) return;

        var file = Path.Combine(contentRoot, ".env.web");
        var settings = File.Exists(file) ? File.ReadLines(file)
            .Where(l => !string.IsNullOrWhiteSpace(l) && !l.TrimStart().StartsWith('#') && l.Contains('='))
            .Select(l => l.Split('=', 2)).ToDictionary(p => p[0].Trim(), p => p[1].Trim()) : new Dictionary<string, string>();
        string? password = Environment.GetEnvironmentVariable("PHARMACY_WEB_ADMIN_PASSWORD")
            ?? settings.GetValueOrDefault("WEB_ADMIN_PASSWORD");
        string nameAdmin = Environment.GetEnvironmentVariable("PHARMACY_WEB_ADMIN_USERNAME")
            ?? settings.GetValueOrDefault("WEB_ADMIN_USERNAME") ?? "webadmin";
        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("Chưa có tài khoản Admin web. Cấu hình WEB_ADMIN_USERNAME và WEB_ADMIN_PASSWORD trong Store.Web/.env.web.");
        var admin = new ApplicationUser { UserName = nameAdmin, FullName = "Quản trị website Pharmacy", EmailConfirmed = true };
        var created = await users.CreateAsync(admin, password);
        if (!created.Succeeded) throw new InvalidOperationException("Không tạo được Admin web. Kiểm tra cấu hình tài khoản khởi tạo.");
        var assigned = await users.AddToRoleAsync(admin, "Admin");
        if (!assigned.Succeeded) throw new InvalidOperationException("Không cấp được vai trò Admin web.");
    }
}
