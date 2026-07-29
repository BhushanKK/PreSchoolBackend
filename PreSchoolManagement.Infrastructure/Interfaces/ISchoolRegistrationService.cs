using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ISchoolRegistrationService
{
    Task<PaginatedResult<SchoolRegistration>> GetAllAsync (PaginationRequest request,CancellationToken cancellationToken);

    Task<SchoolRegistration?> GetByIdAsync(Guid id,CancellationToken cancellationToken);

    Task AddAsync(SchoolRegistration schoolRegistration,CancellationToken cancellationToken);

    Task UpdateAsync(SchoolRegistration schoolRegistration,CancellationToken cancellationToken);

    Task DeleteAsync(SchoolRegistration schoolRegistration,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string SchoolName,OperationType operation ,Guid? SchoolRegistrationId,CancellationToken cancellationToken);

    Task<SchoolRegistration?> GetForUpdateAsync(Guid id,CancellationToken cancellationToken);
}