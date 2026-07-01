using MediatR;
using SchoolAdmission.Application.Features.Auth.Commands;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Application.Features.Auth.Handlers;

public class RegisterUserHandler(IAuthService authService) : IRequestHandler<RegisterUserCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await authService.GetUserByUserNameAsync(request.UserName, cancellationToken);
        if (existingUser is not null)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse("User already exists", 409);
        }

        var user = new UserDetailsMaster
        {
            UserId = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            RoleId = request.RoleId,
            IsActive = true,
            IsDeleted = false,
            FailedLoginAttempts = 0,
            JwtTokenVersion = 1
        };

        var created = await authService.CreateUserAsync(user, request.Password, cancellationToken);
        if (!created)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse("Unable to create user", 500);
        }

        var loginResult = await authService.LoginAsync(request.UserName, request.Password, cancellationToken);
        return loginResult is null
            ? ApiResponse<AuthTokenResponse>.FailureResponse("User created but login failed", 500)
            : ApiResponse<AuthTokenResponse>.SuccessResponse(loginResult, "User registered successfully", 200);
    }
}
