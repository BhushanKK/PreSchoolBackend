using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class DistrictMasterServices(
    ApplicationDbContext context,
    ILanguageService languageService) : IDistrictMasterService
{
    public async Task<PaginatedResult<DistrictMasterQueryDto>> GetAllAsync(
    PaginationRequest request,
    CancellationToken cancellationToken = default)
{
    var language = languageService.CurrentLanguage;

    var query = context.DistrictMasters
        .AsNoTracking()
        .Where(x => !request.Filter || x.IsActive)
        .Select(district => new DistrictMasterQueryDto
        {
            DistrictId = district.DistrictId,
            StateId = district.StateId,

            StateName =
                district.State.Translations
                    .Where(t => t.LanguageCode == language)
                    .Select(t => t.StateName)
                    .FirstOrDefault()
                ?? district.State.StateName,

            DistrictName =
                district.Translations
                    .Where(t => t.LanguageCode == language)
                    .Select(t => t.DistrictName)
                    .FirstOrDefault()
                ?? district.DistrictName,

            IsActive = district.IsActive
        });

    // Search
    if (!string.IsNullOrWhiteSpace(request.SearchText))
    {
        var search = $"%{request.SearchText.Trim()}%";

        query = query.Where(x =>
            EF.Functions.Like(x.DistrictName, search) ||
            EF.Functions.Like(x.StateName, search));
    }

    // Total Records
    var totalCount = await query.CountAsync(cancellationToken);

    // Paging
    var items = await query
        .OrderBy(x => x.StateName)
        .ThenBy(x => x.DistrictName)
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToListAsync(cancellationToken);

    return new PaginatedResult<DistrictMasterQueryDto>
    {
        Items = items,
        TotalCount = totalCount,
        PageNumber = request.PageNumber,
        PageSize = request.PageSize
    };
}

    public async Task<DistrictMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.DistrictMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.DistrictId == id, cancellationToken);
    }

    public async Task AddAsync(DistrictMaster district, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DistrictMasters.AddAsync(district, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a district master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DistrictMaster district, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DistrictMasters.Update(district);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a district master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DistrictMaster district, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DistrictMasters.Remove(district);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a district master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string district, OperationType operation, int? districtId, CancellationToken cancellationToken)
        => context.DistrictMasters.AnyAsync(x => x.DistrictName == district && (districtId == null || x.DistrictId != districtId), cancellationToken);

    public async Task<DistrictMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.DistrictMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.DistrictId == id, cancellationToken);

    private DistrictMaster MapDistrict(DistrictMaster district, string language)
    {
        return new DistrictMaster
        {
            DistrictId = district.DistrictId,

            DistrictName = TranslationHelper.GetTranslatedValue(
                district.Translations,
                language,
                x => x.LanguageCode,
                x => x.DistrictName,
                district.DistrictName),

            IsActive = district.IsActive,

            Translations = district.Translations.ToList()
        };
    }
}