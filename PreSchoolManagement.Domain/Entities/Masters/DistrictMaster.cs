using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class DistrictMaster : BaseEntity
{
    public int DistrictId { get; set; }
    public int StateId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<DistrictTranslation> Translations { get; set; }
        = new List<DistrictTranslation>();
}

public class DistrictTranslation
{
    public int DistrictTranslationId { get; set; }
    public int DistrictId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual DistrictMaster District { get; set; } = null!;
}