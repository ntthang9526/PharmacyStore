-- Chạy trên database hiện tại; không xóa hoặc khởi tạo lại dữ liệu.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF OBJECT_ID('dbo.SystemConfig','U') IS NULL
BEGIN
    CREATE TABLE dbo.SystemConfig (
        [Key] nvarchar(50) PRIMARY KEY,
        [Value] nvarchar(255) NOT NULL,
        UpdatedAt datetime2 NOT NULL DEFAULT GETDATE()
    );
    INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('DataVersion', '1', GETDATE());
    INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('LastModified', CONVERT(nvarchar(30), GETDATE(), 126), GETDATE());
END;
COMMIT;
