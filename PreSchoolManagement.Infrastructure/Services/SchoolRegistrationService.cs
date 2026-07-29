using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class SchoolRegistrationService (ApplicationDbContext context)
: ISchoolRegistrationService
{
    public async Task<PaginatedResult<SchoolRegistration>> GetAllAsync(PaginationRequest request,CancellationToken cancellationToken)
    {
        IQueryable<SchoolRegistration> query = context.SchoolRegistrations.AsNoTracking();

        if (request.Filter)
            query =query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.SchoolName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.SchoolRegistrationId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<SchoolRegistration>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<SchoolRegistration?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await context .SchoolRegistrations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SchoolRegistrationId == id, cancellationToken);
    }

    public async Task AddAsync(SchoolRegistration schoolRegistration,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.SchoolRegistrations.AddAsync(schoolRegistration,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while adding School Registration record.");
            throw;
        }
    }

    public async Task UpdateAsync(SchoolRegistration schoolRegistration,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.SchoolRegistrations.Update(schoolRegistration);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating School Registration record.");
            throw;
        }
    }

    public async Task DeleteAsync (SchoolRegistration schoolRegistration,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SchoolRegistrations.Remove(schoolRegistration);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting School Registration record");
            throw;
        }
    }

    public async Task<SchoolRegistration?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.SchoolRegistrations.FirstOrDefaultAsync(x => x.SchoolRegistrationId == id,cancellationToken);
    }

    public Task<bool> IsExistsAsync(string SchoolName, OperationType operation,Guid? SchoolRegistrationId,CancellationToken cancellationToken)
    {
        return context.SchoolRegistrations
        .AnyAsync(x => x.SchoolName == SchoolName && 
        (SchoolRegistrationId == null || x.SchoolRegistrationId != SchoolRegistrationId)
        ,cancellationToken);
    }
}