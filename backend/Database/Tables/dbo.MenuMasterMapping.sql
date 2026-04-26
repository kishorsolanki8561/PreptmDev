CREATE TABLE [dbo].[MenuMasterMapping] (
    [Id]           INT  IDENTITY(1,1) NOT NULL,
    [MenuId]       INT  NOT NULL,
    [UserTypeCode] INT  NOT NULL,
    CONSTRAINT [PK_MenuMasterMapping] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO