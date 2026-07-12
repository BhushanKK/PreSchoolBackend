namespace SchoolManagement.Domain.Entities;
public class MenuMaster : BaseEntity
{
    public int MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
    public int? ParentMenuId { get; set; }
    public string? MenuUrl { get; set; }
    public string? Icon { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsPublic { get; set; }
    public string? RoleIds { get; set; } 
    public virtual MenuMaster? ParentMenu { get; set; }
    public virtual ICollection<MenuMaster> ChildMenus { get; set; } = new List<MenuMaster>();
}