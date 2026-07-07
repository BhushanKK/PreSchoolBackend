using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IMenuMasterService
{
    Task<List<MenuMasterQueryDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<MenuMaster?> GetByIdAsync(int menuId, CancellationToken cancellationToken);
    Task<MenuMaster> CreateAsync(MenuMaster menuMaster, CancellationToken cancellationToken);
    Task<MenuMaster?> UpdateAsync(MenuMaster menuMaster, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int menuId, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string menuName, int? parentMenuId, OperationType operationType, int? menuId,
    CancellationToken cancellationToken);
    Task<List<ParentMenuDto>> GetParentMenusAsync(CancellationToken cancellationToken);
}
