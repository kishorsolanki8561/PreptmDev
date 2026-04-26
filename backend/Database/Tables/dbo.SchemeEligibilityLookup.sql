CREATE TABLE [dbo].[SchemeEligibilityLookup] (
    [Id]          INT  IDENTITY(1,1) NOT NULL,
    [SchemeId]    INT  NOT NULL,
    [Eligibility] NVARCHAR(MAX) NULL,
    [SortOrder]   INT  NULL DEFAULT 0,
    CONSTRAINT [PK_SchemeEligibilityLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO