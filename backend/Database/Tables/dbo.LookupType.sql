CREATE TABLE [dbo].[LookupType] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [Title]            NVARCHAR(300)  NULL,
    [TitleHindi]       NVARCHAR(300)  NULL,
    [Description]      NVARCHAR(MAX)  NULL,
    [DescriptionHindi] NVARCHAR(MAX)  NULL,
    [Slug]             NVARCHAR(200)  NULL,
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
    CONSTRAINT [PK_LookupType] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
