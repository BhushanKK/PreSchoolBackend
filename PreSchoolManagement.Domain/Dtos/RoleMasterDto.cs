using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class RoleMasterDto
{
    [JsonIgnore]
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public ICollection<RoleTranslationDto> Translations { get; set; } 
    = new List<RoleTranslationDto>();
}

public class RoleTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}




