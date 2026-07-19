
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class HolidayMasterDto
{
    [JsonIgnore]
    public int HolidayId { get; set; }
    public string? HolidayName { get; set; }
    public DateTime HolidayDate { get; set; }
    public string? HolidayType { get; set; }
    public string?   Description { get; set; }
    public ICollection<HolidayTranslationDto> Translations { get; set;}
    = new List<HolidayTranslationDto>();

}

public class HolidayTranslationDto
{
    public string HolidayName { get; set;} = string.Empty;
    public string LanguageCode { get; set;} = string.Empty;
}