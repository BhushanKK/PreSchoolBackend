  
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class ReligionMasterDto
{
    [JsonIgnore]
    public int ReligionId { get; set; }
    public bool IsMinority { get; set; }
    public string? ReligionName { get; set; }  
    public bool IsActive { get; set; } = false; 
    public ICollection<ReligionTranslationDto> Translations {get; set;}
    = new List<ReligionTranslationDto>();
}

public class ReligionTranslationDto
{
    public string LanguageCode {get; set;} = string.Empty;
    public string ReligionName {get; set;} = string.Empty;
}
  
  

