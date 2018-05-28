
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/25/2018 15:48:51
-- Generated from EDMX file: F:\GitHub\CleanProcedure\CleanProcedure\Clean.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Clean];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clean_Card]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_Card];
GO
IF OBJECT_ID(N'[dbo].[Clean_Factory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_Factory];
GO
IF OBJECT_ID(N'[dbo].[Clean_Pretreatment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_Pretreatment];
GO
IF OBJECT_ID(N'[dbo].[Clean_Record]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_Record];
GO
IF OBJECT_ID(N'[dbo].[Clean_RecordList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_RecordList];
GO
IF OBJECT_ID(N'[dbo].[Dic_Area]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dic_Area];
GO
IF OBJECT_ID(N'[dbo].[Dic_UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dic_UserRole];
GO
IF OBJECT_ID(N'[dbo].[Dic_Workgroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dic_Workgroup];
GO
IF OBJECT_ID(N'[dbo].[Endoscopic_Device]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Endoscopic_Device];
GO
IF OBJECT_ID(N'[dbo].[Endoscopic_Mirror]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Endoscopic_Mirror];
GO
IF OBJECT_ID(N'[dbo].[Clean_CardDevice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_CardDevice];
GO
IF OBJECT_ID(N'[dbo].[Clean_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clean_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clean_Card'
CREATE TABLE [dbo].[Clean_Card] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CardNo] nvarchar(50)  NULL,
    [DevType] nvarchar(20)  NULL,
    [CardID] nvarchar(20)  NULL,
    [CardName] nvarchar(20)  NULL,
    [CardType] nvarchar(50)  NULL,
    [Department] nvarchar(20)  NULL,
    [ExamRoom] nvarchar(20)  NULL,
    [Uses] nvarchar(20)  NULL,
    [StartedDate] nvarchar(10)  NULL,
    [Stopped] bit  NOT NULL,
    [StoppedDate] varchar(10)  NULL,
    [StopReason] varchar(20)  NULL,
    [HandlingMan] varchar(20)  NULL,
    [DevRemark] varchar(40)  NULL
);
GO

-- Creating table 'Clean_Factory'
CREATE TABLE [dbo].[Clean_Factory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DevType] varchar(20)  NOT NULL,
    [FcName] varchar(20)  NULL
);
GO

-- Creating table 'Clean_Pretreatment'
CREATE TABLE [dbo].[Clean_Pretreatment] (
    [Row] int IDENTITY(1,1) NOT NULL,
    [CleanUser] nvarchar(20)  NULL,
    [CleanCreateTime] datetime  NULL,
    [CleanWorkStation] nvarchar(20)  NULL,
    [CleanStatus] bit  NOT NULL
);
GO

-- Creating table 'Clean_Record'
CREATE TABLE [dbo].[Clean_Record] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CleanID] nvarchar(20)  NOT NULL,
    [CardIp] nvarchar(20)  NULL,
    [CleanGroup] nvarchar(20)  NULL,
    [CardID] nvarchar(20)  NULL,
    [CardName] nvarchar(20)  NULL,
    [MarrorID] nvarchar(20)  NULL,
    [MarrorName] nvarchar(20)  NULL,
    [CleanType] nvarchar(20)  NULL,
    [CleanStatus] nvarchar(6)  NULL,
    [CleanCreateTime] datetime  NULL,
    [CleanDate] varchar(10)  NULL,
    [NowStep] varchar(5)  NULL,
    [NowStepName] varchar(10)  NULL,
    [NowStepTime] nvarchar(20)  NULL,
    [CleanStart] datetime  NULL,
    [CleanStop] datetime  NULL,
    [CleanTime] varchar(10)  NULL,
    [CleanCardName] nvarchar(200)  NULL,
    [CleanDetail] nvarchar(100)  NULL,
    [CleanAllTime] nvarchar(100)  NULL,
    [UseStatus] nvarchar(20)  NULL,
    [ZuofeiID] nvarchar(20)  NULL,
    [ZuofeiTime] varchar(50)  NULL,
    [Endoscopic] nvarchar(20)  NULL,
    [PatientID] nvarchar(20)  NULL,
    [PatientName] nvarchar(20)  NULL,
    [Sex] nvarchar(4)  NULL,
    [Age] nvarchar(4)  NULL,
    [ExamRoom] nvarchar(20)  NULL,
    [ExamDoctor] nvarchar(20)  NULL,
    [ExamNurse] nvarchar(20)  NULL,
    [ExamDate] nvarchar(20)  NULL,
    [ExamTime] nvarchar(20)  NULL,
    [ExamComputer] nvarchar(20)  NULL,
    [ReportDoctor] nvarchar(20)  NULL,
    [HP] nvarchar(20)  NULL,
    [Negative] nvarchar(6)  NULL,
    [Report] nvarchar(max)  NULL,
    [Remark] nvarchar(200)  NULL,
    [SevereTag] nvarchar(20)  NULL,
    [OtherInfect] nvarchar(20)  NULL,
    [MarrorStatus] bit  NOT NULL,
    [MarrorType] nvarchar(50)  NULL,
    [MarrorCleanPerson] nvarchar(40)  NULL
);
GO

-- Creating table 'Clean_RecordList'
CREATE TABLE [dbo].[Clean_RecordList] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CleanCard] nchar(20)  NOT NULL,
    [CleanIp] nvarchar(20)  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [CleanPort] nvarchar(10)  NOT NULL,
    [StepTime] int  NOT NULL,
    [StepName] nvarchar(20)  NOT NULL,
    [StepNum] int  NOT NULL,
    [MaxNum] int  NOT NULL,
    [WorkCard] nchar(20)  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [Sequence] int  NOT NULL
);
GO

-- Creating table 'Dic_Area'
CREATE TABLE [dbo].[Dic_Area] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AreaNo] int  NULL,
    [AreaName] varchar(20)  NULL
);
GO

-- Creating table 'Dic_UserRole'
CREATE TABLE [dbo].[Dic_UserRole] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleNum] varchar(20)  NULL,
    [RoleName] varchar(20)  NULL,
    [RoleAttr0] varchar(20)  NULL,
    [RoleAttr1] varchar(20)  NULL,
    [RoleAttr2] varchar(20)  NULL,
    [RoleAttr3] varchar(20)  NULL,
    [RoleAttr4] varchar(20)  NULL,
    [RoleAttr5] varchar(20)  NULL,
    [RoleAttr6] varchar(20)  NULL,
    [RoleAttr7] varchar(20)  NULL
);
GO

-- Creating table 'Dic_Workgroup'
CREATE TABLE [dbo].[Dic_Workgroup] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [WorkGroupNum] int  NULL,
    [Name] varchar(20)  NULL
);
GO

-- Creating table 'Endoscopic_Device'
CREATE TABLE [dbo].[Endoscopic_Device] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DevName] varchar(20)  NULL,
    [DevFac] varchar(20)  NULL,
    [Department] varchar(20)  NULL,
    [ExamRoom] varchar(20)  NULL,
    [DevRemark] varchar(40)  NULL
);
GO

-- Creating table 'Endoscopic_Mirror'
CREATE TABLE [dbo].[Endoscopic_Mirror] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MirrorID] varchar(20)  NULL,
    [MirrorCardID] varchar(20)  NULL,
    [DevName] varchar(20)  NULL,
    [DevFac] varchar(20)  NULL,
    [Department] varchar(20)  NULL,
    [ExamRoom] varchar(20)  NULL,
    [Uses] varchar(20)  NULL,
    [DevRemark] varchar(40)  NULL
);
GO

-- Creating table 'Clean_CardDevice'
CREATE TABLE [dbo].[Clean_CardDevice] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CardNo] nvarchar(20)  NULL,
    [CardName] nvarchar(20)  NULL,
    [DevType] nvarchar(20)  NULL,
    [StepCount] smallint  NULL,
    [StepNumber] smallint  NULL,
    [StepName] nvarchar(20)  NULL,
    [StepTime] nvarchar(20)  NULL,
    [StepType] nvarchar(20)  NULL,
    [LastStepNumber] smallint  NULL,
    [LastStepName] nvarchar(20)  NULL,
    [LastStepTime] nvarchar(20)  NULL,
    [DeviceName] nvarchar(40)  NULL,
    [ClientIP] nvarchar(20)  NULL,
    [ClientPort] nvarchar(10)  NULL,
    [WorkStationIP] nvarchar(20)  NULL,
    [CleanID] nvarchar(20)  NULL,
    [ServerIp] varchar(20)  NULL,
    [ServerPort] varchar(5)  NULL,
    [CleanGroup] nvarchar(20)  NULL,
    [StartedDate] nvarchar(10)  NULL,
    [Stopped] bit  NULL,
    [StoppedDate] nvarchar(10)  NULL,
    [StopReason] nvarchar(100)  NULL,
    [HandlingMan] nvarchar(10)  NULL,
    [MsgIp] nvarchar(20)  NULL,
    [Remark] nvarchar(100)  NULL,
    [MarrorID] nvarchar(20)  NULL,
    [MarrorName] nvarchar(20)  NULL,
    [MarrorType] nvarchar(50)  NULL
);
GO

-- Creating table 'Clean_User'
CREATE TABLE [dbo].[Clean_User] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(20)  NULL,
    [LoginName] nvarchar(20)  NULL,
    [CardID] nvarchar(20)  NULL,
    [UserName] nvarchar(20)  NULL,
    [Password] nvarchar(20)  NULL,
    [RoleNum] smallint  NULL,
    [WorkGroupNum] smallint  NULL,
    [Department] nvarchar(20)  NULL,
    [Signature] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Clean_Card'
ALTER TABLE [dbo].[Clean_Card]
ADD CONSTRAINT [PK_Clean_Card]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Clean_Factory'
ALTER TABLE [dbo].[Clean_Factory]
ADD CONSTRAINT [PK_Clean_Factory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Row] in table 'Clean_Pretreatment'
ALTER TABLE [dbo].[Clean_Pretreatment]
ADD CONSTRAINT [PK_Clean_Pretreatment]
    PRIMARY KEY CLUSTERED ([Row] ASC);
GO

-- Creating primary key on [ID] in table 'Clean_Record'
ALTER TABLE [dbo].[Clean_Record]
ADD CONSTRAINT [PK_Clean_Record]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Clean_RecordList'
ALTER TABLE [dbo].[Clean_RecordList]
ADD CONSTRAINT [PK_Clean_RecordList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'Dic_Area'
ALTER TABLE [dbo].[Dic_Area]
ADD CONSTRAINT [PK_Dic_Area]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Dic_UserRole'
ALTER TABLE [dbo].[Dic_UserRole]
ADD CONSTRAINT [PK_Dic_UserRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Dic_Workgroup'
ALTER TABLE [dbo].[Dic_Workgroup]
ADD CONSTRAINT [PK_Dic_Workgroup]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Endoscopic_Device'
ALTER TABLE [dbo].[Endoscopic_Device]
ADD CONSTRAINT [PK_Endoscopic_Device]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Endoscopic_Mirror'
ALTER TABLE [dbo].[Endoscopic_Mirror]
ADD CONSTRAINT [PK_Endoscopic_Mirror]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Clean_CardDevice'
ALTER TABLE [dbo].[Clean_CardDevice]
ADD CONSTRAINT [PK_Clean_CardDevice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Clean_User'
ALTER TABLE [dbo].[Clean_User]
ADD CONSTRAINT [PK_Clean_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------