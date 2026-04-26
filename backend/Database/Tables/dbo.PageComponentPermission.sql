CREATE TABLE [dbo].[PageComponentPermission] (
    [Id]              INT  IDENTITY(1,1) NOT NULL,
    [PageComponentId] INT  NOT NULL,
    [UserTypeCode]    INT  NOT NULL,
    [CanRead]         BIT  NOT NULL DEFAULT 0,
    [CanWrite]        BIT  NOT NULL DEFAULT 0,
    [CanDelete]       BIT  NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PageComponentPermission] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO