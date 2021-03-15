
PRINT 'Creating database - TalDBMain'

CREATE DATABASE [TalDBMain]
GO

USE [TalDBMain]
GO
/****** Object:  Table [dbo].[Occupation]    Script Date: 14-03-2021 11:32:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Occupation](
	[OccupationId] [int] IDENTITY(1,1) NOT NULL,
	[OccupationName] [nvarchar](255) NOT NULL,
	[RatingId] [int] NOT NULL,
 CONSTRAINT [PK_Occupation] PRIMARY KEY CLUSTERED 
(
	[OccupationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 14-03-2021 11:32:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[RatingName] [nvarchar](255) NOT NULL,
	[Factor] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Occupation] ON 
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (1, N'Cleaner', 3)
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (2, N'Doctor', 1)
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (3, N'Author', 2)
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (4, N'Farmer', 4)
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (5, N'Mechanic', 4)
GO
INSERT [dbo].[Occupation] ([OccupationId], [OccupationName], [RatingId]) VALUES (6, N'Florist', 3)
GO
SET IDENTITY_INSERT [dbo].[Occupation] OFF
GO
SET IDENTITY_INSERT [dbo].[Rating] ON 
GO
INSERT [dbo].[Rating] ([RatingId], [RatingName], [Factor]) VALUES (1, N'Professional', CAST(1.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[Rating] ([RatingId], [RatingName], [Factor]) VALUES (2, N'White Collar', CAST(1.25 AS Decimal(6, 2)))
GO
INSERT [dbo].[Rating] ([RatingId], [RatingName], [Factor]) VALUES (3, N'Light Manual

', CAST(1.50 AS Decimal(6, 2)))
GO
INSERT [dbo].[Rating] ([RatingId], [RatingName], [Factor]) VALUES (4, N'Heavy Manual

', CAST(1.75 AS Decimal(6, 2)))
GO
SET IDENTITY_INSERT [dbo].[Rating] OFF
GO
ALTER TABLE [dbo].[Occupation]  WITH CHECK ADD  CONSTRAINT [FK_Occupation_Rating] FOREIGN KEY([RatingId])
REFERENCES [dbo].[Rating] ([RatingId])
GO
ALTER TABLE [dbo].[Occupation] CHECK CONSTRAINT [FK_Occupation_Rating]
GO

CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](128) NULL,
	[TimeStamp] [datetimeoffset](7) NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [xml] NULL,
	[LogEvent] [nvarchar](max) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
