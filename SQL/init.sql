CREATE TABLE Users (
   id INT IDENTITY(1,1) NOT NULL,
   [name] NVARCHAR(100) NOT NULL,
   email NVARCHAR(250) NOT NULL,
   [password] NVARCHAR(255) NOT NULL,
   CONSTRAINT PK_id PRIMARY KEY (id),
   CONSTRAINT UQ_email UNIQUE (email)
);

CREATE PROCEDURE [dbo].[sp_CreateUser]
   @name NVARCHAR(100),
   @email NVARCHAR(250),
   @password NVARCHAR(255)
AS
BEGIN
	INSERT INTO Users (name, email, password)
    VALUES (@name, @email, @password);
END;

ALTER PROCEDURE [dbo].[sp_CreateUser]
   @name NVARCHAR(100),
   @email NVARCHAR(250),
   @password NVARCHAR(255),
   @id int OUTPUT
AS
BEGIN
	INSERT INTO Users (name, email, password)
    VALUES (@name, @email, @password);

	Set @id= SCOPE_IDENTITY();
END;