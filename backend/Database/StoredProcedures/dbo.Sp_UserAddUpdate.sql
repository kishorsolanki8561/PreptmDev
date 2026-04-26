-- Upserts an admin user record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_UserAddUpdate]
    @Id BIGINT = NULL, @Name NVARCHAR(300), @Email NVARCHAR(300), @Password NVARCHAR(500) = NULL, @UserTypeCode INT, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
