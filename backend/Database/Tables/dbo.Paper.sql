CREATE TABLE [dbo].[Paper] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [Title]         NVARCHAR(500)  NOT NULL,
    [TitleHindi]    NVARCHAR(500)  NULL,
    [Description]   NVARCHAR(MAX)  NULL,
    [DescriptionHindi] NVARCHAR(MAX) NULL,
    [Keywords]      NVARCHAR(1000) NULL,
    [Thumbnail]     NVARCHAR(500)  NULL,
    [SlugUrl]       NVARCHAR(500)  NULL,
    [Status]        INT            NULL,
    [VisitCount]    INT            NULL DEFAULT 0,
    [PublisherId]   INT            NULL,
    [PublishedDate] DATETIME2      NULL,
    -- Audit
    [IsActive]    BIT              NOT NULL DEFAULT 1,
    [IsDelete]    BIT              NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME2       NULL,
    [CreatedBy]   INT              NOT NULL DEFAULT 0,
    [ModifiedBy]  INT              NULL,
    [IPAddress]   NVARCHAR(50)     NULL,
    [IPCity]      NVARCHAR(100)    NULL,
    [IPCountry]   NVARCHAR(100)    NULL,
    [Browser]     NVARCHAR(200)    NULL,
    [ScreenName]  NVARCHAR(200)    NULL,
    CONSTRAINT [PK_Paper] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
