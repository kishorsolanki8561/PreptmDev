-- Upserts a front-end (social login) user record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_FrontUserAddUpdate]
    @Id BIGINT = NULL, @Name NVARCHAR(300) = NULL, @Email NVARCHAR(300) = NULL, @MobileNumber NVARCHAR(20) = NULL, @DateOfBirth DATETIME2 = NULL, @ProfileImg NVARCHAR(500) = NULL, @StateId INT = NULL, @AuthToken NVARCHAR(MAX) = NULL, @Provider NVARCHAR(100) = NULL, @FCMToken NVARCHAR(500) = NULL, @Platform NVARCHAR(50) = NULL, @UId NVARCHAR(200) = NULL, @FirstName NVARCHAR(150) = NULL, @LastName NVARCHAR(150) = NULL, @Language NVARCHAR(10) = 'en'
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
