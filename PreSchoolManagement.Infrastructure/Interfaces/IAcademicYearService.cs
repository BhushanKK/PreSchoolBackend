using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IAcademicYearMasterService
{
    Task<List<AcademicYearMaster>> GetAllAsync(bool filter = false, CancellationToken cancellationToken = default);
    Task<AcademicYearMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken);
    Task UpdateAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken);
    Task DeleteAsync(AcademicYearMaster academicYear, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string academicYear, OperationType operation, int? academicYearId,
        CancellationToken cancellationToken);
    Task<AcademicYearMaster?> GetForUpdateAsync(int id,CancellationToken cancellationToken);
}