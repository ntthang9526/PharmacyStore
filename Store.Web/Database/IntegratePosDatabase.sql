-- Chạy sau khi đã sao lưu PharmacyStoreDB. Script có thể chạy lại.
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
IF DB_NAME() <> N'PharmacyStoreDB' AND DB_NAME() NOT LIKE N'PharmacyWebIntegration[_]%'
    THROW 50001,N'Sai database đích. Chỉ dùng database POS hoặc database kiểm tra đã đặt tên.',1;
IF OBJECT_ID(N'dbo.Medicines',N'U') IS NULL OR OBJECT_ID(N'dbo.Customers',N'U') IS NULL
    THROW 50002,N'Database chưa có cấu trúc POS. Không tạo dữ liệu mẫu thay thế.',1;
BEGIN TRANSACTION;
GO
IF COL_LENGTH(N'dbo.Categories',N'Slug') IS NULL EXEC(N'ALTER TABLE dbo.[Categories] ADD [Slug] AS (N''nhom-'' + CONVERT(nvarchar(20), [Id])) PERSISTED;');
GO
IF COL_LENGTH(N'dbo.Categories',N'Description') IS NULL EXEC(N'ALTER TABLE dbo.[Categories] ADD [Description] nvarchar(255) NULL;');
GO
IF COL_LENGTH(N'dbo.Categories',N'IsActive') IS NULL EXEC(N'ALTER TABLE dbo.[Categories] ADD [IsActive] bit NOT NULL CONSTRAINT DF_WebCategoryActive DEFAULT(1) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Categories',N'DisplayOrder') IS NULL EXEC(N'ALTER TABLE dbo.[Categories] ADD [DisplayOrder] int NOT NULL CONSTRAINT DF_WebCategoryOrder DEFAULT(0) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Suppliers',N'IsActive') IS NULL EXEC(N'ALTER TABLE dbo.[Suppliers] ADD [IsActive] bit NOT NULL CONSTRAINT DF_WebSupplierActive DEFAULT(1) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'Slug') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [Slug] AS (N''thuoc-'' + CONVERT(nvarchar(20), [Id])) PERSISTED;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'PackagingSpec') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [PackagingSpec] nvarchar(150) NOT NULL CONSTRAINT DF_WebPackaging DEFAULT(N'''') WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'Featured') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [Featured] bit NOT NULL CONSTRAINT DF_WebFeatured DEFAULT(0) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'DescriptionHtml') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [DescriptionHtml] nvarchar(max) NULL;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'MainImageUrl') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [MainImageUrl] nvarchar(max) NULL;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'CreatedAt') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [CreatedAt] datetime2 NOT NULL CONSTRAINT DF_WebMedicineCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Medicines',N'RowVersion') IS NULL EXEC(N'ALTER TABLE dbo.[Medicines] ADD [RowVersion] rowversion NOT NULL;');
GO
IF COL_LENGTH(N'dbo.Batches',N'ReservedQuantity') IS NULL EXEC(N'ALTER TABLE dbo.[Batches] ADD [ReservedQuantity] int NOT NULL CONSTRAINT DF_WebReserved DEFAULT(0) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Batches',N'RowVersion') IS NULL EXEC(N'ALTER TABLE dbo.[Batches] ADD [RowVersion] rowversion NOT NULL;');
GO
IF COL_LENGTH(N'dbo.Batches',N'CreatedAt') IS NULL EXEC(N'ALTER TABLE dbo.[Batches] ADD [CreatedAt] datetime2 NOT NULL CONSTRAINT DF_WebBatchCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Customers',N'Email') IS NULL EXEC(N'ALTER TABLE dbo.[Customers] ADD [Email] nvarchar(100) NOT NULL CONSTRAINT DF_WebCustomerEmail DEFAULT(N'''') WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Customers',N'WebAccountActive') IS NULL EXEC(N'ALTER TABLE dbo.[Customers] ADD [WebAccountActive] bit NOT NULL CONSTRAINT DF_WebCustomerAccount DEFAULT(0) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Customers',N'IsLocked') IS NULL EXEC(N'ALTER TABLE dbo.[Customers] ADD [IsLocked] bit NOT NULL CONSTRAINT DF_WebCustomerLocked DEFAULT(0) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Customers',N'CreatedAt') IS NULL EXEC(N'ALTER TABLE dbo.[Customers] ADD [CreatedAt] datetime2 NOT NULL CONSTRAINT DF_WebCustomerCreated DEFAULT(SYSUTCDATETIME()) WITH VALUES;');
GO
IF COL_LENGTH(N'dbo.Customers',N'UserId') IS NULL EXEC(N'ALTER TABLE dbo.[Customers] ADD [UserId] nvarchar(450) NULL;');
GO
IF OBJECT_ID(N'dbo.WebRoles',N'U') IS NULL BEGIN
CREATE TABLE [WebRoles] (
    [Id] nvarchar(128) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_WebRoles] PRIMARY KEY ([Id])
);
END;
GO
IF OBJECT_ID(N'dbo.WebVouchers',N'U') IS NULL BEGIN
CREATE TABLE [WebVouchers] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(50) NOT NULL,
    [DiscountType] nvarchar(20) NOT NULL,
    [DiscountValue] decimal(18,2) NOT NULL,
    [MinOrderValue] decimal(18,2) NOT NULL,
    [MaxDiscountCap] decimal(18,2) NULL,
    [Quantity] int NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(255) NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    CONSTRAINT [PK_WebVouchers] PRIMARY KEY ([Id])
);
END;
GO
IF OBJECT_ID(N'dbo.WebStockIns',N'U') IS NULL BEGIN
CREATE TABLE [WebStockIns] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(30) NOT NULL,
    [SupplierId] int NOT NULL,
    [EmployeeName] nvarchar(100) NOT NULL,
    [ImportDate] datetime2 NOT NULL,
    [TotalCost] decimal(18,2) NOT NULL,
    [Note] nvarchar(500) NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_WebStockIns] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebStockIns_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
);
END;
GO
IF OBJECT_ID(N'dbo.WebRoleClaims',N'U') IS NULL BEGIN
CREATE TABLE [WebRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(128) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_WebRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebRoleClaims_WebRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [WebRoles] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebMedicineImages',N'U') IS NULL BEGIN
CREATE TABLE [WebMedicineImages] (
    [Id] int NOT NULL IDENTITY,
    [MedicineId] int NOT NULL,
    [ImageUrl] nvarchar(255) NOT NULL,
    [IsPrimary] bit NOT NULL,
    [DisplayOrder] int NOT NULL,
    CONSTRAINT [PK_WebMedicineImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebMedicineImages_Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [Medicines] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebStockInDetails',N'U') IS NULL BEGIN
CREATE TABLE [WebStockInDetails] (
    [Id] int NOT NULL IDENTITY,
    [StockInId] int NOT NULL,
    [MedicineId] int NOT NULL,
    [BatchNumber] nvarchar(50) NOT NULL,
    [ExpiryDate] datetime2 NOT NULL,
    [Quantity] int NOT NULL,
    [UnitCost] decimal(18,2) NOT NULL,
    [TotalCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_WebStockInDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebStockInDetails_Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [Medicines] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_WebStockInDetails_WebStockIns_StockInId] FOREIGN KEY ([StockInId]) REFERENCES [WebStockIns] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebCustomerAddresses',N'U') IS NULL BEGIN
CREATE TABLE [WebCustomerAddresses] (
    [Id] int NOT NULL IDENTITY,
    [CustomerProfileId] int NOT NULL,
    [Label] nvarchar(50) NOT NULL,
    [RecipientName] nvarchar(100) NOT NULL,
    [Phone] nvarchar(20) NOT NULL,
    [Address] nvarchar(255) NOT NULL,
    [IsDefault] bit NOT NULL,
    CONSTRAINT [PK_WebCustomerAddresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebCustomerAddresses_Customers_CustomerProfileId] FOREIGN KEY ([CustomerProfileId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebOrders',N'U') IS NULL BEGIN
CREATE TABLE [WebOrders] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(30) NOT NULL,
    [CustomerId] int NOT NULL,
    [CustomerName] nvarchar(100) NOT NULL,
    [CustomerPhone] nvarchar(20) NOT NULL,
    [ShippingAddress] nvarchar(255) NOT NULL,
    [Channel] nvarchar(20) NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [OrderStatus] nvarchar(30) NOT NULL,
    [PaymentMethod] nvarchar(30) NOT NULL,
    [PaymentStatus] nvarchar(30) NOT NULL,
    [ShippingStatus] nvarchar(30) NOT NULL,
    [VoucherCode] nvarchar(50) NULL,
    [SubtotalAmount] decimal(18,2) NOT NULL,
    [DiscountAmount] decimal(18,2) NOT NULL,
    [ShippingFee] decimal(18,2) NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [OrderVersion] int NOT NULL,
    [Note] nvarchar(500) NULL,
    [CancelReason] nvarchar(500) NULL,
    [PaidAt] datetime2 NULL,
    [PaidByEmployee] nvarchar(100) NULL,
    [IsRefunded] bit NOT NULL DEFAULT 0,
    [RefundAmount] decimal(18,2) NOT NULL DEFAULT 0,
    [RefundedAt] datetime2 NULL,
    [RefundedByEmployee] nvarchar(100) NULL,
    [RefundReason] nvarchar(500) NULL,
    [ConfirmedAt] datetime2 NULL,
    [ShippedAt] datetime2 NULL,
    [CompletedAt] datetime2 NULL,
    [IdempotencyKey] nvarchar(100) NULL,
    CONSTRAINT [PK_WebOrders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebOrders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
END;
GO
IF OBJECT_ID(N'dbo.WebPrescriptionRequests',N'U') IS NULL BEGIN
CREATE TABLE [WebPrescriptionRequests] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(30) NOT NULL,
    [CustomerId] int NOT NULL,
    [PatientName] nvarchar(100) NOT NULL,
    [Phone] nvarchar(20) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [Status] nvarchar(30) NOT NULL,
    [CustomerNote] nvarchar(1000) NULL,
    [PharmacistNote] nvarchar(1000) NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_WebPrescriptionRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebPrescriptionRequests_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
END;
GO
IF OBJECT_ID(N'dbo.WebUsers',N'U') IS NULL BEGIN
CREATE TABLE [WebUsers] (
    [Id] nvarchar(128) NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [IsLocked] bit NOT NULL,
    [CustomerProfileId] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_WebUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebUsers_Customers_CustomerProfileId] FOREIGN KEY ([CustomerProfileId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
END;
GO
IF OBJECT_ID(N'dbo.WebOrderBatchAllocations',N'U') IS NULL BEGIN
CREATE TABLE [WebOrderBatchAllocations] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [BatchId] int NOT NULL,
    [Quantity] int NOT NULL,
    [IsDeducted] bit NOT NULL,
    CONSTRAINT [PK_WebOrderBatchAllocations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebOrderBatchAllocations_Batches_BatchId] FOREIGN KEY ([BatchId]) REFERENCES [Batches] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_WebOrderBatchAllocations_WebOrders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [WebOrders] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebOrderItems',N'U') IS NULL BEGIN
CREATE TABLE [WebOrderItems] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [MedicineId] int NOT NULL,
    [MedicineName] nvarchar(150) NOT NULL,
    [Unit] nvarchar(50) NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [IsPrescriptionSnapshot] bit NOT NULL,
    CONSTRAINT [PK_WebOrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebOrderItems_Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [Medicines] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_WebOrderItems_WebOrders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [WebOrders] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebOrderStatusHistories',N'U') IS NULL BEGIN
CREATE TABLE [WebOrderStatusHistories] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [Time] datetime2 NOT NULL,
    [Actor] nvarchar(100) NOT NULL,
    [Text] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_WebOrderStatusHistories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebOrderStatusHistories_WebOrders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [WebOrders] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebPrescriptionImages',N'U') IS NULL BEGIN
CREATE TABLE [WebPrescriptionImages] (
    [Id] int NOT NULL IDENTITY,
    [PrescriptionRequestId] int NOT NULL,
    [FileName] nvarchar(255) NOT NULL,
    [StoredPath] nvarchar(500) NOT NULL,
    [FileSizeBytes] bigint NOT NULL,
    [ContentType] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_WebPrescriptionImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebPrescriptionImages_WebPrescriptionRequests_PrescriptionRequestId] FOREIGN KEY ([PrescriptionRequestId]) REFERENCES [WebPrescriptionRequests] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebUserClaims',N'U') IS NULL BEGIN
CREATE TABLE [WebUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(128) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_WebUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WebUserClaims_WebUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [WebUsers] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebUserLogins',N'U') IS NULL BEGIN
CREATE TABLE [WebUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(128) NOT NULL,
    CONSTRAINT [PK_WebUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_WebUserLogins_WebUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [WebUsers] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebUserRoles',N'U') IS NULL BEGIN
CREATE TABLE [WebUserRoles] (
    [UserId] nvarchar(128) NOT NULL,
    [RoleId] nvarchar(128) NOT NULL,
    CONSTRAINT [PK_WebUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_WebUserRoles_WebRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [WebRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_WebUserRoles_WebUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [WebUsers] ([Id]) ON DELETE CASCADE
);
END;
GO
IF OBJECT_ID(N'dbo.WebUserTokens',N'U') IS NULL BEGIN
CREATE TABLE [WebUserTokens] (
    [UserId] nvarchar(128) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_WebUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_WebUserTokens_WebUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [WebUsers] ([Id]) ON DELETE CASCADE
);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebCustomerAddresses_CustomerProfileId' AND object_id=OBJECT_ID(N'dbo.WebCustomerAddresses')) BEGIN
CREATE INDEX [IX_WebCustomerAddresses_CustomerProfileId] ON [WebCustomerAddresses] ([CustomerProfileId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebMedicineImages_MedicineId' AND object_id=OBJECT_ID(N'dbo.WebMedicineImages')) BEGIN
CREATE INDEX [IX_WebMedicineImages_MedicineId] ON [WebMedicineImages] ([MedicineId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrderBatchAllocations_BatchId' AND object_id=OBJECT_ID(N'dbo.WebOrderBatchAllocations')) BEGIN
CREATE INDEX [IX_WebOrderBatchAllocations_BatchId] ON [WebOrderBatchAllocations] ([BatchId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrderBatchAllocations_OrderId' AND object_id=OBJECT_ID(N'dbo.WebOrderBatchAllocations')) BEGIN
CREATE INDEX [IX_WebOrderBatchAllocations_OrderId] ON [WebOrderBatchAllocations] ([OrderId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrderItems_MedicineId' AND object_id=OBJECT_ID(N'dbo.WebOrderItems')) BEGIN
CREATE INDEX [IX_WebOrderItems_MedicineId] ON [WebOrderItems] ([MedicineId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrderItems_OrderId' AND object_id=OBJECT_ID(N'dbo.WebOrderItems')) BEGIN
CREATE INDEX [IX_WebOrderItems_OrderId] ON [WebOrderItems] ([OrderId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrders_Code' AND object_id=OBJECT_ID(N'dbo.WebOrders')) BEGIN
CREATE UNIQUE INDEX [IX_WebOrders_Code] ON [WebOrders] ([Code]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrders_CustomerId_IdempotencyKey' AND object_id=OBJECT_ID(N'dbo.WebOrders')) BEGIN
CREATE UNIQUE INDEX [IX_WebOrders_CustomerId_IdempotencyKey] ON [WebOrders] ([CustomerId], [IdempotencyKey]) WHERE [IdempotencyKey] IS NOT NULL;
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebOrderStatusHistories_OrderId' AND object_id=OBJECT_ID(N'dbo.WebOrderStatusHistories')) BEGIN
CREATE INDEX [IX_WebOrderStatusHistories_OrderId] ON [WebOrderStatusHistories] ([OrderId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebPrescriptionImages_PrescriptionRequestId' AND object_id=OBJECT_ID(N'dbo.WebPrescriptionImages')) BEGIN
CREATE INDEX [IX_WebPrescriptionImages_PrescriptionRequestId] ON [WebPrescriptionImages] ([PrescriptionRequestId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebPrescriptionRequests_Code' AND object_id=OBJECT_ID(N'dbo.WebPrescriptionRequests')) BEGIN
CREATE UNIQUE INDEX [IX_WebPrescriptionRequests_Code] ON [WebPrescriptionRequests] ([Code]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebPrescriptionRequests_CustomerId' AND object_id=OBJECT_ID(N'dbo.WebPrescriptionRequests')) BEGIN
CREATE INDEX [IX_WebPrescriptionRequests_CustomerId] ON [WebPrescriptionRequests] ([CustomerId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebRoleClaims_RoleId' AND object_id=OBJECT_ID(N'dbo.WebRoleClaims')) BEGIN
CREATE INDEX [IX_WebRoleClaims_RoleId] ON [WebRoleClaims] ([RoleId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'RoleNameIndex' AND object_id=OBJECT_ID(N'dbo.WebRoles')) BEGIN
CREATE UNIQUE INDEX [RoleNameIndex] ON [WebRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebStockInDetails_MedicineId' AND object_id=OBJECT_ID(N'dbo.WebStockInDetails')) BEGIN
CREATE INDEX [IX_WebStockInDetails_MedicineId] ON [WebStockInDetails] ([MedicineId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebStockInDetails_StockInId' AND object_id=OBJECT_ID(N'dbo.WebStockInDetails')) BEGIN
CREATE INDEX [IX_WebStockInDetails_StockInId] ON [WebStockInDetails] ([StockInId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebStockIns_Code' AND object_id=OBJECT_ID(N'dbo.WebStockIns')) BEGIN
CREATE UNIQUE INDEX [IX_WebStockIns_Code] ON [WebStockIns] ([Code]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebStockIns_SupplierId' AND object_id=OBJECT_ID(N'dbo.WebStockIns')) BEGIN
CREATE INDEX [IX_WebStockIns_SupplierId] ON [WebStockIns] ([SupplierId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebUserClaims_UserId' AND object_id=OBJECT_ID(N'dbo.WebUserClaims')) BEGIN
CREATE INDEX [IX_WebUserClaims_UserId] ON [WebUserClaims] ([UserId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebUserLogins_UserId' AND object_id=OBJECT_ID(N'dbo.WebUserLogins')) BEGIN
CREATE INDEX [IX_WebUserLogins_UserId] ON [WebUserLogins] ([UserId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebUserRoles_RoleId' AND object_id=OBJECT_ID(N'dbo.WebUserRoles')) BEGIN
CREATE INDEX [IX_WebUserRoles_RoleId] ON [WebUserRoles] ([RoleId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'EmailIndex' AND object_id=OBJECT_ID(N'dbo.WebUsers')) BEGIN
CREATE INDEX [EmailIndex] ON [WebUsers] ([NormalizedEmail]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebUsers_CustomerProfileId' AND object_id=OBJECT_ID(N'dbo.WebUsers')) BEGIN
CREATE INDEX [IX_WebUsers_CustomerProfileId] ON [WebUsers] ([CustomerProfileId]);
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'UserNameIndex' AND object_id=OBJECT_ID(N'dbo.WebUsers')) BEGIN
CREATE UNIQUE INDEX [UserNameIndex] ON [WebUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name=N'IX_WebVouchers_Code' AND object_id=OBJECT_ID(N'dbo.WebVouchers')) BEGIN
CREATE UNIQUE INDEX [IX_WebVouchers_Code] ON [WebVouchers] ([Code]);
END;
GO
IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE name=N'CK_WebBatchReservation')
    EXEC(N'ALTER TABLE dbo.Batches WITH CHECK ADD CONSTRAINT CK_WebBatchReservation CHECK (ReservedQuantity>=0 AND Stock>=ReservedQuantity)');
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name=N'UX_WebCustomerUser' AND object_id=OBJECT_ID(N'dbo.Customers'))
    EXEC(N'CREATE UNIQUE INDEX UX_WebCustomerUser ON dbo.Customers(UserId) WHERE UserId IS NOT NULL');
IF NOT EXISTS(SELECT 1 FROM sys.foreign_key_columns f
    JOIN sys.columns c ON c.object_id=f.parent_object_id AND c.column_id=f.parent_column_id
    WHERE f.parent_object_id=OBJECT_ID(N'dbo.WebUsers') AND f.referenced_object_id=OBJECT_ID(N'dbo.Customers')
        AND c.name=N'CustomerProfileId')
    ALTER TABLE dbo.WebUsers WITH CHECK ADD CONSTRAINT FK_WebUsers_PosCustomer
        FOREIGN KEY(CustomerProfileId) REFERENCES dbo.Customers(Id);
IF COL_LENGTH(N'dbo.WebOrders', N'IsRefunded') IS NULL
    ALTER TABLE dbo.WebOrders ADD [IsRefunded] bit NOT NULL CONSTRAINT DF_WebOrders_IsRefunded DEFAULT 0;
IF COL_LENGTH(N'dbo.WebOrders', N'RefundAmount') IS NULL
    ALTER TABLE dbo.WebOrders ADD [RefundAmount] decimal(18,2) NOT NULL CONSTRAINT DF_WebOrders_RefundAmount DEFAULT 0;
IF COL_LENGTH(N'dbo.WebOrders', N'RefundedAt') IS NULL
    ALTER TABLE dbo.WebOrders ADD [RefundedAt] datetime2 NULL;
IF COL_LENGTH(N'dbo.WebOrders', N'RefundedByEmployee') IS NULL
    ALTER TABLE dbo.WebOrders ADD [RefundedByEmployee] nvarchar(100) NULL;
IF COL_LENGTH(N'dbo.WebOrders', N'RefundReason') IS NULL
    ALTER TABLE dbo.WebOrders ADD [RefundReason] nvarchar(500) NULL;
COMMIT TRANSACTION;
GO
