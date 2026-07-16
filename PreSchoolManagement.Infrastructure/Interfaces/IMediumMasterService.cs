using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IMediumMasterService
{
    Task<List<MediumMaster>> GetAllAsync (CancellationToken cancellationToken);

    Task<MediumMaster?>GetByIdAsync (int id,CancellationToken cancellationToken);

    Task AddAsync (MediumMaster medium,CancellationToken cancellationToken);

    Task UpdateAsync(MediumMaster medium,CancellationToken cancellationToken);

    Task DeleteAsync(MediumMaster medium,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string medium,OperationType operation,int? MediumId,CancellationToken cancellationToken);
}