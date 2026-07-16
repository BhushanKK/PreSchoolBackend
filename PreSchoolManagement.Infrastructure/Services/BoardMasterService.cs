using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class BoardMasterService(ApplicationDbContext context):IBoardMasterService
{
    public Task<List<BoardMaster>> GetAllAsync(CancellationToken cancellationToken)
    => context.BoardMasters.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<BoardMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    => await context.BoardMasters.FindAsync([id],cancellationToken);

    public async Task AddAsync (BoardMaster board,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.BoardMasters.AddAsync(board,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while adding board master record.");
            throw;
        }
    }

    public async Task UpdateAsync(BoardMaster board,CancellationToken cancellationToken)
    {
        await using var transaction = await  context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.BoardMasters.Update(board);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while updating board master record.");
            throw;
        }
    }

    public async Task DeleteAsync(BoardMaster board,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.BoardMasters.Remove(board);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting board master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string boardName, OperationType operation,int? boardId,CancellationToken cancellationToken)
    => context.BoardMasters.AnyAsync(x => x.BoardName == boardName && 
    (boardId == null || x.BoardId != boardId),
    cancellationToken);

}