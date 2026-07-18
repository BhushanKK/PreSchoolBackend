using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class CasteMasterDto
{
    [JsonIgnore]
    public int CasteId { get; set; }

    public int? CategoryId { get; set; }

    public string Caste { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public ICollection<CasteTranslationDto> Translations { get; set; } 
    = new List<CasteTranslationDto>();
}

public class CasteMasterQueryDto : CasteMasterDto
{
    [JsonPropertyName("casteId")]
    public int Id
    {
        get => CasteId;
        set => CasteId = value;
    }
    public string CategoryName { get; set; } = string.Empty;
}

public class CasteTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string Caste { get; set; } = string.Empty;
}