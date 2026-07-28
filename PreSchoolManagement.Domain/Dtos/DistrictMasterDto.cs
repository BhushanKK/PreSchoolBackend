using System.Text.Json.Serialization;

public class DistrictMasterDto
{
    [JsonIgnore]
    public int DistrictId { get; set; }
    public int? StateId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<DistrictTranslationDto> Translations { get; set; } =
    new List<DistrictTranslationDto>();
}

public class DistrictMasterQueryDto : DistrictMasterDto
{
    [JsonPropertyName("districtId")]
    public int Id
    {
        get => DistrictId;
        set => DistrictId = value;
    }
    public string StateName { get; set; } = string.Empty;
}

public class DistrictTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string DistrictName { get; set; } = string.Empty;
}