/*
  Post-Deployment Script — PrepTM Database
  Runs after the main dacpac deployment.
  Use this file to seed lookup/reference data and perform any
  data migrations that cannot be expressed as schema changes.
*/

-- Seed: UserType (if empty)
IF NOT EXISTS (SELECT 1 FROM [dbo].[UserType])
BEGIN
    SET IDENTITY_INSERT [dbo].[UserType] ON;
    INSERT INTO [dbo].[UserType] ([Id], [TypeName], [IsActive], [IsDelete], [CreatedDate], [CreatedBy])
    VALUES (1, 'SuperAdmin', 1, 0, GETUTCDATE(), 1),
           (2, 'Admin',      1, 0, GETUTCDATE(), 1),
           (3, 'Editor',     1, 0, GETUTCDATE(), 1);
    SET IDENTITY_INSERT [dbo].[UserType] OFF;
END
GO

-- Seed: AdditionalPages stubs (if empty)
IF NOT EXISTS (SELECT 1 FROM [dbo].[AdditionalPages])
BEGIN
    INSERT INTO [dbo].[AdditionalPages] ([PageType], [Content], [ContentHindi], [IsActive], [IsDelete], [CreatedDate], [CreatedBy])
    VALUES (1, NULL, NULL, 1, 0, GETUTCDATE(), 1),  -- Terms & Conditions
           (2, NULL, NULL, 1, 0, GETUTCDATE(), 1),  -- Privacy Policy
           (3, NULL, NULL, 1, 0, GETUTCDATE(), 1),  -- About Us
           (4, NULL, NULL, 1, 0, GETUTCDATE(), 1);  -- Manage Account
END
GO
