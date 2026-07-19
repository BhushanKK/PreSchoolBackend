using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class SectionMasterService(
    ApplicationDbContext context,
    ILanguageService languageService)
    : ISectionMasterService
{
    public async Task<List<SectionMaster>> GetAllAsync(
    bool filter = false,
    CancellationToken cancellationToken = default)
    {
        var sections = await context.SectionMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .Where(x => !filter || x.IsActive)
            .ToListAsync(cancellationToken);

        return sections
            .Select(x => MapSection(x, languageService.CurrentLanguage))
            .ToList();
    }

    public async Task<SectionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var language = languageService.CurrentLanguage;

        var section = await context.SectionMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.SectionId == id, cancellationToken);

        return section is null 
            ? null 
            : MapSection(section, languageService.CurrentLanguage);
    }

    public async Task AddAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.SectionMasters.AddAsync(Section, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Section master record.");
            throw;
        }
    }

    public async Task UpdateAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SectionMasters.Update(Section);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Section master record.");
            throw;
        }
    }

    public async Task DeleteAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SectionMasters.Remove(Section);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Section master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string SectionName,
        OperationType operation,
        int? SectionId,
        CancellationToken cancellationToken)
        => context.SectionMasters.AnyAsync(
            x => x.SectionName == SectionName &&
                 (SectionId == null || x.SectionId != SectionId),
            cancellationToken);
    public async Task<SectionMaster?> GetForUpdateAsync(
    int id,
    CancellationToken cancellationToken)
    => await context.SectionMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.SectionId == id, cancellationToken);

    private SectionMaster MapSection(SectionMaster section, string language)
    {
        return new SectionMaster
        {
            SectionId = section.SectionId,
            SectionName = TranslationHelper.GetTranslatedValue(
                section.Translations,
                language,
                x => x.LanguageCode,
                x => x.SectionName,
                section.SectionName),
            IsActive = section.IsActive,
            Translations = section.Translations.ToList()
        };
    }
}