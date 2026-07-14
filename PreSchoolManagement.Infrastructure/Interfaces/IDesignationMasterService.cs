using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IDesignationMasterService
{
    Task<List<DesignationMaster>> GetAllAsync (bool applyfilter,CancellationToken cancellationToken);
    Task<DesignationMaster?>GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(DesignationMaster designationMaster,CancellationToken cancellationToken);
    Task UpdateAsync(DesignationMaster designationMaster,CancellationToken cancellationToken);
    Task DeleteAsync(DesignationMaster designationMaster, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(String designation,OperationType operation,int? designationId,CancellationToken cancellationToken);
}