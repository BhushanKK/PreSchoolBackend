
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class StandardMasterDto
{
    [JsonIgnore]
    public int StandardId { get; set; }
    public string StandardName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public  ICollection<StandardTranslationDto> Translations {get;set;}
    = new List<StandardTranslationDto>();
}

public class StandardTranslationDto
{
    public string LanguageCode {get;set;} = string.Empty;
    public string StandardName {get;set;} = string.Empty;
}





