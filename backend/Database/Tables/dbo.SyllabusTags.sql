CREATE TABLE [dbo].[SyllabusTags] (
    [Id]         INT  IDENTITY(1,1) NOT NULL,
    [SyllabusId] INT  NOT NULL,
    [TagId]      INT  NOT NULL,
    CONSTRAINT [PK_SyllabusTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO