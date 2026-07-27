using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class ChairmanMaster : BaseEntity
{
    public int ChairmanId { get; set; }
    public int CommiteeId { get; set; }
    public int DesignationId { get; set; }
    public string ChairmanName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public string SignaturePath { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<ChairmanTranslation> Translations { get; set; }
        = new List<ChairmanTranslation>();
    public virtual DesignationMaster Designation { get; set; } = null!;
     public virtual CommitteeMaster Committee { get; set; } = null!;
}

public class ChairmanTranslation
{
    public int ChairmanTranslationId { get; set; }
    public int ChairmanId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string ChairmanName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public string SignaturePath { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual DesignationMaster Designation { get; set; } = null!;
     public virtual CommitteeMaster Committee { get; set; } = null!;
}