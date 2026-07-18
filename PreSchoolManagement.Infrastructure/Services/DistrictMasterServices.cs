using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class DistrictMasterServices(
    ApplicationDbContext context,
    ILanguageService languageService) : IDistrictMasterService
{
    public async Task<List<DistrictMasterQueryDto>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        var language = languageService.CurrentLanguage;

        return await (
            from district in context.DistrictMasters.AsNoTracking()

            join state in context.StateMasters.AsNoTracking()
                on district.StateId equals state.StateId

            join districtTranslation in context.DistrictTranslations
                .AsNoTracking()
                .Where(x => x.LanguageCode == language)
                on district.DistrictId equals districtTranslation.DistrictId into dt
            from districtTranslation in dt.DefaultIfEmpty()

            orderby state.StateId

            select new DistrictMasterQueryDto
            {
                DistrictId = district.DistrictId,
                StateId = state.StateId,
                StateName = state.StateName,
                DistrictName = districtTranslation != null
                    ? districtTranslation.DistrictName
                    : district.DistrictName,

                IsActive = district.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DistrictMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var districts = await context.DistrictMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.DistrictId == id, cancellationToken);

        return districts is null
            ? null
            : MapDistrict(districts, languageService.CurrentLanguage);
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
            StateId = district.StateId,
            DistrictName = TranslationHelper.GetTranslatedValue(
                district.Translations,
                language,
                x => x.LanguageCode,
                x => x.DistrictName,
                district.DistrictName),

            IsActive = district.IsActive
        };
    }
}