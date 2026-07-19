using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class MediumMasterDto
{
    [JsonIgnore]
    public int MediumId{get; set;}
    public string Medium{get; set;}=string.Empty;
    public bool IsActive{get; set;}=false;
    public ICollection<MediumTranslationDto> Translations {get;set;}
    = new List<MediumTranslationDto>();
    
}

public class MediumTranslationDto
{
    public string LanguageCode { get;set;} = string.Empty;
    public string MediumName {get;set;} =string.Empty;
}