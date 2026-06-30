using Microsoft.EntityFrameworkCore;
using SchoolAdmission.Domain.Utils;
using SchoolAdmission.Infrastructure.Data;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Services;

public class CasteMasterService(ApplicationDbContext context) : ICasteMasterService
{
    private readonly ApplicationDbContext _context = context;

    public Task<List<CasteMaster>> GetAllAsync(CancellationToken cancellationToken)
        => _context.CasteMasters.ToListAsync(cancellationToken);

    public async Task<CasteMaster?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.CasteMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _context.CasteMasters.AddAsync(caste, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _context.CasteMasters.Update(caste);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _context.CasteMasters.Remove(caste);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken)
        => _context.CasteMasters.AnyAsync(x => x.CasteName == caste && (casteId == null || x.CasteID != casteId), cancellationToken);
}
