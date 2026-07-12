-- =============================================
-- Create Table : SchoolDetailsMaster
-- =============================================

IF OBJECT_ID('dbo.SchoolDetailsMaster', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SchoolDetailsMaster
    (
        SchoolId            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CommitteeId         INT NOT NULL,
        SchoolTypeId        INT NOT NULL,
        SchoolName          NVARCHAR(250) NOT NULL,
        SectionId           INT NOT NULL,
        UDISECode           NVARCHAR(20) NULL,
        Village             NVARCHAR(150) NULL,
        Taluka              NVARCHAR(150) NULL,
        DistrictId          INT NOT NULL,
        State               NVARCHAR(100) NULL,
        PinCode             NVARCHAR(10) NULL,
        SchoolRegistrationNo NVARCHAR(100) NULL,
        ContactNo           NVARCHAR(15) NULL,
        Board               NVARCHAR(150) NULL,
        Email               NVARCHAR(100) NULL,
        Medium              NVARCHAR(100) NULL,
        AffiliationNo       NVARCHAR(100) NULL,
        Status              BIT NOT NULL DEFAULT (1),

        EntryBy             UNIQUEIDENTIFIER NULL,
        EntryDate           DATETIME NULL,
        ModifyBy            UNIQUEIDENTIFIER NULL,
        ModifyDate          DATETIME NULL,

        CONSTRAINT FK_SchoolDetailsMaster_CommitteeMaster
            FOREIGN KEY (CommitteeId)
            REFERENCES dbo.CommiteeMaster(CommitteeId)
    );
END
GO

SET IDENTITY_INSERT dbo.SchoolDetailsMaster ON;
GO

INSERT INTO dbo.SchoolDetailsMaster
(
    SchoolId,
    CommitteeId,
    SchoolTypeId,
    SchoolName,
    SectionId,
    UDISECode,
    Village,
    Taluka,
    DistrictId,
    State,
    PinCode,
    SchoolRegistrationNo,
    ContactNo,
    Board,
    Email,
    Medium,
    AffiliationNo,
    Status,
    EntryBy,
    EntryDate,
    ModifyBy,
    ModifyDate
)
VALUES
(
    1,
    1,
    1,
    N'Pre-Primary School',
    2,
    '27200907602',
    N'Vilholi',
    N'Nashik',
    21,
    N'Maharashtra',
    '422010',
    N'-',
    '9876543210',
    N'-',
    'preprimaryschool@gmail.com',
    N'English',
    '13.07.031',
    1,
    NULL,
    NULL,
    NULL,
    NULL
);

GO

SET IDENTITY_INSERT dbo.SchoolDetailsMaster OFF;
GO

SELECT *
FROM dbo.SchoolDetailsMaster;