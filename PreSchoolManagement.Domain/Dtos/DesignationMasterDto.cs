using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class DesignationMasterDto
{
    [JsonIgnore]
    public int DesignationId{get; set;}
    public string? Designation{get; set;}= string.Empty;
    public bool Status { get; set; } = true;
    public bool IsActive { get; set; } = false;
}