using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class SectionMasterService(ApplicationDbContext context) : ISectionMasterService
{
    public Task<List<SectionMaster>> GetAllAsync(CancellationToken cancellationToken)
        => context.SectionMasters.ToListAsync(cancellationToken);

    public async Task<SectionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.SectionMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.SectionMasters.AddAsync(Section, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding Section master record.");
            throw;
        }
    }

    public async Task UpdateAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SectionMasters.Update(Section);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating Section master record.");
            throw;
        }
    }

    public async Task DeleteAsync(SectionMaster Section, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.SectionMasters.Remove(Section);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting Section master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(
        string SectionName,
        OperationType operation,
        int? SectionId,
        CancellationToken cancellationToken)
        => context.SectionMasters.AnyAsync(
            x => x.SectionName == SectionName &&
                 (SectionId == null || x.SectionId != SectionId),
            cancellationToken);
}