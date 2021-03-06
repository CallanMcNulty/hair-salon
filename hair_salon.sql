USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 7/15/2016 1:44:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[stylist_id] [int] NULL,
	[notes] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stylists]    Script Date: 7/15/2016 1:44:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[availability] [varchar](255) NULL,
	[notes] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [stylist_id], [notes]) VALUES (1, N'Edith', 2, N'Elderly and hard of hearing.')
INSERT [dbo].[clients] ([id], [name], [stylist_id], [notes]) VALUES (7, N'Allen', 2, N'')
INSERT [dbo].[clients] ([id], [name], [stylist_id], [notes]) VALUES (8, N'Sarah', 1, N'Whatever you do, don''t ask her if she''s married.')
INSERT [dbo].[clients] ([id], [name], [stylist_id], [notes]) VALUES (9, N'Elizabeth', 1, N'')
INSERT [dbo].[clients] ([id], [name], [stylist_id], [notes]) VALUES (5, N'Ana', 3, N'Russian, speaks very little English.')
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([id], [name], [availability], [notes]) VALUES (1, N'Debbie', N'M-W-F', N'Has to leave early on Fridays.')
INSERT [dbo].[stylists] ([id], [name], [availability], [notes]) VALUES (2, N'Angie', N'T-Th', N'Clients love her.')
INSERT [dbo].[stylists] ([id], [name], [availability], [notes]) VALUES (3, N'Lily', N'Sat-Sun', N'')
INSERT [dbo].[stylists] ([id], [name], [availability], [notes]) VALUES (5, N'Andrew', N'M-Th-Sat', N'')
SET IDENTITY_INSERT [dbo].[stylists] OFF
