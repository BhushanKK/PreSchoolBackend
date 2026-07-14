using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class EmployeeTypeMasterService(ApplicationDbContext context):IEmployeeTypeMasterService
{
    public async Task<List<EmployeeTypeMaster>> GetAllAsync(bool isFilter =false,CancellationToken cancellationToken= default)
    => await context.EmployeeTypeMasters
       .AsNoTracking()
       .Where(x => !isFilter || x.IsActive)
       .ToListAsync(cancellationToken);

    public Task<EmployeeTypeMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
    => context.EmployeeTypeMasters
        .AsNoTracking()
        .FirstOrDefaultAsync(x =>x.EmployeeTypeId== id, cancellationToken );

    public async Task AddAsync (EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.EmployeeTypeMasters.AddAsync(employeeTypeMaster,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occured when adding a employee type master record.");
            throw;
        }
    }

    public async Task UpdateAsync(EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.EmployeeTypeMasters.Update(employeeTypeMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred  when updating a Employee Type Master record.");
            throw;
        }
    }

    public async Task DeleteAsync(EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.EmployeeTypeMasters.Remove(employeeTypeMaster);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex,"An error occurred while deleting a employee type master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string employeeTypeName,OperationType operation,
        int? employeeTypeId,
        CancellationToken cancellationToken)
        => context.EmployeeTypeMasters.AnyAsync(
            x => x.EmployeeTypeName == employeeTypeName &&
                 (employeeTypeId == null || x.EmployeeTypeId != employeeTypeId),
            cancellationToken);
}