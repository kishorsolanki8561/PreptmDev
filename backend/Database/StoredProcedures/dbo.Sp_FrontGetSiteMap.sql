-- Returns all public URLs for XML sitemap generation. Each loc must be prefixed with https://www.preptm.com/ (en) or https://www.preptm.com/hi/ (hi) by the calling service before writing to XML. Only returns records where IsActive = 1 AND IsDeleted = 0.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontGetSiteMap]
    @langCode NVARCHAR(10) = 'en'
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
