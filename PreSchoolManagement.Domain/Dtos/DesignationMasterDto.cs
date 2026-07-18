using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class DesignationMasterDto
{
    [JsonIgnore]
    public int DesignationId {get; set;}
    public string Designation {get; set;}= string.Empty;
    public bool IsActive { get; set; } = false;
     public ICollection<DesignationTranslationDto> Translations { get; set; }
        = new List<DesignationTranslationDto>();
}

public class DesignationTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
}