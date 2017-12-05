USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[stock_depth]    Script Date: 2017-11-03 16:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[stock_depth]
  @stock_id AS int ,
  @type AS int 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
    SELECT price, num = SUM(undealed) FROM orders WHERE stock_id = @stock_id AND type = @type AND undealed > 0 GROUP BY price ORDER BY price, num
END