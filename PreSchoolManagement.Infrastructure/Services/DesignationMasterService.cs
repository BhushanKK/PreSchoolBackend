using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class DesignationMasterService(ApplicationDbContext context):IDesignationMasterService
{
    public Task<List<DesignationMaster>> GetAllAsync(bool applyfilter = false,CancellationToken cancellationToken= default)
    =>context.DesignationMasters
        .AsNoTracking()
        .Where(x => !applyfilter || x.IsActive)
        .ToListAsync(cancellationToken);

    public async Task<DesignationMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.DesignationMasters
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.DesignationId == id, cancellationToken);

    public async Task AddAsync(DesignationMaster designationMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DesignationMasters.AddAsync(designationMaster,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occureed when adding a designation master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DesignationMaster designationMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.DesignationMasters.Update(designationMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred when  updating a designation master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DesignationMaster designationMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.DesignationMasters.Remove(designationMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting a designation master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string designation,OperationType operation,int? designationId,CancellationToken cancellationToken)
    =>context.DesignationMasters.AnyAsync(
        x => x.Designation == designation && (designationId ==null || x.DesignationId != designationId),
        cancellationToken);
    
}