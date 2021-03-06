USE [MindMap]
GO
ALTER TABLE [dbo].[MindMapDocument] DROP CONSTRAINT [FK_MindMapDocument_MindMapCategory]
GO
/****** Object:  Table [dbo].[MindMapPermission]    Script Date: 9/20/2016 3:11:11 PM ******/
DROP TABLE [dbo].[MindMapPermission]
GO
/****** Object:  Table [dbo].[MindMapMainTitle]    Script Date: 9/20/2016 3:11:11 PM ******/
DROP TABLE [dbo].[MindMapMainTitle]
GO
/****** Object:  Table [dbo].[MindMapDocument]    Script Date: 9/20/2016 3:11:11 PM ******/
DROP TABLE [dbo].[MindMapDocument]
GO
/****** Object:  Table [dbo].[MindMapCategory]    Script Date: 9/20/2016 3:11:11 PM ******/
DROP TABLE [dbo].[MindMapCategory]
GO
/****** Object:  Table [dbo].[MindMapCategory]    Script Date: 9/20/2016 3:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MindMapCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_MindMapCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MindMapDocument]    Script Date: 9/20/2016 3:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MindMapDocument](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_MindMapDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MindMapMainTitle]    Script Date: 9/20/2016 3:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MindMapMainTitle](
	[MainTitle] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MindMapPermission]    Script Date: 9/20/2016 3:11:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MindMapPermission](
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_MIndMapPermission] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[MindMapCategory] ON 

INSERT [dbo].[MindMapCategory] ([Id], [Name], [IsPublic]) VALUES (1, N'Public 1', 1)
INSERT [dbo].[MindMapCategory] ([Id], [Name], [IsPublic]) VALUES (2, N'Public 2', 1)
INSERT [dbo].[MindMapCategory] ([Id], [Name], [IsPublic]) VALUES (3, N'Vip 1', 0)
INSERT [dbo].[MindMapCategory] ([Id], [Name], [IsPublic]) VALUES (4, N'Vip 2', 0)
SET IDENTITY_INSERT [dbo].[MindMapCategory] OFF
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'8323dee4-fc56-4384-9714-42a1798b4092', 0, 1, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'cd4bd866-50fc-4022-a666-7dfa3b2cf387', 0, 2, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'1141056b-c5e5-4508-afff-851d0d8fa693', 0, 2, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'45042fca-4ee4-48fd-b6a6-c21ac12a1fb4', 1, NULL, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'22d96c68-dd2b-4055-9654-e4f279b473dc', 0, 4, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'bf659d73-c397-4b13-b6aa-e602824c4930', 1, NULL, NULL)
INSERT [dbo].[MindMapDocument] ([Id], [UserId], [CategoryId], [ParentId]) VALUES (N'5f1d7cdc-08ac-4004-9f3f-eb2a2203de53', 0, 3, NULL)
INSERT [dbo].[MindMapMainTitle] ([MainTitle]) VALUES (N'New Title')
INSERT [dbo].[MindMapPermission] ([UserId], [CategoryId]) VALUES (1, 2)
ALTER TABLE [dbo].[MindMapDocument]  WITH CHECK ADD  CONSTRAINT [FK_MindMapDocument_MindMapCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[MindMapCategory] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MindMapDocument] CHECK CONSTRAINT [FK_MindMapDocument_MindMapCategory]
GO
