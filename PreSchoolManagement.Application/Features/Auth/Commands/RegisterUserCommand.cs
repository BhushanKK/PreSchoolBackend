using MediatR;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Auth.Commands;

public sealed record RegisterUserCommand(string UserName, string Email, string Password, int RoleId) : IRequest<ApiResponse<AuthTokenResponse>>;
