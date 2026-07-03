using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class AcademicYearMasterService(ApplicationDbContext context) : IAcademicYearMasterService
{
    public Task<List<AcademicYearMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.AcademicYearMasters.ToListAsync(cancellationToken);

    public async Task<AcademicYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.AcademicYearMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.AcademicYearMasters.AddAsync(academicYear, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding academicYear master record.");
            throw;
        }
    }

    public async Task UpdateAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.AcademicYearMasters.Update(academicYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a academicYear master record.");
            throw;
        }
    }

    public async Task DeleteAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.AcademicYearMasters.Remove(academicYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a academicYear master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string academicYear, OperationType operation, int? academicYearId, CancellationToken cancellationToken)
        => context.AcademicYearMasters.AnyAsync(x => x.AcademicYearName == academicYear && (academicYearId == null || x.AcademicYearId != academicYearId), cancellationToken);
}
