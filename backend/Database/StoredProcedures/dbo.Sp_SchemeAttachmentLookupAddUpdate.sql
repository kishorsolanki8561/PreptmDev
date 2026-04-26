-- Upserts a scheme attachment lookup row.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_SchemeAttachmentLookupAddUpdate]
    @Id INT = NULL, @SchemeId INT, @Type INT, @Url NVARCHAR(500) = NULL, @Label NVARCHAR(200) = NULL, @SortOrder INT = 0, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
