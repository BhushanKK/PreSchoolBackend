namespace PreSchoolManagement.Shared.Utils;
public static class DateTimeExtensions
{
    public static string ToIndianDateTime(this DateTime utcDateTime)
    {
        var indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        var indiaTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, indiaTimeZone);

        return indiaTime.ToString("dd-MMM-yyyy h.mmtt");
    }
}