CREATE TABLE [dbo].[PageComponentAction] (
    [Id]          INT  IDENTITY(1,1) NOT NULL,
    [PageId]      INT  NOT NULL,
    [ComponentId] INT  NOT NULL,
    [Action]      NVARCHAR(100) NULL,
    CONSTRAINT [PK_PageComponentAction] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO