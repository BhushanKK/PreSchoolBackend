namespace SchoolManagement.Domain.Entities;

public class RoleMaster : BaseEntity
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
