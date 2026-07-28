using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class ReligionMaster : BaseEntity
{
    public int ReligionId { get; set; }
    public string ReligionName { get; set; } = string.Empty;
    public bool IsMinority { get; set; }
    public bool IsActive { get; set; } = false;    
    public virtual ICollection<ReligionTranslation> Translations {get; set;}
    = new List<ReligionTranslation>();
 }

public class ReligionTranslation
{
    public int ReligionTranslationId { get; set;}
    public int ReligionId {get; set;}
    public string LanguageCode { get; set;} = string.Empty;
    public string ReligionName {get; set;} = string.Empty;
    [JsonIgnore]
    public virtual ReligionMaster Religion {get; set;} = null!;
}