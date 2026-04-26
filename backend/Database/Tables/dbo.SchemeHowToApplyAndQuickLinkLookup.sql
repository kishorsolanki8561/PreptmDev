CREATE TABLE [dbo].[SchemeHowToApplyAndQuickLinkLookup] (
    [Id]                   INT              IDENTITY(1,1) NOT NULL,
    [SchemeId]             INT              NOT NULL,
    [Title]                NVARCHAR(500)    NULL,
    [TitleHindi]           NVARCHAR(500)    NULL,
    [Description]          NVARCHAR(MAX)    NULL,
    [DescriptionHindi]     NVARCHAR(MAX)    NULL,
    [DescriptionJson]      NVARCHAR(MAX)    NULL,
    [DescriptionHindiJson] NVARCHAR(MAX)    NULL,
    [IsQuickLink]          BIT              NOT NULL DEFAULT 0,
    [LinkUrl]              NVARCHAR(500)    NULL,
    [IconClass]            NVARCHAR(100)    NULL,
    CONSTRAINT [PK_SchemeHowToApplyAndQuickLinkLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SchemeHowToApplyQuickLink_SchemeId]
    ON [dbo].[SchemeHowToApplyAndQuickLinkLookup] ([SchemeId] ASC, [IsQuickLink] ASC);
GO
