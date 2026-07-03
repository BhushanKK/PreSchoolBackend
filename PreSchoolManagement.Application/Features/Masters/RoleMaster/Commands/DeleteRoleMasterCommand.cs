using MediatR;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Application.Features.Commands;

public record DeleteRoleMasterCommand(int RoleId) : IRequest<ApiResponse<int>>;