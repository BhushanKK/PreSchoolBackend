
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class StandardMasterDto
{
    [JsonIgnore]
    public int StandardId { get; set; }
    public string StandardName { get; set; } = string.Empty;
}





