-- ==============================================================
-- HỆ THỐNG QUẢN LÝ NHÀ THUỐC (MiniPharmacyDb)
-- Dành cho nhóm 4 người: WinForms POS + Web API + SQL Server
-- ==============================================================

USE master;
GO

-- Xóa database cũ nếu đã tồn tại để tránh xung đột
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'PharmacyStoreDB')
BEGIN
    ALTER DATABASE PharmacyStoreDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PharmacyStoreDB;
END
GO

-- Tạo mới database
CREATE DATABASE PharmacyStoreDB;
GO

USE PharmacyStoreDB;
GO

-- ==============================================================
-- 1. BẢNG TÀI KHOẢN & NHÂN SỰ
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
-- 2. BẢNG NHÀ CUNG CẤP & DANH MỤC THUỐC
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
-- 3. BẢNG THUỐC & LÔ HÀNG (QUẢN LÝ THEO HẠN DÙNG)
-- ==============================================================
-- Thông tin chung của thuốc (Giá niêm yết, hoạt chất, quy cách)
CREATE TABLE Medicines (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL CONSTRAINT FK_Medicines_Categories REFERENCES Categories(Id),
    SupplierId INT NOT NULL CONSTRAINT FK_Medicines_Suppliers REFERENCES Suppliers(Id),
    BarCode VARCHAR(50) NULL,                 -- Mã vạch sản phẩm quét tít
    Name NVARCHAR(150) NOT NULL,
    ActiveIngredient NVARCHAR(150) NULL,      -- Hoạt chất chính
    Unit NVARCHAR(30) NOT NULL,               -- Hộp, Vỉ, Tuýp, Chai
    Price DECIMAL(18,2) NOT NULL,             -- Giá bán lẻ niêm yết
    IsPrescription BIT NOT NULL DEFAULT 0,    -- 1: Thuốc kê đơn, 0: Không kê đơn
    IsActive BIT NOT NULL DEFAULT 1
);

-- Lô hàng thực tế trong kho (Mỗi đợt nhập tạo 1 dòng riêng để kiểm soát date)
CREATE TABLE Batches (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MedicineId INT NOT NULL CONSTRAINT FK_Batches_Medicines REFERENCES Medicines(Id),
    BatchNumber VARCHAR(50) NOT NULL,         -- Số lô in trên bao bì
    ExpiryDate DATE NOT NULL,                 -- Ngày hết hạn
    Stock INT NOT NULL DEFAULT 0              -- Số lượng tồn kho của riêng lô này
);

-- ==============================================================
-- 4. BẢNG KHÁCH HÀNG & GIAO DỊCH BÁN HÀNG
-- ==============================================================
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    Points INT NOT NULL DEFAULT 0             -- Tích điểm
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL CONSTRAINT FK_Orders_Employees REFERENCES Employees(Id),
    CustomerId INT NULL CONSTRAINT FK_Orders_Customers REFERENCES Customers(Id),
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL,
    Channel NVARCHAR(20) NOT NULL DEFAULT 'POS' -- 'POS' (Giai đoạn 1) hoặc 'WEB' (Giai đoạn 2)
);

CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL CONSTRAINT FK_OrderDetails_Orders REFERENCES Orders(Id) ON DELETE CASCADE,
    BatchId INT NOT NULL CONSTRAINT FK_OrderDetails_Batches REFERENCES Batches(Id), -- Bán chính xác từ lô nào
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    DosageInstruction NVARCHAR(255) NULL     -- Cách dùng in ra bill (vd: Sáng 1v, Chiều 1v)
);
GO

-- ==============================================================
-- NẠP DỮ LIỆU MẪU ĐỂ CHẠY THỬ NGAY
-- ==============================================================

-- 1. Tài khoản đăng nhập mẫu
INSERT INTO Employees (FullName, Username, PasswordHash, Phone, IsActive) VALUES
(N'Quản lý hệ thống', 'admin', '123456', '0901111222', 1),
(N'Dược sĩ Mai', 'mai_duocsi', '123456', '0903333444', 1);

-- 2. Nhà cung cấp
INSERT INTO Suppliers (Name, Phone, Address) VALUES 
(N'Dược Hậu Giang (DHG)', '02923891433', N'Cần Thơ'),
(N'Sanofi Việt Nam', '02838298526', N'TP. Hồ Chí Minh'),
(N'Dược phẩm Boston Việt Nam', '02743769606', N'Bình Dương');

-- 3. Danh mục thuốc
INSERT INTO Categories (Name) VALUES 
(N'Thuốc giảm đau - hạ sốt'),
(N'Kháng sinh - Kháng viêm'),
(N'Vitamin & Khoáng chất');

-- 4. Thuốc
INSERT INTO Medicines (CategoryId, SupplierId, BarCode, Name, ActiveIngredient, Unit, Price, IsPrescription) VALUES
(1, 1, '8935001800112', N'Panadol Extra', N'Paracetamol 500mg, Caffeine 65mg', N'Hộp', 45000, 0),
(1, 1, '8935001800113', N'Hapacol 250', N'Paracetamol 250mg', N'Gói', 5000, 0),
(2, 2, '8935001800229', N'Augmentin 1g', N'Amoxicillin, Clavulanic acid', N'Hộp', 240000, 1),
(3, 3, '8935001800336', N'Boston C 1000mg', N'Vitamin C', N'Tuýp', 35000, 0);

-- 5. Lô hàng (Test thử cùng Panadol nhưng 2 lô date khác nhau)
INSERT INTO Batches (MedicineId, BatchNumber, ExpiryDate, Stock) VALUES
(1, 'LOT-PND-2024A', '2026-12-31', 40),  -- Lô 1: Date gần hơn
(1, 'LOT-PND-2025B', '2027-10-15', 60),  -- Lô 2: Date xa hơn
(2, 'LOT-HPC-2025A', '2027-08-20', 100),
(3, 'LOT-AUG-2024X', '2027-05-20', 30),
(4, 'LOT-BTC-2025M', '2028-01-10', 80);

-- 6. Khách hàng
INSERT INTO Customers (FullName, Phone, Points) VALUES
(N'Trần Văn Tuấn', '0912345678', 15),
(N'Lê Thị Mai', '0987654321', 5);
GO