USE [storedb]
GO

/****** Object:  Table [Store].[Account]    Script Date: 12/14/2018 11:13:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Store].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](100) NOT NULL,
	[PasswordSalt] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Email] [varchar](128) NOT NULL,
	[FailedLoginAttempts] [smallint] NOT NULL,
	[EmailVerificationCode] [char](32) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Store].[Account] ADD  CONSTRAINT [DF_Account_FailedPasswordAttempts]  DEFAULT ((0)) FOR [FailedLoginAttempts]
GO



USE [storedb]
GO

/****** Object:  Table [Store].[Profile]    Script Date: 12/14/2018 11:13:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Store].[Profile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](20) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
	[Account_ID] [int] NOT NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Store].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Account] FOREIGN KEY([Account_ID])
REFERENCES [Store].[Account] ([ID])
GO

ALTER TABLE [Store].[Profile] CHECK CONSTRAINT [FK_Profile_Account]
GO

