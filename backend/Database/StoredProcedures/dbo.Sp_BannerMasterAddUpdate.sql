-- Upserts a banner record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_BannerMasterAddUpdate]
    @Id INT = NULL, @Title NVARCHAR(300), @TitleHindi NVARCHAR(300) = NULL, @URL NVARCHAR(500) = NULL, @AttachmentUrl NVARCHAR(500) = NULL, @IsAdvt BIT = 0, @DisplayOrder INT = 0, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
