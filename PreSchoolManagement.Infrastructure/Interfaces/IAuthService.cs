using PreSchoolManagement.Domain.Dtos;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<UserDetailsMaster?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<UserDetailsMaster?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> CreateUserAsync(UserDetailsMaster user, string password, CancellationToken cancellationToken);
    Task<AuthTokenResponse?> LoginAsync(string userName, string password, CancellationToken cancellationToken);
    Task<AuthTokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<AuthTokenResponse> ChangePasswordAsync(
    Guid userId,
    string currentPassword,
    string newPassword,
    CancellationToken cancellationToken);
    Task UpdateUserAsync(
    UserDetailsMaster user,
    CancellationToken cancellationToken);
}
