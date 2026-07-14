using System.Net;
using MediatR;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class ChangePasswordHandler(IAuthService authService)
    : IRequestHandler<ChangePasswordCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.ConfirmPassword)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse(
                "New password and confirm password do not match.",
                (int)HttpStatusCode.BadRequest);
        }

        var result = await authService.ChangePasswordAsync(
            request.UserId,
            request.CurrentPassword,
            request.NewPassword,
            cancellationToken);

        if (!result.Success)
        {
            return ApiResponse<AuthTokenResponse>.FailureResponse(
                result.Message,
                (int)HttpStatusCode.BadRequest,
                result);
        }

        return ApiResponse<AuthTokenResponse>.SuccessResponse(
            result,
            result.Message,
            (int)HttpStatusCode.OK);
    }
}