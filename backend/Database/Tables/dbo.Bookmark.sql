CREATE TABLE [dbo].[Bookmark] (
    [Id]         BIGINT    IDENTITY(1,1) NOT NULL,
    [PostId]     BIGINT    NOT NULL,
    [UserId]     BIGINT    NOT NULL,
    [ModuleEnum] INT       NOT NULL DEFAULT 0,
    [IsActive]   BIT       NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Bookmark] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO