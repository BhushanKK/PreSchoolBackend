using System.Text.Json.Serialization;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Domain.Dtos;

public class StateMasterDto
{
    [JsonIgnore]
    public int StateId { get; set; }
    public string StateName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    public ICollection<StateTranslationDto> Translations {get; set;}
    = new List<StateTranslationDto>();
}

public class StateTranslationDto
{
    public string LangauageCode {get; set;} = string.Empty;

    public string StateName {get;set;}= string.Empty;
}