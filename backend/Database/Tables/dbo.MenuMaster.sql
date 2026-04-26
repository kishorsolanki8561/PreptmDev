CREATE TABLE [dbo].[MenuMaster] (
    [Id]          INT    IDENTITY(1,1) NOT NULL,
    [MenuName]    NVARCHAR(200)  NULL,
    [DisplayName] NVARCHAR(200)  NULL,
    [HashChild]   BIT            NULL DEFAULT 0,
    [ParentId]    INT            NULL,
    [Position]    INT            NULL DEFAULT 0,
    [IconClass]   NVARCHAR(100)  NULL,
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
    CONSTRAINT [PK_MenuMaster] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
