DROP PROCEDURE [dbo].[sp_userChangePass]
GO
CREATE PROCEDURE [dbo].[sp_userChangePass]
	@ID int,
	@password CHAR (64)
AS
	UPDATE Users
	SET password=@password
	WHERE Id=@ID