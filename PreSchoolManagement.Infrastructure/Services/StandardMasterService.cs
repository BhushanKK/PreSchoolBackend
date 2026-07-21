using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class StandardMasterService(ApplicationDbContext context,
ILanguageService languageService) : IStandardMasterService
{
    public async Task<List<StandardMaster>> GetAllAsync(bool filter, CancellationToken cancellationToken = default)
    {
        var standards = await context.StandardMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => !filter || x.IsActive)
            .ToListAsync(cancellationToken);

        return standards
            .Select(standard => MapStandard(standard, languageService.CurrentLanguage))
            .ToList();
    }

    public async Task<StandardMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.StandardMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.StandardId == id,cancellationToken);
    }

    public async Task AddAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.StandardMasters.AddAsync(Standard, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Standard master record.");
            throw;
        }
    }

    public async Task UpdateAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.StandardMasters.Update(Standard);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Standard master record.");
            throw;
        }
    }

    public async Task DeleteAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.StandardMasters.Remove(Standard);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Standard master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string StandardName,
        OperationType operation,
        int? StandardId,
        CancellationToken cancellationToken)
        => context.StandardMasters.AnyAsync(
            x => x.StandardName == StandardName &&
                 (StandardId == null || x.StandardId != StandardId),
            cancellationToken);

    public async Task<StandardMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.StandardMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.StandardId == id,cancellationToken);

    private StandardMaster MapStandard(StandardMaster standard, string language)
    {
        return new StandardMaster
        {
            StandardId = standard.StandardId,
            StandardName = TranslationHelper.GetTranslatedValue(
                standard.Translations,
                language,
                x => x.LanguageCode,
                x => x.StandardName,
                standard.StandardName),

            IsActive = standard.IsActive,

            Translations = standard.Translations.ToList()
        };
    }
}