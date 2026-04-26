CREATE TABLE [dbo].[Note_Subject] (
    [Id]        INT  IDENTITY(1,1) NOT NULL,
    [NoteId]    INT  NOT NULL,
    [Subject]   NVARCHAR(300) NULL,
    [Url]       NVARCHAR(500) NULL,
    [SortOrder] INT  NULL DEFAULT 0,
    CONSTRAINT [PK_Note_Subject] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO