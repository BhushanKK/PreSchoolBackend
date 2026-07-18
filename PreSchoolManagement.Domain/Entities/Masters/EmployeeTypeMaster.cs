namespace SchoolManagement.Domain.Entities;

public class EmployeeTypeMaster : BaseEntity
{
    public int EmployeeTypeId {get; set;}
    public string EmployeeTypeName { get; set; } = string.Empty;
    public bool IsTeachingStaff { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = false;  
}