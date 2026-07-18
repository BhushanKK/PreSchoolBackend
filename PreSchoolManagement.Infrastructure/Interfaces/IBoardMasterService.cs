using PreSchoolManagement.Domain.Utils;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IBoardMasterService
{
    Task<List<BoardMaster>> GetAllAsync(bool filter = false, CancellationToken cancellationToken = default);
    Task<BoardMaster?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(BoardMaster board, CancellationToken cancellationToken);
    Task UpdateAsync(BoardMaster board, CancellationToken cancellationToken);
    Task DeleteAsync(BoardMaster board, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(string BoardName, OperationType operation, int? BoardId, CancellationToken cancellationToken);
    Task<BoardMaster?> GetForUpdateAsync(int id, CancellationToken cancellationToken);
}