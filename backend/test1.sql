IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RecruitmentHowToApplyAndQuickLinkLookup]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [RecruitmentHowToApplyAndQuickLinkLookup] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [RecruitmentHowToApplyAndQuickLinkLookup] ALTER COLUMN [Id] int NOT NULL;
GO

CREATE TABLE [Papers] (
    [Id] integer NOT NULL IDENTITY,
    [CategoryId] integer NULL,
    [DepartmentId] integer NOT NULL,
    [QualificationId] integer NULL,
    [StateId] integer NULL,
    [TitleHindi] nvarchar(150) NOT NULL,
    [TitleHindi1] nvarchar(max) NOT NULL,
    [SlugUrl] nvarchar(Max) NOT NULL,
    [Description] nvarchar(Max) NOT NULL,
    [DescriptionHindi] nvarchar(Max) NOT NULL,
    [Keywords] nvarchar(Max) NOT NULL,
    [KeywordsHindi] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_Papers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PaperSubjects] (
    [Id] integer NOT NULL IDENTITY,
    [PaperId] integer NOT NULL,
    [SubjectName] nvarchar(150) NOT NULL,
    [SubjectNameHindi] nvarchar(150) NOT NULL,
    [YearId] integer NULL,
    [Path] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_PaperSubjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaperSubjects_Papers_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Papers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PaperTags] (
    [Id] integer NOT NULL IDENTITY,
    [TagId] integer NOT NULL,
    [PaperId] integer NOT NULL,
    CONSTRAINT [PK_PaperTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaperTags_Papers_PaperId] FOREIGN KEY ([PaperId]) REFERENCES [Papers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_PaperSubjects_PaperId] ON [PaperSubjects] ([PaperId]);
GO

CREATE INDEX [IX_PaperTags_PaperId] ON [PaperTags] ([PaperId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221060549_Paper_Kishor_20241221_V1', N'6.0.32');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Notes] (
    [Id] integer NOT NULL IDENTITY,
    [CategoryId] integer NULL,
    [DepartmentId] integer NOT NULL,
    [QualificationId] integer NULL,
    [StateId] integer NULL,
    [TitleHindi] nvarchar(150) NOT NULL,
    [TitleHindi1] nvarchar(max) NOT NULL,
    [SlugUrl] nvarchar(Max) NOT NULL,
    [Description] nvarchar(Max) NOT NULL,
    [DescriptionHindi] nvarchar(Max) NOT NULL,
    [Keywords] nvarchar(Max) NOT NULL,
    [KeywordsHindi] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NoteSubjects] (
    [Id] integer NOT NULL IDENTITY,
    [NoteId] integer NOT NULL,
    [SubjectName] nvarchar(150) NOT NULL,
    [SubjectNameHindi] nvarchar(150) NOT NULL,
    [YearId] integer NOT NULL,
    [Path] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_NoteSubjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NoteSubjects_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [NoteTags] (
    [Id] integer NOT NULL IDENTITY,
    [TagId] integer NOT NULL,
    [NoteId] integer NOT NULL,
    CONSTRAINT [PK_NoteTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NoteTags_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_NoteSubjects_NoteId] ON [NoteSubjects] ([NoteId]);
GO

CREATE INDEX [IX_NoteTags_NoteId] ON [NoteTags] ([NoteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221065028_Notes_Kishor_20241221_V2', N'6.0.32');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Syllabus] (
    [Id] integer NOT NULL IDENTITY,
    [CategoryId] integer NULL,
    [DepartmentId] integer NOT NULL,
    [QualificationId] integer NULL,
    [StateId] integer NULL,
    [TitleHindi] nvarchar(150) NOT NULL,
    [TitleHindi1] nvarchar(max) NOT NULL,
    [SlugUrl] nvarchar(Max) NOT NULL,
    [Description] nvarchar(Max) NOT NULL,
    [DescriptionHindi] nvarchar(Max) NOT NULL,
    [Keywords] nvarchar(Max) NOT NULL,
    [KeywordsHindi] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_Syllabus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SyllabusSubjects] (
    [Id] integer NOT NULL IDENTITY,
    [SyllabusId] integer NOT NULL,
    [SubjectName] nvarchar(150) NOT NULL,
    [SubjectNameHindi] nvarchar(150) NOT NULL,
    [YearId] integer NOT NULL,
    [Path] nvarchar(Max) NOT NULL,
    [IsActive] bit NOT NULL,
    [IsDelete] bit NOT NULL,
    [ModifiedDate] datetime2 NULL,
    [CreatedDate] datetime2 NOT NULL,
    [ModifiedBy] int NULL,
    [CreatedBy] int NOT NULL,
    [IPAddress] nvarchar(max) NULL,
    [IPCity] nvarchar(max) NULL,
    [IPCountry] nvarchar(max) NULL,
    [Browser] nvarchar(max) NULL,
    [ScreenName] nvarchar(max) NULL,
    CONSTRAINT [PK_SyllabusSubjects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SyllabusSubjects_Syllabus_SyllabusId] FOREIGN KEY ([SyllabusId]) REFERENCES [Syllabus] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SyllabusTags] (
    [Id] integer NOT NULL IDENTITY,
    [TagId] integer NOT NULL,
    [SyllabusId] integer NOT NULL,
    CONSTRAINT [PK_SyllabusTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SyllabusTags_Syllabus_SyllabusId] FOREIGN KEY ([SyllabusId]) REFERENCES [Syllabus] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SyllabusSubjects_SyllabusId] ON [SyllabusSubjects] ([SyllabusId]);
GO

CREATE INDEX [IX_SyllabusTags_SyllabusId] ON [SyllabusTags] ([SyllabusId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221070958_Syllabus_Kishor_20241221_V3', N'6.0.32');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Syllabus]') AND [c].[name] = N'TitleHindi');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Syllabus] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Syllabus] ALTER COLUMN [TitleHindi] nvarchar(250) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Papers]') AND [c].[name] = N'TitleHindi');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Papers] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Papers] ALTER COLUMN [TitleHindi] nvarchar(250) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'TitleHindi');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Notes] ALTER COLUMN [TitleHindi] nvarchar(250) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221072455_Paper_notes_syllabus_Kishor_20241221_V4', N'6.0.32');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Syllabus]') AND [c].[name] = N'TitleHindi1');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Syllabus] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Syllabus] DROP COLUMN [TitleHindi1];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Papers]') AND [c].[name] = N'TitleHindi1');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Papers] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Papers] DROP COLUMN [TitleHindi1];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'TitleHindi1');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Notes] DROP COLUMN [TitleHindi1];
GO

ALTER TABLE [Syllabus] ADD [Title] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Papers] ADD [Title] nvarchar(250) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Notes] ADD [Title] nvarchar(250) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221072720_Paper_notes_syllabus_Kishor_20241221_V5', N'6.0.32');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Syllabus] ADD [DescriptionJson] nvarchar(Max) NULL;
GO

ALTER TABLE [Syllabus] ADD [DescriptionJsonHindi] nvarchar(Max) NULL;
GO

ALTER TABLE [Syllabus] ADD [PublisherDate] DateTime NULL;
GO

ALTER TABLE [Syllabus] ADD [PublisherId] integer NULL;
GO

ALTER TABLE [Syllabus] ADD [Status] integer NULL;
GO

ALTER TABLE [Syllabus] ADD [VisitCount] integer NULL;
GO

ALTER TABLE [Papers] ADD [DescriptionJson] nvarchar(Max) NULL;
GO

ALTER TABLE [Papers] ADD [DescriptionJsonHindi] nvarchar(Max) NULL;
GO

ALTER TABLE [Papers] ADD [PublisherDate] DateTime NULL;
GO

ALTER TABLE [Papers] ADD [PublisherId] integer NULL;
GO

ALTER TABLE [Papers] ADD [Status] integer NULL;
GO

ALTER TABLE [Papers] ADD [VisitCount] integer NULL;
GO

ALTER TABLE [Notes] ADD [DescriptionJson] nvarchar(Max) NULL;
GO

ALTER TABLE [Notes] ADD [DescriptionJsonHindi] nvarchar(Max) NULL;
GO

ALTER TABLE [Notes] ADD [PublisherDate] DateTime NULL;
GO

ALTER TABLE [Notes] ADD [PublisherId] integer NULL;
GO

ALTER TABLE [Notes] ADD [Status] integer NULL;
GO

ALTER TABLE [Notes] ADD [VisitCount] integer NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221100219_Paper_notes_syllabus_Kishor_20241221_V6', N'6.0.32');
GO

COMMIT;
GO

