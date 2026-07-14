using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Infrastructure.Data;
using SchoolManagement.Domain.Entities;
using PreSchoolManagement.Shared.Utils;

namespace PreSchoolManagement.Infrastructure.Services;

public class AuthService(ApplicationDbContext context,
IConfiguration configuration, IAccountLockoutService accountLockoutService) : IAuthService
{
    public async Task<UserDetailsMaster?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        => await context.UserDetailsMasters
        .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

    public async Task<UserDetailsMaster?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context.UserDetailsMasters
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> CreateUserAsync(UserDetailsMaster user, string password, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHash = HashPassword(password, out var salt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = Convert.ToBase64String(salt);
            user.IsActive = true;
            user.IsDeleted = false;
            user.FailedLoginAttempts = 0;
            user.JwtTokenVersion = AuthConstants.JwtVersion;
            user.EntryDate = DateTime.UtcNow;

            await context.Set<UserDetailsMaster>().AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error creating auth user");
            return false;
        }
    }

    public async Task<AuthTokenResponse?> LoginAsync(string userName, string password, CancellationToken cancellationToken)
    {
        var user = await GetUserByUserNameAsync(userName, cancellationToken);

        if (user == null || !user.IsActive || user.IsDeleted)
        {
            return new AuthTokenResponse
            {
                Success = false,
                Message = AuthConstants.InvalidCredentials
            };
        }

        // Already locked?
        if (accountLockoutService.IsLocked(user))
        {
            return new AuthTokenResponse
            {
                Success = false,
                IsLockedOut = true,
                LockoutEnd = user.LockoutEnd,
                Message = $"Account is locked until {user.LockoutEnd!.Value.ToIndianDateTime()}"
            };
        }

        // Lock expired?
        if (accountLockoutService.IsLockExpired(user))
            accountLockoutService.Reset(user);

        // Invalid password
        if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        {
            accountLockoutService.RegisterFailedAttempt(user);

            await context.SaveChangesAsync(cancellationToken);

            return new AuthTokenResponse
            {
                Success = false,
                IsLockedOut = accountLockoutService.IsLocked(user),
                LockoutEnd = accountLockoutService.GetLockoutEnd(user),
                Message = accountLockoutService.GetRemainingAttemptsMessage(user)
            };
        }

        // Success
        accountLockoutService.Reset(user);
        user.LastLoginDate = DateTime.UtcNow;
        var response = GenerateTokenResponse(user);
        user.AccessToken = response.AccessToken;
        await context.SaveChangesAsync(cancellationToken);
        response.Success = true;
        response.Message = AuthConstants.LoginSuccessful;
        return response;
    }

    public async Task<AuthTokenResponse?> RefreshTokenAsync(
    string refreshToken,
    CancellationToken cancellationToken)
    {
        var token = await context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == refreshToken &&
                !x.IsRevoked &&
                x.ExpiryDate > DateTime.UtcNow,
                cancellationToken);

        if (token?.User == null ||
            !token.User.IsActive ||
            token.User.IsDeleted)
        {
            return new AuthTokenResponse
            {
                Success = false,
                Message = AuthConstants.InvalidCredentials
            };
        }

        var response = GenerateTokenResponse(token.User);

        // Revoke old refresh token
        token.IsRevoked = true;
        token.RevokedDate = DateTime.UtcNow;

        // Save new refresh token
        context.RefreshTokens.Add(new RefreshToken
        {
            UserId = token.UserId,
            Token = response.RefreshToken,
            ExpiryDate = DateTime.UtcNow.AddDays(AuthConstants.RefreshTokenExpiryDays),
            CreatedDate = DateTime.UtcNow
        });

        await context.SaveChangesAsync(cancellationToken);

        response.Success = true;
        response.Message = "Token refreshed successfully.";

        return response;
    }

    private AuthTokenResponse GenerateTokenResponse(UserDetailsMaster user)
    {
        var accessToken = GenerateJwtToken(user);
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        return new AuthTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(AuthConstants.TokenDuration)
        };
    }

    private string GenerateJwtToken(UserDetailsMaster user)
    {
        var claims = new List<Claim>
        {
            new("userId", user.UserId.ToString()),
            new("userName", user.UserName ?? string.Empty),
            new("roleId", user.RoleId.ToString()),
            new("email", user.Email ?? string.Empty),
            new("mobileNo", user.MobileNumber ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(AuthConstants.TokenDuration),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(16);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);

        return Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string password, string storedHash, string? storedSalt)
    {
        if (string.IsNullOrWhiteSpace(storedSalt))
            return false;

        var salt = Convert.FromBase64String(storedSalt);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);

        return Convert.ToBase64String(hash) == storedHash;
    }

    public async Task<AuthTokenResponse> ChangePasswordAsync(
    Guid userId,
    string currentPassword,
    string newPassword,
    CancellationToken cancellationToken)
    {
        var user = await context.UserDetailsMasters
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (user == null || !user.IsActive || user.IsDeleted)
        {
            return new AuthTokenResponse
            {
                Success = false,
                Message = "User not found."
            };
        }

        // Verify current password
        if (!VerifyPassword(currentPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new AuthTokenResponse
            {
                Success = false,
                Message = "Current password is incorrect."
            };
        }

        // Prevent using the same password
        if (VerifyPassword(newPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new AuthTokenResponse
            {
                Success = false,
                Message = "New password cannot be the same as the current password."
            };
        }

        // Generate new hash & salt
        var hash = HashPassword(newPassword, out var salt);

        user.PasswordHash = hash;
        user.PasswordSalt = Convert.ToBase64String(salt);

        // Invalidate existing sessions
        user.JwtTokenVersion++;
       
        var tokens = await context.RefreshTokens
        .Where(x =>
        x.UserId == userId &&
        !x.IsRevoked &&
        x.ExpiryDate > DateTime.UtcNow)
        .ToListAsync(cancellationToken);

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
            token.RevokedDate = DateTime.UtcNow;
        }
        user.AccessToken = null;

        user.ModifyBy = userId;
        user.ModifyDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return new AuthTokenResponse
        {
            Success = true,
            Message = "Password changed successfully."
        };
    }
}
