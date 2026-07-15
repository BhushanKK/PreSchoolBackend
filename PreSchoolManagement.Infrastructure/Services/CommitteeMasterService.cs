using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class CommitteeMasterService(ApplicationDbContext context) : ICommitteeMasterService
{
    public async Task<List<CommitteeMaster>> GetAllAsync(bool isFilter = false, CancellationToken cancellationToken= default)
    => await context.CommitteeMasters
        .AsNoTracking()
        .Where(x => !isFilter || x.IsActive)
        .ToListAsync(cancellationToken);

    public Task<CommitteeMaster?>GetByIdAsync(int id, CancellationToken cancellationToken)
    => context.CommitteeMasters
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.CommitteeId == id, cancellationToken);

    public async Task AddAsync(CommitteeMaster committee,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.CommitteeMasters.AddAsync(committee,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while adding Committee master record.");
            throw;
        }
    }

    public async Task UpdateAsync(CommitteeMaster committee,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.CommitteeMasters.Update(committee);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occureed while updating an Committee master record.");
            throw;
            
        }
    }
    public  async Task  DeleteAsync(CommitteeMaster committee,CancellationToken cancellationToken)
    {
        await using var transaction =await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.CommitteeMasters.Remove(committee);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting Committee master record.");
            throw;
        }
    }

    public Task<bool>IsExistsAsync(string CommitteeName, OperationType operation,int? CommitteeId, CancellationToken cancellationToken)
    => context.CommitteeMasters.AnyAsync(x => x.CommitteeName == CommitteeName &&(CommitteeId == null || x.CommitteeId !=CommitteeId),cancellationToken);
}