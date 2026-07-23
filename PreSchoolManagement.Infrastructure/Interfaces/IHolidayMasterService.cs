using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IHolidayMasterService
{
    Task<PaginatedResult<HolidayMaster>> GetAllAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);

    Task<HolidayMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAsync(HolidayMaster Holiday, CancellationToken cancellationToken);

    Task UpdateAsync(HolidayMaster Holiday, CancellationToken cancellationToken);

    Task DeleteAsync(HolidayMaster Holiday, CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string HolidayName, OperationType operation, int? HolidayId, CancellationToken cancellationToken);

    Task<HolidayMaster?> GetForUpdateAsync (int id,CancellationToken cancellationToken);
}
