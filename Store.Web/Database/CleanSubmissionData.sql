-- Sao luu truoc khi chay. Doc bang UTF-8 (sqlcmd -f 65001).
-- Chi don danh muc va an du lieu thu; giu thuoc, gia, ton kho, chung tu.
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
BEGIN TRY
 BEGIN TRANSACTION;
 SELECT Id,Name,Price,Unit,BarCode,SupplierId,IsPrescription INTO #MedicineBefore FROM dbo.Medicines;
 SELECT Id,MedicineId,BatchNumber,ExpiryDate,Stock,IsBlocked,ReservedQuantity INTO #BatchBefore FROM dbo.Batches;
 DECLARE @orders int=(SELECT COUNT(*) FROM dbo.Orders),@webOrders int=(SELECT COUNT(*) FROM dbo.WebOrders);
 DECLARE @receipts int=(SELECT COUNT(*) FROM dbo.StockIns),@webReceipts int=(SELECT COUNT(*) FROM dbo.WebStockIns);

 UPDATE dbo.Medicines SET IsActive=0,IsSelling=0
 WHERE LTRIM(RTRIM(Name))=N'Xyanua' OR LTRIM(RTRIM(ActiveIngredient))=N'Sản phẩm minh họa';

 UPDATE dbo.Categories SET Name=N'Thuốc khác' WHERE LTRIM(RTRIM(Name))=N'Tình huống cảnh báo';
 SELECT Id,MIN(Id) OVER(PARTITION BY LTRIM(RTRIM(Name))) AS KeepId INTO #CategoryMap FROM dbo.Categories;
 UPDATE m SET CategoryId=x.KeepId FROM dbo.Medicines m JOIN #CategoryMap x ON m.CategoryId=x.Id WHERE x.Id<>x.KeepId;
 DECLARE @moved int=@@ROWCOUNT;
 DELETE c FROM dbo.Categories c JOIN #CategoryMap x ON c.Id=x.Id WHERE x.Id<>x.KeepId;
 DECLARE @merged int=@@ROWCOUNT;

 IF EXISTS(SELECT 1 FROM dbo.Medicines m JOIN dbo.Categories c ON c.Id=m.CategoryId
  WHERE c.Name=N'Vật tư y tế' AND m.Name IN(N'Fexofenadine',N'Omeprazole',N'Lansoprazole',N'Famotidine'))
 BEGIN
  IF NOT EXISTS(SELECT 1 FROM dbo.Categories WHERE Name=N'Thuốc khác')
   INSERT dbo.Categories(Name,IsActive,DisplayOrder) VALUES(N'Thuốc khác',1,0);
  UPDATE m SET CategoryId=(SELECT MIN(Id) FROM dbo.Categories WHERE Name=N'Thuốc khác')
  FROM dbo.Medicines m JOIN dbo.Categories c ON c.Id=m.CategoryId
  WHERE c.Name=N'Vật tư y tế' AND m.Name IN(N'Fexofenadine',N'Omeprazole',N'Lansoprazole',N'Famotidine');
 END;

 UPDATE dbo.Medicines SET ActiveIngredient=LTRIM(STUFF(LTRIM(ActiveIngredient),1,1,N''))
 WHERE LTRIM(ActiveIngredient) LIKE N',%';

 IF EXISTS(SELECT * FROM #MedicineBefore EXCEPT SELECT Id,Name,Price,Unit,BarCode,SupplierId,IsPrescription FROM dbo.Medicines)
 OR (SELECT COUNT(*) FROM dbo.Medicines)<>(SELECT COUNT(*) FROM #MedicineBefore)
 OR EXISTS(SELECT * FROM #BatchBefore EXCEPT SELECT Id,MedicineId,BatchNumber,ExpiryDate,Stock,IsBlocked,ReservedQuantity FROM dbo.Batches)
 OR (SELECT COUNT(*) FROM dbo.Batches)<>(SELECT COUNT(*) FROM #BatchBefore)
 OR @orders<>(SELECT COUNT(*) FROM dbo.Orders) OR @webOrders<>(SELECT COUNT(*) FROM dbo.WebOrders)
 OR @receipts<>(SELECT COUNT(*) FROM dbo.StockIns) OR @webReceipts<>(SELECT COUNT(*) FROM dbo.WebStockIns)
  THROW 50002,N'Kiểm tra bảo toàn dữ liệu không đạt; hoàn tác.',1;

 UPDATE dbo.SystemConfig SET [Value]=CONVERT(nvarchar(255),COALESCE(TRY_CONVERT(bigint,[Value]),0)+1),UpdatedAt=GETDATE()
 WHERE [Key]=N'DataVersion';
 COMMIT TRANSACTION;
 SELECT @merged AS DuplicateCategoriesRemoved,@moved AS MedicinesMerged;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
