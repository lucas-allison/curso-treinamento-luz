USE [DBTreinamento]
GO

/****** Object:  Table [dbo].[Time]    Script Date: 16/11/2021 00:54:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Time](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](50) NULL,
	[Divisao] [nchar](10) NULL,
	[Regiao] [nchar](50) NULL
) ON [PRIMARY]
GO


