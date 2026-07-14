using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IEmployeeTypeMasterService
{
    Task<List<EmployeeTypeMaster>> GetAllAsync (bool isFilter =false,CancellationToken cancellationToken= default);

    Task<EmployeeTypeMaster?> GetByIdAsync(int id,CancellationToken cancellationToken);

    Task AddAsync(EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken);

    Task UpdateAsync(EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken);

    Task DeleteAsync(EmployeeTypeMaster employeeTypeMaster,CancellationToken cancellationToken);

    Task<bool> IsExistsAsync(string EmployeeTypeName,OperationType operation, int?EmployeeTypeId, CancellationToken cancellationToken);
}