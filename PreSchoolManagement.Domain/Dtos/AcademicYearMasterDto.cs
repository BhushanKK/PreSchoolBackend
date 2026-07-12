using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class AcademicYearMasterDto
{
    [JsonIgnore]
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsActive { get; set; } = false;
}
