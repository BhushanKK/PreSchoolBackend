using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IChairmanMasterService
{
    Task<PaginatedResult<ChairmanMasterQueryDto>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default);
    Task<ChairmanMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ChairmanDropdownDto>> GetActiveChairmansAsync(CancellationToken cancellationToken); //for Dropdown
    Task AddAsync(ChairmanMaster Chairman, CancellationToken cancellationToken);
    Task UpdateAsync(ChairmanMaster Chairman, CancellationToken cancellationToken);
    Task DeleteAsync(ChairmanMaster Chairman, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string Chairman, OperationType operation, int? ChairmanId, CancellationToken cancellationToken);
    Task<ChairmanMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}
