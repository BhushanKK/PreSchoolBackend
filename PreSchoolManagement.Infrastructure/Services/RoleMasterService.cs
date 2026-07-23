using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class RoleMasterService(
    ApplicationDbContext context,
    ILanguageService languageService)
    : IRoleMasterService
{
    public async Task<PaginatedResult<RoleMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<RoleMaster> query = context.RoleMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.RoleName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.RoleId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<RoleMaster>
        {
            Items = items
                .Select(x => MapRole(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<RoleMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.RoleMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.RoleId == id,
                cancellationToken);            
    }

    public async Task<RoleMaster?> GetForUpdateAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.RoleMasters
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.RoleId == id,
                cancellationToken);
    }

    public async Task AddAsync(
        RoleMaster role,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.RoleMasters.AddAsync(role, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(ex,
                "An error occurred while adding Role Master.");

            throw;
        }
    }

    public async Task UpdateAsync(
        RoleMaster role,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.RoleMasters.Update(role);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(ex,
                "An error occurred while updating Role Master.");

            throw;
        }
    }

    public async Task DeleteAsync(
        RoleMaster role,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.RoleMasters.Remove(role);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(ex,
                "An error occurred while deleting Role Master.");

            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string roleName,
        OperationType operation,
        int? roleId,
        CancellationToken cancellationToken)
    {
        return context.RoleMasters.AnyAsync(
            x => x.RoleName == roleName &&
            (roleId == null || x.RoleId != roleId),
            cancellationToken);
    }

    private RoleMaster MapRole(RoleMaster role,string language)
    {
        return new RoleMaster
        {
            RoleId = role.RoleId,
            RoleName = TranslationHelper.GetTranslatedValue(
                role.Translations,
                language,
                x => x.LanguageCode,
                x => x.RoleName,
                role.RoleName),

            IsActive = role.IsActive,

            Translations = role.Translations.ToList()
        };
    }
}