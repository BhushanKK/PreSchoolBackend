using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class DesignationMaster : BaseEntity
{
    public int DesignationId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public bool IsActive { get; set; } = false;

    [JsonIgnore]
    public virtual ICollection<DesignationTranslation> Translations { get; set; }
        = new List<DesignationTranslation>();
}

public class DesignationTranslation
{
    public int DesignationTranslationId { get; set; }
    public int DesignationId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public virtual DesignationMaster DesignationMaster { get; set; } = null!;
}