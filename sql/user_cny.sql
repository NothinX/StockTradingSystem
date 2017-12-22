USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_cny]    Script Date: 2017-11-03 15:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_cny]
  @user_id AS bigint ,
  @cny_free AS money OUTPUT ,
  @cny_freezed AS money OUTPUT ,
  @gp_money AS money OUTPUT
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	SELECT @cny_free = cny_free FROM users WHERE @user_id = user_id
	SELECT @cny_freezed = cny_freezed FROM users WHERE @user_id = user_id

	DECLARE @stock_id INT, @num_free INT, @num_freezed INT, @price money
	DECLARE	@t TABLE(stock_id INT, num_free INT, num_freezed INT)
	set @price = 0
    set @gp_money = 0

	INSERT INTO @t EXEC user_stock @user_id
	DECLARE tt CURSOR FOR SELECT * FROM @t
	OPEN tt
	WHILE 1 > 0
	BEGIN
		FETCH NEXT FROM tt INTO @stock_id, @num_free, @num_freezed
        IF @@FETCH_STATUS != 0 BREAK
        SELECT @price = price FROM stocks WHERE @stock_id = stock_id
		SET @gp_money = @gp_money + @price * (@num_free + @num_freezed)
	END
	CLOSE tt
	DEALLOCATE tt
END