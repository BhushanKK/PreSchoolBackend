using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IDivisionMasterService
{
    Task<List<DivisionMaster>> GetAllAsync(bool isFilter = false, CancellationToken cancellationToken = default);
    Task<DivisionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(DivisionMaster Division, CancellationToken cancellationToken);
    Task UpdateAsync(DivisionMaster Division, CancellationToken cancellationToken);
    Task DeleteAsync(DivisionMaster Division, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string DivisionName, OperationType operation, int? DivisionId, CancellationToken cancellationToken);
    Task<DivisionMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}
