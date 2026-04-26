CREATE TABLE [dbo].[RecruitmentJobDesignationLookup] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [RecruitmentId]    INT  NOT NULL,
    [JobDesignationId] INT  NOT NULL,
    [TotalVacancy]     INT  NULL DEFAULT 0,
    CONSTRAINT [PK_RecruitmentJobDesignationLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO