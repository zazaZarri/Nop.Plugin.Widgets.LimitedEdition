-- Esegui su database nopCommerce (SQL Server)
-- Aggiunge colonne scarsità a LimitedTimeProduct se mancanti

IF COL_LENGTH('LimitedTimeProduct', 'InitialQuantity') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [InitialQuantity] INT NOT NULL CONSTRAINT DF_LTP_InitialQuantity DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'RemainingQuantity') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [RemainingQuantity] INT NOT NULL CONSTRAINT DF_LTP_RemainingQuantity DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'SoldCount') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [SoldCount] INT NOT NULL CONSTRAINT DF_LTP_SoldCount DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'ShowRemainingStock') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [ShowRemainingStock] BIT NOT NULL CONSTRAINT DF_LTP_ShowRemainingStock DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'ShowSoldCount') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [ShowSoldCount] BIT NOT NULL CONSTRAINT DF_LTP_ShowSoldCount DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'ShowProgressBar') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [ShowProgressBar] BIT NOT NULL CONSTRAINT DF_LTP_ShowProgressBar DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'ProgressBarMode') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [ProgressBarMode] INT NOT NULL CONSTRAINT DF_LTP_ProgressBarMode DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'DiscountPercentage') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [DiscountPercentage] DECIMAL(18,4) NOT NULL CONSTRAINT DF_LTP_DiscountPercentage DEFAULT(0);

IF COL_LENGTH('LimitedTimeProduct', 'BlockPurchaseWhenExpired') IS NULL
    ALTER TABLE [LimitedTimeProduct] ADD [BlockPurchaseWhenExpired] BIT NOT NULL CONSTRAINT DF_LTP_BlockPurchaseWhenExpired DEFAULT(1);

-- Tabella social proof se manca
IF OBJECT_ID(N'dbo.SocialProofEvent', N'U') IS NULL
BEGIN
    CREATE TABLE [SocialProofEvent] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ProductId] INT NOT NULL,
        [ProductName] NVARCHAR(400) NULL,
        [EventType] NVARCHAR(50) NULL,
        [CityOrRegion] NVARCHAR(100) NULL,
        [CreatedOnUtc] DATETIME2 NOT NULL
    );
END
GO
