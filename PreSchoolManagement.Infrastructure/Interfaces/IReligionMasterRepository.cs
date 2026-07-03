using SchoolAdmission.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Interfaces;

public interface IReligionMasterService
{
    Task<List<ReligionMaster>> GetAllAsync(CancellationToken cancellationToken);

    Task<ReligionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task UpdateAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task DeleteAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string Religion, OperationType operation, int? ReligionId, CancellationToken cancellationToken);
}
