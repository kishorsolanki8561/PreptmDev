CREATE TABLE [dbo].[PageMaster] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [Name]    NVARCHAR(200)  NULL,
    [Icon]    NVARCHAR(100)  NULL,
    [PageUrl] NVARCHAR(500)  NULL,
    [MenuId]  INT            NULL,
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
    CONSTRAINT [PK_PageMaster] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
