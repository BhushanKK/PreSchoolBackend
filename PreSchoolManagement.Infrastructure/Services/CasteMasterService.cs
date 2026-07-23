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

        IQueryable<CasteMaster> query = context.CasteMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Include(x => x.Category)
                .ThenInclude(x => x.Translations);

        if (request.Filter)
        {
            query = query.Where(x => x.IsActive);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            var search = request.SearchText.Trim().ToLower();

            query = query.Where(x =>
                x.CasteName.ToLower().Contains(search) ||
                x.Category.CategoryName.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
    .OrderByDescending(x => x.CasteId)
    .Skip((request.PageNumber - 1) * request.PageSize)
    .Take(request.PageSize)
    .ToListAsync(cancellationToken);

        return new PaginatedResult<CasteMasterQueryDto>
        {
            Items = items
                .Select(x => MapCaste(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
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

    private CasteMasterQueryDto MapCaste(
    CasteMaster caste,
    string language)
    {
        return new CasteMasterQueryDto
        {
            CasteId = caste.CasteId,
            CategoryId = caste.CategoryId,

            CasteName = TranslationHelper.GetTranslatedValue(
                caste.Translations,
                language,
                x => x.LanguageCode,
                x => x.CasteName,
                caste.CasteName),

            IsActive = caste.IsActive,

            Translations = caste.Translations
                .Select(x => new CasteTranslationDto
                {
                    CasteTranslationId = x.CasteTranslationId,
                    CasteId = x.CasteId,
                    LanguageCode = x.LanguageCode,
                    CasteName = x.CasteName
                })
                .ToList()
        };
    }
}
