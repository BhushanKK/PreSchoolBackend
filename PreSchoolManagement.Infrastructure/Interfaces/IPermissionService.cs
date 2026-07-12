namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IPermissionService
{
    Task<List<UserPermissionDto>> GetUserPermissionsAsync(int roleId,CancellationToken token);
}