namespace SchoolManagement.Domain.Entities;

public class CategoryMaster : BaseEntity
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
}
