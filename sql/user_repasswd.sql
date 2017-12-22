USE [gp]
GO
/****** Object:  StoredProcedure [dbo].[user_repasswd]    Script Date: 2017-11-03 17:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[user_repasswd]
  @user_id AS bigint ,
  @old_passwd AS varchar(255) ,
  @new_passwd AS varchar(255)
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	IF EXISTS(SELECT * FROM users WHERE user_id = @user_id AND passwd = @old_passwd)
	BEGIN
		UPDATE users SET passwd = @new_passwd WHERE user_id = @user_id
		SELECT 0
	END
	ELSE
		SELECT -1
END