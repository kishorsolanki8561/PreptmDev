-- Returns full block content detail by Id or SlugUrl.
-- KEY GUARD: Returns 0 rows when IsActive = 0 OR IsDelete = 1 (triggers 404 in API).
-- @IsAdminView = 1 bypasses IsActive guard (admin preview of inactive posts).
CREATE OR ALTER PROCEDURE [dbo].[Sp_FrontBlockContentsDetailsOfIdAndSlug]
    @slugUrl      NVARCHAR(500) = NULL,
    @Id           INT           = NULL,
    @UserId       BIGINT        = NULL,
    @LanguageCode NVARCHAR(10)  = 'en',
    @IsAdminView  BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @BlockContentId  INT;
    DECLARE @BlockTypeId     INT;
    DECLARE @RecruitmentId   INT;
    DECLARE @CategoryId      INT;

    SELECT @BlockContentId = bc.Id,
           @BlockTypeId    = bc.BlockTypeId,
           @RecruitmentId  = bc.RecruitmentId,
           @CategoryId     = bc.CategoryId
    FROM   [dbo].[BlockContents] bc
    WHERE  (bc.Id = @Id OR bc.SlugUrl = @slugUrl)
      AND   bc.IsDelete = 0
      AND  (bc.IsActive = 1 OR @IsAdminView = 1);

    -- ── RS0: Main record ────────────────────────────────────────────────────
    SELECT
        bc.Id,
        CASE WHEN @LanguageCode = 'hi' AND bc.TitleHindi IS NOT NULL
             THEN bc.TitleHindi ELSE bc.Title END                       AS Title,
        CASE WHEN @LanguageCode = 'hi' AND bc.DescriptionHindi IS NOT NULL
             THEN bc.DescriptionHindi ELSE bc.Description END            AS Description,
        bc.Url,
        dm.Name                                                          AS DepartmentName,
        dm.SlugUrl                                                       AS DepartmentSlugUrl,
        dm.Logo                                                          AS DepartmentLogo,
        bc.SortLinks,
        st.StateName                                                     AS StateName,
        bc.NotificationLink,
        bc.Date                                                          AS StartDate,
        bt.Name                                                          AS ModuleName,
        bt.Description                                                   AS ModuleText,
        bt.SlugUrl                                                       AS ModuleSlug,
        bt.Id                                                            AS BlockTypeId,
        bm.Id                                                            AS BookmarkId,
        bc.LastDate,
        bc.ExtendedDate,
        bc.FeePaymentLastDate,
        bc.CorrectionLastDate,
        lku.Title                                                        AS UrlLabel,
        CASE WHEN @LanguageCode = 'hi' AND bc.SummaryHindi IS NOT NULL
             THEN bc.SummaryHindi ELSE bc.Summary END                    AS Summary,
        bc.ExamMode,
        bc.Keywords,
        bc.PublishedDate,
        bc.Thumbnail,
        bc.ThumbnailCredit,
        bc.SocialMediaUrl
    FROM   [dbo].[BlockContents] bc
    LEFT JOIN [dbo].[DepartmentMaster] dm  ON dm.Id = bc.DepartmentId
                                          AND dm.IsActive = 1 AND dm.IsDelete = 0
    LEFT JOIN [dbo].[State]            st  ON st.StateId = bc.StateId
    LEFT JOIN [dbo].[BlockType]        bt  ON bt.Id = bc.BlockTypeId
    LEFT JOIN [dbo].[Lookup]           lku ON lku.Id = bc.UrlLabelId
    LEFT JOIN [dbo].[Bookmark]         bm  ON bm.PostId = bc.Id
                                           AND bm.UserId = @UserId
    WHERE  bc.Id = @BlockContentId;

    -- ── RS1: Documents / Attachments ────────────────────────────────────────
    SELECT bcal.Id, bcal.Path, bcal.BlockContentId
    FROM   [dbo].[BlockContentAttachmentLookup] bcal
    WHERE  bcal.BlockContentId = @BlockContentId;

    -- ── RS2: How To Apply (IsQuickLink = 0) ────────────────────────────────
    SELECT CASE WHEN @LanguageCode = 'hi' AND bchtl.TitleHindi IS NOT NULL
                THEN bchtl.TitleHindi ELSE bchtl.Title END               AS Title,
           CASE WHEN @LanguageCode = 'hi' AND bchtl.DescriptionHindi IS NOT NULL
                THEN bchtl.DescriptionHindi ELSE bchtl.Description END   AS Description,
           bchtl.LinkUrl                                                  AS Url
    FROM   [dbo].[BlockContentsHowToApplyAndQuickLinkLookup] bchtl
    WHERE  bchtl.BlockContentId = @BlockContentId AND bchtl.IsQuickLink = 0;

    -- ── RS3: Quick Links (IsQuickLink = 1) ─────────────────────────────────
    SELECT CASE WHEN @LanguageCode = 'hi' AND bchtl.TitleHindi IS NOT NULL
                THEN bchtl.TitleHindi ELSE bchtl.Title END               AS Title,
           CASE WHEN @LanguageCode = 'hi' AND bchtl.DescriptionHindi IS NOT NULL
                THEN bchtl.DescriptionHindi ELSE bchtl.Description END   AS Description,
           bchtl.LinkUrl                                                  AS Url
    FROM   [dbo].[BlockContentsHowToApplyAndQuickLinkLookup] bchtl
    WHERE  bchtl.BlockContentId = @BlockContentId AND bchtl.IsQuickLink = 1;

    -- ── RS4: Related Recruitments (same category) ───────────────────────────
    SELECT TOP 5
        r.Id,
        r.Title,
        r.Thumbnail,
        r.StartDate,
        r.PublishedDate,
        r.LastDate,
        r.TotalPost,
        r.SlugUrl,
        btr.Name     AS ModuleName,
        btr.Description AS ModuleText,
        btr.SlugUrl  AS BlockTypeSlug
    FROM   [dbo].[Recruitment] r
    LEFT JOIN [dbo].[BlockType] btr ON btr.Id = r.BlockTypeCode
    WHERE  r.CategoryId = @CategoryId
      AND  r.IsActive   = 1
      AND  r.IsDelete   = 0
    ORDER BY r.PublishedDate DESC;

    -- ── RS5: Related Block Contents (same block type) ───────────────────────
    SELECT TOP 5
        bc2.Id,
        bc2.Title,
        bc2.Thumbnail,
        bc2.Date         AS StartDate,
        bc2.PublishedDate,
        bc2.LastDate,
        NULL             AS TotalPost,
        bc2.SlugUrl,
        bt2.Name         AS ModuleName,
        bt2.Description  AS ModuleText,
        bt2.SlugUrl      AS BlockTypeSlug
    FROM   [dbo].[BlockContents] bc2
    LEFT JOIN [dbo].[BlockType] bt2 ON bt2.Id = bc2.BlockTypeId
    WHERE  bc2.BlockTypeId = @BlockTypeId
      AND  bc2.Id         <> @BlockContentId
      AND  bc2.IsActive    = 1
      AND  bc2.IsDelete    = 0
    ORDER BY bc2.PublishedDate DESC;

    -- ── RS6: Mapped Recruitment ─────────────────────────────────────────────
    SELECT
        r2.Title                                                         AS Tittle,
        r2.SlugUrl,
        r2.ShortDesription,
        btr2.SlugUrl                                                     AS ModuleSlug,
        NULL                                                             AS JobDesignation,
        NULL                                                             AS Qualification
    FROM   [dbo].[Recruitment] r2
    LEFT JOIN [dbo].[BlockType] btr2 ON btr2.Id = r2.BlockTypeCode
    WHERE  r2.Id = @RecruitmentId
      AND  r2.IsActive = 1 AND r2.IsDelete = 0;

    -- ── RS7: FAQs ───────────────────────────────────────────────────────────
    SELECT
        CASE WHEN @LanguageCode = 'hi' AND f.QueHindi IS NOT NULL
             THEN f.QueHindi ELSE f.Que END AS Que,
        CASE WHEN @LanguageCode = 'hi' AND f.AnsHindi IS NOT NULL
             THEN f.AnsHindi ELSE f.Ans END AS Ans
    FROM   [dbo].[FAQ] f
    WHERE  f.BlockTypeId = @BlockTypeId;

    -- ── RS8: Tags ───────────────────────────────────────────────────────────
    SELECT
        lk.Title AS Tag,
        lk.Slug  AS SlugUrl
    FROM   [dbo].[BlockContentTags] bct
    JOIN   [dbo].[Lookup] lk ON lk.Id = bct.TagsId
    WHERE  bct.BlockContentId = @BlockContentId;

    -- ── RS9: Related Articles ───────────────────────────────────────────────
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
        SELECT bct.TagsId FROM [dbo].[BlockContentTags] bct
        WHERE bct.BlockContentId = @BlockContentId
    )
    ORDER BY a.PublisherDate DESC;

END
GO
