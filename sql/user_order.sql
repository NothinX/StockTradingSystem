USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_order]    Script Date: 2017-11-03 16:27:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_order]
  @user_id AS bigint 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	SELECT * FROM orders WHERE user_id = @user_id
END