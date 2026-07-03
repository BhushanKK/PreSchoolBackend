using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IRoleMasterService
{
    Task<List<RoleMaster>> GetAllAsync(CancellationToken cancellationToken);

    Task<RoleMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(RoleMaster role, CancellationToken cancellationToken);

    Task UpdateAsync(RoleMaster role, CancellationToken cancellationToken);

    Task DeleteAsync(RoleMaster role, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string RoleName, OperationType operation, int? RoleId, CancellationToken cancellationToken);
}
