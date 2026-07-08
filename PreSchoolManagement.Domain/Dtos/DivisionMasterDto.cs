
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class DivisionMasterDto
{
    [JsonIgnore]
    public int DivisionId { get; set; }
    public string DivisionName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}





