/*
 Navicat Premium Data Transfer

 Source Server         : gp-local
 Source Server Type    : SQL Server
 Source Server Version : 13004001
 Source Host           : localhost:1433
 Source Catalog        : gp
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13004001
 File Encoding         : 65001

 Date: 29/12/2017 17:34:39
*/


-- ----------------------------
-- Table structure for orders
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[orders]') AND type IN ('U'))
	DROP TABLE [dbo].[orders]
GO

CREATE TABLE [dbo].[orders] (
  [order_id] bigint  IDENTITY(1,1) NOT NULL,
  [create_datetime] datetime  NOT NULL,
  [user_id] bigint  NOT NULL,
  [stock_id] int  NOT NULL,
  [type] int  NOT NULL,
  [price] money  NOT NULL,
  [undealed] int  NOT NULL,
  [dealed] int  NOT NULL,
  [canceled] int  NOT NULL
)
GO

ALTER TABLE [dbo].[orders] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单id',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'order_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单创建时间',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'create_datetime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户id',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'user_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'股票id',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'stock_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单类型：
0为买入

1为卖出',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单价格',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'未成交数量',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'undealed'
GO

EXEC sp_addextendedproperty
'MS_Description', N'已成交数量',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'dealed'
GO

EXEC sp_addextendedproperty
'MS_Description', N'取消数量',
'SCHEMA', N'dbo',
'TABLE', N'orders',
'COLUMN', N'canceled'
GO


-- ----------------------------
-- Table structure for stocks
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[stocks]') AND type IN ('U'))
	DROP TABLE [dbo].[stocks]
GO

CREATE TABLE [dbo].[stocks] (
  [stock_id] int  IDENTITY(100000,1) NOT NULL,
  [name] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [price] money  NOT NULL
)
GO

ALTER TABLE [dbo].[stocks] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'股票id',
'SCHEMA', N'dbo',
'TABLE', N'stocks',
'COLUMN', N'stock_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'股票名称',
'SCHEMA', N'dbo',
'TABLE', N'stocks',
'COLUMN', N'name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'股票价格',
'SCHEMA', N'dbo',
'TABLE', N'stocks',
'COLUMN', N'price'
GO


-- ----------------------------
-- Table structure for transactions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[transactions]') AND type IN ('U'))
	DROP TABLE [dbo].[transactions]
GO

CREATE TABLE [dbo].[transactions] (
  [trans_id] bigint  IDENTITY(1,1) NOT NULL,
  [create_datetime] datetime  NOT NULL,
  [buy_order_id] bigint  NOT NULL,
  [sell_order_id] bigint  NOT NULL,
  [dealed] int  NOT NULL,
  [stock_id] int  NOT NULL,
  [deal_price] money  NOT NULL,
  [type] int  NOT NULL
)
GO

ALTER TABLE [dbo].[transactions] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交id',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'trans_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交创建时间',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'create_datetime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'买入订单id',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'buy_order_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'卖出订单id',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'sell_order_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交数量',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'dealed'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交股票id',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'stock_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交价格',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'deal_price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成交类型',
'SCHEMA', N'dbo',
'TABLE', N'transactions',
'COLUMN', N'type'
GO


-- ----------------------------
-- Table structure for user_positions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_positions]') AND type IN ('U'))
	DROP TABLE [dbo].[user_positions]
GO

CREATE TABLE [dbo].[user_positions] (
  [user_id] bigint  NOT NULL,
  [stock_id] int  NOT NULL,
  [num_free] int  NOT NULL,
  [num_freezed] int  NOT NULL
)
GO

ALTER TABLE [dbo].[user_positions] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户id',
'SCHEMA', N'dbo',
'TABLE', N'user_positions',
'COLUMN', N'user_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'股票id',
'SCHEMA', N'dbo',
'TABLE', N'user_positions',
'COLUMN', N'stock_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'具有的可用股票数量',
'SCHEMA', N'dbo',
'TABLE', N'user_positions',
'COLUMN', N'num_free'
GO

EXEC sp_addextendedproperty
'MS_Description', N'冻结的股票数量',
'SCHEMA', N'dbo',
'TABLE', N'user_positions',
'COLUMN', N'num_freezed'
GO


-- ----------------------------
-- Table structure for users
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type IN ('U'))
	DROP TABLE [dbo].[users]
GO

CREATE TABLE [dbo].[users] (
  [user_id] bigint  IDENTITY(10000000,1) NOT NULL,
  [name] nvarchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [login_name] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [passwd] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [type] int  NOT NULL,
  [cny_free] money  NOT NULL,
  [cny_freezed] money  NOT NULL
)
GO

ALTER TABLE [dbo].[users] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户的id',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'user_id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户姓名',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户登录名',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'login_name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录密码',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'passwd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户类型：

0为普通用户

1为系统管理员',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'type'
GO

EXEC sp_addextendedproperty
'MS_Description', N'具有的可用人民币数量',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'cny_free'
GO

EXEC sp_addextendedproperty
'MS_Description', N'冻结的人民币数量',
'SCHEMA', N'dbo',
'TABLE', N'users',
'COLUMN', N'cny_freezed'
GO


-- ----------------------------
-- Procedure structure for exec_order
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[exec_order]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[exec_order]
GO

CREATE PROCEDURE [dbo].[exec_order]
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
GO


-- ----------------------------
-- Procedure structure for cancel_order
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[cancel_order]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[cancel_order]
GO

CREATE PROCEDURE [dbo].[cancel_order]
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
	    SELECT 0
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()
        ROLLBACK TRAN
        SELECT -1
    END CATCH
END
GO


-- ----------------------------
-- Procedure structure for user_stock
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_stock]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_stock]
GO

CREATE PROCEDURE [dbo].[user_stock]
  @user_id AS bigint 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	SELECT stock_id, num_free, num_freezed FROM user_positions WHERE @user_id = user_id ORDER BY stock_id
END
GO


-- ----------------------------
-- Procedure structure for user_cny
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_cny]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_cny]
GO

CREATE PROCEDURE [dbo].[user_cny]
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
GO


-- ----------------------------
-- Procedure structure for user_order
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_order]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_order]
GO

CREATE PROCEDURE [dbo].[user_order]
  @user_id AS bigint 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	SELECT * FROM orders WHERE user_id = @user_id
END
GO


-- ----------------------------
-- Procedure structure for stock_depth
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[stock_depth]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[stock_depth]
GO

CREATE PROCEDURE [dbo].[stock_depth]
  @stock_id AS int ,
  @type AS int 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
    SELECT price, num = SUM(undealed) FROM orders WHERE stock_id = @stock_id AND type = @type AND undealed > 0 GROUP BY price ORDER BY price, num
END
GO


-- ----------------------------
-- Procedure structure for user_login
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_login]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_login]
GO

CREATE PROCEDURE [dbo].[user_login]
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
        RETURN 0
    END
    ELSE
        RETURN -1
END
GO


-- ----------------------------
-- Procedure structure for user_create
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_create]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_create]
GO

CREATE PROCEDURE [dbo].[user_create]
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
GO


-- ----------------------------
-- Procedure structure for user_repasswd
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[user_repasswd]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[user_repasswd]
GO

CREATE PROCEDURE [dbo].[user_repasswd]
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
GO


-- ----------------------------
-- Triggers structure for table orders
-- ----------------------------
CREATE TRIGGER [dbo].[make_transactions]
ON [dbo].[orders]
WITH EXECUTE AS CALLER
FOR INSERT
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
GO


-- ----------------------------
-- Primary Key structure for table orders
-- ----------------------------
ALTER TABLE [dbo].[orders] ADD CONSTRAINT [PK__orders__465962292BAC4855] PRIMARY KEY CLUSTERED ([order_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table stocks
-- ----------------------------
ALTER TABLE [dbo].[stocks] ADD CONSTRAINT [PK__stocks__E86668628588C636] PRIMARY KEY CLUSTERED ([stock_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table transactions
-- ----------------------------
ALTER TABLE [dbo].[transactions] ADD CONSTRAINT [PK__transact__438CAC181F7B8BBA] PRIMARY KEY CLUSTERED ([trans_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table user_positions
-- ----------------------------
ALTER TABLE [dbo].[user_positions] ADD CONSTRAINT [PK__user_pos__87385189C0797B3B] PRIMARY KEY CLUSTERED ([user_id], [stock_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE [dbo].[users] ADD CONSTRAINT [PK__users__B9BE370FB100A511] PRIMARY KEY CLUSTERED ([user_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table orders
-- ----------------------------
ALTER TABLE [dbo].[orders] ADD CONSTRAINT [FK_orders_users] FOREIGN KEY ([user_id]) REFERENCES [users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[orders] ADD CONSTRAINT [FK_orders_stocks] FOREIGN KEY ([stock_id]) REFERENCES [stocks] ([stock_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table transactions
-- ----------------------------
ALTER TABLE [dbo].[transactions] ADD CONSTRAINT [FK_transactions_orders_buy] FOREIGN KEY ([buy_order_id]) REFERENCES [orders] ([order_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[transactions] ADD CONSTRAINT [FK_transactions_orders_sell] FOREIGN KEY ([sell_order_id]) REFERENCES [orders] ([order_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[transactions] ADD CONSTRAINT [FK_transactions_stocks] FOREIGN KEY ([stock_id]) REFERENCES [stocks] ([stock_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table user_positions
-- ----------------------------
ALTER TABLE [dbo].[user_positions] ADD CONSTRAINT [FK_user_positions_users] FOREIGN KEY ([user_id]) REFERENCES [users] ([user_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[user_positions] ADD CONSTRAINT [FK_user_positions_stocks] FOREIGN KEY ([stock_id]) REFERENCES [stocks] ([stock_id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

