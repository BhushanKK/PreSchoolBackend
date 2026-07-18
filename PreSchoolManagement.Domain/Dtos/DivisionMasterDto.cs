
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class DivisionMasterDto
{
    [JsonIgnore]
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public ICollection<DivisionTranslationDto> Translations { get; set; }
    = new List<DivisionTranslationDto>();
}

public class DivisionTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string DivisionName { get; set; } = string.Empty;
}





