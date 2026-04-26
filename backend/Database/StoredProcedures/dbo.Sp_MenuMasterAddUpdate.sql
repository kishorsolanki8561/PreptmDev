-- Upserts a menu master record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_MenuMasterAddUpdate]
    @Id INT = NULL, @MenuName NVARCHAR(200), @DisplayName NVARCHAR(200), @HashChild BIT = 0, @ParentId INT = NULL, @Position INT = 0, @IconClass NVARCHAR(100) = NULL, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
