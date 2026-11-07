-- Sample opening stock for coursework, not supplier-provided stock/expiry data.
-- Run with UTF-8: sqlcmd -S .\SQLEXPRESS -E -C -d PharmacyStoreDB -b -f 65001 -i Seed-SampleStock.sql
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
IF DB_NAME()<>N'PharmacyStoreDB' AND DB_NAME() NOT LIKE N'PharmacyWebIntegration[_]%'
 THROW 50001,N'Sai database đích.',1;
IF (SELECT COUNT(*) FROM dbo.Medicines WHERE BarCode LIKE 'LCREF-[0-9][0-9][0-9]')<>50
 THROW 50002,N'Cần nhập đủ 50 thuốc Long Châu trước.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 INSERT dbo.Batches(MedicineId,BatchNumber,ExpiryDate,Stock,IsBlocked,ReservedQuantity)
 SELECT m.Id,'LC-INIT-20261107-'+RIGHT(m.BarCode,3),DATEFROMPARTS(2028,11,7),100,0,0
 FROM dbo.Medicines m
 WHERE m.BarCode LIKE 'LCREF-[0-9][0-9][0-9]'
 AND NOT EXISTS(SELECT 1 FROM dbo.Batches b WITH(UPDLOCK,HOLDLOCK) WHERE b.MedicineId=m.Id);
 DECLARE @added int=@@ROWCOUNT;
 IF EXISTS(SELECT 1 FROM dbo.Batches b JOIN dbo.Medicines m ON m.Id=b.MedicineId
  WHERE m.BarCode LIKE 'LCREF-[0-9][0-9][0-9]' AND b.ReservedQuantity>b.Stock)
  THROW 50003,N'Số lượng giữ hàng vượt tồn.',1;
 UPDATE dbo.SystemConfig SET [Value]=CONVERT(nvarchar(255),COALESCE(TRY_CONVERT(bigint,[Value]),0)+1),UpdatedAt=GETDATE()
 WHERE [Key]=N'DataVersion';
 COMMIT TRANSACTION;
 SELECT @added AS AddedSampleBatches;
 SELECT COUNT(DISTINCT m.Id) AS MedicinesWithStock,SUM(b.Stock) AS TotalStock
 FROM dbo.Medicines m JOIN dbo.Batches b ON b.MedicineId=m.Id
 WHERE m.BarCode LIKE 'LCREF-[0-9][0-9][0-9]';
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
