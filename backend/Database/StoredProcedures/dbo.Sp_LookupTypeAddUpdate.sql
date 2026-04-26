-- Upserts a lookup type.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_LookupTypeAddUpdate]
    @Id INT = NULL, @Title NVARCHAR(300), @TitleHindi NVARCHAR(300) = NULL, @Description NVARCHAR(MAX) = NULL, @DescriptionHindi NVARCHAR(MAX) = NULL, @Slug NVARCHAR(200) = NULL, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
