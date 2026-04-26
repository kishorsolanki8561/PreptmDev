-- Upserts a recruitment record.
-- NOTE: Script the actual implementation from the live database via SSMS:
--   Right-click stored procedure > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE PROCEDURE [dbo].[Sp_RecruitmentAddUpdate]
    @Id BIGINT = NULL, @Title NVARCHAR(500), @TitleHindi NVARCHAR(500) = NULL, @DepartmentId INT = NULL, @CategoryId INT = NULL, @SubCategoryId INT = NULL, @BlockTypeCode INT = NULL, @StateId INT = NULL, @Salary NVARCHAR(200) = NULL, @Description NVARCHAR(MAX) = NULL, @DescriptionHindi NVARCHAR(MAX) = NULL, @DescriptionJson NVARCHAR(MAX) = NULL, @DescriptionHindiJson NVARCHAR(MAX) = NULL, @ShortDesription NVARCHAR(1000) = NULL, @ShortDesriptionHindi NVARCHAR(1000) = NULL, @HowTo NVARCHAR(MAX) = NULL, @MinAge INT = NULL, @MaxAge INT = NULL, @TotalPost BIGINT = NULL, @ExamMode INT = 0, @Status INT = NULL, @StartDate DATETIME2 = NULL, @LastDate DATETIME2 = NULL, @ExtendedDate DATETIME2 = NULL, @PublishedDate DATETIME2 = NULL, @FeePaymentLastDate DATETIME2 = NULL, @CorrectionLastDate DATETIME2 = NULL, @AdmitCardDate DATETIME2 = NULL, @ApplyLink NVARCHAR(500) = NULL, @OfficialLink NVARCHAR(500) = NULL, @NotificationLink NVARCHAR(500) = NULL, @OtherLinks NVARCHAR(MAX) = NULL, @SortLinks NVARCHAR(MAX) = NULL, @SlugUrl NVARCHAR(500) = NULL, @Thumbnail NVARCHAR(500) = NULL, @Keywords NVARCHAR(1000) = NULL, @IsPrivate BIT = 0, @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- TODO: paste implementation from live DB
    RAISERROR('Stub only — implement from live database.', 16, 1);
END
GO
