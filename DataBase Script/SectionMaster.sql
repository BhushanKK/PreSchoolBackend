-- =============================================
-- Create Table : SectionMaster
-- =============================================

IF OBJECT_ID('dbo.SectionMaster', 'U') IS NULL

BEGIN
    CREATE TABLE dbo.SectionMaster
    (
        SetionId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        SectionName   NVARCHAR(50) NOT NULL,
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

SET IDENTITY_INSERT dbo.SectionMaster ON;
GO

INSERT INTO dbo.SectionMaster
(
    SectionId,
    SectionName,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1,  N'Pre-Primary',      NULL, NULL,NULL, NULL),
(2,  N'Primary',      NULL, NULL,NULL, NULL),
(3,  N'Secondary', NULL, NULL,NULL, NULL),
(4,  N'Higher-Secondary',    NULL, NULL,NULL, NULL)
GO

SET IDENTITY_INSERT dbo.SectionMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.SectionMaster
ORDER BY SectionId;