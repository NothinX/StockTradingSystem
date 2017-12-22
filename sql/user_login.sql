USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_login]    Script Date: 2017-11-03 16:49:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_login]
  @login_name AS varchar(255) ,
  @passwd AS varchar(255) ,
  @user_id AS bigint OUTPUT ,
  @name AS nvarchar(255) OUTPUT ,
  @type AS int OUTPUT
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
    IF EXISTS(SELECT * FROM users WHERE login_name = @login_name AND passwd = @passwd)
    BEGIN
        SELECT @user_id = user_id, @name = name, @type = type FROM users WHERE login_name = @login_name AND passwd = @passwd
        SELECT 0
    END
    ELSE
        SELECT -1
END