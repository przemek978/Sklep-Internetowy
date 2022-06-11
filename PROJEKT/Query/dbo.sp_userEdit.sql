DROP PROCEDURE [dbo].[sp_userEdit]
GO
CREATE PROCEDURE [dbo].[sp_userEdit]
	@ID int,
	@userName CHAR (64),
	@email	  CHAR (64),
	@typeID	  CHAR (64),
	@active BIT
AS
	UPDATE Users
	SET userName=@userName, email=@email,typeID=@typeID, active=@active
	WHERE Id=@ID