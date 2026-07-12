-- Index for CategoryID (used in JOINs and WHERE clauses)
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_CasteMaster_CategoryID'
      AND object_id = OBJECT_ID('dbo.CasteMaster')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_CasteMaster_CategoryID
    ON dbo.CasteMaster (CategoryID);
END
GO

-- Index for CasteName (used in search/filter)
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_CasteMaster_CasteName'
      AND object_id = OBJECT_ID('dbo.CasteMaster')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_CasteMaster_CasteName
    ON dbo.CasteMaster (CasteName);
END
GO

-- Composite Index (Category + CasteName)
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_CasteMaster_CategoryID_CasteName'
      AND object_id = OBJECT_ID('dbo.CasteMaster')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_CasteMaster_CategoryID_CasteName
    ON dbo.CasteMaster (CategoryID, CasteName);
END
GO