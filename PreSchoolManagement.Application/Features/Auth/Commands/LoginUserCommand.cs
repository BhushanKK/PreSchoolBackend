using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Commands;

public sealed record LoginUserCommand(string UserName, string Password) : IRequest<ApiResponse<AuthTokenResponse>>;
