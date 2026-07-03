using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class AcademicYearMasterDto
{
    [JsonIgnore]
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
    public Guid? UserId { get; set; }
}
