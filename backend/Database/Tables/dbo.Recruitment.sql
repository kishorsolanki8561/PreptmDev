CREATE TABLE [dbo].[Recruitment] (
    [Id]                         BIGINT           IDENTITY(1,1) NOT NULL,
    [Title]                      NVARCHAR(500)    NULL,
    [TitleHindi]                 NVARCHAR(500)    NULL,
    [DepartmentId]               INT              NULL,
    [CategoryId]                 INT              NULL,
    [SubCategoryId]              INT              NULL,
    [BlockTypeCode]              INT              NULL,
    [StateId]                    INT              NULL,
    [Salary]                     NVARCHAR(200)    NULL,
    [Description]                NVARCHAR(MAX)    NULL,
    [DescriptionHindi]           NVARCHAR(MAX)    NULL,
    [DescriptionJson]            NVARCHAR(MAX)    NULL,
    [DescriptionHindiJson]       NVARCHAR(MAX)    NULL,
    [ShortDesription]            NVARCHAR(1000)   NULL,
    [ShortDesriptionHindi]       NVARCHAR(1000)   NULL,
    [HowTo]                      NVARCHAR(MAX)    NULL,
    [MinAge]                     INT              NULL,
    [MaxAge]                     INT              NULL,
    [TotalPost]                  BIGINT           NULL,
    [ExamMode]                   INT              NOT NULL DEFAULT 0,
    [Status]                     INT              NULL,
    [StartDate]                  DATETIME2        NULL,
    [LastDate]                   DATETIME2        NULL,
    [ExtendedDate]               DATETIME2        NULL,
    [PublishedDate]              DATETIME2        NULL,
    [FeePaymentLastDate]         DATETIME2        NULL,
    [CorrectionLastDate]         DATETIME2        NULL,
    [AdmitCardDate]              DATETIME2        NULL,
    [ShouldReminder]             DATETIME2        NULL,
    [ReminderDescription]        NVARCHAR(500)    NULL,
    [UpcomingCalendarCode]       INT              NULL,
    [ApplyLink]                  NVARCHAR(500)    NULL,
    [OfficialLink]               NVARCHAR(500)    NULL,
    [NotificationLink]           NVARCHAR(500)    NULL,
    [OtherLinks]                 NVARCHAR(MAX)    NULL,
    [SortLinks]                  NVARCHAR(MAX)    NULL,
    [SlugUrl]                    NVARCHAR(500)    NULL,
    [Thumbnail]                  NVARCHAR(500)    NULL,
    [ThumbnailCaption]           NVARCHAR(300)    NULL,
    [SocialMediaUrl]             NVARCHAR(500)    NULL,
    [Keywords]                   NVARCHAR(1000)   NULL,
    [KeywordsHindi]              NVARCHAR(1000)   NULL,
    [VisitCount]                 INT              NULL DEFAULT 0,
    [PublisherId]                INT              NULL,
    [IsApproved]                 BIT              NULL,
    [IsCompleted]                BIT              NULL DEFAULT 0,
    [IsExpired]                  BIT              NULL DEFAULT 0,
    [IsPrivate]                  BIT              NULL DEFAULT 0,
    -- Audit
    [IsActive]                   BIT              NOT NULL DEFAULT 1,
    [IsDelete]                   BIT              NOT NULL DEFAULT 0,
    [CreatedDate]                DATETIME2        NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate]               DATETIME2        NULL,
    [CreatedBy]                  INT              NOT NULL DEFAULT 0,
    [ModifiedBy]                 INT              NULL,
    [IPAddress]                  NVARCHAR(50)     NULL,
    [IPCity]                     NVARCHAR(100)    NULL,
    [IPCountry]                  NVARCHAR(100)    NULL,
    [Browser]                    NVARCHAR(200)    NULL,
    [ScreenName]                 NVARCHAR(200)    NULL,
    CONSTRAINT [PK_Recruitment] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE NONCLUSTERED INDEX [IX_Recruitment_ActiveDelete]
    ON [dbo].[Recruitment] ([IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([CategoryId], [BlockTypeCode], [PublishedDate]);
GO

CREATE NONCLUSTERED INDEX [IX_Recruitment_CategoryStatus]
    ON [dbo].[Recruitment] ([CategoryId] ASC, [IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([Title], [SlugUrl], [LastDate], [PublishedDate]);
GO
