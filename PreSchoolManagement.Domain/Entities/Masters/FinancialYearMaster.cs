using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class FinancialYearMaster : BaseEntity
{
    public int FinancialYearId { get; set; }
    public string FinancialYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsActive { get; set; } = false;
    public virtual ICollection<FinancialYearTranslation> Translations { get; set; }
        = new List<FinancialYearTranslation>();
}

public class FinancialYearTranslation
{
    public int FinancialYearTranslationId { get; set; }
    public int FinancialYearId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string FinancialYearName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual FinancialYearMaster FinancialYear { get; set; } = null!;
}