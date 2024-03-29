USE [master]
GO
/****** Object:  Database [DB_LF]    Script Date: 6/26/2019 6:49:03 PM ******/
CREATE DATABASE [DB_LF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_LF', FILENAME = N'D:\DATA\DB_LF.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ), 
 FILEGROUP [DADOS] 
( NAME = N'DB_LF_DADOS_1', FILENAME = N'D:\DATA\DB_LF_DADOS_1.ndf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ),
( NAME = N'DB_LF_DADOS_2', FILENAME = N'D:\DATA\DB_LF_DADOS_2.ndf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ),
( NAME = N'DB_LF_DADOS_3', FILENAME = N'D:\DATA\DB_LF_DADOS_3.ndf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ),
( NAME = N'DB_LF_DADOS_4', FILENAME = N'D:\DATA\DB_LF_DADOS_4.ndf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_LF_log', FILENAME = N'D:\DATA\DB_LF_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB_LF] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_LF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_LF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_LF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_LF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_LF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_LF] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_LF] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_LF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_LF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_LF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_LF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_LF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_LF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_LF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_LF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_LF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_LF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_LF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_LF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_LF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_LF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_LF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_LF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_LF] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_LF] SET  MULTI_USER 
GO
ALTER DATABASE [DB_LF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_LF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_LF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_LF] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DB_LF] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_LF', N'ON'
GO
USE [DB_LF]
GO
/****** Object:  Table [dbo].[tbCombinacao]    Script Date: 6/26/2019 6:49:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCombinacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[d1] [tinyint] NOT NULL,
	[d2] [tinyint] NOT NULL,
	[d3] [tinyint] NOT NULL,
	[d4] [tinyint] NOT NULL,
	[d5] [tinyint] NOT NULL,
	[d6] [tinyint] NOT NULL,
	[d7] [tinyint] NOT NULL,
	[d8] [tinyint] NOT NULL,
	[d9] [tinyint] NOT NULL,
	[d10] [tinyint] NOT NULL,
	[d11] [tinyint] NOT NULL,
	[d12] [tinyint] NOT NULL,
	[d13] [tinyint] NOT NULL,
	[d14] [tinyint] NOT NULL,
	[d15] [tinyint] NOT NULL,
 CONSTRAINT [PK_tbCombinacao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbCombinacaoConcurso]    Script Date: 6/26/2019 6:49:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCombinacaoConcurso](
	[IdCombinacao] [int] NOT NULL,
	[IdConcurso] [smallint] NOT NULL,
	[p11] [bit] NULL,
	[p12] [bit] NULL,
	[p13] [bit] NULL,
	[p14] [bit] NULL,
	[p15] [bit] NULL,
 CONSTRAINT [PK_tbCombinacaoConcurso] PRIMARY KEY CLUSTERED 
(
	[IdCombinacao] ASC,
	[IdConcurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DADOS]
) ON [DADOS]

GO
/****** Object:  Table [dbo].[tbConcurso]    Script Date: 6/26/2019 6:49:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbConcurso](
	[Id] [smallint] NOT NULL,
	[DataSorteio] [date] NOT NULL,
	[d1] [tinyint] NOT NULL,
	[d2] [tinyint] NOT NULL,
	[d3] [tinyint] NOT NULL,
	[d4] [tinyint] NOT NULL,
	[d5] [tinyint] NOT NULL,
	[d6] [tinyint] NOT NULL,
	[d7] [tinyint] NOT NULL,
	[d8] [tinyint] NOT NULL,
	[d9] [tinyint] NOT NULL,
	[d10] [tinyint] NOT NULL,
	[d11] [tinyint] NOT NULL,
	[d12] [tinyint] NOT NULL,
	[d13] [tinyint] NOT NULL,
	[d14] [tinyint] NOT NULL,
	[d15] [tinyint] NOT NULL,
 CONSTRAINT [PK_tbConcurso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IDX_ATRASO]    Script Date: 6/26/2019 6:49:03 PM ******/
CREATE NONCLUSTERED INDEX [IDX_ATRASO] ON [dbo].[tbCombinacaoConcurso]
(
	[p11] ASC,
	[p12] ASC,
	[p13] ASC,
	[p14] ASC,
	[p15] ASC
)
INCLUDE ( 	[IdCombinacao],
	[IdConcurso]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DADOS]
GO
/****** Object:  Index [IDX_ATRASO2]    Script Date: 6/26/2019 6:49:03 PM ******/
CREATE NONCLUSTERED INDEX [IDX_ATRASO2] ON [dbo].[tbCombinacaoConcurso]
(
	[IdConcurso] ASC,
	[p11] ASC,
	[p12] ASC,
	[p13] ASC,
	[p14] ASC,
	[p15] ASC
)
INCLUDE ( 	[IdCombinacao]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [DADOS]
GO
USE [master]
GO
ALTER DATABASE [DB_LF] SET  READ_WRITE 
GO
