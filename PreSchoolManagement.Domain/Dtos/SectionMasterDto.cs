
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class SectionMasterDto
{
    [JsonIgnore]
    public int SectionId { get; set; }
    public string SectionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}





