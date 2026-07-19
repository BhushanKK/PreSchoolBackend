using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IStandardMasterService
{
    Task<List<StandardMaster>> GetAllAsync(bool filter = false, CancellationToken cancellationToken = default);

    Task<StandardMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(StandardMaster Standard, CancellationToken cancellationToken);

    Task UpdateAsync(StandardMaster Standard, CancellationToken cancellationToken);

    Task DeleteAsync(StandardMaster Standard, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string StandardName, OperationType operation, int? StandardId, CancellationToken cancellationToken);

    Task<StandardMaster?> GetForUpdateAsync (int id,CancellationToken cancellationToken);
}
