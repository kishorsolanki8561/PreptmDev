CREATE TABLE [dbo].[ArticleTags] (
    [Id]        INT  IDENTITY(1,1) NOT NULL,
    [ArticleId] INT  NOT NULL,
    [TagId]     INT  NOT NULL,
    CONSTRAINT [PK_ArticleTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO