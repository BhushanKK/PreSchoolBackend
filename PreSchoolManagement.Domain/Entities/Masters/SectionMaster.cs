using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class SectionMaster : BaseEntity
{
    public int SectionId { get; set; }
    public string SectionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    [JsonIgnore]
    public virtual ICollection<SectionTranslation> Translations { get;set;} 
    = new List<SectionTranslation>();
}
public class SectionTranslation
{
    public int SectionTranslationId { get; set; }
    public int SectionId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public virtual SectionMaster Section { get; set; } = null!;
}