CREATE TABLE [dbo].[RecruitmentTags] (
    [Id]            INT  IDENTITY(1,1) NOT NULL,
    [RecruitmentId] INT  NOT NULL,
    [TagId]         INT  NOT NULL,
    CONSTRAINT [PK_RecruitmentTags] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO