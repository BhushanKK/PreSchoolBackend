using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class LoginUserHandler(IAuthService authService)
    : IRequestHandler<LoginUserCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(
            request.UserName,
            request.Password,
            cancellationToken);

        if (result is null)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse(
                "Login failed.",
                500);
        }

        if (!result.Success)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse(
                result.Message,
                result.IsLockedOut ? 423 : 401,
                result);
        }

        return ApiResponse<AuthTokenResponse>.SuccessResponse(
            result,
            result.Message,
            200);
    }
}