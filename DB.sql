USE [OrderLunchBox]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 2021/6/3 下午 08:45:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[AccountSid] [int] NOT NULL,
	[GroupName] [nvarchar](50) NULL,
	[GroupLeader] [nvarchar](50) NOT NULL,
	[ShopSid] [int] NOT NULL,
	[GroupImgName] [nvarchar](50) NOT NULL,
	[GroupStatus] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2021/6/3 下午 08:45:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ShopSId] [int] NOT NULL,
	[FoodName] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[FoodImg] [varchar](100) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2021/6/3 下午 08:45:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[MenuSid] [int] NOT NULL,
	[GroupSid] [int] NOT NULL,
	[AccountSid] [int] NOT NULL,
	[WhoOrder] [nvarchar](50) NOT NULL,
	[Qty] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shop]    Script Date: 2021/6/3 下午 08:45:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shop](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[ShopName] [nvarchar](50) NOT NULL,
	[ShopImg] [varchar](100) NULL,
 CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2021/6/3 下午 08:45:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Account] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserImgName] [varchar](100) NULL,
	[IsAdmin] [int] NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (1, 1, N'團一', N'Kos', 1, N'funamori.png', 1)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (2, 1, N'安安吃飯囉', N'Kos', 2, N'perimeni.png', 2)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (3, 1, N'安安吃飯囉', N'Kos', 1, N'perimeni.png', 0)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (6, 1, N'安安吃飯囉1', N'Kos', 1, N'perimeni.png', 0)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (7, 1, N'安安吃飯囉2', N'Kos', 1, N'funamori.png', 2)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (8, 1, N'便當', N'Kos', 2, N'ochaduke.png', 0)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (9, 1, N'便當1', N'Kos', 2, N'perimeni.png', 2)
INSERT [dbo].[Group] ([Sid], [AccountSid], [GroupName], [GroupLeader], [ShopSid], [GroupImgName], [GroupStatus]) VALUES (10, 1, N'安安吃飯囉1', N'Kos', 1, N'funamori.png', 0)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (1, 1, N'招牌鍋貼', CAST(5 AS Decimal(18, 0)), N'w960.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (2, 1, N'玉米鍋貼', CAST(6 AS Decimal(18, 0)), N'w970.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (3, 1, N'招牌水餃', CAST(5 AS Decimal(18, 0)), N'3.png')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (4, 1, N'玉米水餃', CAST(6 AS Decimal(18, 0)), N'w980.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (5, 1, N'玉米濃湯', CAST(30 AS Decimal(18, 0)), N'w990.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (6, 1, N'酸辣湯', CAST(30 AS Decimal(18, 0)), N'w1000.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (7, 2, N'排骨便當', CAST(60 AS Decimal(18, 0)), N'unnamed.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (8, 2, N'控肉便當', CAST(70 AS Decimal(18, 0)), N'80.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (9, 2, N'雞腿便當', CAST(80 AS Decimal(18, 0)), N'90.jpg')
INSERT [dbo].[Menu] ([Sid], [ShopSId], [FoodName], [Price], [FoodImg]) VALUES (10, 2, N'三寶便當', CAST(90 AS Decimal(18, 0)), N'120.jpg')
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (1, 2, 1, 1, N'Kos', 10)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (2, 1, 1, 1, N'Kos', 10)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (5, 2, 1, 1, N'Kos', 10)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (6, 5, 1, 1, N'Kos', 3)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (7, 6, 1, 1, N'Kos', 4)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (8, 1, 1, 1, N'Kos', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (9, 4, 1, 1, N'Kos', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (10, 4, 1, 1, N'Kos', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (12, 10, 9, 1, N'Kos', 4)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (13, 4, 6, 1, N'Kos', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (14, 2, 1, 2, N'Kid', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (15, 3, 1, 2, N'Kid', 5)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (18, 10, 9, 1, N'Kos', 1)
INSERT [dbo].[Order] ([Sid], [MenuSid], [GroupSid], [AccountSid], [WhoOrder], [Qty]) VALUES (19, 4, 6, 1, N'Kos', 5)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Shop] ON 

INSERT [dbo].[Shop] ([Sid], [ShopName], [ShopImg]) VALUES (1, N'八方雲集', N'shop2.jpg')
INSERT [dbo].[Shop] ([Sid], [ShopName], [ShopImg]) VALUES (2, N'便當店', N'shop1.png')
SET IDENTITY_INSERT [dbo].[Shop] OFF
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 

INSERT [dbo].[UserAccount] ([Sid], [Account], [Password], [UserName], [UserImgName], [IsAdmin]) VALUES (1, N'admin', N'admin', N'admin', N'businessman.png', 1)
INSERT [dbo].[UserAccount] ([Sid], [Account], [Password], [UserName], [UserImgName], [IsAdmin]) VALUES (2, N'Kid', N'Kid', N'Kid', N'girl_fashion.png', 1)
INSERT [dbo].[UserAccount] ([Sid], [Account], [Password], [UserName], [UserImgName], [IsAdmin]) VALUES (3, N'Josh', N'123', N'Josh', N'637583339272033619.png', 1)
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Shop] FOREIGN KEY([ShopSid])
REFERENCES [dbo].[Shop] ([Sid])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Shop]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Shop] FOREIGN KEY([ShopSId])
REFERENCES [dbo].[Shop] ([Sid])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Shop]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Group] FOREIGN KEY([GroupSid])
REFERENCES [dbo].[Group] ([Sid])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Group]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Menu] FOREIGN KEY([MenuSid])
REFERENCES [dbo].[Menu] ([Sid])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Menu]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_UserAccount] FOREIGN KEY([AccountSid])
REFERENCES [dbo].[UserAccount] ([Sid])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_UserAccount]
GO
