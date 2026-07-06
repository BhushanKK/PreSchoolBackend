-- =============================================
-- Create Table : StandardMaster
-- =============================================

IF OBJECT_ID('dbo.StandardMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.StandardMaster
    (
        StandardId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        StandardName   NVARCHAR(100) NOT NULL,
        EntryBy        UNIQUEIDENTIFIER NULL,
        EntryDate      DATETIME NULL,
        ModifyBy       UNIQUEIDENTIFIER NULL,
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
    StandardName,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1,  N'Play Group', NULL, NULL, NULL, NULL),
(2,  N'Nursery',    NULL, NULL, NULL, NULL),
(3,  N'Junior KG',  NULL, NULL, NULL, NULL),
(4,  N'Senior KG',  NULL, NULL, NULL, NULL);

GO

SET IDENTITY_INSERT dbo.StandardMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.StandardMaster
ORDER BY StandardId;