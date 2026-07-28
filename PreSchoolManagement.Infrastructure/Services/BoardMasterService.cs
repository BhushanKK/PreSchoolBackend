using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Domain.Models;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Shared.Common;
using SchoolManagement.Domain.Entities;
using Serilog;

namespace PreSchoolManagement.Infrastructure.Services;

public class BoardMasterService(
    ApplicationDbContext context,
    ILanguageService languageService) : IBoardMasterService
{
    public async Task<PaginatedResult<BoardMaster>> GetAllAsync(
        PaginationRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<BoardMaster> query = context.BoardMasters
            .AsNoTracking()
            .Include(x => x.Translations);

        if (request.Filter)
            query = query.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.BoardName.Contains(request.SearchText));

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.BoardId)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<BoardMaster>
        {
            Items = items
                .Select(x => MapBoard(x, languageService.CurrentLanguage))
                .ToList(),

            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<BoardMaster?> GetByIdAsync(int id,
        CancellationToken cancellationToken)
    {
        return await context.BoardMasters
            .AsNoTracking()
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.BoardId == id, cancellationToken);
    }

    public async Task AddAsync(BoardMaster board, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await context.BoardMasters.AddAsync(board, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while adding board master record.");
            throw;
        }
    }

    public async Task UpdateAsync(BoardMaster board, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.BoardMasters.Update(board);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while updating board master record.");
            throw;
        }
    }

    public async Task DeleteAsync(BoardMaster board, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            context.BoardMasters.Remove(board);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            Log.Error(ex, "An error occurred while deleting board master record.");
            throw;
        }
    }

    public Task<bool> IsExistsAsync(string boardName, OperationType operation, int? boardId, CancellationToken cancellationToken)
        => context.BoardMasters.AnyAsync(x => x.BoardName == boardName &&
        (boardId == null || x.BoardId != boardId),
        cancellationToken);

    public async Task<BoardMaster?> GetForUpdateAsync(int id,
    CancellationToken cancellationToken)
    => await context.BoardMasters
        .Include(x => x.Translations)
        .FirstOrDefaultAsync(x => x.BoardId == id, cancellationToken);

    private BoardMaster MapBoard(BoardMaster board, string language)
    {
        return new BoardMaster
        {
            BoardId = board.BoardId,
            BoardName = TranslationHelper.GetTranslatedValue(
                board.Translations,
                language,
                x => x.LanguageCode,
                x => x.BoardName,
                board.BoardName),
            IsActive = board.IsActive,
            Translations = board.Translations.ToList()
        };
    }
}