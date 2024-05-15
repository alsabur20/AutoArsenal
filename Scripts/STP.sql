--> Store Procedures

--> Add Person
CREATE PROCEDURE sp_AddPerson
    -- Add the parameters for the stored procedure here
    @FirstName varchar(50),
    @LastName varchar(50),
    @Contact varchar(20),
    @Gender int,
    @Status int,
    @StreetAddress varchar(50),
    @Country varchar(50),
    @City varchar(50),
    @Province varchar(50),
    @InsertedId int OUTPUT -- Add an output parameter to return the inserted ID
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statement for procedure here
    INSERT INTO PERSON (FirstName, LastName, Contact, Gender, Status, StreetAddress, Country, City, Province)
    VALUES (@FirstName, @LastName, @Contact, @Gender, @Status, @StreetAddress, @Country, @City, @Province);

    -- Set the output parameter to the inserted ID
    SET @InsertedId = SCOPE_IDENTITY();
END
GO


--> Update Person
CREATE PROCEDURE sp_UpdatePerson
	-- Add the parameters for the stored procedure here
	@Id int,
	@FirstName varchar(50),
	@LastName varchar(50),
	@Contact varchar(20),
	@Gender int,
	@Status int,
	@StreetAddress varchar(50),
	@Country varchar(50),
	@City varchar(50),
	@Province varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Person SET FirstName = @FirstName, LastName = @LastName, Contact = @Contact, Gender = @Gender, Status = @Status, StreetAddress = @StreetAddress, Country = @Country, City = @City, Province = @Province WHERE Id = @Id;
END
GO

--> Delete Person
CREATE PROCEDURE sp_DeletePerson
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Person SET Status = 9 WHERE Id = @Id;
END
GO

--> Update Employee
CREATE PROCEDURE sp_UpdateEmployee
	-- Add the parameters for the stored procedure here
	@Id int,
	@JoiningDate datetime,
	@Role int,
	@Salary float,
	@CredentialsId int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Employee SET JoiningDate = @JoiningDate, Role = @Role, CredentialsId= @CredentialsId, Salary = @Salary WHERE Id = @Id;
END
GO

--> Add Employee
CREATE PROCEDURE sp_AddEmployee
    -- Add the parameters for the stored procedure here
    @FirstName varchar(50),
    @LastName varchar(50),
    @Contact varchar(20),
    @Gender int,
    @JoiningDate datetime,
    @Role int,
    @Salary float,
    @StreetAddress varchar(50),
    @Country varchar(50),
    @City varchar(50),
    @Province varchar(50),
    @Status int = 8,
    @CredentialsId int = NULL,
    @InsertedId int OUTPUT -- Add an output parameter to return the inserted ID
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insert into PERSON table with the appropriate isolation level
        DECLARE @PersonId int;
        EXEC sp_AddPerson @FirstName, @LastName, @Contact, @Gender, @Status, @StreetAddress, @Country, @City, @Province, @PersonId OUTPUT;

        -- Insert into Employee table
        INSERT INTO Employee(Id, JoiningDate, Role, Salary, CredentialsId)
        VALUES (@PersonId, @JoiningDate, @Role, @Salary, @CredentialsId);

        -- Set the output parameter to the inserted employee's ID
        SET @InsertedId = @PersonId;

        -- Commit the transaction if everything is successful
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction if there's an error
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Throw the error to the caller
        THROW;
    END CATCH;
END
GO


--> Add Warehouse
CREATE PROCEDURE sp_AddWarehouse
	-- Add the parameters for the stored procedure here
	@Name varchar(50),
	@StreetAddress varchar(50),
	@Country varchar(50),
	@City varchar(50),
	@Province varchar(50),
	@Status int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Warehouse (Name, StreetAddress, Country, City, Province, Status) VALUES (@Name, @StreetAddress, @Country, @City, @Province, @Status);
END
GO

--> Update Warehouse
CREATE PROCEDURE sp_UpdateWarehouse
-- Add the parameters for the stored procedure here
	@Id int,
	@Name varchar(50),
	@StreetAddress varchar(50),
	@Country varchar(50),
	@City varchar(50),
	@Province varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Warehouse SET Name = @Name, StreetAddress = @StreetAddress, Country = @Country, City = @City, Province = @Province WHERE Id = @Id;
END
GO

--> Delete Warehouse
CREATE PROCEDURE sp_DeleteWarehouse
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Warehouse SET Status = 9 WHERE Id = @Id;
END
GO

--> Add Inventory
CREATE PROCEDURE sp_AddInventory
    @StockInShop int = NULL,
    @StockInWarehouse int = NULL,
    @WarehouseId int = NULL,
    @Id int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Inventory (StockInShop, StockInWarehouse, WarehouseId)
    VALUES (CASE WHEN @StockInShop IS NULL THEN NULL ELSE @StockInShop END,
            CASE WHEN @StockInWarehouse IS NULL THEN NULL ELSE @StockInWarehouse END,
            CASE WHEN @WarehouseId IS NULL THEN NULL ELSE @WarehouseId END);

    -- Set the OUTPUT parameter to the inserted Id
    SET @Id = SCOPE_IDENTITY();
END
GO

--> Update Inventory
CREATE PROCEDURE sp_UpdateInventory
    @Id int,
    @StockInShop int,
    @StockInWarehouse int,
    @WarehouseId int
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Inventory 
	SET StockInShop = @StockInShop, StockInWarehouse = @StockInWarehouse, WarehouseId = @WarehouseId 
	WHERE Id = @Id;
END
GO

--> Get Inventory By Id
CREATE PROCEDURE sp_GetInventoryById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        ISNULL(StockInShop, -1) AS StockInShop,
        ISNULL(StockInWarehouse, -1) AS StockInWarehouse,
        ISNULL(WarehouseId, -1) AS WarehouseId
    FROM 
        Inventory
    WHERE 
        Id = @Id;
END
GO

--> Add Customer
CREATE PROCEDURE sp_AddCustomer
    -- Add the parameters for the stored procedure here
    @FirstName varchar(50),
    @LastName varchar(50),
    @Contact varchar(20),
    @Gender int,
    @StreetAddress varchar(50),
    @Country varchar(50),
    @City varchar(50),
    @Province varchar(50),
    @IsTrustworthy bit,
    @Discount decimal(18,2),
    @Credit decimal(18,2)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    BEGIN TRY
    DECLARE @InsertedId int 
        -- Call sp_AddPerson to insert the person record
        EXEC sp_AddPerson 
            @FirstName, 
            @LastName, 
            @Contact, 
            @Gender, 
            8, -- Assuming default status is 8 for a customer
            @StreetAddress, 
            @Country, 
            @City, 
            @Province, 
            @InsertedId OUTPUT;

        -- Insert into Customer table
        INSERT INTO Customer (Id, IsTrustworthy, Discount, Credit)
        VALUES (@InsertedId, @IsTrustworthy, @Discount, @Credit);
    END TRY
    BEGIN CATCH
        -- Rollback the transaction if there's an error
		IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        -- If an error occurs, throw the error to the caller
        THROW;
    END CATCH;
END
GO

--> Update Sale Status
CREATE PROCEDURE dbo.UpdateSaleStatus
AS
BEGIN
    SET NOCOUNT ON;

    -- Update Sale status
    UPDATE s
    SET s.IsDeleted = 1 -- Assuming IsDeleted is the status column
    FROM [AutoArsenal].[dbo].[Sale] s
    WHERE NOT EXISTS (
        SELECT 1
        FROM [AutoArsenal].[dbo].[SaleDetails] sd
        WHERE sd.SaleId = s.Id
        AND sd.Quantity <> 0
    )

END
GO
