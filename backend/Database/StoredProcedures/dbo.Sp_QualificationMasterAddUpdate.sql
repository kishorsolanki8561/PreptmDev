-- Upserts a qualification master record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_QualificationMasterAddUpdate]
    @Id INT = NULL, @Title NVARCHAR(300), @UserId INT, @TitleHindi NVARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
