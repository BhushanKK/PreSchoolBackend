using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class StandardMaster : BaseEntity
{
    public int StandardId { get; set; }
    public string StandardName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    
    [JsonIgnore]
    public virtual ICollection<StandardTranslation> Translations {get;set;}
    = new List<StandardTranslation>();
}

public class StandardTranslation
{
    public int StandardTranslationId {get;set;}
    public int StandardId {get;set;}
    public string LanguageCode {get;set;} = string.Empty;
    public string StandardName {get;set;} = string.Empty;
    public virtual StandardMaster Standard {get;set;} = null!;

}