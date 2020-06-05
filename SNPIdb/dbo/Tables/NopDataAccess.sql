CREATE TABLE [dbo].[dataAccess]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userId] NVARCHAR(50) NOT NULL, 
    [clientId] NVARCHAR(50) NOT NULL, 
    [clientSecret] NVARCHAR(50) NOT NULL, 
    [serverUrl] NVARCHAR(50) NOT NULL, 
    [redirectUrl] NVARCHAR(50) NOT NULL
)
