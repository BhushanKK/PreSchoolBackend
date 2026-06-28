namespace PreSchoolManagement.Domain.Entities;

public class StudentDetails
{
    public Guid StudentId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = string.Empty;
}