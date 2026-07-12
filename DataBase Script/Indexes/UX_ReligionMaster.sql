IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_ReligionMaster_Religion'
      AND object_id = OBJECT_ID('dbo.ReligionMaster')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_ReligionMaster_Religion
    ON dbo.ReligionMaster (Religion)
    WITH
    (
        FILLFACTOR = 80,
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        SORT_IN_TEMPDB = OFF,
        DROP_EXISTING = OFF,
        ONLINE = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON,
        OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    );
END
GO