using PreSchoolManagement.Infrastructure.Interfaces;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?
                .User
                .FindFirst("userId")?.Value;

            return Guid.TryParse(value, out var id) ? id : null;
        }
    }

    public string? UserName =>
        httpContextAccessor.HttpContext?.User.FindFirst("userName")?.Value;

    public int RoleId =>
    int.TryParse(
        httpContextAccessor.HttpContext?.User.FindFirst("roleId")?.Value,
        out var roleId)
            ? roleId
            : 0;
}