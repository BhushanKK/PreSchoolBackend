using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class RoleMasterService(ApplicationDbContext context) : IRoleMasterService
{
    public Task<List<RoleMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.RoleMasters.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<RoleMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.RoleMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(RoleMaster role, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.RoleMasters.AddAsync(role, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding role master record.");
            throw;
        }
    }

    public async Task UpdateAsync(RoleMaster role, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.RoleMasters.Update(role);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating role master record.");
            throw;
        }
    }

    public async Task DeleteAsync(RoleMaster role, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.RoleMasters.Remove(role);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting role master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string roleName,
        OperationType operation,
        int? roleId,
        CancellationToken cancellationToken)
        => context.RoleMasters.AnyAsync(
            x => x.RoleName == roleName &&
                 (roleId == null || x.RoleId != roleId),
            cancellationToken);
}