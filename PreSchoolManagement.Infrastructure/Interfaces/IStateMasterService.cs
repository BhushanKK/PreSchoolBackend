using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IStateMasterService
{
    Task<PaginatedResult<StateMaster>> GetAllAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);
    Task<StateMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(StateMaster state,CancellationToken cancellationToken);
    Task UpdateAsync(StateMaster state,CancellationToken cancellationToken);
    Task DeleteAsync(StateMaster state,CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string state,OperationType operation,int? stateId, CancellationToken cancellationToken);
    Task<StateMaster?> GetForUpdateAsync (int id,CancellationToken cancellationToken);
}