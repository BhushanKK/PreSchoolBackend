using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Common;

namespace PreSchoolManagement.Infrastructure.Services;

public class StateMasterService(ApplicationDbContext context,
ILanguageService languageService):IStateMasterService
{
    public async Task<List<StateMaster>> GetAllAsync(
        CancellationToken cancellationToken )
    {
        var states = await context.StateMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .ToListAsync(cancellationToken);
        
        return states.Select(x => MapState(x, languageService.CurrentLanguage)).ToList();
    }

    public async Task<StateMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    { 
        var state = await context.StateMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.StateId == id, cancellationToken);

        return state is null 
            ? null
            : MapState(state, languageService.CurrentLanguage);
    }

    public async Task AddAsync (StateMaster state,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.StateMasters.AddAsync(state,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred when adding a state master record.");
            throw;
        }
    }

    public async Task UpdateAsync(StateMaster state,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.StateMasters.Update(state);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred when updting a state master record.");
            throw;
        }
    }

    public async Task DeleteAsync(StateMaster state,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.StateMasters.Remove(state);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a state master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string state,
        OperationType operation, 
        int? stateId, 
        CancellationToken cancellationToken)
    => context.StateMasters.AnyAsync(
           x => x.StateName == state &&
           (stateId == null || x.StateId != stateId),
           cancellationToken);

    public async Task<StateMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.StateMasters
        .Include(x =>x.Translations)
        .FirstOrDefaultAsync(x => x.StateId == id,cancellationToken);

    private StateMaster MapState(StateMaster state, string language)
    {
        return new StateMaster
        {
            StateId = state.StateId,
            StateName = TranslationHelper.GetTranslatedValue(
                state.Translations,
                language,
                x => x.LanguageCode,
                x => x.StateName,
                state.StateName),
            
            IsActive = state.IsActive
        };
    }
}
