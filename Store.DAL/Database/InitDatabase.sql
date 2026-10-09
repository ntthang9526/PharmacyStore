-- ==============================================================
-- CƠ SỞ DỮ LIỆU: PharmacyStoreDB (Đã bỏ Role ở Employees)
-- Dành cho nhóm: WinForms POS + ASP.NET Core Web API + SQL Server
-- ==============================================================

USE master;
GO

-- Xóa database cũ nếu đã tồn tại để làm mới đồng bộ
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'PharmacyStoreDB')
BEGIN
    ALTER DATABASE PharmacyStoreDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PharmacyStoreDB;
END
GO

-- Khởi tạo database mới
CREATE DATABASE PharmacyStoreDB;
GO

USE PharmacyStoreDB;
GO

-- Cấp quyền owner để mở Database Diagram không bị lỗi
ALTER AUTHORIZATION ON DATABASE::PharmacyStoreDB TO sa;
GO

-- ==============================================================
-- 1. TÀI KHOẢN & NHÂN SỰ (ĐÃ BỎ CỘT ROLE)
-- ==============================================================
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username VARCHAR(50) UNIQUE NOT NULL,     -- Tài khoản đăng nhập
    PasswordHash VARCHAR(255) NOT NULL,       -- Mật khẩu
    Phone VARCHAR(15) NULL,
    IsActive BIT NOT NULL DEFAULT 1           -- 1: Đang làm việc, 0: Đã khóa
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