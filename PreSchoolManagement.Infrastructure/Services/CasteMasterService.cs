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

public class CasteMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : ICasteMasterService
{
    public async Task<PaginatedResult<CasteMasterQueryDto>> GetAllAsync(
    PaginationRequest request,
    CancellationToken cancellationToken = default)
    {
        var language = languageService.CurrentLanguage;

        var query =
            from caste in context.CasteMasters.AsNoTracking()

            join category in context.CategoryMasters.AsNoTracking()
                on caste.CategoryId equals category.CategoryId

            join casteTranslation in context.CasteTranslations.AsNoTracking()
                .Where(x => x.LanguageCode == language)
                on caste.CasteId equals casteTranslation.CasteId into ct
            from casteTranslation in ct.DefaultIfEmpty()

            join categoryTranslation in context.CategoryTranslations.AsNoTracking()
                .Where(x => x.LanguageCode == language)
                on category.CategoryId equals categoryTranslation.CategoryId into cat
            from categoryTranslation in cat.DefaultIfEmpty()

            where !request.Filter || caste.IsActive

            select new CasteMasterQueryDto
            {
                CasteId = caste.CasteId,
                CategoryId = category.CategoryId,

                CategoryName = categoryTranslation != null
                    ? categoryTranslation.CategoryName
                    : category.CategoryName,

                CasteName = casteTranslation != null
                    ? casteTranslation.CasteName
                    : caste.CasteName,

                IsActive = caste.IsActive
            };

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var search = request.SearchText.Trim().ToLower();

            query = query.Where(x =>
                x.CasteName.ToLower().Contains(search) ||
                x.CategoryName.ToLower().Contains(search));
        }

        // Total records
        var totalCount = await query.CountAsync(cancellationToken);

        // Paging
        var items = await query
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.CasteName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<CasteMasterQueryDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<List<CasteDropdownDto>> GetActiveCastesAsync(
    CancellationToken cancellationToken)
    {
        var roles = await context.CasteMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => x.IsActive)
            .OrderBy(x => x.CasteName)
            .ToListAsync(cancellationToken);

        return roles.Select(x => new CasteDropdownDto
        {
            CasteId = x.CasteId,
            CasteName = TranslationHelper.GetTranslatedValue(
                x.Translations,
                languageService.CurrentLanguage,
                t => t.LanguageCode,
                t => t.CasteName,
                x.CasteName)
        }).ToList();
    }

    public async Task<CasteMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await context.CasteMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.CasteId == id, cancellationToken);
    }

    public async Task AddAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.CasteMasters.AddAsync(caste, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a caste master record.");
            throw;
        }
    }

    public async Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CasteMasters.Update(caste);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a caste master record.");
            throw;
        }
    }

    public async Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CasteMasters.Remove(caste);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a caste master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken)
        => context.CasteMasters.AnyAsync(x => x.CasteName == caste && (casteId == null || x.CasteId != casteId), cancellationToken);

    public async Task<CasteMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.CasteMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.CasteId == id, cancellationToken);

    private CasteMaster MapCaste(CasteMaster caste, string language)
    {
        return new CasteMaster
        {
            CasteId = caste.CasteId,
            CasteName = TranslationHelper.GetTranslatedValue(
                caste.Translations,
                language,
                x => x.LanguageCode,
                x => x.CasteName,
                caste.CasteName),

            IsActive = caste.IsActive,

            Translations = caste.Translations.ToList()
        };
    }
}
