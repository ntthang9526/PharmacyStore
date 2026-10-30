-- Lưu thông tin hiển thị ở đầu hóa đơn mới, không suy đoán lịch sử cũ.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Orders','CustomerNameSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD CustomerNameSnapshot nvarchar(100) NULL;
IF COL_LENGTH('dbo.Orders','CustomerPhoneSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD CustomerPhoneSnapshot varchar(15) NULL;
IF COL_LENGTH('dbo.Orders','EmployeeNameSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD EmployeeNameSnapshot nvarchar(100) NULL;
IF COL_LENGTH('dbo.Orders','PaidEmployeeNameSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD PaidEmployeeNameSnapshot nvarchar(100) NULL;
IF COL_LENGTH('dbo.Orders','VoucherCodeSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD VoucherCodeSnapshot nvarchar(50) NULL;
IF COL_LENGTH('dbo.Orders','ModifiedEmployeeNameSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD ModifiedEmployeeNameSnapshot nvarchar(100) NULL;
IF COL_LENGTH('dbo.Orders','CancelledEmployeeNameSnapshot') IS NULL
 ALTER TABLE dbo.Orders ADD CancelledEmployeeNameSnapshot nvarchar(100) NULL;
COMMIT;
