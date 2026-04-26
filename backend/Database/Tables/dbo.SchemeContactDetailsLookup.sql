CREATE TABLE [dbo].[SchemeContactDetailsLookup] (
    [Id]          INT  IDENTITY(1,1) NOT NULL,
    [SchemeId]    INT  NOT NULL,
    [Name]        NVARCHAR(200) NULL,
    [Phone]       NVARCHAR(50)  NULL,
    [Email]       NVARCHAR(200) NULL,
    [Designation] NVARCHAR(200) NULL,
    CONSTRAINT [PK_SchemeContactDetailsLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO