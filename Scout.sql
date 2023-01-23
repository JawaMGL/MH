USE [ScoutingReports]
GO

/****** Object:  Table [dbo].[Scout]    Script Date: 1/23/2023 9:05:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Scout](
	[ScoutKey] [int] IDENTITY(1,1) NOT NULL,
	[ScoutFirstName] [nvarchar](50) NOT NULL,
	[ScoutLastName] [nvarchar](50) NOT NULL,
	[ScoutPhoneNumber] [nvarchar](50) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Scout] PRIMARY KEY CLUSTERED 
(
	[ScoutKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

