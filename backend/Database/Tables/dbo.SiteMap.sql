CREATE TABLE [dbo].[SiteMap] (
    [Id]              INT           IDENTITY(1,1) NOT NULL,
    [SlugUrl]         NVARCHAR(500) NULL,
    [ModifiedDate]    DATETIME2     NULL,
    [ModuleName]      NVARCHAR(200) NULL,
    [ModuleNameHindi] NVARCHAR(200) NULL,
    CONSTRAINT [PK_SiteMap] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
GO

CREATE NONCLUSTERED INDEX [IX_Sitemap_LangPriority]
    ON [dbo].[SiteMap] ([ModuleName] ASC)
    INCLUDE ([SlugUrl], [ModifiedDate]);
GO