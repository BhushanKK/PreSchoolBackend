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

    public async Task<CasteMaster?> GetByIdWithCategoryAsync(int id, CancellationToken cancellationToken)
        => await _context.CasteMasters.FindAsync([id], cancellationToken);

    public async Task AddAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        await _context.CasteMasters.AddAsync(caste, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        _context.CasteMasters.Update(caste);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken)
    {
        _context.CasteMasters.Remove(caste);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken)
        => _context.CasteMasters.AnyAsync(x => x.CasteName == caste && (casteId == null || x.CasteID != casteId), cancellationToken);
}
