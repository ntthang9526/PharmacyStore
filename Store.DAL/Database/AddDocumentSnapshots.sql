-- Nâng cấp database hiện tại, không xóa dữ liệu và không suy đoán lịch sử cũ.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.OrderDetails','MedicineNameSnapshot') IS NULL
 ALTER TABLE dbo.OrderDetails ADD MedicineNameSnapshot nvarchar(150) NULL;
IF COL_LENGTH('dbo.OrderDetails','MedicineUnitSnapshot') IS NULL
 ALTER TABLE dbo.OrderDetails ADD MedicineUnitSnapshot nvarchar(30) NULL;
IF COL_LENGTH('dbo.OrderPendingItems','MedicineNameSnapshot') IS NULL
 ALTER TABLE dbo.OrderPendingItems ADD MedicineNameSnapshot nvarchar(150) NULL;
IF COL_LENGTH('dbo.OrderPendingItems','MedicineUnitSnapshot') IS NULL
 ALTER TABLE dbo.OrderPendingItems ADD MedicineUnitSnapshot nvarchar(30) NULL;
IF COL_LENGTH('dbo.StockInDetails','MedicineNameSnapshot') IS NULL
 ALTER TABLE dbo.StockInDetails ADD MedicineNameSnapshot nvarchar(150) NULL;
IF COL_LENGTH('dbo.StockInDetails','MedicineUnitSnapshot') IS NULL
 ALTER TABLE dbo.StockInDetails ADD MedicineUnitSnapshot nvarchar(30) NULL;
COMMIT;
