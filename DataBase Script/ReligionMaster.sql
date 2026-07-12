-- =============================================
-- Create Table : ReligionMaster
-- =============================================

IF OBJECT_ID('dbo.ReligionMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ReligionMaster
    (
        ReligionId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Religion        NVARCHAR(100) NOT NULL,
        IsMinority      BIT NOT NULL DEFAULT(0),
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

SET IDENTITY_INSERT dbo.ReligionMaster ON;
GO

INSERT INTO dbo.ReligionMaster
(
    ReligionId,
    Religion,
    IsMinority,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1,  N'Buddhist',    1, NULL, NULL, NULL, NULL),
(2,  N'Muslim',      1, NULL, NULL, NULL, NULL),
(3,  N'Islam',       1, NULL, NULL, NULL, NULL),
(4,  N'Jain',        1, NULL, NULL, NULL, NULL),
(6,  N'Hindu',       0, NULL, NULL, NULL, NULL),
(7,  N'Christian',   1, NULL, NULL, NULL, NULL),
(8,  N'Parsi',       1, NULL, NULL, NULL, NULL),
(9,  N'Sikh',        1, NULL, NULL, NULL, NULL),
(11, N'Other',       1, NULL, NULL, NULL, NULL);

GO

SET IDENTITY_INSERT dbo.ReligionMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.ReligionMaster
ORDER BY ReligionId;