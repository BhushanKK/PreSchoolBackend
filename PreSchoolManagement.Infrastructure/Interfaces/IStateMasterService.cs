using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IStateMasterService
{
    Task<List<StateMaster>> GetAllAsync(bool applyFilter = false, CancellationToken cancellationToken=default);

    Task<StateMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(StateMaster state,CancellationToken cancellationToken);

    Task UpdateAsync(StateMaster state,CancellationToken cancellationToken);

    Task DeleteAsync(StateMaster state,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(String state,OperationType operation,int? stateId, CancellationToken cancellationToken);
}