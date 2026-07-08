CREATE TABLE MenuMaster
(
    MenuId INT IDENTITY(1,1) PRIMARY KEY,

    MenuName NVARCHAR(100) NOT NULL,

    ParentMenuId INT NULL,

    MenuUrl NVARCHAR(200) NULL,

    Icon NVARCHAR(100) NULL,

    DisplayOrder INT NOT NULL DEFAULT(1),

    IsPublic BIT NOT NULL DEFAULT(0),

    IsActive BIT NOT NULL DEFAULT(1),

    EntryDate DATETIME NOT NULL DEFAULT(GETDATE()),

    ModifyDate DATETIME NULL,

    EntryBy uniqueidentifier NULL,

    ModifyBy uniqueidentifier NULL,

    CONSTRAINT FK_MenuMaster_Parent
        FOREIGN KEY (ParentMenuId)
        REFERENCES MenuMaster(MenuId)
);

CREATE TABLE RoleMenuPermission
(
    RoleMenuPermissionId INT IDENTITY(1,1) PRIMARY KEY,

    RoleId INT NOT NULL,

    MenuId INT NOT NULL,

    CanView BIT NOT NULL DEFAULT(0),

    CanAdd BIT NOT NULL DEFAULT(0),

    CanEdit BIT NOT NULL DEFAULT(0),

    CanDelete BIT NOT NULL DEFAULT(0),

    CanPrint BIT NOT NULL DEFAULT(0),

    CanExport BIT NOT NULL DEFAULT(0),

    IsActive BIT NOT NULL DEFAULT(1),

    EntryDate DATETIME NOT NULL DEFAULT(GETDATE()),

    ModifyDate DATETIME NULL,

    EntryBy uniqueidentifier NULL,

    ModifyBy uniqueidentifier NULL,

    CONSTRAINT FK_RMP_Role
        FOREIGN KEY(RoleId)
        REFERENCES RoleMaster(RoleId),

    CONSTRAINT FK_RMP_Menu
        FOREIGN KEY(MenuId)
        REFERENCES MenuMaster(MenuId),

    CONSTRAINT UQ_Role_Menu
        UNIQUE(RoleId, MenuId)
);

alter table UserDetailsMaster Add CONSTRAINT FK_User_Role
        FOREIGN KEY(RoleId)
        REFERENCES RoleMaster(RoleId)


SET IDENTITY_INSERT MenuMaster ON;
GO
INSERT INTO MenuMaster
(
    MenuId,
    MenuName,
    ParentMenuId,
    MenuUrl,
    Icon,
    DisplayOrder,
    IsPublic,
    IsActive,
    EntryDate
)
VALUES
-- Root Menus
(1, 'Dashboard', NULL, '/dashboard', 'Dashboard', 1, 0, 1, GETDATE()),
(2, 'Masters', NULL, NULL, 'AccountTree', 2, 0, 1, GETDATE())
-- Masters
INSERT INTO MenuMaster
(
    MenuId,
    MenuName,
    ParentMenuId,
    MenuUrl,
    Icon,
    DisplayOrder,
    IsPublic,
    IsActive,
    EntryDate
)
VALUES
(9, 'Role', 2, '/masters/role', 'Person', 1, 0, 1, GETDATE()),
(10, 'Menu', 2, '/masters/menu', 'Menu', 2, 0, 1, GETDATE()),
(11, 'Academic Year', 2, '/masters/academic-year', 'CalendarMonth', 3, 0, 1, GETDATE()),
(12, 'Financial Year', 2, '/masters/financial-year', 'CalendarToday', 4, 0, 1, GETDATE()),
(13, 'Religion', 2, '/masters/religion', 'TempleHindu', 5, 0, 1, GETDATE()),
(14, 'Category', 2, '/masters/category', 'Category', 6, 0, 1, GETDATE()),
(15, 'Caste', 2, '/masters/caste', 'Groups', 7, 0, 1, GETDATE());
SET IDENTITY_INSERT MenuMaster OFF;	
GO

ALTER TABLE MenuMaster
ADD RoleIds NVARCHAR(500) NULL;
