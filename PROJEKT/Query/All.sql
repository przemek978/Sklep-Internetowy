DROP PROCEDURE [dbo].[sp_userAdd]
DROP PROCEDURE [dbo].[sp_userEdit]
DROP PROCEDURE [dbo].[sp_userDel]
DROP PROCEDURE [dbo].[sp_userChangePass]
DROP PROCEDURE [dbo].[sp_userDetails]
DROP PROCEDURE [dbo].[sp_userBan]

GO
CREATE PROCEDURE [dbo].[sp_userAdd]
	@userName CHAR (64),
	@email	  CHAR (64),
	@password CHAR (64),
	@typeID	  CHAR (64),
	@active BIT
AS
	INSERT INTO Users (userName,email, password,typeID,active) VALUES (@userName,@email,@password,@typeID,@active)
go	
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
go
CREATE PROCEDURE [dbo].[sp_userDel]
	@ID int
AS
	DELETE FROM Users
	WHERE Id=@ID
GO
CREATE PROCEDURE [dbo].[sp_userChangePass]
	@ID int,
	@password CHAR (64)
AS
	UPDATE Users
	SET password=@password
	WHERE Id=@ID
GO
CREATE PROCEDURE [dbo].[sp_userBan]
	@ID int,
	@active BIT
AS
	UPDATE Users
	SET active=@active
	WHERE Id=@ID
GO
CREATE PROCEDURE [dbo].[sp_userDetails]
AS
	SELECT userName, password, active FROM Users
GO	
CREATE PROCEDURE [dbo].[sp_productList]
AS
SELECT * FROM Product