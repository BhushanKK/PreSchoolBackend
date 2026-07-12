-- =============================================
-- Create Table : HolidayMaster
-- =============================================

IF OBJECT_ID('dbo.HolidayMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.HolidayMaster
    (
        HolidayId       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        HolidayName     NVARCHAR(200) NOT NULL,
        HolidayDate     DATE NOT NULL,
        HolidayType     NVARCHAR(50) NULL,
        Description     NVARCHAR(500) NULL,
        IsActive        BIT NOT NULL DEFAULT(1),
        EntryBy         INT NULL,
        EntryDate       DATETIME NULL,
        ModifyBy        INT NULL,
        ModifyDate      DATETIME NULL
    );
END
GO

-- =============================================
-- Insert Master Data
-- =============================================

SET IDENTITY_INSERT dbo.HolidayMaster ON;
GO

INSERT INTO dbo.HolidayMaster
(
    HolidayId,
    HolidayName,
    HolidayDate,
    HolidayType,
    Description,
    IsActive,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(1, N'Republic Day',                     '2026-01-26', N'National', N'National Holiday',                1, NULL, NULL, NULL, NULL),
(2, N'Maha Shivratri',                   '2026-02-15', N'Festival', N'Hindu Festival',                  1, NULL, NULL, NULL, NULL),
(3, N'Holi',                             '2026-03-04', N'Festival', N'Festival of Colours',             1, NULL, NULL, NULL, NULL),
(4, N'Gudhi Padwa',                      '2026-03-20', N'Festival', N'Marathi New Year',                1, NULL, NULL, NULL, NULL),
(5, N'Dr. Babasaheb Ambedkar Jayanti',   '2026-04-14', N'National', N'Birth Anniversary',               1, NULL, NULL, NULL, NULL),
(6, N'Maharashtra Day',                  '2026-05-01', N'State',    N'Maharashtra Foundation Day',      1, NULL, NULL, NULL, NULL),
(7, N'Independence Day',                 '2026-08-15', N'National', N'National Holiday',                1, NULL, NULL, NULL, NULL),
(8, N'Ganesh Chaturthi',                 '2026-09-15', N'Festival', N'Ganesh Festival',                 1, NULL, NULL, NULL, NULL),
(9, N'Gandhi Jayanti',                   '2026-10-02', N'National', N'Birth Anniversary',               1, NULL, NULL, NULL, NULL),
(10, N'Diwali',                          '2026-11-08', N'Festival', N'Deepavali Festival',              1, NULL, NULL, NULL, NULL),
(11, N'Christmas',                       '2026-12-25', N'National', N'Christmas Holiday',               1, NULL, NULL, NULL, NULL);

GO

SET IDENTITY_INSERT dbo.HolidayMaster OFF;
GO

-- =============================================
-- Verify Data
-- =============================================

SELECT *
FROM dbo.HolidayMaster
ORDER BY HolidayDate;