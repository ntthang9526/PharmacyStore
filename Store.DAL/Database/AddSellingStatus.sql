-- Chạy trên database đang sử dụng; không tạo database mới hoặc xóa dữ liệu.
-- IsActive vẫn là cờ xóa mềm. IsSelling chỉ kiểm soát việc bán.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.Medicines','IsSelling') IS NULL
 ALTER TABLE dbo.Medicines ADD IsSelling bit NOT NULL CONSTRAINT DF_Medicines_IsSelling DEFAULT 1 WITH VALUES;
IF COL_LENGTH('dbo.Batches','IsBlocked') IS NULL
 ALTER TABLE dbo.Batches ADD IsBlocked bit NOT NULL CONSTRAINT DF_Batches_IsBlocked DEFAULT 0 WITH VALUES;
COMMIT;
