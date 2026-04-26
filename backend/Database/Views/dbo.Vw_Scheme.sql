-- View joining Scheme with its lookup tables for reporting/admin queries.
-- NOTE: Script the actual definition from the live database via SSMS:
--   Right-click view > Script As > CREATE To > New Query Window
--   Then replace this stub with the generated script.
CREATE VIEW [dbo].[Vw_Scheme]
AS
    SELECT
        s.[Id],
        s.[Title],
        s.[TitleHindi],
        s.[Slug],
        s.[DepartmentId],
        d.[Name]          AS [DepartmentName],
        s.[StateId],
        st.[Name]         AS [StateName],
        s.[LevelType],
        s.[Mode],
        s.[StartDate],
        s.[EndDate],
        s.[Status],
        s.[IsActive],
        s.[IsDelete],
        s.[CreatedDate],
        s.[ModifiedDate]
    FROM [dbo].[Scheme]          AS s
    LEFT JOIN [dbo].[DepartmentMaster] AS d  ON d.[Id] = s.[DepartmentId]  AND d.[IsDelete] = 0
    LEFT JOIN [dbo].[State]            AS st ON st.[Id] = s.[StateId];
GO
