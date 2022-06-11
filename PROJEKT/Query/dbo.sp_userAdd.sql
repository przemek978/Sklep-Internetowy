DROP PROCEDURE [dbo].[sp_userAdd];
GO
CREATE PROCEDURE [dbo].[sp_userAdd]
	@userName CHAR (64),
	@email	  CHAR (64),
	@password CHAR (64),
	@typeID	  CHAR (64),
	@active BIT
AS
	INSERT INTO Users (userName,email, password,typeID,active) VALUES (@userName,@email,@password,@typeID,@active)