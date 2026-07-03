  
using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class ReligionMasterDto
{
    [JsonIgnore]
    public int ReligionId { get; set; }
    public bool IsMinority { get; set; }
    public string? Religion { get; set; }
}

  
  
  

