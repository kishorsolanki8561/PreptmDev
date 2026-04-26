-- Returns full recruitment detail by Id or SlugUrl.
-- KEY GUARD: Returns 0 rows when IsActive = 0 OR IsDelete = 1 (triggers 404 in API).
-- @IsAdminView = 1 bypasses IsActive guard (admin preview of inactive posts).
CREATE OR ALTER PROCEDURE [dbo].[Sp_FrontRecruitmentDetailsOfIdAndSlug]
    @slugUrl     NVARCHAR(500) = NULL,
    @Id          BIGINT        = NULL,
    @UserId      BIGINT        = NULL,
    @LanguageCode NVARCHAR(10) = 'en',
    @IsAdminView BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RecruitmentId BIGINT;
    DECLARE @CategoryId    INT;

    -- Resolve the record Id once (avoids repeating the WHERE predicate)
    SELECT @RecruitmentId = r.Id,
           @CategoryId    = r.CategoryId
    FROM   [dbo].[Recruitment] r
    WHERE  (r.Id = @Id OR r.SlugUrl = @slugUrl)
      AND   r.IsDelete = 0
      AND  (r.IsActive = 1 OR @IsAdminView = 1);

    -- ── RS0: Main record ────────────────────────────────────────────────────
    SELECT
        r.Id,
        CASE WHEN @LanguageCode = 'hi' AND r.TitleHindi IS NOT NULL
             THEN r.TitleHindi ELSE r.Title END                             AS Title,
        dm.Name                                                              AS DepartmentName,
        dm.SlugUrl                                                           AS DepartmentSlugUrl,
        dm.Logo                                                              AS DepartmentLogo,
        r.Salary,
        CASE WHEN @LanguageCode = 'hi' AND r.DescriptionHindi IS NOT NULL
             THEN r.DescriptionHindi ELSE r.Description END                  AS Description,
        r.MinAge,
        r.MaxAge,
        r.StartDate,
        r.LastDate,
        r.ExtendedDate,
        r.FeePaymentLastDate,
        r.CorrectionLastDate,
        r.AdmitCardDate,
        r.PublishedDate,
        r.ExamMode,
        r.ApplyLink,
        r.OfficialLink,
        r.NotificationLink,
        r.TotalPost,
        CASE WHEN @LanguageCode = 'hi' AND r.ShortDesriptionHindi IS NOT NULL
             THEN r.ShortDesriptionHindi ELSE r.ShortDesription END          AS ShortDesription,
        r.SortLinks,
        r.SlugUrl,
        r.Thumbnail,
        r.ThumbnailCaption                                                   AS ThumbnailCredit,
        r.SocialMediaUrl,
        r.Keywords,
        st.StateName                                                         AS StateName,
        bt.Name                                                              AS ModuleName,
        bt.Description                                                       AS ModuleText,
        bt.SlugUrl                                                           AS ModuleSlug,
        bt.Id                                                                AS BlockTypeId,
        r.IsPrivate,
        bm.Id                                                                AS BookmarkId,
        cm.Name                                                              AS CategoryName,
        sc.Name                                                              AS SubCategoryName
    FROM   [dbo].[Recruitment] r
    LEFT JOIN [dbo].[DepartmentMaster] dm ON dm.Id = r.DepartmentId
                                          AND dm.IsActive = 1 AND dm.IsDelete = 0
    LEFT JOIN [dbo].[State]            st ON st.StateId = r.StateId
    LEFT JOIN [dbo].[BlockType]        bt ON bt.Id = r.BlockTypeCode
    LEFT JOIN [dbo].[CategoryMaster]   cm ON cm.Id = r.CategoryId
    LEFT JOIN [dbo].[SubCategory]      sc ON sc.Id = r.SubCategoryId
    LEFT JOIN [dbo].[Bookmark]         bm ON bm.PostId = r.Id
                                          AND bm.UserId = @UserId
    WHERE  r.Id = @RecruitmentId;

    -- ── RS1: Job Designations ───────────────────────────────────────────────
    SELECT
        jdm.Id,
        CASE WHEN @LanguageCode = 'hi' AND jdm.NameHindi IS NOT NULL
             THEN jdm.NameHindi ELSE jdm.Name END AS Name,
        l.JobDesignationId,
        l.RecruitmentId
    FROM   [dbo].[RecruitmentJobDesignationLookup] l
    JOIN   [dbo].[JobDesignationMaster] jdm ON jdm.Id = l.JobDesignationId
    WHERE  l.RecruitmentId = @RecruitmentId;

    -- ── RS2: Qualifications ─────────────────────────────────────────────────
    SELECT
        qm.Id,
        CASE WHEN @LanguageCode = 'hi' AND qm.TitleHindi IS NOT NULL
             THEN qm.TitleHindi ELSE qm.Title END AS Title,
        l.QualificationId,
        l.RecruitmentId
    FROM   [dbo].[RecruitmentQualificationLookup] l
    JOIN   [dbo].[QualificationMaster] qm ON qm.Id = l.QualificationId
    WHERE  l.RecruitmentId = @RecruitmentId;

    -- ── RS3: Documents ──────────────────────────────────────────────────────
    SELECT rdl.Id, rdl.Path, rdl.RecruitmentId
    FROM   [dbo].[RecruitmentDocumentLookup] rdl
    WHERE  rdl.RecruitmentId = @RecruitmentId;

    -- ── RS4: How To Apply (IsQuickLink = 0) ────────────────────────────────
    SELECT rhtl.Id,
           CASE WHEN @LanguageCode = 'hi' AND rhtl.TitleHindi IS NOT NULL
                THEN rhtl.TitleHindi ELSE rhtl.Title END           AS Title,
           CASE WHEN @LanguageCode = 'hi' AND rhtl.DescriptionHindi IS NOT NULL
                THEN rhtl.DescriptionHindi ELSE rhtl.Description END AS Description,
           rhtl.LinkUrl                                              AS Url
    FROM   [dbo].[RecruitmentHowToApplyAndQuickLinkLookup] rhtl
    WHERE  rhtl.RecruitmentId = @RecruitmentId AND rhtl.IsQuickLink = 0;

    -- ── RS5: Quick Links (IsQuickLink = 1) ─────────────────────────────────
    SELECT rhtl.Id,
           CASE WHEN @LanguageCode = 'hi' AND rhtl.TitleHindi IS NOT NULL
                THEN rhtl.TitleHindi ELSE rhtl.Title END           AS Title,
           CASE WHEN @LanguageCode = 'hi' AND rhtl.DescriptionHindi IS NOT NULL
                THEN rhtl.DescriptionHindi ELSE rhtl.Description END AS Description,
           rhtl.LinkUrl                                              AS Url
    FROM   [dbo].[RecruitmentHowToApplyAndQuickLinkLookup] rhtl
    WHERE  rhtl.RecruitmentId = @RecruitmentId AND rhtl.IsQuickLink = 1;

    -- ── RS6: Related (same category) ───────────────────────────────────────
    SELECT TOP 5
        r2.Id,
        r2.Title,
        r2.Thumbnail,
        r2.StartDate,
        r2.PublishedDate,
        r2.LastDate,
        r2.TotalPost,
        r2.SlugUrl,
        bt2.Name        AS ModuleName,
        bt2.Description AS ModuleText,
        bt2.SlugUrl     AS BlockTypeSlug
    FROM   [dbo].[Recruitment] r2
    LEFT JOIN [dbo].[BlockType] bt2 ON bt2.Id = r2.BlockTypeCode
    WHERE  r2.CategoryId = @CategoryId
      AND  r2.Id        <> @RecruitmentId
      AND  r2.IsActive   = 1
      AND  r2.IsDelete   = 0
    ORDER BY r2.PublishedDate DESC;

    -- ── RS7: Related Block Content ──────────────────────────────────────────
    SELECT TOP 5
        bc.Id,
        bc.Title,
        bc.Thumbnail,
        bc.Date          AS StartDate,
        bc.PublishedDate,
        bc.LastDate,
        NULL             AS TotalPost,
        bc.SlugUrl,
        bt3.Name         AS ModuleName,
        bt3.Description  AS ModuleText,
        bt3.SlugUrl      AS BlockTypeSlug
    FROM   [dbo].[BlockContents] bc
    LEFT JOIN [dbo].[BlockType] bt3 ON bt3.Id = bc.BlockTypeId
    WHERE  bc.RecruitmentId = @RecruitmentId
      AND  bc.IsActive       = 1
      AND  bc.IsDelete        = 0
    ORDER BY bc.PublishedDate DESC;

    -- ── RS8: FAQs ───────────────────────────────────────────────────────────
    SELECT
        CASE WHEN @LanguageCode = 'hi' AND f.QueHindi IS NOT NULL
             THEN f.QueHindi ELSE f.Que END AS Que,
        CASE WHEN @LanguageCode = 'hi' AND f.AnsHindi IS NOT NULL
             THEN f.AnsHindi ELSE f.Ans END AS Ans
    FROM   [dbo].[FAQ] f
    WHERE  f.BlockTypeId = (
        SELECT r3.BlockTypeCode FROM [dbo].[Recruitment] r3 WHERE r3.Id = @RecruitmentId
    );

    -- ── RS9: Tags ───────────────────────────────────────────────────────────
    SELECT
        lk.Title  AS Tag,
        lk.Slug   AS SlugUrl
    FROM   [dbo].[RecruitmentTags] rt
    JOIN   [dbo].[Lookup] lk ON lk.Id = rt.TagsId
    WHERE  rt.RecruitmentId = @RecruitmentId;

    -- ── RS10: Related Articles ──────────────────────────────────────────────
    SELECT TOP 5
        a.Id,
        a.Title,
        a.Thumbnail,
        NULL             AS StartDate,
        a.PublisherDate  AS PublishedDate,
        NULL             AS LastDate,
        NULL             AS TotalPost,
        a.SlugUrl,
        'Article'        AS ModuleName,
        'Article'        AS ModuleText,
        'articles'       AS BlockTypeSlug
    FROM   [dbo].[ArticleTags] atrg
    JOIN   [dbo].[Article] a ON a.Id = atrg.ArticleId
                             AND a.IsActive = 1 AND a.IsDelete = 0
    WHERE  atrg.TagsId IN (
        SELECT rt.TagsId FROM [dbo].[RecruitmentTags] rt
        WHERE rt.RecruitmentId = @RecruitmentId
    )
    ORDER BY a.PublisherDate DESC;

END
GO
