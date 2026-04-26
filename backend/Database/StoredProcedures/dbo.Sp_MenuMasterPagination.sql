-- Admin paginated menu list.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_MenuMasterPagination]
    @Page INT, @PageSize INT, @Search NVARCHAR(500) = NULL, @OrderBy NVARCHAR(50) = NULL, @OrderByAsc BIT = 0, @IsActive BIT = NULL, @FromDate DATETIME2 = NULL, @ToDate DATETIME2 = NULL, @MenuName NVARCHAR(200) = NULL, @HashChild BIT = NULL, @ParentMenuId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
