using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class DivisionMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : IDivisionMasterService
{
    public async Task<List<DivisionMaster>> GetAllAsync(
    bool filter = false,
    CancellationToken cancellationToken = default)
    {
        var divisions = await context.DivisionMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => !filter || x.IsActive)
            .ToListAsync(cancellationToken);

        return divisions
            .Select(x => MapDivision(x, languageService.CurrentLanguage))
            .ToList();
    }

    public async Task<DivisionMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var division = await context.DivisionMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.DivisionId == id, cancellationToken);

        return division is null
            ? null
            : MapDivision(division, languageService.CurrentLanguage);
    }

    public async Task AddAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DivisionMasters.AddAsync(Division, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Division master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DivisionMasters.Update(Division);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Division master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DivisionMasters.Remove(Division);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Division master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string DivisionName,
        OperationType operation,
        int? DivisionId,
        CancellationToken cancellationToken)
        => context.DivisionMasters.AnyAsync(
            x => x.DivisionName == DivisionName &&
                 (DivisionId == null || x.DivisionId != DivisionId),
            cancellationToken);

    public async Task<DivisionMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.DivisionMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.DivisionId == id, cancellationToken);

    private DivisionMaster MapDivision(DivisionMaster division, string language)
    {
        return new DivisionMaster
        {
            DivisionId = division.DivisionId,
            DivisionName = TranslationHelper.GetTranslatedValue(
                division.Translations,
                language,
                x => x.LanguageCode,
                x => x.DivisionName,
                division.DivisionName),

            IsActive = division.IsActive
        };
    }
}