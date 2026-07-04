CREATE TABLE AcademicYearMaster (AcademicYearId int identity(1,1) primary key not null,
AcademicYearName nvarchar(100),FromDate DateTime2,ToDate DateTime2,IsActive Bit,
entryby uniqueidentifier default null,EntryDate DATETIME2,
modifyby uniqueidentifier default null,ModifyDate DATETIME2)

CREATE TABLE FinancialYearMaster (AcademicYearId int identity(1,1) primary key not null,
FinancialYearName nvarchar(100),FromDate DateTime2,ToDate DateTime2,IsActive Bit,
entryby uniqueidentifier default null,EntryDate DATETIME2,
modifyby uniqueidentifier default null,ModifyDate DATETIME2)

