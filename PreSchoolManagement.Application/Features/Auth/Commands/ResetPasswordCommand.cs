using MediatR;

namespace PreSchoolManagement.Application.Features.Auth.Commands;

public record ResetPasswordCommand(
    string Token,
    string Password,
    string ConfirmPassword)
    : IRequest<bool>;