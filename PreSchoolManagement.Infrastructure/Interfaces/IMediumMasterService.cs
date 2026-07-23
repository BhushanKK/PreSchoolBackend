using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IMediumMasterService
{
    Task<PaginatedResult<MediumMaster>> GetAllAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);

    Task<MediumMaster?>GetByIdAsync (int id,CancellationToken cancellationToken);

    Task AddAsync (MediumMaster medium,CancellationToken cancellationToken);

    Task UpdateAsync(MediumMaster medium,CancellationToken cancellationToken);

    Task DeleteAsync(MediumMaster medium,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string medium,OperationType operation,int? MediumId,CancellationToken cancellationToken);

    Task<MediumMaster?> GetForUpdateAsync (int id,CancellationToken cancellationToken);
}