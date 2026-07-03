using Microsoft.EntityFrameworkCore;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Data;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace SchoolAdmission.Infrastructure.Services;

public class ReligionMasterService(ApplicationDbContext context) : IReligionMasterService
{
    public Task<List<ReligionMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.ReligionMasters.ToListAsync(cancellationToken);

    public async Task<ReligionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.ReligionMasters.FindAsync([id], cancellationToken);

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