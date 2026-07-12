using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class RefreshTokenHandler(IAuthService authService) 
    : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        return result is null
            ? ApiResponse<AuthTokenResponse>.FailureResponse("Invalid or expired refresh token", 401)
            : ApiResponse<AuthTokenResponse>.SuccessResponse(result, "Token refreshed successfully", 200);
    }
}
