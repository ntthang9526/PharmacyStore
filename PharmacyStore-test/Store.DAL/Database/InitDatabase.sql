-- ==============================================================
-- CƠ SỞ DỮ LIỆU ĐẦY ĐỦ: PharmacyStoreTrialDB
-- ĐÂY LÀ FILE DUY NHẤT CẦN CHẠY TRÊN SQL SERVER MANAGEMENT STUDIO (SSMS)
-- Bao gồm: Tạo CSDL, Tất cả Bảng, Ràng buộc 8 Quy tắc dữ liệu, Triggers, Indexes & Dữ liệu mẫu
-- ==============================================================

USE master;
GO

-- Xóa database cũ nếu đã tồn tại để tạo mới hoàn toàn sạch sẽ
IF DB_ID(N'PharmacyStoreTrialDB') IS NOT NULL
BEGIN
    ALTER DATABASE PharmacyStoreTrialDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PharmacyStoreTrialDB;
END
GO

-- Khởi tạo database mới
CREATE DATABASE PharmacyStoreTrialDB;
GO

USE PharmacyStoreTrialDB;
GO

-- ==============================================================
-- 1. TÀI KHOẢN & NHÂN SỰ
-- ==============================================================
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,     -- Tài khoản đăng nhập
    PasswordHash VARCHAR(255) NOT NULL,       -- Mật khẩu
    Phone VARCHAR(15) NULL,
    IsActive BIT NOT NULL DEFAULT 1           -- 1: Đang làm việc, 0: Đã khóa
);
GO

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
GO

-- ==============================================================
-- 3. THUỐC & LÔ HÀNG (QUẢN LÝ THEO HẠN DÙNG)
-- Quy tắc: Mã thuốc không trùng, Hạn dùng sau NSX, Giá decimal(18,2)
-- ==============================================================
CREATE TABLE Medicines (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL CONSTRAINT FK_Medicines_Categories REFERENCES Categories(Id),
    SupplierId INT NOT NULL CONSTRAINT FK_Medicines_Suppliers REFERENCES Suppliers(Id),
    BarCode VARCHAR(50) NULL,
    Name NVARCHAR(150) NOT NULL,
    ActiveIngredient NVARCHAR(150) NULL,
    Unit NVARCHAR(30) NOT NULL,                                                     -- Hộp, Vỉ, Tuýp, Chai
    Price DECIMAL(18,2) NOT NULL CONSTRAINT CK_Medicines_Price CHECK (Price >= 0),  -- Giá decimal không dùng float
    IsPrescription BIT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Batches (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MedicineId INT NOT NULL CONSTRAINT FK_Batches_Medicines REFERENCES Medicines(Id),
    BatchNumber VARCHAR(50) NOT NULL,                                              -- Số lô in trên bao bì
    ManufacturingDate DATE NULL,                                                   -- Ngày sản xuất
    ExpiryDate DATE NOT NULL,                                                      -- Ngày hết hạn
    Stock INT NOT NULL DEFAULT 0 CONSTRAINT CK_Batches_Stock CHECK (Stock >= 0),   -- Không bán vượt tồn (Stock >= 0)
    CONSTRAINT CK_Batches_ExpiryAfterMfg CHECK (ManufacturingDate IS NULL OR ExpiryDate > ManufacturingDate) -- Hạn sau NSX
);
GO

-- ==============================================================
-- 4. KHÁCH HÀNG & MÃ GIẢM GIÁ (VOUCHERS)
-- ==============================================================
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    Points INT NOT NULL DEFAULT 0 CONSTRAINT CK_Customers_Points CHECK (Points >= 0)
);

CREATE TABLE Vouchers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(20) UNIQUE NOT NULL,                                              -- Mã nhập (vd: 'GIAM10K')
    DiscountType VARCHAR(10) NOT NULL,                                             -- 'FIXED' hoặc 'PERCENT'
    DiscountValue DECIMAL(18,2) NOT NULL CONSTRAINT CK_Vouchers_DiscountValue CHECK (DiscountValue > 0),
    Quantity INT NOT NULL DEFAULT 0 CONSTRAINT CK_Vouchers_Quantity CHECK (Quantity >= 0),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- ==============================================================
-- 5. HÓA ĐƠN & CHI TIẾT BÁN HÀNG
-- Quy tắc: Có hình thức thanh toán, Số lượng bán > 0, Tiền decimal
-- ==============================================================
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL CONSTRAINT FK_Orders_Employees REFERENCES Employees(Id),
    CustomerId INT NULL CONSTRAINT FK_Orders_Customers REFERENCES Customers(Id),
    VoucherId INT NULL CONSTRAINT FK_Orders_Vouchers REFERENCES Vouchers(Id),
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL CONSTRAINT CK_Orders_TotalAmount CHECK (TotalAmount >= 0),
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0 CONSTRAINT CK_Orders_DiscountAmount CHECK (DiscountAmount >= 0),
    Channel NVARCHAR(20) NOT NULL DEFAULT 'POS',
    PaymentMethod NVARCHAR(50) NOT NULL CONSTRAINT DF_Orders_PaymentMethod DEFAULT N'Tiền mặt' -- Tiền mặt / Chuyển khoản QR
);

CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL CONSTRAINT FK_OrderDetails_Orders REFERENCES Orders(Id) ON DELETE CASCADE,
    BatchId INT NOT NULL CONSTRAINT FK_OrderDetails_Batches REFERENCES Batches(Id),
    Quantity INT NOT NULL CONSTRAINT CK_OrderDetails_Quantity CHECK (Quantity > 0), -- Số lượng bán > 0
    UnitPrice DECIMAL(18,2) NOT NULL CONSTRAINT CK_OrderDetails_UnitPrice CHECK (UnitPrice >= 0),
    DosageInstruction NVARCHAR(255) NULL
);
GO

-- ==============================================================
-- 6. PHIẾU NHẬP KHO & CHI TIẾT NHẬP KHO
-- Quy tắc: Số lượng nhập > 0, Tiền decimal
-- ==============================================================
CREATE TABLE StockIns (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId INT NOT NULL CONSTRAINT FK_StockIns_Suppliers REFERENCES Suppliers(Id),
    EmployeeId INT NOT NULL CONSTRAINT FK_StockIns_Employees REFERENCES Employees(Id),
    ImportDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalCost DECIMAL(18,2) NOT NULL CONSTRAINT CK_StockIns_TotalCost CHECK (TotalCost >= 0),
    Note NVARCHAR(250) NULL
);

CREATE TABLE StockInDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StockInId INT NOT NULL CONSTRAINT FK_StockInDetails_StockIns REFERENCES StockIns(Id) ON DELETE CASCADE,
    BatchId INT NOT NULL CONSTRAINT FK_StockInDetails_Batches REFERENCES Batches(Id),
    Quantity INT NOT NULL CONSTRAINT CK_StockInDetails_Quantity CHECK (Quantity > 0), -- Số lượng nhập > 0
    UnitCost DECIMAL(18,2) NOT NULL CONSTRAINT CK_StockInDetails_UnitCost CHECK (UnitCost >= 0)
);
GO

-- ==============================================================
-- 7. RÀNG BUỘC UNIQUE & INDEXES TĂNG TỐC TRUY VẤN
-- ==============================================================

-- Unique BarCode (chỉ kiểm tra khi có nhập mã vạch)
CREATE UNIQUE NONCLUSTERED INDEX UQ_Medicines_BarCode 
ON dbo.Medicines(BarCode) 
WHERE BarCode IS NOT NULL AND LTRIM(RTRIM(BarCode)) <> '';
GO

-- Unique Số lô cho cùng một loại thuốc
CREATE UNIQUE NONCLUSTERED INDEX UQ_Batches_Medicine_Batch 
ON dbo.Batches(MedicineId, BatchNumber);
GO

-- Indexes tối ưu hiệu năng
CREATE NONCLUSTERED INDEX IX_Orders_OrderDate 
ON dbo.Orders(OrderDate DESC) 
INCLUDE (TotalAmount, DiscountAmount, EmployeeId, CustomerId, PaymentMethod);
GO

CREATE NONCLUSTERED INDEX IX_OrderDetails_OrderId 
ON dbo.OrderDetails(OrderId) 
INCLUDE (BatchId, Quantity, UnitPrice);
GO

CREATE NONCLUSTERED INDEX IX_Batches_Medicine_Expiry 
ON dbo.Batches(MedicineId, ExpiryDate) 
INCLUDE (Stock, BatchNumber);
GO

CREATE NONCLUSTERED INDEX IX_Medicines_Name 
ON dbo.Medicines(Name) 
INCLUDE (CategoryId, SupplierId, Unit, Price, IsActive);
GO

CREATE NONCLUSTERED INDEX IX_StockIns_ImportDate 
ON dbo.StockIns(ImportDate DESC) 
INCLUDE (SupplierId, EmployeeId, TotalCost);
GO

-- ==============================================================
-- 8. TRIGGERS ĐẢM BẢO TOÀN VẸN DỮ LIỆU
-- ==============================================================

-- Trigger 1: Không cho bán thuốc đã hết hạn sử dụng
CREATE OR ALTER TRIGGER dbo.TR_OrderDetails_CheckExpiry
ON dbo.OrderDetails
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN dbo.Batches b ON b.Id = i.BatchId
        JOIN dbo.Orders o ON o.Id = i.OrderId
        WHERE b.ExpiryDate < CAST(o.OrderDate AS DATE)
    )
    BEGIN
        RAISERROR(N'Không được phép bán thuốc đã hết hạn sử dụng!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- Trigger 2: Không xóa thuốc nếu đang được tham chiếu bởi hóa đơn (chuyển sang ngừng kinh doanh)
CREATE OR ALTER TRIGGER dbo.TR_Medicines_PreventDeleteIfOrdered
ON dbo.Medicines
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1 
        FROM deleted d
        JOIN dbo.Batches b ON b.MedicineId = d.Id
        JOIN dbo.OrderDetails od ON od.BatchId = b.Id
    )
    BEGIN
        RAISERROR(N'Không thể xóa thuốc đã phát sinh hóa đơn bán hàng! Vui lòng chuyển sang trạng thái ngừng kinh doanh (IsActive = 0).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    DELETE FROM dbo.Batches WHERE MedicineId IN (SELECT Id FROM deleted);
    DELETE FROM dbo.Medicines WHERE Id IN (SELECT Id FROM deleted);
END;
GO

-- Trigger 3: Tổng tiền hóa đơn phải khớp với chi tiết đơn hàng
CREATE OR ALTER TRIGGER dbo.TR_Orders_ValidateTotal
ON dbo.Orders
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(TotalAmount)
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM inserted i
            JOIN (
                SELECT OrderId, SUM(Quantity * UnitPrice) AS TongChiTiet
                FROM dbo.OrderDetails
                GROUP BY OrderId
            ) d ON i.Id = d.OrderId
            WHERE ABS(i.TotalAmount - d.TongChiTiet) > 0.01
        )
        BEGIN
            RAISERROR(N'Tổng tiền hóa đơn (TotalAmount) không khớp với chi tiết đơn hàng!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END
END;
GO

-- ==============================================================
-- 9. NẠP DỮ LIỆU MẪU KIỂM THỬ (SAMPLE DATA)
-- ==============================================================

-- Nhân viên
INSERT INTO Employees (FullName, Username, PasswordHash, Phone, IsActive) VALUES
(N'Quản lý hệ thống', 'admin', '123456', '0901111222', 1),
(N'Dược sĩ Mai', 'mai_duocsi', '123456', '0903333444', 1);

-- Nhà cung cấp
INSERT INTO Suppliers (Name, Phone, Address) VALUES 
(N'Dược Hậu Giang (DHG)', '02923891433', N'Cần Thơ'),
(N'Sanofi Việt Nam', '02838298526', N'TP. Hồ Chí Minh'),
(N'Dược phẩm Boston Việt Nam', '02743769606', N'Bình Dương');

-- Danh mục nhóm thuốc
INSERT INTO Categories (Name) VALUES 
(N'Thuốc giảm đau - hạ sốt'),
(N'Kháng sinh - Kháng viêm'),
(N'Vitamin & Khoáng chất');

-- Danh mục thuốc
INSERT INTO Medicines (CategoryId, SupplierId, BarCode, Name, ActiveIngredient, Unit, Price, IsPrescription) VALUES
(1, 1, '8935001800112', N'Panadol Extra', N'Paracetamol 500mg, Caffeine 65mg', N'Hộp', 45000, 0),
(1, 1, '8935001800113', N'Hapacol 250', N'Paracetamol 250mg', N'Gói', 5000, 0),
(2, 2, '8935001800229', N'Augmentin 1g', N'Amoxicillin, Clavulanic acid', N'Hộp', 240000, 1),
(3, 3, '8935001800336', N'Boston C 1000mg', N'Vitamin C', N'Tuýp', 35000, 0);

-- Lô thuốc
INSERT INTO Batches (MedicineId, BatchNumber, ManufacturingDate, ExpiryDate, Stock) VALUES
(1, 'LOT-PND-2024A', '2024-01-01', '2026-12-31', 40),
(1, 'LOT-PND-2025B', '2025-01-01', '2027-10-15', 60),
(2, 'LOT-HPC-2025A', '2025-02-01', '2027-08-20', 100),
(3, 'LOT-AUG-2024X', '2024-05-01', '2027-05-20', 30),
(4, 'LOT-BTC-2025M', '2025-01-01', '2028-01-10', 80);

-- Khách hàng
INSERT INTO Customers (FullName, Phone, Points) VALUES
(N'Trần Văn Tuấn', '0912345678', 15),
(N'Lê Thị Mai', '0987654321', 5);

-- Voucher giảm giá
INSERT INTO Vouchers (Code, DiscountType, DiscountValue, Quantity, IsActive) VALUES
('GIAM10K', 'FIXED', 10000, 50, 1),
('GIAM10', 'PERCENT', 10, 20, 1);
GO

PRINT N'===========================================================';
PRINT N'ĐÃ KHỞI TẠO THÀNH CÔNG DATABASE PharmacyStoreTrialDB!';
PRINT N'ĐẦY ĐỦ 8 QUY TẮC TOÀN VẸN DỮ LIỆU & CỘT HÌNH THỨC THANH TOÁN';
PRINT N'===========================================================';
GO
