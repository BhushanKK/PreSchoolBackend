namespace SchoolManagement.Domain.Entities;

public class FinancialYearMaster : BaseEntity
{
    public int FinancialYearId { get; set; }
    public string FinancialYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}