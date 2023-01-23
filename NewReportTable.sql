USE [ScoutingReports]
GO

/****** Object:  Table [dbo].[NewReport]    Script Date: 1/23/2023 9:03:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NewReport](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerKey] [int] NOT NULL,
	[TeamKey] [int] NOT NULL,
	[ScoutKey] [int] NOT NULL,
	[Comments] [nvarchar](500) NULL,
	[HighlightLink] [nvarchar](50) NULL,
	[DefenseRating] [int] NULL,
	[ReboundRating] [int] NULL,
	[ShootingRating] [int] NULL,
	[AssistRating] [int] NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[UpdatedDateTime] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

