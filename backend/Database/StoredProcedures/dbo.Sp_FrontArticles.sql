-- Paginated article list with type/tag filters.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontArticles]
    @Page INT, @PageSize INT, @OrderBy NVARCHAR(50) = NULL, @OrderByAsc BIT = 0, @Title NVARCHAR(500) = NULL, @ArticleTypeSlug NVARCHAR(200) = NULL, @TagTypeSlug NVARCHAR(200) = NULL, @Search NVARCHAR(500) = NULL, @LanguageCode NVARCHAR(10) = 'en'
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
