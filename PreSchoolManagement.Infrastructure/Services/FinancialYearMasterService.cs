using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class FinancialYearMasterService(ApplicationDbContext context
,ILanguageService languageService) : IFinancialYearMasterService
{
    public async Task<List<FinancialYearMaster>> GetAllAsync(bool filter = false,
    CancellationToken cancellationToken = default)
    {
        var financialYears = await context.FinancialYearMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .ToListAsync(cancellationToken);
        
        return financialYears.Select(x => MapFinancialYear(x, languageService.CurrentLanguage)).ToList();
    }

    public async Task<FinancialYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var financialYear = await context.FinancialYearMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.FinancialYearId == id,cancellationToken);

        return financialYear is null
            ? null
            : MapFinancialYear(financialYear,languageService.CurrentLanguage);
    }

    public async Task AddAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.FinancialYearMasters.AddAsync(financialYear, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding financialear master record.");
            throw;
        }
    }

    public async Task UpdateAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Update(financialYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating financial Year master record.");
            throw;
        }
    }

    public async Task DeleteAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Remove(financialYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a Financial Year master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string financialYear, OperationType operation, int? financialYearId, CancellationToken cancellationToken)
        => context.FinancialYearMasters.AnyAsync(x => x.FinancialYearName == financialYear && (financialYearId == null || x.FinancialYearId != financialYearId), cancellationToken);

    public async Task<FinancialYearMaster?> GetForUpdateAsync ( int id, CancellationToken cancellationToken)
    => await context.FinancialYearMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.FinancialYearId == id,cancellationToken);

    private FinancialYearMaster MapFinancialYear(FinancialYearMaster financialYear,string language)
    {
        return new FinancialYearMaster
        {
            FinancialYearId = financialYear.FinancialYearId,
            FinancialYearName = TranslationHelper.GetTranslatedValue(
                financialYear.Translations,
                language,
                x => x.LanguageCode,
                x => x.FinancialYearName,
                financialYear.FinancialYearName
            ),
            IsActive = financialYear.IsActive
        };
    }
}
