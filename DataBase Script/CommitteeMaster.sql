-- =============================================
-- Create Table : CommitteeMaster
-- =============================================

IF OBJECT_ID('dbo.CommitteeMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.CommitteeMaster
    (
        CommitteeId     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CommitteeName   NVARCHAR(200) NOT NULL,
        Status          BIT NOT NULL DEFAULT (1),
        Slogan          NVARCHAR(500) NULL,
        Logo            NVARCHAR(255) NULL,
        LogoPath        NVARCHAR(500) NULL,
        EntryBy         INT NULL,
        EntryDate       DATETIME NULL,
        ModifyBy        INT NULL,
        ModifyDate      DATETIME NULL
    );
END
GO

SET IDENTITY_INSERT dbo.CommitteeMaster ON;
GO

INSERT INTO dbo.CommitteeMaster
(
    CommitteeId,
    CommitteeName,
    Status,
    Slogan,
    Logo,
    LogoPath,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(
    1,
    N'ABC Education Society',
    1,
    N'Knowledge is Power',
    N'logo.png',
    N'/uploads/logo.png',
    NULL,
    NULL,
    NULL,
    NULL
);

GO

SET IDENTITY_INSERT dbo.CommitteeMaster OFF;
GO