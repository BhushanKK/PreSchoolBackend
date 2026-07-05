
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class RoleMasterDto
{
    [JsonIgnore]
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? RoleDescription { get; set; }
    public bool IsActive { get; set; } = false;
}





