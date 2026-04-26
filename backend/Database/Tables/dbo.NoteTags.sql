CREATE TABLE [dbo].[NoteTags] (
    [Id]     INT  IDENTITY(1,1) NOT NULL,
    [NoteId] INT  NOT NULL,
    [TagId]  INT  NOT NULL,
    CONSTRAINT [PK_NoteTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO