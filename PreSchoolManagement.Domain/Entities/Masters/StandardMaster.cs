namespace SchoolManagement.Domain.Entities;

public class StandardMaster : BaseEntity
{
    public int StandardId { get; set; }

    public string StandardName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}
