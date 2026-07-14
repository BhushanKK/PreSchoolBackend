namespace PreSchoolManagement.Domain.Dtos;

public class LoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; } = 1;
    public string MobileNumber { get; set; } = string.Empty;
}

public class AuthTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsLockedOut { get; set; }
    public DateTime? LockoutEnd { get; set; }
}

public static class AuthConstants
{
    public const int MaxFailedLoginAttempts = 3;
    public const int JwtVersion=1;
    public const int LockoutDurationMinutes = 15;
    public const int RefreshTokenExpiryDays=7;
    public const int TokenDuration=60;
    public const string LoginSuccessful = "Login successful.";
    public const string InvalidCredentials = "Invalid username or password.";
    public const string AccountLocked = "Account locked due to multiple failed login attempts.";
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}