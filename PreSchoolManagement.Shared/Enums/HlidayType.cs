namespace PreSchoolManagement.Domain.Utils;
public enum HolidayType
{
    PublicHoliday = 1,          // National/Public Holiday
    GovernmentHoliday = 2,      // Government Declared Holiday
    CollectorDeclaredHoliday = 3, // District Collector Declared Holiday
    SchoolHoliday = 4,          // School Specific Holiday
    FestivalHoliday = 5,        // Diwali, Eid, Christmas, etc.
    SummerVacation = 6,         // Summer Vacation
    WinterVacation = 7,         // Winter Vacation
    MonsoonVacation = 8,        // Monsoon Break (if applicable)
    MidTermBreak = 9,           // Mid-term Holidays
    ExamHoliday = 10,           // Study Leave / Exam Break
    ElectionHoliday = 11,       // Election Day
    EmergencyHoliday = 12,      // Natural Disaster / Emergency
    LocalHoliday = 13,          // District/City Specific Holiday
    OptionalHoliday = 14,       // Optional / Restricted Holiday
    Other = 15                  // Any Other Holiday
}