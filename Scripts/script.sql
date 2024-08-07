USE [master]
GO
/****** Object:  Database [AutoArsenal]    Script Date: 26/04/2024 9:48:08 am ******/
CREATE DATABASE [AutoArsenal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutoArsenal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AutoArsenal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutoArsenal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AutoArsenal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AutoArsenal] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoArsenal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutoArsenal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AutoArsenal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AutoArsenal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AutoArsenal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AutoArsenal] SET ARITHABORT OFF 
GO
ALTER DATABASE [AutoArsenal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AutoArsenal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AutoArsenal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AutoArsenal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AutoArsenal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AutoArsenal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AutoArsenal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AutoArsenal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AutoArsenal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AutoArsenal] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AutoArsenal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AutoArsenal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AutoArsenal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AutoArsenal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AutoArsenal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AutoArsenal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AutoArsenal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AutoArsenal] SET RECOVERY FULL 
GO
ALTER DATABASE [AutoArsenal] SET  MULTI_USER 
GO
ALTER DATABASE [AutoArsenal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AutoArsenal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AutoArsenal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AutoArsenal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AutoArsenal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AutoArsenal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AutoArsenal', N'ON'
GO
ALTER DATABASE [AutoArsenal] SET QUERY_STORE = ON
GO
ALTER DATABASE [AutoArsenal] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AutoArsenal]
GO
/****** Object:  Table [dbo].[Credentials]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Employee_Notification]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Inventory]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Location]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StreetAddress] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Contact] [varchar](20) NULL,
	[LocationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Person]    Script Date: 26/04/2024 9:48:08 am ******/
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
	[LocationId] [int] NULL,
 CONSTRAINT [PK__Person__3214EC079388B2A4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[ManufacturerId] [int] NULL,
	[InventoryId] [int] NULL,
	[UnitPrice] [float] NULL,
	[SalePrice] [float] NULL,
	[Image] [varchar](max) NULL,
	[Category] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateOfPurchase] [datetime] NULL,
	[PaymentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[Quantity] [int] NULL,
	[PurchaseId] [int] NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC,
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Return]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Return](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateOfReturn] [datetime] NULL,
	[ReturnType] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReturnDetails]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnDetails](
	[ReturnId] [int] NOT NULL,
	[ProductCategory] [int] NOT NULL,
	[ReturnQuantity] [int] NULL,
	[ReturnPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReturnId] ASC,
	[ProductCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 26/04/2024 9:48:08 am ******/
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleDetails]    Script Date: 26/04/2024 9:48:08 am ******/
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
/****** Object:  Table [dbo].[Warehouse]    Script Date: 26/04/2024 9:48:08 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[LocationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Lookup] ON 

INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (1, N'Gender', N'Male')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (2, N'Gender', N'Female')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (3, N'Product_Category', N'High_Quality')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (4, N'Product_Category', N'Medium_Quality')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (5, N'Product_Category', N'Low_Quality')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (6, N'Payment_Method', N'Online')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (7, N'Payment_Method', N'Cash')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (8, N'Status', N'Active')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (9, N'Status', N'Inactive')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (10, N'Employee_Type', N'Worker')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (11, N'Employee_Type', N'Cashier')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (12, N'Type', N'Purchase')
INSERT [dbo].[Lookup] ([Id], [Category], [Value]) VALUES (13, N'Type', N'Sale')
SET IDENTITY_INSERT [dbo].[Lookup] OFF
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
ALTER TABLE [dbo].[Manufacturer]  WITH CHECK ADD FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
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
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK__Person__Location__3C69FB99] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK__Person__Location__3C69FB99]
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
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[Purchase] ([Id])
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
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
USE [master]
GO
ALTER DATABASE [AutoArsenal] SET  READ_WRITE 
GO
