CREATE TABLE [dbo].[EDCMappings]
(
	[Id] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [shopCategory] NVARCHAR(50) NOT NULL, 
    [supplierCategory] NVARCHAR(50) NOT NULL, 
    [shopCategoryId] NVARCHAR(50) NOT NULL, 
    [SupplierCategoryId] NVARCHAR(50) NOT NULL
)
