USE [DBTreinamento]
GO

/****** Object:  Table [dbo].[Time]    Script Date: 16/11/2021 00:54:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Time](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NULL,
	[Divisao] [nvarchar](10) NULL,
	[Regiao] [nvarchar](50) NULL,
	[NomeCampeonato] [nvarchar](100) NULL
) ON [PRIMARY]
GO


