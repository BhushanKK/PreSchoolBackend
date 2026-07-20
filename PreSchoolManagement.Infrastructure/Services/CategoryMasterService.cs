using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class CategoryMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : ICategoryMasterService
{
    public async Task<List<CategoryMaster>> GetAllAsync(
    bool filter = false,
    CancellationToken cancellationToken = default)
    {
        var categories = await context.CategoryMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => !filter || x.IsActive)
            .ToListAsync(cancellationToken);

        return categories
            .Select(x => MapCategory(x, languageService.CurrentLanguage))
            .ToList();
    }

    public async Task<CategoryMaster?> GetByIdAsync(
    int id,
    CancellationToken cancellationToken)
    {
        return await context.CategoryMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(
                x => x.CategoryId == id,
                cancellationToken);
    }

    public async Task AddAsync(CategoryMaster category, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.CategoryMasters.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a category master record.");
            throw;
        }
    }

    public async Task UpdateAsync(CategoryMaster category, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CategoryMasters.Update(category);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a category master record.");
            throw;
        }
    }

    public async Task DeleteAsync(CategoryMaster category, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CategoryMasters.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a category master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string category,
        OperationType operation,
        int? categoryId,
        CancellationToken cancellationToken)
        => context.CategoryMasters.AnyAsync(
            x => x.CategoryName == category &&
                 (categoryId == null || x.CategoryId != categoryId),
            cancellationToken);

    public async Task<CategoryMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.CategoryMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.CategoryId == id, cancellationToken);

    private CategoryMaster MapCategory(CategoryMaster category, string language)
    {
        return new CategoryMaster
        {
            CategoryId = category.CategoryId,
            CategoryName = TranslationHelper.GetTranslatedValue(
                category.Translations,
                language,
                x => x.LanguageCode,
                x => x.CategoryName,
                category.CategoryName),

            IsActive = category.IsActive,

            Translations = category.Translations.ToList()
        };
    }
}