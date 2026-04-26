-- Correct-spelling version of SchemeAttchamentLookup (which has a typo).
-- Run this migration to create the properly-named table that Sp_SchemeDetailsOfIdAndSlug expects.
-- The SP uses OBJECT_ID guards so it works with either table name in the meantime.
CREATE TABLE [dbo].[SchemeAttachmentLookup] (
    [Id]          INT              IDENTITY(1,1) NOT NULL,
    [SchemeId]    INT              NOT NULL,
    [Title]       NVARCHAR(200)    NULL,
    [Description] NVARCHAR(MAX)    NULL,
    [Path]        NVARCHAR(500)    NOT NULL,
    [Type]        INT              NOT NULL DEFAULT 0,  -- matches ATTACHMENT_TYPE enum
    CONSTRAINT [PK_SchemeAttachmentLookup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_SchemeAttachmentLookup_SchemeId]
    ON [dbo].[SchemeAttachmentLookup] ([SchemeId] ASC);
GO
