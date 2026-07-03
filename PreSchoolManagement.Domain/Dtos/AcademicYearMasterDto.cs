using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class AcademicYearMasterDto
{
    [JsonIgnore]
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
}
