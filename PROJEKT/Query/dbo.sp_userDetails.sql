DROP PROCEDURE [dbo].[sp_userDetails]
GO
CREATE PROCEDURE [dbo].[sp_userDetails]
AS
	SELECT userName, password, active FROM Users