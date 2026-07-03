  
using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class RoleMasterCommonDto
{
    [JsonIgnore]
    public int RoleId { get; set; }   
    public string RoleName { get; set; }
    public string? RoleDescription { get; set; }
}

  
  
  

