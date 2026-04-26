-- Paginated block content (admit card, result, answer key, etc.) with filters.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontBlockContentPagination]
    @Page INT, @PageSize INT, @Search NVARCHAR(500) = NULL, @OrderBy NVARCHAR(50) = NULL, @OrderByAsc BIT = 0, @FromDate DATETIME2 = NULL, @ToDate DATETIME2 = NULL, @Title NVARCHAR(500) = NULL, @DepartmentId INT = NULL, @CategoryId INT = NULL, @SubCategoryId INT = NULL, @GroupId INT = NULL, @RecruitmentId INT = NULL, @UserId BIGINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
