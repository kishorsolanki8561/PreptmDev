-- Submits or updates a contact/feedback message.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_UserFeedbackAddUpdate]
    @Id INT = NULL, @UserId BIGINT = NULL, @Status INT = 0, @Type INT, @Message NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
