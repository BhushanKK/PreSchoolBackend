using System.Security.Claims;

namespace PreSchoolManagement.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
        => user.FindFirst("userId")?.Value;
    
    public static string? GetUserName(this ClaimsPrincipal user)
        => user.FindFirst("userName")?.Value;

    public static int? GetRoleId(this ClaimsPrincipal user)
    {
        var role = user.FindFirst("roleId")?.Value;
        return int.TryParse(role, out var roleId)
            ? roleId
            : null;
    }

    public static string? GetEmail(this ClaimsPrincipal user)
        => user.FindFirst("email")?.Value;
    
    public static string? GetMobileNumber(this ClaimsPrincipal user)
        => user.FindFirst("mobileNo")?.Value;
    
}