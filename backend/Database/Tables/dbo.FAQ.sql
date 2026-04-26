CREATE TABLE [dbo].[FAQ] (
    [Id]            INT              IDENTITY(1,1) NOT NULL,
    [ModuleId]      INT              NULL,
    [BlockTypeId]   INT              NULL,
    [Que]           NVARCHAR(500)    NULL,
    [Ans]           NVARCHAR(MAX)    NULL,
    [QueHindi]      NVARCHAR(500)    NULL,
    [AnsHindi]      NVARCHAR(MAX)    NULL,
    -- Audit
    [IsActive]      BIT              NOT NULL DEFAULT 1,
    [IsDelete]      BIT              NOT NULL DEFAULT 0,
    [CreatedDate]   DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate]  DATETIME2        NULL,
    [CreatedBy]     INT              NOT NULL DEFAULT 0,
    [ModifiedBy]    INT              NULL,
    [IPAddress]     NVARCHAR(50)     NULL,
    [IPCity]        NVARCHAR(100)    NULL,
    [IPCountry]     NVARCHAR(100)    NULL,
    [Browser]       NVARCHAR(200)    NULL,
    [ScreenName]    NVARCHAR(200)    NULL,
    CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_FAQ_BlockTypeId]
    ON [dbo].[FAQ] ([BlockTypeId] ASC)
    INCLUDE ([Que], [Ans]);
GO
