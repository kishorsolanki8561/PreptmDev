CREATE TABLE [dbo].[RecruitmentQualificationLookup] (
    [Id]              INT  IDENTITY(1,1) NOT NULL,
    [RecruitmentId]   INT  NOT NULL,
    [QualificationId] INT  NOT NULL,
    CONSTRAINT [PK_RecruitmentQualificationLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO