-- =============================================
-- Create Table : StandardMaster
-- =============================================

IF OBJECT_ID('dbo.StandardMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.StandardMaster
    (
        StandardId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        SectionId      INT NOT NULL,
        StandardName   NVARCHAR(100) NOT NULL,
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

SET IDENTITY_INSERT dbo.StandardMaster ON;
GO

INSERT INTO dbo.StandardMaster
(
    StandardId,
    SectionId,
    StandardName,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1, 1, N'Play Group', NULL, NULL, NULL, NULL),
(2, 1, N'Nursery',    NULL, NULL, NULL, NULL),
(3, 1, N'Junior KG',  NULL, NULL, NULL, NULL),
(4, 1, N'Senior KG',  NULL, NULL, NULL, NULL);

GO

SET IDENTITY_INSERT dbo.StandardMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.StandardMaster
ORDER BY StandardId;