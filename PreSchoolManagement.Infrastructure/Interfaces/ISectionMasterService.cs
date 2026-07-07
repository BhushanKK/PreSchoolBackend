using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ISectionMasterService
{
    Task<List<SectionMaster>> GetAllAsync(CancellationToken cancellationToken);

    Task<SectionMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(SectionMaster Section, CancellationToken cancellationToken);

    Task UpdateAsync(SectionMaster Section, CancellationToken cancellationToken);

    Task DeleteAsync(SectionMaster Section, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string SectionName, OperationType operation, int? SectionId, CancellationToken cancellationToken);
}
