using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class DivisionMaster : BaseEntity
{
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<DivisionTranslation> Translations { get; set; }
        = new List<DivisionTranslation>();
}

public class DivisionTranslation
{
    public int DivisionTranslationId { get; set; }
    public int DivisionId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string DivisionName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual DivisionMaster Division { get; set; } = null!;
}