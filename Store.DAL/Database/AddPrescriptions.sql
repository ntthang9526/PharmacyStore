-- Bổ sung thông tin đơn thuốc; không suy đoán dữ liệu lịch sử.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Orders','PrescriptionNumber') IS NULL
 ALTER TABLE dbo.Orders ADD PrescriptionNumber nvarchar(100) NULL, PatientName nvarchar(150) NULL,
 Prescriber nvarchar(150) NULL, PrescriptionDate date NULL, PrescriptionCheckedBy int NULL REFERENCES dbo.Employees(Id), PrescriptionCheckedAt datetime2 NULL;
IF COL_LENGTH('dbo.OrderPendingItems','DosageInstruction') IS NULL
 ALTER TABLE dbo.OrderPendingItems ADD DosageInstruction nvarchar(500) NULL;
IF COL_LENGTH('dbo.OrderPendingItems','IsPrescriptionSnapshot') IS NULL
 ALTER TABLE dbo.OrderPendingItems ADD IsPrescriptionSnapshot bit NULL;
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID('dbo.OrderDetails') AND name='DosageInstruction' AND max_length<1000)
 ALTER TABLE dbo.OrderDetails ALTER COLUMN DosageInstruction nvarchar(500) NULL;
IF COL_LENGTH('dbo.OrderDetails','IsPrescriptionSnapshot') IS NULL
 ALTER TABLE dbo.OrderDetails ADD IsPrescriptionSnapshot bit NULL;
COMMIT;
