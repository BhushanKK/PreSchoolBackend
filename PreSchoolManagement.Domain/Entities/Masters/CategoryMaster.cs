using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class CategoryMaster : BaseEntity
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    [JsonIgnore]
    public virtual ICollection<CategoryTranslation> Translations { get; set; }
        = new List<CategoryTranslation>();
}

public class CategoryTranslation
{
    public int CategoryTranslationId { get; set; }
    public int CategoryId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public virtual CategoryMaster Category { get; set; } = null!;
}