-- Unique Index on CategoryName
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'UX_CategoryMaster_CategoryName'
      AND object_id = OBJECT_ID('dbo.CategoryMaster')
)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_CategoryMaster_CategoryName
    ON dbo.CategoryMaster (CategoryName)
    WITH
    (
        FILLFACTOR = 80,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON
    );
END
GO