-- =============================================
-- Create Table : RoleMaster
-- =============================================

IF OBJECT_ID('dbo.RoleMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.RoleMaster
    (
        RoleId          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        RoleName        NVARCHAR(100) NOT NULL,
        RoleDescription NVARCHAR(250) NULL,
        IsActive        BIT NOT NULL DEFAULT(1),
        EntryBy         UNIQUEIDENTIFIER NULL,
        EntryDate       DATETIME NULL,
        ModifyBy        UNIQUEIDENTIFIER NULL,
        ModifyDate      DATETIME NULL
    );
END
GO

-- =============================================
-- Insert Master Data
-- =============================================

SET IDENTITY_INSERT dbo.RoleMaster ON;
GO

INSERT INTO dbo.RoleMaster
(
    RoleId,
    RoleName,
    RoleDescription,
    IsActive,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1, N'Admin',       N'System Administrator',                1, NULL, NULL, NULL, NULL),
(2, N'Head Master', N'School Head Master',                  1, NULL, NULL, NULL, NULL),
(3, N'Teacher',     N'Teaching Staff',                      1, NULL, NULL, NULL, NULL),
(4, N'Parent',      N'Student Parent or Guardian',          1, NULL, NULL, NULL, NULL);

GO

SET IDENTITY_INSERT dbo.RoleMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.RoleMaster
ORDER BY RoleId;