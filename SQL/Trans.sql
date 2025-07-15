USE [finance-dev]
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 15-07-2025 01:47:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions](
	[transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[amount] [decimal](10, 2) NOT NULL,
	[description] [nvarchar](255) NULL,
	[transaction_date] [date] NOT NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_transaction_id] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO

ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO


USE [finance-dev]
GO

/****** Object:  StoredProcedure [dbo].[sp_Transactions]    Script Date: 15-07-2025 01:47:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Transactions]
    @sp_Operation NVARCHAR(10),
    @transaction_id INT = NULL,
    @user_id INT = NULL,
    @category_id INT = NULL,
    @amount DECIMAL(10,2) = NULL,
    @description NVARCHAR(255) = NULL,
    @transaction_date DATE = NULL
AS
BEGIN
    IF @sp_Operation = 'Add'
    BEGIN
        INSERT INTO Transactions (user_id, category_id, amount, description, transaction_date)
        VALUES (@user_id, @category_id, @amount, @description, @transaction_date);
    END
    ELSE IF @sp_Operation = 'Update'
    BEGIN
        UPDATE Transactions
        SET amount = @amount,
            description = @description,
            transaction_date = @transaction_date
        WHERE transaction_id = @transaction_id;
    END
    ELSE IF @sp_Operation = 'Delete'
    BEGIN
      UPDATE Transactions
		 set isActive= 0
        WHERE transaction_id = @transaction_id;
    END
END;
GO


USE [finance-dev]
GO

/****** Object:  View [dbo].[v_Transactions]    Script Date: 15-07-2025 01:47:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_Transactions] AS
SELECT 
    t.transaction_id,
    u.name AS user_name,
    c.category_name,
    t.amount,
    t.description,
    t.transaction_date
FROM Transactions t
JOIN Users u ON t.user_id = u.id
JOIN Categories c ON t.category_id = c.category_id;
GO


