CREATE TABLE [dbo].[PageComponent] (
    [Id]     INT  IDENTITY(1,1) NOT NULL,
    [Name]   NVARCHAR(200) NULL,
    [PageId] INT  NOT NULL,
    CONSTRAINT [PK_PageComponent] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO