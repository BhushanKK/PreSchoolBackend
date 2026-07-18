using Microsoft.EntityFrameworkCore;
using PreSchoolManagement.Infrastructure.Data;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class PasswordResetService(ApplicationDbContext context) : IPasswordResetService
{
    public async Task CreateAsync(PasswordResetToken token)
    {
        await context.PasswordResetTokens.AddAsync(token);
        await context.SaveChangesAsync();
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
        => await context.PasswordResetTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    

    public async Task UpdateAsync(PasswordResetToken token)
    {
        context.PasswordResetTokens.Update(token);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int passwordResetTokenId)
    {
        var entity = await context.PasswordResetTokens
            .FirstOrDefaultAsync(x => x.PasswordResetTokenId == passwordResetTokenId);

        if (entity == null)
            return;

        context.PasswordResetTokens.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task InvalidateTokensAsync(Guid userId)
    {
        var tokens = await context.PasswordResetTokens
            .Where(x =>
                x.UserId == userId &&
                !x.IsUsed &&
                x.ExpiryDate > DateTime.UtcNow)
            .ToListAsync();

        if (!tokens.Any())
            return;

        foreach (var token in tokens)
        {
            token.IsUsed = true;
            token.UsedDate = DateTime.UtcNow;
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteExpiredTokensAsync()
    {
        var expiredTokens = await context.PasswordResetTokens
            .Where(x => x.ExpiryDate <= DateTime.UtcNow)
            .ToListAsync();

        if (!expiredTokens.Any())
            return;

        context.PasswordResetTokens.RemoveRange(expiredTokens);
        await context.SaveChangesAsync();
    }
}