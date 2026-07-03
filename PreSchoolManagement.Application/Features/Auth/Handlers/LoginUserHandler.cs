using MediatR;
using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Application.Features.Auth.Commands;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Handlers;

public class LoginUserHandler(IAuthService authService) 
    : IRequestHandler<LoginUserCommand, ApiResponse<AuthTokenResponse>>
{
    public async Task<ApiResponse<AuthTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.UserName, request.Password, cancellationToken);

        return result is null
            ? ApiResponse<AuthTokenResponse>.FailureResponse("Invalid username or password", 401)
            : ApiResponse<AuthTokenResponse>.SuccessResponse(result, "Login successful", 200);
    }
}
