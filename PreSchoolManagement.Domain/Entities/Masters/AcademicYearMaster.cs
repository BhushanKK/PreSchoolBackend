namespace SchoolManagement.Domain.Entities;

public class AcademicYearMaster : BaseEntity
{
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}