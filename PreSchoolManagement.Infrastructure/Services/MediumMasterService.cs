using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
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
    public async Task<PaginatedResult<MediumMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<MediumMaster> query = context.MediumMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.MediumName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.MediumId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<MediumMaster>
        {
            Items = items
                .Select(x => MapMedium(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<MediumMaster?> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        return await context.MediumMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.MediumId == id, cancellationToken);
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
    => context.MediumMasters.AnyAsync(x => x.MediumName == medium &&
    (mediumId == null || x.MediumId != mediumId),cancellationToken);

    public async Task<MediumMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.MediumMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.MediumId == id, cancellationToken);

    private MediumMaster MapMedium(MediumMaster mediumMaster, string language)
    {
        return new MediumMaster
        {
            MediumId = mediumMaster.MediumId,

            MediumName = TranslationHelper.GetTranslatedValue(
                mediumMaster.Translations,
                language,
                x => x.LanguageCode,
                x => x.MediumName,
                mediumMaster.MediumName),

            IsActive = mediumMaster.IsActive,

            Translations = mediumMaster.Translations.ToList()
        };
    }
}