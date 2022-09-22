USE [EQC_NEW]
GO
DROP TABLE [dbo].[CourtVerdict]
DROP TABLE [dbo].[JUList]
DROP TABLE [dbo].[JUDoc]
/****** Object:  Table [dbo].[CourtVerdict]    Script Date: 2022/9/22 上午 07:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourtVerdict](
	[Seq] [int] IDENTITY(1,1) NOT NULL,
	[JID] [nvarchar](80) NULL,
	[JYear] [nvarchar](5) NULL,
	[JCase] [nvarchar](20) NULL,
	[JNo] [varchar](20) NULL,
	[JDate] [nvarchar](10) NULL,
	[JTitle] [nvarchar](500) NULL,
	[JFull] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUser] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[ModifyUser] [int] NULL,
 CONSTRAINT [PK_CourtVerdict] PRIMARY KEY CLUSTERED 
(
	[Seq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JUDoc]    Script Date: 2022/9/22 上午 07:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JUDoc](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ListDate] [nvarchar](12) NOT NULL,
	[Attachments] [nvarchar](2000) NOT NULL,
	[JFullX] [nvarchar](max) NOT NULL,
	[JID] [nvarchar](200) NOT NULL,
	[JYear] [nvarchar](3) NOT NULL,
	[JCase] [nvarchar](80) NOT NULL,
	[JNO] [nvarchar](10) NOT NULL,
	[JDate] [nvarchar](10) NOT NULL,
	[JTitle] [nvarchar](255) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateUser] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[ModifyUser] [int] NULL,
 CONSTRAINT [PK_JUDoc] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JUList]    Script Date: 2022/9/22 上午 07:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JUList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ListDate] [nvarchar](12) NOT NULL,
	[ListItem] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateUser] [int] NULL,
	[ModifyTime] [datetime] NULL,
	[ModifyUser] [int] NULL,
 CONSTRAINT [PK_JList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'Seq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'判別書ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JCase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'號次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審判日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判案由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判書全文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'JFull'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'判決書資料' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CourtVerdict'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁決書異動日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'ListDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判書附檔' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'Attachments'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判書全文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JFullX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判書 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JYear'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字別' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JCase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'號次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JNO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁判案由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'JTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUDoc', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁決書異動日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'ListDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'裁決書異動ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'ListItem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動人員序號' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JUList', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
