CREATE TABLE [dbo].[BlockContentsTags] (
    [Id]             INT  IDENTITY(1,1) NOT NULL,
    [BlockContentId] INT  NOT NULL,
    [TagId]          INT  NOT NULL,
    CONSTRAINT [PK_BlockContentsTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO