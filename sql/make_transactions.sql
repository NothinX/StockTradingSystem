/*    ==脚本参数==

    源服务器版本 : SQL Server 2016 (13.0.4001)
    源数据库引擎版本 : Microsoft SQL Server Express Edition
    源数据库引擎类型 : 独立的 SQL Server

    目标服务器版本 : SQL Server 2017
    目标数据库引擎版本 : Microsoft SQL Server Standard Edition
    目标数据库引擎类型 : 独立的 SQL Server
*/

USE [gp]
GO
/****** Object:  Trigger [dbo].[make_transactions]    Script Date: 2017-09-29 17:38:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[make_transactions]
ON [dbo].[orders]
WITH EXECUTE AS CALLER
AFTER INSERT
AS
BEGIN
    -- Type the SQL Here.
    DECLARE @order_id bigint, @user_id bigint, @stock_id INT,
        @type INT, @price money, @undealed INT, @dealed INT, @canceled INT
	DECLARE @temp_order_id bigint, @temp_order_undealed INT,
            @temp_order_dealed INT, @temp_price MONEY, @temp_user_id bigint
    SELECT @order_id = order_id, @user_id = user_id, @stock_id = stock_id,
        @type = type, @price = price, @undealed = undealed,
        @dealed = dealed, @canceled = canceled
    FROM inserted

	IF @type = 0
	BEGIN
		DECLARE temp_orders CURSOR FOR
            SELECT user_id, price, order_id, undealed, dealed
        FROM orders
        WHERE type = 1 AND stock_id = @stock_id AND
            price <= @price AND undealed > 0
        ORDER BY price DESC, order_id
        OPEN temp_orders
	END
	ELSE
	BEGIN
        DECLARE temp_orders CURSOR FOR
            SELECT user_id, price, order_id, undealed, dealed
        FROM orders
        WHERE type = 0 AND stock_id = @stock_id AND
            price >= @price AND undealed > 0
        ORDER BY price, order_id
        OPEN temp_orders
	END

    BEGIN TRAN
    BEGIN TRY

	WHILE @undealed > 0
    BEGIN
        FETCH NEXT FROM temp_orders INTO
            @temp_user_id, @temp_price, @temp_order_id, @temp_order_undealed, @temp_order_dealed
        IF @@FETCH_STATUS != 0 BREAK
		DECLARE @temp_deal INT
		SET @temp_deal = @undealed - @temp_order_undealed
		IF @temp_deal < 0
			SET @temp_deal = @undealed
		ELSE
			SET @temp_deal = @temp_order_undealed
		SET @dealed = @dealed + @temp_deal
		SET @undealed = @undealed - @temp_deal
		SET @temp_order_dealed = @temp_order_dealed + @temp_deal
		SET @temp_order_undealed = @temp_order_undealed - @temp_deal

		IF @type = 0
		BEGIN
			UPDATE orders SET dealed = @temp_order_dealed, undealed = @temp_order_undealed
				WHERE order_id = @temp_order_id
            INSERT INTO transactions VALUES(GETDATE(), @order_id, @temp_order_id, @temp_deal, @stock_id, @price, @type)
            UPDATE stocks SET price = @price WHERE stock_id = @stock_id

            IF EXISTS(SELECT * FROM user_positions WHERE user_id = @user_id AND stock_id = @stock_id)
		        UPDATE user_positions SET num_free = num_free + @temp_deal
                    WHERE user_id = @user_id AND stock_id = @stock_id
            ELSE BEGIN
                INSERT user_positions
                VALUES(@user_id, @stock_id, @temp_deal, 0)
            END
            UPDATE users SET cny_freezed = cny_freezed - @temp_deal * @price
                WHERE user_id = @user_id
            UPDATE users SET cny_free = cny_free + @temp_deal * @price
                WHERE user_id = @temp_user_id
            UPDATE user_positions SET num_freezed = num_freezed - @temp_deal
                WHERE user_id = @temp_user_id AND stock_id = @stock_id
		END
		ELSE
		BEGIN
			UPDATE orders SET dealed = @temp_order_dealed, undealed = @temp_order_undealed
                WHERE order_id = @temp_order_id
            INSERT INTO transactions VALUES(GETDATE(), @temp_order_id, @order_id, @temp_deal, @stock_id, @temp_price, @type)
            UPDATE stocks SET price = @temp_price WHERE stock_id = @stock_id

            IF EXISTS(SELECT * FROM user_positions WHERE user_id = @temp_user_id AND stock_id = @stock_id)
		        UPDATE user_positions SET num_free=num_free + @temp_deal
                    WHERE user_id = @temp_user_id AND stock_id = @stock_id
            ELSE
                INSERT user_positions
                VALUES(@temp_user_id, @stock_id, @temp_deal, 0)
            UPDATE users SET cny_free = cny_free + @temp_deal * (@temp_price - @price), cny_freezed = cny_freezed - @temp_deal * @temp_price
                WHERE user_id = @temp_user_id
            UPDATE users SET cny_free = cny_free + @temp_deal * @price
                WHERE user_id = @user_id
            UPDATE user_positions SET num_freezed = num_freezed - @temp_deal
                WHERE user_id = @user_id AND stock_id = @stock_id
		END
	END

	UPDATE orders SET dealed = @dealed, undealed = @undealed
        WHERE order_id = @order_id
    CLOSE temp_orders
    DEALLOCATE temp_orders

    COMMIT TRAN
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()
    END CATCH
END