using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IDesignationMasterService
{
    Task<PaginatedResult<DesignationMaster>> GetAllAsync (
        PaginationRequest request, 
        CancellationToken cancellationToken);
    Task<DesignationMaster?>GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(DesignationMaster designationMaster,CancellationToken cancellationToken);
    Task UpdateAsync(DesignationMaster designationMaster,CancellationToken cancellationToken);
    Task DeleteAsync(DesignationMaster designationMaster, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string designation,OperationType operation,int? designationId,CancellationToken cancellationToken);
    Task<DesignationMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}