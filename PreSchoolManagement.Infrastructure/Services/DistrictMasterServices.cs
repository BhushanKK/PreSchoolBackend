using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Infrastructure.Services;

public class DistrictMasterServices(ApplicationDbContext context) : IDistrictMasterService
{
    public async Task<List<DistrictMasterQueryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await(
            from district in context.DistrictMasters.AsNoTracking()
            join State in context.StateMasters.AsNoTracking()
            on district.StateId equals State.StateId
            orderby State.StateId
            select new DistrictMasterQueryDto
            {
                DistrictId = district.DistrictId,
                StateId = State.StateId,
                DistrictName = district.DistrictName,
                IsActive = district.IsActive
            }).ToListAsync(cancellationToken);
        
    }

    public async Task<DistrictMaster?> GetByIdAsync(int id,CancellationToken cancellationToken)
        => await context.DistrictMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(DistrictMaster district, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.DistrictMasters.AddAsync(district,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a district master record.");
            throw;
        }
    }

    public async Task UpdateAsync(DistrictMaster district,CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DistrictMasters.Update(district);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a district master record.");
            throw;
        }
    }

    public async Task DeleteAsync(DistrictMaster district, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.DistrictMasters.Remove(district);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a district master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string district, OperationType operation, int? districtId, CancellationToken cancellationToken)
        => context.DistrictMasters.AnyAsync(x =>x.DistrictName == district && (districtId == null || x.DistrictId != districtId), cancellationToken);
}