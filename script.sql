USE [master]
GO
/****** Object:  Database [Bikedatabase]    Script Date: 10/4/2024 3:13:25 PM ******/
CREATE DATABASE [Bikedatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bikedatabase', FILENAME = N'C:\Users\UT01211\Bikedatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bikedatabase_log', FILENAME = N'C:\Users\UT01211\Bikedatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Bikedatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bikedatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bikedatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bikedatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bikedatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bikedatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bikedatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bikedatabase] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Bikedatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bikedatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bikedatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bikedatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bikedatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bikedatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bikedatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bikedatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bikedatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Bikedatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bikedatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bikedatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bikedatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bikedatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bikedatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bikedatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bikedatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Bikedatabase] SET  MULTI_USER 
GO
ALTER DATABASE [Bikedatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bikedatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bikedatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bikedatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bikedatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bikedatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Bikedatabase] SET QUERY_STORE = OFF
GO
USE [Bikedatabase]
GO
/****** Object:  Table [dbo].[Bikes]    Script Date: 10/4/2024 3:13:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bikes](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ImagePath] [nvarchar](500) NULL,
	[Regnumber] [int] NOT NULL,
	[Brand] [nvarchar](255) NOT NULL,
	[Category] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Model] [nvarchar](255) NOT NULL,
	[IsAvailable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/4/2024 3:13:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[Mobilenumber] [bigint] NOT NULL,
	[Licence] [bigint] NOT NULL,
	[Nic] [bigint] NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 10/4/2024 3:13:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Dateofhire] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 10/4/2024 3:13:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[id] [uniqueidentifier] NOT NULL,
	[CustomerID] [uniqueidentifier] NOT NULL,
	[MotorbikeID] [uniqueidentifier] NOT NULL,
	[RentalDate] [datetime] NOT NULL,
	[Returndate] [datetime] NULL,
	[Isoverdue] [bit] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Rental] ADD  DEFAULT ((0)) FOR [Isoverdue]
GO
ALTER TABLE [dbo].[Rental] ADD  DEFAULT ('Rent') FOR [status]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD FOREIGN KEY([MotorbikeID])
REFERENCES [dbo].[Bikes] ([ID])
GO
USE [master]
GO
ALTER DATABASE [Bikedatabase] SET  READ_WRITE 
GO
