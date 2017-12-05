USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[cancel_order]    Script Date: 2017-10-27 17:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[cancel_order]
  @user_id AS bigint ,
  @order_id AS bigint 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
    BEGIN TRAN
    BEGIN TRY
        DECLARE @stock_id bigint, @type int, @undealed int, @canceled int, @price money
        SELECT @stock_id = stock_id, @type = type, @undealed = undealed, @canceled = canceled, @price = price FROM orders WHERE order_id = @order_id
        UPDATE orders SET canceled = @undealed, undealed = 0 WHERE order_id = @order_id
        IF @type = 0
        BEGIN
            UPDATE users SET cny_free = cny_free + @price * @undealed, cny_freezed = cny_freezed - @price * @undealed WHERE user_id = @user_id
        END
        ELSE
        BEGIN
            UPDATE user_positions SET num_free = num_free + @undealed, num_freezed = num_freezed - @undealed WHERE user_id = @user_id AND stock_id = @stock_id
        END
        COMMIT
	    RETURN 0
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()
        ROLLBACK TRAN
        RETURN -1
    END CATCH
END