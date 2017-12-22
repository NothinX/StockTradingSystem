USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_create]    Script Date: 2017-11-03 17:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_create]
  @login_name AS varchar(255) ,
  @passwd AS varchar(255) ,
  @name AS nvarchar(255) ,
  @type AS int ,
  @cny_free AS money = 0
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
    IF EXISTS(SELECT * FROM users WHERE login_name = @login_name)
        SELECT -1
    ELSE
    BEGIN
        INSERT INTO users VALUES(@name, @login_name, @passwd, @type, @cny_free, 0)
        SELECT 0
    END
END