using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IDistrictMasterService
{
    Task<List<DistrictMasterQueryDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<DistrictMaster?> GetByIdAsync (int id, CancellationToken cancellationToken);

    Task AddAsync(DistrictMaster district, CancellationToken cancellationToken);

    Task UpdateAsync(DistrictMaster district, CancellationToken cancellationToken);

    Task DeleteAsync(DistrictMaster district,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string district, OperationType operation , int? districtId,CancellationToken cancellationToken);
}