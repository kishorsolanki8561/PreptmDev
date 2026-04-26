CREATE TABLE [dbo].[ArticleFaq] (
    [Id]        INT  IDENTITY(1,1) NOT NULL,
    [ArticleId] INT  NOT NULL,
    [Question]  NVARCHAR(500) NULL,
    [Answer]    NVARCHAR(MAX) NULL,
    [SortOrder] INT  NULL DEFAULT 0,
    CONSTRAINT [PK_ArticleFaq] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO