drop procedure [dbo].[sp_userAdd]

CREATE PROCEDURE [dbo].[sp_userAdd]
@userName CHAR (64),
@email	  CHAR (64),
@password CHAR (64),
@typeID	  CHAR (64)
AS
INSERT INTO Users (userName,email, password,typeID) VALUES (@userName,@email,@password,@typeID)