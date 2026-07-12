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

namespace PreSchoolManagement.Infrastructure.Services;

public class AuthService(ApplicationDbContext context, IConfiguration configuration) : IAuthService
{
    public async Task<UserDetailsMaster?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        => await context.Set<UserDetailsMaster>()
            .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

    public async Task<UserDetailsMaster?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context.Set<UserDetailsMaster>()
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
            user.JwtTokenVersion = 1;
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

        if (user is null || !user.IsActive || user.IsDeleted)
            return null;

        if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            return null;

        var response = GenerateTokenResponse(user);

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.LastLoginDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return response;
    }

    public async Task<AuthTokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await context.Set<UserDetailsMaster>()
            .FirstOrDefaultAsync(x =>
                x.RefreshToken == refreshToken &&
                x.RefreshTokenExpiry > DateTime.UtcNow,
                cancellationToken);

        if (user is null)
            return null;

        var response = GenerateTokenResponse(user);

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await context.SaveChangesAsync(cancellationToken);

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
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
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
            expires: DateTime.UtcNow.AddMinutes(60),
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
}

