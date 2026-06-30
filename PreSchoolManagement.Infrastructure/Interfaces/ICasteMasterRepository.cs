using SchoolAdmission.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Interfaces;

public interface ICasteMasterService
{
    Task<List<CasteMaster>> GetAllAsync(CancellationToken cancellationToken);

    Task<CasteMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(CasteMaster caste, CancellationToken cancellationToken);

    Task UpdateAsync(CasteMaster caste, CancellationToken cancellationToken);

    Task DeleteAsync(CasteMaster caste, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string caste, OperationType operation, int? casteId, CancellationToken cancellationToken);
}
