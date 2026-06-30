-- =============================================
-- Create Table : AuditLogs
-- =============================================

IF OBJECT_ID('dbo.AuditLogs', 'U') IS NULL
BEGIN
CREATE TABLE dbo.AuditLog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TableName NVARCHAR(255) NOT NULL,
    Action NVARCHAR(50) NOT NULL,
    EntityId NVARCHAR(255) NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    ChangedColumns NVARCHAR(MAX) NULL,
    UserId NVARCHAR(255) NULL,
    UserName NVARCHAR(255) NULL,
    RequestMethod NVARCHAR(50) NULL,
    RequestPath NVARCHAR(500) NULL,
    Timestamp DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
END
GO
