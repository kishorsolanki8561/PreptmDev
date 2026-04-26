CREATE TABLE [dbo].[RecruitmentDocumentLookup] (
    [Id]            INT  IDENTITY(1,1) NOT NULL,
    [RecruitmentId] INT  NOT NULL,
    [Document]      NVARCHAR(MAX) NULL,
    [SortOrder]     INT  NULL DEFAULT 0,
    CONSTRAINT [PK_RecruitmentDocumentLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO