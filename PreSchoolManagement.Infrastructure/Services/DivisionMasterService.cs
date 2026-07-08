using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class DivisionMasterService(ApplicationDbContext context) : IDivisionMasterService
{
    public Task<List<DivisionMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.DivisionMasters.ToListAsync(cancellationToken);

    public async Task<DivisionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.DivisionMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DivisionMasters.AddAsync(Division, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Division master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DivisionMasters.Update(Division);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Division master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DivisionMaster Division, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DivisionMasters.Remove(Division);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Division master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string DivisionName,
        OperationType operation,
        int? DivisionId,
        CancellationToken cancellationToken)
        => context.DivisionMasters.AnyAsync(
            x => x.DivisionName == DivisionName &&
                 (DivisionId == null || x.DivisionId != DivisionId),
            cancellationToken);
}