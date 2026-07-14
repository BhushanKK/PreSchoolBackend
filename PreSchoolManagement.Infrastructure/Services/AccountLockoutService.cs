using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Services;

public class AccountLockoutService : IAccountLockoutService
{
    public bool IsLocked(UserDetailsMaster user)
        => user.LockoutEnd.HasValue &&
               user.LockoutEnd.Value > DateTime.UtcNow;

    public bool IsLockExpired(UserDetailsMaster user)
        => user.LockoutEnd.HasValue &&
               user.LockoutEnd.Value <= DateTime.UtcNow;

    public void RegisterFailedAttempt(UserDetailsMaster user)
    {
        user.FailedLoginAttempts++;
        user.LastFailedLoginDate = DateTime.UtcNow;

        if (user.FailedLoginAttempts >= AuthConstants.MaxFailedLoginAttempts)
            user.LockoutEnd = DateTime.UtcNow.AddMinutes(AuthConstants.LockoutDurationMinutes);
    }

    public void Reset(UserDetailsMaster user)
    {
        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;
        user.LastFailedLoginDate = null;
    }

    public string GetRemainingAttemptsMessage(UserDetailsMaster user)
    {
        var remaining = AuthConstants.MaxFailedLoginAttempts - user.FailedLoginAttempts;

        return remaining <= 0
            ? "Account locked."
            : $"Invalid username or password. Remaining attempts: {remaining}";
    }

    public DateTime? GetLockoutEnd(UserDetailsMaster user)
    => user.LockoutEnd;
}