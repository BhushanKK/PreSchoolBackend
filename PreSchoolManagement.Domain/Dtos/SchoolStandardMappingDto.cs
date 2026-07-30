using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class SchoolStandardMappingDto
{
    [JsonIgnore]
    public Guid SchoolStandardMappingId { get; set; }
    public Guid SchoolRegistrationId { get; set; }
    public int StandardId { get; set; }
    public bool IsActive { get; set; } = false;

}