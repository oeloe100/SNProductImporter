CREATE TABLE [dbo].[EDCMappings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userId] NVARCHAR(50) NOT NULL,
    [shopCategory] NVARCHAR(50) NOT NULL, 
    [supplierCategory] NVARCHAR(50) NOT NULL, 
    [shopCategoryId] NVARCHAR(50) NOT NULL, 
    [SupplierCategoryId] NVARCHAR(50) NOT NULL
)
