using System.Text.Json.Serialization;
using SchoolManagement.Domain;

public class MenuMasterDto
{
    [JsonIgnore]
    public int MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
    public int? ParentMenuId { get; set; }
    public string? MenuUrl { get; set; }
    public string? Icon { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsPublic { get; set; }
    public bool IsActive { get; set; }
    public string? RoleIds { get; set; }   // NEW
}

public class MenuMasterQueryDto : MenuMasterDto
{
    [JsonPropertyName("menuId")]
    public int Id
    {
        get => MenuId;
        set => MenuId = value;
    }
    public string? ParentMenuName { get; set; }
}
public class ParentMenuDto
{
    public int MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
}