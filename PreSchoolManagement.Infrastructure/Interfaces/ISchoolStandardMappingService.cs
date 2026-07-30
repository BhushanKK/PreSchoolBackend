using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ISchoolStandardMappingService
{
    Task<PaginatedResult<SchoolStandardMapping>> GetAllAsync(PaginationRequest request,CancellationToken cancellationToken);

    Task<SchoolStandardMapping?> GetByIdAsync(Guid id,CancellationToken cancellationToken);

    Task AddAsync(SchoolStandardMapping schoolStandardMapping,CancellationToken cancellationToken);

    Task DeleteAsync(SchoolStandardMapping schoolStandardMapping,CancellationToken cancellationToken);

    Task UpdateAsync(SchoolStandardMapping schoolStandardMapping, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(Guid schoolRegistrationId,int standardId,OperationType operation,
        Guid? schoolStandardMappingId,
        CancellationToken cancellationToken);

    Task<SchoolStandardMapping?> GetForUpdateAsync(Guid schoolStandardMappingId,CancellationToken cancellationToken);
}