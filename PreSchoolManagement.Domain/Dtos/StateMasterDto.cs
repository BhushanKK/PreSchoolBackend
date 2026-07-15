using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class StateMasterDto
{
    [JsonIgnore]
    public int StateId { get; set;}
    public string? StateName { get; set;}
    public bool IsActive { get; set;} = false;
}