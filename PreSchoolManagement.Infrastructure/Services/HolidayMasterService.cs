using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class HolidayMasterService(ApplicationDbContext context) : IHolidayMasterService
{
    public Task<List<HolidayMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.HolidayMasters.ToListAsync(cancellationToken);

    public async Task<HolidayMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.HolidayMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.HolidayMasters.AddAsync(Holiday, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Holiday master record.");
            throw;
        }
    }

    public async Task UpdateAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.HolidayMasters.Update(Holiday);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Holiday master record.");
            throw;
        }
    }

    public async Task DeleteAsync(HolidayMaster Holiday, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.HolidayMasters.Remove(Holiday);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Holiday master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string HolidayName,
        OperationType operation,
        int? HolidayId,
        CancellationToken cancellationToken)
        => context.HolidayMasters.AnyAsync(
            x => x.HolidayName == HolidayName &&
                 (HolidayId == null || x.HolidayId != HolidayId),
            cancellationToken);
}