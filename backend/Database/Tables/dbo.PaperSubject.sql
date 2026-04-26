CREATE TABLE [dbo].[PaperSubject] (
    [Id]        INT  IDENTITY(1,1) NOT NULL,
    [PaperId]   INT  NOT NULL,
    [Subject]   NVARCHAR(300) NULL,
    [Url]       NVARCHAR(500) NULL,
    [SortOrder] INT  NULL DEFAULT 0,
    CONSTRAINT [PK_PaperSubject] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO