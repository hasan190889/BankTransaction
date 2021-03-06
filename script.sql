USE [master]
GO
/****** Object:  Database [DBAccount]    Script Date: 1/30/2018 7:32:48 PM ******/
CREATE DATABASE [DBAccount]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBAccount', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DBAccount.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBAccount_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DBAccount_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBAccount] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBAccount].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBAccount] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBAccount] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBAccount] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBAccount] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBAccount] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBAccount] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBAccount] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBAccount] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBAccount] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBAccount] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBAccount] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBAccount] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBAccount] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBAccount] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBAccount] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBAccount] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBAccount] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBAccount] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBAccount] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBAccount] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBAccount] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBAccount] SET  MULTI_USER 
GO
ALTER DATABASE [DBAccount] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBAccount] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBAccount] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBAccount] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DBAccount]
GO
/****** Object:  StoredProcedure [dbo].[USP_Accountbalance]    Script Date: 1/30/2018 7:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Accountbalance] 
(	
	@AccountNumber BIGINT
)
AS
BEGIN

	DECLARE	@Balance	MONEY = -1,  
			@Status		BIT = 0, 
			@Message	VARCHAR(1000),
			@Currency	VARCHAR(10)

	SELECT	@Balance= ISNULL( Balance,-1), 
			@Currency = Currnecy FROM TblAccountBalance WHERE AccountNumber = @AccountNumber 

BEGIN TRY	
	If @Balance = -1
		RAISERROR('Account Number does not exists',16,1)
	ELSE 
		BEGIN
			SET @Status = 1
			SET @Message = 'Success'
		END
END TRY
BEGIN CATCH
	SET @Message  = ERROR_MESSAGE();
	SET @Balance = 0
END CATCH
	
	SELECT	@AccountNumber[AccountNumber],
			@Status[Successful],
			@Balance[Balance],
			@Currency [Currency],
			@Message[Message]
END


GO
/****** Object:  StoredProcedure [dbo].[USP_Deposit]    Script Date: 1/30/2018 7:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_Deposit] 
(	
	@AccountNumber	BIGINT,
	@Amount			MONEY,
	@Currency		VARCHAR(10)
)
AS
BEGIN
	BEGIN TRY
		Declare @Status			BIT = 0, 
				@Balance		MONEY = 0, 
				@Message		VARCHAR(1000),
				@TransactionId	BIGINT = 0

	If @Amount <= 0
	BEGIN
		SET @Status = 0
		RAISERROR('Amount should be greater then zero.',16,1)
	END

	IF EXISTS (SELECT ABId FROM TblAccountBalance(NOLOCK) WHERE AccountNumber = @AccountNumber )
	BEGIN
		BEGIN TRAN
			INSERT INTO TblAccountTransaction 
			(
				AccountNumber,
				Amount,
				Currency,
				[Status],
				TransactionType
			) 
			VALUES
			(
				@AccountNumber,
				@Amount,
				@Currency,
				'Success',
				'C'
			)

			SELECT @TransactionId = SCOPE_IDENTITY();
			
			UPDATE TblAccountBalance 
			SET Balance = Balance + @Amount, 
				ModifyDate = GETDATE() 
			WHERE AccountNumber = @AccountNumber
			
			SELECT @Balance = balance, 
					@Currency= Currnecy 
			FROM TblAccountBalance 
			WHERE AccountNumber = @AccountNumber
			
			SET @Status= 1 -- true
			SET @Message = 'Success'
		COMMIT 
	END
	ELSE
	BEGIN
		SET @Status = 0
		RAISERROR('Account Number does not exists.',16,1)
	END
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
	ROLLBACK tran
	SET @Message  = ERROR_MESSAGE();
END CATCH	
	
	SELECT	@AccountNumber[AccountNumber],
			@Status[Successful],
			@Balance[Balance],
			@Currency [Currency],
			@Message[Message],
			@TransactionId [TransactionId]
END


GO
/****** Object:  StoredProcedure [dbo].[USP_Withdraw]    Script Date: 1/30/2018 7:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Withdraw] 
(	
	@AccountNumber	BIGINT,
	@Amount			MONEY,
	@Currency		VARCHAR(10)
)
AS
BEGIN
BEGIN TRY	
	DECLARE	@Status			BIT = 0, 
			@Balance		MONEY = -1, 
			@Message		VARCHAR(1000),
			@TransactionId	BIGINT= 0
	
	If @Amount <=0
	BEGIN
		SET @Status = 0
		RAISERROR('Amount should be greater then zero.',16,1)
	END

	SELECT @Balance = ISNULL( Balance,0) FROM TblAccountBalance(NOLOCK) WHERE AccountNumber = @AccountNumber
	If @Balance > = @Amount
	BEGIN
		BEGIN TRAN
			INSERT INTO TblAccountTransaction 
			(
				AccountNumber,
				Amount,
				Currency,
				[Status],
				TransactionType
			) 
			VALUES
			(	
				@AccountNumber,
				@Amount,
				@Currency,
				'Success',
				'D'
			)
			
			SELECT @TransactionId = SCOPE_IDENTITY();
			
			UPDATE TblAccountBalance 
			SET Balance = Balance - @Amount, 
				ModifyDate = GETDATE() 
			WHERE AccountNumber = @AccountNumber
			
			SELECT	@Balance = balance, 
					@Currency= Currnecy 
			FROM	TblAccountBalance 
			WHERE	AccountNumber = @AccountNumber

			SET @Status= 1 -- true
			SET @Message = 'Success'
		COMMIT 
	END
	ELSE
	BEGIN
		SET @Status = 0
		If(@Balance = -1)
			BEGIN
				SET @Balance = 0
				RAISERROR('Account Number does not exists.',16,1)
			END
		ELSE
			RAISERROR('Insufficient Balnace',16,1)		
	END
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
	ROLLBACK TRAN
	SET @Message  = ERROR_MESSAGE();
END CATCH

	SELECT	@AccountNumber[AccountNumber],
			@Status[Successful],
			@Balance[Balance],
			@Currency [Currency],
			@Message[Message],
			@TransactionId [TransactionId]
END


GO
/****** Object:  Table [dbo].[TblAccountBalance]    Script Date: 1/30/2018 7:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblAccountBalance](
	[ABId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [bigint] NOT NULL,
	[Balance] [money] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[Currnecy] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblAccountTransaction]    Script Date: 1/30/2018 7:32:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblAccountTransaction](
	[TransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [bigint] NULL,
	[Amount] [money] NOT NULL,
	[Currency] [varchar](10) NULL,
	[TransactionType] [varchar](2) NULL,
	[Status] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TblAccountBalance] ADD  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[TblAccountBalance] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblAccountTransaction] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TblAccountTransaction]  WITH CHECK ADD  CONSTRAINT [fk_AccountBalance_AccountNumber] FOREIGN KEY([AccountNumber])
REFERENCES [dbo].[TblAccountBalance] ([AccountNumber])
GO
ALTER TABLE [dbo].[TblAccountTransaction] CHECK CONSTRAINT [fk_AccountBalance_AccountNumber]
GO
USE [master]
GO
ALTER DATABASE [DBAccount] SET  READ_WRITE 
GO


Insert into TblAccountBalance (AccountNumber , Balance ,Currnecy )values
(1000000001,10000,'INR'),
(1000000002,10000,'INR'),
(1000000003,10000,'INR'),
(1000000004,10000,'INR'),
(1000000005,10000,'INR'),
(1000000006,10000,'INR'),
(1000000007,10000,'INR'),
(1000000008,10000,'INR'),
(1000000009,10000,'INR'),
(1000000010,10000,'INR')
GO