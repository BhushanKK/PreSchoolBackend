using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class EmployeeTypeMasterDto
{   
    [JsonIgnore]
    public int EmployeeTypeId { get; set; }
    public string EmployeeTypeName { get; set; } = string.Empty;
    public bool IsTeachingStaff { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}