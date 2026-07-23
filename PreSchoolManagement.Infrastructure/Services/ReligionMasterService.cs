using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class ReligionMasterService(ApplicationDbContext context,ILanguageService languageService) 
: IReligionMasterService
{
    public async Task<PaginatedResult<ReligionMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<ReligionMaster> query = context.ReligionMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.ReligionName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.ReligionId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ReligionMaster>
        {
            Items = items
                .Select(x => MapReligion(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<ReligionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.ReligionMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.ReligionId == id,cancellationToken);
    }

    public async Task AddAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.ReligionMasters.AddAsync(religion, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a religion master record.");
            throw;
        }
    }

    public async Task UpdateAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ReligionMasters.Update(religion);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a religion master record.");
            throw;
        }
    }

    public async Task DeleteAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ReligionMasters.Remove(religion);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a religion master record.");
            throw;
        }
    }
    public Task<bool> IsExistsAsync(string religion, OperationType operation, int? religionId, CancellationToken cancellationToken)
    => context.ReligionMasters.AnyAsync(x => x.ReligionName == religion && (religionId == null || x.ReligionId != religionId), cancellationToken);

    public async Task<ReligionMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.ReligionMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.ReligionId == id,cancellationToken);

    private ReligionMaster MapReligion(ReligionMaster religion, string language)
    {
        return new ReligionMaster
        {
            ReligionId = religion.ReligionId,
            ReligionName = TranslationHelper.GetTranslatedValue(
                religion.Translations,
                language,
                x => x.LanguageCode,
                x => x.ReligionName,
                religion.ReligionName),
            IsMinority=religion.IsMinority,
            IsActive = religion.IsActive,

            Translations = religion.Translations.ToList()
        };
    }
}