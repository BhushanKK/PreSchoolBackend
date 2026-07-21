
using System.Text.Json.Serialization;
using PreSchoolManagement.Domain.Utils;

namespace PreSchoolManagement.Domain.Dtos;

public class HolidayMasterDto
{
    [JsonIgnore]
    public int HolidayId { get; set; }
    public string? HolidayName { get; set; }
    public DateTime? HolidayFromDate { get; set; }
    public DateTime? HolidayToDate { get; set; }
    public HolidayType HolidayType { get; set; } = HolidayType.PublicHoliday;
    public string? Description { get; set; }
    public ICollection<HolidayTranslationDto> Translations { get; set;}
    = new List<HolidayTranslationDto>();

}

public class HolidayTranslationDto
{
    public string HolidayName { get; set;} = string.Empty;
    public string LanguageCode { get; set;} = string.Empty;
}