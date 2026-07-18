namespace SchoolManagement.Domain.Entities;
public class BoardMaster : BaseEntity
{
    public int BoardId { get; set;}
    public string BoardName { get; set;} = string.Empty;
    public bool IsActive { get; set; } = false;
}