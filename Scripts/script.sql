USE [master]
GO
/****** Object:  Database [AutoArsenal]    Script Date: 4/26/2025 3:20:03 PM ******/
CREATE DATABASE [AutoArsenal]
GO
USE [AutoArsenal]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] NOT NULL,
	[IsTrustworthy] [bit] NULL,
	[Discount] [float] NULL,
	[Credit] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Customers]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Customers
CREATE VIEW [dbo].[View_Customers]
AS
SELECT * FROM Customer
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] NOT NULL,
	[JoiningDate] [datetime] NULL,
	[Role] [int] NULL,
	[CredentialsId] [int] NULL,
	[Salary] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Employees]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Employees
CREATE VIEW [dbo].[View_Employees]
AS
SELECT * FROM Employee
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StockInShop] [int] NULL,
	[StockInWarehouse] [int] NULL,
	[WarehouseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Inventory]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Inventory
CREATE VIEW [dbo].[View_Inventory]
AS
SELECT * FROM Inventory
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Products]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Products
CREATE VIEW [dbo].[View_Products]
AS
SELECT * FROM Product
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[CustomerId] [int] NULL,
	[DateOfSale] [datetime] NULL,
	[PaymentId] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Sales]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Sales
CREATE VIEW [dbo].[View_Sales]
AS
SELECT * FROM Sale
GO
/****** Object:  Table [dbo].[Credentials]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credentials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](24) NULL,
	[Password] [varchar](12) NULL,
	[Email] [varchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_Notification]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Notification](
	[Employee_Id] [int] NOT NULL,
	[Notification_EmployeeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC,
	[Notification_EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](max) NULL,
	[Value] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Contact] [varchar](20) NULL,
	[StreetAddress] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[ReceiveTime] [datetime] NULL,
	[Message] [varchar](max) NULL,
	[IsRead] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetails](
	[PaymentId] [int] NULL,
	[Amount] [float] NULL,
	[PaymentMethod] [int] NULL,
	[PaymentAccount] [varchar](20) NULL,
	[PaymentType] [int] NULL,
	[DateOfPayment] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Contact] [varchar](20) NULL,
	[Gender] [int] NULL,
	[Status] [int] NULL,
	[StreetAddress] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
 CONSTRAINT [PK__Person__3214EC079388B2A4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[ManufacturerId] [int] NULL,
	[InventoryId] [int] NULL,
	[SalePrice] [float] NULL,
	[Image] [varchar](max) NULL,
	[Category] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 4/26/2025 3:20:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateOfPurchase] [datetime] NULL,
	[PaymentId] [int] NULL,
	[AddedBy] [int] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[Quantity] [int] NULL,
	[PurchaseId] [int] NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[UnitPrice] [float] NULL,
	[ManufacturerId] [int] NULL,
	[ReceivedQuantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC,
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Return]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Return](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateOfReturn] [datetime] NULL,
	[ReturnType] [int] NULL,
	[AddedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReturnDetails]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnDetails](
	[ReturnId] [int] NOT NULL,
	[ProductCategory] [int] NOT NULL,
	[ReturnQuantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReturnId] ASC,
	[ProductCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetails]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleDetails](
	[SaleId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[ProductCategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC,
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouse]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[StreetAddress] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseDetail] ADD  CONSTRAINT [DF_PurchaseDetail_ReceivedQuantity]  DEFAULT ((0)) FOR [ReceivedQuantity]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK__Customer__Id__3F466844] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK__Customer__Id__3F466844]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([CredentialsId])
REFERENCES [dbo].[Credentials] ([Id])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__Id__440B1D61] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__Id__440B1D61]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([Role])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Employee_Notification]  WITH CHECK ADD FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Employee_Notification]  WITH CHECK ADD FOREIGN KEY([Notification_EmployeeId])
REFERENCES [dbo].[Notification] ([Id])
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouse] ([Id])
GO
ALTER TABLE [dbo].[PaymentDetails]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
GO
ALTER TABLE [dbo].[PaymentDetails]  WITH CHECK ADD FOREIGN KEY([PaymentMethod])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[PaymentDetails]  WITH CHECK ADD FOREIGN KEY([PaymentType])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK__Person__Status__3B75D760] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK__Person__Status__3B75D760]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[Lookup] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Gender]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([InventoryId])
REFERENCES [dbo].[Inventory] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([Id])
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Employee] FOREIGN KEY([AddedBy])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Employee]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[Purchase] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Manufacturer] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Manufacturer]
GO
ALTER TABLE [dbo].[Return]  WITH CHECK ADD FOREIGN KEY([ReturnType])
REFERENCES [dbo].[Lookup] ([Id])
GO
ALTER TABLE [dbo].[ReturnDetails]  WITH CHECK ADD FOREIGN KEY([ProductCategory])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[ReturnDetails]  WITH CHECK ADD FOREIGN KEY([ReturnId])
REFERENCES [dbo].[Return] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[SaleDetails]  WITH CHECK ADD FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sale] ([Id])
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[sp_AddCustomer]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddCustomer]
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
/****** Object:  StoredProcedure [dbo].[sp_AddEmployee]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Add Employee
CREATE PROCEDURE [dbo].[sp_AddEmployee]
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
    @CredentialsId int = NULL
    
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
/****** Object:  StoredProcedure [dbo].[sp_AddInventory]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddInventory]
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
/****** Object:  StoredProcedure [dbo].[sp_AddPerson]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddPerson]
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
/****** Object:  StoredProcedure [dbo].[sp_AddWarehouse]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddWarehouse]
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
/****** Object:  StoredProcedure [dbo].[sp_DeletePerson]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--> Delete Person
CREATE PROCEDURE [dbo].[sp_DeletePerson]
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
/****** Object:  StoredProcedure [dbo].[sp_DeleteWarehouse]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Delete Warehouse
CREATE PROCEDURE [dbo].[sp_DeleteWarehouse]
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
/****** Object:  StoredProcedure [dbo].[sp_GetInventoryById]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Get Inventory By Id
CREATE PROCEDURE [dbo].[sp_GetInventoryById]
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
/****** Object:  StoredProcedure [dbo].[sp_UpdateEmployee]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--> Update Employee
CREATE PROCEDURE [dbo].[sp_UpdateEmployee]
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
/****** Object:  StoredProcedure [dbo].[sp_UpdateInventory]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Update Inventory
CREATE PROCEDURE [dbo].[sp_UpdateInventory]
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
/****** Object:  StoredProcedure [dbo].[sp_UpdatePerson]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--> Update Person
CREATE PROCEDURE [dbo].[sp_UpdatePerson]
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
/****** Object:  StoredProcedure [dbo].[sp_UpdateWarehouse]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateWarehouse]
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
/****** Object:  StoredProcedure [dbo].[UpdateSaleStatus]    Script Date: 4/26/2025 3:20:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--> Update Sale Status
CREATE PROCEDURE [dbo].[UpdateSaleStatus]
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
USE [master]
GO
ALTER DATABASE [AutoArsenal] SET  READ_WRITE 
GO
