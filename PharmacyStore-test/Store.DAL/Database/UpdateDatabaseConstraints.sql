-- ==============================================================
-- SCRIPT NÂNG CẤP CƠ SỞ DỮ LIỆU: ĐẢM BẢO 8 QUY TẮC DỮ LIỆU CHUẨN MỰC
-- Cơ sở dữ liệu: PharmacyStoreTrialDB (SQL Server)
-- Script Idempotent: Có thể chạy nhiều lần trên database hiện tại mà không bị lỗi.
-- ==============================================================

USE PharmacyStoreTrialDB;
GO

PRINT N'Bắt đầu kiểm tra và cập nhật các quy tắc dữ liệu...';
GO

-- ==============================================================
-- QUY TẮC 1: MÃ THUỐC KHÔNG ĐƯỢC TRÙNG
-- Medicines.Id đã là PRIMARY KEY IDENTITY (duy nhất tuyệt đối).
-- Đồng thời bổ sung UNIQUE cho mã vạch (BarCode) khi có nhập.
-- ==============================================================
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Medicines_BarCode' AND object_id = OBJECT_ID('dbo.Medicines'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Medicines_BarCode 
    ON dbo.Medicines(BarCode) 
    WHERE BarCode IS NOT NULL AND LTRIM(RTRIM(BarCode)) <> '';
    PRINT N'[Quy tắc 1] Đã tạo FILTERED UNIQUE INDEX UQ_Medicines_BarCode.';
END
ELSE
    PRINT N'[Quy tắc 1] UQ_Medicines_BarCode đã tồn tại.';
GO

-- Số lô của cùng 1 loại thuốc không được trùng
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Batches_Medicine_Batch' AND object_id = OBJECT_ID('dbo.Batches'))
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UQ_Batches_Medicine_Batch 
    ON dbo.Batches(MedicineId, BatchNumber);
    PRINT N'[Quy tắc 1] Đã tạo UNIQUE INDEX UQ_Batches_Medicine_Batch.';
END
GO


-- ==============================================================
-- QUY TẮC 2: SỐ LƯỢNG NHẬP, SỐ LƯỢNG BÁN PHẢI LỚN HƠN 0
-- ==============================================================
-- 2.1 Số lượng bán > 0 (OrderDetails)
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_OrderDetails_Quantity')
BEGIN
    ALTER TABLE dbo.OrderDetails ADD CONSTRAINT CK_OrderDetails_Quantity CHECK (Quantity > 0);
    PRINT N'[Quy tắc 2] Đã thêm CHECK Quantity > 0 cho OrderDetails.';
END
GO

-- 2.2 Số lượng nhập > 0 (StockInDetails nếu có bảng)
IF OBJECT_ID('dbo.StockInDetails', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_StockInDetails_Quantity')
    BEGIN
        ALTER TABLE dbo.StockInDetails ADD CONSTRAINT CK_StockInDetails_Quantity CHECK (Quantity > 0);
        PRINT N'[Quy tắc 2] Đã thêm CHECK Quantity > 0 cho StockInDetails.';
    END
END
GO


-- ==============================================================
-- QUY TẮC 3: KHÔNG CHO PHÉP BÁN VƯỢT SỐ LƯỢNG TỒN CỦA LÔ
-- Ràng buộc Stock >= 0 đảm bảo khi trừ kho không bao giờ bị âm.
-- ==============================================================
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Batches_Stock')
BEGIN
    ALTER TABLE dbo.Batches ADD CONSTRAINT CK_Batches_Stock CHECK (Stock >= 0);
    PRINT N'[Quy tắc 3] Đã thêm CHECK Stock >= 0 cho Batches.';
END
ELSE
    PRINT N'[Quy tắc 3] CK_Batches_Stock đã tồn tại.';
GO


-- ==============================================================
-- QUY TẮC 4: KHÔNG CHO BÁN THUỐC ĐÃ HẾT HẠN SỬ DỤNG
-- Dùng Trigger cấp DB để ngăn chặn triệt để mọi hành vi bán thuốc hết hạn.
-- ==============================================================
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
        RAISERROR(N'[Vi phạm quy tắc 4] Không được phép bán thuốc đã hết hạn sử dụng!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO
PRINT N'[Quy tắc 4] Đã tạo TRIGGER TR_OrderDetails_CheckExpiry kiểm tra hạn dùng khi bán.';
GO


-- ==============================================================
-- QUY TẮC 5: NGÀY HẾT HẠN PHẢI SAU NGÀY SẢN XUẤT NẾU CÓ CẢ HAI NGÀY
-- Bổ sung cột ManufacturingDate (nếu chưa có) và ràng buộc CHECK.
-- ==============================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.Batches') AND name = 'ManufacturingDate'
)
BEGIN
    ALTER TABLE dbo.Batches ADD ManufacturingDate DATE NULL;
    PRINT N'[Quy tắc 5] Đã thêm cột ManufacturingDate vào bảng Batches.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Batches_ExpiryAfterMfg')
BEGIN
    ALTER TABLE dbo.Batches 
    ADD CONSTRAINT CK_Batches_ExpiryAfterMfg 
    CHECK (ManufacturingDate IS NULL OR ExpiryDate > ManufacturingDate);
    PRINT N'[Quy tắc 5] Đã thêm CHECK ExpiryDate > ManufacturingDate cho Batches.';
END
ELSE
    PRINT N'[Quy tắc 5] CK_Batches_ExpiryAfterMfg đã tồn tại.';
GO


-- ==============================================================
-- QUY TẮC 6: KHÔNG XÓA THUỐC NẾU ĐANG ĐƯỢC THAM CHIẾU BỞI HÓA ĐƠN;
--            CÓ THỂ CHUYỂN SANG TRẠNG THÁI NGỪNG KINH DOANH (IsActive = 0)
-- ==============================================================
CREATE OR ALTER TRIGGER dbo.TR_Medicines_PreventDeleteIfOrdered
ON dbo.Medicines
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    -- Kiểm tra thuốc đang bị xóa có đơn hàng trong OrderDetails không
    IF EXISTS (
        SELECT 1 
        FROM deleted d
        JOIN dbo.Batches b ON b.MedicineId = d.Id
        JOIN dbo.OrderDetails od ON od.BatchId = b.Id
    )
    BEGIN
        RAISERROR(N'[Vi phạm quy tắc 6] Không thể xóa thuốc đã phát sinh hóa đơn bán hàng! Vui lòng chuyển sang trạng thái ngừng kinh doanh (IsActive = 0).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Nếu chỉ có trong kho hoặc chưa từng bán, cho phép xóa lô và xóa thuốc
    DELETE FROM dbo.Batches WHERE MedicineId IN (SELECT Id FROM deleted);
    DELETE FROM dbo.Medicines WHERE Id IN (SELECT Id FROM deleted);
END;
GO
PRINT N'[Quy tắc 6] Đã tạo TRIGGER TR_Medicines_PreventDeleteIfOrdered bảo vệ dữ liệu thuốc.';
GO


-- ==============================================================
-- QUY TẮC 7: TỔNG TIỀN HÓA ĐƠN PHẢI KHỚP VỚI CHI TIẾT HÓA ĐƠN
-- Trigger kiểm tra tính toàn vẹn khi cập nhật TotalAmount trên Orders.
-- ==============================================================
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
            RAISERROR(N'[Vi phạm quy tắc 7] Tổng tiền hóa đơn (TotalAmount) không khớp với chi tiết đơn hàng!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END
END;
GO
PRINT N'[Quy tắc 7] Đã tạo TRIGGER TR_Orders_ValidateTotal xác thực tổng tiền hóa đơn.';
GO


-- ==============================================================
-- QUY TẮC 8: GIÁ TIỀN SỬ DỤNG KIỂU DỮ LIỆU PHÙ HỢP (DECIMAL), KHÔNG DÙNG FLOAT
-- Bổ sung CHECK >= 0 cho tất cả các trường tiền tệ.
-- ==============================================================
-- 8.1 Giá thuốc >= 0
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Medicines_Price')
BEGIN
    ALTER TABLE dbo.Medicines ADD CONSTRAINT CK_Medicines_Price CHECK (Price >= 0);
    PRINT N'[Quy tắc 8] Đã thêm CHECK Price >= 0.';
END
GO

-- 8.2 Đơn giá bán trong chi tiết đơn hàng >= 0
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_OrderDetails_UnitPrice')
BEGIN
    ALTER TABLE dbo.OrderDetails ADD CONSTRAINT CK_OrderDetails_UnitPrice CHECK (UnitPrice >= 0);
    PRINT N'[Quy tắc 8] Đã thêm CHECK UnitPrice >= 0.';
END
GO

-- 8.3 Tổng tiền và giảm giá trong hóa đơn >= 0
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Orders_TotalAmount')
BEGIN
    ALTER TABLE dbo.Orders ADD CONSTRAINT CK_Orders_TotalAmount CHECK (TotalAmount >= 0);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Orders_DiscountAmount')
BEGIN
    ALTER TABLE dbo.Orders ADD CONSTRAINT CK_Orders_DiscountAmount CHECK (DiscountAmount >= 0);
END
GO

-- 8.4 Giá trị voucher > 0
IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Vouchers_DiscountValue')
BEGIN
    ALTER TABLE dbo.Vouchers ADD CONSTRAINT CK_Vouchers_DiscountValue CHECK (DiscountValue > 0);
END
GO

-- 8.5 Giá vốn nhập kho >= 0
IF OBJECT_ID('dbo.StockIns', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_StockIns_TotalCost')
    BEGIN
        ALTER TABLE dbo.StockIns ADD CONSTRAINT CK_StockIns_TotalCost CHECK (TotalCost >= 0);
    END
END
GO

IF OBJECT_ID('dbo.StockInDetails', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_StockInDetails_UnitCost')
    BEGIN
        ALTER TABLE dbo.StockInDetails ADD CONSTRAINT CK_StockInDetails_UnitCost CHECK (UnitCost >= 0);
    END
END
GO


-- ==============================================================
-- BỔ SUNG: CỘT HÌNH THỨC THANH TOÁN (PaymentMethod) VÀO ORDERS
-- ==============================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.Orders') AND name = 'PaymentMethod'
)
BEGIN
    ALTER TABLE dbo.Orders 
    ADD PaymentMethod NVARCHAR(50) NOT NULL CONSTRAINT DF_Orders_PaymentMethod DEFAULT N'Tiền mặt';
    PRINT N'-> Đã thêm cột PaymentMethod vào bảng Orders.';
END
GO


-- ==============================================================
-- BỔ SUNG: CÁC INDEXES TĂNG TỐC TRUY VẤN
-- ==============================================================
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Orders_OrderDate' AND object_id = OBJECT_ID('dbo.Orders'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Orders_OrderDate 
    ON dbo.Orders(OrderDate DESC) 
    INCLUDE (TotalAmount, DiscountAmount, EmployeeId, CustomerId, PaymentMethod);
    PRINT N'-> Đã tạo INDEX IX_Orders_OrderDate.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_OrderDetails_OrderId' AND object_id = OBJECT_ID('dbo.OrderDetails'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_OrderDetails_OrderId 
    ON dbo.OrderDetails(OrderId) 
    INCLUDE (BatchId, Quantity, UnitPrice);
    PRINT N'-> Đã tạo INDEX IX_OrderDetails_OrderId.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Batches_Medicine_Expiry' AND object_id = OBJECT_ID('dbo.Batches'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Batches_Medicine_Expiry 
    ON dbo.Batches(MedicineId, ExpiryDate) 
    INCLUDE (Stock, BatchNumber);
    PRINT N'-> Đã tạo INDEX IX_Batches_Medicine_Expiry.';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Medicines_Name' AND object_id = OBJECT_ID('dbo.Medicines'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Medicines_Name 
    ON dbo.Medicines(Name) 
    INCLUDE (CategoryId, SupplierId, Unit, Price, IsActive);
    PRINT N'-> Đã tạo INDEX IX_Medicines_Name.';
END
GO

IF OBJECT_ID('dbo.StockIns', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_StockIns_ImportDate' AND object_id = OBJECT_ID('dbo.StockIns'))
    BEGIN
        CREATE NONCLUSTERED INDEX IX_StockIns_ImportDate 
        ON dbo.StockIns(ImportDate DESC) 
        INCLUDE (SupplierId, EmployeeId, TotalCost);
        PRINT N'-> Đã tạo INDEX IX_StockIns_ImportDate.';
    END
END
GO

PRINT N'==============================================================';
PRINT N'ĐÃ HOÀN TẤT KIỂM TRA VÀ THIẾT LẬP TOÀN BỘ 8 QUY TẮC DỮ LIỆU!';
PRINT N'==============================================================';
GO
