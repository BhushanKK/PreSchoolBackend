using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class ChairmanMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : IChairmanMasterService
{
    public async Task<PaginatedResult<ChairmanMasterQueryDto>> GetAllAsync(
    PaginationRequest request,
    CancellationToken cancellationToken = default)
    {
        var language = languageService.CurrentLanguage;

        var query = context.ChairmanMasters
            .AsNoTracking()
            .Where(x => !request.Filter || x.IsActive)
            .Select(Chairman => new ChairmanMasterQueryDto
            {
                CommitteeId = Chairman.CommiteeId,
                DesignationId = Chairman.DesignationId,

                Designation =
                    Chairman.Designation.Translations
                        .Where(t => t.LanguageCode == language)
                        .Select(t => t.DesignationName)
                        .FirstOrDefault()
                    ?? Chairman.Designation.DesignationName,

                ChairmanName =
                    Chairman.Translations
                        .Where(t => t.LanguageCode == language)
                        .Select(t => t.ChairmanName)
                        .FirstOrDefault()
                    ?? Chairman.ChairmanName,

                IsActive = Chairman.IsActive
            });

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var search = $"%{request.SearchText.Trim()}%";

            query = query.Where(x =>
                EF.Functions.Like(x.Designation, search) ||
                EF.Functions.Like(x.CommitteeName, search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.CommitteeName)
            .ThenBy(x => x.ChairmanName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ChairmanMasterQueryDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<List<ChairmanDropdownDto>> GetActiveChairmansAsync(
    CancellationToken cancellationToken)
    {
        var roles = await context.ChairmanMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => x.IsActive)
            .OrderBy(x => x.ChairmanName)
            .ToListAsync(cancellationToken);

        return roles.Select(x => new ChairmanDropdownDto
        {
            ChairmanId = x.ChairmanId,
            ChairmanName = TranslationHelper.GetTranslatedValue(
                x.Translations,
                languageService.CurrentLanguage,
                t => t.LanguageCode,
                t => t.ChairmanName,
                x.ChairmanName)
        }).ToList();
    }

    public async Task<ChairmanMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.ChairmanMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.ChairmanId == id, cancellationToken);
    }

    public async Task AddAsync(ChairmanMaster Chairman, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.ChairmanMasters.AddAsync(Chairman, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a Chairman master record.");
            throw;
        }
    }

    public async Task UpdateAsync(ChairmanMaster Chairman, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ChairmanMasters.Update(Chairman);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a Chairman master record.");
            throw;
        }
    }

    public async Task DeleteAsync(ChairmanMaster Chairman, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ChairmanMasters.Remove(Chairman);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a Chairman master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string Chairman, OperationType operation, int? ChairmanId, CancellationToken cancellationToken)
        => context.ChairmanMasters.AnyAsync(x => x.ChairmanName == Chairman && (ChairmanId == null || x.ChairmanId != ChairmanId), cancellationToken);

    public async Task<ChairmanMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.ChairmanMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.ChairmanId == id, cancellationToken);

    private ChairmanMaster MapChairman(ChairmanMaster Chairman, string language)
    {
        return new ChairmanMaster
        {
            ChairmanId = Chairman.ChairmanId,
            ChairmanName = TranslationHelper.GetTranslatedValue(
                Chairman.Translations,
                language,
                x => x.LanguageCode,
                x => x.ChairmanName,
                Chairman.ChairmanName),

            IsActive = Chairman.IsActive,

            Translations = Chairman.Translations.ToList()
        };
    }
}
