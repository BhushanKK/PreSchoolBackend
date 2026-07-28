using System.Text.Json.Serialization;
using PreSchoolManagement.Domain.Utils;

namespace SchoolManagement.Domain.Entities;

public class HolidayMaster : BaseEntity
{
    public int HolidayId { get; set; }
    public string HolidayName { get; set; } = string.Empty;
    public DateTime? HolidayFromDate { get; set; }
    public DateTime? HolidayToDate { get; set; }
    public HolidayType HolidayType { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = false;

    public virtual ICollection<HolidayTranslation> Translations {get; set;}
    = new List<HolidayTranslation>();
}

public class HolidayTranslation
{
    public int HolidayTranslationId {get; set;}
    public int HolidayId {get; set;} 
    public string LanguageCode {get;set;} = string.Empty;
    public string HolidayName {get; set;} = string.Empty;

    [JsonIgnore]
    public virtual HolidayMaster Holiday{get; set;} = null!;

}