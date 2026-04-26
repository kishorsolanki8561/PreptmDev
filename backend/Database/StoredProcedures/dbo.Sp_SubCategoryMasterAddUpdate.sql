-- Upserts a sub-category master record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_SubCategoryMasterAddUpdate]
    @Id INT = NULL, @Name NVARCHAR(200), @SlugUrl NVARCHAR(200), @CategoryId INT, @Icon NVARCHAR(500) = NULL, @UserId INT, @NameHindi NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
