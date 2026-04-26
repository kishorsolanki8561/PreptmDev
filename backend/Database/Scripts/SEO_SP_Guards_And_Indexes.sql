-- ============================================================
-- PrepTM: SEO Fixes — SP Guards + Covering Indexes
-- Run on: StagePreptm / Preptm database
-- ============================================================

-- ------------------------------------------------------------
-- PART 1: Covering Indexes
-- NOTE: These indexes are already defined in the table DDL files
-- (dbo.Recruitment.sql, dbo.Article.sql, dbo.BlockContents.sql, dbo.Scheme.sql).
-- Run these only if deploying to a DB that predates the table DDL update.
-- ------------------------------------------------------------

-- NOTE: SlugUrl / Slug columns are nvarchar(max) and cannot be used as index keys.
-- These indexes cover IsActive + IsDelete filters (used in every detail SP guard)
-- and PublishedDate ordering (used in all related-posts queries).

-- Recruitment: active record filter + published ordering
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Recruitment_ActiveDelete'
      AND object_id = OBJECT_ID('dbo.Recruitment')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Recruitment_ActiveDelete]
    ON [dbo].[Recruitment] ([IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([CategoryId], [BlockTypeCode], [PublishedDate]);
END
GO

-- Article: active record filter + published ordering
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Article_ActiveDelete'
      AND object_id = OBJECT_ID('dbo.Article')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Article_ActiveDelete]
    ON [dbo].[Article] ([IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([ArticleType], [PublisherDate]);
END
GO

-- BlockContents: active record filter + published ordering
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_BlockContent_ActiveDelete'
      AND object_id = OBJECT_ID('dbo.BlockContents')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_BlockContent_ActiveDelete]
    ON [dbo].[BlockContents] ([IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([BlockTypeId], [CategoryId], [PublishedDate]);
END
GO

-- Scheme: active record filter + published ordering
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Scheme_ActiveDelete'
      AND object_id = OBJECT_ID('dbo.Scheme')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Scheme_ActiveDelete]
    ON [dbo].[Scheme] ([IsActive] ASC, [IsDelete] ASC)
    INCLUDE ([BlockTypeCode], [DepartmentId], [PublishedDate]);
END
GO

PRINT 'Covering indexes: done.';
GO

-- ------------------------------------------------------------
-- PART 2: SP Guards — IsActive / IsDelete filters
--
-- These are ALTER scripts for the 4 detail stored procedures.
-- They add WHERE guards so inactive/deleted records return 0 rows,
-- which causes the API controllers to return HTTP 404 instead of
-- HTTP 200 with empty data (soft 404 fix for Google Search Console).
--
-- HOW TO APPLY:
--   1. In SSMS, right-click each SP > Script As > ALTER To > New Query Window.
--   2. Find the main SELECT on the primary table (Recruitment / Article /
--      BlockContents / Scheme) and add the two lines shown below to the WHERE.
--   3. Run against StagePreptm first, verify, then run against Preptm.
--
-- Guard to add to the main SELECT WHERE clause of each SP:
--   AND ISNULL(r.IsActive, 1) = 1    -- use table alias matching the SP (r/a/bc/s)
--   AND ISNULL(r.IsDelete, 0) = 0
--
-- SP 1: Sp_FrontRecruitmentDetailsOfIdAndSlug
--   Table alias: r (Recruitment r)
--   Add: AND ISNULL(r.IsActive, 1) = 1 AND ISNULL(r.IsDelete, 0) = 0
--
-- SP 2: Sp_FrontArticleDetails
--   Table alias: a (Article a)
--   Add: AND ISNULL(a.IsActive, 1) = 1 AND ISNULL(a.IsDelete, 0) = 0
--
-- SP 3: Sp_SchemeDetailsOfIdAndSlug
--   Table alias: s (Scheme s)
--   Slug column is [Slug] (not SlugUrl)
--   Add: AND ISNULL(s.IsActive, 1) = 1 AND ISNULL(s.IsDelete, 0) = 0
--
-- SP 4: Sp_FrontBlockContentsDetailsOfIdAndSlug
--   Table alias: bc (BlockContents bc)
--   Add: AND ISNULL(bc.IsActive, 1) = 1 AND ISNULL(bc.IsDelete, 0) = 0
--
-- SP 5: Sp_FrontGetSiteMap
--   Exclude inactive/deleted records from sitemap output.
--   Add to each sub-SELECT WHERE: AND ISNULL(IsActive, 1) = 1 AND ISNULL(IsDelete, 0) = 0
-- ------------------------------------------------------------

PRINT 'SP guard instructions printed above. Apply manually via SSMS ALTER scripts.';
GO
