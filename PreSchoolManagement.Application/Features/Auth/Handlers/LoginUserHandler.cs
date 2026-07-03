using MediatR;
using SchoolAdmission.Application.Features.Auth.Commands;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;
using SchoolAdmission.Infrastructure.Interfaces;

namespace SchoolAdmission.Application.Features.Auth.Handlers;

public class LoginUserHandler(IAuthService authService) : IRequestHandler<LoginUserCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.UserName, request.Password, cancellationToken);

        return result is null
            ? ApiResponse<AuthTokenResponse>.FailureResponse("Invalid username or password", 401)
            : ApiResponse<AuthTokenResponse>.SuccessResponse(result, "Login successful", 200);
    }
}
