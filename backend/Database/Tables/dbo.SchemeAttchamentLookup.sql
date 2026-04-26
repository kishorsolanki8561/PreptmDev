CREATE TABLE [dbo].[SchemeAttchamentLookup] (
    [Id]        INT  IDENTITY(1,1) NOT NULL,
    [SchemeId]  INT  NOT NULL,
    [Type]      INT  NOT NULL DEFAULT 0,
    [Url]       NVARCHAR(500) NULL,
    [Label]     NVARCHAR(200) NULL,
    [SortOrder] INT  NULL DEFAULT 0,
    CONSTRAINT [PK_SchemeAttchamentLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO