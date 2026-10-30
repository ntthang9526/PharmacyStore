:ON ERROR EXIT
-- ==============================================================
-- BỔ SUNG: PHIẾU NHẬP KHO (chạy trên database PharmacyStoreDB đã có sẵn)
-- Script chạy lại nhiều lần không bị lỗi.
-- ==============================================================
USE PharmacyStoreDB;
GO

IF OBJECT_ID('dbo.StockIns', 'U') IS NULL
CREATE TABLE StockIns (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId INT NOT NULL CONSTRAINT FK_StockIns_Suppliers REFERENCES Suppliers(Id),
    EmployeeId INT NOT NULL CONSTRAINT FK_StockIns_Employees REFERENCES Employees(Id),
    ImportDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalCost DECIMAL(18,2) NOT NULL,        -- Tổng tiền vốn của phiếu nhập
    Note NVARCHAR(250) NULL
);
GO

IF OBJECT_ID('dbo.StockInDetails', 'U') IS NULL
CREATE TABLE StockInDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StockInId INT NOT NULL CONSTRAINT FK_StockInDetails_StockIns REFERENCES StockIns(Id) ON DELETE CASCADE,
    BatchId INT NOT NULL CONSTRAINT FK_StockInDetails_Batches REFERENCES Batches(Id),
    Quantity INT NOT NULL,
    UnitCost DECIMAL(18,2) NOT NULL          -- Giá vốn / 1 đơn vị
);
GO
