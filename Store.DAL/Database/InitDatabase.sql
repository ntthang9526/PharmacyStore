:ON ERROR EXIT
-- ==============================================================
-- CƠ SỞ DỮ LIỆU: PharmacyStoreDB (Admin / Nhân viên)
-- Dành cho nhóm: WinForms POS + ASP.NET Core Web API + SQL Server
-- ==============================================================

USE master;
GO

IF DB_ID(N'PharmacyStoreDB') IS NOT NULL
    THROW 50001, N'Database thử đã tồn tại. Không ghi đè dữ liệu.', 1;
GO

-- Khởi tạo database mới
CREATE DATABASE PharmacyStoreDB;
GO

USE PharmacyStoreDB;
GO

-- Cấp quyền owner để mở Database Diagram không bị lỗi
-- Giữ chủ sở hữu là tài khoản tạo database.
GO

-- ==============================================================
-- 1. TÀI KHOẢN & NHÂN SỰ (ADMIN / STAFF)
-- ==============================================================
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,     -- Tài khoản đăng nhập
    PasswordHash VARCHAR(255) NOT NULL,       -- Mật khẩu
    Phone VARCHAR(15) NULL,
    IsActive BIT NOT NULL DEFAULT 1,          -- 1: Đang làm việc, 0: Đã khóa
    Role VARCHAR(10) NOT NULL DEFAULT 'STAFF' CHECK(Role IN ('ADMIN','STAFF'))
);

-- ==============================================================
-- 2. NHÀ CUNG CẤP & DANH MỤC THUỐC
-- ==============================================================
CREATE TABLE Suppliers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Address NVARCHAR(250) NULL
);

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- ==============================================================
-- 3. THUỐC & LÔ HÀNG (QUẢN LÝ THEO DATE)
-- ==============================================================
CREATE TABLE Medicines (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL CONSTRAINT FK_Medicines_Categories REFERENCES Categories(Id),
    SupplierId INT NOT NULL CONSTRAINT FK_Medicines_Suppliers REFERENCES Suppliers(Id),
    BarCode VARCHAR(50) NULL,
    Name NVARCHAR(150) NOT NULL,
    ActiveIngredient NVARCHAR(150) NULL,
    Unit NVARCHAR(30) NOT NULL,           -- Hộp, Vỉ, Tuýp, Chai
    Price DECIMAL(18,2) NOT NULL,
    IsPrescription BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Batches (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MedicineId INT NOT NULL CONSTRAINT FK_Batches_Medicines REFERENCES Medicines(Id),
    BatchNumber VARCHAR(50) NOT NULL,     -- Số lô in trên bao bì
    ExpiryDate DATE NOT NULL,             -- Ngày hết hạn
    Stock INT NOT NULL DEFAULT 0          -- Tồn kho thực tế của lô
);

-- ==============================================================
-- 4. KHÁCH HÀNG & MÃ GIẢM GIÁ (VOUCHERS)
-- ==============================================================
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    Points INT NOT NULL DEFAULT 0
);

CREATE TABLE Vouchers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,      -- Mã nhập (vd: 'GIAM10K', 'TRIAN10')
    DiscountType VARCHAR(10) NOT NULL,     -- 'FIXED' (tiền cố định) hoặc 'PERCENT' (%)
    DiscountValue DECIMAL(18,2) NOT NULL,  -- Giá trị (vd: 10000 hoặc 10)
    Quantity INT NOT NULL DEFAULT 0,       -- Số lượt còn lại
    IsActive BIT NOT NULL DEFAULT 1        -- 1: Hoạt động, 0: Tạm khóa
);

-- ==============================================================
-- 5. HÓA ĐƠN & CHI TIẾT ĐƠN HÀNG
-- ==============================================================
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL CONSTRAINT FK_Orders_Employees REFERENCES Employees(Id),
    CustomerId INT NULL CONSTRAINT FK_Orders_Customers REFERENCES Customers(Id),
    VoucherId INT NULL CONSTRAINT FK_Orders_Vouchers REFERENCES Vouchers(Id),
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL,              -- Tổng tiền hàng ban đầu
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0, -- Số tiền được giảm từ voucher
    Channel NVARCHAR(20) NOT NULL DEFAULT 'POS'      -- 'POS' (Giai đoạn 1) hoặc 'WEB' (Giai đoạn 2)
);

CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL CONSTRAINT FK_OrderDetails_Orders REFERENCES Orders(Id) ON DELETE CASCADE,
    BatchId INT NOT NULL CONSTRAINT FK_OrderDetails_Batches REFERENCES Batches(Id),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    DosageInstruction NVARCHAR(255) NULL
);
GO

-- ==============================================================
-- NẠP DỮ LIỆU MẪU KIỂM THỬ
-- ==============================================================

-- 1. Tài khoản nhân viên (Đã bỏ cột Role)
INSERT INTO Employees (FullName, Username, PasswordHash, Phone, IsActive) VALUES
(N'Quản lý hệ thống', 'admin', '123456', '0901111222', 1),
(N'Dược sĩ Mai', 'mai_duocsi', '123456', '0903333444', 1);
UPDATE Employees SET Role='ADMIN' WHERE Username='admin';

-- 2. Nhà cung cấp & Danh mục
INSERT INTO Suppliers (Name, Phone, Address) VALUES 
(N'Dược Hậu Giang (DHG)', '02923891433', N'Cần Thơ'),
(N'Sanofi Việt Nam', '02838298526', N'TP. Hồ Chí Minh'),
(N'Dược phẩm Boston Việt Nam', '02743769606', N'Bình Dương');

INSERT INTO Categories (Name) VALUES 
(N'Thuốc giảm đau - hạ sốt'),
(N'Kháng sinh - Kháng viêm'),
(N'Vitamin & Khoáng chất');

-- 3. Thuốc
INSERT INTO Medicines (CategoryId, SupplierId, BarCode, Name, ActiveIngredient, Unit, Price, IsPrescription) VALUES
(1, 1, '8935001800112', N'Panadol Extra', N'Paracetamol 500mg, Caffeine 65mg', N'Hộp', 45000, 0),
(1, 1, '8935001800113', N'Hapacol 250', N'Paracetamol 250mg', N'Gói', 5000, 0),
(2, 2, '8935001800229', N'Augmentin 1g', N'Amoxicillin, Clavulanic acid', N'Hộp', 240000, 1),
(3, 3, '8935001800336', N'Boston C 1000mg', N'Vitamin C', N'Tuýp', 35000, 0);

-- 4. Lô hàng
INSERT INTO Batches (MedicineId, BatchNumber, ExpiryDate, Stock) VALUES
(1, 'LOT-PND-2024A', '2026-12-31', 40),
(1, 'LOT-PND-2025B', '2027-10-15', 60),
(2, 'LOT-HPC-2025A', '2027-08-20', 100),
(3, 'LOT-AUG-2024X', '2027-05-20', 30),
(4, 'LOT-BTC-2025M', '2028-01-10', 80);

-- 5. Khách hàng
INSERT INTO Customers (FullName, Phone, Points) VALUES
(N'Trần Văn Tuấn', '0912345678', 15),
(N'Lê Thị Mai', '0987654321', 5);

-- 6. Voucher mẫu
INSERT INTO Vouchers (Code, DiscountType, DiscountValue, Quantity, IsActive) VALUES
('GIAM10K', 'FIXED', 10000, 50, 1),
('GIAM10', 'PERCENT', 10, 20, 1);
GO

-- ==============================================================
-- BỔ SUNG: PHIẾU NHẬP KHO (chạy trên database PharmacyStoreDB đã có sẵn)
-- Script chạy lại nhiều lần không bị lỗi.
-- ==============================================================

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

GO
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
IF OBJECT_ID('dbo.SystemConfig','U') IS NULL
BEGIN
 CREATE TABLE dbo.SystemConfig(
  [Key] nvarchar(50) PRIMARY KEY,
  [Value] nvarchar(255) NOT NULL,
  UpdatedAt datetime2 NOT NULL DEFAULT GETDATE()
 );
 INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('DataVersion', '1', GETDATE());
 INSERT INTO dbo.SystemConfig ([Key], [Value], UpdatedAt) VALUES ('LastModified', CONVERT(nvarchar(30), GETDATE(), 126), GETDATE());
END;
COMMIT;

GO
-- Chạy trên database đang sử dụng; không tạo database mới hoặc xóa dữ liệu.
-- IsActive vẫn là cờ xóa mềm. IsSelling chỉ kiểm soát việc bán.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Medicines','IsSelling') IS NULL
 ALTER TABLE dbo.Medicines ADD IsSelling bit NOT NULL CONSTRAINT DF_Medicines_IsSelling DEFAULT 1 WITH VALUES;
IF COL_LENGTH('dbo.Batches','IsBlocked') IS NULL
 ALTER TABLE dbo.Batches ADD IsBlocked bit NOT NULL CONSTRAINT DF_Batches_IsBlocked DEFAULT 0 WITH VALUES;
COMMIT;

GO
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

GO
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
