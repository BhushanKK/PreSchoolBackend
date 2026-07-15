using MediatR;

namespace PreSchoolManagement.Application.Features.Auth.Commands;
public record ForgotPasswordCommand(string Email)
    : IRequest<bool>;