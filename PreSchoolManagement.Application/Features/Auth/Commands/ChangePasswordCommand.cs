using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Commands;

public record ChangePasswordCommand(
    Guid UserId,
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword)
    : IRequest<ApiResponse<AuthTokenResponse>>;