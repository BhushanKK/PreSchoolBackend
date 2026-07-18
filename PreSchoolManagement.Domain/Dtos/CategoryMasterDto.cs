using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class CategoryMasterDto
{
    [JsonIgnore]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public ICollection<CategoryTranslationDto> Translations { get; set; } 
    = new List<CategoryTranslationDto>();
}

public class CategoryTranslationDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;
}
