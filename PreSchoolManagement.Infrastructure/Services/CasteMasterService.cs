using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class CasteMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : ICasteMasterService
{
    public async Task<List<CasteMasterQueryDto>> GetAllAsync(
    bool applyFilter = false,
    CancellationToken cancellationToken = default)
    {
        var language = languageService.CurrentLanguage;

        var query =
            from caste in context.CasteMasters.AsNoTracking()

            join category in context.CategoryMasters.AsNoTracking()
                on caste.CategoryID equals category.CategoryId

            join casteTranslation in context.CasteTranslations.AsNoTracking()
                .Where(x => x.LanguageCode == language)
                on caste.CasteID equals casteTranslation.CasteID into ct
            from casteTranslation in ct.DefaultIfEmpty()

            join categoryTranslation in context.CategoryTranslations.AsNoTracking()
                .Where(x => x.LanguageCode == language)
                on category.CategoryId equals categoryTranslation.CategoryId into cat
            from categoryTranslation in cat.DefaultIfEmpty()

            select new CasteMasterQueryDto
            {
                CasteId = caste.CasteID,
                CategoryId = category.CategoryId,

                CategoryName = categoryTranslation != null
                    ? categoryTranslation.CategoryName
                    : category.CategoryName,

                Caste = casteTranslation != null
                    ? casteTranslation.CasteName
                    : caste.CasteName,

                IsActive = caste.IsActive
            };

        if (applyFilter)
            query = query.Where(x => x.IsActive);

        return await query
            .OrderBy(x => x.CategoryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<CasteMaster?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var castes = await context.CasteMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.CasteID == id, cancellationToken);

        return castes is null
            ? null
            : MapCaste(castes, languageService.CurrentLanguage);
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
        => context.CasteMasters.AnyAsync(x => x.CasteName == caste && (casteId == null || x.CasteID != casteId), cancellationToken);

    public async Task<CasteMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.CasteMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.CasteID == id, cancellationToken);

    private CasteMaster MapCaste(CasteMaster caste, string language)
    {
        return new CasteMaster
        {
            CasteID = caste.CasteID,
            CategoryID = caste.CategoryID,
            CasteName = TranslationHelper.GetTranslatedValue(
                caste.Translations,
                language,
                x => x.LanguageCode,
                x => x.CasteName,
                caste.CasteName),

            IsActive = caste.IsActive
        };
    }
}
