using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class FinancialYearMasterService(ApplicationDbContext context) : IFinancialYearMasterService
{
    public Task<List<FinancialYearMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.FinancialYearMasters.ToListAsync(cancellationToken);

    public async Task<FinancialYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.FinancialYearMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.FinancialYearMasters.AddAsync(financialYear, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding financialear master record.");
            throw;
        }
    }

    public async Task UpdateAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Update(financialYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating financial Year master record.");
            throw;
        }
    }

    public async Task DeleteAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.FinancialYearMasters.Remove(financialYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a Financial Year master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string financialYear, OperationType operation, int? financialYearId, CancellationToken cancellationToken)
        => context.FinancialYearMasters.AnyAsync(x => x.FinancialYearName == financialYear && (financialYearId == null || x.FinancialYearId != financialYearId), cancellationToken);
}
