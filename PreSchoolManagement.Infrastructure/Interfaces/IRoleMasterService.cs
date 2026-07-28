using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IRoleMasterService
{
    Task<PaginatedResult<RoleMaster>> GetAllAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);
    Task<RoleMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<RoleDropdownDto>> GetActiveRolesAsync(CancellationToken cancellationToken); //for Dropdown
    Task AddAsync(RoleMaster role, CancellationToken cancellationToken);
    Task UpdateAsync(RoleMaster role, CancellationToken cancellationToken);
    Task DeleteAsync(RoleMaster role, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string RoleName, OperationType operation, int? RoleId, CancellationToken cancellationToken);
    Task<RoleMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}
