using System.Text.Json.Serialization;

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
    public bool IsActive { get; set; } = false;
    public virtual ICollection<MenuMaster> ChildMenus { get; set; } = new List<MenuMaster>();
    public virtual ICollection<MenuTranslation> Translations { get; set; }
    = new List<MenuTranslation>();
}

public class MenuTranslation
{
    public int MenuTranslationId { get; set; }
    public int MenuId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual MenuMaster MenuMaster { get; set; } = null!;
}