using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class StateMasterDto
{
    [JsonIgnore]
    public int StateId { get; set; }
    public string StateName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;

    public ICollection<StateTranslationDto> Translations { get; set; }
    = new List<StateTranslationDto>();
}

public class StateTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;

    public string StateName { get; set; } = string.Empty;
}

public class StateDropdownDto
{
    public int StateId { get; set; }
    public string StateName {get; set; } = string.Empty;
}

