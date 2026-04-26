-- Paginated listing across all content types for the public dashboard.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontDashboardPagination]
    @Page INT, @PageSize INT, @OrderBy NVARCHAR(50) = NULL, @OrderByAsc BIT = 0, @FromDate DATETIME2 = NULL, @ToDate DATETIME2 = NULL, @Title NVARCHAR(500) = NULL, @DepartmentId INT = NULL, @QualificationId INT = NULL, @JobDesignationId INT = NULL, @CategorySlug NVARCHAR(200) = NULL, @SubCategorySlug NVARCHAR(200) = NULL, @BlockTypeSlug NVARCHAR(200) = NULL, @EligibilityId INT = NULL, @CategoryId INT = NULL, @UserId BIGINT = NULL, @SearchText NVARCHAR(500) = NULL, @LanguageCode NVARCHAR(10) = 'en', @StateId INT = NULL, @IsPrivate BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
