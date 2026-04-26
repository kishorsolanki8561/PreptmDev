CREATE TABLE [dbo].[AdditionalPages] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [PageType]         INT           NOT NULL,
    [Content]          NVARCHAR(MAX)  NULL,
    [ContentHindi]     NVARCHAR(MAX)  NULL,
    [ContentJson]      NVARCHAR(MAX)  NULL,
    [ContentHindiJson] NVARCHAR(MAX)  NULL,
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
    CONSTRAINT [PK_AdditionalPages] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
