using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using System.Net;

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
                (int)HttpStatusCode.InternalServerError);
        }

        if (!result.Success)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse(
                result.Message,
                result.IsLockedOut ? (int)HttpStatusCode.Locked : (int)HttpStatusCode.Unauthorized,
                result);
        }

        return ApiResponse<AuthTokenResponse>.SuccessResponse(
            result,
            result.Message,
            (int)HttpStatusCode.OK);
    }
}