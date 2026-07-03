  
using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class RoleMasterDto
{
    [JsonIgnore]
    public int RoleId { get; set; }   
    public string RoleName { get; set; } = string.Empty;
    public string? RoleDescription { get; set; }
    [JsonIgnore]
    public Guid? UserId { get; set; }
}

  
  
  

