CREATE TABLE [dbo].[Categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](100) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_category_id] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_isActive]  DEFAULT ((1)) FOR [isActive]
GO


CREATE TABLE [dbo].[Budgets](
	[budget_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[amount] [decimal](10, 2) NOT NULL,
	[month] [int] NOT NULL,
	[year] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_budget_id] PRIMARY KEY CLUSTERED 
(
	[budget_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Budgets] ADD  CONSTRAINT [DF_Budgets_isActive]  DEFAULT ((1)) FOR [isActive]
GO

ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO

ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO


CREATE PROCEDURE sp_Budgets
    @sp_Operation NVARCHAR(10),
    @budget_id INT = NULL,
    @user_id INT = NULL,
    @category_id INT = NULL,
    @amount DECIMAL(10,2) = NULL,
    @month INT = NULL,
    @year INT = NULL
AS
BEGIN
    IF @sp_Operation = 'Add'
    BEGIN
        INSERT INTO Budgets (user_id, category_id, amount, month, year)
        VALUES (@user_id, @category_id, @amount, @month, @year);
    END
    ELSE IF @sp_Operation = 'Update'
    BEGIN
        UPDATE Budgets
        SET amount = @amount,
            month = @month,
            year = @year
        WHERE budget_id = @budget_id;
    END
    ELSE IF @sp_Operation = 'Delete'
    BEGIN
         UPDATE Budgets
		 set isActive= 0
        WHERE budget_id = @budget_id;
    END
END;


CREATE VIEW v_Budgets AS
SELECT 
    b.budget_id,
    u.name AS user_name,
    c.category_name,
    b.amount,
    b.month,
    b.year, 
    b.isActive
FROM Budgets b
JOIN Users u ON b.user_id = u.id
JOIN Categories c ON b.category_id = c.category_id;