using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class StandardMasterService(ApplicationDbContext context) : IStandardMasterService
{
    public Task<List<StandardMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.StandardMasters.ToListAsync(cancellationToken);

    public async Task<StandardMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.StandardMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.StandardMasters.AddAsync(Standard, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Standard master record.");
            throw;
        }
    }

    public async Task UpdateAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.StandardMasters.Update(Standard);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Standard master record.");
            throw;
        }
    }

    public async Task DeleteAsync(StandardMaster Standard, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.StandardMasters.Remove(Standard);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Standard master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string StandardName,
        OperationType operation,
        int? StandardId,
        CancellationToken cancellationToken)
        => context.StandardMasters.AnyAsync(
            x => x.StandardName == StandardName &&
                 (StandardId == null || x.StandardId != StandardId),
            cancellationToken);
}