using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class FinancialYearMasterDto
{
    [JsonIgnore]
    public int FinancialYearId { get; set; }
    public string FinancialYearName { get; set; } = string.Empty;
}
