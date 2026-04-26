-- Upserts a department master record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_DepartmentMasterAddUpdate]
    @Id INT = NULL, @Name NVARCHAR(300), @Url NVARCHAR(500) = NULL, @ShortName NVARCHAR(100) = NULL, @Logo NVARCHAR(500) = NULL, @Address NVARCHAR(500) = NULL, @MapUrl NVARCHAR(500) = NULL, @Email NVARCHAR(200) = NULL, @PhoneNumber NVARCHAR(50) = NULL, @StateId INT = NULL, @SlugUrl NVARCHAR(500), @UserId INT, @Description NVARCHAR(MAX) = NULL, @FaceBookLink NVARCHAR(500) = NULL, @TwitterLink NVARCHAR(500) = NULL, @NameHindi NVARCHAR(300) = NULL, @AddressHindi NVARCHAR(500) = NULL, @DescriptionHindi NVARCHAR(MAX) = NULL, @WikipediaEnglishUrl NVARCHAR(500) = NULL, @WikipediaHindiUrl NVARCHAR(500) = NULL, @DescriptionJson NVARCHAR(MAX) = NULL, @DescriptionHindiJson NVARCHAR(MAX) = NULL, @ShortDescription NVARCHAR(1000) = NULL, @ShortDescriptionHindi NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
