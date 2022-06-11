DROP PROCEDURE [dbo].[sp_userBan]
GO
CREATE PROCEDURE [dbo].[sp_userBan]
	@ID int,
	@active BIT
AS
	UPDATE Users
	SET active=@active
	WHERE Id=@ID