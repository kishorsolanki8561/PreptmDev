-- Returns full scheme detail by Id or SlugUrl.
-- KEY GUARD: Returns 0 rows when IsActive = 0 OR IsDelete = 1 (triggers 404 in API).
-- @IsAdminView = 1 bypasses IsActive guard (admin preview of inactive posts).
CREATE OR ALTER PROCEDURE [dbo].[Sp_SchemeDetailsOfIdAndSlug]
    @slugUrl      NVARCHAR(500) = NULL,
    @Id           INT           = NULL,
    @UserId       BIGINT        = NULL,
    @LanguageCode NVARCHAR(10)  = 'en',
    @IsAdminView  BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SchemeId      INT;
    DECLARE @BlockTypeCode INT;

    SELECT @SchemeId      = s.Id,
           @BlockTypeCode = s.BlockTypeCode
    FROM   [dbo].[Scheme] s
    WHERE  (s.Id = @Id OR s.[Slug] = @slugUrl)
      AND   s.IsDelete = 0
      AND  (s.IsActive = 1 OR @IsAdminView = 1);

    -- RS0: Main record
    SELECT
        s.Id,
        CASE WHEN @LanguageCode = 'hi' AND s.TitleHindi IS NOT NULL
             THEN s.TitleHindi ELSE s.Title END                         AS Title,
        dm.Name                                                          AS DepartmentName,
        dm.SlugUrl                                                       AS DepartmentSlugUrl,
        dm.Logo                                                          AS DepartmentLogo,
        st.StateName                                                     AS State,
        s.MinAge,
        s.MaxAge,
        s.StartDate,
        s.EndDate,
        s.ExtendedDate,
        s.CorrectionLastDate,
        s.PostponeDate,
        s.LevelType,
        s.Mode,
        s.OfficelLink                                                    AS OfficialLink,
        s.ApplyLink,
        CASE WHEN @LanguageCode = 'hi' AND s.ShortDescriptionHindi IS NOT NULL
             THEN s.ShortDescriptionHindi ELSE s.ShortDescription END    AS ShortDescription,
        CASE WHEN @LanguageCode = 'hi' AND s.KeywordsHindi IS NOT NULL
             THEN s.KeywordsHindi ELSE s.Keywords END                    AS Keywords,
        CASE WHEN @LanguageCode = 'hi' AND s.DescriptionHindi IS NOT NULL
             THEN s.DescriptionHindi ELSE s.Description END              AS Description,
        s.Fee,
        bt.Name                                                          AS ModuleName,
        bt.Description                                                   AS ModuleText,
        bt.SlugUrl                                                       AS ModuleSlug,
        bt.Id                                                            AS BlockTypeId,
        s.PublishedDate,
        s.Thumbnail,
        s.ThumbnailCredit,
        s.SocialMediaUrl,
        s.SortLinks                                                      AS sortLinks,
        bm.Id                                                            AS BookmarkId
    FROM   [dbo].[Scheme] s
    LEFT JOIN [dbo].[DepartmentMaster] dm ON dm.Id = s.DepartmentId
                                          AND dm.IsActive = 1 AND dm.IsDelete = 0
    LEFT JOIN [dbo].[State]            st ON st.StateId = s.StateId
    LEFT JOIN [dbo].[BlockType]        bt ON bt.Id = s.BlockTypeCode
    LEFT JOIN [dbo].[Bookmark]         bm ON bm.PostId = s.Id
                                          AND bm.UserId = @UserId
    WHERE  s.Id = @SchemeId;

    -- RS1: Documents
    SELECT sdl.Description AS Document
    FROM   [dbo].[SchemeDocumentLookup] sdl
    WHERE  sdl.SchemeId = @SchemeId;

    -- RS2: Attachments
    -- The live DB may have 'SchemeAttachmentLookup' (correct) OR 'SchemeAttchamentLookup'
    -- (typo). Use OBJECT_ID guards so the SP never fails at runtime regardless of which
    -- table name is present, preventing the entire SP from returning null -> 404.
    IF OBJECT_ID('dbo.SchemeAttachmentLookup', 'U') IS NOT NULL
        EXEC sp_executesql
            N'SELECT sal.Title, sal.Description, sal.Path, sal.Type
              FROM   dbo.SchemeAttachmentLookup sal
              WHERE  sal.SchemeId = @s',
            N'@s INT', @s = @SchemeId;
    ELSE IF OBJECT_ID('dbo.SchemeAttchamentLookup', 'U') IS NOT NULL
        -- Typo-named table: map Url->Path, Label->Title
        EXEC sp_executesql
            N'SELECT sal.Label      AS Title,
                     NULL           AS Description,
                     sal.Url        AS Path,
                     sal.Type
              FROM   dbo.SchemeAttchamentLookup sal
              WHERE  sal.SchemeId = @s',
            N'@s INT', @s = @SchemeId;
    ELSE
        -- Neither table exists - return empty result set so the SP does not fail
        SELECT TOP 0
            CAST(NULL AS NVARCHAR(200)) AS Title,
            CAST(NULL AS NVARCHAR(MAX)) AS Description,
            CAST(NULL AS NVARCHAR(500)) AS Path,
            CAST(NULL AS INT)           AS [Type];

    -- RS3: Eligibilities
    SELECT lk.Title AS Eligibility
    FROM   [dbo].[SchemeEligibilityLookup] sel
    JOIN   [dbo].[Lookup] lk ON lk.Id = sel.EligibilityId
    WHERE  sel.SchemeId = @SchemeId;

    -- RS4: Contact Details
    SELECT CASE WHEN @LanguageCode = 'hi' AND scd.NodalOfficerNameHindi IS NOT NULL
                THEN scd.NodalOfficerNameHindi ELSE scd.NodalOfficerName END AS NodalOfficerName,
           dm.Name                                                            AS DepartmentName,
           scd.PhoneNo,
           scd.Email
    FROM   [dbo].[SchemeContactDetailsLookup] scd
    LEFT JOIN [dbo].[DepartmentMaster] dm ON dm.Id = scd.DepartmentId
    WHERE  scd.SchemeId = @SchemeId;

    -- RS5: How To Apply (IsQuickLink = 0)
    SELECT CASE WHEN @LanguageCode = 'hi' AND shal.DescriptionHindi IS NOT NULL
                THEN shal.DescriptionHindi ELSE shal.Description END     AS Description,
           CASE WHEN @LanguageCode = 'hi' AND shal.TitleHindi IS NOT NULL
                THEN shal.TitleHindi ELSE shal.Title END                  AS Title,
           shal.LinkUrl                                                   AS Url
    FROM   [dbo].[SchemeHowToApplyAndQuickLinkLookup] shal
    WHERE  shal.SchemeId = @SchemeId AND shal.IsQuickLink = 0;

    -- RS6: Quick Links (IsQuickLink = 1)
    SELECT CASE WHEN @LanguageCode = 'hi' AND shal.DescriptionHindi IS NOT NULL
                THEN shal.DescriptionHindi ELSE shal.Description END     AS Description,
           CASE WHEN @LanguageCode = 'hi' AND shal.TitleHindi IS NOT NULL
                THEN shal.TitleHindi ELSE shal.Title END                  AS Title,
           shal.LinkUrl                                                   AS Url,
           shal.IconClass
    FROM   [dbo].[SchemeHowToApplyAndQuickLinkLookup] shal
    WHERE  shal.SchemeId = @SchemeId AND shal.IsQuickLink = 1;

    -- RS7: Other Schemes (same block type)
    SELECT TOP 5
        s2.Id,
        CASE WHEN @LanguageCode = 'hi' AND s2.TitleHindi IS NOT NULL
             THEN s2.TitleHindi ELSE s2.Title END AS Title,
        s2.Thumbnail,
        s2.StartDate,
        s2.EndDate   AS LastDate,
        s2.[Slug]    AS SlugUrl,
        bt2.Name     AS ModuleName,
        bt2.SlugUrl  AS BlockTypeSlug
    FROM   [dbo].[Scheme] s2
    LEFT JOIN [dbo].[BlockType] bt2 ON bt2.Id = s2.BlockTypeCode
    WHERE  s2.BlockTypeCode = @BlockTypeCode
      AND  s2.Id           <> @SchemeId
      AND  s2.IsActive      = 1
      AND  s2.IsDelete      = 0
    ORDER BY s2.PublishedDate DESC;

    -- RS8: FAQs
    SELECT
        CASE WHEN @LanguageCode = 'hi' AND f.QueHindi IS NOT NULL
             THEN f.QueHindi ELSE f.Que END AS Que,
        CASE WHEN @LanguageCode = 'hi' AND f.AnsHindi IS NOT NULL
             THEN f.AnsHindi ELSE f.Ans END AS Ans
    FROM   [dbo].[FAQ] f
    WHERE  f.BlockTypeId = @BlockTypeCode;

END
GO
