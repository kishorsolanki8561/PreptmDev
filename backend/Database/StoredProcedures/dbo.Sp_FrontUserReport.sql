-- Admin report of front-end registered users.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontUserReport]
    @Page INT, @PageSize INT, @Search NVARCHAR(500) = NULL, @OrderBy NVARCHAR(50) = NULL, @OrderByAsc BIT = 0, @IsActive BIT = NULL, @Name NVARCHAR(300) = NULL, @Email NVARCHAR(300) = NULL, @FromDate DATETIME2 = NULL, @ToDate DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
