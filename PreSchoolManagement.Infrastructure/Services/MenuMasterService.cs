using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class MenuMasterService(
    ApplicationDbContext context,
    ICurrentUserService currentUser,
    ILanguageService languageService)
    : IMenuMasterService
{
    public async Task<PaginatedResult<MenuMasterQueryDto>> GetAllAsync(
    PaginationRequest request,
    CancellationToken cancellationToken)
{
    var roleId = currentUser.RoleId.ToString();

    IQueryable<MenuMaster> query = context.MenuMasters
        .AsNoTracking()
        .Include(x => x.Translations);

    if (request.Filter)
        query = query.Where(x => x.IsActive);

    if (!string.IsNullOrWhiteSpace(request.SearchText))
    {
        query = query.Where(x =>
            x.MenuName.Contains(request.SearchText));
    }

    var totalCount = await query.CountAsync(cancellationToken);

    var menus = await query
        .OrderBy(x => x.DisplayOrder)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

    var mappedMenus = menus
        .Select(x => MapMenu(x, languageService.CurrentLanguage))
        .ToList();

    var result = mappedMenus
        .Select(menu =>
        {
            var parent = mappedMenus.FirstOrDefault(x => x.MenuId == menu.ParentMenuId);

            return new MenuMasterQueryDto
            {
                MenuId = menu.MenuId,
                MenuName = menu.MenuName,
                ParentMenuId = menu.ParentMenuId,
                ParentMenuName = parent?.MenuName,
                MenuUrl = menu.MenuUrl,
                Icon = menu.Icon,
                DisplayOrder = menu.DisplayOrder,
                IsPublic = menu.IsPublic,
                IsActive = menu.IsActive,
                RoleIds = menu.RoleIds
            };
        })
        .ToList();

    return new PaginatedResult<MenuMasterQueryDto>
    {
        Items = result,
        TotalCount = totalCount,
        PageNumber = request.PageNumber,
        PageSize = request.PageSize
    };
}

    public async Task<MenuMaster?> GetByIdAsync(
        int menuId,
        CancellationToken cancellationToken)
    {
        return await context.MenuMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.MenuId == menuId,
                cancellationToken);
    }

    public async Task<MenuMaster?> GetForUpdateAsync(
        int menuId,
        CancellationToken cancellationToken)
    {
        return await context.MenuMasters
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.MenuId == menuId,
                cancellationToken);
    }

    public async Task<MenuMaster> CreateAsync(
        MenuMaster menuMaster,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.MenuMasters.AddAsync(menuMaster, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return menuMaster;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while adding menu master.");
            throw;
        }
    }

    public async Task<MenuMaster?> UpdateAsync(
        MenuMaster menuMaster,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.MenuMasters.Update(menuMaster);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return menuMaster;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(ex,
                "An error occurred while updating menu master.");

            throw;
        }
    }

    public async Task<bool> DeleteAsync(
        int menuId,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var menu = await context.MenuMasters
                .FirstOrDefaultAsync(
                    x => x.MenuId == menuId,
                    cancellationToken);

            if (menu == null)
                return false;

            var hasChildren = await context.MenuMasters
                .AnyAsync(
                    x => x.ParentMenuId == menuId,
                    cancellationToken);

            if (hasChildren)
                throw new InvalidOperationException(
                    "Cannot delete this menu because it has child menus.");

            var permissions = await context.RoleMenuPermissions
                .Where(x => x.MenuId == menuId)
                .ToListAsync(cancellationToken);

            if (permissions.Any())
                context.RoleMenuPermissions.RemoveRange(permissions);

            context.MenuMasters.Remove(menu);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(ex,
                "An error occurred while deleting menu master.");

            throw;
        }
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
            OperationType.Add =>
                query.Where(x =>
                    x.MenuName == menuName &&
                    x.ParentMenuId == parentMenuId),

            OperationType.Update =>
                query.Where(x =>
                    x.MenuName == menuName &&
                    x.ParentMenuId == parentMenuId &&
                    x.MenuId != menuId),

            _ => query
        };

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<List<ParentMenuDto>> GetParentMenusAsync(
        CancellationToken cancellationToken)
    {
        var parents = await context.MenuMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => x.IsActive && x.ParentMenuId == null)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);

        return parents
            .Select(x => new ParentMenuDto
            {
                MenuId = x.MenuId,
                MenuName = TranslationHelper.GetTranslatedValue(
                    x.Translations,
                    languageService.CurrentLanguage,
                    t => t.LanguageCode,
                    t => t.MenuName,
                    x.MenuName)
            })
            .ToList();
    }

    private MenuMaster MapMenu(
        MenuMaster menu,
        string language)
    {
        return new MenuMaster
        {
            MenuId = menu.MenuId,

            MenuName = TranslationHelper.GetTranslatedValue(
            menu.Translations,
            language,
            x => x.LanguageCode,
            x => x.MenuName,
            menu.MenuName),

            ParentMenuId = menu.ParentMenuId,
            MenuUrl = menu.MenuUrl,
            Icon = menu.Icon,
            DisplayOrder = menu.DisplayOrder,
            IsPublic = menu.IsPublic,
            IsActive = menu.IsActive,
            RoleIds = menu.RoleIds,

            Translations = menu.Translations.ToList()
        };
    }
}