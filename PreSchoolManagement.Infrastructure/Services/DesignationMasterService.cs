using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class DesignationMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : IDesignationMasterService
{
    public async Task<List<DesignationMaster>> GetAllAsync(
    bool filter = false,
    CancellationToken cancellationToken = default)
    {
        var designations = await context.DesignationMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => !filter || x.IsActive)
            .ToListAsync(cancellationToken);

        return designations
            .Select(x => MapDesignation(x, languageService.CurrentLanguage))
            .ToList();
    }

    public async Task<DesignationMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var designation = await context.DesignationMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.DesignationId == id, cancellationToken);

        return designation is null
            ? null
            : MapDesignation(designation, languageService.CurrentLanguage);
    }

    public async Task AddAsync(DesignationMaster designationMaster, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DesignationMasters.AddAsync(designationMaster, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occureed when adding a designation master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DesignationMaster designationMaster, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.DesignationMasters.Update(designationMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred when  updating a designation master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DesignationMaster designationMaster, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.DesignationMasters.Remove(designationMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a designation master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string designation, OperationType operation, int? designationId, CancellationToken cancellationToken)
    => context.DesignationMasters.AnyAsync(
        x => x.Designation == designation && (designationId == null || x.DesignationId != designationId),
        cancellationToken);

    public async Task<DesignationMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.DesignationMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.DesignationId == id, cancellationToken);

    private DesignationMaster MapDesignation(DesignationMaster designation, string language)
    {
        return new DesignationMaster
        {
            DesignationId = designation.DesignationId,
            Designation = TranslationHelper.GetTranslatedValue(
                designation.Translations,
                language,
                x => x.LanguageCode,
                x => x.Designation,
                designation.Designation),

            Status = designation.Status,
            IsActive = designation.IsActive
        };
    }
}