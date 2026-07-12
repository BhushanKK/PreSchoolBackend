using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class RoleMenuPermissionService(
    ApplicationDbContext context)
    : IRoleMenuPermissionService
{
    #region Get Role Menu Permission

    public async Task<List<RoleMenuPermissionDto>> GetByRoleAsync(
        int roleId,
        CancellationToken cancellationToken)
    {
        return await
        (
            from menu in context.MenuMasters.AsNoTracking()

            join permission in context.RoleMenuPermissions
                    .Where(x => x.RoleId == roleId && x.IsActive)
                on menu.MenuId equals permission.MenuId
                into permissionMenus

            from permission in permissionMenus.DefaultIfEmpty()

            where menu.IsActive

            orderby menu.DisplayOrder

            select new RoleMenuPermissionDto
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,

                CanView = permission != null && permission.CanView,
                CanAdd = permission != null && permission.CanAdd,
                CanEdit = permission != null && permission.CanEdit,
                CanDelete = permission != null && permission.CanDelete,
                CanPrint = permission != null && permission.CanPrint,
                CanExport = permission != null && permission.CanExport
            }
        ).ToListAsync(cancellationToken);
    }

    #endregion

    #region Save Role Menu Permission

    public async Task SaveAsync(
        SaveRoleMenuPermissionDto model,
        Guid? userId,
        CancellationToken cancellationToken)
    {
        // Remove existing permissions for the role
        var existingPermissions = await context.RoleMenuPermissions
            .Where(x => x.RoleId == model.RoleId)
            .ToListAsync(cancellationToken);

        if (existingPermissions.Count > 0)
            context.RoleMenuPermissions.RemoveRange(existingPermissions);

        // Insert only menus that have at least one permission
        var permissions = model.Permissions
            .Where(x =>
                x.CanView ||
                x.CanAdd ||
                x.CanEdit ||
                x.CanDelete ||
                x.CanPrint ||
                x.CanExport)
            .Select(x => new RoleMenuPermission
            {
                RoleId = model.RoleId,
                MenuId = x.MenuId,

                CanView = x.CanView,
                CanAdd = x.CanAdd,
                CanEdit = x.CanEdit,
                CanDelete = x.CanDelete,
                CanPrint = x.CanPrint,
                CanExport = x.CanExport,
                IsActive = true,
                EntryDate = DateTime.Now,
                EntryBy = userId
            });

        await context.RoleMenuPermissions.AddRangeAsync(
            permissions,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
    #endregion
}