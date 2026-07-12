namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IRoleMenuPermissionService
{
    Task<List<RoleMenuPermissionDto>> GetByRoleAsync(
        int roleId,
        CancellationToken cancellationToken);

    Task SaveAsync(
        SaveRoleMenuPermissionDto model,
        Guid? userId,
        CancellationToken cancellationToken);
}