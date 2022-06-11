DROP PROCEDURE [dbo].[sp_userDel]
GO
CREATE PROCEDURE [dbo].[sp_userDel]
	@ID int
AS
	DELETE FROM Users
	WHERE Id=@ID