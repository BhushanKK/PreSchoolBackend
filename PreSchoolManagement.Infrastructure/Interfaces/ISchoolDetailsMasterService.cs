using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;
public interface ISchoolDetailsMasterService
{
    Task<PaginatedResult<SchoolDetailsMaster>> GetAllAsync (PaginationRequest request,CancellationToken cancellationToken);

    Task<SchoolDetailsMaster?> GetByIdAsync(Guid id,CancellationToken cancellationToken);

    Task AddAsync(SchoolDetailsMaster schoolDetails,CancellationToken cancellationToken);
    
    Task UpdateAsync(SchoolDetailsMaster schoolDetailsMaster,CancellationToken cancellationToken);

    Task DeleteAsync(SchoolDetailsMaster schoolDetailsMaster,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string SchoolName,OperationType operation,Guid? SchoolDetailsId,CancellationToken cancellationToken);

    Task<SchoolDetailsMaster?>GetForUpdateAsync(Guid id, CancellationToken cancellationToken);

}