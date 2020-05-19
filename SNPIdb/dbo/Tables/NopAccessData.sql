CREATE TABLE [dbo].[NopAccessData]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[clientId] NVARCHAR(50) NOT NULL, 
    [clientSecret] NVARCHAR(50) NOT NULL, 
    [serverUrl] NVARCHAR(50) NOT NULL, 
    [redirectUrl] NVARCHAR(50) NOT NULL, 
)
