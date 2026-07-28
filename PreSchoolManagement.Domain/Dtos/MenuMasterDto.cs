using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

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

    public string? RoleIds { get; set; }
    public ICollection<MenuTranslationDto> Translations { get; set; }
        = new List<MenuTranslationDto>();
}

public class MenuTranslationDto
{
    public string LanguageCode { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
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