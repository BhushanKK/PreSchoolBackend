using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IAccountLockoutService
{
    bool IsLocked(UserDetailsMaster user);
    bool IsLockExpired(UserDetailsMaster user);
    void RegisterFailedAttempt(UserDetailsMaster user);
    void Reset(UserDetailsMaster user);
    string GetRemainingAttemptsMessage(UserDetailsMaster user);
    DateTime? GetLockoutEnd(UserDetailsMaster user);
}