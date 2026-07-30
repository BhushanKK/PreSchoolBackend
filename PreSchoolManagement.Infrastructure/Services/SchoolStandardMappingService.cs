using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class SchoolStandardMappingService(ApplicationDbContext context)
: ISchoolStandardMappingService
{
    
    public async Task<PaginatedResult<SchoolStandardMapping>> GetAllAsync
    (PaginationRequest request,CancellationToken cancellationToken)
    {
        IQueryable<SchoolStandardMapping> query = context.SchoolStandardMappings
            .AsNoTracking();

        if(request.Filter)
            query =query.Where(x => x.IsActive);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query 
            .OrderByDescending(x => x.SchoolStandardMappingId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<SchoolStandardMapping>
        {
            Items = items, 
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<SchoolStandardMapping?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await context.SchoolStandardMappings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SchoolStandardMappingId == id,cancellationToken);
    }

    public async Task AddAsync(SchoolStandardMapping schoolStandardMapping, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.SchoolStandardMappings.AddAsync(schoolStandardMapping,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding school standard mapping record.");
            throw;
        }
    }

    public async Task UpdateAsync(SchoolStandardMapping schoolStandardMapping, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.SchoolStandardMappings.Update(schoolStandardMapping);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while updating school standard mapping record.");
            throw;
        }

    }

    public async Task DeleteAsync(SchoolStandardMapping schoolStandardMapping, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SchoolStandardMappings.Remove(schoolStandardMapping); 
            await  context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting school standard mapping record.");
            throw;
        }

    }

    public async Task<SchoolStandardMapping?> GetForUpdateAsync(Guid schoolStandardMappingId, CancellationToken cancellationToken)
    => await context.SchoolStandardMappings
        .FirstOrDefaultAsync(x => x.SchoolStandardMappingId == schoolStandardMappingId,cancellationToken);

    public Task<bool> IsExistsAsync(Guid schoolRegistrationId, int standardId, OperationType operation, Guid? schoolStandardMappingId, CancellationToken cancellationToken)
    =>  context.SchoolStandardMappings.AnyAsync(
            x => x.SchoolRegistrationId == schoolRegistrationId
              && x.StandardId == standardId
              && (schoolStandardMappingId == null ||
                  x.SchoolStandardMappingId != schoolStandardMappingId),
            cancellationToken);
    
}