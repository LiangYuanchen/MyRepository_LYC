/****** 上海浦东发展银行外规库数据库生成脚本 ******/
/****** 数据库名[SPDAct]                   ******/
/****** 脚本日期: 06/18/2013 09:56:41      ******/

/****新建法规数据库****/
CREATE DATABASE [SPDAct] ON  PRIMARY 
( NAME = N'SPDAct', FILENAME = N'E:\SPDAct.mdf' , SIZE = 7744KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SPDAct_log', FILENAME = N'E:\SPDAct_log.ldf' , SIZE = 1792KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
ALTER DATABASE [SPDAct] SET DB_CHAINING OFF 

/****打开法规数据库****/
USE [SPDAct]

/****新建法规表****/
CREATE TABLE [dbo].[Act](
	[ActID] [int] NOT NULL,
	[ActName] [nvarchar](500) NULL,
	[FileNumber] [nvarchar](200) NULL,
	[Pub_Date] [datetime] NULL,
	[Sta_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Depts] [nvarchar](500) NULL,
	[DeptIDs] [nvarchar](300) NULL,
	[Content] [nvarchar](Max) NULL,
	[State] [int] NULL CONSTRAINT [DF_Act_State]  DEFAULT ((0)),
	[OpDate] [datetime] NULL CONSTRAINT [DF_Act_OpDate]  DEFAULT (getdate()),
	[Effect] [int] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Act_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法规ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'ActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法规名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'ActName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法规文号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'FileNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颁布日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'Pub_Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'Sta_Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'失效日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'End_Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颁布机构' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'Depts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颁布机构ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'DeptIDs'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示全文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据状态：0默认，1已经删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'OpDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'效力属性：1、失效，2、有效，3、修正，4、颁布待生效，5、待失效，9、未知' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'Effect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act', @level2type=N'COLUMN',@level2name=N'ID'


/****新建法规拆条表****/
CREATE TABLE [dbo].[Act_Items](
	[ItemID] [int] NOT NULL,
	[ActID] [int] NOT NULL,
	[Item_Name] [nvarchar](200) NULL,
	[Item_Type] [int] NULL,
	[Item_ParentID] [int] NULL,
	[Orders] [int] NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Act_Items] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法规ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'ActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'Item_Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条类型:0、未知，1、序，2、附，3、章，4、条，5、目录，6、节；' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'Item_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条父级ID' , @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'Item_ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'Orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Items', @level2type=N'COLUMN',@level2name=N'Content'


/****新建法规注释表****/
CREATE TABLE [dbo].[ActClauseNote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToActName] [nvarchar](500) NULL,
	[ToActID] [int] NULL,
	[ToItemID] [int] NULL,
	[FromActName] [nvarchar](500) NULL,
	[FromActID] [int] NULL,
	[FromItemID] [int] NULL,
	[Content] [nvarchar](max) NULL,
	[Summary] [nvarchar](200) NULL,
	[source] [nvarchar](max) NULL,
	[FromItemName] [nvarchar](500) NULL,
 CONSTRAINT [PK_ActClauseNote] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被注释法规名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'ToActName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被注释法规ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'ToActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被注释法条ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'ToItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释法规名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'FromActName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释法规ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'FromActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释法条ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'FromItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'Summary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注释依据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条条目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActClauseNote', @level2type=N'COLUMN',@level2name=N'FromItemName'


/****新建法规关联表****/
CREATE TABLE [dbo].[Act_Correlation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ToActID] [int] NULL,
	[ToItemID] [int] NULL,
	[FromActID] [int] NULL,
	[FromItemID] [int] NULL,
	[FromActName] [nvarchar](500) NULL,
	[FromItemContent] [nvarchar](max) NULL,
	[FromItemName] [nvarchar](500) NULL,
 CONSTRAINT [PK_Act_Correlation_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法规ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'ToActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'ToItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联法规的ActID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'FromActID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联法规的条ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'FromItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联法规的标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'FromActName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联法规的条目内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'FromItemContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'法条条目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Act_Correlation', @level2type=N'COLUMN',@level2name=N'FromItemName'



/****新建颁布机构表****/
CREATE TABLE [dbo].[ActDeptLevelCode](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [nvarchar](50) NULL,
	[Layer] [int] NULL,
	[DeptName] [nvarchar](100) NULL,
 CONSTRAINT [PK_ActDeptLevelCode_1] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActDeptLevelCode', @level2type=N'COLUMN',@level2name=N'DeptID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'层级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActDeptLevelCode', @level2type=N'COLUMN',@level2name=N'ClassID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'层级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActDeptLevelCode', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颁布机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActDeptLevelCode', @level2type=N'COLUMN',@level2name=N'DeptName'
GO
