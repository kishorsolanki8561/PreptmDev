-- Returns full article detail by Id or SlugUrl.
-- KEY GUARD: Returns 0 rows when IsActive = 0 OR IsDelete = 1 (triggers 404 in API).
-- @IsAdminView = 1 bypasses IsActive guard (admin preview of inactive posts).
CREATE OR ALTER PROCEDURE [dbo].[Sp_FrontArticleDetails]
    @Id           INT           = NULL,
    @SlugUrl      NVARCHAR(500) = NULL,
    @LanguageCode NVARCHAR(10)  = 'en',
    @IsAdminView  BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ArticleId INT;

    SELECT @ArticleId = a.Id
    FROM   [dbo].[Article] a
    WHERE  (a.Id = @Id OR a.SlugUrl = @SlugUrl)
      AND   a.IsDelete = 0
      AND  (a.IsActive = 1 OR @IsAdminView = 1);

    -- ── RS0: Main article ───────────────────────────────────────────────────
    SELECT
        a.Id,
        CASE WHEN @LanguageCode = 'hi' AND a.TitleHindi IS NOT NULL
             THEN a.TitleHindi ELSE a.Title END                     AS Title,
        a.Thumbnail,
        CASE WHEN @LanguageCode = 'hi' AND a.DescriptionHindi IS NOT NULL
             THEN a.DescriptionHindi ELSE a.Description END          AS Description,
        a.ArticleType,
        CASE WHEN @LanguageCode = 'hi' AND a.SummaryHindi IS NOT NULL
             THEN a.SummaryHindi ELSE a.Summary END                  AS Summary,
        CASE WHEN @LanguageCode = 'hi' AND a.KeywordHindi IS NOT NULL
             THEN a.KeywordHindi ELSE a.Keywords END                 AS Keywords,
        a.ThumbnailCredit,
        a.ModifiedDate
    FROM   [dbo].[Article] a
    WHERE  a.Id = @ArticleId;

    -- ── RS1: Tags ───────────────────────────────────────────────────────────
    SELECT
        lk.Title  AS Tag,
        lk.Slug   AS SlugUrl
    FROM   [dbo].[ArticleTags] atrg
    JOIN   [dbo].[Lookup] lk ON lk.Id = atrg.TagsId
    WHERE  atrg.ArticleId = @ArticleId;

    -- ── RS2: FAQs ───────────────────────────────────────────────────────────
    SELECT
        CASE WHEN @LanguageCode = 'hi' AND af.QueHindi IS NOT NULL
             THEN af.QueHindi ELSE af.Que END AS Que,
        CASE WHEN @LanguageCode = 'hi' AND af.AnsHindi IS NOT NULL
             THEN af.AnsHindi ELSE af.Ans END AS Ans
    FROM   [dbo].[ArticleFaq] af
    WHERE  af.ArticleId = @ArticleId;

    -- ── RS3: Related Recruitments (via shared tags) ─────────────────────────
    SELECT TOP 5
        r.Id,
        r.Title,
        r.Thumbnail,
        r.StartDate,
        r.LastDate,
        r.TotalPost,
        r.SlugUrl,
        bt.Name      AS ModuleName,
        bt.Description AS ModuleText,
        bt.SlugUrl   AS BlockTypeSlug
    FROM   [dbo].[RecruitmentTags] rt
    JOIN   [dbo].[Recruitment] r ON r.Id = rt.RecruitmentId
                                 AND r.IsActive = 1 AND r.IsDelete = 0
    LEFT JOIN [dbo].[BlockType] bt ON bt.Id = r.BlockTypeCode
    WHERE  rt.TagsId IN (
        SELECT atrg.TagsId FROM [dbo].[ArticleTags] atrg
        WHERE atrg.ArticleId = @ArticleId
    )
    ORDER BY r.PublishedDate DESC;

    -- ── RS4: Related Block Contents (via shared tags) ───────────────────────
    SELECT TOP 5
        bc.Id,
        bc.Title,
        bc.Thumbnail,
        bc.Date      AS StartDate,
        bc.LastDate,
        bc.SlugUrl,
        bt2.Name     AS ModuleName,
        bt2.Description AS ModuleText,
        bt2.SlugUrl  AS BlockTypeSlug
    FROM   [dbo].[BlockContentTags] bct
    JOIN   [dbo].[BlockContents] bc ON bc.Id = bct.BlockContentId
                                    AND bc.IsActive = 1 AND bc.IsDelete = 0
    LEFT JOIN [dbo].[BlockType] bt2 ON bt2.Id = bc.BlockTypeId
    WHERE  bct.TagsId IN (
        SELECT atrg.TagsId FROM [dbo].[ArticleTags] atrg
        WHERE atrg.ArticleId = @ArticleId
    )
    ORDER BY bc.PublishedDate DESC;

    -- ── RS5: Module text ────────────────────────────────────────────────────
    SELECT bt.Name AS ModuleText
    FROM   [dbo].[BlockType] bt
    WHERE  bt.SlugUrl = 'articles';

END
GO
