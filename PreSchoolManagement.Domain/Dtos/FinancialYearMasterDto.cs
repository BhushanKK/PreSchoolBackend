using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class FinancialYearMasterDto
{
    [JsonIgnore]
    public int FinancialYearId { get; set; }
    public string FinancialYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsActive { get; set; }
}
