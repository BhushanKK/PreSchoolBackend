using MediatR;
using PreSchoolManagement.Domain.Dtos;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Auth.Commands;

public sealed record RegisterUserCommand(
string UserName,
string Email,
string Password,
int RoleId,
string MobileNumber)
: IRequest<ApiResponse<AuthTokenResponse>>;
