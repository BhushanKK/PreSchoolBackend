using MediatR;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;

namespace PreSchoolManagement.Api.Endpoints;

public static class AuthApi
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        group.MapPost("/register", Register)
            .WithName("RegisterUser")
            .WithSummary("Register a new user")
            .WithDescription("Creates a new user and returns JWT tokens.");

        group.MapPost("/login", Login)
            .WithName("LoginUser")
            .WithSummary("Login user")
            .WithDescription("Authenticates a user and returns JWT tokens.");

        group.MapPost("/refresh", Refresh)
            .WithName("RefreshToken")
            .WithSummary("Refresh access token")
            .WithDescription("Refreshes the JWT token pair using a valid refresh token.");
 
        group.MapPost("/change-password", ChangePassword)
        .RequireAuthorization()
        .WithName("ChangePassword")
        .WithSummary("Change user password")
        .WithDescription("Changes the password of the authenticated user.");

        return app;
    }

    private static async Task<IResult> Register(RegisterRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RegisterUserCommand(request.UserName, request.Email, request.Password, request.RoleId,request.MobileNumber), cancellationToken);
        return result.Success ? TypedResults.Ok(result) : Results.Json(result, statusCode: result.StatusCode);
    }

    private static async Task<IResult> Login(LoginRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new LoginUserCommand(request.UserName, request.Password), cancellationToken);
        return result.Success ? TypedResults.Ok(result) : Results.Json(result, statusCode: result.StatusCode);
    }

    private static async Task<IResult> Refresh(RefreshTokenRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RefreshTokenCommand(request.RefreshToken), cancellationToken);
        return result.Success ? TypedResults.Ok(result) : Results.Json(result, statusCode: result.StatusCode);
    }

    private static async Task<IResult> ChangePassword(
    ChangePasswordRequest request,
    HttpContext httpContext,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var userIdClaim = httpContext.User.FindFirst("userId")?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
            return TypedResults.Unauthorized();

        var result = await sender.Send(
            new ChangePasswordCommand(
                userId,
                request.CurrentPassword,
                request.NewPassword,
                request.ConfirmPassword),
            cancellationToken);

        return result.Success
            ? TypedResults.Ok(result)
            : Results.Json(result, statusCode: result.StatusCode);
    }
}
