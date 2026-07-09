using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
namespace PreSchoolManagement.Infrastructure.Services;

public class PermissionService(ApplicationDbContext context) : IPermissionService
{
    public async Task<List<UserPermissionDto>> GetUserPermissionsAsync(int roleId,CancellationToken token)
    {
        var permissions = await
            (from rpm in context.RoleMenuPermissions

             join menu in context.MenuMasters
             on rpm.MenuId equals menu.MenuId

             where rpm.RoleId == roleId

             select new UserPermissionDto
             {
                 MenuId = menu.MenuId,
                 MenuName = menu.MenuName,
                 MenuUrl = menu.MenuUrl,
                 Icon = menu.Icon,
                 CanView = rpm.CanView,
                 CanAdd = rpm.CanAdd,
                 CanEdit = rpm.CanEdit,
                 CanDelete = rpm.CanDelete,
                 CanExport=rpm.CanExport,
                 CanPrint=rpm.CanPrint
             })

            .ToListAsync();


        return permissions;
    }
}