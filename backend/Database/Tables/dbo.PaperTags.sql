CREATE TABLE [dbo].[PaperTags] (
    [Id]      INT  IDENTITY(1,1) NOT NULL,
    [PaperId] INT  NOT NULL,
    [TagId]   INT  NOT NULL,
    CONSTRAINT [PK_PaperTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO