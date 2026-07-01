IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_SchoolDetailsMaster_UDISECode'
      AND object_id = OBJECT_ID('dbo.SchoolDetailsMaster')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_SchoolDetailsMaster_UDISECode
    ON dbo.SchoolDetailsMaster (UDISECode)
    WHERE UDISECode IS NOT NULL
    WITH
    (
        FILLFACTOR = 80,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    );
END
GO