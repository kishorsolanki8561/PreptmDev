-- Upserts a scheme record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_SchemeAddUpdate]
    @Id INT = NULL, @Title NVARCHAR(500), @TitleHindi NVARCHAR(500) = NULL, @DepartmentId INT = NULL, @StateId INT = NULL, @Slug NVARCHAR(500), @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
