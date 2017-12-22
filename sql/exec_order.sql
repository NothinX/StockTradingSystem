USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[exec_order]    Script Date: 2017-10-27 17:04:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[exec_order]
    @user_id AS bigint ,
    @stock_id AS INT ,
    @type AS INT ,
    @price AS money ,
    @amount AS INT
AS
BEGIN
    -- routine body goes here, e.g.
    -- SELECT 'Navicat for SQL Server'
    BEGIN TRAN
    BEGIN TRY
	    IF @type = 0
        BEGIN
            IF (SELECT cny_free FROM users WHERE user_id = @user_id) >= @price * @amount
            BEGIN
                UPDATE users SET cny_free = cny_free - @price * @amount, cny_freezed = cny_freezed + @price * @amount WHERE user_id = @user_id
            END
            ELSE
            BEGIN
                COMMIT TRAN
                SELECT -1
            END
        END
        ELSE
        BEGIN
            IF (SELECT num_free FROM user_positions WHERE user_id = @user_id AND stock_id = @stock_id) >= @amount
            BEGIN
                UPDATE user_positions SET num_free = num_free - @amount, num_freezed = num_freezed + @amount WHERE user_id = @user_id AND stock_id = @stock_id
            END
            ELSE
            BEGIN
                COMMIT TRAN
                SELECT -2
            END
        END
        INSERT INTO orders VALUES(GETDATE(), @user_id, @stock_id, @type, @price, @amount, 0, 0)
        COMMIT TRAN
	    SELECT 0
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()
        ROLLBACK TRAN
        SELECT -3
    END CATCH
END