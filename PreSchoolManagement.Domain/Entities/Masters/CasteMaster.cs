using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class CasteMaster : BaseEntity
{
    public int CasteID { get; set; }
    public int CategoryID { get; set; }
    public string CasteName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<CasteTranslation> Translations { get; set; }
        = new List<CasteTranslation>();
    public virtual CategoryMaster Category { get; set; } = null!;
}

public class CasteTranslation
{
    public int CasteTranslationId { get; set; }
    public int CasteID { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string CasteName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual CasteMaster Caste { get; set; } = null!;
}