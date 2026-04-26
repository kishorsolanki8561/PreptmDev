CREATE TABLE [dbo].[User] (
    [Id]          BIGINT    IDENTITY(1,1) NOT NULL,
    [Name]            NVARCHAR(300)  NULL,
    [Email]           NVARCHAR(300)  NULL,
    [Password]        NVARCHAR(500)  NULL,
    [UserTypeCode]    INT            NOT NULL DEFAULT 0,
    [IsAutoLoggedOut] BIT            NULL DEFAULT 0,
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
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
