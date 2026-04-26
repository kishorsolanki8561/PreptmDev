/*
  Pre-Deployment Script — PrepTM Database
  Runs before the main dacpac deployment.
  Use this file for compatibility checks, backup notices, or any
  statements that must execute before table/SP changes are applied.
*/

-- Verify minimum SQL Server version (2019 = 150)
IF CAST(SERVERPROPERTY('ProductMajorVersion') AS INT) < 15
BEGIN
    RAISERROR('This database requires SQL Server 2019 (version 15) or later.', 16, 1);
    RETURN;
END
GO
