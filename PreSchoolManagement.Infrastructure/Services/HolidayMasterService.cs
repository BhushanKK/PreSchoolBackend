using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class HolidayMasterService(ApplicationDbContext context,ILanguageService languageService) : IHolidayMasterService
{
    public async Task<PaginatedResult<HolidayMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<HolidayMaster> query = context.HolidayMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.HolidayName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.HolidayId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<HolidayMaster>
        {
            Items = items
                .Select(x => MapHoliday(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<HolidayMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.HolidayMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.HolidayId == id,
                cancellationToken
            );
    }
    public async Task AddAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.HolidayMasters.AddAsync(Holiday, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Holiday master record.");
            throw;
        }
    }

    public async Task UpdateAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.HolidayMasters.Update(Holiday);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Holiday master record.");
            throw;
        }
    }

    public async Task DeleteAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.HolidayMasters.Remove(Holiday);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Holiday master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string HolidayName,
        OperationType operation,
        int? HolidayId,
        CancellationToken cancellationToken)
        => context.HolidayMasters.AnyAsync(
            x => x.HolidayName == HolidayName &&
                 (HolidayId == null || x.HolidayId != HolidayId),
            cancellationToken);
    
    private HolidayMaster MapHoliday(HolidayMaster holiday, string language)
    {
        return new HolidayMaster
        {
            HolidayId = holiday.HolidayId,
            HolidayName = TranslationHelper.GetTranslatedValue(
                holiday.Translations,
                language,
                x => x.LanguageCode,
                x => x.HolidayName,
                holiday.HolidayName),
            HolidayType=holiday.HolidayType,
            HolidayFromDate=holiday.HolidayFromDate,
            HolidayToDate=holiday.HolidayToDate,
            Description=holiday.Description,
            IsActive = holiday.IsActive,
        };
    }

    public async Task<HolidayMaster?> GetForUpdateAsync(int id, CancellationToken cancellationToken)
    => await context.HolidayMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.HolidayId == id, cancellationToken);
}