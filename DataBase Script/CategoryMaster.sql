-- =============================================
-- Create Table : CategoryMaster
-- =============================================

IF OBJECT_ID('dbo.CategoryMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.CategoryMaster
    (
        CategoryId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CategoryName   NVARCHAR(50) NOT NULL,
        EntryBy        INT NULL,
        EntryDate      DATETIME NULL,
        ModifyBy       INT NULL,
        ModifyDate     DATETIME NULL
    );
END
GO

-- =============================================
-- Insert Master Data
-- =============================================

SET IDENTITY_INSERT dbo.CategoryMaster ON;
GO

INSERT INTO dbo.CategoryMaster
(
    CategoryId,
    CategoryName,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1,  N'SC',      NULL, NULL,NULL, NULL),
(2,  N'ST',      NULL, NULL,NULL, NULL),
(3,  N'VJ/NT-A', NULL, NULL,NULL, NULL),
(4,  N'NT-B',    NULL, NULL,NULL, NULL),
(6,  N'NT-C',    NULL, NULL,NULL, NULL),
(7,  N'NT-D',    NULL, NULL,NULL, NULL),
(8,  N'SBC',     NULL, NULL,NULL, NULL),
(9,  N'OBC',     NULL, NULL,NULL, NULL),
(10, N'Open',    NULL, NULL,NULL, NULL),
(20, N'Other',   NULL, NULL,NULL, NULL);

GO

SET IDENTITY_INSERT dbo.CategoryMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.CategoryMaster
ORDER BY CategoryId;