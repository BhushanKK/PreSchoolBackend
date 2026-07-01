CREATE TABLE dbo.StaffMaster
(
    StaffId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),

    UserId UNIQUEIDENTIFIER NOT NULL,

    EmployeeCode NVARCHAR(20) UNIQUE,

    FirstName NVARCHAR(100),
    MiddleName NVARCHAR(100),
    LastName NVARCHAR(100),

    GenderId INT,
	SchoolId INT,
    ReligionId INT,
    CategoryId INT,

    DOB DATE,

    MobileNumber NVARCHAR(15),

    Email NVARCHAR(200),

    JoiningDate DATE,

    DesignationId INT,

    DepartmentId INT,

    Address NVARCHAR(500),

    PhotoPath NVARCHAR(500),

    AadhaarNumber NVARCHAR(20),

    PANNumber NVARCHAR(20),

    IsTeachingStaff BIT,

    IsActive BIT DEFAULT(1),

    EntryBy UNIQUEIDENTIFIER,
    EntryDate DATETIME DEFAULT(GETDATE()),

    ModifyBy UNIQUEIDENTIFIER,
    ModifyDate DATETIME,

    CONSTRAINT FK_Staff_User
        FOREIGN KEY(UserId)
        REFERENCES UserDetailsMaster(UserId)
);