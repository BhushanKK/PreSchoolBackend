using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ICategoryMasterService
{
    Task<PaginatedResult<CategoryMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default);

    Task<CategoryMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(CategoryMaster category, CancellationToken cancellationToken);
    Task UpdateAsync(CategoryMaster category, CancellationToken cancellationToken);
    Task DeleteAsync(CategoryMaster category, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(
        string category,
        OperationType operation,
        int? categoryId,
        CancellationToken cancellationToken);
    Task<CategoryMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}