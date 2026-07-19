using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IReligionMasterService
{
    Task<List<ReligionMaster>> GetAllAsync(bool filter = false, CancellationToken cancellationToken = default);

    Task<ReligionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task UpdateAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task DeleteAsync(ReligionMaster religion, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string Religion, OperationType operation, int? ReligionId, CancellationToken cancellationToken);

    Task<ReligionMaster?> GetForUpdateAsync (int id,CancellationToken cancellationToken);
}
