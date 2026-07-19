using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class BoardMaster : BaseEntity
{
    public int BoardId { get; set; }
    public string BoardName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<BoardTranslation> Translations { get; set; }
        = new List<BoardTranslation>();
}

public class BoardTranslation
{
    public int BoardTranslationId { get; set; }
    public int BoardId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string BoardName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual BoardMaster Board { get; set; } = null!;
}