using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public  class MediumMasterService(ApplicationDbContext context,ILanguageService languageService)
:IMediumMasterService
{
    public async Task<List<MediumMaster>>GetAllAsync(CancellationToken cancellationToken)
    {
        var mediums = await context.MediumMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .ToListAsync(cancellationToken);

        return mediums.Select(x => MapMedium(x,languageService.CurrentLanguage)).ToList();
    }

    public async Task<MediumMaster?> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        var medium = await context.MediumMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.MediumId == id, cancellationToken);

        return medium is null
            ? null
            : MapMedium(medium, languageService.CurrentLanguage);
    }

    public async Task AddAsync (MediumMaster medium,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.MediumMasters.AddAsync(medium,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while adding medium master record.");
            throw;
        }
    }

    public async Task UpdateAsync(MediumMaster medium,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.MediumMasters.Update(medium);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while updating medium master record.");
            throw;
        }
    }

    public async Task DeleteAsync(MediumMaster medium,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.MediumMasters.Remove(medium);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting a medium master record.");
            throw;
        }
    }

    public Task<bool>IsExistsAsync(string medium,OperationType operation,int? mediumId,CancellationToken cancellationToken)
    => context.MediumMasters.AnyAsync(x => x.Medium == medium &&
    (mediumId == null || x.MediumId != mediumId),cancellationToken);

    public async Task<MediumMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.MediumMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.MediumId == id, cancellationToken);

    private MediumMaster MapMedium(MediumMaster medium,string language)
    {
        return new MediumMaster
        {
            MediumId = medium.MediumId,
            Medium = TranslationHelper.GetTranslatedValue(
                medium.Translations,
                language,
                x => x.LanguageCode,
                x => x.MediumName,
                medium.Medium),
            
            IsActive = medium.IsActive

        };
    }

}