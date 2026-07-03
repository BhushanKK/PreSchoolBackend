using MediatR;
using PreSchoolManagement.Domain.ResponseModels;

namespace PreSchoolManagement.Application.Features.Commands;

public record DeleteRoleMasterCommand(int RoleId) : IRequest<ApiResponse<int>>;