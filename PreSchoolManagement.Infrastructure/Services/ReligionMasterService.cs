using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class ReligionMasterService(ApplicationDbContext context) : IReligionMasterService
{
    public Task<List<ReligionMaster>> GetAllAsync(bool filter = false, CancellationToken cancellationToken = default)
        => context.ReligionMasters
        .Where(x => !filter|| x.IsActive)
        .AsNoTracking()
        .ToListAsync(cancellationToken);

    public async Task<ReligionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.ReligionMasters
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.ReligionId == id, cancellationToken);

    public async Task AddAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.ReligionMasters.AddAsync(religion, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a religion master record.");
            throw;
        }
    }

    public async Task UpdateAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ReligionMasters.Update(religion);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a religion master record.");
            throw;
        }
    }

    public async Task DeleteAsync(ReligionMaster religion, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.ReligionMasters.Remove(religion);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a religion master record.");
            throw;
        }
    }
    public Task<bool> IsExistsAsync(string religion, OperationType operation, int? religionId, CancellationToken cancellationToken)
    => context.ReligionMasters.AnyAsync(x => x.Religion == religion && (religionId == null || x.ReligionId != religionId), cancellationToken);
}