-- Upserts a block content record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_BlockContentsAddUpdate]
    @Id INT = NULL, @Title NVARCHAR(500), @TitleHindi NVARCHAR(500) = NULL, @BlockTypeId INT, @RecruitmentId INT = NULL, @DepartmentId INT = NULL, @CategoryId INT = NULL, @SubCategoryId INT = NULL, @GroupId INT = NULL, @SlugUrl NVARCHAR(500), @Description NVARCHAR(MAX) = NULL, @DescriptionHindi NVARCHAR(MAX) = NULL, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
