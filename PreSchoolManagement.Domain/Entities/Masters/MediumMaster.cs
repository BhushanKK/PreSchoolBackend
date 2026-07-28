using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;
public class MediumMaster : BaseEntity
{
    public int MediumId{get;set;}
    public string MediumName {get; set;}= string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<MediumTranslation> Translations {get;set;}
    = new List<MediumTranslation>();
}

public class MediumTranslation
{
    public int MediumTranslationId {get;set;}
    public int MediumId {get; set;}
    public string LanguageCode {get;set;} = string.Empty;
    public string MediumName {get;set;}= string.Empty;
    [JsonIgnore]
    public virtual MediumMaster Medium {get;set;} = null!;
}