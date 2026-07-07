using Serilog;
using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Infrastructure.Services;

public class CasteMasterService(ApplicationDbContext context) : ICasteMasterService
{
    public async Task<List<CasteMasterQueryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await (
            from caste in context.CasteMasters.AsNoTracking()
            join category in context.CategoryMasters.AsNoTracking()
            on caste.CategoryID equals category.CategoryId
            where caste.IsActive
            orderby category.CategoryId
            select new CasteMasterQueryDto
            {
                CasteId = caste.CasteID,
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Caste = caste.CasteName
            }).ToListAsync(cancellationToken);
    }


    public async Task<CasteMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.CasteMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await context.CasteMasters.AddAsync(caste, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding a caste master record.");
            throw;
        }
    }

    public async Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CasteMasters.Update(caste);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating a caste master record.");
            throw;
        }
    }

    public async Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            context.CasteMasters.Remove(caste);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting a caste master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken)
        => context.CasteMasters.AnyAsync(x => x.CasteName == caste && (casteId == null || x.CasteID != casteId), cancellationToken);
}
