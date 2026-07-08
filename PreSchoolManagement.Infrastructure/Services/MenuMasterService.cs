using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class MenuMasterService(ApplicationDbContext context, ICurrentUserService currentUser)
    : IMenuMasterService
{
    public async Task<List<MenuMasterQueryDto>> GetAllAsync(
    bool applyRoleFilter,
    CancellationToken cancellationToken)
    {
        var roleId = currentUser.RoleId.ToString();

        var menus = await
        (
            from menu in context.MenuMasters.AsNoTracking()
            join parent in context.MenuMasters.AsNoTracking()
                on menu.ParentMenuId equals parent.MenuId
                into parentMenus
            from parent in parentMenus.DefaultIfEmpty()
            orderby menu.DisplayOrder
            select new MenuMasterQueryDto
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                ParentMenuId = menu.ParentMenuId,
                ParentMenuName = parent != null
                    ? parent.MenuName
                    : null,
                MenuUrl = menu.MenuUrl,
                Icon = menu.Icon,
                DisplayOrder = menu.DisplayOrder,
                IsPublic = menu.IsPublic,
                IsActive = menu.IsActive,
                RoleIds = menu.RoleIds
            }
        ).ToListAsync(cancellationToken);

        if (!applyRoleFilter)
            return menus;

        return menus
    .Where(x =>
        x.IsActive &&
        (
            x.IsPublic ||
            (
                !string.IsNullOrWhiteSpace(x.RoleIds) &&
                x.RoleIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Contains(roleId)
            )
        ))
    .ToList();
    }

    public Task<MenuMaster?> GetByIdAsync(
        int menuId,
        CancellationToken cancellationToken)
    {
        return context.MenuMasters
            .FirstOrDefaultAsync(
                x => x.MenuId == menuId,
                cancellationToken);
    }

    public async Task<MenuMaster> CreateAsync(
        MenuMaster menuMaster,
        CancellationToken cancellationToken)
    {
        await context.MenuMasters.AddAsync(
            menuMaster,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        return menuMaster;
    }

    public async Task<MenuMaster?> UpdateAsync(
        MenuMaster menuMaster,
        CancellationToken cancellationToken)
    {
        context.MenuMasters.Update(menuMaster);
        await context.SaveChangesAsync(cancellationToken);
        return menuMaster;
    }

    public async Task<bool> DeleteAsync(
        int menuId,
        CancellationToken cancellationToken)
    {
        var menu = await context.MenuMasters
            .FindAsync(menuId, cancellationToken);

        if (menu == null)
            return false;

        context.MenuMasters.Remove(menu);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> IsExistsAsync(
    string menuName,
    int? parentMenuId,
    OperationType operationType,
    int? menuId,
    CancellationToken cancellationToken)
    {
        var query = context.MenuMasters.AsQueryable();

        query = operationType switch
        {
            OperationType.Add => query.Where(x =>
                x.MenuName == menuName &&
                x.ParentMenuId == parentMenuId),

            OperationType.Update => query.Where(x =>
                x.MenuName == menuName &&
                x.ParentMenuId == parentMenuId &&
                x.MenuId != menuId),

            _ => query
        };

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<List<ParentMenuDto>> GetParentMenusAsync(CancellationToken cancellationToken)
    {
        return await context.MenuMasters
            .Where(x => x.IsActive && x.ParentMenuId == null)
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new ParentMenuDto
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName
            })
            .ToListAsync(cancellationToken);
    }
}