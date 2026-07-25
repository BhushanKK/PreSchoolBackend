using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class SchoolDetailsMasterService(ApplicationDbContext context)
    : ISchoolDetailsMasterService
{
    public async Task<PaginatedResult<SchoolDetailsMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<SchoolDetailsMaster> query = context.SchoolDetailsMasters
            .AsNoTracking();

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.SchoolName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.SchoolDetailsId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<SchoolDetailsMaster>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<SchoolDetailsMaster?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await context.SchoolDetailsMasters
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SchoolDetailsId == id, cancellationToken);
    }

    public async Task AddAsync(
        SchoolDetailsMaster schoolDetailsMaster,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.SchoolDetailsMasters.AddAsync(
                schoolDetailsMaster,
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding School Details master record.");
            throw;
        }
    }

    public async Task UpdateAsync(
        SchoolDetailsMaster schoolDetailsMaster,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SchoolDetailsMasters.Update(schoolDetailsMaster);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating School Details master record.");
            throw;
        }
    }

    public async Task DeleteAsync(
        SchoolDetailsMaster schoolDetailsMaster,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SchoolDetailsMasters.Remove(schoolDetailsMaster);

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting School Details master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string schoolName,
        OperationType operation,
        Guid? schoolDetailsId,
        CancellationToken cancellationToken)
    {
        return context.SchoolDetailsMasters.AnyAsync(
            x => x.SchoolName == schoolName &&
                 (schoolDetailsId == null || x.SchoolDetailsId != schoolDetailsId),
            cancellationToken);
    }

    public async Task<SchoolDetailsMaster?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await context.SchoolDetailsMasters
            .FirstOrDefaultAsync(x => x.SchoolDetailsId == id, cancellationToken);
    }

}