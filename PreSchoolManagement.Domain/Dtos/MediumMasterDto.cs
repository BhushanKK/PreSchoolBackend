using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class MediumMasterDto
{
    [JsonIgnore]
    public int MediumId{get; set;}
    public string Medium{get; set;}=string.Empty;
    public bool IsActive{get; set;}=false;

}