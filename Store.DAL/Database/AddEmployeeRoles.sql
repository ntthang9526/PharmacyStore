-- Chạy trên database hiện tại; không xóa hoặc khởi tạo lại dữ liệu.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Employees','Role') IS NULL
BEGIN
 ALTER TABLE dbo.Employees ADD Role varchar(10) NOT NULL CONSTRAINT DF_Employees_Role DEFAULT 'STAFF' WITH VALUES;
 EXEC(N'UPDATE dbo.Employees SET Role=''ADMIN'' WHERE Username=''admin'' AND IsActive=1');
END;
IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE name='CK_Employees_Role')
 EXEC(N'ALTER TABLE dbo.Employees ADD CONSTRAINT CK_Employees_Role CHECK(Role IN (''ADMIN'',''STAFF''))');
EXEC(N'IF NOT EXISTS(SELECT 1 FROM dbo.Employees WHERE Role=''ADMIN'' AND IsActive=1)
 THROW 50020, N''Cần một tài khoản Admin đang hoạt động trước khi áp dụng phân quyền.'', 1;');
COMMIT;
