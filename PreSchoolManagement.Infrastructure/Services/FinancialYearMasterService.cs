using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class FinancialYearMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : IFinancialYearMasterService
{
    public async Task<PaginatedResult<FinancialYearMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<FinancialYearMaster> query = context.FinancialYearMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.FinancialYearName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.FinancialYearId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<FinancialYearMaster>
        {
            Items = items
                .Select(x => MapFinancialYear(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<FinancialYearMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.FinancialYearMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.FinancialYearId == id,
                cancellationToken);
    }

    public async Task AddAsync(
        FinancialYearMaster financialYear,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.FinancialYearMasters.AddAsync(
                financialYear,
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(
                ex,
                "An error occurred while adding Financial Year master record.");

            throw;
        }
    }

    public async Task UpdateAsync(
        FinancialYearMaster financialYear,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Update(financialYear);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(
                ex,
                "An error occurred while updating Financial Year master record.");

            throw;
        }
    }

    public async Task DeleteAsync(
        FinancialYearMaster financialYear,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Remove(financialYear);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            Log.Error(
                ex,
                "An error occurred while deleting Financial Year master record.");

            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string financialYearName,
        OperationType operation,
        int? financialYearId,
        CancellationToken cancellationToken)
        => context.FinancialYearMasters.AnyAsync(
            x => x.FinancialYearName == financialYearName &&
            (financialYearId == null || x.FinancialYearId != financialYearId),
            cancellationToken);

    public async Task<FinancialYearMaster?> GetForUpdateAsync(
        int id,
        CancellationToken cancellationToken)
        => await context.FinancialYearMasters
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.FinancialYearId == id,
                cancellationToken);

    private FinancialYearMaster MapFinancialYear(
        FinancialYearMaster financialYear,
        string language)
    {
        return new FinancialYearMaster
        {
            FinancialYearId = financialYear.FinancialYearId,

            FinancialYearName = TranslationHelper.GetTranslatedValue(
                financialYear.Translations,
                language,
                x => x.LanguageCode,
                x => x.FinancialYearName,
                financialYear.FinancialYearName),

            FromDate = financialYear.FromDate,
            ToDate = financialYear.ToDate,
            IsActive = financialYear.IsActive,

            Translations = financialYear.Translations.ToList()
        };
    }
}