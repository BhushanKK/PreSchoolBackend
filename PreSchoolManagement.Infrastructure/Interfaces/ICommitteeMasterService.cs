using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ICommitteeMasterService
{
    Task<PaginatedResult<CommitteeMaster>> GetAllAsync(
    PaginationRequest request, 
    CancellationToken cancellationToken = default);
    Task<CommitteeMaster?> GetByIdAsync(Guid committeeId, CancellationToken cancellationToken);
    Task AddAsync(CommitteeMaster committee, CancellationToken cancellationToken);
    Task UpdateAsync(CommitteeMaster committee, CancellationToken cancellationToken);
    Task DeleteAsync(CommitteeMaster committee, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string committeeName, OperationType operation, Guid? committeeId, CancellationToken cancellationToken);
}