CREATE TABLE [dbo].[NopTokens]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AccessToken] NVARCHAR(500) NULL, 
    [RefreshToken] NVARCHAR(500) NULL 
)
