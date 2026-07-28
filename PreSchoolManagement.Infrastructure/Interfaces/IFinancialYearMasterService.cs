using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IFinancialYearMasterService
{
    Task<PaginatedResult<FinancialYearMaster>> GetAllAsync(
        PaginationRequest request, 
        CancellationToken cancellationToken);
    Task<FinancialYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task UpdateAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task DeleteAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string financialYear, OperationType operation, int? financialYearId,
        CancellationToken cancellationToken);
    Task<FinancialYearMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);    
}