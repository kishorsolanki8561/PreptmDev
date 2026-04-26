CREATE TABLE [dbo].[Syllabus_Subject] (
    [Id]         INT  IDENTITY(1,1) NOT NULL,
    [SyllabusId] INT  NOT NULL,
    [Subject]    NVARCHAR(300) NULL,
    [Url]        NVARCHAR(500) NULL,
    [SortOrder]  INT  NULL DEFAULT 0,
    CONSTRAINT [PK_Syllabus_Subject] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO