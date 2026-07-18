using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IPasswordResetService
{
    Task CreateAsync(PasswordResetToken token);

    Task<PasswordResetToken?> GetByTokenAsync(string token);

    Task UpdateAsync(PasswordResetToken token);

    Task DeleteAsync(int passwordResetTokenId);

    Task InvalidateTokensAsync(Guid userId);

    Task DeleteExpiredTokensAsync();
}