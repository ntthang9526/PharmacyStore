-- Nâng cấp database hiện tại; không xóa dữ liệu, không tạo database mới.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Orders','PaymentMethod') IS NULL
 ALTER TABLE dbo.Orders ADD PaymentMethod nvarchar(50) NOT NULL CONSTRAINT DF_Orders_PaymentMethod DEFAULT N'Chưa xác định' WITH VALUES;
IF COL_LENGTH('dbo.Orders','PaymentStatus') IS NULL
 ALTER TABLE dbo.Orders ADD PaymentStatus varchar(10) NOT NULL CONSTRAINT DF_Orders_PaymentStatus DEFAULT 'PAID' WITH VALUES;
IF COL_LENGTH('dbo.Orders','PaidAt') IS NULL ALTER TABLE dbo.Orders ADD PaidAt datetime2 NULL;
IF COL_LENGTH('dbo.Orders','PaidEmployeeId') IS NULL
 ALTER TABLE dbo.Orders ADD PaidEmployeeId int NULL CONSTRAINT FK_Orders_PaidEmployee REFERENCES dbo.Employees(Id);
IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE name='CK_Orders_PaymentStatus')
 EXEC(N'ALTER TABLE dbo.Orders ADD CONSTRAINT CK_Orders_PaymentStatus CHECK(PaymentStatus IN (''PENDING'',''PAID''))');
IF OBJECT_ID('dbo.OrderPendingItems','U') IS NULL
 CREATE TABLE dbo.OrderPendingItems(
  Id int IDENTITY PRIMARY KEY,
  OrderId int NOT NULL CONSTRAINT FK_Pending_Order REFERENCES dbo.Orders(Id) ON DELETE CASCADE,
  MedicineId int NOT NULL CONSTRAINT FK_Pending_Medicine REFERENCES dbo.Medicines(Id),
  Quantity int NOT NULL CONSTRAINT CK_Pending_Quantity CHECK(Quantity>0),
  UnitPrice decimal(18,2) NOT NULL CONSTRAINT CK_Pending_Price CHECK(UnitPrice>=0),
  CONSTRAINT UQ_Pending_Order_Medicine UNIQUE(OrderId,MedicineId)
 );
COMMIT;
