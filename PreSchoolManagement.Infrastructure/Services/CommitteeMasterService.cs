using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Models;

namespace PreSchoolManagement.Infrastructure.Services;

public class CommitteeMasterService(ApplicationDbContext context) : ICommitteeMasterService
{
    public async Task<PaginatedResult<CommitteeMaster>> GetAllAsync(
    PaginationRequest request,
    CancellationToken cancellationToken)
    {
        IQueryable<CommitteeMaster> query = context.CommitteeMasters
            .AsNoTracking();

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x =>
                x.CommitteeName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CommitteeId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<CommitteeMaster>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public Task<CommitteeMaster?> GetByIdAsync(
        Guid committeeId,
        CancellationToken cancellationToken = default)
        => context.CommitteeMasters
            .FirstOrDefaultAsync(x => x.CommitteeId == committeeId, cancellationToken);

    public async Task AddAsync(
        CommitteeMaster committee,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.CommitteeMasters.AddAsync(committee, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding CommitteeMaster.");
            throw;
        }
    }

    public async Task UpdateAsync(
        CommitteeMaster committee,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CommitteeMasters.Update(committee);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating CommitteeMaster.");
            throw;
        }
    }

    public async Task DeleteAsync(
        CommitteeMaster committee,
        CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CommitteeMasters.Remove(committee);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting CommitteeMaster.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
    string committeeName,
    OperationType operation,
    Guid? committeeId,
    CancellationToken cancellationToken = default)
    {
        return operation switch
        {
            OperationType.Add =>
                context.CommitteeMasters.AnyAsync(
                    x => x.CommitteeName == committeeName,
                    cancellationToken),

            OperationType.Update =>
                context.CommitteeMasters.AnyAsync(
                    x => x.CommitteeName == committeeName &&
                         x.CommitteeId != committeeId,
                    cancellationToken),

            _ => Task.FromResult(false)
        };
    }
}