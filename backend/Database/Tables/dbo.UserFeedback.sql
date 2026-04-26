CREATE TABLE [dbo].[UserFeedback] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [UserId]  INT           NULL,
    [Type]    INT           NOT NULL DEFAULT 0,
    [Status]  INT           NOT NULL DEFAULT 0,
    [Message] NVARCHAR(MAX) NULL,
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
    CONSTRAINT [PK_UserFeedback] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
