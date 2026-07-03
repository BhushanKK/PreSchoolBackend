using MediatR;
using SchoolAdmission.Domain.Dtos;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Auth.Commands;

public sealed record LoginUserCommand(string UserName, string Password) : IRequest<ApiResponse<AuthTokenResponse>>;
