using MediatR;
using SchoolAdmission.Application.Features.Auth.Commands;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;

namespace SchoolAdmission.Application.Features.Auth.Handlers;

public class RefreshTokenHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        return result is null
            ? ApiResponse<AuthTokenResponse>.FailureResponse("Invalid or expired refresh token", 401)
            : ApiResponse<AuthTokenResponse>.SuccessResponse(result, "Token refreshed successfully", 200);
    }
}
