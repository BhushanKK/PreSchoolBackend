public class RoleMenuPermissionDto
{
    public int MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
    public bool CanView { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanPrint { get; set; }
    public bool CanExport { get; set; }
}

public class SaveRoleMenuPermissionDto
{
    public int RoleId { get; set; }
    public List<RoleMenuPermissionDto> Permissions { get; set; } = new();
}

public class UserPermissionDto : RoleMenuPermissionDto
{
    public string? MenuUrl { get; set; }
    public string? Icon { get; set; }
}