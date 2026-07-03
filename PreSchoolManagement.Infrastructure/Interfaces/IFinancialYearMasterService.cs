using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IFinancialYearMasterService
{
    Task<List<FinancialYearMaster>> GetAllAsync(CancellationToken cancellationToken);
    Task<FinancialYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task UpdateAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task DeleteAsync(FinancialYearMaster financialYear, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string financialYear, OperationType operation, int? financialYearId, 
        CancellationToken cancellationToken);   
}