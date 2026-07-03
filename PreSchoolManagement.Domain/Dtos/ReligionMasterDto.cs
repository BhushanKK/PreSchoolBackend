  
using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class ReligionMasterDto
{
    [JsonIgnore]
    public int ReligionId { get; set; }
    public bool IsMinority { get; set; }
    public string? Religion { get; set; }
    public Guid? UserId { get; set; }
}

  
  
  

