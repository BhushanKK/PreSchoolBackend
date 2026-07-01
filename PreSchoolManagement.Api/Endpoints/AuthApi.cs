using MediatR;
using SchoolAdmission.Application.Features.Auth.Commands;
using SchoolAdmission.Domain.Dtos;

namespace SchoolAdmission.Api.Endpoints;

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

        return app;
    }

    private static async Task<IResult> Register(RegisterRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RegisterUserCommand(request.UserName, request.Email, request.Password, request.RoleId), cancellationToken);
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
}
