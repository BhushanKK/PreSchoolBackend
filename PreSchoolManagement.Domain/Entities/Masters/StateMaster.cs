namespace SchoolManagement.Domain.Entities;

public class StateMaster : BaseEntity
{
    public int StateId { get; set;}
    public string StateName { get; set;} = string.Empty;
    public bool IsActive { get; set; } = false;
}