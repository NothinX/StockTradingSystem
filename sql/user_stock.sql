USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_stock]    Script Date: 2017-11-03 13:44:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_stock]
  @user_id AS bigint 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	SELECT stock_id, num_free, num_freezed FROM user_positions WHERE @user_id = user_id ORDER BY stock_id
END