CREATE TABLE [dbo].[Logs] (
    [Id]          BIGINT    IDENTITY(1,1) NOT NULL,
    [Message]     NVARCHAR(MAX)  NULL,
    [Level]       NVARCHAR(50)   NULL,
    [TimeStamp]   DATETIME2      NULL DEFAULT GETUTCDATE(),
    [Exception]   NVARCHAR(MAX)  NULL,
    [Properties]  NVARCHAR(MAX)  NULL,
    [MachineName] NVARCHAR(200)  NULL,
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
    CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
