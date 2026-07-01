namespace SchoolManagement.Domain.Entities;

public class StaffMaster : BaseEntity
{
    public Guid StaffId { get; set; }

    public Guid UserId { get; set; }

    public string? EmployeeCode { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public int? GenderId { get; set; }

    public int? SchoolId { get; set; }

    public int? ReligionId { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? DOB { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? JoiningDate { get; set; }

    public int? DesignationId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Address { get; set; }

    public string? PhotoPath { get; set; }

    public string? AadhaarNumber { get; set; }

    public string? PANNumber { get; set; }

    public bool? IsTeachingStaff { get; set; }

    public bool? IsActive { get; set; }
}
