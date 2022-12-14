USE [master]
GO
/****** Object:  Database [AssessmentTask]    Script Date: 08-12-2022 00:56:19 ******/
CREATE DATABASE [AssessmentTask]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AssessmentTask', FILENAME = N'C:\Users\Acer\AssessmentTask.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AssessmentTask_log', FILENAME = N'C:\Users\Acer\AssessmentTask_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AssessmentTask] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AssessmentTask].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AssessmentTask] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AssessmentTask] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AssessmentTask] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AssessmentTask] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AssessmentTask] SET ARITHABORT OFF 
GO
ALTER DATABASE [AssessmentTask] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AssessmentTask] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AssessmentTask] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AssessmentTask] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AssessmentTask] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AssessmentTask] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AssessmentTask] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AssessmentTask] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AssessmentTask] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AssessmentTask] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AssessmentTask] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AssessmentTask] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AssessmentTask] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AssessmentTask] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AssessmentTask] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AssessmentTask] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AssessmentTask] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AssessmentTask] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AssessmentTask] SET  MULTI_USER 
GO
ALTER DATABASE [AssessmentTask] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AssessmentTask] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AssessmentTask] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AssessmentTask] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AssessmentTask] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AssessmentTask] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AssessmentTask] SET QUERY_STORE = OFF
GO
USE [AssessmentTask]
GO
/****** Object:  Table [dbo].[ApplicationLog]    Script Date: 08-12-2022 00:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationLog](
	[ApplicationLogID] [uniqueidentifier] NOT NULL,
	[LogDate] [datetime2](7) NOT NULL,
	[LogOriginator] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ApplicationLog] PRIMARY KEY CLUSTERED 
(
	[ApplicationLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 08-12-2022 00:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 08-12-2022 00:56:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[TokenCreated] [datetime2](7) NULL,
	[TokenExpires] [datetime2](7) NULL,
	[isActive] [int] NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[ApplicationLog] ([ApplicationLogID], [LogDate], [LogOriginator], [Message], [Exception], [UserID]) VALUES (N'2a3d7879-929a-4e4c-aa70-5c1b5f3bee85', CAST(N'2022-12-07T22:59:28.7181539' AS DateTime2), N'AssessmentTask.UserService.RegisterUser', N'Invalid object name ''User''.', N'Microsoft.Data.SqlClient.SqlException (0x80131904): Invalid object name ''User''.
   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Linq.Enumerable.TryGetSingle[TSource](IEnumerable`1 source, Boolean& found)
   at lambda_method15(Closure , QueryContext )
   at System.Linq.Queryable.FirstOrDefault[TSource](IQueryable`1 source)
   at AssessmentTask.Services.UserService.UserService.RegisterUser(User user, DatabaseContext _context) in F:\AssessmentTask\AssessmentTask\Services\UserService\UserService.cs:line 158
ClientConnectionId:d6e4d175-b003-4597-9f97-a47c7ed38afc
Error Number:208,State:1,Class:16', N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[ApplicationLog] ([ApplicationLogID], [LogDate], [LogOriginator], [Message], [Exception], [UserID]) VALUES (N'dcdaa275-b379-4aad-b429-6df4b019a110', CAST(N'2022-12-07T23:03:58.6626838' AS DateTime2), N'AssessmentTask.UserService.RegisterUser', N'Invalid object name ''User''.', N'Microsoft.Data.SqlClient.SqlException (0x80131904): Invalid object name ''User''.
   at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean isAsync, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at Microsoft.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry, String method)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteReader(CommandBehavior behavior)
   at Microsoft.Data.SqlClient.SqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReader(RelationalCommandParameterObject parameterObject)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.InitializeReader(Enumerator enumerator)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.Enumerator.MoveNext()
   at System.Linq.Enumerable.TryGetSingle[TSource](IEnumerable`1 source, Boolean& found)
   at lambda_method13(Closure , QueryContext )
   at System.Linq.Queryable.FirstOrDefault[TSource](IQueryable`1 source)
   at AssessmentTask.Services.UserService.UserService.RegisterUser(User user, DatabaseContext _context) in F:\AssessmentTask\AssessmentTask\Services\UserService\UserService.cs:line 158
ClientConnectionId:596abfea-626d-4418-a38b-75449be3835c
Error Number:208,State:1,Class:16', N'00000000-0000-0000-0000-000000000000')
GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Quantity], [Price], [CreatedDate], [UpdatedDate]) VALUES (N'21828d4f-d91b-4831-9854-41db886f4fec', N'string', N'string', 11241, 124.124, CAST(N'2022-12-08T00:43:01.8133151' AS DateTime2), CAST(N'2022-12-08T00:45:21.6078875' AS DateTime2))
GO
INSERT [dbo].[User] ([UserId], [Username], [PasswordHash], [PasswordSalt], [RefreshToken], [TokenCreated], [TokenExpires], [isActive]) VALUES (N'9674173b-9c81-4e9c-bec1-a46dc7915963', N'arun', N'pEh2XnTiL51JSCTSs1/MRVg0gjqjixNoNd2FWS96poLguMrEvzgeaSJLGrunr+YqPiw6S8B//iMZVsF7v5/s1Q==', N'2ku1Zd7//rxb2NN0wPEK4o3+oxY3Y06dWxddaJsNZ746dQ7pc7H9xOWXFOrAZvYsgbW7427Urh1Y/8bGNHaHfXbd5sgVxMCrh20wcqjCgE7K0Con7FqoF4Nih7TztSlktxCoEMArRW+G+uEs4b676ejokxEmRzwPfJm3QBPhUa4=', N'fyb7K+QLL6eF0sEUxjVS1w3Q1UE0Y1jHeDzoO3QmW5mVWm8M6vCCqlMasiugcWVvQVEL1/u6obwK34N+rOtVKQ==', CAST(N'2022-12-08T00:22:02.1079280' AS DateTime2), CAST(N'2022-12-15T00:22:02.1078857' AS DateTime2), 1)
INSERT [dbo].[User] ([UserId], [Username], [PasswordHash], [PasswordSalt], [RefreshToken], [TokenCreated], [TokenExpires], [isActive]) VALUES (N'38fa478c-af2e-408e-8069-c6e92ba81edf', N'string', N'0WYraUhG0EXAy++4LNcDLCjIA8zWO4RMDuxWKsIHvV1VjWeP9TexyUyxJrdk9lCuK0Px7MIzf6gX3YU5rTaOSw==', N'dNErzQX2TntgUJiWVIxmZXfg0RGb0eRUjTWDL79YkiwygNPW5KpJo9JUq3bnzx5uGgbytDx3RxIJ2sctetFY2943HCsGMt8MlNJChc/lYLgXLs20xpUHSFBFA2/Dziyfvc6K4eUQfwUPUl5AFif5vUAhwSfdS2LHpvaocjGXjHM=', N'mnKSxhrA2VGV+9e1HGbB45XkQ9nNxE5tpDkM6uBaF/H/GWXE/ZNsPRuE3I9GhJAJcygbjEXWxkUxPlsaFBnYoA==', CAST(N'2022-12-08T00:40:54.2687681' AS DateTime2), CAST(N'2022-12-15T00:40:54.2687680' AS DateTime2), 1)
GO
USE [master]
GO
ALTER DATABASE [AssessmentTask] SET  READ_WRITE 
GO
