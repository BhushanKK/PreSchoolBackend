namespace SchoolManagement.Shared.Enums;

public enum SchoolGrantType : byte
{
    Granted = 1,
    PartiallyGranted = 2,
    NonGranted = 3,
    SelfFinance = 4
}

public enum SchoolManagementType : byte
{
    Minority = 1,
    NonMinority = 2
}

public enum SchoolAreaType : byte
{
    Urban = 1,
    Rural = 2,
    Metro = 3
}