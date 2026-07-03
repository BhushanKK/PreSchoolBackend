namespace SchoolManagement.Domain.Entities;

public class AcademicYearMaster : BaseEntity
{
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
}