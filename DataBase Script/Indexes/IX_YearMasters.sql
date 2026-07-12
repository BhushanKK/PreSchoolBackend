-- Unique Index on AcademicYearMaster
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_AcademicYearMaster_AcademicYearName'
      AND object_id = OBJECT_ID('dbo.AcademicYearMaster')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_AcademicYearMaster_AcademicYearName
    ON AcademicYearMaster (AcademicYearName)
    WITH
    (
        FILLFACTOR = 80,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    );
END
GO


-- Unique Index on FinancialYearName
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_FinancialYearMaster_FinancialYearName'
      AND object_id = OBJECT_ID('dbo.FinancialYearMaster')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_FinancialYearMaster_FinancialYearName
    ON FinancialYearMaster(FinancialYearName)
    WITH
    (
        FILLFACTOR = 80,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    );
END
GO
