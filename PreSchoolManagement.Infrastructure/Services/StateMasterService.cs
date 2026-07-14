using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class StateMasterService(ApplicationDbContext context):IStateMasterService
{
    public Task<List<StateMaster>> GetAllAsync(
        bool applyFilter = false,
        CancellationToken cancellationToken = default) =>
        context.StateMasters
            .AsNoTracking()
            .Where(x => !applyFilter || x.IsActive)
            .ToListAsync(cancellationToken);

    public async Task<StateMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.StateMasters
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.StateId == id, cancellationToken);

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
}