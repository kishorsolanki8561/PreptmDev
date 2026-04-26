-- Upserts an additional static page (About, Privacy, Terms, Manage Account).
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_AdditionalPagesAddUpdate]
    @Id INT = NULL, @PageType INT, @Content NVARCHAR(MAX) = NULL, @ContentHindi NVARCHAR(MAX) = NULL, @ContentJson NVARCHAR(MAX) = NULL, @ContentHindiJson NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
