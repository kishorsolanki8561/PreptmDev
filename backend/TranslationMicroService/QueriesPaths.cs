namespace TranslationMicroService
{
    public class QueriesPaths
    {
        public static class SchemeQueries
        {
            public static string QAddUpdate = @"Sp_SchemeAddUpdate @Id,@Title,@TitleHindi,@DepartmentId,@StateId,@MinAge,@MaxAge
                                                ,@StartDate,@EndDate,@ExtendedDate,@CorrectionLastDate,@PostponeDate,@LevelType
                                                ,@Mode,@OfficelLink,@ApplyLink,@ShortDescription,@ShortDescriptionHindi,@keywords
                                                ,@Description,@DescriptionHindi,@DescriptionJson,@DescriptionHindiJson,@Thumbnail
                                                ,@Fee,@Slug,@UserId,@DocumentIds,@EligibilityIds,@DBDeleteEligibilityIds,@DBDeleteDocumentIds
                                                ,@DBSchemeContactDetailsLookups,@DBHowToApplyAndQuickLinkLookups,@DBSchemeAttachmentLookups,@FAQLookup,@SocialMediaUrl,@ThumbnailCredit,@IsCompleted,@ShouldReminder,@ReminderDescription,@UpcomingCalendarCode,@DBDeleteSchemeContactDetailsLookupIds,@DBDeleteHowToApplyAndQuickLinkLookupIds,@DBDeleteSchemeAttachmentLookupIds,@keywordsHindi";

            public static string QSchemeDocumentLookupAddUpdate = @"Sp_SchemeDocumentLookupAddUpdate @Id,@SchemeId,@LookupId,@Description,@DescriptionHindi";

            public static string QSchemeAttachmentLookupAddUpdate = @"Sp_SchemeAttachmentLookupAddUpdate @Id,@SchemeId,@Title,@TitleHindi,@Description,@DescriptionHindi,@Type,@Path";

            public static string QGetSchemeDocumentAndAttachmentLookupDataBySchemeId = @"Sp_GetSchemeDocumentAndAttachmentLookupDataBySchemeId @SchemeId";

            public static string QGetById = @"select * from Vw_Scheme where Id=@Id";

            public static string QDelete = @"Sp_SchemeDeleteUpdateStatus @Type='Delete',@Id=@Id,@UserId=@UserId";

            public static string QSchemeDocumentDelete = @"Sp_SchemeDocumentDelete @Ids";

            public static string QSchemeAttachmentDelete = @"Sp_SchemeAttachmentDelete @Ids";

            public static string QPagination = @"Sp_SchemePagination @Page,@PageSize,@Search,@OrderBy,@OrderByAsc,@IsActive,@FromDate,@ToDate,@Title,@TitleHindi";

            public static string QUpdateStatus = @"Sp_SchemeDeleteUpdateStatus @Type='Status',@Id=@Id,@UserId=@UserId";
            public static string QProgressUpdateStatus = @"Sp_SchemeDeleteUpdateStatus @Type='ProgressStatus',@Id=@Id,@UserId=@UserId,@StatusCode=@StatusCode,@SortLinks=@SortLinks";
        }

        public static class BlockContentsQueries
        {
            public static string QAddUpdate = @"Sp_BlockContentsAddUpdate @Id,@Title,@BlockTypeId,@RecruitmentId,@DepartmentId,@CategoryId
                ,@SlugUrl,@Url,@Description,@UserId,@Date,@GroupId,@SubCategoryId,@Keywords,@StateId,@NotificationLink,@TitleHindi,
                 @DescriptionHindi,@Summary,@DBDeleteAttachmentLookupIds,@DBAttachmentLookup,@DBHowToApplyAndQuickLinkLookup,@FAQLookup,@LastDate,@ExtendedDate,@FeePaymentLastDate,@CorrectionLastDate,@UrlLabelId,@ExamMode,@Thumbnail,@ThumbnailCredit,@SocialMediaUrl,@IsCompleted,@ShouldReminder,@ReminderDescription,@UpcomingCalendarCode,@DescriptionJson,@DescriptionHindiJson,@KeywordsHindi,@SummaryHindi";
        }
    }
}
