using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ICasteMasterService
{
    Task<PaginatedResult<CasteMasterQueryDto>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default);
    Task<CasteMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(CasteMaster caste, CancellationToken cancellationToken);
    Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken);
    Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken);
    Task<CasteMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}
