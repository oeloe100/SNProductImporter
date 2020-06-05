CREATE TABLE [dbo].[NopTokens]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [userId] NVARCHAR(50) NOT NULL, 
    [accessToken] NVARCHAR(900) NOT NULL, 
    [refreshToken] NVARCHAR(900) NOT NULL, 
    [date] DATETIME NOT NULL, 
)
