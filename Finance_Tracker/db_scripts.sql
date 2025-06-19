---Table----

--CREATE TABLE Categories (
--    category_id INT PRIMARY KEY IDENTITY(1,1),
--    category_name NVARCHAR(100) NOT NULL
--);

--CREATE TABLE Users (
--    user_id INT PRIMARY KEY IDENTITY(1,1),
--    name NVARCHAR(100) NOT NULL,
--    salary DECIMAL(10, 2) NOT NULL
--);

--CREATE TABLE Budgets (
--    budget_id INT PRIMARY KEY IDENTITY(1,1),
--    user_id INT NOT NULL,
--    category_id INT NOT NULL,
--    amount DECIMAL(10, 2) NOT NULL,
--    month INT NOT NULL,
--    year INT NOT NULL,
--    FOREIGN KEY (user_id) REFERENCES Users(user_id),
--    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
--);

--CREATE TABLE Transactions (
--    transaction_id INT PRIMARY KEY IDENTITY(1,1),
--    user_id INT NOT NULL,
--    category_id INT NOT NULL,
--    amount DECIMAL(10, 2) NOT NULL,
--    description NVARCHAR(255),
--    transaction_date DATE NOT NULL,
--    FOREIGN KEY (user_id) REFERENCES Users(user_id),
--    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
--);

---Store Procedure---

--CREATE PROCEDURE sp_Categories
--    @sp_Operation NVARCHAR(10),
--    @category_id INT = NULL,
--    @category_name NVARCHAR(100) = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Categories (category_name)
--        VALUES (@category_name);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Categories
--        SET category_name = @category_name
--        WHERE category_id = @category_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        DELETE FROM Categories
--        WHERE category_id = @category_id;
--    END
--END;


CREATE PROCEDURE sp_Users
   @sp_Operation NVARCHAR(10),
   @user_id INT = NULL,
   @name NVARCHAR(100) = NULL,
   @salary DECIMAL(10,2) = NULL
AS
BEGIN
   IF @sp_Operation = 'Add'
   BEGIN
       INSERT INTO Users (name, salary)
       VALUES (@name, @salary);
   END
   ELSE IF @sp_Operation = 'Update'
   BEGIN
       UPDATE Users
       SET name = @name,
           salary = @salary
       WHERE user_id = @user_id;
   END
   ELSE IF @sp_Operation = 'Delete'
   BEGIN
       DELETE FROM Users
       WHERE user_id = @user_id;
   END
END;



--CREATE PROCEDURE sp_Budgets
--    @sp_Operation NVARCHAR(10),
--    @budget_id INT = NULL,
--    @user_id INT = NULL,
--    @category_id INT = NULL,
--    @amount DECIMAL(10,2) = NULL,
--    @month INT = NULL,
--    @year INT = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Budgets (user_id, category_id, amount, month, year)
--        VALUES (@user_id, @category_id, @amount, @month, @year);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Budgets
--        SET amount = @amount,
--            month = @month,
--            year = @year
--        WHERE budget_id = @budget_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        DELETE FROM Budgets
--        WHERE budget_id = @budget_id;
--    END
--END;


--CREATE PROCEDURE sp_Transactions
--    @sp_Operation NVARCHAR(10),
--    @transaction_id INT = NULL,
--    @user_id INT = NULL,
--    @category_id INT = NULL,
--    @amount DECIMAL(10,2) = NULL,
--    @description NVARCHAR(255) = NULL,
--    @transaction_date DATE = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Transactions (user_id, category_id, amount, description, transaction_date)
--        VALUES (@user_id, @category_id, @amount, @description, @transaction_date);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Transactions
--        SET amount = @amount,
--            description = @description,
--            transaction_date = @transaction_date
--        WHERE transaction_id = @transaction_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        DELETE FROM Transactions
--        WHERE transaction_id = @transaction_id;
--    END
--END;


---Views---

--CREATE VIEW v_Categories AS
--SELECT 
--    c.category_id,
--    c.category_name
--FROM Categories c;

--CREATE VIEW v_Users AS
--SELECT 
--    u.user_id,
--    u.name,
--    u.salary
--FROM Users u;

--CREATE VIEW v_Budgets AS
--SELECT 
--    b.budget_id,
--    u.name AS user_name,
--    c.category_name,
--    b.amount,
--    b.month,
--    b.year
--FROM Budgets b
--JOIN Users u ON b.user_id = u.user_id
--JOIN Categories c ON b.category_id = c.category_id;

--CREATE VIEW v_Transactions AS
--SELECT 
--    t.transaction_id,
--    u.name AS user_name,
--    c.category_name,
--    t.amount,
--    t.description,
--    t.transaction_date
--FROM Transactions t
--JOIN Users u ON t.user_id = u.user_id
--JOIN Categories c ON t.category_id = c.category_id;

---Modification--- 

--ALTER TABLE Users
--ADD IsActive BIT NOT NULL DEFAULT 1;

--ALTER TABLE Categories
--ADD IsActive BIT NOT NULL DEFAULT 1;

--ALTER TABLE Transactions
--ADD IsActive BIT NOT NULL DEFAULT 1;

--ALTER TABLE Budgets
--ADD IsActive BIT NOT NULL DEFAULT 1;


--ALTER PROCEDURE sp_Categories
--    @sp_Operation NVARCHAR(10),
--    @category_id INT = NULL,
--    @category_name NVARCHAR(100) = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Categories (category_name, IsActive)
--        VALUES (@category_name, 1);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Categories
--        SET category_name = @category_name
--        WHERE category_id = @category_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        UPDATE Categories
--        SET IsActive = 0
--        WHERE category_id = @category_id;
--    END
--END;
--GO


--ALTER PROCEDURE sp_Users
--    @sp_Operation NVARCHAR(10),
--    @user_id INT = NULL,
--    @name NVARCHAR(100) = NULL,
--    @salary DECIMAL(10,2) = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Users (name, salary, IsActive)
--        VALUES (@name, @salary, 1);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Users
--        SET name = @name,
--            salary = @salary
--        WHERE user_id = @user_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        UPDATE Users
--        SET IsActive = 0
--        WHERE user_id = @user_id;
--    END
--END;
--GO


--ALTER PROCEDURE sp_Budgets
--    @sp_Operation NVARCHAR(10),
--    @budget_id INT = NULL,
--    @user_id INT = NULL,
--    @category_id INT = NULL,
--    @amount DECIMAL(10,2) = NULL,
--    @month INT = NULL,
--    @year INT = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Budgets (user_id, category_id, amount, month, year, IsActive)
--        VALUES (@user_id, @category_id, @amount, @month, @year, 1);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Budgets
--        SET amount = @amount,
--            month = @month,
--            year = @year
--        WHERE budget_id = @budget_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        UPDATE Budgets
--        SET IsActive = 0
--        WHERE budget_id = @budget_id;
--    END
--END;
--GO


--ALTER PROCEDURE sp_Transactions
--    @sp_Operation NVARCHAR(10),
--    @transaction_id INT = NULL,
--    @user_id INT = NULL,
--    @category_id INT = NULL,
--    @amount DECIMAL(10,2) = NULL,
--    @description NVARCHAR(255) = NULL,
--    @transaction_date DATE = NULL
--AS
--BEGIN
--    IF @sp_Operation = 'Add'
--    BEGIN
--        INSERT INTO Transactions (user_id, category_id, amount, description, transaction_date, IsActive)
--        VALUES (@user_id, @category_id, @amount, @description, @transaction_date, 1);
--    END
--    ELSE IF @sp_Operation = 'Update'
--    BEGIN
--        UPDATE Transactions
--        SET amount = @amount,
--            description = @description,
--            transaction_date = @transaction_date
--        WHERE transaction_id = @transaction_id;
--    END
--    ELSE IF @sp_Operation = 'Delete'
--    BEGIN
--        UPDATE Transactions
--        SET IsActive = 0
--        WHERE transaction_id = @transaction_id;
--    END
--END;
--GO
