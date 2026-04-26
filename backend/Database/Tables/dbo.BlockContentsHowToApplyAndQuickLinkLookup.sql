CREATE TABLE [dbo].[BlockContentsHowToApplyAndQuickLinkLookup] (
    [Id]             INT  IDENTITY(1,1) NOT NULL,
    [BlockContentId] INT  NOT NULL,
    [Type]           INT  NOT NULL DEFAULT 0,
    [Text]           NVARCHAR(MAX) NULL,
    [Url]            NVARCHAR(500) NULL,
    [Label]          NVARCHAR(200) NULL,
    [SortOrder]      INT  NULL DEFAULT 0,
    CONSTRAINT [PK_BlockContentsHowToApplyAndQuickLinkLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO