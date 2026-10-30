-- Cho phép sửa/hủy đơn chờ; giữ nguyên chứng từ và không thay đổi tồn/voucher.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Orders','OrderVersion') IS NULL
 ALTER TABLE dbo.Orders ADD OrderVersion rowversion;
IF COL_LENGTH('dbo.Orders','ModifiedAt') IS NULL
 ALTER TABLE dbo.Orders ADD ModifiedAt datetime2 NULL;
IF COL_LENGTH('dbo.Orders','ModifiedEmployeeId') IS NULL
 ALTER TABLE dbo.Orders ADD ModifiedEmployeeId int NULL REFERENCES dbo.Employees(Id);
IF COL_LENGTH('dbo.Orders','CancelledAt') IS NULL
 ALTER TABLE dbo.Orders ADD CancelledAt datetime2 NULL;
IF COL_LENGTH('dbo.Orders','CancelledEmployeeId') IS NULL
 ALTER TABLE dbo.Orders ADD CancelledEmployeeId int NULL REFERENCES dbo.Employees(Id);
IF COL_LENGTH('dbo.Orders','CancelReason') IS NULL
 ALTER TABLE dbo.Orders ADD CancelReason nvarchar(250) NULL;
IF EXISTS(SELECT 1 FROM sys.check_constraints WHERE name='CK_Orders_PaymentStatus' AND parent_object_id=OBJECT_ID('dbo.Orders'))
 ALTER TABLE dbo.Orders DROP CONSTRAINT CK_Orders_PaymentStatus;
ALTER TABLE dbo.Orders WITH CHECK ADD CONSTRAINT CK_Orders_PaymentStatus CHECK(PaymentStatus IN ('PENDING','PAID','CANCELLED'));
COMMIT;

